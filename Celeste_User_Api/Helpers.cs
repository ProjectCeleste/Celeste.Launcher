#region Using directives

using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

#endregion

namespace Celeste_User
{
    public class Helpers
    {
        private const string MatchEmailPattern =
            @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
            + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
            + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
            + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$";

        private const string MatchUserNamePattern =
            @"^[A-Za-z0-9]{3,16}$";

        private static readonly Random Random = new Random();

        public static bool IsValideEmailAdress(string emailAdress)
        {
            return !string.IsNullOrEmpty(emailAdress) && Regex.IsMatch(emailAdress, MatchEmailPattern);
        }

        public static bool IsValideUserName(string userName)
        {
            return !string.IsNullOrEmpty(userName) && Regex.IsMatch(userName, MatchUserNamePattern);
        }

        public static string RandomString(int length)
        {
            const string chars = @"ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789$%-_#@\/&*-+=!?.";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }

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
    }
}