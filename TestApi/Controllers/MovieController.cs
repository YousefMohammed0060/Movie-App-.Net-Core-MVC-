using Microsoft.AspNetCore.Mvc;
using Movies_and_Series.Data;
using Newtonsoft.Json;
using System.Data;
using System.Xml.Linq;
using TestApi.Models;

namespace TestApi.Controllers
{
    public class MovieController : Controller
    {
        
        Movie movie;
        GetData<Result,Movie> data;
        public MovieController()
        {
            movie = new Movie();
            data = new GetData<Result, Movie>();
        }

        public IActionResult Index()
        {           
            return View(addBase(data.apiData("movie/popular?api_key=52a18783ed514602a5facb15a0177e61")));
        }

        public IActionResult NowPlaying()
        {
            
            return View(addBase(data.apiData("movie/now_playing?api_key=52a18783ed514602a5facb15a0177e61")));
        }

        public IActionResult TopRated()
        {
            
            return View(addBase(data.apiData("movie/top_rated?api_key=52a18783ed514602a5facb15a0177e61")));
        }

        public IActionResult Upcoming()
        {
            
            return View(addBase(data.apiData("movie/upcoming?api_key=52a18783ed514602a5facb15a0177e61")));
        }

        public IActionResult Filter(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                return View("Index", addBase(data.apiData("search/movie?api_key=52a18783ed514602a5facb15a0177e61&query=" + searchString)));
            }
            return View("Index", addBase(data.apiData("movie/popular?api_key=52a18783ed514602a5facb15a0177e61")));
        }

        // To get movie details 
        public IActionResult MovieDetails(int id)
        {
            movie=data.getSingle("movie/" + id + "?api_key=52a18783ed514602a5facb15a0177e61");
            movie.poster_path = "https://image.tmdb.org/t/p/original" + movie.poster_path;
            movie.backdrop_path = "https://image.tmdb.org/t/p/original" + movie.backdrop_path;
            return View(movie);
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
