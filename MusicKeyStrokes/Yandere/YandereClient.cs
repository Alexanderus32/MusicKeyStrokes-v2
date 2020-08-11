using MusicKeyStrokes.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MusicKeyStrokes.Yandere
{
    public class YandereClient
    {
        private string BaseUrl = "https://yande.re/post.json?";
        private string TagsUrl = "https://yande.re/tag.json?";

        public async Task<List<YandereImage>> GetImagesAsync(YandereTag Tag)
        {
            string URL = $"{BaseUrl}{Tag.GenerateTags()}";
            return await GetJsonData<YandereImage>(URL);
        }

        private async Task<List<T>> GetJsonData<T>(string URL) where T : new()
        {
            HttpClient Client = new HttpClient();
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

        public async Task<List<YandereTags>> GetTags(YandereTag Tag)
        {
            string URL = $"{TagsUrl}{Tag.SearchTags()}";
            return await GetJsonData<YandereTags>(URL);
        }
    }
}
