using System.Text.Json.Serialization;
using ISharedStrangerInfo = Mirai.CSharp.Models.IStrangerInfo;

namespace Mirai.CSharp.HttpApi.Models
{
    /// <inheritdoc cref="ISharedStrangerInfo"/>
    public interface IStrangerInfo : ISharedStrangerInfo, IBaseInfo
    {
#if !NETSTANDARD2_0
        [JsonPropertyName("remark")]
        abstract string ISharedStrangerInfo.Remark { get; }
#else
        [JsonPropertyName("remark")]
        new string Remark { get; }
#endif
    }

    public class StrangerInfo : BaseInfo, IStrangerInfo
    {
        [JsonPropertyName("remark")]
        public string Remark { get; set; } = null!;

        public StrangerInfo()
        {
        }

        public StrangerInfo(long id, string name, string remark) : base(id, name)
        {
            Remark = remark;
        }

#if NETSTANDARD2_0
        [JsonPropertyName("remark")]
        string ISharedStrangerInfo.Remark => Remark;
#endif
    }
}
