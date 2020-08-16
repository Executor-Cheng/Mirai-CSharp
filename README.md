## Mirai-CSharp  
[![NuGet version (Mirai-CSharp)](https://img.shields.io/nuget/v/Mirai-CSharp.svg?style=flat)](https://www.nuget.org/packages/Mirai-CSharp/)  

### 关于本项目  
#### 这是一个帮助C#开发者与[mirai](https://github.com/mamoe/mirai)交互的项目  
#### 它通过调用[mirai-api-http](https://github.com/mamoe/mirai-api-http)提供的http-api与其交互  

### 开始使用
#### 从 nuget 上获取 **Mirai-CSharp** 即可

### 注意事项  
#### 本项目使用`C# 8.0`编写, 你需要至少`.NET Core 2.0` 或 `.NET Framework 4.6.1`才能使用本项目, 其中所有的api均为**异步**方法  
#### 使用.NET Standard 2.0的用户无法使用`Plugin\Interfaces`下的所有接口, 请自行实现`IPlugin<TEventArgs>`接口

### 一些例子  
- [配置Session](https://github.com/Executor-Cheng/Mirai-CSharp/tree/master/Mirai-CSharp.Example/Program.cs)  
- [处理好友消息](https://github.com/Executor-Cheng/Mirai-CSharp/blob/master/Mirai-CSharp.Example/ExamplePlugin.FriendMessage.cs)  
- [处理群消息](https://github.com/Executor-Cheng/Mirai-CSharp/blob/master/Mirai-CSharp.Example/ExamplePlugin.GroupMessage.cs)  
- [处理好友申请](https://github.com/Executor-Cheng/Mirai-CSharp/blob/master/Mirai-CSharp.Example/ExamplePlugin.NewFriendApply.cs)  
- [处理入群申请](https://github.com/Executor-Cheng/Mirai-CSharp/blob/master/Mirai-CSharp.Example/ExamplePlugin.GroupApply.cs)  
- [处理Bot受邀入群申请](https://github.com/Executor-Cheng/Mirai-CSharp/blob/master/Mirai-CSharp.Example/ExamplePlugin.BotInvitedJoinGroup.cs)  
- [发送图片](https://github.com/Executor-Cheng/Mirai-CSharp/blob/master/Mirai-CSharp.Example/ExamplePlugin.SendPicture.cs)  
- [处理Session掉线](https://github.com/Executor-Cheng/Mirai-CSharp/blob/master/Mirai-CSharp.Example/ExamplePlugin.Disconnected.cs)  
