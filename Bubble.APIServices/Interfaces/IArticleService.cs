using Bubble.Shared.Models.Request;
using Bubble.Shared.Models.Response;

namespace Bubble.APIServices.Interfaces;
public interface IArticleService
{
    Task AddNewArticlesToDB();
    Task<List<GetArticlesPageAsReaderResponse>> GetArticlesPageAsReaderAsync(GetArticlesPageAsReaderRequest request);
    Task<List<GetArticlesPageAsEditorResponse>> GetArticlesPageAsEditorAsync(GetArticlesPageAsEditorRequest request);
    Task<GetArticleResponse> GetArticleAsync(Guid id);
    Task<int> GetArticlesPagesAmountReaderAsync(GetArticlesPageAsReaderRequest request);
    Task<int> GetArticlesPagesAmountEditorAsync(GetArticlesPageAsEditorRequest request);
    Task RateUnratedArticlesGoodness();
    Task<List<string>> GetArticlesSourcesAsync();
    Task<bool> ChangeArticleApprovalAsync(Guid id);
    Task<int> DeleteArticleAsync(Guid id);
}
