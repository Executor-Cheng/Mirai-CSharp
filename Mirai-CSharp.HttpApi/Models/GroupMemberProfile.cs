using ISharedGroupMemberProfile = Mirai.CSharp.Models.IGroupMemberProfile;

namespace Mirai.CSharp.HttpApi.Models
{
    /// <inheritdoc cref="ISharedGroupMemberProfile"/>
    public interface IGroupMemberProfile : ISharedGroupMemberProfile, ICommonProfile
    {

    }

    public class GroupMemberProfile : CommonProfile, IGroupMemberProfile
    {
        public GroupMemberProfile()
        {

        }

        public GroupMemberProfile(string nickname, string email, int age, int level, string sign, CSharp.Models.ProfileGender gender) : base(nickname, email, age, level, sign, gender)
        {

        }
    }
}
