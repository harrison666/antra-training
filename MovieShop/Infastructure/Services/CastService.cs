using ApplicationCore.Entities;
using ApplicationCore.Models.Response;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CastService : ICastService
    {
        private readonly IAsyncRepository<Cast> _castRepository;

        public CastService(IAsyncRepository<Cast> castRepository)
        {
            _castRepository = castRepository;
        }

        public async Task<CastResponseModel> GetCastAsync(int id)
        {
            var cast = await _castRepository.GetByIdAsync(id);
            var response = new CastResponseModel()
            {
                Id = cast.Id,
                Name = cast.Name,
                Gender = cast.Gender,
                ProfilePath = cast.ProfilePath,
                TmdbUrl = cast.TmdbUrl,
            };
            return response;
        }
    }
}
