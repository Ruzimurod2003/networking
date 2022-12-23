using System.Net;

namespace Networking.HttpProtocol
{
    public partial class Base
    {
        public static async void RecieveCookiesOfSetCookie()
        {
            HttpClient httpClient = new HttpClient();

            // адрес сервера
            Uri uri = new Uri($"{URL}/setCookie");

            using var response = await httpClient.GetAsync(uri);
            CookieContainer cookies = new CookieContainer();
            // получаем из запроса все элементы с заголовком Set-Cookie
            foreach (var cookieHeader in response.Headers.GetValues("Set-Cookie"))
                // добавляем заголовки кук в CookieContainer
                cookies.SetCookies(uri, cookieHeader);

            // получение всех куки
            foreach (Cookie cookie in cookies.GetCookies(uri))
                Console.WriteLine($"{cookie.Name}: {cookie.Value}");

            // получение отдельных куки
            // получаем куку "email"
            Cookie? email = cookies.GetCookies(uri).FirstOrDefault(c => c.Name == "email");
            Console.WriteLine($"Электронный адрес: {email?.Value}");
        }
        public static async void RecieveCookiesOfHttpClientHandler()
        {
            HttpClient? httpClient;

            // адрес сервера
            Uri uri = new Uri($"{URL}/setCookie");
            var cookies = new CookieContainer();
            using var handler = new HttpClientHandler();
            handler.CookieContainer = cookies;

            httpClient = new HttpClient(handler);

            using var response = await httpClient.GetAsync(uri);
            // получение всех куки
            foreach (Cookie cookie in cookies.GetCookies(uri))
                Console.WriteLine($"{cookie.Name}: {cookie.Value}");
            // получение отдельных куки
            // получаем куку "email"
            Cookie? email = cookies.GetCookies(uri).FirstOrDefault(c => c.Name == "email");
            Console.WriteLine($"Электронный адрес: {email?.Value}");
        }
        public static async void SendCookieOfHeader()
        {
            HttpClient httpClient = new HttpClient();

            // адрес сервера
            Uri uri = new Uri($"{URL}/getCookie");

            CookieContainer cookies = new CookieContainer();
            // устанавливаем куки name и email
            cookies.Add(uri, new Cookie("name", "Bob"));
            cookies.Add(uri, new Cookie("email", "bob@localhost.com"));
            // устанавливаем заголовок cookie
            httpClient.DefaultRequestHeaders.Add("cookie", cookies.GetCookieHeader(uri));

            using var response = await httpClient.GetAsync(uri);
            string responseText = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseText);
        }
    }
}