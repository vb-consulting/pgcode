﻿using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Pgcode.Middleware
{
    public class CookieUserProfileModel
    {
        public string User { get; set; }
    }

    public class CookieMiddleware : IMiddleware
    {
        private const string CookieName = "pgcode";

        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            IgnoreNullValues = false,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };

        private static readonly CookieOptions CookieOptions = new CookieOptions
        {
            IsEssential = true,
            Path = "/",
            Expires = DateTime.UtcNow.AddDays(30)
        };

        public void ProcessCookieAndAddIdentity(HttpContext context)
        {
            var cookieModel = context.Request.Cookies.TryGetValue(CookieName, out var value)
                ? JsonSerializer.Deserialize<CookieUserProfileModel>(value, JsonOptions)
                : new CookieUserProfileModel();

            if (Program.Settings.RunAsUser != null)
            {
                cookieModel.User = Program.Settings.RunAsUser;
            }

            if (string.IsNullOrEmpty(cookieModel.User))
            {
                cookieModel.User = $"{Guid.NewGuid().ToString().Substring(0, 8)}";
            }

            context.User = new ClaimsPrincipal();
            context.User.AddIdentity(new GenericIdentity(cookieModel.User));
            context.Response.Cookies.Append(CookieName, JsonSerializer.Serialize(cookieModel, JsonOptions), CookieOptions);
        }

        public void ProcessHttpContext(HttpContext context)
        {
            if (context.Request.Path != "/")
            {
                return;
            }

            ProcessCookieAndAddIdentity(context);
        }
    }
}
