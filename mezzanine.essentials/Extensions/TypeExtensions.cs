using mezzanine.Utility;
using System;

namespace mezzanine.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Create a JSON example for this type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string JSONExample(this Type type)
        {
            string result = string.Empty;

            using (JSONSerialiser js = new JSONSerialiser())
            {
                try
                {
                    // see if a default can be created
                    result = js.Serialize(Activator.CreateInstance(type, true));
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }                
            }

            return result;
        }

        /// <summary>
        /// Returns a short string (without the namespace) for the type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ToShortString(this Type type)
        {
            return type.ToString().Replace(type.Namespace, string.Empty).Replace(".", string.Empty);
        }
    }
}
