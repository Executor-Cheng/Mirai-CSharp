using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Mirai.CSharp.Models;
using Mirai.CSharp.Services;
using SkiaSharp;

namespace Mirai_CSharp.ImageConverter
{
    public class SkiasharpImageConverter : IImageConverter
    {
        public unsafe bool TryConvert(ReadOnlySpan<byte> inputSpan, ImageFormat convertTo, out ImageFormat outputFormat, out byte[]? output)
        {
            fixed (byte* inputPtr = &inputSpan.GetPinnableReference())
            {
                using SKData inputData = SKData.Create((nint)inputPtr, inputSpan.Length);
                using SKCodec codec = SKCodec.Create(inputData);
                var skformat = codec.EncodedFormat;
                ImageFormat inputFormat = skformat switch
                {
                    SKEncodedImageFormat.Png => ImageFormat.Png,
                    SKEncodedImageFormat.Jpeg => ImageFormat.Jpeg,
                    SKEncodedImageFormat.Gif => ImageFormat.Gif,
                    _ => ImageFormat.Unknown
                };
                inputFormat &= convertTo;
                if ((inputFormat & convertTo) != ImageFormat.Unknown) // 如果输入的图片是请求输出格式的其中一种就原样返回
                {
                    outputFormat = inputFormat;
                    output = null;
                    return true;
                }
                convertTo = (ImageFormat)((int)convertTo & -(int)convertTo); // 保留最右侧的位，其余位全部置0
                if (convertTo == ImageFormat.Unknown)
                {
                    outputFormat = ImageFormat.Unknown;
                    output = null;
                    return false;
                }
                // 1 => 4
                // 2 => 3
                // 4 => 1
                SKEncodedImageFormat skFormat = (SKEncodedImageFormat)(5 - (int)convertTo); // 看上边的对照表
                using SKBitmap bitmap = SKBitmap.Decode(codec);
                using SKData encoded = bitmap.Encode(skFormat, 100);
                outputFormat = convertTo;
                output = encoded.ToArray();
                return true;
            }
        }

        public async Task<(ImageFormat outputFormat, byte[] output)> ConvertAsync(Stream inputStream, ImageFormat convertTo, CancellationToken token = default)
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
            if (!TryConvert(buffer, convertTo, out ImageFormat outputFormat, out byte[]? output))
            {
                throw new ArgumentOutOfRangeException(nameof(convertTo), convertTo, "无效的输出格式");
            }
#if !NETSTANDARD2_0
            return (outputFormat, output ?? (length == buffer.Length ? buffer : buffer[..length]));
#else
            if (output != null)
            {
                return (outputFormat, output);
            }
            if (length != buffer.Length)
            {
                Array.Resize(ref buffer, length);
            }
            return (outputFormat, buffer);
#endif
        }
    }
}
