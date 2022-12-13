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
        public MovieController()
        {
            client= new HttpClient();
            client.BaseAddress = BASE_URL;
        }

        public IActionResult Index()
        {
            result = getData();
            return View(result);
        }

        public IActionResult MovieDetails(int id)
        {
            result = getData();
            foreach (var item in result.results)
            {
                if (item.id == id)
                {
                    return View(item);
                }
            }
            return View("Not Found");
        }

        private Result addBase(Result result)
        {
            foreach (var item in result.results)
            {
                item.poster_path = "https://image.tmdb.org/t/p/original" + item.poster_path;
                item.backdrop_path = "https://image.tmdb.org/t/p/original" + item.backdrop_path;
            }
            return result;
        }

        private Result getData()
        {
            HttpResponseMessage response = client.GetAsync(BASE_URL + "movie/popular?api_key=52a18783ed514602a5facb15a0177e61").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<Result>(data);
            }
            return addBase(result);
        }
    }
}
