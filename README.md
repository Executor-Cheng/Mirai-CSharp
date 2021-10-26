<div align="center">
    <h1>Mirai-CSharp</h1>
</div>
<div align="center">
    <a href="https://www.nuget.org/packages/Mirai-CSharp">
        <img src="https://img.shields.io/nuget/v/Mirai-CSharp"></a>
    <a href="https://www.nuget.org/packages/Mirai-CSharp">
    	<img src="https://img.shields.io/nuget/vpre/Mirai-CSharp"></a>
    <a href="https://www.nuget.org/packages/Mirai-CSharp">
	    <img src="https://img.shields.io/nuget/dt/Mirai-CSharp"></a>
    <img src="https://img.shields.io/github/last-commit/Executor-Cheng/Mirai-CSharp">
    <img src="https://img.shields.io/github/stars/Executor-Cheng/Mirai-CSharp"
</div>



## 关于本项目  
这是一个帮助C#开发者与 [Mirai](https://github.com/mamoe/mirai) 交互的项目  
它通过调用 [mirai-api-http](https://github.com/mamoe/mirai-api-http) 提供的 http-api 与其交互  

## 开始使用

### 安装
最简单的方式是从 nuget 上获取 Mirai-CSharp 包, 并且我们也推荐你在 nuget 包管理器中为项目安装它, 不过你也可以手动克隆项目, 编译, 并直接引用链接库.

> 在使用 nuget 安装包时, 如若要使用最新功能, 请勾选 "包括发行版"
>
> 注意, 最新版本已将包分离为 Mirai-CSharp 以及 Mirai-CSharp.HttpApi, 其中第一个中只包含程序接口之类的, 第二个中包含的是其实现

### 示例

下面以一个最简单的控制台程序为示例, 对 QQ 内的任何 at 自己了的群聊消息响应 "Hello world" 文本消息

在目前的最新版本中, Mirai-CSharp 的常用核心组件位于 Mirai.CSharp 以及 Mirai.CSharp.Models 命名空间中.

> 在已正式发布的最新版本中, 命名空间是 Mirai_CSharp 而不是 Mirai.CSharp

首先我们可以引用它, 下面是基础框架:

```csharp
using System;
using System.Linq;
using Mirai.CSharp;
using Mirai.CSharp.Models;

namespace TestProj
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
}
```

Mirai-CSharp 是要与 Mirai 的 mira-http-api 进行交互的, 所以我们接下来创建一个会话(Session), 并连接到在 Mirai 中已经登录的 QQ

```csharp
// 下面是位于 Main 方法的代码
MiraiHttpSession session = new MiraiHttpSession();               // 创建会话
session.ConnectAsync(                                            // 连接并等待
    new MiraiHttpSessionOptions("localhost", 1234, "authKey"),   // 连接选项, 地址, 端口, 以及验证密钥, 这些均位于 mirai-http-api 配置文件中
    1234567890).Wait();                                          // Mirai 中已经登录的 QQ 机器人的 QQ 号码	
```

下面为 session 添加群聊成员消息时间的处理方法:

```csharp
// 下面是位于 Main 方法的代码
session.GroupMessageEvt += async (sender, e) =>      
{
    await sender.SendGroupMessageAsync(e.Sender.Group.Id, new PlainMessage("Hello world"));   // 在消息发送者所在的群聊内发送 Hello world
    return false;
};
session.GroupMessageEvt += async (sender, e) =>      // Mirai-CSharp 的事件处理应该是纯异步的, 我们应该使用异步方法(返回Task<bool>)
{
    if (e.Chain.Where(v => v is AtMessage atMsg && atMsg.Target == session.QQNumber).Any())       // 判断是否 at 自己
        await sender.SendGroupMessageAsync(e.Sender.Group.Id, new PlainMessage("Hello world"));   // 发送 "Hello world"
    
    // PlainMessage 位于 Mirai.CSharp.Models 命名空间下, 基于 IMessage 

    return false;    // Task 的返回结果标识当前事件是否被阻断, 如果返回 true, 那么后面的事件订阅者将不会收到事件 (这里返回false表示不阻断)
};
```





## 注意事项  
- 本项目使用`C# 9.0`编写, 你需要至少`.NET Core 2.0` 或 `.NET Framework 4.6.1`才能使用本项目, 其中所有的api均为**异步**方法  

## 使用例子
- [基于任何实现框架的插件](https://github.com/Executor-Cheng/Mirai-CSharp/blob/master/Mirai-CSharp.Example/MiraiPlugin.cs)
- [基于特定实现框架的插件](https://github.com/Executor-Cheng/Mirai-CSharp/blob/master/Mirai-CSharp.Example/HttpApiPlugin.cs)
- [对动态增删的注释](https://github.com/Executor-Cheng/Mirai-CSharp/blob/master/Mirai-CSharp.Example/DynamicPlugin.cs)
- [配置Session](https://github.com/Executor-Cheng/Mirai-CSharp/tree/master/Mirai-CSharp.Example/Program.cs)  
- [处理好友消息](https://github.com/Executor-Cheng/Mirai-CSharp/blob/master/Mirai-CSharp.Example/ExamplePlugin.FriendMessage.cs) 
- [处理群消息](https://github.com/Executor-Cheng/Mirai-CSharp/blob/master/Mirai-CSharp.Example/ExamplePlugin.GroupMessage.cs)  
- [处理好友申请](https://github.com/Executor-Cheng/Mirai-CSharp/blob/master/Mirai-CSharp.Example/ExamplePlugin.NewFriendApply.cs)  
- [处理入群申请](https://github.com/Executor-Cheng/Mirai-CSharp/blob/master/Mirai-CSharp.Example/ExamplePlugin.GroupApply.cs)  
- [处理Bot受邀入群申请](https://github.com/Executor-Cheng/Mirai-CSharp/blob/master/Mirai-CSharp.Example/ExamplePlugin.BotInvitedJoinGroup.cs)  
- [发送图片](https://github.com/Executor-Cheng/Mirai-CSharp/blob/master/Mirai-CSharp.Example/ExamplePlugin.SendPicture.cs)  
- [处理Session掉线](https://github.com/Executor-Cheng/Mirai-CSharp/blob/master/Mirai-CSharp.Example/ExamplePlugin.Disconnected.cs)  
