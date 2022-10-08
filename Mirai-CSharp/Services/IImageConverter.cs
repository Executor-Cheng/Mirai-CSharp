using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Mirai.CSharp.Models;

namespace Mirai.CSharp.Services
{
    public interface IImageConverter
    {
        bool TryConvert(ReadOnlySpan<byte> inputSpan, ImageFormat convertTo, out ImageFormat outputFormat, out byte[]? output);

        Task<(ImageFormat outputFormat, byte[] output)> ConvertAsync(Stream inputStream, ImageFormat convertTo, CancellationToken token = default);
    }
}
