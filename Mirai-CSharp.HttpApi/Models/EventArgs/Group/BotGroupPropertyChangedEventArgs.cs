using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Utility.JsonConverters;
using System.Text.Json;
#if NETSTANDARD2_0
using ISharedGroupInfo = Mirai.CSharp.Models.IGroupInfo;
using ISharedGroupEventArgs = Mirai.CSharp.Models.EventArgs.IGroupEventArgs;
#endif

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供Bot在群中属性改变的信息接口。继承自 <see cref="Mirai.CSharp.Models.EventArgs.IBotGroupPropertyChangedEventArgs{TRawdata, TProperty}"/> <see cref="IPropertyChangedEventArgs{TProperty}"/> 和 <see cref="IGroupEventArgs"/>
    /// </summary>
    /// <typeparam name="TProperty">属性类型</typeparam>
    public interface IBotGroupPropertyChangedEventArgs<TProperty> : Mirai.CSharp.Models.EventArgs.IBotGroupPropertyChangedEventArgs<JsonElement, TProperty>, IPropertyChangedEventArgs<TProperty>, IGroupEventArgs
    {

    }

    public abstract class BotGroupPropertyChangedEventArgs<TProperty> : PropertyChangedEventArgs<TProperty>, IBotGroupPropertyChangedEventArgs<TProperty>
    {
        /// <summary>
        /// 机器人所在群信息
        /// </summary>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupInfo, IGroupInfo>))]
        [JsonPropertyName("group")]
        public IGroupInfo Group { get; set; } = null!;

        protected BotGroupPropertyChangedEventArgs() { }

        protected BotGroupPropertyChangedEventArgs(IGroupInfo group, TProperty origin, TProperty current) : base(origin, current)
        {
            Group = group;
        }

#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupInfo, ISharedGroupInfo>))]
        [JsonPropertyName("group")]
        ISharedGroupInfo ISharedGroupEventArgs.Group => Group;
#endif
    }
}
