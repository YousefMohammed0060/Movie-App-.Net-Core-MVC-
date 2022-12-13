using Newtonsoft.Json;
using System.ComponentModel;

namespace TestApi.Models
{
    public class Movie
    {
        [DisplayName("Id")]
        //[JsonProperty("id")]
        public int id { get; set; }

        [DisplayName("Title")]
       // [JsonProperty("title")]
        public string title { get; set; }

        [DisplayName("Poster Path")]
        //[JsonProperty("poster_path")]
        public string poster_path { get; set; }

        [DisplayName("Backdrop Path")]
        //[JsonProperty("backdrop_path")]
        public string backdrop_path { get; set; }

        [DisplayName("Release Date")]
        //[JsonProperty("release_date")]
        public string release_date { get; set; }

        [DisplayName("Overview")]
        //[JsonProperty("overview")]
        public string overview { get; set; }

        [DisplayName("Vote Average")]
        //[JsonProperty("vote_average")]
        public float vote_average { get; set; }
    }
}
