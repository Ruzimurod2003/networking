namespace Networking.HttpProtocol
{
    public partial class Base
    {
        public static async void SetupHeadLinesAndPostMethod()
        {
            HttpClient httpClient = new HttpClient();

            HttpContent content = new StringContent("Hello METANIT.COM");
            // устанавливаем заголовок 
            content.Headers.Add("SecreteCode", "Anything");

            using var response = await httpClient.PostAsync($"{URL}/text", content);
            string responseText = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseText);
        }
        public static async void SetupHeadLinesForRequest()
        {
            HttpClient httpClient = new HttpClient();

            // адрес сервера
            var serverAddress = $"{URL}/headlines";
            using var request = new HttpRequestMessage(HttpMethod.Get, serverAddress);
            // устанавливаем оба заголовка
            request.Headers.Add("User-Agent", "Mozilla Failfox 5.6");
            request.Headers.Add("SecreteCode", "salom");

            using var response = await httpClient.SendAsync(request);
            var responseText = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseText);
        }
        public static async void SendGetAsyncOfHeadLines()
        {
            HttpClient httpClient = new HttpClient();

            // адрес сервера
            var serverAddress = $"{URL}/headlines";
            // устанавливаем оба заголовка
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla FIrefox 5.4");
            httpClient.DefaultRequestHeaders.Add("SecreteCode", "hello");

            using var response = await httpClient.GetAsync(serverAddress);
            var responseText = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseText);
        }

        public static async void RecieveHeadLines()
        {
            HttpClient httpClient = new HttpClient();

            var serverAddress = $"{URL}";
            using var response = await httpClient.GetAsync(serverAddress);
            var dateValues = response.Headers.GetValues("Date");

            Console.WriteLine(dateValues.FirstOrDefault());
        }
        public static async void RecieveHeadLinesOfTryValue()
        {
            HttpClient httpClient = new HttpClient();

            var serverAddress = $"{URL}/headlines";
            using var response = await httpClient.GetAsync(serverAddress);
            response.Headers.TryGetValues("date", out var dateValues);

            Console.WriteLine(dateValues?.FirstOrDefault());
        }
    }
}