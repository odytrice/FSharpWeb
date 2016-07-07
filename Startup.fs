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
open System

type Startup (env: IHostingEnvironment) =
    let builder = ConfigurationBuilder()
                    .SetBasePath(env.ContentRootPath)
                    .AddJsonFile("appsettings.json", true, true)
                    .AddJsonFile(sprintf "appsettings.%s.json" env.EnvironmentName, true)
                    .AddEnvironmentVariables()

    let config = builder.Build()

    member this.ConfigureServices (services: IServiceCollection) =
        services.AddMvc() |> ignore

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

        

        app.UseStaticFiles()
            |> ignore

        app.UseMvc(fun (routes:IRouteBuilder) -> routes.MapRoute("default","{controller=Home}/{action=Index}/{id?}") |> ignore) |> ignore

        app.UseWelcomePage() |> ignore        