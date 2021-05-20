using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.RepositoryInterface;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository: EfRepository<User>, IUserRepository
    {
        public UserRepository(TaskManagementSystemDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<User> GetByIdAsync(int id)
        {
            var user = await _dbContext.Users.Include(u => u.Tasks).Include(u => u.TaskHistories).FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                throw new NotFoundException("User Not found");
            }
            return user;
        }
    }
}
