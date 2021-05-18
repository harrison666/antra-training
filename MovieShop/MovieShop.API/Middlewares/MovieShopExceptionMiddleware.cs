using ApplicationCore.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MovieShop.API.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MovieShopExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public MovieShopExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleException(httpContext, e);

            }
            
        }

        private async Task HandleException(HttpContext httpConext, Exception e)
        {
            // always give user friendly message, never send actual exception to the user
            // log the exception, text files, database, json diles.
            // Date time, actual error message, Stack Trace, User,
            // Send Notificaiton to Development team email
            // send proper http status codes.
            switch (e)
            {
                case ConflictException conflictException:
                    httpConext.Response.StatusCode = (int) HttpStatusCode.Conflict;
                    break;
                case NotFoundException notFoundException:
                    httpConext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case UnauthorizedAccessException unauthorizedAccessException:
                    httpConext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;
                case Exception exception:
                    httpConext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;

            }

            
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MovieShopExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseMovieShopExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MovieShopExceptionMiddleware>();
        }
    }
}
