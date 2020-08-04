using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MusicKeyStrokes.CommandsTelegram
{
    class CommandTelegramS : ACommandTelegram
    {
        public override string CommandName => "+";

        private string BaseUrl = "https://yande.re/post.json?";

        HttpClient Client = new HttpClient();

        List<Image> Images = null;

        public override async void Execute(Message message, TelegramBotClient client)
        {
            //string URL = $"{BaseUrl}tags=Explicit&limit=60&page=1";
            // string URL = $"{BaseUrl}tags=rating:saber&limit=60&page=1";
            string URL = $"{BaseUrl}tags=saber+fate/stay_night&limit=60&page=1";
            Images =  await GetJsonData<Image>(URL);
            Random rand = new Random();
            int rnd = rand.Next(Images.Count);
            await client.SendPhotoAsync(message.Chat.Id,Images[rnd].SampleUrl);
        }

        private async Task<List<T>> GetJsonData<T>(string URL) where T : new()
        {
           // HttpClient Client = new HttpClient();
            string jsonData = string.Empty;
            try
            {
                jsonData = await Client.GetStringAsync(URL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return !string.IsNullOrEmpty(jsonData) ? JsonConvert.DeserializeObject<List<T>>(jsonData) : new List<T>();
        }
    }
}
