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
        //string url = "http://localhost:4444/api/primes/";

            static HttpClient client = new HttpClient();

            static void ShowResponse(string placeholder)
            {
                Console.WriteLine(placeholder);
            }

        static async Task<string> CreateProductAsync(Object ob)
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

            static async Task<string> GetProductAsync(Uri path)
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
                // Update port # in the following line.
                client.BaseAddress = new Uri("http://localhost:4444/prime/" + id);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                // Get the product
                string placeholder = await GetProductAsync(client.BaseAddress);
                    ShowResponse(placeholder);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                Console.ReadLine();
            }

        public static async Task RunCountPrime(int to, int from)
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://localhost:4444/prime/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                // Get the product
                string placeholder = await CreateProductAsync(new{ before = to, after = from});
                ShowResponse(placeholder);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ReadLine();
        }
    }
}
