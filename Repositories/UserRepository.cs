using Api_AIKO.Data;
using Api_AIKO.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api_AIKO.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DoesUserIdExist(string userId)
        {
            return await _context.Users.AnyAsync(u => u.Id == userId);
        }
    }
}
