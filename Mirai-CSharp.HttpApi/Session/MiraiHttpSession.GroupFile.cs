using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Mirai.CSharp.Extensions;
using Mirai.CSharp.HttpApi.Extensions;
using Mirai.CSharp.HttpApi.Models;
using ISharedGroupFileInfo = Mirai.CSharp.Models.IGroupFileInfo;
using System.Net.Http.Headers;
using System.Net.Mime;
#if NET5_0_OR_GREATER
using System.Net.Http.Json;
#endif

#pragma warning disable CA2016 // Forward the 'CancellationToken' parameter to methods
namespace Mirai.CSharp.HttpApi.Session
{
    public partial class MiraiHttpSession
    {
        /// <inheritdoc/>
        public override Task<ISharedGroupFileInfo[]> GetFilelistAsync(long groupNumber, string? id, bool fetchDownloadInfo, int offset, int size, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            KeyValuePair<string?, string?>[] payload = new KeyValuePair<string?, string?>[6]
            {
                new KeyValuePair<string?, string?>("sessionKey", session.SessionKey),
                new KeyValuePair<string?, string?>("id", id ?? null),
                new KeyValuePair<string?, string?>("target", groupNumber.ToString()),
                new KeyValuePair<string?, string?>("withDownloadInfo", fetchDownloadInfo.ToString().ToLower()),
                new KeyValuePair<string?, string?>("offset", offset.ToString()),
                new KeyValuePair<string?, string?>("size", size.ToString()),
            };
            using HttpContent content = new FormUrlEncodedContent(payload);
            string query = content.ReadAsStringAsync().GetAwaiter().GetResult();
            return _client.GetAsync($"{_options.BaseUrl}/file/list?{query}", token)
                .AsApiRespV2Async<ISharedGroupFileInfo[], GroupFileInfo[]>(token)
                .DisposeWhenCompleted(cts);
        }

        /// <inheritdoc/>
        public override Task<ISharedGroupFileInfo> GetFileInfoAsync(long groupNumber, string? id, bool fetchDownloadInfo, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            KeyValuePair<string?, string?>[] payload = new KeyValuePair<string?, string?>[4]
            {
                new KeyValuePair<string?, string?>("sessionKey", session.SessionKey),
                new KeyValuePair<string?, string?>("id", id ?? null),
                new KeyValuePair<string?, string?>("target", groupNumber.ToString()),
                new KeyValuePair<string?, string?>("withDownloadInfo", fetchDownloadInfo.ToString().ToLower()),
            };
            using HttpContent content = new FormUrlEncodedContent(payload);
            string query = content.ReadAsStringAsync().GetAwaiter().GetResult();
            return _client.GetAsync($"{_options.BaseUrl}/file/info?{query}", token)
                .AsApiRespV2Async<ISharedGroupFileInfo, GroupFileInfo>(token)
                .DisposeWhenCompleted(cts);
        }

        /// <inheritdoc/>
        public override Task CreateDirectoryAsync(long groupNumber, string? id, string directoryName, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            var payload = new
            {
                sessionKey = session.SessionKey,
                id = id ?? "",
                target = groupNumber,
                group = groupNumber,
                qq = groupNumber,
                directoryName,
            };
            return _client.PostAsJsonAsync($"{_options.BaseUrl}/file/mkdir", payload, token)
                .AsApiRespAsync(token)
                .DisposeWhenCompleted(cts);
        }

        /// <inheritdoc/>
        public override Task DeleteFileAsync(long groupNumber, string id, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            var payload = new
            {
                sessionKey = session.SessionKey,
                id = id ?? "",
                target = groupNumber,
                group = groupNumber,
                qq = groupNumber,
            };
            return _client.PostAsJsonAsync($"{_options.BaseUrl}/file/delete", payload, token)
                .AsApiRespAsync(token)
                .DisposeWhenCompleted(cts);
        }

        /// <inheritdoc/>
        public override Task MoveFileAsync(long groupNumber, string srcId, string? dstId, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            var payload = new
            {
                sessionKey = session.SessionKey,
                id = srcId,
                target = groupNumber,
                group = groupNumber,
                qq = groupNumber,
                moveTo = dstId
            };
            return _client.PostAsJsonAsync($"{_options.BaseUrl}/file/move", payload, token)
                .AsApiRespAsync(token)
                .DisposeWhenCompleted(cts);
        }

        /// <inheritdoc/>
        public override Task RenameFileAsync(long groupNumber, string id, string renameTo, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            var payload = new
            {
                sessionKey = session.SessionKey,
                id = id,
                target = groupNumber,
                group = groupNumber,
                qq = groupNumber,
                renameTo
            };
            return _client.PostAsJsonAsync($"{_options.BaseUrl}/file/rename", payload, token)
                .AsApiRespAsync(token)
                .DisposeWhenCompleted(cts);
        }

        /// <inheritdoc/>
        public override Task<ISharedGroupFileInfo> UploadFileAsync(long groupNumber, string? id, string fileName, Stream fileStream, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            MultipartFormDataContent payload = new MultipartFormDataContent(_multipartFormdataBoundary);

            payload.Add(new StringContent(session.SessionKey), "sessionKey");
            payload.Add(new StringContent("group"), "type");
            payload.Add(new StringContent(groupNumber.ToString()), "target");
            payload.Add(new StringContent(id ?? ""), "path");

            StreamContent sContent = new StreamContent(fileStream);
            payload.Add(sContent, "file", fileName);
            sContent.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Octet);

            return _client.PostAsync($"{_options.BaseUrl}/file/upload", payload, token)
                .AsApiRespV2Async<ISharedGroupFileInfo, GroupFileInfo>(token)
                .DisposeWhenCompleted(cts);
        }

        public override Task<ISharedGroupFileInfo> UploadFileAsync(string? id, long groupNumber, string FilePath, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            MultipartFormDataContent payload = new MultipartFormDataContent(_multipartFormdataBoundary);

            StringContent skContent = new StringContent(session.SessionKey);
            skContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form");
            skContent.Headers.ContentDisposition!.Name = "sessionKey";

            StringContent typeContent = new StringContent("group");
            typeContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form");
            typeContent.Headers.ContentDisposition!.Name = "type";

            StringContent targetContent = new StringContent(groupNumber.ToString());
            targetContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form");
            targetContent.Headers.ContentDisposition!.Name = "target";

            StringContent pathContent = new StringContent(id ?? "");
            pathContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form");
            pathContent.Headers.ContentDisposition!.Name = "path";

            Stream fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            string fileName = Path.GetFileName(FilePath);

            StreamContent fsContent = new StreamContent(fileStream);
            fsContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("file");
            fsContent.Headers.ContentDisposition!.Name = "file";
            fsContent.Headers.ContentDisposition.FileName = fileName;
            fsContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            payload.Add(skContent);
            payload.Add(typeContent);
            payload.Add(targetContent);
            payload.Add(pathContent);
            payload.Add(fsContent);
            return _client.PostAsync($"{_options.BaseUrl}/file/upload", payload, token)
                .AsApiRespV2Async<ISharedGroupFileInfo, GroupFileInfo>(token)
                .DisposeWhenCompleted(cts);
        }
    }
}
