using System;
using System.IO;
using System.Runtime.CompilerServices;
#if NET5_0_OR_GREATER
using System.Runtime.InteropServices;
#endif

namespace Mirai.CSharp.VoiceConverter
{
    public unsafe interface ISilkLameCoder : IDisposable
    {
        byte[] EncodeMp3ToSilk(ReadOnlySpan<byte> buffer);

        byte[] DecodeSilkToMp3(ReadOnlySpan<byte> buffer, int samplerate);

        int TryEncodeMp3ToSilk(ReadOnlySpan<byte> buffer, out byte[]? result);

        int TryEncodeMp3ToSilk(ReadOnlySpan<byte> buffer, out byte* resultPtr, out int resultSize);

        int TryDecodeSilkToMp3(ReadOnlySpan<byte> buffer, int samplerate, out byte[]? result);

        int TryDecodeSilkToMp3(ReadOnlySpan<byte> buffer, int samplerate, out byte* resultPtr, out int resultSize);
    }

    public unsafe partial class SilkLameCoder : ISilkLameCoder
    {
        protected readonly void* _globalLameFlag;

#if NETSTANDARD2_0
        protected readonly void* _loadedSilkLame;
#endif

        public SilkLameCoder()
        {
#if NETSTANDARD2_0
            _loadedSilkLame = LoadSilklame();
#endif
            void* glf = NativeHelper.CreateDefaultLameFlag();
            if (glf == null)
            {
                throw new OutOfMemoryException();
            }
            _globalLameFlag = glf;
        }

        ~SilkLameCoder()
        {
            Dispose(false);
        }

#if NETSTANDARD2_0
        private void* LoadSilklame()
        {
            if (Environment.Version is not { Major: 4, Minor: 0, Build: 30319 })
            {
                return null;
            }
            string path = IntPtr.Size == 4 ? $@"x86\{NativeHelper.SilklameName}.dll" : $@"x64\{NativeHelper.SilklameName}.dll";
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Unable to find native asset.", path);
            }
            void* ptr;
            if ((ptr = NativeHelper.Win32.LoadLibrary(path)) == default)
            {
                throw new BadImageFormatException("Format of the executable (.exe) or library (.dll) is invalid.");
            }
            return ptr;
        }
#endif

        protected static byte[] CreateManagedBuffer(byte* ptr, int size)
        {
#if NET5_0_OR_GREATER
            byte[] result = GC.AllocateUninitializedArray<byte>(size);
            Unsafe.CopyBlock(ref MemoryMarshal.GetArrayDataReference(result), ref *ptr, (uint)size);
#else
            byte[] result = new byte[size];
            Unsafe.CopyBlock(ref result[0], ref *ptr, (uint)size);
#endif
            return result;
        }

        protected static void ThrowNativeException(int code)
        {
            throw code switch
            {
                1 => new OutOfMemoryException(),
                2 => new InvalidDataException("Invalid audio data was provided."),
                _ => new InvalidOperationException()
            };
        }

        public virtual byte[] EncodeMp3ToSilk(ReadOnlySpan<byte> buffer)
        {
            int code = TryEncodeMp3ToSilk(buffer, out byte* resultPtr, out int resuleSize);
            if (code != 0)
            {
                ThrowNativeException(code);
            }
            try
            {
                return CreateManagedBuffer(resultPtr, resuleSize);
            }
            finally
            {
                NativeHelper.Free(resultPtr);
            }
        }

        public virtual byte[] DecodeSilkToMp3(ReadOnlySpan<byte> buffer, int samplerate)
        {
            int code = TryDecodeSilkToMp3(buffer, samplerate, out byte* resultPtr, out int resuleSize);
            if (code != 0)
            {
                ThrowNativeException(code);
            }
            try
            {
                return CreateManagedBuffer(resultPtr, resuleSize);
            }
            finally
            {
                NativeHelper.Free(resultPtr);
            }
        }

