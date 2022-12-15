using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestApi.Models;

namespace TestApi.Controllers
{
    public class SeriesController : Controller
    {
        static Uri BASE_URL = new Uri("https://api.themoviedb.org/3/");
        HttpClient client;
        SeriesResult result = new SeriesResult();
        Series series;

        public SeriesController()
        {
            client = new HttpClient();
            client.BaseAddress = BASE_URL;
        }

        public IActionResult Index()
        {
            result = getPopular();
            return View(result);
        }

        public IActionResult AiringToday()
        {
            result = getAiringToday();
            return View(result);
        }

        public IActionResult TopRated()
        {
            result = getTopRated();
            return View(result);
        }
        public IActionResult OnTheAir()
        {
            result = getOnTheAir();
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

        // To get series details  
        public IActionResult SeriesDetails(int id)
        {
            Series series = new Series();
            HttpResponseMessage response = client.GetAsync(BASE_URL + "tv/" + id + "?api_key=52a18783ed514602a5facb15a0177e61").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                series = JsonConvert.DeserializeObject<Series>(data);
                series.poster_path = "https://image.tmdb.org/t/p/original" + series.poster_path;
                series.backdrop_path = "https://image.tmdb.org/t/p/original" + series.backdrop_path;
            }
            return View(series);
        }

        // To get popular series
        private SeriesResult getPopular()
        {
            HttpResponseMessage response = client.GetAsync(BASE_URL + "tv/popular?api_key=52a18783ed514602a5facb15a0177e61").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<SeriesResult>(data);
            }
            return addBase(result);
        }


        // To get airing today series
        private SeriesResult getAiringToday()
        {
            HttpResponseMessage response = client.GetAsync(BASE_URL + "tv/airing_today?api_key=52a18783ed514602a5facb15a0177e61").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<SeriesResult>(data);
            }
            return addBase(result);
        }


        // To get top rated series
        private SeriesResult getTopRated()
        {
            HttpResponseMessage response = client.GetAsync(BASE_URL + "tv/top_rated?api_key=52a18783ed514602a5facb15a0177e61").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<SeriesResult>(data);
            }
            return addBase(result);
        }

        // To get on the air series
        private SeriesResult getOnTheAir()
        {
            HttpResponseMessage response = client.GetAsync(BASE_URL + "tv/on_the_air?api_key=52a18783ed514602a5facb15a0177e61").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<SeriesResult>(data);
            }
            return addBase(result);
        }

        // To get search for series
        private SeriesResult getSearch(string name)
        {
            HttpResponseMessage response = client.GetAsync(BASE_URL + "search/tv?api_key=52a18783ed514602a5facb15a0177e61&query=" + name).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<SeriesResult>(data);
            }
            return addBase(result);
        }

        // To add baseURL to poster and backdrop
        private SeriesResult addBase(SeriesResult result)
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
