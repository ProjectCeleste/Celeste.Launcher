#region Using directives

using System;
using System.ComponentModel;
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
    
    public enum ConnectionType
    {
        [XmlEnum("WAN")] Wan,
        [XmlEnum("LAN")] Lan,
        [XmlEnum("OTHER")] Other
    }
    
    public enum PortMappingType
    {
        [XmlEnum("NATPunch")] NatPunch,
        [XmlEnum("UPnP")] Upnp,
        [XmlEnum("Manual")] Manual
    }

    [XmlRoot(ElementName = "Celeste_Launcher_Gui_Config")]
    public class UserConfig
    {
#if DEBUG
        [DefaultValue("wss://127.0.0.1:4513/")]
        [XmlElement(ElementName = "ServerUri")]
        public string ServerUri { get; set; } = "wss://127.0.0.1:4513/";
#else
        [DefaultValue("wss://ns544971.ip-66-70-180.net:4513/")]
        [XmlElement(ElementName = "ServerUri")]
        public string ServerUri { get; set; } = "wss://ns544971.ip-66-70-180.net:4513/";
#endif

        [XmlElement(ElementName = "GameFilesPath")]
        public string GameFilesPath { get; set; } = string.Empty;

        [XmlIgnore]
        public bool IsSteamVersion { get; set; } = false;

        [XmlElement(ElementName = "LoginInfo")]
        public LoginInfo LoginInfo { get; set; } = new LoginInfo();

        [XmlElement(ElementName = "GameLanguage")]
        public GameLanguage GameLanguage { get; set; } = GameLanguage.enUS;

        [XmlElement(ElementName = "MpSettings")]
        public MpSettings MpSettings { get; set; } = new MpSettings();

        [DefaultValue(false)]
        [XmlElement(ElementName = "IsDiagnosticMode")]
        public bool IsDiagnosticMode { get; set; }

        public static UserConfig Load(string path)
        {
            var userConfig = XmlUtils.DeserializeFromFile<UserConfig>(path);

            if (userConfig.MpSettings.ConnectionType != ConnectionType.Lan)
                return userConfig;

            if (!string.IsNullOrWhiteSpace(userConfig.MpSettings.LanNetworkInterface))
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
            userConfig.MpSettings.ConnectionType = ConnectionType.Wan;

            return userConfig;
        }

        public void Save(string path)
        {
            this.SerializeToXmlFile(path);
        }
    }

    [XmlRoot(ElementName = "LoginInfo")]
    public class LoginInfo
    {
        [XmlIgnore] private string _cryptedPassword = string.Empty;

        [XmlIgnore] private string _uncryptedPassword = string.Empty;

        [DefaultValue(null)]
        [XmlElement(ElementName = "Email")]
        public string Email { get; set; }

        [DefaultValue(null)]
        [XmlElement(ElementName = "Password")]
        public string CryptedPassword
        {
            get => _cryptedPassword;
            set
            {
                _cryptedPassword = value;

                if (!string.IsNullOrWhiteSpace(_uncryptedPassword))
                    return;

                try
                {
                    _uncryptedPassword = string.IsNullOrWhiteSpace(value)
                        ? string.Empty
                        : EncryptDecrypt.Decrypt(value, true);
                }
                catch (Exception)
                {
                    //
                }
            }
        }

        [DefaultValue(false)]
        [XmlElement(ElementName = "RememberMe")]
        public bool RememberMe { get; set; }

        [DefaultValue(false)]
        [XmlElement(ElementName = "AutoLogin")]
        public bool AutoLogin { get; set; }

        [XmlIgnore]
        public string Password
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_uncryptedPassword))
                    return _uncryptedPassword;

                try
                {
                    _uncryptedPassword = string.IsNullOrWhiteSpace(CryptedPassword)
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
                if (string.IsNullOrWhiteSpace(value))
                    CryptedPassword = string.Empty;

                try
                {
                    CryptedPassword = string.IsNullOrWhiteSpace(value)
                        ? string.Empty
                        : EncryptDecrypt.Encrypt(value, true);
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
        [XmlIgnore] private string _publicIp;

        [DefaultValue(ConnectionType.Wan)]
        [XmlElement(ElementName = "ConnectionType")]
        public ConnectionType ConnectionType { get; set; } = ConnectionType.Wan;

        [DefaultValue(null)]
        [XmlElement(ElementName = "LanNetworkInterface")]
        public string LanNetworkInterface { get; set; }

        [DefaultValue(PortMappingType.NatPunch)]
        [XmlElement(ElementName = "PortMappingType")]
        public PortMappingType PortMappingType { get; set; } = PortMappingType.NatPunch;

        [XmlIgnore]
        public string PublicIp
        {
            get => string.IsNullOrWhiteSpace(_publicIp) ? "127.0.0.1" : _publicIp;
            set => _publicIp = value;
        }
    }
}