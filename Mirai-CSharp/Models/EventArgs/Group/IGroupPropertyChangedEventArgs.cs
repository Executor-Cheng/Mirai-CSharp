#pragma warning disable CS1712 // Type parameter has no matching typeparam tag in the XML comment (but other type parameters do)
namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供群属性改变的信息接口。继承自 <see cref="IPropertyChangedEventArgs{TProperty}"/> 和 <see cref="IGroupOperatingEventArgs"/>
    /// </summary>
    /// <typeparam name="TProperty">属性类型</typeparam>
    public interface IGroupPropertyChangedEventArgs<TProperty> : IPropertyChangedEventArgs<TProperty>, IGroupOperatingEventArgs
    {

    }

    /// <summary>
    /// 提供群属性改变的信息接口。继承自 <see cref="IGroupPropertyChangedEventArgs{TProperty}"/> <see cref="IPropertyChangedEventArgs{TRawdata, TProperty}"/> 和 <see cref="IGroupOperatingEventArgs{TRawdata}"/>
    /// </summary>
    /// <inheritdoc cref="IGroupPropertyChangedEventArgs{TProperty}"/>
    /// <typeparam name="TRawdata">消息原始数据类型</typeparam>
    public interface IGroupPropertyChangedEventArgs<TRawdata, TProperty> : IGroupPropertyChangedEventArgs<TProperty>, IPropertyChangedEventArgs<TRawdata, TProperty>, IGroupOperatingEventArgs<TRawdata>
    {

    }
}
