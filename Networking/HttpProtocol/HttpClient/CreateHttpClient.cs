using Microsoft.Extensions.DependencyInjection;

namespace Networking.HttpProtocol
{
    public partial class Base
    {
        public static void CreateHttpClientWithIHttpClientFactory()
        {
            // определяем коллекцию сервисов
            var services = new ServiceCollection();
            // добавляем сервисы, связанные с HttpClient, в том числе IHttpClientFactory
            services.AddHttpClient();
            // создаем провайдер сервисов
            var serviceProvider = services.BuildServiceProvider();
            // получаем сервис IHttpClientFactory
            var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
            // создаем объект HttpClient
            var httpClient = httpClientFactory?.CreateClient();

            // использование HttpClient
        }
        public static async Task SocketHttpClient()
        {
            HttpClient? httpClient;
            var socketsHandler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(2)
            };
            httpClient = new HttpClient(socketsHandler);

            // использование 
            HttpResponseMessage result = await httpClient.GetAsync("https://www.google.com");
            Console.WriteLine(result.StatusCode);
            string data = await result.Content.ReadAsStringAsync();
            Console.WriteLine(data);
        }
        public static async void CreateHttpClient()
        {
            Console.WriteLine("Приложение начало работу");
            for (int i = 0; i < 10; i++)
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage result = await client.GetAsync("https://google.com");
                    Console.WriteLine(result.StatusCode);
                }
            }
            Console.WriteLine("Приложение завершило работу");
        }
    }
}