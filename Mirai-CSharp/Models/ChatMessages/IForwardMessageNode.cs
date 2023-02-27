using System;

namespace Mirai.CSharp.Models.ChatMessages
{
    public interface IForwardMessageNode
    {
        int? Id { get; }

        string? Name { get; }

        long? QQNumber { get; }

        DateTime? Time { get; }

        IChatMessage[]? Chain { get; }

        IForwardMessageNodeReference? Reference { get; }
    }
}
