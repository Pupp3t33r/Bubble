module OnlinerScraper

open FSharp.Data

type feed = XmlProvider<"https://www.onliner.by/feed">

type Article = 
    {
        title : string
        text : string
        link: string
        pubDate : System.DateTimeOffset
    }

let articles = feed.GetSample()

let getOnlinerArticleText (link:string) =
    let doc = HtmlDocument.Load(link)

    let div = doc.CssSelect("div.news-text") |> List.head

    let text = div.Elements() 
                    |> Seq.filter (fun x -> 
                                not(x.HasName("div"))
                                && not (x.HasName("script")))
                    |> Seq.map (fun x -> x.InnerText())
                    |> String.concat "\n"
    text

let getOnlinerArticles () = 
    let articles = articles.Channel.Items 
                    |> Seq.map (fun x -> { title = x.Title;
                                           link = x.Link; 
                                           text = getOnlinerArticleText x.Link; 
                                           pubDate = x.PubDate })
    articles