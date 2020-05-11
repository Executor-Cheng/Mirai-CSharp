using Mirai_CSharp.Models;
using Mirai_CSharp.Utility;
using System;
using System.IO;
using System.Net.WebSockets;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading;

#pragma warning disable CA1031 // Do not catch general exception types
namespace Mirai_CSharp
{
    public partial class MiraiHttpSession
    {
        private async void ReceiveMessageLoop(InternalSessionInfo session)
        {
            JsonSerializerOptions opt = new JsonSerializerOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
            CancellationToken token = session.Canceller.Token;
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
                                BotOnlineEvt?.InvokeAsync(this, root.GetProperty("qq").GetInt64());
                                break;
                            }
                        case "BotOfflineEventActive":
                            {
                                BotPositiveOfflineEvt?.InvokeAsync(this, root.GetProperty("qq").GetInt64());
                                break;
                            }
                        case "BotOfflineEventForce":
                            {
                                BotKickedOfflineEvt?.InvokeAsync(this, root.GetProperty("qq").GetInt64());
                                break;
                            }
                        case "BotOfflineEventDropped":
                            {
                                BotDroppedEvt?.InvokeAsync(this, root.GetProperty("qq").GetInt64());
                                break;
                            }
                        case "BotReloginEvent":
                            {
                                BotReloginEvt?.InvokeAsync(this, root.GetProperty("qq").GetInt64());
                                break;
                            }
                        case "FriendMessage":
                            {
                                FriendMessageEvt?.InvokeAsync(this, Utils.Deserialize<FriendMessageEventArgs>(in root));
                                break;
                            }
                        case "GroupMessage":
                            {
                                GroupMessageEvt?.InvokeAsync(this, Utils.Deserialize<GroupMessageEventArgs>(in root));
                                break;
                            }
                        case "TempMessage":
                            {
                                break;
                            }
                        case "GroupRecallEvent":
                            {
                                GroupMessageRevokedEvt?.InvokeAsync(this, Utils.Deserialize<GroupMessageRevokedEventArgs>(in root));
                                break;
                            }
                        case "FriendRecallEvent":
                            {
                                FriendMessageRevokedEvt?.InvokeAsync(this, Utils.Deserialize<FriendMessageRevokedEventArgs>(in root));
                                break;
                            }
                        case "BotGroupPermissionChangeEvent":
                            {
                                BotGroupPermissionChangedEvt?.InvokeAsync(this, Utils.Deserialize<BotGroupEnumPropertyChangedEventArgs<GroupPermission>>(in root));
                                break;
                            }
                        case "BotMuteEvent":
                            {
                                BotMutedEvt?.InvokeAsync(this, Utils.Deserialize<BotMutedEventArgs>(in root));
                                break;
                            }
                        case "BotUnmuteEvent":
                            {
                                BotUnmutedEvt?.InvokeAsync(this, Utils.Deserialize<BotUnmutedEventArgs>(in root));
                                break;
                            }
                        case "BotJoinGroupEvent":
                            {
                                BotJoinedGroupEvt?.InvokeAsync(this, Utils.Deserialize<GroupEventArgs>(in root));
                                break;
                            }
                        case "BotLeaveEventActive":
                            {
                                BotPositiveLeaveGroupEvt?.InvokeAsync(this, Utils.Deserialize<GroupEventArgs>(in root));
                                break;
                            }
                        case "BotLeaveEventKick":
                            {
                                BotKickedOutEvt?.InvokeAsync(this, Utils.Deserialize<GroupEventArgs>(in root));
                                break;
                            }
                        case "GroupNameChangeEvent":
                            {
                                GroupNameChangedEvt?.InvokeAsync(this, Utils.Deserialize<GroupPropertyChangedEventArgs<string>>(in root));
                                break;
                            }
                        case "GroupEntranceAnnouncementChangeEvent":
                            {
                                GroupEntranceAnnouncementChangedEvt?.InvokeAsync(this, Utils.Deserialize<GroupPropertyChangedEventArgs<string>>(in root));
                                break;
                            }
                        case "GroupMuteAllEvent":
                            {
                                GroupMuteAllChangedEvt?.InvokeAsync(this, Utils.Deserialize<GroupPropertyChangedEventArgs<bool>>(in root));
                                break;
                            }
                        case "GroupAllowAnonymousChatEvent":
                            {
                                GroupAnonymousChatChangedEvt?.InvokeAsync(this, Utils.Deserialize<GroupPropertyChangedEventArgs<bool>>(in root));
                                break;
                            }
                        case "GroupAllowConfessTalkEvent":
                            {
                                GroupConfessTalkChangedEvt?.InvokeAsync(this, Utils.Deserialize<GroupPropertyChangedEventArgs<bool>>(in root));
                                break;
                            }
                        case "GroupAllowMemberInviteEvent":
                            {
                                GroupMemberInviteChangedEvt?.InvokeAsync(this, Utils.Deserialize<GroupPropertyChangedEventArgs<bool>>(in root));
                                break;
                            }
                        case "MemberJoinEvent":
                            {
                                GroupMemberJoinedEvt?.Invoke(this, Utils.Deserialize<MemberEventArgs>(in root));
                                break;
                            }
                        case "MemberLeaveEventKick":
                            {
                                GroupMemberKickedEvt?.InvokeAsync(this, Utils.Deserialize<MemberOperatingEventArgs>(in root));
                                break;
                            }
                        case "MemberLeaveEventQuit":
                            {
                                GroupMemberPositiveLeaveEvt?.InvokeAsync(this, Utils.Deserialize<MemberEventArgs>(in root));
                                break;
                            }
                        case "MemberCardChangeEvent":
                            {
                                GroupMemberCardChangedEvt?.InvokeAsync(this, Utils.Deserialize<GroupMemberPropertyChangedEventArgs<string>>(in root));
                                break;
                            }
                        case "MemberSpecialTitleChangeEvent":
                            {
                                GroupMemberSpecialTitleChangedEvt?.InvokeAsync(this, Utils.Deserialize<GroupMemberPropertyChangedEventArgs<string>>(in root));
                                break;
                            }
                        case "MemberPermissionChangeEvent":
                            {
                                GroupMemberPermissionChangedEvt?.InvokeAsync(this, Utils.Deserialize<GroupMemberEnumPropertyChangedEventArgs<GroupPermission>>(in root));
                                break;
                            }
                        case "MemberMuteEvent":
                            {
                                GroupMemberMutedEvt?.InvokeAsync(this, Utils.Deserialize<MemberMutedEventArgs>(in root));
                                break;
                            }
                        case "MemberUnmuteEvent":
                            {
                                GroupMemberUnmutedEvt?.InvokeAsync(this, Utils.Deserialize<MemberUnmutedEventArgs>(in root));
                                break;
                            }
                        case "NewFriendRequestEvent":
                            {
                                NewFriendApplyEvt?.InvokeAsync(this, Utils.Deserialize<NewFriendApplyEventArgs>(in root));
                                break;
                            }
                        case "MemberJoinRequestEvent":
                            {
                                GroupApplyEvt?.InvokeAsync(this, Utils.Deserialize<GroupApplyEventArgs>(in root));
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
                    try { Disconnected?.Invoke(this, e); } catch { } // 扔掉所有异常
                }
            }
        }
    }
}
