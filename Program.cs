    using Microsoft.EntityFrameworkCore;

    //НЕ работает кнопка сохранить
    // underfined во всей бд наверно надо переписать applicationcontext



    var builder = WebApplication.CreateBuilder();
    builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite("Data Source=web_db.db"));//возможно исправить тутs
    
    var app = builder.Build();

    app.UseDefaultFiles();
    app.UseStaticFiles();


    app.MapGet("/api/persons", async (ApplicationContext db) => await db.Persons.ToListAsync());
    app.MapGet("/api/persons/{id:int}", async (int id, ApplicationContext db) =>
    {
    // получаем пользователя по id
    Person? person = await db.Persons.FirstOrDefaultAsync(u => u.Id == id);
 
    // если не найден, отправляем статусный код и сообщение об ошибке
    if (person == null) return Results.NotFound(new { message = "Пользователь не найден" });
 
    // если пользователь найден, отправляем его
    return Results.Json(person);
    });

    app.MapDelete("/api/persons/{id:int}", async (int id, ApplicationContext db) =>
    {
    // получаем пользователя по id
    Person? person = await db.Persons.FirstOrDefaultAsync(u => u.Id == id);
 
    // если не найден, отправляем статусный код и сообщение об ошибке
    if (person == null) return Results.NotFound(new { message = "Пользователь не найден" });
 
    // если пользователь найден, удаляем его
    db.Persons.Remove(person);
    await db.SaveChangesAsync();
    return Results.Json(person);
    });

    app.MapPost("/api/persons", async (Person person, ApplicationContext db) =>
    {
    // добавляем пользователя в массив
    await db.Persons.AddAsync(person);
    await db.SaveChangesAsync();
    return person;
    }); 
    
    app.MapPut("/api/persons", async (Person personData, ApplicationContext db) =>
    {
    // получаем пользователя по id
    var person = await db.Persons.FirstOrDefaultAsync(u => u.Id == personData.Id);
 
    // если не найден, отправляем статусный код и сообщение об ошибке
    if (person == null) return Results.NotFound(new { message = "Пользователь не найден" });
 
    // если пользователь найден, изменяем его данные и отправляем обратно клиенту
        person.Name = personData.Name;
        person.MobilePhone = personData.MobilePhone;
        person.JobTitle = personData.JobTitle;
        person.BirthDate = personData.BirthDate;
    await db.SaveChangesAsync();
    return Results.Json(person);
    });

    app.Run();
