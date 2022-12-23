using System.Net.Http.Json;

namespace Networking.HttpProtocol
{
    public partial class Base
    {
        //PostAsync(); PutAsync(); PatchAsync() larning ishatlishi bir hil.
        public static async void SendTextRequest()
        {
            HttpClient httpClient = new HttpClient();

            StringContent content = new StringContent("Tom");
            // определяем данные запроса
            using var request = new HttpRequestMessage(HttpMethod.Post, $"{URL}/text");
            // установка отправляемого содержимого
            request.Content = content;
            // отправляем запрос
            using var response = await httpClient.SendAsync(request);
            // получаем ответ
            string responseText = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseText);
        }
        public static async void SendTextWithPostMethod()
        {
            HttpClient httpClient = new HttpClient();

            StringContent content = new StringContent("Tom");
            using var response = await httpClient.PostAsync($"{URL}/text", content);
            string responseText = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseText);
        }
        public static async void SendFormAndFormUrlEncodedContent()
        {
            HttpClient httpClient = new HttpClient();

            // данные для отправки в виде объекта IEnumerable<KeyValuePair<string, string>>
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                ["name"] = "Tom",
                ["email"] = "tom@localhost.com",
                ["age"] = "38"
            };
            // создаем объект HttpContent
            HttpContent contentForm = new FormUrlEncodedContent(data);
            // отправляем запрос
            using var response = await httpClient.PostAsync($"{URL}/form", contentForm);
            // получаем ответ
            string responseText = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseText);
        }
    }
    public class User
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public int Age { get; set; }
    }
}