using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IPurchaseRepository
    {
        Task<IEnumerable<Purchase>> GetAllPurchases();
        Task<IEnumerable<Purchase>> GetAllPurchasesByMovie(int movieId);
    }
}
