module OnlinerScraper

open FSharp.Data
open FSharp.Collections.ParallelSeq
open Bubble.Datascrapper.Types

type feed = XmlProvider<"https://www.onliner.by/feed">

let _getOnlinerArticleText (link:string) = async {
    let doc = HtmlDocument.Load(link)

    let div = doc.CssSelect("div.news-text") |> List.head

    let text = div.Elements() 
                    |> Seq.filter (fun x -> 
                                not(x.HasName("div"))
                                && not (x.HasName("script")))
                    |> Seq.map (fun x -> x.InnerText())
                    |> String.concat "\n"
    return text
}

let _getOnlinerArticles () = async {
    let rssResults = feed.GetSample()

    let articles = rssResults.Channel.Items
                    |> PSeq.withDegreeOfParallelism 5
                    |> PSeq.map (fun x -> { title = x.Title;
                                            link = x.Link; 
                                            text = _getOnlinerArticleText x.Link |> Async.RunSynchronously; 
                                            pubDate = x.PubDate })
                    |> PSeq.toList
    return articles
}

let GetOnlinerArticlesAsync () = 
    let mytask = Async.StartAsTask(_getOnlinerArticles())
    mytask