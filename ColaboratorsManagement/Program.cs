using ColaboratorsManagement.Application;
using ColaboratorsManagement.Application.Interface;
using ColaboratorsManagement.Database;
using Microsoft.EntityFrameworkCore; // Add this using directive to resolve the issue

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
   options.UseSqlite("Data Source=colaborators.db"));
builder.Services.AddScoped<ICollaboratorService, CollaboratorService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
   name: "default",
   pattern: "{controller=Collaborator}/{action=Index}/{id?}");

app.Run();
