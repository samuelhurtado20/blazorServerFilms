using blazorServerFilms.Data;
using blazorServerFilms.Interfaces;
using blazorServerFilms.Repositories;
using Films.Models;

namespace blazorServerFilms.Service
{
    public class FilmService(SqlConfiguration sqlConfiguration) : IFilmService
    {
        private readonly IFilmRepository _filmRepository = new FilmRepository(sqlConfiguration.ConString);

        Task<Film> IFilmService.AddFilmAsync(Film film)
        {
            return _filmRepository.AddFilmAsync(film);
        }

        Task IFilmService.DeleteFilmAsync(int id)
        {
            return _filmRepository.DeleteFilmAsync(id);
        }

        Task<Film> IFilmService.GetFilmAsync(int id)
        {
            return _filmRepository.GetFilmAsync(id);
        }

        Task<IEnumerable<Film>> IFilmService.GetFilmsAsync()
        {
            return _filmRepository.GetFilmsAsync();
        }

        Task<Film> IFilmService.UpdateFilmAsync(Film film)
        {
            return _filmRepository.UpdateFilmAsync(film);
        }
    }
}