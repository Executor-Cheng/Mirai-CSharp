using System;
using ISharedBotProfile = Mirai.CSharp.Models.IBotProfile;

namespace Mirai.CSharp.HttpApi.Models
{
    /// <inheritdoc cref="ISharedBotProfile"/>
    public interface IBotProfile : ISharedBotProfile, ICommonProfile
    {

    }

    public class BotProfile : CommonProfile, IBotProfile
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public BotProfile()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public BotProfile(string nickname, string email, int age, int level, string sign, Mirai.CSharp.Models.ProfileGender gender) : base(nickname, email, age, level, sign, gender)
        {

        }
    }
}
