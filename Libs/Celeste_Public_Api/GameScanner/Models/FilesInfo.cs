#region Using directives

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Serialization;
using Celeste_Public_Api.Helpers;

#endregion

namespace Celeste_Public_Api.GameScanner.Models
{
    [XmlRoot(ElementName = "FilesInfo")]
    public class FilesInfo
    {
        [XmlIgnore]
        private Dictionary<string, FileInfo> FileInfo { get; set; } = new Dictionary<string, FileInfo>();

        [XmlElement(ElementName = "FilesInfo")]
        public FileInfo[] FilesInfoArray
        {
            get
            {
                var list = new List<FileInfo>();
                if (FileInfo != null)
                    list.AddRange(FileInfo.Keys.Select(key => FileInfo[key]));

                return list.ToArray();
            }
            set
            {
                FileInfo = new Dictionary<string, FileInfo>();
                if (value == null) return;
                foreach (var item in value)
                    FileInfo.Add(item.FileName, item);
            }
        }

        public static FilesInfo GetGameFiles()
        {
            var retVal = new FilesInfo();

            //Load default manifest
            foreach (var fileInfo in FileInfoFromGameManifest("production", 6148))
                if (retVal.FileInfo.ContainsKey(fileInfo.FileName))
                    retVal.FileInfo[fileInfo.FileName] = fileInfo;
                else
                    retVal.FileInfo.Add(fileInfo.FileName, fileInfo);

            //Override for celeste file
            //foreach (var fileInfo in FileInfoOverrideFromCelesteXml())
            //{
            //    if (retVal.GameFile.ContainsKey(fileInfo.FileName))
            //        retVal.GameFile[fileInfo.FileName] = fileInfo;
            //    else
            //        retVal.GameFile.Add(fileInfo.FileName, fileInfo);
            //}

            return retVal;
        }

        private static IEnumerable<FileInfo> FileInfoFromGameManifest(string type, int build)
        {
            var tempFileName = Path.GetTempFileName();

            using (var client = new WebClient())
            {
                client.DownloadFile($"http://spartan.msgamestudios.com/content/spartan/{type}/{build}/manifest.txt",
                    tempFileName);
            }

            var retVal = from line in File.ReadAllLines(tempFileName)
                where line.StartsWith("+")
                where !line.StartsWith("+AoeOnlineDlg.dll") && !line.StartsWith("+AoeOnlinePatch.dll") &&
                      !line.StartsWith("+expapply.dll") && !line.StartsWith("+LauncherLocList.txt") &&
                      !line.StartsWith("+LauncherStrings-de-DE.xml") &&
                      !line.StartsWith("+LauncherStrings-en-US.xml") &&
                      !line.StartsWith("+LauncherStrings-es-ES.xml") &&
                      !line.StartsWith("+LauncherStrings-fr-FR.xml") &&
                      !line.StartsWith("+LauncherStrings-it-IT.xml") &&
                      !line.StartsWith("+LauncherStrings-zh-CHT.xml") && !line.StartsWith("+AOEOnline.exe.cfg") &&
                      !line.StartsWith("+steam_api.dll") && !line.StartsWith("+t3656t4234.tmp")
                select line.Split('|')
                into lineSplit
                select new FileInfo
                {
                    FileName = lineSplit[0].Substring(1, lineSplit[0].Length - 1),
                    Crc32 = Convert.ToUInt32(lineSplit[1]),
                    Size = Convert.ToInt64(lineSplit[2]),
                    HttpLink = $"http://spartan.msgamestudios.com/content/spartan/{type}/{build}/{lineSplit[3]}",
                    BinCrc32 = Convert.ToUInt32(lineSplit[4]),
                    BinSize = Convert.ToInt64(lineSplit[5])
                };

            if (File.Exists(tempFileName))
                File.Delete(tempFileName);

            return retVal;
        }

        private static IEnumerable<FileInfo> FileInfoOverrideFromCelesteXml(bool isBetaUpdate)
        {
            var tempFileName = Path.GetTempFileName();

            using (var client = new WebClient())
            {
                client.DownloadFile(
                    isBetaUpdate
                        ? "https://projectceleste.com/static/celeste_gamefile/manifest_override_b.xml"
                        : "https://projectceleste.com/static/celeste_gamefile/manifest_override.xml",
                    tempFileName);
            }

            // TODO

            var retVal = new List<FileInfo>();

            if (File.Exists(tempFileName))
                File.Delete(tempFileName);

            return retVal;
        }

        public void ToXml(string filename)
        {
            XmlUtils.SerializeToFile(this, filename);
        }
    }
}