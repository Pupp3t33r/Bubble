using AutoMapper;
using Bubble.APIServices.Interfaces;
using Bubble.APIServices.JsonModels;
using Bubble.Data.Entities;
using Bubble.CQS.Command;
using Bubble.CQS.Query;
using Bubble.Shared.Models.Request;
using Bubble.Shared.Models.Response;
using MediatR;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Bubble.APIServices.Services;
public class ArticleService : IArticleService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public ArticleService(IMediator mediator, IMapper mapper)
    {
        (_mediator, _mapper) = (mediator, mapper);
    }

    public async Task AddNewArticlesToDB()
    {
        var articleRecords = (await OnlinerScraper.GetOnlinerArticlesAsync()).ToList();
        var articles = _mapper.Map<List<Article>>(articleRecords);
        var dbUrls = await GetAllArticleUrls();
        List<Article> articlesToWrite = new();
        foreach (var article in articles)
        {
            if (!dbUrls.Any(url => url == article.SourceURL))
            {
                article.Source = "Onliner";
                article.Approved = false;
                articlesToWrite.Add(article);
            }
        }
        await _mediator.Send(new AddNewArticlesCommand { ArticlesToWrite = articlesToWrite });
    }

    public async Task RateUnratedArticlesGoodness()
    {
        var WordRatings = GetAFINN();

        var articlesToRate = await _mediator.Send(new GetFirst5UnratedArticlesQuery());

        foreach (var article in articlesToRate)
        {
            var preparedText = article.ArticleText.Replace("\n", " ");

            var words = await GetLemmasFromAPI(preparedText);

            words = PrepareWordsToRate(words);

            var overallRating = RateArticle(words, WordRatings);

            _ = await _mediator.Send(new RateArticleGoodnessCommand { Id = article.Id, GoodnessRating = overallRating });
        }
    }

    public async Task<List<GetArticlesPageAsReaderResponse>> GetArticlesPageAsReaderAsync(GetArticlesPageAsReaderRequest request)
    {
        var articles = await _mediator.Send(new GetArticlesPageAsReaderQuery { ArticlesRequest = request });
        return _mapper.Map<List<GetArticlesPageAsReaderResponse>>(articles);
    }
    public async Task<List<GetArticlesPageAsEditorResponse>> GetArticlesPageAsEditorAsync(GetArticlesPageAsEditorRequest request)
    {
        var articles = await _mediator.Send(new GetArticlesPageAsEditorQuery { filters = request });
        return _mapper.Map<List<GetArticlesPageAsEditorResponse>>(articles);
    }

    public async Task<GetArticleResponse> GetArticleAsync(Guid id)
    {
        var article = await _mediator.Send(new GetArticleQuery { ArticleId = id });
        return _mapper.Map<GetArticleResponse>(article);
    }

    public async Task<int> GetArticlesPagesAmountReaderAsync(GetArticlesPageAsReaderRequest request)
    {
        return await _mediator.Send(new GetArticlesPagesAmountReaderQuery { filters = request });
    }

    public async Task<int> GetArticlesPagesAmountEditorAsync(GetArticlesPageAsEditorRequest request)
    {
        return await _mediator.Send(new GetArticlesPagesAmountEditorQuery { filters = request });
    }

    public async Task<List<string>> GetArticlesSourcesAsync()
    {
        return await _mediator.Send(new GetArticlesSourcesQuery());
    }

    public async Task<bool> ChangeArticleApprovalAsync(Guid id)
    {
        return await _mediator.Send(new ChangeArticleApprovalCommand() { ArticleId = id });
    }

    public async Task<int> DeleteArticleAsync(Guid id)
    {
        return await _mediator.Send(new DeleteArticleCommand() { ArticleId = id });
    }

    private async Task<List<string>> GetAllArticleUrls()
    {
        return await _mediator.Send(new GetAllArticlesUrlQuery());
    }

    private Dictionary<string, int?> GetAFINN()
    {
        var buildDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var filePath = buildDir + @"\AFINN-165-RU.json";
        Dictionary<string, int?> WordRatings;

        using (var jsonPath = File.Open(filePath, FileMode.Open))
        {
            return WordRatings = JsonSerializer.Deserialize<Dictionary<string, int?>>(jsonPath);
        }
    }

    private async Task<List<string>> GetLemmasFromAPI(string textToLemma)
    {
        HttpResponseMessage? response;

        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var request = new HttpRequestMessage(HttpMethod.Post, "http://api.ispras.ru/texterra/v1/nlp?targetType=lemma&apikey=3772a07bb9c09957d64b80479c8cb5d56717dd58")
            {
                Content = new StringContent("[ {\"text\":\"" + textToLemma + "\"} ]",

                    Encoding.UTF8,
                    "application/json")
            };
            response = await httpClient.SendAsync(request);
        }

        var responseString = await response.Content.ReadAsStringAsync();

        responseString = responseString.Remove(0, 1);
        responseString = responseString.Remove(responseString.Length - 1, 1);

        var deserializedResponse = JsonSerializer.Deserialize<Root>(responseString);

        var words = deserializedResponse.Annotations.Lemma.Select(x => x.Value).ToList();

        return words;
    }

    private List<string> PrepareWordsToRate(List<string> words)
    {
        words.RemoveAll(x => x == "");

        Parallel.For(0, words.Count,
                i =>
                    {
                        if (words[i] == "не")
                        {
                            words[i + 1] = "не " + words[i + 1];
                        }
                    });
        words.RemoveAll(x => x == "не");
        return words;
    }

    private int RateArticle(List<string> words, Dictionary<string, int?> WordRatings)
    {
        int overallRating = 0;

        Parallel.ForEach(words, word =>
        {
            if (WordRatings.TryGetValue(word, out int? rating))
                Interlocked.Add(ref overallRating, rating.Value);
        });

        return overallRating;
    }
}
