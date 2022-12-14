using Newtonsoft.Json;
using System.ComponentModel;

namespace TestApi.Models
{
    public class Movie
    {
        [DisplayName("Id")]
        public int id { get; set; }

        [DisplayName("Title")]
        public string title { get; set; }

        [DisplayName("Poster Path")]
        public string poster_path { get; set; }

        [DisplayName("Backdrop Path")]
        public string backdrop_path { get; set; }

        [DisplayName("Release Date")]
        public string release_date { get; set; }

        [DisplayName("Overview")]
        public string overview { get; set; }

        [DisplayName("Vote Average")]
        public float vote_average { get; set; }
    }
}
