using Microsoft.AspNetCore.Mvc;
using Movies_and_Series.Data;
using Newtonsoft.Json;
using System.Xml.Linq;
using TestApi.Models;

namespace TestApi.Controllers
{
    public class SeriesController : Controller
    {
        Series series;
        GetData<Result<Series>,Series> data;

        public SeriesController()
        {
            series = new Series();
            data = new GetData<Result<Series>, Series>();
        }

        public IActionResult Index()
        {
            
            return View(addBase(data.apiData("tv/popular?api_key=52a18783ed514602a5facb15a0177e61")));
        }

        public IActionResult AiringToday()
        {
            return View(addBase(data.apiData("tv/airing_today?api_key=52a18783ed514602a5facb15a0177e61")));
        }

        public IActionResult TopRated()
        {
            return View(addBase(data.apiData("tv/top_rated?api_key=52a18783ed514602a5facb15a0177e61")));
        }

        public IActionResult OnTheAir()
        {
            
            return View(addBase(data.apiData("tv/on_the_air?api_key=52a18783ed514602a5facb15a0177e61")));
        }

        public IActionResult Filter(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                return View("Index", addBase(data.apiData("search/tv?api_key=52a18783ed514602a5facb15a0177e61&query=" + searchString)));
            }
            return View("Index", addBase(data.apiData("tv/popular?api_key=52a18783ed514602a5facb15a0177e61")));
        }



        // To get series details  
        public IActionResult SeriesDetails(int id)
        {
            series = data.getSingle("tv/" + id + "?api_key=52a18783ed514602a5facb15a0177e61");
            series.poster_path = "https://image.tmdb.org/t/p/original" + series.poster_path;
            series.backdrop_path = "https://image.tmdb.org/t/p/original" + series.backdrop_path;
            return View(series);
        }


        // To add baseURL to poster and backdrop
        private Result<Series> addBase(Result<Series> result)
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
