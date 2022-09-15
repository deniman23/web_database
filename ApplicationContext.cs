using Microsoft.EntityFrameworkCore;
public class ApplicationContext : DbContext
{
    public DbSet<Person> Persons { get; set; } = null!;
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.EnsureCreated();   // создаем базу данных при первом обращении
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>().HasData(
                new Person { Id = 1, Name = "Tom", MobilePhone = 3732312, JobTitle = "Senior Developer", BirthDate = "23.07.1986" },
                new Person { Id = 2, Name = "Bob", MobilePhone = 5734213, JobTitle = "Junior Developer", BirthDate = "01.04.2001" },
                new Person { Id = 3, Name = "Sam", MobilePhone = 6337358, JobTitle = "Project Manager", BirthDate = "16.08.1988" }
        );
    }
}