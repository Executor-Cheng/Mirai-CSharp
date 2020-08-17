using System;
using System.IO;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Mirai_CSharp.Utility
{
    public static class WebSocketExtensions
    {
#if NETSTANDARD2_0
        public static async ValueTask ReceiveFullyAsync(this WebSocket ws, Memory<byte> buffer, CancellationToken token = default)
        {
            MemoryMarshal.TryGetArray(buffer, out ArraySegment<byte> segment);
            while (true)
            {
                WebSocketReceiveResult result = await ws.ReceiveAsync(segment, token);
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
                ValueWebSocketReceiveResult result = await ws.ReceiveAsync(buffer, token);
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
#endif

#if NETSTANDARD2_0
        public static async ValueTask<MemoryStream> ReceiveFullyAsync(this WebSocket webSocket, CancellationToken token = default)
        {
            byte[] buffer = new byte[1024];
            ArraySegment<byte> segment = new ArraySegment<byte>(buffer);
            MemoryStream ms = new MemoryStream(1024);
            try
            {
                WebSocketReceiveResult result;
                while (!(result = await webSocket.ReceiveAsync(segment, token)).EndOfMessage)
                {
                    ms.Write(buffer, 0, result.Count);
                }
                if (result.MessageType == WebSocketMessageType.Close && ms.Length == 0)
                {
                    throw new WebSocketException(10054);
                }
                ms.Write(buffer, 0, result.Count);
                return ms;
            }
            catch
            {
                ms.Dispose();
                throw;
            }
        }
#else
        public static async ValueTask<MemoryStream> ReceiveFullyAsync(this WebSocket webSocket, CancellationToken token = default)
        {
            byte[] buffer = new byte[1024];
            MemoryStream ms = new MemoryStream(1024);
            try
            {
                ValueWebSocketReceiveResult result;
                while (!(result = await webSocket.ReceiveAsync(buffer.AsMemory(), token)).EndOfMessage)
                {
                    ms.Write(buffer, 0, result.Count);
                }
                if (result.MessageType == WebSocketMessageType.Close && ms.Length == 0)
                {
                    throw new WebSocketException(10054);
                }
                ms.Write(buffer, 0, result.Count);
                return ms;
            }
            catch
            {
                ms.Dispose();
                throw;
            }
        }
#endif
    }
}
