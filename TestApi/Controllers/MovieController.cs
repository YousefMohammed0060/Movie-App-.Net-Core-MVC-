using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using TestApi.Models;

namespace TestApi.Controllers
{
    public class MovieController : Controller
    {
        static Uri BASE_URL = new Uri("https://api.themoviedb.org/3/");
        HttpClient client;
        Result result = new Result();
        Movie movie;
        public MovieController()
        {
            client= new HttpClient();
            client.BaseAddress = BASE_URL;
        }

        public IActionResult Index()
        {
            result = getPopular();
            return View(result);
        }

        public IActionResult NowPlaying()
        {
            result = getNowPlaying();
            return View(result);
        }

        public IActionResult TopRated()
        {
            result = getTopRated();
            return View(result);
        }

        public IActionResult Upcoming()
        {
            result = getUpcoming();
            return View(result);
        }

        public IActionResult Filter(string searchString)
        {
            result = getPopular();
            if (!string.IsNullOrEmpty(searchString))
            {
                return View("Index", getSearch(searchString));
            }
            return View("Index", result);
        }

        // To get movie details 
        public IActionResult MovieDetails(int id)
        {
            Movie movie = new Movie();
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



        
        // To get popular movies
        private Result getPopular()
        {
            HttpResponseMessage response = client.GetAsync(BASE_URL + "movie/popular?api_key=52a18783ed514602a5facb15a0177e61").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<Result>(data);
            }
            return addBase(result);
        }


        // To get now playing movies
        private Result getNowPlaying()
        {
            HttpResponseMessage response = client.GetAsync(BASE_URL + "movie/now_playing?api_key=52a18783ed514602a5facb15a0177e61").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<Result>(data);
            }
            return addBase(result);
        }

        // To get top rated movies
        private Result getTopRated()
        {
            HttpResponseMessage response = client.GetAsync(BASE_URL + "movie/top_rated?api_key=52a18783ed514602a5facb15a0177e61").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<Result>(data);
            }
            return addBase(result);
        }
        
        // To get upcoming movies
        private Result getUpcoming()
        {
            HttpResponseMessage response = client.GetAsync(BASE_URL + "movie/upcoming?api_key=52a18783ed514602a5facb15a0177e61").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<Result>(data);
            }
            return addBase(result);
        }

        // To get upcoming movies
        private Result getSearch(string name)
        {
            HttpResponseMessage response = client.GetAsync(BASE_URL + "search/movie?api_key=52a18783ed514602a5facb15a0177e61&query=" + name).Result;
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
