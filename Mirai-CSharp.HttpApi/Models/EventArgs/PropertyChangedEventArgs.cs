using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <inheritdoc cref="Mirai.CSharp.Models.EventArgs.IPropertyChangedEventArgs{TProperty}"/>
    public interface IPropertyChangedEventArgs<TProperty> : IMiraiHttpMessage, Mirai.CSharp.Models.EventArgs.IPropertyChangedEventArgs<JsonElement, TProperty>
    {
#if NETSTANDARD2_0
        /// <inheritdoc cref="Mirai.CSharp.Models.EventArgs.IPropertyChangedEventArgs{TProperty}.Origin"/>
        [JsonPropertyName("origin")]
        new TProperty Origin { get; }
        /// <inheritdoc cref="Mirai.CSharp.Models.EventArgs.IPropertyChangedEventArgs{TProperty}.Current"/>
        [JsonPropertyName("current")]
        new TProperty Current { get; }
#else
        /// <inheritdoc/>
        [JsonPropertyName("origin")]
        abstract TProperty Mirai.CSharp.Models.EventArgs.IPropertyChangedEventArgs<TProperty>.Origin { get; }
        /// <inheritdoc/>
        [JsonPropertyName("current")]
        abstract TProperty Mirai.CSharp.Models.EventArgs.IPropertyChangedEventArgs<TProperty>.Current { get; }
#endif
    }

    public abstract class PropertyChangedEventArgs<TProperty> : MiraiHttpMessage, IPropertyChangedEventArgs<TProperty>
    {
        /// <inheritdoc/>
        [JsonPropertyName("origin")]
        public virtual TProperty Origin { get; set; } = default!;
        /// <inheritdoc/>
        [JsonPropertyName("current")]
        public virtual TProperty Current { get; set; } = default!;

        protected PropertyChangedEventArgs() { }

        protected PropertyChangedEventArgs(TProperty origin, TProperty current)
        {
            Origin = origin;
            Current = current;
        }

#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonPropertyName("origin")]
        TProperty Mirai.CSharp.Models.EventArgs.IPropertyChangedEventArgs<TProperty>.Origin => Origin;
        /// <inheritdoc/>
        [JsonPropertyName("current")]
        TProperty Mirai.CSharp.Models.EventArgs.IPropertyChangedEventArgs<TProperty>.Current => Current;
#endif
    }
}
