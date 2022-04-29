using Bubble.Shared.Models.Request;

namespace Bubble.APIServices.Interfaces;

public interface ICommentService
{
    Task<int> AddCommentAsync(AddCommentRequest addCommentRequest);
}