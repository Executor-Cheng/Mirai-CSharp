using System;
using ISharedFriendProfile = Mirai.CSharp.Models.IFriendProfile;

namespace Mirai.CSharp.HttpApi.Models
{
    /// <inheritdoc cref="ISharedFriendProfile"/>
    public interface IFriendProfile : ISharedFriendProfile, ICommonProfile
    {

    }

    public class FriendProfile : CommonProfile, IFriendProfile
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public FriendProfile()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public FriendProfile(string nickname, string email, int age, int level, string sign, CSharp.Models.ProfileGender gender) : base(nickname, email, age, level, sign, gender)
        {
            
        }
    }
}
