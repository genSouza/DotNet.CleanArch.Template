using DotNet.CleanArch.Template.Domain.Common.Repositories;
using DotNet.CleanArch.Template.Domain.Entities;
using DotNet.CleanArch.Template.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;


namespace DotNet.CleanArch.Template.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly ApplicationDbContext _context;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(T entity)
    {
        try
        {

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            // Log the exception or handle it as needed
            return false;
        }
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        var result = await _context.Set<T>().AsNoTracking().ToListAsync();
        return result ?? [];
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        var result = await _context.Set<T>().FindAsync(id);

        return result;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity;

    }
}
