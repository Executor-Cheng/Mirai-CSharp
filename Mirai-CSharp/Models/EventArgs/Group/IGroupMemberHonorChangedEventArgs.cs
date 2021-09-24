namespace Mirai.CSharp.Models.EventArgs
{
    public interface IGroupMemberHonorChangedEventArgs : IMemberEventArgs
    {
        GroupHonorState State { get; }

        string Honor { get; }
    }

    public interface IGroupMemberHonorChangedEventArgs<TRawdata> : IGroupMemberHonorChangedEventArgs, IMemberEventArgs<TRawdata>
    {

    }
}
