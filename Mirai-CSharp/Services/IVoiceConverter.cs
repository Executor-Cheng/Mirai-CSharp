using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Mirai.CSharp.Services
{
    public interface IVoiceConverter
    {
        bool TryConvert(ReadOnlySpan<byte> inputSpan, out byte[]? output);

        Task<byte[]> ConvertAsync(Stream inputStream, CancellationToken token = default);
    }
}
