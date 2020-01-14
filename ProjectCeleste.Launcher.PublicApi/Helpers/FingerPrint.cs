﻿#region Using directives

using System;
using System.Collections.Generic;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

#endregion Using directives

namespace ProjectCeleste.Launcher.PublicApi.Helpers
{
    public static class FingerPrintProvider
    {
        private static Task<string> FingerPrintTask;

        public static void Initialize()
        {
            FingerPrintTask = Task.Factory.StartNew(FingerPrint.GenerateValue);
        }

        public static async Task<string> GetFingerprintAsync()
        {
            if (FingerPrintTask == null)
            {
                Initialize();
            }

            return await FingerPrintTask;
        }
    }

    /// <summary>
    ///     Generates a 16 byte Unique Identification code of a computer
    ///     Example: 4876-8DB5-EE85-69D3-FE52-8CF7-395D-2EA9
    ///     https://www.codeproject.com/Articles/28678/Generating-Unique-Key-Finger-Print-for-a-Computer
    /// </summary>
    public static class FingerPrint
    {
        private static string _fingerPrint = string.Empty;

        public static string GenerateValue()
        {
            if (string.IsNullOrEmpty(_fingerPrint))
            {
                _fingerPrint = GetHash("CPU >> " + CpuId() +
                                       "\nBIOS >> " + BiosId() +
                                       "\nBASE >> " + BaseId() +
                                       //"\nDISK >> "+ diskId() +
                                       "\nVIDEO >> " + VideoId() +
                                       "\nMAC >> " + MacId()
                );
            }

            return _fingerPrint;
        }

        private static string GetHash(string s)
        {
            MD5 sec = new MD5CryptoServiceProvider();
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] bt = enc.GetBytes(s);
            return GetHexString(sec.ComputeHash(bt));
        }

        private static string GetHexString(IReadOnlyList<byte> bt)
        {
            string s = string.Empty;
            for (int i = 0; i < bt.Count; i++)
            {
                int n = bt[i];
                int n1 = n & 15;
                int n2 = (n >> 4) & 15;
                if (n2 > 9)
                    s += ((char)(n2 - 10 + 'A')).ToString();
                else
                    s += n2.ToString();
                if (n1 > 9)
                    s += ((char)(n1 - 10 + 'A')).ToString();
                else
                    s += n1.ToString();
                if (i + 1 != bt.Count && (i + 1) % 2 == 0) s += "-";
            }
            return s;
        }

        #region Original Device ID Getting Code

        //Return a hardware identifier
        // ReSharper disable once InconsistentNaming
        private static string Identifier
            (string wmiClass, string wmiProperty, string wmiMustBeTrue)
        {
            string result = "";
            ManagementClass mc =
                new ManagementClass(wmiClass);
            foreach (ManagementBaseObject o in mc.GetInstances())
            {
                ManagementObject mo = (ManagementObject)o;
                if (mo[wmiMustBeTrue].ToString() != "True") continue;
                if (result != "") continue;
                try
                {
                    result = mo[wmiProperty].ToString();
                    break;
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            return result;
        }

        //Return a hardware identifier
        // ReSharper disable once InconsistentNaming
        private static string Identifier(string wmiClass, string wmiProperty)
        {
            string result = "";
            ManagementClass mc =
                new ManagementClass(wmiClass);
            foreach (ManagementBaseObject o in mc.GetInstances())
            //Only get the first one
            {
                ManagementObject mo = (ManagementObject)o;
                if (result != "") continue;
                try
                {
                    result = mo[wmiProperty].ToString();
                    break;
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            return result;
        }

        private static string CpuId()
        {
            //Uses first CPU identifier available in order of preference
            //Don't get all identifiers, as it is very time consuming
            string retVal = Identifier("Win32_Processor", "UniqueId");

            if (retVal != "") return retVal;

            retVal = Identifier("Win32_Processor", "ProcessorId");

            if (retVal != "") return retVal;

            retVal = Identifier("Win32_Processor", "Name");
            if (retVal == "") //If no Name, use Manufacturer
                retVal = Identifier("Win32_Processor", "Manufacturer");
            //Add clock speed for extra security
            retVal += Identifier("Win32_Processor", "MaxClockSpeed");
            return retVal;
        }

        //BIOS Identifier
        private static string BiosId()
        {
            return Identifier("Win32_BIOS", "Manufacturer")
                   + Identifier("Win32_BIOS", "SMBIOSBIOSVersion")
                   + Identifier("Win32_BIOS", "IdentificationCode")
                   + Identifier("Win32_BIOS", "SerialNumber")
                   + Identifier("Win32_BIOS", "ReleaseDate")
                   + Identifier("Win32_BIOS", "Version");
        }

        //Main physical hard drive ID
        //private static string DiskId()
        //{
        //    return identifier("Win32_DiskDrive", "Model")
        //           + identifier("Win32_DiskDrive", "Manufacturer")
        //           + identifier("Win32_DiskDrive", "Signature")
        //           + identifier("Win32_DiskDrive", "TotalHeads");
        //}

        //Motherboard ID
        private static string BaseId()
        {
            return Identifier("Win32_BaseBoard", "Model")
                   + Identifier("Win32_BaseBoard", "Manufacturer")
                   + Identifier("Win32_BaseBoard", "Name")
                   + Identifier("Win32_BaseBoard", "SerialNumber");
        }

        //Primary video controller ID
        private static string VideoId()
        {
            return Identifier("Win32_VideoController", "DriverVersion")
                   + Identifier("Win32_VideoController", "Name");
        }

        //First enabled network card ID
        private static string MacId()
        {
            return Identifier("Win32_NetworkAdapterConfiguration",
                "MACAddress", "IPEnabled");
        }

        #endregion Original Device ID Getting Code
    }
}