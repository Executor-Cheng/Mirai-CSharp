using System;
using System.IO;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
#if NETSTANDARD2_0
using System.Runtime.InteropServices;
#endif

namespace Mirai.CSharp.HttpApi.Utility
{
    public static class WebSocketExtensions
    {
#if NETSTANDARD2_0
        public static async ValueTask ReceiveFullyAsync(this WebSocket ws, Memory<byte> buffer, CancellationToken token = default)
        {
            MemoryMarshal.TryGetArray(buffer, out ArraySegment<byte> segment);
            while (true)
            {
                var result = await ws.ReceiveAsync(segment, token);
                if (result.Count < buffer.Length)
                {
                    if (result.EndOfMessage)
                    {
                        throw new EndOfStreamException();
                    }
                    buffer = buffer.Slice(result.Count);
                }
                else
                {
                    return;
                }
            }
        }
#else
        public static async ValueTask ReceiveFullyAsync(this WebSocket ws, Memory<byte> buffer, CancellationToken token = default)
        {
            while (true)
            {
                var result = await ws.ReceiveAsync(buffer, token);
                if (result.Count < buffer.Length)
                {
                    if (result.EndOfMessage)
                    {
                        throw new EndOfStreamException();
                    }
                    buffer = buffer[result.Count..];
                }
                else
                {
                    return;
                }
            }
        }
#endif

#if NETSTANDARD2_0
        public static async ValueTask ReceiveFullyAsync(this WebSocket webSocket, MemoryStream stream, CancellationToken token = default)
        {
            var buffer = new byte[1024];
            var segment = new ArraySegment<byte>(buffer);
            WebSocketReceiveResult result;
            while (!(result = await webSocket.ReceiveAsync(segment, token)).EndOfMessage)
            {
                stream.Write(buffer, 0, result.Count);
            }
            if (result.MessageType == WebSocketMessageType.Close && stream.Length == 0)
            {
                throw new WebSocketException(10054);
            }
            stream.Write(buffer, 0, result.Count);
        }
#else
        public static async ValueTask ReceiveFullyAsync(this WebSocket webSocket, MemoryStream stream, CancellationToken token = default)
        {
            var buffer = new byte[1024];
            ValueWebSocketReceiveResult result;
            while (!(result = await webSocket.ReceiveAsync(buffer.AsMemory(), token)).EndOfMessage)
            {
                stream.Write(buffer, 0, result.Count);
            }
            if (result.MessageType == WebSocketMessageType.Close && stream.Length == 0)
            {
                throw new WebSocketException(10054);
            }
            stream.Write(buffer, 0, result.Count);
        }
#endif
    }
}
