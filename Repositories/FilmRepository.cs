using blazorServerFilms.Interfaces;
using Films.Models;
using Microsoft.Data.SqlClient;

namespace blazorServerFilms.Repositories
{
    public class FilmRepository(string conString) : IFilmRepository
    {
        private readonly string _conString = conString;

        protected SqlConnection _sqlConnection => new(_conString);

        public async Task<Film> AddFilmAsync(Film film)
        {
            var db = _sqlConnection;

            string query = "INSERT INTO Films (Title, Year, Director) VALUES (@Title, @Year, @Director); SELECT CAST(SCOPE_IDENTITY() as int);";

            using SqlCommand command = new(query, db);
            command.Parameters.AddWithValue("@Title", film.Title);
            command.Parameters.AddWithValue("@Year", film.Year);
            command.Parameters.AddWithValue("@Director", film.Director);

            db.Open();
            film.Id = await command.ExecuteNonQueryAsync();
            db.Close();
            return film;
        }

        public async Task DeleteFilmAsync(int id)
        {
            var db = _sqlConnection;

            string query = "delete from films where id = @Id";

            using SqlCommand command = new(query, db);
            command.Parameters.AddWithValue("@Id", id);

            db.Open();
            await command.ExecuteNonQueryAsync();
            db.Close();
        }

        public async Task<Film> GetFilmAsync(int id)
        {
            Film film = new();

            try
            {
                using var db = _sqlConnection;
                string query = "SELECT * FROM films WHERE Id = @Id";

                using SqlCommand command = new(query, db);
                command.Parameters.AddWithValue("@Id", id);

                await db.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync()) // Verifica si hay resultados
                {
                    film = new Film
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Title = reader.GetString(reader.GetOrdinal("Title")),
                        Year = reader.GetInt32(reader.GetOrdinal("Year")),
                        Director = reader.GetString(reader.GetOrdinal("Director"))
                    };
                }

                return film;
            }
            catch (Exception)
            {
                throw; // Mantiene la pila de llamadas original
            }
        }

        public async Task<IEnumerable<Film>> GetFilmsAsync()
        {
            var films = new List<Film>();
            var db = _sqlConnection;
            string query = "SELECT * FROM films";

            using SqlCommand command = new(query, db);
            await db.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                films.Add(new Film
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Title = reader.GetString(reader.GetOrdinal("Title")),
                    Year = reader.GetInt32(reader.GetOrdinal("Year")),
                    Director = reader.GetString(reader.GetOrdinal("Director"))
                });
            }

            return films;
        }

        public async Task<Film> UpdateFilmAsync(Film film)
        {
            var db = _sqlConnection;

            string query = "update Films set Title = @title, Year = @year, Director = @director where Id = @id;";

            using SqlCommand command = new(query, db);
            command.Parameters.AddWithValue("@id", film.Id);
            command.Parameters.AddWithValue("@title", film.Title);
            command.Parameters.AddWithValue("@year", film.Year);
            command.Parameters.AddWithValue("@director", film.Director);

            db.Open();
            await command.ExecuteNonQueryAsync();
            db.Close();
            return film;
        }
    }
}