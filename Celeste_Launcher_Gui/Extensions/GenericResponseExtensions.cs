using Celeste_Launcher_Gui.Properties;
using Celeste_Public_Api.WebSocket_Api.WebSocket.CommandInfo;

namespace Celeste_Launcher_Gui.Extensions
{
    public static class GenericResponseExtensions
    {
        public static string GetLocalizedMessage(this IGenericResponse response)
        {
            var culture = Resources.Culture;
            var stringKey = "ServerResponse_" + response.Message;

            var localizedString = Resources.ResourceManager.GetString(stringKey, culture);

            if (string.IsNullOrWhiteSpace(localizedString))
            {
                return string.Format(Resources.UnknownServerResponse, response.Message);
            }

            return localizedString;
        }
    }
}
