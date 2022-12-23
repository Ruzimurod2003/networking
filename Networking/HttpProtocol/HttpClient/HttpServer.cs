using System.Net;
using System.Text;

namespace Networking.HttpProtocol
{
    public partial class Base
    {
        public static async void StartContextWithHttpServer()
        {
            HttpListener server = new HttpListener();
            // установка адресов прослушки
            server.Prefixes.Add("http://127.0.0.1:8888/connection/");
            server.Start(); // начинаем прослушивать входящие подключения

            // получаем контекст
            var context = await server.GetContextAsync();

            var request = context.Request;  // получаем данные запроса
            var response = context.Response;    // получаем объект для установки ответа
            var user = context.User;        // получаем данные пользователя

            server.Stop(); // останавливаем сервер
        }
        public static async void StartHttpServerOfResult()
        {
            HttpListener server = new HttpListener();
            // установка адресов прослушки
            server.Prefixes.Add("http://127.0.0.1:8888/connection/");
            server.Start(); // начинаем прослушивать входящие подключения

            // получаем контекст
            var context = await server.GetContextAsync();

            var response = context.Response;
            // отправляемый в ответ код htmlвозвращает
            string responseText =
                @"<!DOCTYPE html>
                    <html>
                        <head>
                            <meta charset='utf8'>
                            <title>METANIT.COM</title>
                        </head>
                        <body>
                            <h2>Hello METANIT.COM</h2>
                        </body>
                    </html>";
            byte[] buffer = Encoding.UTF8.GetBytes(responseText);
            // получаем поток ответа и пишем в него ответ
            response.ContentLength64 = buffer.Length;
            using Stream output = response.OutputStream;
            // отправляем данные
            await output.WriteAsync(buffer);
            await output.FlushAsync();

            Console.WriteLine("Запрос обработан");

            server.Stop();
        }
        public static async void RecieveInformationOfRequest()
        {

            HttpListener server = new HttpListener();
            // установка адресов прослушки
            server.Prefixes.Add("http://127.0.0.1:8888/connection/");
            server.Start(); // начинаем прослушивать входящие подключения
            Console.WriteLine("Сервер запущен. Ожидание подключений...");

            // получаем контекст
            var context = await server.GetContextAsync();

            var request = context.Request;  // получаем данные запроса

            Console.WriteLine($"адрес приложения: {request.LocalEndPoint}");
            Console.WriteLine($"адрес клиента: {request.RemoteEndPoint}");
            Console.WriteLine(request.RawUrl);
            Console.WriteLine($"Запрошен адрес: {request.Url}");
            Console.WriteLine("Заголовки запроса:");
            foreach (string item in request.Headers.Keys)
            {
                Console.WriteLine($"{item}:{request.Headers[item]}");
            }

            var response = context.Response;    // получаем объект для установки ответа
            byte[] buffer = Encoding.UTF8.GetBytes("Hello METANIT");
            // получаем поток ответа и пишем в него ответ
            response.ContentLength64 = buffer.Length;
            using Stream output = response.OutputStream;
            // отправляем данные
            await output.WriteAsync(buffer);
            await output.FlushAsync();

            server.Stop(); // останавливаем сервер
        }
    }
}