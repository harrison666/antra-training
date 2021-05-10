using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : EfRepository<User>, IUserRepository
    {
        public UserRepository(MovieShopDbContext dbContext): base(dbContext)
        {

        }

        public async Task<IEnumerable<Movie>> GetPurchasesByIdAsync(int id)
        {
            var totalPurchaseCountByUser = await _dbContext.Purchases.Where(p => p.Id == id).Select(p => p.Movie).CountAsync();

            if (totalPurchaseCountByUser == 0)
            {
                throw new Exception("NO purchases found for this user");
            }
            //.Include(p => p.Movie)
            var movies = await _dbContext.Purchases.Where(p => p.Id == id).Select(p => p.Movie).ToListAsync();
            return movies;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }
    }
}
