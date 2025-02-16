using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace MovieCatalog.ViewModels;

public class MovieListViewModel : ObservableObject
{
    public ObservableCollection<MovieViewModel> Movies { get; set; }

    // Campo privado para SelectedMovie
    private MovieViewModel _selectedMovie;

    // Propiedad pública SelectedMovie con notificación de cambio
    public MovieViewModel SelectedMovie
    {
        get => _selectedMovie;
        set => SetProperty(ref _selectedMovie, value);
    }

    public MovieListViewModel() =>
        Movies = new ObservableCollection<MovieViewModel>();

    public async Task RefreshMovies()
    {
        IEnumerable<Models.Movie> moviesData = await Models.MoviesDatabase.GetMovies();

        foreach (Models.Movie movie in moviesData)
            Movies.Add(new MovieViewModel(movie));
    }

    public void DeleteMovie(MovieViewModel movie) =>
        Movies.Remove(movie);
}
