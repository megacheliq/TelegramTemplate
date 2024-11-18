using DataAccess.Domain.Models;
using DataAccess.Repositories.Abstract;
using MediatR;

namespace TelegramTemplate.Application.UseCases.Queries.UserQueries;

public class GetUserByUserIdQuery : IRequest<User?>
{
    public long UserId { get; set; }
}

public class GetUserByUserIdQueryHandler : IRequestHandler<GetUserByUserIdQuery, User?>
{
    private readonly IUserRepository _userRepository;

    public GetUserByUserIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> Handle(GetUserByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.GetByIdAsync(request.UserId);
    }
}