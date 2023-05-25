using MovieBookingApplication.BookingModels;

namespace MovieBookingApplication.BookingRepositories.Interfaces
{
    public interface IMovieInterface
    {
        List<Movie> Get();
        Movie Get(string id);
        Movie Create(Movie movie);
        void Update(string id, Movie movie);
        void Delete(string id);

        Movie Exists(string movie, string theatre);
        Movie GetMovie(string id);
    }
}
