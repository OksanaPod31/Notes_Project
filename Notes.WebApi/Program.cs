using Microsoft.Extensions.FileProviders;
using Notes.Application;
using Notes.Application.Common.Mapping;
using Notes.Application.Interfaces;
using Notes.Persistence;
using Notes.WebApi.Middleware;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(INoteDbContext).Assembly));
});
builder.Services.AddAplication();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<NotesDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception e)
    {

    }
}





app.UseCustomExceptionHandler();
app.UseRouting();
//var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
//string wrongpath = "\\bin\\Debug\\net0.6\\";
//assemblyDirectory = assemblyDirectory.Substring(0, assemblyDirectory.Length - wrongpath.Length+1);
//var assetDirectory = Path.Combine(assemblyDirectory, "Static");

//// use it
//app.UseStaticFiles(new StaticFileOptions
//{
//    //FileProvider = new PhysicalFileProvider(
//    //       Path.Combine(builder.Environment.ContentRootPath, "Static")),
//    //RequestPath = "/Static"
//    FileProvider = new PhysicalFileProvider(assetDirectory),
//    RequestPath = "/Static"
//});
//D:\ProjVis\Portfolio\Testovoe\Notes.Backend\Notes.WebApi\Static\
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Note}/{action=GetAll}/{id?}");


//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapDefaultControllerRoute();
//});
//app.MapGet("/", () => "Hello World!");

app.Run();
