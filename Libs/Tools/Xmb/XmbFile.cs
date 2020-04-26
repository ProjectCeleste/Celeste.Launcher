#region Using directives

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

#endregion

namespace ProjectCeleste.GameFiles.Tools.Xmb
{
    public class XmbAttribute
    {
        public XmbAttribute(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }

        public string Value { get; }
    }

    public class XmbElement
    {
        public XmbElement(int unk0, string head, string name, int lineNum, string text,
            IReadOnlyList<XmbElement> childs, IReadOnlyList<XmbAttribute> attrs)
        {
            Unk0 = unk0;
            Head = head;
            Name = name;
            LineNum = lineNum;
            Text = text;
            Childs = childs;
            Attrs = attrs;
        }

        public int Unk0 { get; }

        public string Head { get; }

        public string Name { get; }

        public int LineNum { get; }

        public string Text { get; }

        public IReadOnlyList<XmbElement> Childs { get; }

        public IReadOnlyList<XmbAttribute> Attrs { get; }

        public static XmbElement DeserializeXmbElement(BinaryReader reader, IReadOnlyList<string> elementNames,
            IReadOnlyList<string> attrNames)
        {
            var head = new string(reader.ReadChars(2));
            if (head != "XN")
                throw new Exception("Invalid Node (head does not equal XN)");

            var unk0 = reader.ReadInt32();
            var txtLength = reader.ReadUInt32();
            var text = Encoding.Unicode.GetString(reader.ReadBytes((int) txtLength * 2));
            var name = elementNames[reader.ReadInt32()];
            var lineNum = reader.ReadInt32();
            var attrs = new List<XmbAttribute>();
            var num1 = reader.ReadInt32();
            for (var index1 = 0; num1 > index1; ++index1)
            {
                var index2 = reader.ReadInt32();
                var valueLength = reader.ReadUInt32();
                var value = Encoding.Unicode.GetString(reader.ReadBytes((int) valueLength * 2));
                var xmbAttribute = new XmbAttribute(attrNames[index2], value);
                attrs.Add(xmbAttribute);
            }
            var childs = new List<XmbElement>();
            var num2 = reader.ReadInt32();
            for (var index = 0; num2 > index; ++index)
            {
                var node = DeserializeXmbElement(reader, elementNames, attrNames);
                childs.Add(node);
            }

            return new XmbElement(unk0, head, name, lineNum, text, childs, attrs);
        }

        public string ToXml(string indent = "")
        {
            var str1 = "\r\n" + indent + "<" + Name;
            foreach (var t in Attrs)
            {
                var name = t.Name;
                var value = t.Value.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;")
                    .Replace("\"", "&quot;").Replace("'", "&apos;");
                str1 = str1 + " " + name + "=\"" + value + "\"";
            }
            string str2;
            if (Text.Length > 0)
            {
                var text = Text.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;")
                    .Replace("\"", "&quot;").Replace("'", "&apos;");

                if (Childs.Count > 0)
                {
                    var str3 = str1 + ">" + "\r\n" + indent + "\t" + text;
                    var indent1 = indent + "\t";
                    str3 = Childs.Aggregate(str3, (current, t) => current + t.ToXml(indent1));
                    str2 = str3 + "\r\n" + indent + "</" + Name + ">";
                }
                else
                {
                    str2 = str1 + ">" + text + "</" + Name + ">";
                }
            }
            else if (Childs.Count > 0)
            {
                var str3 = str1 + ">";
                var indent1 = indent + "\t";
                str3 = Childs.Aggregate(str3, (current, t) => current + t.ToXml(indent1));
                str2 = str3 + "\r\n" + indent + "</" + Name + ">";
            }
            else
            {
                str2 = str1 + "/>";
            }
            return str2;
        }
    }
}