using System.Collections;
using System.Collections.Generic;
using Mirai.CSharp.Models;
using Mirai.CSharp.Models.ChatMessages;

namespace Mirai.CSharp.Builders
{
    /// <summary>
    /// 用于创建发送消息时要使用的消息链数组的接口
    /// </summary>
    /// <remarks>
    /// 不保证实现类的所有方法一定线程安全
    /// </remarks>
    public interface IMessageChainBuilder : IEnumerable<IChatMessage>
    {
        int Count { get; }

        IChatMessage[] Build();

        IMessageChainBuilder Add(IChatMessage message);

        IMessageChainBuilder AddRange(IEnumerable<IChatMessage> message);

        /// <summary>
        /// 为给定的 <see cref="IMessageChainBuilder"/> 添加一条 <see cref="IPlainMessage"/>
        /// </summary>
        /// <param name="text">消息内容</param>
        /// <remarks>
        /// 不会检查传入的 <paramref name="text"/> 为非 <see langword="null"/> 或非空
        /// </remarks>
        /// <returns>传入的 <see cref="IMessageChainBuilder"/>, 可继续用于链式调用</returns>
        IMessageChainBuilder AddPlainMessage(string text);

        /// <summary>
        /// 为给定的 <see cref="IMessageChainBuilder"/> 添加一条 <see cref="IImageMessage"/>
        /// </summary>
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
        /// <returns>传入的 <see cref="IMessageChainBuilder"/>, 可继续用于链式调用</returns>
        IMessageChainBuilder AddImageMessage(string? imageId = null, string? url = null, string? path = null);

        /// <summary>
        /// 为给定的 <see cref="IMessageChainBuilder"/> 添加一条 <see cref="IFlashImageMessage"/>
        /// </summary>
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
        /// <returns>传入的 <see cref="IMessageChainBuilder"/>, 可继续用于链式调用</returns>
        IMessageChainBuilder AddFlashImageMessage(string? imageId = null, string? url = null, string? path = null);

        /// <summary>
        /// 为给定的 <see cref="IMessageChainBuilder"/> 添加一条 <see cref="IAtMessage"/>
        /// </summary>
        /// <param name="target">要@的群员QQ号</param>
        /// <remarks>
        /// 不会检查传入的 <paramref name="target"/> 是否有效
        /// </remarks>
        /// <returns>传入的 <see cref="IMessageChainBuilder"/>, 可继续用于链式调用</returns>
        IMessageChainBuilder AddAtMessage(long target);

        /// <summary>
        /// 为给定的 <see cref="IMessageChainBuilder"/> 添加一条 <see cref="IAtAllMessage"/>
        /// </summary>
        /// <returns>传入的 <see cref="IMessageChainBuilder"/>, 可继续用于链式调用</returns>
        IMessageChainBuilder AddAtAllMessage();

        /// <summary>
        /// 为给定的 <see cref="IMessageChainBuilder"/> 添加一条 <see cref="IFaceMessage"/>
        /// </summary>
        /// <param name="id">QQ表情编号, 优先高于 <paramref name="name"/></param>
        /// <param name="name">QQ表情拼音, 可选</param>
        /// <remarks>
        /// 不会检查传入的参数是否有效。 <paramref name="id"/> 的取值参见 <a href="https://github.com/mamoe/mirai/blob/master/mirai-core/src/commonMain/kotlin/net.mamoe.mirai/message/data/Face.kt#L41"/>
        /// </remarks>
        /// <returns>传入的 <see cref="IMessageChainBuilder"/>, 可继续用于链式调用</returns>
        IMessageChainBuilder AddFaceMessage(int id, string? name = null);

        /// <summary>
        /// 为给定的 <see cref="IMessageChainBuilder"/> 添加一条 <see cref="IXmlMessage"/>
        /// </summary>
        /// <param name="xml">xml原始字符串</param>
        /// <remarks>
        /// 不会检查传入的 <paramref name="xml"/> 是否有效
        /// </remarks>
        /// <returns>传入的 <see cref="IMessageChainBuilder"/>, 可继续用于链式调用</returns>
        IMessageChainBuilder AddXmlMessage(string xml);

