using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicKeyStrokes.Yandere
{
    public class YandereTag
    {
        public List<string> Tags;

        public YandereRating Rating;

        public int Limit = 80;

        public int Page = 1;

        public YandereTag()
        {
            Tags = new List<string>();
            Rating = YandereRating.all;
        }

        public readonly IList<string> _blacklistedTags = new List<string>
        {
            "lolicon",
            "shotacon",
            "futanari"
        };

        public string GenerateTags()
        {
            string TagString = Tags.Aggregate((i, b) => $"{i}+{b}");
            if (Rating != YandereRating.all)
            {
                switch (Rating)
                {
                    case YandereRating.Safe:
                        TagString = $"rating:s+{TagString}";
                        break;
                    case YandereRating.Questionable:
                        TagString = $"rating:q+{TagString}";
                        break;
                    case YandereRating.Explicit:
                        TagString = $"rating:e+{TagString}";
                        break;
                }
            }
            return $"tags={TagString}&limit={Limit}&page={Page}";
        }

        public string SearchTags()
        {
            return $"name={Tags[0]}&order=count&commit=Search";
        }
    }
}
