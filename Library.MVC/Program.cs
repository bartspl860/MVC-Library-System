using Library.BLL;
using Library.DAL;
using Library.Model;
using Library.MVC.Controllers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContextPool<DbLibraryContext>(options =>
    options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False")
);

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IPublishingHouseRepository, PublishingHouseRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IReaderRepository, ReaderRepository>();
builder.Services.AddScoped<IBorrowRepository, BorrowRepository>();

builder.Services.AddScoped<IUnitOfWork>(provider =>
{
    var dbContext = provider.GetService<DbLibraryContext>();
    var unitofwork = new UnitOfWork();

    unitofwork.BooksRepository = new BookRepository(dbContext);
    unitofwork.PublishingHousesRepository = new PublishingHouseRepository(dbContext);
    unitofwork.AuthorsRepository = new AuthorRepository(dbContext);
    unitofwork.ReadersRepository = new ReaderRepository(dbContext);
    unitofwork.BorrowsRepository = new BorrowRepository(dbContext);

    return unitofwork;
});


builder.Services.AddScoped<BooksController>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<HomeController>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(bld =>
    {
        bld.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseCors();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
