using AutoMapper;
using Bubble.APIServices.Interfaces;
using Bubble.CQRS.Command;
using Bubble.Shared.Models.Request;
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
}
