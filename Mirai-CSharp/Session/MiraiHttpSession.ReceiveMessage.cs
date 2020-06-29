using Mirai_CSharp.Models;
using Mirai_CSharp.Plugin.Interfaces;
using Mirai_CSharp.Utility;
using System;
using System.IO;
using System.Net.WebSockets;
using System.Text.Json;
using System.Threading;

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
                    ms.Seek(0, SeekOrigin.Begin);
                    using JsonDocument j = await JsonDocument.ParseAsync(ms, default, token);
                    JsonElement root = j.RootElement;
                    switch (root.GetProperty("type").GetString())
                    {
                        case "BotOnlineEvent":
                            {
                                _ = InvokeAsync<IBotOnline, IBotOnlineEventArgs>(Plugins, BotOnlineEvt, this, root.Deserialize<BotEventArgs>());
                                break;
                            }
                        case "BotOfflineEventActive":
                            {
                                _ = InvokeAsync<IBotPositiveOffline, IBotPositiveOfflineEventArgs>(Plugins, BotPositiveOfflineEvt, this, root.Deserialize<BotEventArgs>());
                                break;
                            }
                        case "BotOfflineEventForce":
                            {
                                _ = InvokeAsync<IBotKickedOffline, IBotKickedOfflineEventArgs>(Plugins, BotKickedOfflineEvt, this, root.Deserialize<BotEventArgs>());
                                break;
                            }
                        case "BotOfflineEventDropped":
                            {
                                _ = InvokeAsync<IBotDropped, IBotDroppedEventArgs>(Plugins, BotDroppedEvt, this, root.Deserialize<BotEventArgs>());
                                break;
                            }
                        case "BotReloginEvent":
                            {
                                _ = InvokeAsync<IBotRelogin, IBotReloginEventArgs>(Plugins, BotReloginEvt, this, root.Deserialize<BotEventArgs>());
                                break;
                            }
                        case "BotInvitedJoinGroupRequestEvent":
                            {
                                _ = InvokeAsync<IBotInvitedJoinGroup, IBotInvitedJoinGroupEventArgs>(Plugins, BotInvitedJoinGroupEvt, this, root.Deserialize<CommonGroupApplyEventArgs>());
                                break;
                            }
                        case "FriendMessage":
                            {
                                _ = InvokeAsync<IFriendMessage, IFriendMessageEventArgs>(Plugins, FriendMessageEvt, this, root.Deserialize<FriendMessageEventArgs>());
                                break;
                            }
                        case "GroupMessage":
                            {
                                _ = InvokeAsync<IGroupMessage, IGroupMessageEventArgs>(Plugins, GroupMessageEvt, this, root.Deserialize<GroupMessageEventArgs>());
                                break;
                            }
                        case "TempMessage":
                            {
                                _ = InvokeAsync<ITempMessage, ITempMessageEventArgs>(Plugins, TempMessageEvt, this, root.Deserialize<TempMessageEventArgs>());
                                break;
                            }
                        case "GroupRecallEvent":
                            {
                                _ = InvokeAsync<IGroupMessageRevoked, IGroupMessageRevokedEventArgs>(Plugins, GroupMessageRevokedEvt, this, root.Deserialize<GroupMessageRevokedEventArgs>());
                                break;
                            }
                        case "FriendRecallEvent":
                            {
                                _ = InvokeAsync<IFriendMessageRevoked, IFriendMessageRevokedEventArgs>(Plugins, FriendMessageRevokedEvt, this, root.Deserialize<FriendMessageRevokedEventArgs>());
                                break;
                            }
                        case "BotGroupPermissionChangeEvent":
                            {
                                _ = InvokeAsync<IBotGroupPermissionChanged, IBotGroupPermissionChangedEventArgs>(Plugins, BotGroupPermissionChangedEvt, this, root.Deserialize<BotGroupPermissionChangedEventArgs>());
                                break;
                            }
                        case "BotMuteEvent":
                            {
                                _ = InvokeAsync<IBotMuted, IBotMutedEventArgs>(Plugins, BotMutedEvt, this, root.Deserialize<BotMutedEventArgs>());
                                break;
                            }
                        case "BotUnmuteEvent":
                            {
                                _ = InvokeAsync<IBotUnmuted, IBotUnmutedEventArgs>(Plugins, BotUnmutedEvt, this, root.Deserialize<BotUnmutedEventArgs>());
                                break;
                            }
                        case "BotJoinGroupEvent":
                            {
                                _ = InvokeAsync<IBotJoinedGroup, IBotJoinedGroupEventArgs>(Plugins, BotJoinedGroupEvt, this, root.Deserialize<GroupEventArgs>());
                                break;
                            }
                        case "BotLeaveEventActive":
                            {
                                _ = InvokeAsync<IBotPositiveLeaveGroup, IBotPositiveLeaveGroupEventArgs>(Plugins, BotPositiveLeaveGroupEvt, this, root.Deserialize<GroupEventArgs>());
                                break;
                            }
                        case "BotLeaveEventKick":
                            {
                                _ = InvokeAsync<IBotKickedOut, IBotKickedOutEventArgs>(Plugins, BotKickedOutEvt, this, root.Deserialize<GroupEventArgs>());
                                break;
                            }
                        case "GroupNameChangeEvent":
                            {
                                _ = InvokeAsync<IGroupNameChanged, IGroupNameChangedEventArgs>(Plugins, GroupNameChangedEvt, this, root.Deserialize<GroupStringPropertyChangedEventArgs>());
                                break;
                            }
                        case "GroupEntranceAnnouncementChangeEvent":
                            {
                                _ = InvokeAsync<IGroupEntranceAnnouncementChanged, IGroupEntranceAnnouncementChangedEventArgs>(Plugins, GroupEntranceAnnouncementChangedEvt, this, root.Deserialize<GroupStringPropertyChangedEventArgs>());
                                break;
                            }
                        case "GroupMuteAllEvent":
                            {
                                _ = InvokeAsync<IGroupMuteAllChanged, IGroupMuteAllChangedEventArgs>(Plugins, GroupMuteAllChangedEvt, this, root.Deserialize<GroupBoolPropertyChangedEventArgs>());
                                break;
                            }
                        case "GroupAllowAnonymousChatEvent":
                            {
                                _ = InvokeAsync<IGroupAnonymousChatChanged, IGroupAnonymousChatChangedEventArgs>(Plugins, GroupAnonymousChatChangedEvt, this, root.Deserialize<GroupBoolPropertyChangedEventArgs>());
                                break;
                            }
                        case "GroupAllowConfessTalkEvent":
                            {
                                _ = InvokeAsync<IGroupConfessTalkChanged, IGroupConfessTalkChangedEventArgs>(Plugins, GroupConfessTalkChangedEvt, this, root.Deserialize<GroupBoolPropertyChangedEventArgs>());
                                break;
                            }
                        case "GroupAllowMemberInviteEvent":
                            {
                                _ = InvokeAsync<IGroupMemberInviteChanged, IGroupMemberInviteChangedEventArgs>(Plugins, GroupMemberInviteChangedEvt, this, root.Deserialize<GroupBoolPropertyChangedEventArgs>());
                                break;
                            }
                        case "MemberJoinEvent":
                            {
                                _ = InvokeAsync<IGroupMemberJoined, IGroupMemberJoinedEventArgs>(Plugins, GroupMemberJoinedEvt, this, root.Deserialize<MemberEventArgs>());
                                break;
                            }
                        case "MemberLeaveEventKick":
                            {
                                _ = InvokeAsync<IGroupMemberKicked, IGroupMemberKickedEventArgs>(Plugins, GroupMemberKickedEvt, this, root.Deserialize<MemberOperatingEventArgs>());
                                break;
                            }
                        case "MemberLeaveEventQuit":
                            {
                                _ = InvokeAsync<IGroupMemberPositiveLeave, IGroupMemberPositiveLeaveEventArgs>(Plugins, GroupMemberPositiveLeaveEvt, this, root.Deserialize<MemberEventArgs>());
                                break;
                            }
                        case "MemberCardChangeEvent":
                            {
                                _ = InvokeAsync<IGroupMemberCardChanged, IGroupMemberCardChangedEventArgs>(Plugins, GroupMemberCardChangedEvt, this, root.Deserialize<GroupMemberStringPropertyChangedEventArgs>());
                                break;
                            }
                        case "MemberSpecialTitleChangeEvent":
                            {
                                _ = InvokeAsync<IGroupMemberSpecialTitleChanged, IGroupMemberSpecialTitleChangedEventArgs>(Plugins, GroupMemberSpecialTitleChangedEvt, this, root.Deserialize<GroupMemberStringPropertyChangedEventArgs>());
                                break;
                            }
                        case "MemberPermissionChangeEvent":
                            {
                                _ = InvokeAsync<IGroupMemberPermissionChanged, IGroupMemberPermissionChangedEventArgs>(Plugins, GroupMemberPermissionChangedEvt, this, root.Deserialize<GroupMemberPermissionChangedEventArgs>());
                                break;
                            }
                        case "MemberMuteEvent":
                            {
                                _ = InvokeAsync<IGroupMemberMuted, IGroupMemberMutedEventArgs>(Plugins, GroupMemberMutedEvt, this, root.Deserialize<GroupMemberMutedEventArgs>());
                                break;
                            }
                        case "MemberUnmuteEvent":
                            {
                                _ = InvokeAsync<IGroupMemberUnmuted, IGroupMemberUnmutedEventArgs>(Plugins, GroupMemberUnmutedEvt, this, root.Deserialize<GroupMemberUnmutedEventArgs>());
                                break;
                            }
                        case "NewFriendRequestEvent":
                            {
                                _ = InvokeAsync<INewFriendApply, INewFriendApplyEventArgs>(Plugins, NewFriendApplyEvt, this, root.Deserialize<NewFriendApplyEventArgs>());
                                break;
                            }
                        case "MemberJoinRequestEvent":
                            {
                                _ = InvokeAsync<IGroupApply, IGroupApplyEventArgs>(Plugins, GroupApplyEvt, this, root.Deserialize<CommonGroupApplyEventArgs>());
                                break;
                            }
                        default:
                            {
                                _ = InvokeAsync<IUnknownMessage, IUnknownMessageEventArgs>(Plugins, UnknownMessageEvt, this, new UnknownMessageEventArgs(root.Clone()));
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
                    _ = InternalReleaseAsync(session); // 不异步等待, 省的抛错没地捕获
                    try { DisconnectedEvt?.Invoke(this, e); } catch { } // 扔掉所有异常
                }
            }
        }

        private async void ReceiveCommandLoop(InternalSessionInfo session, CancellationToken token)
        {
            using ClientWebSocket ws = new ClientWebSocket();
            ws.Options.KeepAliveInterval = TimeSpan.FromSeconds(-1);
            try
            {
                await ws.ConnectAsync(new Uri($"ws://{session.Options.Host}:{session.Options.Port}/command?authKey={session.Options.AuthKey}"), token);
                while (true)
                {
                    using MemoryStream ms = await ws.ReceiveFullyAsync(token);
                    ms.Seek(0, SeekOrigin.Begin);
                    using JsonDocument j = await JsonDocument.ParseAsync(ms, default, token);
                    JsonElement root = j.RootElement;
                    _ = InvokeAsync<ICommandExecuted, ICommandExecutedEventArgs>(Plugins, CommandExecuted, this, root.Deserialize<CommandExecutedEventArgs>());
                }
            }
            catch (OperationCanceledException)
            {

            }
            catch (Exception e)
            {
                if (Interlocked.CompareExchange(ref SessionInfo, null, session) != null)
                {
                    _ = InternalReleaseAsync(session); // 不异步等待, 省的抛错没地捕获
                    try { DisconnectedEvt?.Invoke(this, e); } catch { } // 扔掉所有异常
                }
            }
        }
    }
}
