using Microsoft.Extensions.Logging;
using System;

namespace mezzanine.Extensions
{
    /// <summary>
    /// Extensions for the string type.
    /// </summary>
    public static class StringExtentensions
    {
        /// <summary>
        /// Remove line breaks, tabs etc from a string.
        /// </summary>
        /// <param name="trim">Optionally, trim the white space from the string</param>
        /// <param name="value">The string you want to minify.</param>
        /// <returns>A flat string</returns>
        public static string Minify(this string value, bool trim = true)
        {
            value = value.Replace("\r", string.Empty); // Carriage Return
            value = value.Replace("\n", string.Empty); // New Line
            value = value.Replace(Environment.NewLine, string.Empty);

            value = value.Replace("\0", string.Empty); // Null
            value = value.Replace("\a", string.Empty); // Alert
            value = value.Replace("\b", string.Empty); // Backspace
            value = value.Replace("\f", string.Empty); // Form feed
            value = value.Replace("\t", string.Empty); // Horizontal tab
            value = value.Replace("\v", string.Empty); // Vertical tab

            if (trim == true)
            {
                value = value.Trim();
            }

            return value;
        }

        /// <summary>
        /// Add an s to the end of text when the count is above 1.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="count"></param>
        /// <returns></returns>        
        public static string Pluralize(this string value, decimal count)
        {
            if (count != 1)
            {
                value += "s";
            }

            return value;
        }

        public static string Pluralize(this string value, long count)
        {
            return Pluralize(value, (decimal)count);
        }

        public static string Pluralize(this string value, int count)
        {
            return Pluralize(value, (decimal)count);
        }

        public static string Pluralize(this string value, short count)
        {
            return Pluralize(value, (decimal)count);
        }

        public static string Pluralize(this string value, byte count)
        {
            return Pluralize(value, (decimal)count);
        }

        public static string Pluralize(this string value, double count)
        {
            return Pluralize(value, (decimal)count);
        }

        /// <summary>
        /// Get the log level from a string (ie: from the json config file)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static LogLevel ToLogLevel(this string value)
        {
            LogLevel retLevel = Microsoft.Extensions.Logging.LogLevel.None;

            switch (value.ToLower())
            {
                case "trace":
                case "0":
                    retLevel = Microsoft.Extensions.Logging.LogLevel.Trace;
                    break;

                case "debug":
                case "1":
                    retLevel = Microsoft.Extensions.Logging.LogLevel.Debug;
                    break;

                case "information":
                case "info":
                case "2":
                    retLevel = Microsoft.Extensions.Logging.LogLevel.Information;
                    break;

                case "warning":
                case "3":
                    retLevel = Microsoft.Extensions.Logging.LogLevel.Warning;
                    break;

                case "error":
                case "4":
                    retLevel = Microsoft.Extensions.Logging.LogLevel.Error;
                    break;

                case "critical":
                case "5":
                    retLevel = Microsoft.Extensions.Logging.LogLevel.Error;
                    break;

                case "none":
                case "6":
                    retLevel = Microsoft.Extensions.Logging.LogLevel.None;
                    break;

                default:
                    retLevel = Microsoft.Extensions.Logging.LogLevel.None;
                    break;
            }

            return retLevel;
        }

