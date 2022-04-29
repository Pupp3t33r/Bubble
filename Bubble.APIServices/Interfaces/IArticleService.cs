using Bubble.Shared.Models.Request;
using Bubble.Shared.Models.Response;

namespace Bubble.Service.Interfaces;
public interface IArticleService
{
    Task AddNewArticlesToDB();
    Task<List<GetArticlesPageAsReaderResponse>> GetArticlesPageAsReader(GetArticlesPageAsReaderRequest request);
    Task<List<GetArticlesPageAsEditorResponse>> GetArticlesPageAsEditor(GetArticlesPageAsEditorRequest request);
    Task<GetArticleResponse> GetArticle(Guid id);
    Task<int> GetArticlesPagesAmountReader(GetArticlesPageAsReaderRequest request);
    Task RateUnratedArticlesGoodness();
    Task<List<string>> GetArticlesSources();
}
