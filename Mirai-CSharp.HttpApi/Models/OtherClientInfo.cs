using System.Text.Json.Serialization;
using ISharedOtherClientInfo = Mirai.CSharp.Models.IOtherClientInfo;

namespace Mirai.CSharp.HttpApi.Models
{
    /// <inheritdoc cref="ISharedOtherClientInfo"/>
    public interface IOtherClientInfo : ISharedOtherClientInfo, IBaseInfo
    {
#if !NETSTANDARD2_0
        [JsonPropertyName("platform")]
        abstract string ISharedOtherClientInfo.Platform { get; }
#else
        [JsonPropertyName("platform")]
        new string Platform { get; }
#endif
    }

    public class OtherClientInfo : BaseInfo, IOtherClientInfo
    {
        [JsonPropertyName("platform")]
        public string Platform { get; set; } = null!;

        public OtherClientInfo()
        {
        }

        public OtherClientInfo(long id, string name, string platform) : base(id, name)
        {
            Platform = platform;
        }

#if NETSTANDARD2_0
        [JsonPropertyName("remark")]
        string ISharedOtherClientInfo.Platform => Platform;
#endif
    }
}
