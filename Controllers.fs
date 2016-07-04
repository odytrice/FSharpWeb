namespace FSharpWeb

type HomeController() =
    inherit Controller()

    member this.Index() = this.View()

    member this.About() =
        ViewData["Message"] <- "Your application description page.";
        this.View()

    member this.Contact() =
        ViewData["Message"] <- "Your contact page.";
        this.View()

    member this.Error() = this.View()