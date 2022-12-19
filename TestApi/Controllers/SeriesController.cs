using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Xml.Linq;
using TestApi.Models;

namespace TestApi.Controllers
{
    public class SeriesController : Controller
    {
        static Uri BASE_URL = new Uri("https://api.themoviedb.org/3/");
        HttpClient client;
        SeriesResult result;
        Series series;

        public SeriesController()
        {
            client = new HttpClient();
            client.BaseAddress = BASE_URL;
            series = new Series();
            result = new SeriesResult();
        }

        public IActionResult Index()
        {
            
            return View(getSeries("tv/popular?api_key=52a18783ed514602a5facb15a0177e61"));
        }

        public IActionResult AiringToday()
        {
            return View(getSeries("tv/airing_today?api_key=52a18783ed514602a5facb15a0177e61"));
        }

        public IActionResult TopRated()
        {
            return View(getSeries("tv/top_rated?api_key=52a18783ed514602a5facb15a0177e61"));
        }

        public IActionResult OnTheAir()
        {
            
            return View(getSeries("tv/on_the_air?api_key=52a18783ed514602a5facb15a0177e61"));
        }

        public IActionResult Filter(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                return View("Index", getSeries("search/tv?api_key=52a18783ed514602a5facb15a0177e61&query=" + searchString));
            }
            return View("Index", getSeries("tv/popular?api_key=52a18783ed514602a5facb15a0177e61"));
        }



        // To get series details  
        public IActionResult SeriesDetails(int id)
        {
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

        // To get Series
        private SeriesResult getSeries(string seriesPath)
        {
            HttpResponseMessage response = client.GetAsync(BASE_URL + seriesPath).Result;
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
