using System.Net;
using System.Net.Http.Json;

namespace Networking.HttpProtocol.ConnectWebApi
{
    public class ServerApp
    {
        private const string URL = "http://localhost:5205";
        public static async void GetAllUser()
        {
            HttpClient httpClient = new HttpClient();

            List<Person>? people = await httpClient.GetFromJsonAsync<List<Person>>($"{URL}/api/users");
            if (people != null)
            {
                foreach (var person in people)
                {
                    Console.WriteLine($"Id: {person.Id}, Name: {person.Name}, Age: {person.Age}");
                }
            }
        }
        public static async void GetUserById(int id)
        {
            HttpClient httpClient = new HttpClient();

            using var response = await httpClient.GetAsync($"{URL}/api/users/{id}");
            // если объект на сервере найден, то есть статусный код равен 404
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Error? error = await response.Content.ReadFromJsonAsync<Error>();
                Console.WriteLine(error?.Message);
            }
            else if (response.StatusCode == HttpStatusCode.OK)
            {
                // считываем ответ
                Person? person = await response.Content.ReadFromJsonAsync<Person>();
                Console.WriteLine($"Id: {person?.Id}, Name: {person?.Name}, Age: {person?.Age}");
            }

        }
        public static async void AddUser(Person newPerson)
        {
            HttpClient httpClient = new HttpClient();

            using var response = await httpClient.PostAsJsonAsync($"{URL}/api/users/", newPerson);
            // считываем ответ и десериализуем данные в объект Person
            Person? person = await response.Content.ReadFromJsonAsync<Person>();
            Console.WriteLine($"{person?.Id} - {person?.Name}");
        }
        public static async void UpdateUser(Person updatePerson)
        {
            HttpClient httpClient = new HttpClient();

            using var response = await httpClient.PutAsJsonAsync($"{URL}/api/users/", updatePerson);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // если возникла ошибка, считываем сообщение об ошибке
                Error? error = await response.Content.ReadFromJsonAsync<Error>();
                Console.WriteLine(error?.Message);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                // десериализуем ответ в объект Person
                Person? person = await response.Content.ReadFromJsonAsync<Person>();
                Console.WriteLine($"{person?.Id} - {person?.Name} ({person?.Age})");
            }
        }
        public static async void DeleteUser(int id)
        {
            HttpClient httpClient = new HttpClient();

            using var response = await httpClient.DeleteAsync($"{URL}/api/users/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // если возникла ошибка, считываем сообщение об ошибке
                Error? error = await response.Content.ReadFromJsonAsync<Error>();
                Console.WriteLine(error?.Message);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                // десериализуем ответ в объект Person
                Person? person = await response.Content.ReadFromJsonAsync<Person>();
                Console.WriteLine($"{person?.Id} - {person?.Name} ({person?.Age})");
            }
        }
        record Error(string Message);
    }
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Age { get; set; }
    }
}