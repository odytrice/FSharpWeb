// Learn more about F# at http://fsharp.org

open System
open System.IO
open FSharpWeb
open Microsoft.AspNetCore.Hosting

[<EntryPoint>]
let main argv = 
    let host = WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
    host.Run() 
    0 // return an integer exit code
