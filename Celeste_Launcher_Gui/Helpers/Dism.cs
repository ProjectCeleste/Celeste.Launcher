#region Using directives

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
            var enumerable = featureNames as string[] ?? featureNames.ToArray();
            if (!enumerable.Any())
                throw new ArgumentException();

            DismApi.Initialize(DismLogLevel.LogErrors);

            var retVal = new List<KeyValuePair<string, DismFeatureInfo>>();
            try
            {
                using (var session = DismApi.OpenOnlineSession())
                {
                    retVal.AddRange(enumerable.Where(key => !string.IsNullOrEmpty(key))
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

        public static async Task EnableWindowsFeatures(string featureName,
            DismProgressCallback dismProgressCallback)
        {
            if (string.IsNullOrEmpty(featureName))
                throw new ArgumentException();

            DismApi.Initialize(DismLogLevel.LogErrors);

            try
            {
                using (var session = DismApi.OpenOnlineSession())
                {
                    DismApi.EnableFeatureByPackageName(session, featureName, null, false, true, new List<string>(),
                        dismProgressCallback);
                }
            }
            finally
            {
                DismApi.Shutdown();
            }

            await Task.Delay(200).ConfigureAwait(false);
        }
    }
}