using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace booking_api.Models
{
    public class Response
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public static Response Ok(string message = "")
        {
            return new Response
            {
                Message = message,
                Success = true,
                StatusCode = StatusCodes.Status200OK,
            };
        }

        public static Response Created(string message = "")
        {
            return new Response
            {
                Message = message,
                Success = true,
                StatusCode = StatusCodes.Status201Created,
            };
        }

        public static Response BadRequest(string message = "")
        {
            return new Response
            {
                Message = message,
                Success = false,
                StatusCode = StatusCodes.Status400BadRequest,
            };
        }
        
        public static Response Unauthorized(string message = "")
        {
            return new Response
            {
                Message = message,
                Success = false,
                StatusCode = StatusCodes.Status401Unauthorized,
            };
        }
        
        public static Response Forbidden(string message = "")
        {
            return new Response
            {
                Message = message,
                Success = false,
                StatusCode = StatusCodes.Status403Forbidden,
            };
        }
        
        public static Response NotFound(string message = "")
        {
            return new Response
            {
                Message = message,
                Success = false,
                StatusCode = StatusCodes.Status404NotFound,
            };
        }
        
        public static Response UnprocessableEntity(string message = "")
        {
            return new Response
            {
                Message = message,
                Success = false,
                StatusCode = StatusCodes.Status422UnprocessableEntity,
            };
        }
        
        public static Response InternalServerError(string message = "")
        {
            return new Response
            {
                Message = message,
                Success = false,
                StatusCode = StatusCodes.Status500InternalServerError,
            };
        }

    }
}