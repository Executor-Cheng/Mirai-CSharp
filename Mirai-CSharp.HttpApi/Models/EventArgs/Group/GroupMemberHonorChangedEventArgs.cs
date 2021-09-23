using System;
using System.Text.Json.Serialization;
using Mirai.CSharp.Models;
using ISharedGroupMemberHonorChangedEventArgs = Mirai.CSharp.Models.EventArgs.IGroupMemberHonorChangedEventArgs;
using ISharedJsonGroupMemberHonorChangedEventArgs = Mirai.CSharp.Models.EventArgs.IGroupMemberHonorChangedEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    public interface IGroupMemberHonorChangedEventArgs : ISharedJsonGroupMemberHonorChangedEventArgs, IMemberEventArgs
    {
#if NETSTANDARD2_0
        [JsonPropertyName("action")]
        new GroupHonorState State { get; }
        
        [JsonPropertyName("honor")]
        new string Honor { get; }
#else
        [JsonPropertyName("action")]
        abstract GroupHonorState ISharedGroupMemberHonorChangedEventArgs.State { get; }

        [JsonPropertyName("honor")]
        abstract string ISharedGroupMemberHonorChangedEventArgs.Honor { get; }
#endif
    }

    public class GroupMemberHonorChangedEventArgs : MemberEventArgs, IGroupMemberHonorChangedEventArgs
    {
        [JsonPropertyName("action")]
        public GroupHonorState State { get; set; }

        [JsonPropertyName("honor")]
        public string Honor { get; set; } = null!;

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberHonorChangedEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberHonorChangedEventArgs(IGroupMemberInfo member, GroupHonorState state, string honor) : base(member)
        {
            State = state;
            Honor = honor;
        }

#if NETSTANDARD2_0
        [JsonPropertyName("action")]
        GroupHonorState ISharedGroupMemberHonorChangedEventArgs.State => State;

        [JsonPropertyName("honor")]
        string ISharedGroupMemberHonorChangedEventArgs.Honor => Honor;
#endif
    }
}
