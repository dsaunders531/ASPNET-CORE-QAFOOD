using Microsoft.AspNetCore.Http;
using System;

namespace mezzanine.Extensions
{
    public static class HttpResponseExtensions
    {
        public static void CreateCookie(this HttpResponse me, string key, string value, int lifeTimeDays)
        {
            me.Cookies.Append(key, value, new CookieOptions() { IsEssential = true, Expires = DateTime.Now.AddDays(lifeTimeDays) });
        }

        public static void DeleteCookie(this HttpResponse me, string key)
        {
            me.Cookies.Delete(key);
        }

        /// <summary>
        /// Return text (en-GB) for the status code. The request is used to provide extra information in the response.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static string StatusCodeMeaning(this HttpResponse response)
        {
            return response.StatusCode.ToStatusCodeMeaning(response.HttpContext?.Request ?? default(HttpRequest));
        }
    }
}
