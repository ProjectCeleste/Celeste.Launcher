using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Celeste_Launcher_Gui.Services
{
    class NewsPictureLoader
    {
        private const string NewsDescriptionUri = "https://i.shikashi.me/5EX8X";

        public async Task<NewsPicture> GetNewsDescription()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(NewsDescriptionUri);
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
                ImageSource = "pack://application:,,,/Celeste_Launcher_Gui;component/Resources/DefaultNewsGraphics.png",
                Href = "https://forums.projectceleste.com/"
            };
        }
    }
}
