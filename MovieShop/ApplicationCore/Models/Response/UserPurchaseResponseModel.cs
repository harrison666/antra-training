using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models.Response
{
    public class UserPurchaseResponseModel
    {
        public int UserId { get; set; }
        public List<UserPurchasedMovieResponseModel> PurchasedMovies { get; set; }

        public class UserPurchasedMovieResponseModel : MovieResponseModel
        {
            public DateTime PurchaseDateTime { get; set; }
        }
    }
}
