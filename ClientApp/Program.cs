var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

#region For Web Api
int id = 1; // для генерации id объектов
// начальные данные
List<Person> users = new List<Person>
{
    new() { Id = id++, Name = "Tom", Age = 37 },
    new() { Id = id++, Name = "Bob", Age = 41 },
    new() { Id = id++, Name = "Sam", Age = 24 }
};

app.MapGet("/api/users", () => users);

app.MapGet("/api/users/{id}", (int id) =>
{
    // получаем пользователя по id
    Person? user = users.FirstOrDefault(u => u.Id == id);
    // если не найден, отправляем статусный код и сообщение об ошибке
    if (user == null) return Results.NotFound(new { message = "Пользователь не найден" });

    // если пользователь найден, отправляем его
    return Results.Json(user);
});

app.MapDelete("/api/users/{id}", (int id) =>
{
    // получаем пользователя по id
    Person? user = users.FirstOrDefault(u => u.Id == id);

    // если не найден, отправляем статусный код и сообщение об ошибке
    if (user == null) return Results.NotFound(new { message = "Пользователь не найден" });

    // если пользователь найден, удаляем его
    users.Remove(user);
    return Results.Json(user);
});

app.MapPost("/api/users", (Person user) =>
{

    // устанавливаем id для нового пользователя
    user.Id = id++;
    // добавляем пользователя в список
    users.Add(user);
    return user;
});

app.MapPut("/api/users", (Person userData) =>
{

    // получаем пользователя по id
    var user = users.FirstOrDefault(u => u.Id == userData.Id);
    // если не найден, отправляем статусный код и сообщение об ошибке
    if (user == null) return Results.NotFound(new { message = "Пользователь не найден" });
    // если пользователь найден, изменяем его данные и отправляем обратно клиенту

    user.Age = userData.Age;
    user.Name = userData.Name;
    return Results.Json(user);
});

#endregion

#region All for HttpClient
// app.MapGet("/", () => "Hello World!");

// app.MapGet("/test-user", () =>
// {
//     return new Person(1, "Tom", 18);
// });

// app.MapGet("/user/{id?}", (int? id) =>
// {
//     if (id is null)
//         return Results.BadRequest(new { Message = "Некорректные данные в запросе" });
//     else if (id != 1)
//         return Results.NotFound(new { Message = $"Объект с id={id} не существует" });
//     else
//         return Results.Json(new Person(1, "Bob", 42));
// });

// app.MapGet("/headlines", (HttpContext context) =>
// {
//     // пытаемся получить заголовок "SecreteCode"
//     context.Request.Headers.TryGetValue("User-Agent", out var userAgent);
//     // пытаемся получить заголовок "SecreteCode"
//     context.Request.Headers.TryGetValue("SecreteCode", out var secreteCode);
//     // отправляем данные обратно клиенту
//     return $"User-Agent: {userAgent}    SecreteCode: {secreteCode}";
// });

// app.MapPost("/text", async (HttpContext httpContext) =>
// {
//     using StreamReader reader = new StreamReader(httpContext.Request.Body);
//     string name = await reader.ReadToEndAsync();
//     return $"Получены данные: {name}";
// });

// app.MapPost("/create", (User person) =>
// {
//     // устанавливает id у объекта Person
//     person.Id = Guid.NewGuid().ToString();
//     // отправляем обратно объект Person
//     return person;
// });

// app.MapPost("/form", async (HttpContext httpContext) =>
// {
//     // получаем данные формы
//     var form = httpContext.Request.Form;
//     string? name = form["name"];
//     string? email = form["email"];
//     string? age = form["age"];
//     await httpContext.Response.WriteAsync($"Name: {name}   Email:{email}    Age: {age}");
// });

// app.MapPost("/upload_JPG", async (HttpContext httpContext) =>
// {
//     // путь к папке, где будут храниться файлы
//     var uploadPath = $"{Directory.GetCurrentDirectory()}/uploads";
//     if (!Directory.Exists(uploadPath))
//         // создаем папку для хранения файлов
//         Directory.CreateDirectory(uploadPath);
//     // генерируем произвольное название файла с помощью guid
//     string fileName = Guid.NewGuid().ToString();
//     // получаем поток
//     using (var fileStream = new FileStream($"{uploadPath}/{fileName}.jpg", FileMode.Create))
//     {
//         await httpContext.Request.Body.CopyToAsync(fileStream);
//     }

//     await httpContext.Response.WriteAsync("Данные сохранены");
// });

// app.MapPost("/stream", async (HttpContext httpContext) =>
// {
//     // считываем полученные данные в строку
//     using StreamReader streamReader = new StreamReader(httpContext.Request.Body);
//     string message = await streamReader.ReadToEndAsync();
//     await httpContext.Response.WriteAsync($"Отправлено сообщение: {message}");
// });

// app.MapPost("/upload_one_file", async (HttpContext context) =>
// {
//     // получем коллецию загруженных файлов
//     IFormFileCollection files = context.Request.Form.Files;
//     // путь к папке, где будут храниться файлы
//     var uploadPath = $"{Directory.GetCurrentDirectory()}/uploads";
//     if (!Directory.Exists(uploadPath))
//         // создаем папку для хранения файлов
//         Directory.CreateDirectory(uploadPath);

//     // пробегаемся по всем файлам
//     foreach (var file in files)
//     {
//         // формируем путь к файлу в папке uploads
//         string fullPath = $"{uploadPath}/{file.FileName}";

//         // сохраняем файл в папку uploads
//         using (var fileStream = new FileStream(fullPath, FileMode.Create))
//         {
//             await file.CopyToAsync(fileStream);
//         }
//     }
//     await context.Response.WriteAsync("Файлы успешно загружены");
// });

// app.MapPost("/upload_many_files", async (HttpContext context) =>
// {
//     var form = context.Request.Form;
//     // получаем отдельные данные
//     string? username = form["username"];
//     string? email = form["email"];

//     // получаем коллецию загруженных файлов
//     IFormFileCollection files = form.Files;
//     // путь к папке, где будут храниться файлы
//     var uploadPath = $"{Directory.GetCurrentDirectory()}/uploads";
//     if (!Directory.Exists(uploadPath))
//         // создаем папку для хранения файлов
//         Directory.CreateDirectory(uploadPath);

//     foreach (var file in files)
//     {
//         // путь к папке uploads
//         string fullPath = $"{uploadPath}/{file.FileName}";

//         // сохраняем файл в папку uploads
//         using (var fileStream = new FileStream(fullPath, FileMode.Create))
//         {
//             await file.CopyToAsync(fileStream);
//         }
//     }
//     return $"Данные пользователя {username} ({email}) успешно загружены";
// });

// app.MapGet("/setCookie", (HttpContext context) =>
// {
//     // устанавливаем куки
//     context.Response.Cookies.Append("name", "Tom");
//     context.Response.Cookies.Append("email", "tom@localhost.com");
// });

// app.MapGet("/getCookie", (HttpContext context) =>
// {
//     // получаем куки
//     context.Request.Cookies.TryGetValue("name", out string? name);
//     context.Request.Cookies.TryGetValue("email", out string? email);

//     return $"Name: {name}   Email:{email}";
// });
#endregion

app.Run();

#region All for HttpClient
// class User
// {
//     public string Id { get; set; } = "";
//     public string Name { get; set; } = "";
//     public int Age { get; set; }
// }
// record Person(int Id, string Name, int Age);
#endregion

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Age { get; set; }
}