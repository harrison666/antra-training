using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterface;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class TaskRepository : EfRepository<ApplicationCore.Entities.Task>, ITaskRepository
    {
        public TaskRepository(TaskManagementSystemDbContext dbContext) : base(dbContext)
        {

        }
    }
}
