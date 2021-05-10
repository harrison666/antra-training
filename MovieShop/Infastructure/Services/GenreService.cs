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
    public class GenreService : IGenreService
    {
        private readonly IAsyncRepository<Genre> _genreRepository;

        public GenreService(IAsyncRepository<Genre> genreRepository)
        {
            _genreRepository = genreRepository;
        }
        public async Task<IEnumerable<GenreResponseModel>> GetAllGenres()
        {
            var genres = await _genreRepository.ListAllAsync();

            var genresList = new List<GenreResponseModel>();
            foreach (var genre in genres)
            {
                genresList.Add(new GenreResponseModel { Id = genre.Id, Name = genre.Name });
            }

            return genresList;
        }
    }
}
