using Newtonsoft.Json;
using TestApi.Models;

namespace Movies_and_Series.Data
{
    public class GetData<T,Y>
    {

        static Uri BASE_URL = new Uri("https://api.themoviedb.org/3/");
        HttpClient client;
        T result;
        Y movie;

        public GetData()
        {
            client = new HttpClient();
            client.BaseAddress = BASE_URL;
        }


        // To get list of Data
        public T apiData(string moviePath)
        {
            HttpResponseMessage response = client.GetAsync(BASE_URL + moviePath).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<T>(data);
            }
            return result;
        }

        // To get Single Data
        public Y getSingle(string path)
        {
            HttpResponseMessage response = client.GetAsync(BASE_URL + path).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                movie = JsonConvert.DeserializeObject<Y>(data);
            }
            return movie;
        }

    }
}
