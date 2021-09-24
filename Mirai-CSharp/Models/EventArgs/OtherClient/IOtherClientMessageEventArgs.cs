namespace Mirai.CSharp.Models.EventArgs
{
    public interface IOtherClientMessageEventArgs : ICommonMessageEventArgs
    {
        IOtherClientInfo Sender { get; }
    }

    public interface IOtherClientMessageEventArgs<TRawdata> : IOtherClientMessageEventArgs, ICommonMessageEventArgs<TRawdata>
    {

    }
}
