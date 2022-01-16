using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace odata_client
{
    public class HTTPClientHelper
    {
        HttpClient client;
       
        public HTTPClientHelper(string baseUrl)
        {
            this.client = new HttpClient();
            this.client.BaseAddress = new Uri(baseUrl);
        }

        public async Task<T> GetAsync<T>(string url)
        {
            T data;
            try
            {
                using (HttpResponseMessage response = await client.GetAsync(url))
                using (HttpContent content = response.Content)
                {
                    string d = await content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode && d != null)
                    {
                        data = JsonConvert.DeserializeObject<T>(d);
                        return (T)data;
                    }
                    else if (!response.IsSuccessStatusCode) {
                        Console.WriteLine(d);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            return default(T);
        }

        public async Task<T> PostAsync<T>(string url, HttpContent record)
        {
            T data;
            try
            {
                using (HttpResponseMessage response = await client.PostAsync(url, record))
                using (HttpContent content = response.Content)
                {
                    string d = await content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode && d != null)
                    {
                        data = JsonConvert.DeserializeObject<T>(d);
                        return (T)data;
                    }
                    else if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(d);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            return default(T);
        }

        public async Task<T> PutAsync<T>(string url, HttpContent record)
        {
            T data;
            try
            {
                using (HttpResponseMessage response = await client.PatchAsync(url, record))
                using (HttpContent content = response.Content)
                {
                    string d = await content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode && d != null)
                    {
                        data = JsonConvert.DeserializeObject<T>(d);
                        return (T)data;
                    }
                    else if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(d);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            return default(T);
        }

    }
}