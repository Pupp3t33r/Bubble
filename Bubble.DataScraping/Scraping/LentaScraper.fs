module LentaScraper

open FSharp.Data
open FSharp.Collections.ParallelSeq
open System.Text.RegularExpressions
open Bubble.Datascrapper.Types

type feed = XmlProvider<"https://lenta.ru/rss">

let _getLentaArticles () = async {
    let rssResults = feed.GetSample()

    let articles = rssResults.Channel.Items
                    |> PSeq.withDegreeOfParallelism 5
                    |> PSeq.map (fun x -> { title = x.Title;
                                            link = x.Link; 
                                            shortText = x.Description.ToString();
                                            source = "Lenta";
                                            text = null;
                                            pubDate = x.PubDate; }:LentaArticleRecord)
                    |> PSeq.toList
    return articles
}

let _getLentaArticleText (link:string) = async {
    let doc = HtmlDocument.Load(link)

    let div = doc.CssSelect("div.topic-body__content") |> List.head

    let text = div.Elements() 
                    |> Seq.filter (fun x -> 
                                not(x.HasName("div"))
                                && not (x.HasName("script")))
                    |> Seq.map (fun x -> x.InnerText())
                    |> String.concat "\n"
    return text
}

let GetLentaArticleText (link:string) = 
    let mytask = Async.StartAsTask(_getLentaArticleText(link))
    mytask

let GetLentaArticlesAsync () = 
    let mytask = Async.StartAsTask(_getLentaArticles())
    mytask