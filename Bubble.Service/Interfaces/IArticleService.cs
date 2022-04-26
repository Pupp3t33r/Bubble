using Bubble.Shared.Models.Request;
using Bubble.Shared.Models.Response;

namespace Bubble.Service.Interfaces;
public interface IArticleService
{
    Task AddNewArticlesToDB();
    Task<List<GetArticlesAsReaderResponse>> GetArticlesPageAsReader(GetArticlesPageAsReaderRequest request);
}
