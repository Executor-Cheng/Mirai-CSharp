using Mirai_CSharp.Plugin.Interfaces;

namespace Mirai_CSharp.Example
{
    public partial class ExamplePlugin : IFriendMessage, // 你想处理什么事件就实现什么事件对应的接口
                                 IGroupMessage,
                                 INewFriendApply,
                                 IGroupApply,
                                 IBotInvitedJoinGroup,
                                 IDisconnected
    {
        
    }
}
