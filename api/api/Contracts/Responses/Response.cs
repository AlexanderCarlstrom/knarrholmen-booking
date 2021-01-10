using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace api.Contracts.Responses
{
    public class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; }
        [JsonIgnore] public int StatusCode { get; set; }

        /// <summary>
        /// Basic response
        /// </summary>
        public Response(bool success, int statusCode)
        {
            Success = success;
            StatusCode = statusCode;
        }
        
        /// <summary>
        /// Basic response with message
        /// </summary>
        public Response(bool success, int statusCode, string message)
        {
            Success = success;
            StatusCode = statusCode;
            Message = message;
        }

        /// <summary>
        /// Basic error response
        /// </summary>
        public Response(int statusCode, string message)
        {
            Success = false;
            StatusCode = statusCode;
            Message = message;
        }

        public Response(int statusCode, string message, IEnumerable<string> errors)
        {
            Success = false;
            StatusCode = statusCode;
            Message = message;
            Errors = errors;
        }
    }
}