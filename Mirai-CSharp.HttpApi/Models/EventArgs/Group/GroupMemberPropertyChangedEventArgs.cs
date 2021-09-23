using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Utility.JsonConverters;
#if NETSTANDARD2_0
using ISharedGroupMemberInfo = Mirai.CSharp.Models.IGroupMemberInfo;
using ISharedMemberEventArgs = Mirai.CSharp.Models.EventArgs.IMemberEventArgs;
#endif

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供群成员属性改变的信息接口。继承自 <see cref="Mirai.CSharp.Models.EventArgs.IGroupMemberPropertyChangedEventArgs{TRawdata, TProperty}"/>, <see cref="IPropertyChangedEventArgs{TProperty}"/> 和 <see cref="IMemberEventArgs"/>
    /// </summary>
    /// <typeparam name="TProperty">属性类型</typeparam>
    public interface IGroupMemberPropertyChangedEventArgs<TProperty> : Mirai.CSharp.Models.EventArgs.IGroupMemberPropertyChangedEventArgs<JsonElement, TProperty>, IPropertyChangedEventArgs<TProperty>, IMemberEventArgs
    {

    }

    public class GroupMemberPropertyChangedEventArgs<TProperty> : PropertyChangedEventArgs<TProperty>, IGroupMemberPropertyChangedEventArgs<TProperty>
    {
        /// <summary>
        /// 被操作者信息
        /// </summary>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupMemberInfo, IGroupMemberInfo>))]
        [JsonPropertyName("member")]
        public IGroupMemberInfo Member { get; set; } = null!;

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberPropertyChangedEventArgs() { }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberPropertyChangedEventArgs(IGroupMemberInfo member, TProperty origin, TProperty current) : base(origin, current)
        {
            Member = member;
        }

#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupMemberInfo, ISharedGroupMemberInfo>))]
        [JsonPropertyName("member")]
        ISharedGroupMemberInfo ISharedMemberEventArgs.Member => Member;
#endif
    }
}
