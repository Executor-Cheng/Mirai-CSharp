## 这是一个使用C#封装[mirai-api-http](https://github.com/mamoe/mirai-api-http)的项目  
### 本项目使用 `.NET Core 3.1` 编写, 其中所有的api均为**异步**方法  
### 使用方法:  
- using Mirai_CSharp;  
- using Mirai_CSharp.Models;  
- 实例化一个 `MiraiHttpSession` 和一个 `MiraiHttpSessionOptions`  
- 填写 `MiraiHttpSessionOptions` 里边的 `Host`, `Port`, `AccessKey`
- 调用 `MiraiHttpSession.ConnectAsync(MiraiHttpSessionOptions, long)`
- 监听 `MiraiHttpSession` 内的事件, 如果事件处理方法返回`true`将截断事件传播
- 使用完毕后请调用 `MiraiHttpSession.DisposeAsync` 释放`mirai-api-http`中的Session
- (`MiraiHttpSession`可以重复使用)