        public virtual int TryEncodeMp3ToSilk(ReadOnlySpan<byte> buffer, out byte[]? result)
        {
            int code = TryEncodeMp3ToSilk(buffer, out byte* resultPtr, out int resultSize);
            if (code == 0)
            {
                result = CreateManagedBuffer(resultPtr, resultSize);
                NativeHelper.Free(resultPtr);
            }
            else
            {
                result = null;
            }
            return code;
        }

        public virtual int TryEncodeMp3ToSilk(ReadOnlySpan<byte> buffer, out byte* resultPtr, out int resultSize)
        {
            fixed (byte* bufferPtr = buffer)
            {
                int code = NativeHelper.DecodeToPcm(_globalLameFlag, bufferPtr, buffer.Length, out Mp3Metadata metadata, out byte* pcm, out int pcmSize);
                if (code != 0)
                {
                    goto fail;
                }
                try
                {
                    code = NativeHelper.EncodeToSilk(pcm, pcmSize, 1, metadata.Samplerate, 24000, 20 * metadata.Samplerate / 1000, 0, 0, 0, 2, 25000, out byte* silk, out int silkSize);
                    if (code != 0)
                    {
                        goto fail;
                    }
                    resultPtr = silk;
                    resultSize = silkSize;
                    return 0;
                }
                finally
                {
                    NativeHelper.Free(pcm);
                }
fail:
                resultPtr = null;
                resultSize = 0;
                return code;
            }
        }

        public virtual int TryDecodeSilkToMp3(ReadOnlySpan<byte> buffer, int samplerate, out byte[]? result)
        {
            int code = TryDecodeSilkToMp3(buffer, samplerate, out byte* resultPtr, out int resultSize);
            if (code == 0)
            {
                result = CreateManagedBuffer(resultPtr, resultSize);
                NativeHelper.Free(resultPtr);
            }
            else
            {
                result = null;
            }
            return code;
        }

        public virtual int TryDecodeSilkToMp3(ReadOnlySpan<byte> buffer, int samplerate, out byte* resultPtr, out int resultSize)
        {
            fixed (byte* bufferPtr = buffer)
            {
                int code;
                void* glf = NativeHelper.CreateLameFlag();
                if (glf == null)
                {
                    code = 1;
                    goto fail;
                }
                try
                {
                    NativeHelper.SetLameOutputBitrate(glf, 320);
                    NativeHelper.SetLameInputSamplerate(glf, samplerate);
                    NativeHelper.SetLameOutputSamplerate(glf, samplerate);
                    NativeHelper.SetLameQuality(glf, 0);
                    NativeHelper.SetLameInputChannelNum(glf, 1);
                    NativeHelper.SetLameOutputChannelMode(glf, LameOutputChannelMode.Mono);
                    NativeHelper.InitializeLameFlag(glf);
                    code = NativeHelper.DecodeToPcm(bufferPtr, buffer.Length, samplerate, 0f, out byte* pcm, out int pcmSize);
                    if (code != 0)
                    {
                        goto fail;
                    }
                    try
                    {
                        code = NativeHelper.EncodeToMp3(glf, pcm, pcmSize, out byte* mp3, out int mp3Size);
                        if (code != 0)
                        {
                            goto fail;
                        }
                        resultPtr = mp3;
                        resultSize = mp3Size;
                        return 0;
                    }
                    finally
                    {
                        NativeHelper.Free(pcm);
                    }
                }
                finally
                {
                    NativeHelper.CloseLameFlag(glf);
                }
fail:
                resultPtr = null;
                resultSize = 0;
                return code;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
#if NETSTANDARD2_0
            if (_loadedSilkLame != null)
            {
                NativeHelper.Win32.FreeLibrary(_loadedSilkLame);
            }
#endif
            NativeHelper.CloseLameDecoder();
            NativeHelper.Free(_globalLameFlag);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
