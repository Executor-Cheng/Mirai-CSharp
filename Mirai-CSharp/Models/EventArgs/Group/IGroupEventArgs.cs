namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供群事件的信息接口
    /// </summary>
    public interface IGroupEventArgs : IMiraiMessage
    {
        /// <summary>
        /// 来源群信息
        /// </summary>
        IGroupInfo Group { get; }
    }

    /// <inheritdoc/>
    public interface IGroupEventArgs<TRawdata> : IGroupEventArgs, IMiraiMessage<TRawdata>
    {
        
    }

    /// <summary>
    /// 提供群成员信息的接口
    /// </summary>
    public interface IMemberEventArgs : IMiraiMessage
    {
        /// <summary>
        /// 来源成员信息
        /// </summary>
        IGroupMemberInfo Member { get; }
    }

    /// <inheritdoc/>
    public interface IMemberEventArgs<TRawdata> : IMemberEventArgs, IMiraiMessage<TRawdata>
    {
        
    }

    /// <summary>
    /// 提供群内操作者信息的接口
    /// </summary>
    public interface IOperatorEventArgs : IMiraiMessage
    {
        /// <summary>
        /// 来源管理员信息
        /// </summary>
        IGroupMemberInfo Operator { get; }
    }

    /// <inheritdoc/>
    public interface IOperatorEventArgs<TRawdata> : IOperatorEventArgs, IMiraiMessage<TRawdata>
    {
        
    }

    /// <summary>
    /// 提供群内管理事件相关信息的接口。继承自 <see cref="IGroupEventArgs"/> 和 <see cref="IOperatorEventArgs"/>
    /// </summary>
    public interface IGroupOperatingEventArgs : IGroupEventArgs, IOperatorEventArgs
    {

    }

    /// <summary>
    /// 提供群内管理事件相关信息的接口。继承自 <see cref="IGroupOperatingEventArgs"/>, <see cref="IGroupEventArgs{TRawdata}"/> 和 <see cref="IOperatorEventArgs{TRawdata}"/>
    /// </summary>
    public interface IGroupOperatingEventArgs<TRawdata> : IGroupOperatingEventArgs, IGroupEventArgs<TRawdata>, IOperatorEventArgs<TRawdata>
    {
        
    }

    /// <summary>
    /// 提供群内成员被管理操作事件相关信息的接口。继承自 <see cref="IMemberEventArgs"/> 和 <see cref="IOperatorEventArgs"/>
    /// </summary>
    public interface IMemberOperatingEventArgs : IMemberEventArgs, IOperatorEventArgs
    {

    }

    /// <summary>
    /// 提供群内成员被管理操作事件相关信息的接口。继承自 <see cref="IMemberOperatingEventArgs"/>, <see cref="IMemberEventArgs{TRawdata}"/> 和 <see cref="IOperatorEventArgs{TRawdata}"/>
    /// </summary>
    public interface IMemberOperatingEventArgs<TRawdata> : IMemberOperatingEventArgs, IMemberEventArgs<TRawdata>, IOperatorEventArgs<TRawdata>
    {

    }

    /// <summary>
    /// 提供邀请人相关信息的接口。
    /// </summary>
    public interface IInviterEventArgs : IMiraiMessage
    {
        /// <summary>
        /// 邀请人信息
        /// </summary>
        /// <remarks>
        /// 仅当 mirai-api-http 版本至少为 2.3.0 时, 此属性可能不为 <see langword="null"/>
        /// </remarks>
        IGroupMemberInfo? Inviter { get; }
    }

    /// <inheritdoc/>
    public interface IInviterEventArgs<TRawdata> : IInviterEventArgs, IMiraiMessage<TRawdata>
    {

    }
}
