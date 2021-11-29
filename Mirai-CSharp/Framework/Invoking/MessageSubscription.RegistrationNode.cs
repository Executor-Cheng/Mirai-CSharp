using Mirai.CSharp.Framework.Handlers;

// see: https://source.dot.net/System.Private.CoreLib/CancellationTokenSource.cs.html#e46e9cfd59295b58
namespace Mirai.CSharp.Framework.Invoking
{
    public partial class MessageSubscription
    {
        protected internal sealed class RegistrationNode
        {
            public readonly Registrations Registrations;
            public RegistrationNode? Prev;
            public RegistrationNode? Next;

            public long Id;
            public IMessageHandler? Handler;

            public RegistrationNode(Registrations registrations)
            {
                Registrations = registrations;
            }
        }
    }
}
