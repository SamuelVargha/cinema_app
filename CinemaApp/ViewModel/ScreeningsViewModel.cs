using CinemaApp.Model;
using System.Collections.ObjectModel;

namespace CinemaApp.ViewModel
{
    public class ScreeningsViewModel : ViewModelBase
    {
        public string MovieTitle { get; }
        public ObservableCollection<Screening> Screenings { get; }

        public ScreeningsViewModel(string movieTitle)
        {
            MovieTitle = movieTitle;

            // TODO: Replace with your database screenings
            Screenings = new ObservableCollection<Screening>
            {
                new Screening { MovieTitle = movieTitle, Date = "2025-03-01", Time = "18:30", Hall = "Hall 1" },
                new Screening { MovieTitle = movieTitle, Date = "2025-03-01", Time = "21:00", Hall = "Hall 2" },
                new Screening { MovieTitle = movieTitle, Date = "2025-03-02", Time = "17:00", Hall = "Hall 1" }
            };
        }
    }
}
