using BlazorCrud.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorCrud.Client.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.SessionStorage;
using BlazorCrud.Client.Extensiones;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:29638") });

builder.Services.AddScoped<IProyectoService, ProyectoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<ITareaService, TareaService>();
builder.Services.AddScoped<IRecursoService, RecursoService>();
builder.Services.AddScoped<IRolPermisoService, RolPermisoService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddSingleton<IEmailService, EmailService>();


builder.Services.AddSingleton(new EmailSettings
{
    FromAddress = "dantetc987@gmail.com",
    SmtpServer = "smtp.gmail.com",
    SmtpPort = 587,
    SmtpUser = "dantetc987@gmail.com",
    SmtpPassword = "aoeirqurkqhvdsra"
});

Console.WriteLine("Email settings initialized: " +
                  $"From: {builder.Services.BuildServiceProvider().GetRequiredService<EmailSettings>().FromAddress}, " +
                  $"SMTP Server: {builder.Services.BuildServiceProvider().GetRequiredService<EmailSettings>().SmtpServer}, " +
                  $"Port: {builder.Services.BuildServiceProvider().GetRequiredService<EmailSettings>().SmtpPort}");


builder.Services.AddSweetAlert2();

builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<AuthenticationStateProvider, AutenticacionExtension>();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
