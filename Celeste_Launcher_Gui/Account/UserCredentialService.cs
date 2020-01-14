using CredentialManagement;
using System.Security;

namespace Celeste_Launcher_Gui.Account
{
    internal static class UserCredentialService
    {
        private const string CelesteLauncherVaultName = "Project Celeste";

        internal static void StoreCredential(string email, SecureString password)
        {
            Credential credentials = new Credential
            {
                Target = CelesteLauncherVaultName,
                Username = email,
                SecurePassword = password,
                PersistanceType = PersistanceType.LocalComputer,
            };

            credentials.Save();
        }

        internal static UserCredentials GetStoredUserCredentials()
        {
            Credential credentials = new Credential { Target = CelesteLauncherVaultName };

            if (!credentials.Load())
            {
                return null;
            }

            return new UserCredentials
            {
                Email = credentials.Username,
                Password = credentials.SecurePassword
            };
        }

        internal static void ClearVault()
        {
            Credential credentials = new Credential { Target = CelesteLauncherVaultName };
            credentials.Delete();
        }
    }

    internal class UserCredentials
    {
        public string Email { get; set; }
        public SecureString Password { get; set; }
    }
}
