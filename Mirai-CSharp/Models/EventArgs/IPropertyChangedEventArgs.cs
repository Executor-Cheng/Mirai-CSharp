#pragma warning disable CS1712 // Type parameter has no matching typeparam tag in the XML comment (but other type parameters do)
namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供修改前和修改后的 <typeparamref name="TProperty"/> 信息接口
    /// </summary>
    /// <typeparam name="TProperty">属性类型</typeparam>
    public interface IPropertyChangedEventArgs<TProperty> : IMiraiMessage
    {
        /// <summary>
        /// 修改前
        /// </summary>
        TProperty Origin { get; }
        /// <summary>
        /// 修改后
        /// </summary>
        TProperty Current { get; }
    }

    /// <inheritdoc cref="IPropertyChangedEventArgs{TProperty}"/>
    /// <typeparam name="TRawdata">消息原始数据类型</typeparam>
    public interface IPropertyChangedEventArgs<TRawdata, TProperty> : IPropertyChangedEventArgs<TProperty>, IMiraiMessage<TRawdata>
    {
        
    }
}
