using CinemaApp.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CinemaApp.ViewModel
{
    public class MovieViewModel : ViewModelBase
    {
        private ObservableCollection<Movie> _movies;

        private UserModel _userModel;

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

        public MovieViewModel(UserModel userModel, ObservableCollection<Movie> movies)
        {
            // Sample data (replace with DB or repo data later)


            _movies = movies;
            _userModel = userModel;
            SelectMovieCommand = new ViewModelCommand(ExecuteSelectMovie);
        }

        private void ExecuteSelectMovie(object parameter)
        {
            if (parameter is Movie movie)
            {
                var detailsWindow = new CinemaApp.View.MovieDetailsWindow(_userModel, movie);
                detailsWindow.ShowDialog();
            }
        }
    }
}
