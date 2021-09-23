#pragma warning disable CS1712 // Type parameter has no matching typeparam tag in the XML comment (but other type parameters do)
namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供群成员属性改变的信息接口。继承自 <see cref="IPropertyChangedEventArgs{TProperty}"/> 和 <see cref="IMemberEventArgs"/>
    /// </summary>
    /// <typeparam name="TProperty">属性类型</typeparam>
    public interface IGroupMemberPropertyChangedEventArgs<TProperty> : IPropertyChangedEventArgs<TProperty>, IMemberEventArgs
    {

    }

    /// <summary>
    /// 提供群成员属性改变的信息接口。继承自 <see cref="IGroupMemberPropertyChangedEventArgs{TProperty}"/> <see cref="IPropertyChangedEventArgs{TRawdata, TProperty}"/> 和 <see cref="IMemberEventArgs{TRawdata}"/>
    /// </summary>
    /// <inheritdoc cref="IGroupMemberPropertyChangedEventArgs{TProperty}"/>
    /// <typeparam name="TRawdata">消息原始数据类型</typeparam>
    public interface IGroupMemberPropertyChangedEventArgs<TRawdata, TProperty> : IGroupMemberPropertyChangedEventArgs<TProperty>, IPropertyChangedEventArgs<TRawdata, TProperty>, IMemberEventArgs<TRawdata>
    {

    }
}
