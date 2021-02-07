using System;
using System.IO;

namespace Celeste_Launcher_Gui.Helpers
{
    public class FileSystem
    {
        public static bool IsWritableDirectory(string path)
        {
            try
            {
                var testTmpFileLocation = Path.Combine(path, "test.tmp");
                File.WriteAllText(testTmpFileLocation, "_");
                File.Delete(testTmpFileLocation);
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }
    }
}
