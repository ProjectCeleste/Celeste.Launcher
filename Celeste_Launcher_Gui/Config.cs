#region Using directives

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Xml.Serialization;

#endregion

namespace Celeste_Launcher_Gui
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum GameLanguage
    {
        [XmlEnum("de-DE")] deDE,
        [XmlEnum("en-US")] enUS,
        [XmlEnum("es-ES")] esES,
        [XmlEnum("fr-FR")] frFR,
        [XmlEnum("it-IT")] itIT,
        [XmlEnum("zh-CHT")] zhCHT
    }

    [XmlRoot(ElementName = "Celeste_Launcher_Gui_Config")]
    public class UserConfig
    {
        [XmlElement(ElementName = "LoginInfo")]
        public LoginInfo LoginInfo { get; set; } = new LoginInfo();

        [XmlElement(ElementName = "GameLanguage")]
        public GameLanguage GameLanguage { get; set; } = GameLanguage.enUS;

        [XmlElement(ElementName = "MpSettings")]
        public MpSettings MpSettings { get; set; } = new MpSettings();

        public void Save(string path)
        {
            Celeste_User.Helpers.SerializeToFile(this, path);
        }

        public static UserConfig Load(string path)
        {
            var userConfig = Celeste_User.Helpers.DeserializeFromFile<UserConfig>(path);

            if (userConfig.MpSettings.IsOnline) return userConfig;

            var firstOrDefault = Dns.GetHostEntry(Dns.GetHostName()).AddressList
                .FirstOrDefault(key => key.AddressFamily == AddressFamily.InterNetwork);

            userConfig.MpSettings.PublicIp = firstOrDefault?.ToString() ?? @"127.0.0.1";

            return userConfig;
        }
    }

    [XmlRoot(ElementName = "LoginInfo")]
    public class LoginInfo
    {
        private string _uncryptedPassword;

        [XmlElement(ElementName = "Email")]
        public string Email { get; set; }

        [XmlElement(ElementName = "Password")]
        public string CryptedPassword { get; set; } = "";

        [XmlElement(ElementName = "RememberMe")]
        public bool RememberMe { get; set; }

        [XmlIgnore]
        public string Password
        {
            get
            {
                if (string.IsNullOrEmpty(CryptedPassword))
                    _uncryptedPassword = "";

                try
                {
                    _uncryptedPassword = Celeste_User.Helpers.Decrypt(CryptedPassword, true);
                }
                catch (Exception)
                {
                    //
                }

                return _uncryptedPassword;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    CryptedPassword = "";

                try
                {
                    CryptedPassword = Celeste_User.Helpers.Encrypt(value, true);
                }
                catch (Exception)
                {
                    //
                }

                _uncryptedPassword = value;
            }
        }
    }

    [XmlRoot(ElementName = "MpSettings")]
    public class MpSettings
    {
        private int _publicPort;

        [XmlElement(ElementName = "isOnline")]
        public bool IsOnline { get; set; } = true;

        [XmlElement(ElementName = "AutoPortMapping")]
        public bool AutoPortMapping { get; set; } = true;

        [XmlIgnore]
        public string PublicIp { get; set; } = "127.0.0.1";

        [XmlElement(ElementName = "PublicPort")]
        public int PublicPort
        {
            get
            {
                if (_publicPort != 0) return _publicPort;

                var rnd = new Random(DateTime.UtcNow.Millisecond);
                _publicPort = rnd.Next(1001, ushort.MaxValue);

                return _publicPort;
            }
            set
            {
                if (value != 0)
                {
                    _publicPort = value;
                    return;
                }

                var rnd = new Random(DateTime.UtcNow.Millisecond);
                _publicPort = rnd.Next(1001, ushort.MaxValue);
            }
        }
    }
}