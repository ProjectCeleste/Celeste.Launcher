#region Using directives

using System.IO.Compression;
using System.Threading.Tasks;

#endregion

namespace Celeste_Launcher_Gui.Helpers
{
    public static class ZipUtils
    {
        public static async Task ExtractZipFile(string archiveFileName, string outFolder)
        {
            await Task.Run(() =>
            {
                ZipFile.ExtractToDirectory(archiveFileName, outFolder);
            }).ConfigureAwait(false);
        }
    }
}