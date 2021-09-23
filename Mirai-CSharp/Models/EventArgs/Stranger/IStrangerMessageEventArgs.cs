namespace Mirai.CSharp.Models.EventArgs
{
    public interface IStrangerMessageEventArgs : ICommonMessageEventArgs
    {
        IStrangerInfo Sender { get; }
    }

    public interface IStrangerMessageEventArgs<TRawdata> : IStrangerMessageEventArgs, ICommonMessageEventArgs<TRawdata>
    {

    }
}
