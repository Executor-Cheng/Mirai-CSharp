using System.Runtime.InteropServices;
using Mirai.CSharp.HttpApi.Models;

#pragma warning disable CA1401 // P/Invokes should not be visible
namespace Mirai.CSharp.HttpApi.Utility
{
    public unsafe partial class SilkLameCoder
    {
        protected static class NativeHelper
        {
            [DllImport("libsilklame", CallingConvention = CallingConvention.StdCall, EntryPoint = "lameCoder_initializeDefaultFlag")]
            public static extern void* CreateDefaultLameFlag();

            [DllImport("libsilklame", CallingConvention = CallingConvention.StdCall, EntryPoint = "lameCoder_createFlag")]
            public static extern void* CreateLameFlag();

            [DllImport("libsilklame", CallingConvention = CallingConvention.StdCall, EntryPoint = "lameCoder_initializeFlag")]
            public static extern void InitializeLameFlag(void* glf);

            [DllImport("libsilklame", CallingConvention = CallingConvention.StdCall, EntryPoint = "lameCoder_closeFlag")]
            public static extern void CloseLameFlag(void* glf);

            [DllImport("libsilklame", CallingConvention = CallingConvention.StdCall, EntryPoint = "lameCoder_decodeToPcm")]
            public static extern int DecodeToPcm(void* glf, byte* source, int sourceSize, out Mp3Metadata metadata, out byte* destination, out int destinationSize);

            [DllImport("libsilklame", CallingConvention = CallingConvention.StdCall, EntryPoint = "lameCoder_encodeToMp3")]
            public static extern int EncodeToMp3(void* glf, byte* source, int sourceSize, out byte* destination, out int destinationSize);

            [DllImport("libsilklame", CallingConvention = CallingConvention.StdCall, EntryPoint = "lameCoder_initializeDecoder")]
            public static extern void InitializeLameDecoder();

            [DllImport("libsilklame", CallingConvention = CallingConvention.StdCall, EntryPoint = "lameCoder_closeDecoder")]
            public static extern void CloseLameDecoder();

            [DllImport("libsilklame", CallingConvention = CallingConvention.StdCall, EntryPoint = "lameCoder_setInputSamplerate")]
            public static extern void SetLameInputSamplerate(void* glf, int inputSamplerate);

            [DllImport("libsilklame", CallingConvention = CallingConvention.StdCall, EntryPoint = "lameCoder_setInputChannelNum")]
            public static extern void SetLameInputChannelNum(void* glf, int inputChannels);

            [DllImport("libsilklame", CallingConvention = CallingConvention.StdCall, EntryPoint = "lameCoder_setOutputSamplerate")]
            public static extern void SetLameOutputSamplerate(void* glf, int outputSamplerate);

            [DllImport("libsilklame", CallingConvention = CallingConvention.StdCall, EntryPoint = "lameCoder_setOutputBitrate")]
            public static extern void SetLameOutputBitrate(void* glf, int outputBitrate);

            [DllImport("libsilklame", CallingConvention = CallingConvention.StdCall, EntryPoint = "lameCoder_setOutputChannelMode")]
            public static extern void SetLameOutputChannelMode(void* glf, LameOutputChannelMode outputChannelMode);

            [DllImport("libsilklame", CallingConvention = CallingConvention.StdCall, EntryPoint = "lameCoder_setInputScale")]
            public static extern void SetLameInputScale(void* glf, float scale);

            [DllImport("libsilklame", CallingConvention = CallingConvention.StdCall, EntryPoint = "lameCoder_setQuality")]
            public static extern void SetLameQuality(void* glf, int quality);

            [DllImport("libsilklame", CallingConvention = CallingConvention.StdCall, EntryPoint = "silkCoder_encodeToSilk")]
            public static extern int EncodeToSilk(byte* source, int sourceSize, int tencent, int sourceFrequency, int maxInternalSampleRate, int packetSize, int packetLossPercentage, int useInBandFEC, int useDTX, int complexity, int bitRate, out byte* destination, out int destinationSize);

            [DllImport("libsilklame", CallingConvention = CallingConvention.StdCall, EntryPoint = "silkCoder_decodeToPcm")]
            public static extern int DecodeToPcm(byte* source, int sourceSize, int fs_hz, float loss, out byte* destination, out int destinationSize);

            public static void Free(void* block)
            {
#if NET6_0_OR_GREATER
                NativeMemory.Free(block);
#else
                Marshal.FreeHGlobal((System.IntPtr)block);
#endif
            }
        }
    }
}
