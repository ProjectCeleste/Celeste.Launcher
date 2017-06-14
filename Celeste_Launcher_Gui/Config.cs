#region Using directives

using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;
using Celeste_User;

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

        public void Save(string path)
        {
            Helpers.SerializeToFile(this, path);
        }

        public static UserConfig Load(string path)
        {
            return Helpers.DeserializeFromFile<UserConfig>(path);
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
                    _uncryptedPassword = Helpers.Decrypt(CryptedPassword, true);
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
                    CryptedPassword = Helpers.Encrypt(value, true);
                }
                catch (Exception)
                {
                    //
                }

                _uncryptedPassword = value;
            }
        }
    }
}