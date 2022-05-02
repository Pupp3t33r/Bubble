module OnlinerScraper

open FSharp.Data
open FSharp.Collections.ParallelSeq
open System.Text.RegularExpressions
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

    let edit1 = text.Replace("Auto.Onlíner в Telegram: обстановка на дорогах и только самые важные новости
Есть о чем рассказать? Пишите в наш телеграм-бот. Это анонимно и быстро
Перепечатка текста и фотографий Onlíner без разрешения редакции запрещена. ng@onliner.by", "")

    let edit2 = text.Replace("Пишите нам: pv@onliner.by или t.me/vitpetrovich. Auto.Onlíner в Telegram: обстановка на дорогах и только самые важные новости Есть о чем рассказать? Пишите в наш телеграм-бот. Это анонимно и быстро", "")

    let finalText = edit2.Replace("Наш канал в Telegram. Присоединяйтесь!
Есть о чем рассказать? Пишите в наш телеграм-бот. Это анонимно и быстро
Перепечатка текста и фотографий Onlíner без разрешения редакции запрещена. ng@onliner.by", "")

    return finalText
}

let _getShortText (description:string) = async {
    let textWithoutMarkup = Regex.Replace(input = description, pattern = "<.*?>", replacement = "")
    let finalText = textWithoutMarkup.Replace(oldValue = "Читать далее…", newValue = "")
    return finalText
}

let _getOnlinerArticles () = async {
    let rssResults = feed.GetSample()

    let articles = rssResults.Channel.Items
                    |> PSeq.withDegreeOfParallelism 5
                    |> PSeq.map (fun x -> { title = x.Title;
                                            link = x.Link; 
                                            shortText = _getShortText x.Description |> Async.RunSynchronously;
                                            text = _getOnlinerArticleText x.Link |> Async.RunSynchronously;
                                            pubDate = x.PubDate })
                    |> PSeq.toList
    return articles
}

let GetOnlierArticleText (link:string) = 
    let mytask = Async.StartAsTask(_getOnlinerArticleText(link))
    mytask

let GetOnlinerArticlesAsync () = 
    let mytask = Async.StartAsTask(_getOnlinerArticles())
    mytask