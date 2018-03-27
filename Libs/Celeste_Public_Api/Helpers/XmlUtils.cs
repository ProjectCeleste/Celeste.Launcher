#region Using directives

using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

#endregion

namespace Celeste_Public_Api.Helpers
{
    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }

    public static class XmlUtils
    {
        public static string ToXml(this object serializableObject)
        {
            return SerializeToString(serializableObject);
        }

        public static void SerializeToXmlFile(this object serializableObject, string xmlFilePath, bool backup = true)
        {
            var xml = SerializeToString(serializableObject);

            if (File.Exists(xmlFilePath))
            {
                if (backup)
                {
                    var backupFile = $"{xmlFilePath}.bak";

                    if (File.Exists(backupFile))
                        File.Delete(backupFile);

                    File.Move(xmlFilePath, backupFile);
                }

                File.Delete(xmlFilePath);
            }

            File.WriteAllText(xmlFilePath, xml, Encoding.UTF8);
        }

        public static string SerializeToString(object serializableObject)
        {
            if (serializableObject == null)
                return null;

            string output;
            var serializer = new XmlSerializer(serializableObject.GetType());
            var settings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                Indent = true,
                OmitXmlDeclaration = true,
                NewLineHandling = NewLineHandling.None
            };
            var ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);
            using (var stringWriter = new Utf8StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
                {
                    serializer.Serialize(xmlWriter, serializableObject, ns);
                }

                output = stringWriter.ToString();
            }
            return output;
        }

        public static T DeserializeFromFile<T>(string xmlFilePath) where T : class
        {
            return !File.Exists(xmlFilePath)
                ? null
                : DeserializeFromString<T>(File.ReadAllText(xmlFilePath, Encoding.UTF8));
        }

        public static T DeserializeFromString<T>(string xml) where T : class
        {
            if (string.IsNullOrEmpty(xml))
                return null;

            T output;
            var xmls = new XmlSerializer(typeof(T));
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                output = (T) xmls.Deserialize(ms);
            }
            return output;
        }

        public static string PrettyXml(string xml)
        {
            string output;
            var xmlDoc = XDocument.Parse(xml);
            using (var stringWriter = new Utf8StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings
                {
                    Encoding = Encoding.UTF8,
                    Indent = true,
                    OmitXmlDeclaration = false,
                    NewLineHandling = NewLineHandling.None
                }))
                {
                    xmlDoc.Save(xmlWriter);
                }
                output = stringWriter.ToString();
            }
            return output;
        }
    }
}