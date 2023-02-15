using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;

namespace Activity5
{
    class Joke
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("setup")]
        public string Setup { get; set; }

        [JsonProperty("punchline")]
        public string Punchline { get; set; }
    }

    class Program
    {
        // For sending HTTP requests and receiving HTTP responses from a resource identified by a URI
        private static readonly HttpClient client = new HttpClient();


        static async Task Main(string[] args)
        {
            await ProcessRepositories();
        }

        private static async Task ProcessRepositories()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter any character for a random joke. Press enter without a character to quit the program.");
                    var requestJoke = Console.ReadLine();

                    if (string.IsNullOrEmpty(requestJoke))
                    {
                        break;
                    }

                    var result = await client.GetAsync("https://official-joke-api.appspot.com/random_joke");
                    var resultRead = await result.Content.ReadAsStringAsync();

                    var joke = JsonConvert.DeserializeObject<Joke>(resultRead);

                    Console.WriteLine("---");
                    Console.WriteLine("Id # " + joke.Id);
                    Console.WriteLine("Type: " + joke.Type);
                    Console.WriteLine("Setup: " + joke.Setup);
                    Console.WriteLine("Punchline: " + joke.Punchline);
                    Console.WriteLine("\n---");
                }
                catch (Exception)
                {
                    Console.WriteLine("ERROR. Please enter a valid input!");
                }
            }
        
        }
    }

}