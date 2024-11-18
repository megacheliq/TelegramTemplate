using DataAccess.Domain.Models;
using DataAccess.Repositories.Abstract;
using MediatR;

namespace TelegramTemplate.Application.UseCases.Commands.UserCommands;

public class CreateUserCommand : IRequest<User>
{
    public required long UserId { get; set; }
    
    public string? Username { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userToCreate = new User { Id = request.UserId, Username = request.Username };
        return await _userRepository.AddAsync(userToCreate);
    }
}