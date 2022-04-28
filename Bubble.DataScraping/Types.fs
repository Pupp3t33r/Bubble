namespace Bubble.Datascrapper.Types

type ArticleRecord = 
    {
        title : string
        text : string
        shortText : string
        link: string
        pubDate : System.DateTimeOffset
    }