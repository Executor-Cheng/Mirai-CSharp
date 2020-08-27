using Mirai_CSharp.Models;
using System;

namespace Mirai_CSharp.Extensions
{
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// 为给定的 <see cref="IMessageBuilder"/> 添加一条 <see cref="PlainMessage"/>
        /// </summary>
        /// <param name="builder">要添加到的目标</param>
        /// <param name="text">消息内容</param>
        /// <remarks>
        /// 不会检查传入的 <paramref name="text"/> 为非 <see langword="null"/> 或非空
        /// </remarks>
        /// <returns>传入的 <see cref="IMessageBuilder"/>, 可继续用于链式调用</returns>
        public static IMessageBuilder AddPlainMessage(this IMessageBuilder builder, string text)
        {
            return builder.Add(new PlainMessage(text));
        }
        /// <summary>
        /// 为给定的 <see cref="IMessageBuilder"/> 添加一条 <see cref="ImageMessage"/>
        /// </summary>
        /// <param name="builder">要添加到的目标</param>
        /// <param name="imageId">图片格式。不为空时将忽略 <paramref name="url"/> 参数
        /// <list type="number">
        /// <item><term>{XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX}.mirai</term><description>群图片</description></item>
        /// <item><term>/XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX</term><description>好友图片</description></item>
        /// </list>
        /// </param>
        /// <param name="url">网络图片链接</param>
        /// <param name="path">本地图片路径。相对路径于 plugins/MiraiAPIHTTP/images</param>
        /// <remarks>
        /// 不会检查传入的 <paramref name="imageId"/>, <paramref name="url"/>, <paramref name="path"/> 至少有一个为非 <see langword="null"/>
        /// </remarks>
        /// <returns>传入的 <see cref="IMessageBuilder"/>, 可继续用于链式调用</returns>
        public static IMessageBuilder AddImageMessage(this IMessageBuilder builder, string? imageId = null, string? url = null, string? path = null)
        {
            return builder.Add(new ImageMessage(imageId, url, path));
        }
        /// <summary>
        /// 为给定的 <see cref="IMessageBuilder"/> 添加一条 <see cref="FlashImageMessage"/>
        /// </summary>
        /// <param name="builder">要添加到的目标</param>
        /// <param name="imageId">图片格式。不为空时将忽略 <paramref name="url"/> 参数
        /// <list type="number">
        /// <item><term>{XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX}.mirai</term><description>群图片</description></item>
        /// <item><term>/XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX</term><description>好友图片</description></item>
        /// </list>
        /// </param>
        /// <param name="url">网络图片链接</param>
        /// <param name="path">本地图片路径。相对路径于 plugins/MiraiAPIHTTP/images</param>
        /// <remarks>
        /// 不会检查传入的 <paramref name="imageId"/>, <paramref name="url"/>, <paramref name="path"/> 至少有一个为非 <see langword="null"/>
        /// </remarks>
        /// <returns>传入的 <see cref="IMessageBuilder"/>, 可继续用于链式调用</returns>
        public static IMessageBuilder AddFlashImageMessage(this IMessageBuilder builder, string? imageId = null, string? url = null, string? path = null)
        {
            return builder.Add(new FlashImageMessage(imageId, url, path));
        }
        /// <summary>
        /// 为给定的 <see cref="IMessageBuilder"/> 添加一条 <see cref="AtMessage"/>
        /// </summary>
        /// <param name="builder">要添加到的目标</param>
        /// <param name="target">要@的群员QQ号</param>
        /// <remarks>
        /// 不会检查传入的 <paramref name="target"/> 是否有效
        /// </remarks>
        /// <returns>传入的 <see cref="IMessageBuilder"/>, 可继续用于链式调用</returns>
        public static IMessageBuilder AddAtMessage(this IMessageBuilder builder, long target)
        {
            return builder.Add(new AtMessage(target));
        }
        /// <summary>
        /// 为给定的 <see cref="IMessageBuilder"/> 添加一条 <see cref="AtAllMessage"/>
        /// </summary>
        /// <param name="builder">要添加到的目标</param>
        /// <returns>传入的 <see cref="IMessageBuilder"/>, 可继续用于链式调用</returns>
        public static IMessageBuilder AddAtAllMessage(this IMessageBuilder builder)
        {
            return builder.Add(new AtAllMessage());
        }
        /// <summary>
        /// 为给定的 <see cref="IMessageBuilder"/> 添加一条 <see cref="FaceMessage"/>
        /// </summary>
        /// <param name="builder">要添加到的目标</param>
        /// <param name="id">QQ表情编号, 优先高于 <paramref name="name"/></param>
        /// <param name="name">QQ表情拼音, 可选</param>
        /// <remarks>
        /// 不会检查传入的参数是否有效。 <paramref name="id"/> 的取值参见 <a href="https://github.com/mamoe/mirai/blob/master/mirai-core/src/commonMain/kotlin/net.mamoe.mirai/message/data/Face.kt#L41"/>
        /// </remarks>
        /// <returns>传入的 <see cref="IMessageBuilder"/>, 可继续用于链式调用</returns>
        public static IMessageBuilder AddFaceMessage(this IMessageBuilder builder, int id, string? name = null)
        {
            return builder.Add(new FaceMessage(id, name));
        }
        /// <summary>
        /// 为给定的 <see cref="IMessageBuilder"/> 添加一条 <see cref="XmlMessage"/>
        /// </summary>
        /// <param name="builder">要添加到的目标</param>
        /// <param name="xml">xml原始字符串</param>
        /// <remarks>
        /// 不会检查传入的 <paramref name="xml"/> 是否有效
        /// </remarks>
        /// <returns>传入的 <see cref="IMessageBuilder"/>, 可继续用于链式调用</returns>
        public static IMessageBuilder AddXmlMessage(this IMessageBuilder builder, string xml)
        {
            return builder.Add(new XmlMessage(xml));
        }
        /// <summary>
        /// 为给定的 <see cref="IMessageBuilder"/> 添加一条 <see cref="JsonMessage"/>
        /// </summary>
        /// <param name="builder">要添加到的目标</param>
        /// <param name="json">json原始字符串</param>
        /// <remarks>
        /// 不会检查传入的 <paramref name="json"/> 是否有效
        /// </remarks>
        /// <returns>传入的 <see cref="IMessageBuilder"/>, 可继续用于链式调用</returns>
        public static IMessageBuilder AddJsonMessage(this IMessageBuilder builder, string json)
        {
            return builder.Add(new JsonMessage(json));
        }
        /// <summary>
        /// 为给定的 <see cref="IMessageBuilder"/> 添加一条 <see cref="AppMessage"/>
        /// </summary>
        /// <param name="builder">要添加到的目标</param>
        /// <param name="content">消息内容</param>
        /// <remarks>
        /// 不会检查传入的 <paramref name="content"/> 是否有效
        /// </remarks>
        /// <returns>传入的 <see cref="IMessageBuilder"/>, 可继续用于链式调用</returns>
        public static IMessageBuilder AddAppMessage(this IMessageBuilder builder, string content)
        {
            return builder.Add(new AppMessage(content));
        }
        /// <summary>
        /// 为给定的 <see cref="IMessageBuilder"/> 添加一条 <see cref="PokeMessage"/>
        /// </summary>
        /// <param name="builder">要添加到的目标</param>
        /// <param name="name">戳一戳的类型</param>
        /// <remarks>
        /// 不会检查传入的 <paramref name="name"/> 是否有效
        /// </remarks>
        /// <returns>传入的 <see cref="IMessageBuilder"/>, 可继续用于链式调用</returns>
        public static IMessageBuilder AddPokeMessage(this IMessageBuilder builder, PokeMessage.PokeType name)
        {
            return builder.Add(new PokeMessage(name));
        }
        /// <summary>
        /// 为给定的 <see cref="IMessageBuilder"/> 添加一条 <see cref="VoiceMessage"/>
        /// </summary>
        /// <param name="builder">要添加到的目标</param>
        /// <param name="fileName">语音文件路径</param>
        /// <remarks>
        /// 不会检查传入的 <paramref name="fileName"/> 是否存在
        /// </remarks>
        /// <returns>传入的 <see cref="IMessageBuilder"/>, 可继续用于链式调用</returns>
        [Obsolete("请使用AddVoiceMessage(IMessageBuilder, string, string, string)。", true)]
        public static IMessageBuilder AddVoiceMessage(this IMessageBuilder builder, string? fileName)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 为给定的 <see cref="IMessageBuilder"/> 添加一条 <see cref="VoiceMessage"/>
        /// </summary>
        /// <param name="builder">要添加到的目标</param>
        /// <param name="voiceId">语音Id</param>
        /// <param name="url">用于下载并发送语音的Url</param>
        /// <param name="path">语音文件路径, 相对路径于 plugins/MiraiAPIHTTP/voices</param>
        /// <remarks>
        /// 不会检查传入的 <paramref name="path"/> 是否存在
        /// </remarks>
        /// <returns>传入的 <see cref="IMessageBuilder"/>, 可继续用于链式调用</returns>
        public static IMessageBuilder AddVoiceMessage(this IMessageBuilder builder, string? voiceId = null, string? url = null, string? path = null)
        {
            return builder.Add(new VoiceMessage(voiceId, url, path));
        }
    }
}
