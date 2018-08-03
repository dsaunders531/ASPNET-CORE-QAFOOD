using mezzanine.Extensions;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace mezzanine.Utility
{
    /// <summary>
    /// Base serializer class.
    /// </summary>
    public abstract class SerializerBase : IDisposable
    {
        public void Dispose()
        {
            // There are no large objects to dispose but I want to use the serialisers in a using statement ...
        }

        /// <summary>
        /// You will need the object name for object convertion.
        /// </summary>
        /// <param name="typeToConvert"></param>
        /// <returns></returns>
        public string ObjectNameForXml(Type typeToConvert)
        {
            string result = typeToConvert.ToString();

            if (result.Contains("[") == true && result.Contains("]") == true)
            {
                // get the underlying object name
                int startPos = result.IndexOf("[") + 1;
                int len = result.IndexOf("]") - startPos;

                if (len > 0)
                {
                    result = result.Substring(startPos, len);
                }
                else
                {
                    result = result.Replace("[", string.Empty);
                    result = result.Replace("]", string.Empty);
                }

                result += "s";
            }

            if (result.Contains(".") == true)
            {
                result = result.Split(".").Last();
            }

            return result;
        }

        public string XmlToJSON(XmlElement xml)
        {
            return JsonConvert.SerializeXmlNode(xml);
        }

        public string XmlToJSON(XmlDocument xmlDoc)
        {
            return XmlToJSON(xmlDoc.DocumentElement);
        }
    }

    /// <summary>
    /// Perform serialization operations using XML data.
    /// </summary>
    public class XMLSerializer : SerializerBase
    {
        public T Deserialize<T>(XmlDocument xmlDocument)
        {
            return this.Deserialize<T>(xmlDocument, new XmlRootAttribute(base.ObjectNameForXml(typeof(T))));
        }

        public T Deserialize<T>(XmlDocument xmlDocument, string rootNodeName)
        {
            return this.Deserialize<T>(xmlDocument, new XmlRootAttribute(rootNodeName));
        }

        public T Deserialize<T>(XmlDocument xmlDocument, XmlRootAttribute rootAttr)
        {
            T result = default(T);

            using (MemoryStream ms = new MemoryStream())
            {
                xmlDocument.Save(ms);
                ms.Flush(); // write all
                ms.Seek(0, SeekOrigin.Begin); // move to start

                XmlSerializer xs = new XmlSerializer(typeof(T), rootAttr);

                result = (T)xs.Deserialize(ms);

                xs = null;
            }

            return result;
        }

        public T Deserialize<T>(string XMLFilePath)
        {
            XmlDocument x = new XmlDocument();
            x.Load(XMLFilePath);
            return this.Deserialize<T>(x);
        }

        /// <summary>
        /// Converts any object to XML and returns the XML element. No need to manually type the convertion of an object to XML!
        /// </summary>
        /// <returns></returns>
        public XmlElement Serialize(Type type, object o, XmlRootAttribute rootAttr)
        {
            MemoryStream ms = new MemoryStream();
            XmlSerializer xs = new XmlSerializer(type, rootAttr);
            XmlDocument outDoc = new XmlDocument();
            XmlElement returnElement = null;

            xs.Serialize(ms, o);
            ms.Flush(); // write all
            ms.Seek(0, SeekOrigin.Begin); // move to start

            outDoc.Load(ms);

            // Create the element with extra attributes.
            returnElement = outDoc.CreateElement(outDoc.DocumentElement.Name);
            // Add attibutes to say this is a converted thing
            XmlAttribute xa = outDoc.CreateAttribute("xmlns:xsi");
            xa.Value = "http://www.w3.org/2001/XMLSchema-instance";
            returnElement.Attributes.Append(xa);

            xa = outDoc.CreateAttribute("xmlns:xsd");
            xa.Value = "http://www.w3.org/2001/XMLSchema";
            returnElement.Attributes.Append(xa);

            returnElement.InnerXml = outDoc.DocumentElement.InnerXml;

            ms.Dispose();
            ms = null;
            xs = null;
            outDoc = null;

            return returnElement;
        }

        public XmlElement Serialize(Type type, object o, string rootNodeName)
        {
            return Serialize(type, o, new XmlRootAttribute(rootNodeName));
        }

        public XmlElement Serialize(Type type, object o)
        {
            return Serialize(type, o, new XmlRootAttribute(base.ObjectNameForXml(type)));
        }

    }
    
    /// <summary>
    /// Perform serialization operations using JSON data.
    /// </summary>
    public class JSONSerialiser : SerializerBase
    {
        public T Deserialize<T>(string data)
        {
            T result = default(T);

            // In case of problems when json is read from a file strip any line feeds.
            data = data.Minify();

            result = JsonConvert.DeserializeObject<T>(data);

            return result;
        }

        /// <summary>
        /// Reads a local file (ie: one on the accessible filesystem of the machine) into a JSON object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="JsonFilePath"></param>
        /// <returns></returns>
        public T Deserialize<T>(Uri JsonFilePath)
        {
            T result = default(T);

            using (StreamReader sr = File.OpenText(JsonFilePath.LocalPath))
            {
                JsonSerializer jsonSerializer = new JsonSerializer(); // Note there are lots of settings for this. String formats, culture etc.
                result = (T)jsonSerializer.Deserialize(sr, typeof(T));
                jsonSerializer = null;
            }

            return result;
        }

        public string Serialize(object o)
        {
            return JsonConvert.SerializeObject(o);
        }
    }
}
