using System;
using System.Text.Json.Serialization;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供Bot在群中属性改变的信息接口。继承自 <see cref="IEnumPropertyChangedEventArgs{TProperty}"/> 和 <see cref="IGroupEventArgs"/>
    /// </summary>
    /// <remarks>
    /// 本接口是对于 <see langword="enum"/> 的特定实现
    /// </remarks>
    /// <typeparam name="TProperty">属性类型, 必须为枚举类型</typeparam>
    public interface IBotGroupEnumPropertyChangedEventArgs<TProperty> : IEnumPropertyChangedEventArgs<TProperty>, IBotGroupPropertyChangedEventArgs<TProperty> where TProperty : Enum
    {

    }

    public class BotGroupEnumPropertyChangedEventArgs<TProperty> : BotGroupPropertyChangedEventArgs<TProperty>, IBotGroupEnumPropertyChangedEventArgs<TProperty> where TProperty : Enum
    {
        /// <inheritdoc/>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("origin")]
        public override TProperty Origin { get; set; } = default!;

        /// <inheritdoc/>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("current")]
        public override TProperty Current { get; set; } = default!;

        [Obsolete("此类不应由用户主动创建实例。")]
        public BotGroupEnumPropertyChangedEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public BotGroupEnumPropertyChangedEventArgs(IGroupInfo group, TProperty origin, TProperty current) : base(group, origin, current)
        {

        }

#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("origin")]
        TProperty IEnumPropertyChangedEventArgs<TProperty>.Origin => Origin;

        /// <inheritdoc/>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("current")]
        TProperty IEnumPropertyChangedEventArgs<TProperty>.Current => Current;
#endif
    }
}
