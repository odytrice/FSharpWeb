namespace FSharpWeb

open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Configuration
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.DependencyInjection;
open Microsoft.AspNetCore.Routing
open Microsoft.AspNetCore.Http
open System.Threading.Tasks
open Microsoft.FSharp.Control
open Microsoft.Extensions.Logging

type Startup (env: IHostingEnvironment) =
    let builder = ConfigurationBuilder()
                    .SetBasePath(env.ContentRootPath)
                    .AddJsonFile("appsettings.json", true, true)
                    .AddJsonFile(sprintf "appsettings.%s.json" env.EnvironmentName, true)
                    .AddEnvironmentVariables()

    let config = builder.Build()

    member this.ConfigureServices (services: IServiceCollection) = ()

    member this.Configure (app:IApplicationBuilder) (loggerFactory: ILoggerFactory) =
        loggerFactory.AddConsole(config.GetSection("Logging"))
                     .AddDebug() 
                     |> ignore

        if env.IsDevelopment() then
            app.UseDeveloperExceptionPage()
               .UseBrowserLink() 
               |> ignore
        else
            app.UseExceptionHandler("/Home/Error") 
            |> ignore

        app.UseDefaultFiles()
            .UseStaticFiles()
            |> ignore
        