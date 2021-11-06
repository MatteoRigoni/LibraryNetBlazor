using Library.Repository;
using Library.Repository.ApiClient;
using Library.UseCases;
using Library.WebApp;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddSingleton<AuthenticationStateProvider, CustomTokenAuthenticationStateProvider>();

builder.Services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddTransient<IAuthorRepository, AuthorRepository>();
builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddTransient<IAuthenticationUseCase, AuthenticationUseCase>();
builder.Services.AddTransient<IAuthorsScreenUseCase, AuthorsScreenUseCase>();
builder.Services.AddTransient<IBooksScreenUseCase, BooksScreenUseCase>();

builder.Services.AddSingleton<ITokenRepository, TokenRepository>();
builder.Services.AddSingleton<IWebApiExecuter>(w => new WebApiExecuter(
    "https://localhost:7200",
    new HttpClient(),
    w.GetRequiredService<ITokenRepository>()));
    //"blazorwasm",
    //"secret"));

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
