﻿#region Using directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Dism;

#endregion

namespace Celeste_Launcher_Gui.Helpers
{
    public static class Dism
    {
        public static async Task<IEnumerable<KeyValuePair<string, DismFeatureInfo>>> GetWindowsFeatureInfo(
            IEnumerable<string> featureNames)
        {
            string[] enumerable = featureNames as string[] ?? featureNames.ToArray();
            if (enumerable.Length == 0)
                throw new ArgumentException();

            DismApi.Initialize(DismLogLevel.LogErrors);

            List<KeyValuePair<string, DismFeatureInfo>> retVal = new List<KeyValuePair<string, DismFeatureInfo>>();
            try
            {
                using (DismSession session = DismApi.OpenOnlineSession())
                {
                    retVal.AddRange(enumerable.Where(key => !string.IsNullOrWhiteSpace(key))
                        .Select(featureName => new KeyValuePair<string, DismFeatureInfo>(featureName,
                            DismApi.GetFeatureInfo(session, featureName))));
                }
            }
            finally
            {
                DismApi.Shutdown();
            }

            await Task.Delay(200).ConfigureAwait(false);

            return retVal;
        }

        public static async Task<DismFeatureInfo> GetWindowsFeatureInfo(string featureName)
        {
            DismApi.Initialize(DismLogLevel.LogErrors);

            DismFeatureInfo retVal;
            try
            {
                using (DismSession session = DismApi.OpenOnlineSession())
                {
                    retVal = DismApi.GetFeatureInfo(session, featureName);
                }
            }
            finally
            {
                DismApi.Shutdown();
            }

            await Task.Delay(200).ConfigureAwait(false);

            return retVal;
        }

        public static async Task<DismFeatureInfo> EnableWindowsFeatures(string featureName,
            DismProgressCallback dismProgressCallback)
        {
            if (string.IsNullOrWhiteSpace(featureName))
                throw new ArgumentException();

            DismApi.Initialize(DismLogLevel.LogErrors);

            DismFeatureInfo retVal;
            try
            {
                using (DismSession session = DismApi.OpenOnlineSession())
                {
                    DismApi.EnableFeatureByPackageName(session, featureName, null, false, true, new List<string>(),
                        dismProgressCallback);

                    retVal = DismApi.GetFeatureInfo(session, featureName);
                }
            }
            finally
            {
                DismApi.Shutdown();
            }

            await Task.Delay(200).ConfigureAwait(false);

            return retVal;
        }
    }
}