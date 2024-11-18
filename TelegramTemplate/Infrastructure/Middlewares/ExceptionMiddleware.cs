﻿using TelegramTemplate.Infrastructure.Exceptions.Abstract;
using TelegramTemplate.Infrastructure.Exceptions.Model;
using static TelegramTemplate.Infrastructure.Exceptions.Model.ExceptionConstants;

namespace TelegramTemplate.Infrastructure.Middlewares;

public class ExceptionMidleware
    {
        private ILogger<ExceptionMidleware> Logger { get; }
        private readonly RequestDelegate _next;

        public ExceptionMidleware(RequestDelegate next, ILogger<ExceptionMidleware> logger)
        {
            _next = next;
            Logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

                await HandleHttpCodes(context);
            }
            catch (OperationCanceledException)
            {
                Logger.LogInformation("Request was canceled");
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleHttpCodes(HttpContext context)
        {
            var statusCode = context.Response.StatusCode;

            var message = statusCode switch
            {
                StatusCodes.Status404NotFound => NOT_FOUND_MESSAGE,
                StatusCodes.Status403Forbidden => FORBIDDEN_MESSAGE,
                StatusCodes.Status401Unauthorized => NOT_AUTHORIZED_MESSAGE,
                StatusCodes.Status405MethodNotAllowed => GetMethodNotAllowedMessage(context.Request.Method),
                _ => null
            };

            if (message != null)
            {
                await WriteJsonContentAsync(context, statusCode, message);
                Logger.LogError("Exception occured: {Message}", message);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            Logger.LogError(ex, "");

            return ex is IHttpException e ?
                WriteJsonContentAsync(context, e.StatusCode, e.GetMessage())
                :
                WriteJsonContentAsync(context, StatusCodes.Status500InternalServerError, new ExceptionMessageDto { Message = ex.Message });

        }

        private async Task WriteJsonContentAsync(HttpContext context, int statucCode, object messageObject)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statucCode;

            await context.Response.WriteAsJsonAsync(messageObject);
        }
    }