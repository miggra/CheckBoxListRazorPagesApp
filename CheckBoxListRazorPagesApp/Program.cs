using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CheckBoxListRazorPagesApp.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(
    );
builder.Services.AddDbContext<CheckBoxListContext>(options =>
    options.UseInMemoryDatabase("Messages")
    //options.UseSqlServer(builder.Configuration.GetConnectionString("CheckBoxListContext") ?? throw new InvalidOperationException("Connection string 'CheckBoxListContext' not found."))
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    var dbContext = services.GetService<CheckBoxListContext>();
    dbContext.InitialFillTheMessageDb();
}

app.Run();
