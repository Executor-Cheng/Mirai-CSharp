using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Mirai.CSharp.Services;

namespace Mirai.CSharp.VoiceConverter
{
    public class SilkLameVoiceConverter : IVoiceConverter
    {
        private static ReadOnlySpan<byte> _silkHeader => new byte[10] { 2, 35, 33, 83, 73, 76, 75, 95, 86, 51 };

        private readonly ISilkLameCoder _coder;

        public SilkLameVoiceConverter(ISilkLameCoder coder)
        {
            _coder = coder;
        }

        public bool TryConvert(ReadOnlySpan<byte> inputSpan, out byte[]? output)
        {
            if (inputSpan.Length <= 10)
            {
                output = null;
                return false;
            }
            if (inputSpan.Slice(0, _silkHeader.Length).SequenceEqual(_silkHeader))
            {
                output = null;
                return true;
            }
            if (inputSpan.Slice(0, 9).SequenceEqual(_silkHeader.Slice(1)))
            {
                byte[] copied = new byte[inputSpan.Length + 1];
                copied[0] = 2;
#if NET5_0_OR_GREATER
                Unsafe.CopyBlock(ref Unsafe.AddByteOffset(ref MemoryMarshal.GetArrayDataReference(copied), new IntPtr(1)), ref MemoryMarshal.GetReference(inputSpan), (uint)inputSpan.Length);
#else
                Unsafe.CopyBlock(ref copied[1], ref MemoryMarshal.GetReference(inputSpan), (uint)inputSpan.Length);
#endif
                output = copied;
                return true;
            }
            return _coder.TryEncodeMp3ToSilk(inputSpan, out output) == 0;
        }

        public async Task<byte[]> ConvertAsync(Stream inputStream, CancellationToken token = default)
        {
            byte[] buffer;
            int length;
            if (inputStream is not MemoryStream ms) // 对于非 MemoryStream 的, 一律假定其读取行为是阻塞的
            {
                ms = new MemoryStream(8192);
                await inputStream.CopyToAsync(ms, 81920, token);
                buffer = ms.GetBuffer();
                length = (int)ms.Length;
            }
            else
            {
                length = (int)(ms.Length - ms.Position);
                buffer = new byte[length];
                ms.Read(buffer, 0, length);
            }
            if (!TryConvert(buffer, out byte[]? output))
            {
                throw new ArgumentException("输入的音频格式不为mp3, 因此无法将其转为silk格式进行发送", nameof(inputStream));
            }
#if !NETSTANDARD2_0
            return output ?? (length == buffer.Length ? buffer : buffer[..length]);
#else
            if (output != null)
            {
                return output;
            }
            if (length != buffer.Length)
            {
                Array.Resize(ref buffer, length);
            }
            return buffer;
#endif
        }
    }
}
