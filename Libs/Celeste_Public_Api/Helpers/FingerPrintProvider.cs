using System.Threading.Tasks;

namespace Celeste_Public_Api.Helpers
{
    public static class FingerPrintProvider
    {
        private static Task<string> FingerPrintTask;

        public static void Initialize()
        {
            FingerPrintTask = Task.Factory.StartNew(() => FingerPrint.GenerateValue());
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
}
