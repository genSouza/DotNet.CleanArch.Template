using DotNet.CleanArch.Template.Domain.Common.Repositories;
using DotNet.CleanArch.Template.Domain.Entities;
using DotNet.CleanArch.Template.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace DotNet.CleanArch.Template.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        if (email == null) throw new ArgumentNullException(nameof(email), "Email cannot be null.");
        
        var user = await _context.Users
            .AsNoTracking()
            .Where(u => u.Email.ToLower() == email.ToLower()) 
            .FirstOrDefaultAsync();
        
        return user;
    }
}
