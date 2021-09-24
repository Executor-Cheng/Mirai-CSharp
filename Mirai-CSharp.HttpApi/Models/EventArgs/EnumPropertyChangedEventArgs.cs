using System;
using System.Text.Json.Serialization;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供修改前和修改后的 <typeparamref name="TProperty"/> 信息接口
    /// </summary>
    /// <remarks>
    /// 本接口是对于 <see langword="enum"/> 的特定实现
    /// </remarks>
    /// <typeparam name="TProperty">属性类型, 必须为枚举类型</typeparam>
    public interface IEnumPropertyChangedEventArgs<TProperty> : IPropertyChangedEventArgs<TProperty> where TProperty : Enum
    {
#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("origin")]
        new TProperty Origin { get; }

        /// <inheritdoc/>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("current")]
        new TProperty Current { get; }
#else
        /// <inheritdoc/>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("origin")]
        abstract TProperty Mirai.CSharp.Models.EventArgs.IPropertyChangedEventArgs<TProperty>.Origin { get; }

        /// <inheritdoc/>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("current")]
        abstract TProperty Mirai.CSharp.Models.EventArgs.IPropertyChangedEventArgs<TProperty>.Current { get; }
#endif
    }

    public abstract class EnumPropertyChangedEventArgs<TProperty> : PropertyChangedEventArgs<TProperty>, IEnumPropertyChangedEventArgs<TProperty> where TProperty : Enum
    {
        /// <inheritdoc/>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("origin")]
        public override TProperty Origin { get; set; } = default!;

        /// <inheritdoc/>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("current")]
        public override TProperty Current { get; set; } = default!;

        protected EnumPropertyChangedEventArgs()
        {

        }

        protected EnumPropertyChangedEventArgs(TProperty origin, TProperty current) : base(origin, current)
        {

        }

#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("origin")]
        TProperty IPropertyChangedEventArgs<TProperty>.Origin => Origin;

        /// <inheritdoc/>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("current")]
        TProperty IPropertyChangedEventArgs<TProperty>.Current => Current;
#endif
    }
}
