namespace Bubble.CQS.Handlers.Command;
public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, int>
{
    private readonly NewsDbContext _dbContext;

    public AddCommentCommandHandler(NewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Handle(AddCommentCommand request, CancellationToken cancellationToken)
    {
        var user = (await _dbContext.Users.AsNoTracking()
                        .FirstOrDefaultAsync(x=>x.Name==request.CommentRequest.UserName, cancellationToken));
        if (user is null)
        {
            return 0;
        }
        _dbContext.Comments.Add(
            new()
            {
                ArticleId = request.CommentRequest.ArticleId,
                UserId = user.Id,
                CommentText = request.CommentRequest.CommentText,
                PostTime = DateTime.Now
            });

        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
