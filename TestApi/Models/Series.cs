using System.ComponentModel;

namespace TestApi.Models
{
    public class Series
    {
        [DisplayName("Id")]
        public int id { get; set; }

        [DisplayName("Title")]
        public string name { get; set; }

        [DisplayName("Poster Path")]
        public string poster_path { get; set; }

        [DisplayName("Backdrop Path")]
        public string backdrop_path { get; set; }

        [DisplayName("Release Date")]
        public string first_air_date { get; set; }

        [DisplayName("Overview")]
        public string overview { get; set; }

        [DisplayName("Vote Average")]
        public float vote_average { get; set; }
    }
}
