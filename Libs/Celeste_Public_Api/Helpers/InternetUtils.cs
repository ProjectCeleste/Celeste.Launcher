﻿#region Using directives

using Celeste_Public_Api.Logging;
using Serilog;
using System;
using System.Net;
using System.Net.Security;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

#endregion

namespace Celeste_Public_Api.Helpers
{
    public static class InternetUtils
    {
        private static readonly ILogger Logger = LoggerFactory.GetLogger();

        public static void SslFix()
        {

            ServicePointManager.ServerCertificateValidationCallback += ValidateRemoteCertificate;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls |
                                                   SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        }
        /// <summary>
        ///     Certificate validation callback.
        /// </summary>
        private static bool ValidateRemoteCertificate(object sender, X509Certificate cert, X509Chain chain,
            SslPolicyErrors error)
        {
            // If the certificate is a valid, signed certificate, return true.
            if (error == SslPolicyErrors.None)
                return true;

//#if DEBUG
            if (error == SslPolicyErrors.RemoteCertificateNameMismatch || error == SslPolicyErrors.RemoteCertificateChainErrors)
                return true;
//#endif      

            Logger.Error($"X509Certificate [{cert.Subject}] Policy Error: {error}");

            return false;
        }

        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(out int description, int reservedValue);

        public static bool IsConnectedToInternet()
        {
            try
            {
                return InternetGetConnectedState(out int _, 0);
            }
            catch (Exception)
            {
                return true;
            }
        }
    }
}