        /// <summary>
        /// Return text (en-GB) for the status code. The request is used to provide extra information in the response.
        /// </summary>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public static string StatusCodeDescription(this string value, int statusCode)
        {
            string strReturn = string.Empty;

            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Status
            switch (statusCode)
            {
                // Information codes
                case 100:
                    strReturn = string.Format("Code {0}. Continue.", statusCode);
                    break;
                case 101:
                    strReturn = string.Format("Code {0}. Switching Protocol (Upgrade in request header).", statusCode);
                    break;
                case 102:
                    strReturn = string.Format("Code {0}. Processing.", statusCode);
                    break;

                // Sucess
                case 200:
                    strReturn = string.Format("Code {0}. OK.", statusCode);
                    break;
                case 201:
                    strReturn = string.Format("Code {0}. Created.", statusCode);
                    break;
                case 203:
                    strReturn = string.Format("Code {0}. Non-authoritative information (meta information does not match origin server).", statusCode);
                    break;
                case 204:
                    strReturn = string.Format("Code {0}. No content.", statusCode);
                    break;
                case 205:
                    strReturn = string.Format("Code {0}. Reset content.", statusCode);
                    break;
                case 206:
                    strReturn = string.Format("Code {0}. Partial Content.", statusCode);
                    break;
                case 207:
                    strReturn = string.Format("Code {0}. Multistatus (WebDAV).", statusCode);
                    break;
                case 208:
                    strReturn = string.Format("Code {0}. Multistatus (WebDAV - Property status).", statusCode);
                    break;
                case 226:
                    strReturn = string.Format("Code {0}. Delta encoding - partial update of existing client entity. IM Used.", statusCode);
                    break;

                // Redirections
                case 300:
                    strReturn = string.Format("Code {0}. Multi choice. There is more than one redirection possible.", statusCode);
                    break;
                case 301:
                    strReturn = string.Format("Code {0}. Page moved permanently.", statusCode);
                    break;
                case 302:
                    strReturn = string.Format("Code {0}. URI Not Found.", statusCode);
                    break;
                case 303:
                    strReturn = string.Format("Code {0}. See other (there is another URI in the GET request).", statusCode);
                    break;
                case 304:
                    strReturn = string.Format("Code {0}. Not modified.", statusCode);
                    break;
                case 305:
                    strReturn = string.Format("Code {0}. A proxy must be used (be suspicious).", statusCode);
                    break;
                case 306:
                    strReturn = string.Format("Code {0}. Unused (http 1.1 reserved code).", statusCode);
                    break;
                case 307:
                    strReturn = string.Format("Code {0}. Temporary Redirect.", statusCode);
                    break;
                case 308:
                    strReturn = string.Format("Code {0}. Permanent Redirect.", statusCode);
                    break;

                // Client error responses
                case 400:
                    strReturn = string.Format("Code {0}. Bad request. The server does not understand what it recieved. (Check the URL, form or cookie data).", statusCode);
                    break;
                case 401:
                    strReturn = string.Format("Code {0}. Unauthorized. You are not allowed to access this resource.", statusCode);
                    break;
                case 402:
                    strReturn = string.Format("Code {0}. Payment required.", statusCode);
                    break;
                case 403:
                    strReturn = string.Format("Code {0}. Forbidden. We know who you are. You are not allowed to access this resource.", statusCode);
                    break;
                case 404:
                    strReturn = string.Format("Code {0}. Page not found.", statusCode);
                    break;
                case 405:
                    strReturn = string.Format("Code {0}. Method Not Allowed.", statusCode);
                    break;
                case 406:
                    strReturn = string.Format("Code {0}. Not Acceptable. The server is unable to find any matching content.", statusCode);
                    break;
                case 407:
                    strReturn = string.Format("Code {0}. Proxy Authentication Required. You are not allowed to access this resource unless authorised via a proxy.", statusCode);
                    break;
                case 408:
                    strReturn = string.Format("Code {0}. Request Timeout.", statusCode);
                    break;
                case 409:
                    strReturn = string.Format("Code {0}. Conflict. The request conflicts with the server state.", statusCode);
                    break;
                case 410:
                    strReturn = string.Format("Code {0}. Gone. The resource has been moved or permanently deleted with no forwarding address.", statusCode);
                    break;
                case 411:
                    strReturn = string.Format("Code {0}. Length Required. The content-length header is missing.", statusCode);
                    break;
                case 412:
                    strReturn = string.Format("Code {0}. Precondition Failed. There are missing headers in the request.", statusCode);
                    break;
                case 413:
                    strReturn = string.Format("Code {0}. Payload Too Large. Request is too big for the server to handle.", statusCode);
                    break;
                case 414:
                    strReturn = string.Format("Code {0}. URI is too Long.", statusCode);
                    break;
                case 415:
                    strReturn = string.Format("Code {0}. Unsupported Media Type.", statusCode);
                    break;
                case 416:
                    strReturn = string.Format("Code {0}. Requested Range Not Satisfiable. The range header is incorrect (the data may be smaller than the range specified).", statusCode);
                    break;
                case 417:
                    strReturn = string.Format("Code {0}. Expectation Failed. The requests expect field cannot be met by the server.", statusCode);
                    break;
                case 418:
                    strReturn = string.Format("Code {0}. Insufficient drainage in the lower field.", statusCode);
                    break;
                case 421:
                    strReturn = string.Format("Code {0}. Misdirected Request.", statusCode);
                    break;
                case 422:
                    strReturn = string.Format("Code {0}. Unprocessable Entity (WebDAV).", statusCode);
                    break;
                case 423:
                    strReturn = string.Format("Code {0}. Resource is locked. (WebDAV).", statusCode);
                    break;
                case 424:
                    strReturn = string.Format("Code {0}. Failed Dependency. (WebDAV)", statusCode);
                    break;
                case 426:
                    strReturn = string.Format("Code {0}. Upgrade Required. A different protocol is required (check the upgrade header in the response).", statusCode);
                    break;
                case 428:
                    strReturn = string.Format("Code {0}. Precondition Required. The request needs to be conditional.", statusCode);
                    break;
                case 429:
                    strReturn = string.Format("Code {0}. Too Many Requests. Your request limit has been reached. Go and do something else.", statusCode);
                    break;
                case 431:
                    strReturn = string.Format("Code {0}. Request Header Fields Too Large.", statusCode);
                    break;
                case 452:
                    strReturn = string.Format("Code {0}. Unavailable For Legal Reasons. The content you are trying to access has been censored.", statusCode);
                    break;

                // Server responses
                case 500:
                    strReturn = string.Format("Code {0}. Internal Server Error.", statusCode);
                    break;
                case 501:
                    strReturn = string.Format("Code {0}. Method Not Implemented.", statusCode);
                    break;
                case 502:
                    strReturn = string.Format("Code {0}. Bad Gateway.", statusCode);
                    break;
                case 503:
                    strReturn = string.Format("Code {0}. Service Unavailable. The server is not ready to process your request.", statusCode);
                    break;
                case 504:
                    strReturn = string.Format("Code {0}. Gateway Timeout.", statusCode);
                    break;
                case 505:
                    strReturn = string.Format("Code {0}. HTTP Version Not Supported.", statusCode);
                    break;
                case 506:
                    strReturn = string.Format("Code {0}. Circular reference detected. Variant Also Negotiates.", statusCode);
                    break;
                case 507:
                    strReturn = string.Format("Code {0}. Internal configuration error. Content negotiation does not result in an end point. Insufficient Storage.", statusCode);
                    break;
                case 508:
                    strReturn = string.Format("Code {0}. Loop Detected (WebDAV). An infinite loop was detected while processing the request. Fire the developer.", statusCode);
                    break;
                case 510:
                    strReturn = string.Format("Code {0}. Not Extended. Request extensions are missing.", statusCode);
                    break;
                case 511:
                    strReturn = string.Format("Code {0}. Access Denied. Network Authentication Required.", statusCode);
                    break;

                default:
                    strReturn = string.Format("Unknown status code {0}.", statusCode);
                    break;
            }

            return strReturn;
        }    
    }
}
