using AutoMapper;
using Bubble.APIServices.Interfaces;
using Bubble.CQS.Command;
using Bubble.CQS.Query;
using Bubble.Data.Entities;
using Bubble.Shared.Models.Request;
using Bubble.Shared.Models.Response;
using MediatR;

namespace Bubble.APIServices.Services;
public class CommentService: ICommentService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public CommentService(IMediator mediator, IMapper mapper)
    {
        (_mediator, _mapper) = (mediator, mapper);
    }

    public async Task<int> AddCommentAsync(AddCommentRequest addCommentRequest)
    {
        return await _mediator.Send(new AddCommentCommand { CommentRequest = addCommentRequest });
    }

    public async Task<List<GetCommentsResponse>> GetCommentsForArticleAsync(Guid ArticleId, CancellationToken cancellationToken)
    {
        var comments = await _mediator.Send(new GetCommentsForArticleQuery { ArticleId = ArticleId }, cancellationToken);
        return _mapper.Map<List<GetCommentsResponse>>(comments);
    }
}
