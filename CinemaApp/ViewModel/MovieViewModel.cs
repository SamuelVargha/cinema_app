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
                new Movie { Title = "Iron Man 2", PosterPath = "/CinemaApp;component/Images/iron_man_2.jpg", Description = "Tony Stark faces new enemies and confronts his past as Iron Man." },
                new Movie { Title = "The Revenant", PosterPath = "/CinemaApp;component/Images/revenant.jpg", Description = "A frontiersman fights for survival after being left for dead." },
                new Movie { Title = "Fight Club", PosterPath = "/CinemaApp;component/Images/fight_club.jpg", Description = "An office worker forms an underground fight club with a soap maker." },
                new Movie { Title = "Who Killed Captain Alex", PosterPath = "/CinemaApp;component/Images/alex.jpg", Description = "Ugandan action at its finest — Wakaliwood’s best!" }
            };


            SelectMovieCommand = new ViewModelCommand(ExecuteSelectMovie);
        }

        private void ExecuteSelectMovie(object parameter)
        {
            if (parameter is Movie movie)
            {
                var detailsWindow = new CinemaApp.View.MovieDetailsWindow(movie);
                detailsWindow.ShowDialog();
            }
        }
    }
}
