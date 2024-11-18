using DataAccess.Domain.Models;
using DataAccess.Repositories.Abstract;

namespace DataAccess.Repositories;

public class UserRepository(AppDbContext context) : RepositoryBase<User>(context), IUserRepository
{
    
}