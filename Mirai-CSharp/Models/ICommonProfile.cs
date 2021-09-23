namespace Mirai.CSharp.Models
{
    /// <summary>
    /// 提供通用资料信息的接口
    /// </summary>
    public interface ICommonProfile
    {
        string Nickname { get; }

        string Email { get; }

        int Age { get; }

        int Level { get; }

        string Sign { get; }

        ProfileGender Gender { get; }
    }
}
