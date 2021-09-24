using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Utility.JsonConverters;
#if NETSTANDARD2_0
using ISharedGroupMemberInfo = Mirai.CSharp.Models.IGroupMemberInfo;
using ISharedOperatorEventArgs = Mirai.CSharp.Models.EventArgs.IOperatorEventArgs;
#endif

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供群属性改变的信息接口。继承自 <see cref="Mirai.CSharp.Models.EventArgs.IGroupPropertyChangedEventArgs{TRawdata, TProperty}"/>, <see cref="IPropertyChangedEventArgs{TProperty}"/> 和 <see cref="IGroupOperatingEventArgs"/>
    /// </summary>
    /// <typeparam name="TProperty">属性类型</typeparam>
    public interface IGroupPropertyChangedEventArgs<TProperty> : Mirai.CSharp.Models.EventArgs.IGroupPropertyChangedEventArgs<JsonElement, TProperty>, IPropertyChangedEventArgs<TProperty>, IGroupOperatingEventArgs
    {

    }

    public class GroupPropertyChangedEventArgs<TProperty> : BotGroupPropertyChangedEventArgs<TProperty>, IGroupPropertyChangedEventArgs<TProperty>
    {
        /// <summary>
        /// 操作者信息
        /// </summary>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupMemberInfo, IGroupMemberInfo>))]
        [JsonPropertyName("operator")]
        public IGroupMemberInfo Operator { get; set; } = null!;

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupPropertyChangedEventArgs() { }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupPropertyChangedEventArgs(IGroupInfo group, IGroupMemberInfo @operator, TProperty origin, TProperty current) : base(group, origin, current)
        {
            Operator = @operator;
        }

#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupMemberInfo, IGroupMemberInfo>))]
        [JsonPropertyName("operator")]
        ISharedGroupMemberInfo ISharedOperatorEventArgs.Operator => Operator;
#endif
    }
}
