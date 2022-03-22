using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace app
{
    public class Requestmaker
    {
        private static HttpClient client;
        public Requestmaker()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:4444/prime/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

            static void ShowResponse(string placeholder)
            {
                Console.WriteLine(placeholder);
            }

        static async Task<string> CountPrimeAsync(Object ob)
            {
            JsonContent content = JsonContent.Create(ob);
            HttpResponseMessage response = await client.PostAsync(
                    "", content); 
                response.EnsureSuccessStatusCode();
                string placeholder = null;
                if (response.IsSuccessStatusCode)
            {
                placeholder = await response.Content.ReadAsStringAsync();
            }
            return placeholder;
            }

            static async Task<string> GetPrimeAsync(Uri path)
            {
                string placeholder = null;
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    placeholder = await response.Content.ReadAsStringAsync();
            }
                return placeholder;
            }

            public static async Task RunCheckIfPrime(int id)
            {
                try
                {
                // Get the prime
                    string placeholder = await GetPrimeAsync(new Uri(client.BaseAddress.ToString() + id));
                    ShowResponse(placeholder);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

        public static async Task RunCountPrime(int to, int from)
        {
            try
            {
                // Get the product
                string placeholder = await CountPrimeAsync(new{ before = to, after = from});
                ShowResponse(placeholder);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
