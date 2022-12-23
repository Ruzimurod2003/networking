using System.Net.Http.Headers;
namespace Networking.HttpProtocol
{
    public partial class Base
    {
        public static async void SendStreamAndArrayBytes()
        {
            HttpClient httpClient = new HttpClient();

            // отправляемые данные
            string filePath = @"C:\Images\MySertificate.jpg";
            using var fileStream = File.OpenRead(filePath);
            // создаем объект HttpContent
            StreamContent content = new StreamContent(fileStream);
            // отправляем запрос
            using var response = await httpClient.PostAsync($"{URL}/upload_JPG", content);
            // получаем ответ
            string responseText = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseText);
        }
        public static async void SendStreamInByteArrayContent_String()
        {
            HttpClient httpClient = new HttpClient();

            // отправляемые данные
            string message = "Hello METANIT.COM";
            // считываем строку в массив байтов 
            byte[] messageToBytes = System.Text.Encoding.UTF8.GetBytes(message);
            // формируем отправляемое содержимое
            var content = new ByteArrayContent(messageToBytes);
            // отправляем запрос
            using var response = await httpClient.PostAsync($"{URL}/stream", content);
            // получаем ответ
            string responseText = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseText);
        }
        public static async void SendOneFileInMultipartFormDataContent()
        {
            HttpClient httpClient = new HttpClient();

            // адрес сервера
            var serverAddress = $"{URL}/upload_one_file";
            // пусть к файлу
            var filePath = @"C:\Images\MySertificate.jpg";

            // создаем MultipartFormDataContent
            using var multipartFormContent = new MultipartFormDataContent();
            // Загружаем отправляемый файл
            var fileStreamContent = new StreamContent(File.OpenRead(filePath));
            // Устанавливаем заголовок Content-Type
            fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            // Добавляем загруженный файл в MultipartFormDataContent
            multipartFormContent.Add(fileStreamContent, name: "file", fileName: "forest.jpg");

            // Отправляем файл
            using var response = await httpClient.PostAsync(serverAddress, multipartFormContent);
            // считываем ответ
            var responseText = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseText);
        }
        public static async void SendManyFileUploads()
        {
            HttpClient httpClient = new HttpClient();

            // адрес для отправки 
            var serverAddress = $"{URL}/upload_one_file";
            // пути к файлам
            var files = new string[] { "C:\\Images\\MySertificate.jpg", "C:\\Images\\MySertificate_1.jpg" };

            using var multipartFormContent = new MultipartFormDataContent();
            // в цикле добавляем все файлы в MultipartFormDataContent
            foreach (var file in files)
            {
                // получаем краткое имя файла
                var fileName = Path.GetFileName(file);
                var fileStreamContent = new StreamContent(File.OpenRead(file));
                fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                multipartFormContent.Add(fileStreamContent, name: "files", fileName: fileName);
            }
            // Отправляем файлы
            using var response = await httpClient.PostAsync(serverAddress, multipartFormContent);
            // считываем ответ
            var responseText = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseText);

        }
        public static async void SendFileWithUserData()
        {
            HttpClient httpClient = new HttpClient();

            using var multipartFormContent = new MultipartFormDataContent();

            // добавляем обычные данные
            multipartFormContent.Add(new StringContent("Tom"), name: "username");
            multipartFormContent.Add(new StringContent("tom@localhost.com"), name: "email");

            // добавляем файл
            var fileStreamContent = new StreamContent(File.OpenRead("C:\\Images\\MySertificate.jpg"));
            fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            multipartFormContent.Add(fileStreamContent, name: "files", fileName: "logo.jpg");

            // Отправляем данные
            using var response = await httpClient.PostAsync($"{URL}/upload_many_files", multipartFormContent);
            // считываем ответ
            var responseText = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseText);

        }
    }
}