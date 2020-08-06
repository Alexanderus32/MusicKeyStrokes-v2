using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MusicKeyStrokes.Models
{
    class Image
    {
        [JsonProperty("id")]
        public int ID;

        [JsonProperty("created_at")]
        public long CreatedAt;

        [JsonProperty("author")]
        public string Author;

        [JsonProperty("file_size")]
        public long FileSize;

        [JsonProperty("file_ext")]
        public string FileExtension;

        [JsonProperty("file_url")]
        public string ImageUrl;

        [JsonProperty("rating")]
        public char Rating;

        [JsonProperty("width")]
        public int Width;

        [JsonProperty("height")]
        public int Height;

        [JsonProperty("sample_url")]
        public string SampleUrl;

        [JsonProperty("sample_width")]
        public int SampleWidth;

        [JsonProperty("sample_height")]
        public int SampleHeight;

        [JsonProperty("sample_file_size")]
        public long SampleFileSize;

        [JsonProperty("tags")]
        public string Tags;
    }
}
