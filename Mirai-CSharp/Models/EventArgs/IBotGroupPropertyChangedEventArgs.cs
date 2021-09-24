#pragma warning disable CS1712 // Type parameter has no matching typeparam tag in the XML comment (but other type parameters do)
namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供Bot在群中属性改变的信息接口。继承自 <see cref="IPropertyChangedEventArgs{TProperty}"/> 和 <see cref="IGroupEventArgs"/>
    /// </summary>
    /// <typeparam name="TProperty">属性类型</typeparam>
    public interface IBotGroupPropertyChangedEventArgs<TProperty> : IPropertyChangedEventArgs<TProperty>, IGroupEventArgs
    {

    }

    /// <summary>
    /// 提供Bot在群中属性改变的信息接口。继承自 <see cref="IBotGroupPropertyChangedEventArgs{TProperty}"/>, <see cref="IPropertyChangedEventArgs{TRawdata, TProperty}"/> 和 <see cref="IGroupEventArgs{TRawdata}"/>
    /// </summary>
    /// <typeparam name="TRawdata">消息原始数据类型</typeparam>
    public interface IBotGroupPropertyChangedEventArgs<TRawdata, TProperty> : IBotGroupPropertyChangedEventArgs<TProperty>, IPropertyChangedEventArgs<TRawdata, TProperty>, IGroupEventArgs<TRawdata>
    {

    }
}
