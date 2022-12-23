using System.Net;
using System.Net.Http.Json;

namespace Networking.HttpProtocol
{
    public partial class Base
    {
        public static async void SendGetAsyncJson()
        {
            //Birinchi ClientApp proyektini ishlatib qo'yish kerak;
            HttpClient httpClient = new HttpClient();

            object? data = await httpClient.GetFromJsonAsync($"{URL}/test-user", typeof(Person));
            if (data is Person person)
            {
                Console.WriteLine($"Name: {person.Name}   Age: {person.Age}");
            }
        }
        public static async void SendGetAsyncJsonSecond()
        {
            //Birinchi ClientApp proyektini ishlatib qo'yish kerak;
            HttpClient httpClient = new HttpClient();

            Person? person = await httpClient.GetFromJsonAsync<Person>($"{URL}/test-user");
            Console.WriteLine($"Name: {person?.Name}   Age: {person?.Age}");
        }
        public static async void SendGetAsyncJsonWithParametrs()
        {
            List<int?> tests = new List<int?> { 12, 1, null };
            foreach (var test in tests)
            {
                HttpClient httpClient = new HttpClient();
                string url = $"{URL}/user/{test}";
                using var response = await httpClient.GetAsync(url);

                if (response.StatusCode == HttpStatusCode.BadRequest ||
                         response.StatusCode == HttpStatusCode.NotFound)
                {
                    // получаем информацию об ошибке
                    Error? error = await response.Content.ReadFromJsonAsync<Error>();
                    Console.WriteLine(response.StatusCode);
                    Console.WriteLine(error?.Message);
                }
                else
                {
                    // если запрос завершился успешно, получаем объект Person
                    Person? person = await response.Content.ReadFromJsonAsync<Person>();
                    Console.WriteLine($"Name: {person?.Name}   Age: {person?.Age}");
                }
            }
        }
        public static async void SendJsonRequest()
        {
            HttpClient httpClient = new HttpClient();

            // отправляемый объект 
            User tom = new User { Name = "Tom", Age = 38 };
            // создаем JsonContent
            JsonContent content = JsonContent.Create(tom);
            // отправляем запрос
            using var response = await httpClient.PostAsync($"{URL}/create", content);
            User? person = await response.Content.ReadFromJsonAsync<User>();
            Console.WriteLine($"{person?.Id} - {person?.Name}");
        }
        public static async void SendPostAsJsonAsync()
        {
            HttpClient httpClient = new HttpClient();

            // отправляемый объект 
            User tom = new User { Name = "Tom", Age = 38 };
            // отправляем запрос
            using var response = await httpClient.PostAsJsonAsync($"{URL}/create", tom);
            User? person = await response.Content.ReadFromJsonAsync<User>();
            Console.WriteLine($"{person?.Id} - {person?.Name}");
        }
    }
}