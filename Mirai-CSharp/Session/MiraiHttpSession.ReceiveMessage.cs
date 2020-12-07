using Mirai_CSharp.Models;
using Mirai_CSharp.Utility;
using System;
using System.IO;
using System.Net.WebSockets;
using System.Text.Json;
using System.Threading;

#pragma warning disable CS0618 // 类型或成员已过时
#pragma warning disable CA1031 // Do not catch general exception types
namespace Mirai_CSharp
{
    public partial class MiraiHttpSession
    {
        private async void ReceiveMessageLoop(InternalSessionInfo session, CancellationToken token)
        {
            using ClientWebSocket ws = new ClientWebSocket();
            try
            {
                await ws.ConnectAsync(new Uri($"ws://{session.Options.Host}:{session.Options.Port}/all?sessionKey={session.SessionKey}"), token);
                while (true)
                {
                    using MemoryStream ms = await ws.ReceiveFullyAsync(token);
                    JsonElement root = JsonSerializer.Deserialize<JsonElement>(new ReadOnlySpan<byte>(ms.GetBuffer(), 0, (int)ms.Length));
                    switch (root.GetProperty("type").GetString())
                    {
                        case "BotOnlineEvent":
                            {
                                _ = InvokeAsync(Plugins, BotOnlineEvt, this, root.Deserialize<BotEventArgs>());
                                break;
                            }
                        case "BotOfflineEventActive":
                            {
                                _ = InvokeAsync(Plugins, BotPositiveOfflineEvt, this, root.Deserialize<BotEventArgs>());
                                break;
                            }
                        case "BotOfflineEventForce":
                            {
                                _ = InvokeAsync(Plugins, BotKickedOfflineEvt, this, root.Deserialize<BotEventArgs>());
                                break;
                            }
                        case "BotOfflineEventDropped":
                            {
                                _ = InvokeAsync(Plugins, BotDroppedEvt, this, root.Deserialize<BotEventArgs>());
                                break;
                            }
                        case "BotReloginEvent":
                            {
                                _ = InvokeAsync(Plugins, BotReloginEvt, this, root.Deserialize<BotEventArgs>());
                                break;
                            }
                        case "BotInvitedJoinGroupRequestEvent":
                            {
                                _ = InvokeAsync(Plugins, BotInvitedJoinGroupEvt, this, root.Deserialize<CommonGroupApplyEventArgs>());
                                break;
                            }
                        case "FriendMessage":
                            {
                                _ = InvokeAsync(Plugins, FriendMessageEvt, this, root.Deserialize<FriendMessageEventArgs>());
                                break;
                            }
                        case "GroupMessage":
                            {
                                _ = InvokeAsync(Plugins, GroupMessageEvt, this, root.Deserialize<GroupMessageEventArgs>());
                                break;
                            }
                        case "TempMessage":
                            {
                                _ = InvokeAsync(Plugins, TempMessageEvt, this, root.Deserialize<TempMessageEventArgs>());
                                break;
                            }
                        case "GroupRecallEvent":
                            {
                                _ = InvokeAsync(Plugins, GroupMessageRevokedEvt, this, root.Deserialize<GroupMessageRevokedEventArgs>());
                                break;
                            }
                        case "FriendRecallEvent":
                            {
                                _ = InvokeAsync(Plugins, FriendMessageRevokedEvt, this, root.Deserialize<FriendMessageRevokedEventArgs>());
                                break;
                            }
                        case "BotGroupPermissionChangeEvent":
                            {
                                _ = InvokeAsync(Plugins, BotGroupPermissionChangedEvt, this, root.Deserialize<BotGroupPermissionChangedEventArgs>());
                                break;
                            }
                        case "BotMuteEvent":
                            {
                                _ = InvokeAsync(Plugins, BotMutedEvt, this, root.Deserialize<BotMutedEventArgs>());
                                break;
                            }
                        case "BotUnmuteEvent":
                            {
                                _ = InvokeAsync(Plugins, BotUnmutedEvt, this, root.Deserialize<BotUnmutedEventArgs>());
                                break;
                            }
                        case "BotJoinGroupEvent":
                            {
                                _ = InvokeAsync(Plugins, BotJoinedGroupEvt, this, root.Deserialize<GroupEventArgs>());
                                break;
                            }
                        case "BotLeaveEventActive":
                            {
                                _ = InvokeAsync(Plugins, BotPositiveLeaveGroupEvt, this, root.Deserialize<GroupEventArgs>());
                                break;
                            }
                        case "BotLeaveEventKick":
                            {
                                _ = InvokeAsync(Plugins, BotKickedOutEvt, this, root.Deserialize<GroupEventArgs>());
                                break;
                            }
                        case "GroupNameChangeEvent":
                            {
                                _ = InvokeAsync(Plugins, GroupNameChangedEvt, this, root.Deserialize<GroupStringPropertyChangedEventArgs>());
                                break;
                            }
                        case "GroupEntranceAnnouncementChangeEvent":
                            {
                                _ = InvokeAsync(Plugins, GroupEntranceAnnouncementChangedEvt, this, root.Deserialize<GroupStringPropertyChangedEventArgs>());
                                break;
                            }
                        case "GroupMuteAllEvent":
                            {
                                _ = InvokeAsync(Plugins, GroupMuteAllChangedEvt, this, root.Deserialize<GroupBoolPropertyChangedEventArgs>());
                                break;
                            }
                        case "GroupAllowAnonymousChatEvent":
                            {
                                _ = InvokeAsync(Plugins, GroupAnonymousChatChangedEvt, this, root.Deserialize<GroupBoolPropertyChangedEventArgs>());
                                break;
                            }
                        case "GroupAllowConfessTalkEvent":
                            {
                                _ = InvokeAsync(Plugins, GroupConfessTalkChangedEvt, this, root.Deserialize<GroupBoolPropertyChangedEventArgs>());
                                break;
                            }
                        case "GroupAllowMemberInviteEvent":
                            {
                                _ = InvokeAsync(Plugins, GroupMemberInviteChangedEvt, this, root.Deserialize<GroupBoolPropertyChangedEventArgs>());
                                break;
                            }
                        case "MemberJoinEvent":
                            {
                                _ = InvokeAsync(Plugins, GroupMemberJoinedEvt, this, root.Deserialize<MemberEventArgs>());
                                break;
                            }
                        case "MemberLeaveEventKick":
                            {
                                _ = InvokeAsync(Plugins, GroupMemberKickedEvt, this, root.Deserialize<MemberOperatingEventArgs>());
                                break;
                            }
                        case "MemberLeaveEventQuit":
                            {
                                _ = InvokeAsync(Plugins, GroupMemberPositiveLeaveEvt, this, root.Deserialize<MemberEventArgs>());
                                break;
                            }
                        case "MemberCardChangeEvent":
                            {
                                _ = InvokeAsync(Plugins, GroupMemberCardChangedEvt, this, root.Deserialize<GroupMemberStringPropertyChangedEventArgs>());
                                break;
                            }
                        case "MemberSpecialTitleChangeEvent":
                            {
                                _ = InvokeAsync(Plugins, GroupMemberSpecialTitleChangedEvt, this, root.Deserialize<GroupMemberStringPropertyChangedEventArgs>());
                                break;
                            }
                        case "MemberPermissionChangeEvent":
                            {
                                _ = InvokeAsync(Plugins, GroupMemberPermissionChangedEvt, this, root.Deserialize<GroupMemberPermissionChangedEventArgs>());
                                break;
                            }
                        case "MemberMuteEvent":
                            {
                                _ = InvokeAsync(Plugins, GroupMemberMutedEvt, this, root.Deserialize<GroupMemberMutedEventArgs>());
                                break;
                            }
                        case "MemberUnmuteEvent":
                            {
                                _ = InvokeAsync(Plugins, GroupMemberUnmutedEvt, this, root.Deserialize<GroupMemberUnmutedEventArgs>());
                                break;
                            }
                        case "NewFriendRequestEvent":
                            {
                                _ = InvokeAsync(Plugins, NewFriendApplyEvt, this, root.Deserialize<NewFriendApplyEventArgs>());
                                break;
                            }
                        case "MemberJoinRequestEvent":
                            {
                                _ = InvokeAsync(Plugins, GroupApplyEvt, this, root.Deserialize<CommonGroupApplyEventArgs>());
                                break;
                            }
                        default:
                            {
                                _ = InvokeAsync(Plugins, UnknownMessageEvt, this, new UnknownMessageEventArgs(root.Clone()));
                                break;
                            }
                    }
                }
            }
            catch (OperationCanceledException)
            {

            }
            catch (Exception e)
            {
                if (Interlocked.CompareExchange(ref SessionInfo, null, session) != null)
                {
                    _ = InternalReleaseAsync(session, default); // 不异步等待, 省的抛错没地捕获
                    _ = InvokeAsync(Plugins, DisconnectedEvt, this, new DisconnectedEventArgs(e));
                }
            }
        }

        private async void ReceiveCommandLoop(InternalSessionInfo session, CancellationToken token)
        {
            using ClientWebSocket ws = new ClientWebSocket();
            try
            {
                await ws.ConnectAsync(new Uri($"ws://{session.Options.Host}:{session.Options.Port}/command?authKey={session.Options.AuthKey}"), token);
                while (true)
                {
                    using MemoryStream ms = await ws.ReceiveFullyAsync(token);
                    ms.Seek(0, SeekOrigin.Begin);
                    using JsonDocument j = await JsonDocument.ParseAsync(ms, default, token);
                    JsonElement root = j.RootElement;
                    _ = InvokeAsync(Plugins, CommandExecuted, this, root.Deserialize<CommandExecutedEventArgs>());
                }
            }
            catch (OperationCanceledException)
            {

            }
            catch (Exception e)
            {
                if (Interlocked.CompareExchange(ref SessionInfo, null, session) != null)
                {
                    _ = InternalReleaseAsync(session, default); // 不异步等待, 省的抛错没地捕获
                    _ = InvokeAsync(Plugins, DisconnectedEvt, this, new DisconnectedEventArgs(e));
                }
            }
        }
    }
}
