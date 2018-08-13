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
    }
}
