using System;
using System.Reflection;

namespace QAFood.Areas.api
{
    /// <summary>
    /// Static class for converting objects into other objects
    /// </summary>
    public class Transposition : IDisposable
    {
        public void Dispose()
        {
            // do nothing. this is needed so this class can be used in a using statement;
        }

        /// <summary>
        /// Take the public properties from the input object and set the value on matching properties on the output object.
        /// </summary>
        /// <typeparam name="InputT">The type of input object</typeparam>
        /// <typeparam name="OutputT">The type of output object</typeparam>
        /// <param name="input">An instance of the input object with values you want to set on the output object.</param>
        /// <param name="output">The object which has some matching fields of the input object.</param>
        /// <returns></returns>
        public OutputT Transpose<InputT, OutputT>(InputT input, OutputT output)
        {
            OutputT result = output;

            InputT sourceType = (InputT)input;
            PropertyInfo[] sourceProperties = sourceType.GetType().GetProperties();

            for (int i = 0; i < sourceProperties.Length; i++)
            {
                object value = sourceProperties[i].GetValue(input);
                result.GetType().GetProperty(sourceProperties[i].Name)?.SetValue(result, value);
            }

            return result;
        }
    }
}
