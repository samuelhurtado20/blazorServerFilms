﻿using Films.Models;

namespace blazorServerFilms.Interfaces;

public interface IFilmRepository
{
    Task<IEnumerable<Film>> GetFilmsAsync();

    Task<Film> GetFilmAsync(int id);

    Task<Film> AddFilmAsync(Film film);

    Task<Film> UpdateFilmAsync(Film film);

    Task DeleteFilmAsync(int id);
}