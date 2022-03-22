using System;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Mirai.CSharp.Example.Hosting
{
    public class ResponseModel
    {
        protected static readonly ResponseModel _defaultSuccessModel = CreateFromStatusCode(0, "Success", Array.Empty<object>());

        [JsonPropertyName("code")]
        public int Code { get; set; }
        [JsonPropertyName("msg")]
        public string? Message { get; set; }
        [JsonPropertyName("data")]
        public object? Data { get; set; }

        public ResponseModel() { }

        public ResponseModel(int code, string? message = null, object? data = null)
        {
            Code = code;
            Message = message;
            Data = data ?? Array.Empty<object>();
        }

        public string GetJson(JsonSerializerOptions? options = null)
            => JsonSerializer.Serialize(this, options);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ResponseModel CreateFromStatusCode(int statusCode, string? message)
            => CreateFromStatusCode(statusCode, message, Array.Empty<object>());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ResponseModel CreateFromStatusCode(int statusCode, string? message, object? data)
            => new ResponseModel { Code = -statusCode, Message = message, Data = data };

        public static ResponseModel CreateSuccess()
            => _defaultSuccessModel;

        public static ResponseModel CreateSuccess(string? message = "Success")
            => CreateFromStatusCode(0, message, Array.Empty<object>());

        public static ResponseModel CreateSuccess(string? message = "Success", object? data = null)
            => CreateFromStatusCode(0, message, data);

        public static ResponseModel CreateBadRequest()
            => CreateFromStatusCode(400, "Bad request.", Array.Empty<object>());

        public static ResponseModel CreateBadRequest(string? message = "Bad request.")
            => CreateFromStatusCode(400, message, Array.Empty<object>());

        public static ResponseModel CreateBadRequest(string? message = "Bad request.", object? data = null)
            => CreateFromStatusCode(400, message, data);

        public static ResponseModel CreateUnauthorized()
            => CreateFromStatusCode(401, "Unauthorized.", Array.Empty<object>());

        public static ResponseModel CreateUnauthorized(string? message = "Unauthorized.")
            => CreateFromStatusCode(401, message, Array.Empty<object>());

        public static ResponseModel CreateUnauthorized(string? message = "Unauthorized.", object? data = null)
            => CreateFromStatusCode(401, message, data);

        public static ResponseModel CreateForbidden()
            => CreateFromStatusCode(403, "Forbidden.", Array.Empty<object>());

        public static ResponseModel CreateForbidden(string? message = "Forbidden.")
            => CreateFromStatusCode(403, message, Array.Empty<object>());

        public static ResponseModel CreateForbidden(string? message = "Forbidden.", object? data = null)
            => CreateFromStatusCode(403, message, data);

        public static ResponseModel CreateNotFound()
            => CreateFromStatusCode(404, "Not found.", Array.Empty<object>());

        public static ResponseModel CreateNotFound(string? message = "Not found.")
            => CreateFromStatusCode(404, message, Array.Empty<object>());

        public static ResponseModel CreateNotFound(string? message = "Not found.", object? data = null)
            => CreateFromStatusCode(404, message, data);

        public static ResponseModel CreateInternalServerError()
            => CreateFromStatusCode(500, "Internal server error.", Array.Empty<object>());

        public static ResponseModel CreateInternalServerError(string? message = "Internal server error.")
            => CreateFromStatusCode(500, message, Array.Empty<object>());

        public static ResponseModel CreateInternalServerError(string? message = "Internal server error.", object? data = null)
            => CreateFromStatusCode(500, message, data);

        public static ResponseModel CreateServiceUnavailable()
            => CreateFromStatusCode(503, "Service unavailable.", Array.Empty<object>());

        public static ResponseModel CreateServiceUnavailable(string? message = "Service unavailable.")
            => CreateFromStatusCode(503, message, Array.Empty<object>());

        public static ResponseModel CreateServiceUnavailable(string? message = "Service unavailable.", object? data = null)
            => CreateFromStatusCode(503, message, data);

        public static explicit operator JsonResult(ResponseModel model)
        {
            return new JsonResult(model);
        }
    }
}
