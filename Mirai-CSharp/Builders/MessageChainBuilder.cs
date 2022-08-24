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
        /// <summary>
        /// 已添加的消息计数
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 使用已添加的所有消息节点创建一个 <see cref="IChatMessage"/>[] 实例
        /// </summary>
        /// <returns>一个 <see cref="IChatMessage"/>[] 实例, 可用于消息发送</returns>
        IChatMessage[] Build();

        /// <summary>
        /// 为当前的 <see cref="IMessageChainBuilder"/> 添加一条现有的 <see cref="IChatMessage"/>
        /// </summary>
        /// <param name="message"></param>
        /// <returns>传入的 <see cref="IMessageChainBuilder"/> 实例, 可继续用于链式调用</returns>
        IMessageChainBuilder Add(IChatMessage message);

        /// <summary>
        /// 为当前的 <see cref="IMessageChainBuilder"/> 添加一些现有的 <see cref="IChatMessage"/>
        /// </summary>
        /// <param name="messages">一个 <see cref="IChatMessage"/> 集合, 其中所有的实例都将添加进本构建器</param>
        /// <returns>传入的 <see cref="IMessageChainBuilder"/> 实例, 可继续用于链式调用</returns>
        IMessageChainBuilder AddRange(IEnumerable<IChatMessage> messages);

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
        /// 为给定的 <see cref="IMessageChainBuilder"/> 添加一条 <see cref="IForwardMessage"/>
        /// </summary>
        /// <param name="builder">用于构建 <see cref="IForwardMessage"/> 的构建器实例</param>
        /// <returns>传入的 <see cref="IMessageChainBuilder"/>, 可继续用于链式调用</returns>
        IMessageChainBuilder AddForwardMessage(IForwardMessageBuilder builder);

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

        /// <summary>
        /// 获取用于创建适用于本实例的 <see cref="IForwardMessage"/> 构建器实例
        /// </summary>
        /// <returns>一个合适的 <see cref="IForwardMessageBuilder"/> 实例</returns>
        IForwardMessageBuilder GetForwardMessageBuilder();
    }

    /// <summary>
    /// 用于创建发送消息时要使用的消息链数组
    /// </summary>
    public abstract class MessageChainBuilder : IMessageChainBuilder
    {
        protected readonly List<IChatMessage> _list = new List<IChatMessage>();

        /// <inheritdoc/>
        public virtual int Count => _list.Count;

        /// <inheritdoc/>
        public virtual IChatMessage[] Build()
            => _list.ToArray();

        /// <inheritdoc/>
        public virtual IMessageChainBuilder Add(IChatMessage message)
        {
            _list.Add(message);
            return this;
        }

        /// <inheritdoc/>
        public virtual IMessageChainBuilder AddRange(IEnumerable<IChatMessage> messages)
        {
            _list.AddRange(messages);
            return this;
        }

        /// <inheritdoc/>
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
        public abstract IMessageChainBuilder AddForwardMessage(IForwardMessageBuilder builder);

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

        /// <inheritdoc/>
        public abstract IForwardMessageBuilder GetForwardMessageBuilder();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
