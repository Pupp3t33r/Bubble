using Bubble.Data.Entities;
using Bubble.Shared.Models.Request;
using Bubble.Shared.Models.Response;

namespace Bubble.APIServices.Interfaces;

public interface ICommentService
{
    Task<int> AddCommentAsync(AddCommentRequest addCommentRequest);
    Task<List<GetCommentsResponse>> GetCommentsForArticleAsync(Guid ArticleId, CancellationToken cancellationToken);
}