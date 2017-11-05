#region Using directives

using Celeste_Public_Api.GameFileInfo;

#endregion

namespace Celeste_Public_Api
{
    public static class Api
    {
        public static GameFiles GameFiles = GameFiles.GetGameFiles();
    }
}