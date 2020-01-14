#region Using directives

using System.Text.RegularExpressions;

#endregion Using directives

namespace ProjectCeleste.Launcher.PublicApi.Helpers
{
    public static class IsValidString
    {
        private const string MatchEmailPattern =
            @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$";

        private const string MatchUserNamePattern =
            "^[A-Za-z0-9]{3,15}$";

        private const string MatchUserNameClanPattern =
            "^[A-Za-z0-9_]{3,15}$";

        public static bool IsValidEmailAddress(string emailAddress)
        {
            return !string.IsNullOrWhiteSpace(emailAddress) && Regex.IsMatch(emailAddress, MatchEmailPattern);
        }

        public static bool IsValidUserName(string userName, bool allowClanTag = false)
        {
            return !string.IsNullOrWhiteSpace(userName) && Regex.IsMatch(userName,
                       allowClanTag ? MatchUserNameClanPattern : MatchUserNamePattern);
        }

        public static bool IsValidUserNameLength(string userName)
        {
            return !string.IsNullOrWhiteSpace(userName) && !(userName.Length < 3 || userName.Length > 15);
        }

        public static bool IsValidPassword(string password)
        {
            return !string.IsNullOrWhiteSpace(password) && !password.Contains("'") && !password.Contains("\"");
        }

        public static bool IsValidPasswordLength(string password)
        {
            return !string.IsNullOrWhiteSpace(password) && !(password.Length < 8 || password.Length > 32);
        }

        public static bool IsValidVerifyKey(string verifyKey)
        {
            return !string.IsNullOrWhiteSpace(verifyKey) && verifyKey.Length == 32;
        }
    }
}