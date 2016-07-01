namespace FSharpWeb

open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Configuration
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.DependencyInjection;
open Microsoft.AspNetCore.Http
open System.Threading.Tasks
open Microsoft.FSharp.Control

type Startup (env: IHostingEnvironment) =
    let builder = ConfigurationBuilder()
                    .SetBasePath(env.ContentRootPath)
                    .AddJsonFile("appsettings.json", true, true)
                    .AddJsonFile(sprintf "appsettings.%s.json" env.EnvironmentName, true)
                    .AddEnvironmentVariables()

    let config = builder.Build()

    member this.ConfigureServices (services: IServiceCollection) = ()

    member this.Configure (app:IApplicationBuilder) =
    
        app.UseFileServer() |> ignore
        ()
