#region Using directives

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Xml.Serialization;
using Celeste_Launcher_Gui.Helpers;
using Celeste_Public_Api.Helpers;

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
        [XmlElement(ElementName = "GameFilesPath")]
        public string GameFilesPath { get; set; } = string.Empty;

        [XmlElement(ElementName = "BetaUpdate")]
        public bool BetaUpdate { get; set; }

        [XmlElement(ElementName = "IsSteam")]
        public bool IsSteam { get; set; }

        [XmlElement(ElementName = "LoginInfo")]
        public LoginInfo LoginInfo { get; set; } = new LoginInfo();

        [XmlElement(ElementName = "GameLanguage")]
        public GameLanguage GameLanguage { get; set; } = GameLanguage.enUS;

        [XmlElement(ElementName = "MpSettings")]
        public MpSettings MpSettings { get; set; } = new MpSettings();

        public void Save(string path)
        {
            XmlUtils.SerializeToFile(this, path);
        }

        public static UserConfig Load(string path)
        {
            var userConfig = XmlUtils.DeserializeFromFile<UserConfig>(path);

            if (userConfig.MpSettings.IsOnline)
                return userConfig;

            if (!string.IsNullOrEmpty(userConfig.MpSettings.LanNetworkInterface))
            {
                var selectedNetInt = userConfig.MpSettings.LanNetworkInterface;
                var netInterface = NetworkInterface.GetAllNetworkInterfaces()
                    .FirstOrDefault(elem => elem.Name == selectedNetInt);

                if (netInterface == null)
                    goto notfound;

                // Get IPv4 address:
                foreach (var ip in netInterface.GetIPProperties().UnicastAddresses)
                    if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        userConfig.MpSettings.PublicIp = ip.Address.ToString();
                        return userConfig;
                    }
            }

            notfound:
            //Fall back to wan
            userConfig.MpSettings.IsOnline = true;

            return userConfig;
        }
    }

    [XmlRoot(ElementName = "LoginInfo")]
    public class LoginInfo
    {
        private string _cryptedPassword = string.Empty;
        private string _uncryptedPassword = string.Empty;

        [XmlElement(ElementName = "Email")]
        public string Email { get; set; }

        [XmlElement(ElementName = "Password")]
        public string CryptedPassword
        {
            get => _cryptedPassword;
            set
            {
                _cryptedPassword = value;

                if (!string.IsNullOrEmpty(_uncryptedPassword))
                    return;

                try
                {
                    _uncryptedPassword = string.IsNullOrEmpty(value)
                        ? string.Empty
                        : EncryptDecrypt.Decrypt(value, true);
                }
                catch (Exception)
                {
                    //
                }
            }
        }

        [XmlElement(ElementName = "RememberMe")]
        public bool RememberMe { get; set; }

        [XmlElement(ElementName = "AutoLogin")]
        public bool AutoLogin { get; set; }

        [XmlIgnore]
        public string Password
        {
            get
            {
                if (!string.IsNullOrEmpty(_uncryptedPassword))
                    return _uncryptedPassword;

                try
                {
                    _uncryptedPassword = string.IsNullOrEmpty(CryptedPassword)
                        ? string.Empty
                        : EncryptDecrypt.Decrypt(CryptedPassword, true);
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
                    CryptedPassword = string.Empty;

                try
                {
                    CryptedPassword = string.IsNullOrEmpty(value) ? string.Empty : EncryptDecrypt.Encrypt(value, true);
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
        private bool _autoPortMapping;
        private string _publicIp;

        //private int _publicPort;

        [XmlElement(ElementName = "isOnline")]
        public bool IsOnline { get; set; } = true;

        [XmlElement(ElementName = "LanNetworkInterface")]
        public string LanNetworkInterface { get; set; }

        [XmlElement(ElementName = "isAutoPortMapping")]
        public bool AutoPortMapping
        {
            get => IsOnline && _autoPortMapping;
            set => _autoPortMapping = value;
        }

        [XmlIgnore]
        public string PublicIp
        {
            get => string.IsNullOrEmpty(_publicIp) ? "127.0.0.1" : _publicIp;
            set => _publicIp = value;
        }

        //        var rnd = new Random(DateTime.UtcNow.Millisecond);
        //        if (_publicPort != 0) return _publicPort;
        //    {

        //{
        //public int PublicPort

        //[XmlElement(ElementName = "PublicPort")]
        //        _publicPort = rnd.Next(1001, ushort.MaxValue);

        //        return _publicPort;
        //    }
        //    set
        //    {
        //        if (value != 0)
        //        {
        //            _publicPort = value;
        //            return;
        //        }

        //        var rnd = new Random(DateTime.UtcNow.Millisecond);
        //        _publicPort = rnd.Next(1001, ushort.MaxValue);
        //    }
        //}
    }
}