using CinemaApp.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CinemaApp.ViewModel
{
    public class MovieViewModel : ViewModelBase
    {
        private ObservableCollection<Movie> _movies;
        public ObservableCollection<Movie> Movies
        {
            get => _movies;
            set
            {
                _movies = value;
                OnPropertyChanged(nameof(Movies));
            }
        }

        public ICommand SelectMovieCommand { get; }

        public MovieViewModel()
        {
            // Sample data (replace with DB or repo data later)
            Movies = new ObservableCollection<Movie>
            {
                new Movie { Title = "Iron Man 2", PosterPath = "/CinemaApp;component/Images/iron_man_2.jpg" },
                new Movie { Title = "The Revenant", PosterPath = "/CinemaApp;component/Images/revenant.jpg" },
                new Movie { Title = "Fight Club", PosterPath = "/CinemaApp;component/Images/fight_club.jpg" },
                new Movie { Title = "Who Killed Captain Alex", PosterPath = "/CinemaApp;component/Images/alex.jpg" }
            };

            SelectMovieCommand = new ViewModelCommand(ExecuteSelectMovie);
        }

        private void ExecuteSelectMovie(object parameter)
        {
            if (parameter is Movie movie)
            {
                // TODO: open details or show screenings
                System.Windows.MessageBox.Show($"You clicked {movie.Title}");
            }
        }
    }
}
