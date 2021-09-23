using System.Text.Json.Serialization;
using Mirai.CSharp.Models;
using ISharedCommonProfile = Mirai.CSharp.Models.ICommonProfile;

namespace Mirai.CSharp.HttpApi.Models
{
    /// <inheritdoc/>
    public interface ICommonProfile : ISharedCommonProfile
    {
#if !NETSTANDARD2_0
        [JsonPropertyName("nickname")]
        abstract string ISharedCommonProfile.Nickname { get; }

        [JsonPropertyName("email")]
        abstract string ISharedCommonProfile.Email { get; }

        [JsonPropertyName("age")]
        abstract int ISharedCommonProfile.Age { get; }

        [JsonPropertyName("level")]
        abstract int ISharedCommonProfile.Level { get; }

        [JsonPropertyName("sign")]
        abstract string ISharedCommonProfile.Sign { get; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("sex")]
        abstract ProfileGender ISharedCommonProfile.Gender { get; }
#else
        [JsonPropertyName("nickname")]
        new string Nickname { get; }
        
        [JsonPropertyName("email")]
        new string Email { get; }
        
        [JsonPropertyName("age")]
        new int Age { get; }
        
        [JsonPropertyName("level")]
        new int Level { get; }
        
        [JsonPropertyName("sign")]
        new string Sign { get; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("sex")]
        new ProfileGender Gender { get; }
#endif
    }

    public abstract class CommonProfile : ICommonProfile
    {
        [JsonPropertyName("nickname")]
        public string Nickname { get; set; } = null!;

        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;

        [JsonPropertyName("age")]
        public int Age { get; set; }

        [JsonPropertyName("level")]
        public int Level { get; set; }

        [JsonPropertyName("sign")]
        public string Sign { get; set; } = null!;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("sex")]
        public ProfileGender Gender { get; set; }

        protected CommonProfile()
        {

        }

        protected CommonProfile(string nickname, string email, int age, int level, string sign, ProfileGender gender)
        {
            Nickname = nickname;
            Email = email;
            Age = age;
            Level = level;
            Sign = sign;
            Gender = gender;
        }

#if NETSTANDARD2_0
        [JsonPropertyName("nickname")]
        string ISharedCommonProfile.Nickname => Nickname;
        
        [JsonPropertyName("email")]
        string ISharedCommonProfile.Email => Email;
        
        [JsonPropertyName("age")]
        int ISharedCommonProfile.Age => Age;
        
        [JsonPropertyName("level")]
        int ISharedCommonProfile.Level => Level;
        
        [JsonPropertyName("sign")]
        string ISharedCommonProfile.Sign => Sign;
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("sex")]
        ProfileGender ISharedCommonProfile.Gender => Gender;
#endif
    }
}
