
using Course.IdentityServer.Data;
using Course.IdentityServer.Dtos;
using Course.IdentityServer.Models;
using Course.IdentityServer.Services.Abstracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.IdentityServer.Services.Concretes
{
    public class GenericService<T>
        : IGenericService<T>
        where T : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;
        private DbSet<T> _dbSet;

        public GenericService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public async Task<Response<bool>> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0
                ? Response<bool>.Success(true, 200)
                : Response<bool>.Fail($"{typeof(T)} Could not created.", 500);
        }

        public async Task<Response<T>> GetByIdAsync(string id)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(x=>x.Id == id);
            return entity != null
                ? Response<T>.Success(entity, 200)
                : Response<T>.Fail($"{typeof(T)} Could not found.", 404);
        }

        public async Task<Response<List<T>>> GetByUserIdAsync(string userId)
        {
            var entity = await _dbSet.Where(x => x.UserId == userId).ToListAsync();
            return entity != null
                ? Response<List<T>>.Success(entity, 200)
                : Response<List<T>>.Fail($"{typeof(T)} Could not found.", 404);
        }

        public async Task<Response<bool>> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0
                ? Response<bool>.Success(true, 200)
                : Response<bool>.Fail($"{typeof(T)} Could not updated.", 500);
        }
    }
}