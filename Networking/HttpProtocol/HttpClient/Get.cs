using System.Net;
using System.Net.Http.Json;

namespace Networking.HttpProtocol
{
    public partial class Base
    {
        public static async void SendAsync()
        {
            HttpClient httpClient = new HttpClient();

            // определяем данные запроса
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://google.com");

            // получаем ответ
            using HttpResponseMessage response = await httpClient.SendAsync(request);

            // просматриваем данные ответа
            // статус
            Console.WriteLine($"Status: {response.StatusCode}\n");
            //заголовки
            Console.WriteLine("Headers");
            foreach (var header in response.Headers)
            {
                Console.Write($"{header.Key}:");
                foreach (var headerValue in header.Value)
                {
                    Console.WriteLine(headerValue);
                }
            }
            // содержимое ответа
            Console.WriteLine("\nContent");
            // string content = await response.Content.ReadAsStringAsync();
            // Console.WriteLine(content);
        }
        public static async void SendGetAsync()
        {
            HttpClient httpClient = new HttpClient();

            // получаем ответ
            using HttpResponseMessage response = await httpClient.GetAsync("https://www.google.com");
            // получаем ответ
            string content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
        }
        public static async void SendGetAsyncString()
        {
            HttpClient httpClient = new HttpClient();

            string content = await httpClient.GetStringAsync("https://www.google.com");
            Console.WriteLine(content);
        }
        public static async void SendGetAsyncByte()
        {
            HttpClient httpClient = new HttpClient();

            byte[] buffer = await httpClient.GetByteArrayAsync("https://www.google.com");
            Console.WriteLine(System.Text.Encoding.UTF8.GetString(buffer));
        }
        public static async void SendGetAsyncStream()
        {
            HttpClient httpClient = new HttpClient();

            using Stream stream = await httpClient.GetStreamAsync("https://www.google.com");
            StreamReader reader = new StreamReader(stream);
            string content = await reader.ReadToEndAsync();     // считываем поток в строку
            Console.WriteLine(content);
        }
        record Person(string Name, int Age);
        record Error(string Message);
    }
}