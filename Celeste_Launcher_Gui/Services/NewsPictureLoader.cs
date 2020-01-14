using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Celeste_Launcher_Gui.Services
{
    internal class NewsPictureLoader
    {
        private const string NewsDescriptionUri = "https://static.projectceleste.com/launcher/news.json";

        public async Task<NewsPicture> GetNewsDescription()
        {
            using (HttpClient client = new HttpClient())
            {
                string response = await client.GetStringAsync(NewsDescriptionUri);
                return JsonConvert.DeserializeObject<NewsPicture>(response);
            }
        }
    }

    public class NewsPicture
    {
        public string ImageSource { get; set; }
        public string Href { get; set; }

        public static NewsPicture Default()
        {
            return new NewsPicture
            {
                ImageSource = "pack://application:,,,/Celeste Launcher;component/Resources/DefaultNewsGraphics.png",
                Href = "https://forums.projectceleste.com/"
            };
        }
    }
}
