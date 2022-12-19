using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Xml.Linq;
using TestApi.Models;

namespace TestApi.Controllers
{
    public class MovieController : Controller
    {
        static Uri BASE_URL = new Uri("https://api.themoviedb.org/3/");
        HttpClient client;
        Movie movie;
        Result result;
        public MovieController()
        {
            client= new HttpClient();
            client.BaseAddress = BASE_URL;
            movie = new Movie();
            result = new Result();
        }

        public IActionResult Index()
        {           
            return View(getMovies("movie/popular?api_key=52a18783ed514602a5facb15a0177e61"));
        }

        public IActionResult NowPlaying()
        {
            
            return View(getMovies("movie/now_playing?api_key=52a18783ed514602a5facb15a0177e61"));
        }

        public IActionResult TopRated()
        {
            
            return View(getMovies("movie/top_rated?api_key=52a18783ed514602a5facb15a0177e61"));
        }

        public IActionResult Upcoming()
        {
            
            return View(getMovies("movie/upcoming?api_key=52a18783ed514602a5facb15a0177e61"));
        }

        public IActionResult Filter(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                return View("Index", getMovies("search/movie?api_key=52a18783ed514602a5facb15a0177e61&query=" + searchString));
            }
            return View("Index", getMovies("movie/popular?api_key=52a18783ed514602a5facb15a0177e61"));
        }




        // To get movie details 
        public IActionResult MovieDetails(int id)
        {
            HttpResponseMessage response = client.GetAsync(BASE_URL + "movie/"+id+"?api_key=52a18783ed514602a5facb15a0177e61").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                movie = JsonConvert.DeserializeObject<Movie>(data);
                movie.poster_path= "https://image.tmdb.org/t/p/original" + movie.poster_path;
                movie.backdrop_path= "https://image.tmdb.org/t/p/original" + movie.backdrop_path;
            }
            return View(movie);
        }

        // To get movies
        private Result getMovies(string moviePath)
        {
            HttpResponseMessage response = client.GetAsync(BASE_URL +moviePath ).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<Result>(data);
            }
            return addBase(result);
        }

        // To add baseURL to poster and backdrop
        private Result addBase(Result result)
        {
            foreach (var item in result.results)
            {
                item.poster_path = "https://image.tmdb.org/t/p/original" + item.poster_path;
                item.backdrop_path = "https://image.tmdb.org/t/p/original" + item.backdrop_path;
            }
            return result;
        }
    }
}
