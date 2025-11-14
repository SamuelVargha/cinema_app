using CinemaApp.Model;
using System.Collections.Generic;
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
                new Movie
                {
                    Title = "Iron Man 2",
                    PosterPath = "/CinemaApp;component/Images/iron_man_2.jpg",
                    Description = "Tony Stark faces growing pressure from the government, rivals, and his own declining health as he continues to operate as Iron Man, leading to new alliances and dangerous enemies.",
                    Screenings = new List<Screening>
                    {
                        new Screening { Date = "2025-03-01", Time = "18:30", Hall = "Hall 1" },
                        new Screening { Date = "2025-03-01", Time = "21:00", Hall = "Hall 2" },
                    }
                },

                new Movie
                {
                    Title = "The Revenant",
                    PosterPath = "/CinemaApp;component/Images/revenant.jpg",
                    Description = "A frontiersman fights to survive in the brutal wilderness after being left for dead, pushing himself through extreme conditions driven by determination and resilience.",
                    Screenings = new List<Screening>
                    {
                        new Screening { Date = "2025-03-02", Time = "17:00", Hall = "Hall 1" },
                        new Screening { Date = "2025-03-03", Time = "20:30", Hall = "Hall 2" },
                    }
                },
                new Movie
                {
                    Title = "Fight Club",
                    PosterPath = "/CinemaApp;component/Images/fight_club.jpg",
                    Description = "A disillusioned office worker forms an underground fight club with a mysterious stranger, unraveling into a dark exploration of identity, rebellion, and modern masculinity.",
                    Screenings = new List<Screening>
                    {
                        new Screening { Date = "2025-03-02", Time = "17:00", Hall = "Hall 1" },
                        new Screening { Date = "2025-03-03", Time = "20:30", Hall = "Hall 2" },
                    }
                },
                new Movie
                {
                    Title = "Who Killed Captain Alex",
                    PosterPath = "/CinemaApp;component/Images/alex.jpg",
                    Description = "Uganda’s first action movie follows a chaotic mission to uncover the fate of a special forces commander, blending over-the-top action, humor, and the unforgettable commentary of VJ Emmie.",
                    Screenings = new List<Screening>
                    {
                        new Screening { Date = "2025-03-02", Time = "17:00", Hall = "Hall 2" },
                        new Screening { Date = "2025-03-03", Time = "20:30", Hall = "Hall 1" },
                        new Screening { Date = "2025-03-02", Time = "17:00", Hall = "Hall 2" },
                        new Screening { Date = "2025-03-03", Time = "20:30", Hall = "Hall 1" },
                        new Screening { Date = "2025-03-02", Time = "17:00", Hall = "Hall 2" },
                        new Screening { Date = "2025-03-03", Time = "20:30", Hall = "Hall 1" },
                        new Screening { Date = "2025-03-02", Time = "17:00", Hall = "Hall 2" },
                        new Screening { Date = "2025-03-03", Time = "20:30", Hall = "Hall 1" },
                    }
                }
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
