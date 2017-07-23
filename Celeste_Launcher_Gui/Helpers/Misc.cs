#region Using directives

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

#endregion

namespace Celeste_Launcher_Gui.Helpers
{
    public class Misc
    {
        public static string Encrypt(string toEncrypt, bool useHashing = false)
        {
            //https://www.codeproject.com/Articles/14150/Encrypt-and-Decrypt-Data-with-C

            byte[] keyArray;
            var toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);

            var key = FingerPrint.Value();

            if (useHashing)
            {
                var hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));

                hashmd5.Clear();
            }
            else
            {
                keyArray = Encoding.UTF8.GetBytes(key);
            }

            var tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            var cTransform = tdes.CreateEncryptor();

            var resultArray =
                cTransform.TransformFinalBlock(toEncryptArray, 0,
                    toEncryptArray.Length);

            tdes.Clear();

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string cipherString, bool useHashing = false)
        {
            //https://www.codeproject.com/Articles/14150/Encrypt-and-Decrypt-Data-with-C
            try
            {
                byte[] keyArray;

                var toEncryptArray = Convert.FromBase64String(cipherString);

                var key = FingerPrint.Value();

                if (useHashing)
                {
                    var hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));

                    hashmd5.Clear();
                }
                else
                {
                    keyArray = Encoding.UTF8.GetBytes(key);
                }

                var tdes = new TripleDESCryptoServiceProvider
                {
                    Key = keyArray,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };

                var cTransform = tdes.CreateDecryptor();
                var resultArray = cTransform.TransformFinalBlock(
                    toEncryptArray, 0, toEncryptArray.Length);
                tdes.Clear();
                return Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static void SerializeToFile(object serializableObject, string xmlFilePath, bool backup = true)
        {
            var xml = SerializeToString(serializableObject);
            if (File.Exists(xmlFilePath))
            {
                if (backup)
                    File.Copy(xmlFilePath, $"{xmlFilePath}.bak", true);
                File.Delete(xmlFilePath);
            }
            File.WriteAllText(xmlFilePath, xml, Encoding.UTF8);
        }

        public static string SerializeToString(object serializableObject)
        {
            var serializer = new XmlSerializer(serializableObject.GetType());
            var settings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                Indent = true,
                OmitXmlDeclaration = true,
                NewLineHandling = NewLineHandling.None
            };
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            using (var stringWriter = new Utf8StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
                {
                    serializer.Serialize(xmlWriter, serializableObject, ns);
                }

                return stringWriter.ToString();
            }
        }

        public static T DeserializeFromFile<T>(string xmlFilePath) where T : class
        {
            return DeserializeFromString<T>(File.ReadAllText(xmlFilePath, Encoding.UTF8));
        }

        public static T DeserializeFromString<T>(string xml) where T : class
        {
            if (string.IsNullOrEmpty(xml))
                return null;

            var xmls = new XmlSerializer(typeof(T));
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                return (T) xmls.Deserialize(ms);
            }
        }

        internal class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;
        }
    }
}