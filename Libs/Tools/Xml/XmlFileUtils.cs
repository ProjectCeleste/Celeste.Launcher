#region Using directives

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

#endregion

namespace ProjectCeleste.GameFiles.Tools.Xml
{
    public class CustomEncodingStringWriter : StringWriter
    {
        public CustomEncodingStringWriter(Encoding encoding)
        {
            Encoding = encoding;
        }

        public override Encoding Encoding { get; }
    }

    public static class XmlFileUtils
    {
        #region Misc

        public static string PrettyXml(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
                throw new ArgumentNullException(nameof(xml));

            string output;
            var xmlDoc = XDocument.Parse(xml);
            using (var stringWriter = new CustomEncodingStringWriter(Encoding.UTF8))
            {
                using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings
                {
                    Encoding = Encoding.UTF8,
                    Indent = true,
                    OmitXmlDeclaration = true,
                    NewLineHandling = NewLineHandling.None
                }))
                {
                    xmlDoc.Save(xmlWriter);
                }
                output = stringWriter.ToString();
            }
            return output;
        }

        #endregion

        #region Serialize

        public static void SerializeToXmlFile(this object serializableObject, string xmlFilePath,
            bool autoBackup = false, int backupMaxCount = 10)
        {
            if (serializableObject == null)
                throw new ArgumentNullException(nameof(serializableObject));

            if (string.IsNullOrWhiteSpace(xmlFilePath))
                throw new ArgumentNullException(nameof(xmlFilePath));

            var xml = SerializeToXml(serializableObject);
            var dir = Path.GetDirectoryName(xmlFilePath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            if (autoBackup && File.Exists(xmlFilePath))
            {
                var backupFile = $"{xmlFilePath}.{DateTime.UtcNow.ToFileTimeUtc():X8}.bak";

                if (File.Exists(backupFile))
                    File.Delete(backupFile);

                File.Move(xmlFilePath, backupFile);

                // Cleanup Backup
                Directory.GetFiles(dir, $"{xmlFilePath}.*.bak", SearchOption.TopDirectoryOnly)
                    .OrderByDescending(File.GetLastWriteTime)
                    .Skip(backupMaxCount)
                    .ToList()
                    .ForEach(File.Delete);
            }

            File.WriteAllText(xmlFilePath, xml, Encoding.UTF8);
        }

        public static string SerializeToXml(this object serializableObject)
        {
            if (serializableObject == null)
                throw new ArgumentNullException(nameof(serializableObject));

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
            using (var stringWriter = new CustomEncodingStringWriter(Encoding.UTF8))
            {
                using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
                {
                    serializer.Serialize(xmlWriter, serializableObject, ns);
                }

                output = stringWriter.ToString();
            }
            return output;
        }

        #endregion

        #region Deserialize

        public static T DeserializeFromXmlFile<T>(string xmlFilePath) where T : class
        {
            if (string.IsNullOrWhiteSpace(xmlFilePath))
                throw new ArgumentNullException(nameof(xmlFilePath));

            return !File.Exists(xmlFilePath)
                ? throw new FileNotFoundException("File Not Found", xmlFilePath)
                : DeserializeFromXml<T>(File.ReadAllText(xmlFilePath, Encoding.UTF8));
        }

        public static T DeserializeFromXml<T>(string xml) where T : class
        {
            if (string.IsNullOrWhiteSpace(xml))
                throw new ArgumentNullException(nameof(xml));

            T output;
            var xmls = new XmlSerializer(typeof(T));
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                output = (T) xmls.Deserialize(ms);
            }
            return output;
        }

        #endregion
    }
}