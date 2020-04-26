#region Using directives

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

#endregion

namespace ProjectCeleste.GameFiles.Tools.Xmb
{
    public static class XmbFileUtils
    {
        public static void XmbToXml(string inputFileName, string outputFileName)
        {
            using (var fileStream = File.Open(inputFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var result = XmbToXml(fileStream);
                File.WriteAllText(outputFileName, result, Encoding.UTF8);
            }
        }

        public static byte[] XmbToXml(byte[] data)
        {
            using (var fileStream = new MemoryStream(data, false))
            {
                var result = XmbToXml(fileStream);
                return Encoding.UTF8.GetBytes(result);
            }
        }
        
        private static string XmbToXml(Stream stream)
        {
            using (var reader = new BinaryReader(stream))
            {
                var head = new string(reader.ReadChars(2));
                if (head != "X1")
                    throw new Exception("Invalid XMB (head does not equal X1)");
                reader.ReadInt32();
                reader.ReadChars(2);
                reader.ReadInt32();
                reader.ReadInt32();
                //
                var elementcount = reader.ReadInt32();
                var elementNames = new List<string>();
                for (var index = 0; index < elementcount; ++index)
                {
                    var strLength = reader.ReadUInt32();
                    var str = Encoding.Unicode.GetString(reader.ReadBytes((int) strLength * 2));

                    elementNames.Add(str);
                }
                //
                var attrcount = reader.ReadInt32();
                var attrNames = new List<string>();
                for (var index = 0; index < attrcount; ++index)
                {
                    var strLength = reader.ReadUInt32();
                    var str = Encoding.Unicode.GetString(reader.ReadBytes((int) strLength * 2));

                    attrNames.Add(str);
                }
                //
                var xmbElement = XmbElement.DeserializeXmbElement(reader, elementNames, attrNames);

                var xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" + xmbElement.ToXml();

                return xml;
            }
        }
    }
}