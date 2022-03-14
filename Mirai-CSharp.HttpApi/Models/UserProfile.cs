using System;
using ISharedUserProfile = Mirai.CSharp.Models.IUserProfile;

namespace Mirai.CSharp.HttpApi.Models
{
    /// <inheritdoc cref="ISharedFriendProfile"/>
    public interface IUserProfile : ISharedUserProfile, ICommonProfile
    {

    }

    public class UserProfile : CommonProfile, IUserProfile
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public UserProfile()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public UserProfile(string nickname, string email, int age, int level, string sign, CSharp.Models.ProfileGender gender) : base(nickname, email, age, level, sign, gender)
        {
            
        }
    }
}
