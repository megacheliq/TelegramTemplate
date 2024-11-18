using DataAccess.Domain.Models;
using DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class MessageRepository(AppDbContext context) : RepositoryBase<Message>(context), IMessageRepository
{
    public async Task<Message?> GetMessageByIdInChatAsync(int idInChat)
    {
        return await Context.Messages
            .Where(m => m.IdInChat == idInChat)
            .FirstOrDefaultAsync();
    }
}