        /// <summary>
        /// 为给定的 <see cref="IMessageChainBuilder"/> 添加一条 <see cref="IJsonMessage"/>
        /// </summary>
        /// <param name="json">json原始字符串</param>
        /// <remarks>
        /// 不会检查传入的 <paramref name="json"/> 是否有效
        /// </remarks>
        /// <returns>传入的 <see cref="IMessageChainBuilder"/>, 可继续用于链式调用</returns>
        IMessageChainBuilder AddJsonMessage(string json);

        /// <summary>
        /// 为给定的 <see cref="IMessageChainBuilder"/> 添加一条 <see cref="IAppMessage"/>
        /// </summary>
        /// <param name="content">消息内容</param>
        /// <remarks>
        /// 不会检查传入的 <paramref name="content"/> 是否有效
        /// </remarks>
        /// <returns>传入的 <see cref="IMessageChainBuilder"/>, 可继续用于链式调用</returns>
        IMessageChainBuilder AddAppMessage(string content);

        /// <summary>
        /// 为给定的 <see cref="IMessageChainBuilder"/> 添加一条 <see cref="IPokeMessage"/>
        /// </summary>
        /// <param name="name">戳一戳的类型</param>
        /// <remarks>
        /// 不会检查传入的 <paramref name="name"/> 是否有效
        /// </remarks>
        /// <returns>传入的 <see cref="IMessageChainBuilder"/>, 可继续用于链式调用</returns>
        IMessageChainBuilder AddPokeMessage(PokeType name);

        /// <summary>
        /// 为给定的 <see cref="IMessageChainBuilder"/> 添加一条 <see cref="IVoiceMessage"/>
        /// </summary>
        /// <param name="voiceId">语音Id</param>
        /// <param name="url">用于下载并发送语音的Url</param>
        /// <param name="path">语音文件路径, 相对路径于 plugins/MiraiAPIHTTP/voices</param>
        /// <remarks>
        /// 不会检查传入的 <paramref name="path"/> 是否存在
        /// </remarks>
        /// <returns>传入的 <see cref="IMessageChainBuilder"/>, 可继续用于链式调用</returns>
        IMessageChainBuilder AddVoiceMessage(string? voiceId = null, string? url = null, string? path = null);
    }

    /// <summary>
    /// 用于创建发送消息时要使用的消息链数组
    /// </summary>
    public abstract class MessageChainBuilder : IMessageChainBuilder
    {
        protected readonly List<IChatMessage> _list = new List<IChatMessage>();

        public virtual int Count => _list.Count;

        public virtual IChatMessage[] Build()
            => _list.ToArray();

        public virtual IMessageChainBuilder Add(IChatMessage message)
        {
            _list.Add(message);
            return this;
        }

        public virtual IMessageChainBuilder AddRange(IEnumerable<IChatMessage> message)
        {
            _list.AddRange(message);
            return this;
        }

        public virtual IEnumerator<IChatMessage> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        /// <inheritdoc/>
        public abstract IMessageChainBuilder AddPlainMessage(string text);

        /// <inheritdoc/>
        public abstract IMessageChainBuilder AddImageMessage(string? imageId = null, string? url = null, string? path = null);

        /// <inheritdoc/>
        public abstract IMessageChainBuilder AddFlashImageMessage(string? imageId = null, string? url = null, string? path = null);

        /// <inheritdoc/>
        public abstract IMessageChainBuilder AddAtMessage(long target);

        /// <inheritdoc/>
        public abstract IMessageChainBuilder AddAtAllMessage();

        /// <inheritdoc/>
        public abstract IMessageChainBuilder AddFaceMessage(int id, string? name = null);

        /// <inheritdoc/>
        public abstract IMessageChainBuilder AddXmlMessage(string xml);

        /// <inheritdoc/>
        public abstract IMessageChainBuilder AddJsonMessage(string json);

        /// <inheritdoc/>
        public abstract IMessageChainBuilder AddAppMessage(string content);

        /// <inheritdoc/>
        public abstract IMessageChainBuilder AddPokeMessage(PokeType name);

        /// <inheritdoc/>
        public abstract IMessageChainBuilder AddVoiceMessage(string? voiceId = null, string? url = null, string? path = null);

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
