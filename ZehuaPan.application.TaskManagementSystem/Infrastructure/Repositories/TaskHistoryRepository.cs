using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterface;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class TaskHistoryRepository : EfRepository<TaskHistory>, ITaskHistoryRepository
    {
        public TaskHistoryRepository(TaskManagementSystemDbContext dbContext) : base(dbContext)
        {

        }
    }
}
