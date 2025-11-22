using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CinemaApp.Model;
using CinemaApp.View;

namespace CinemaApp.ViewModel
{
    public class ScreeningsViewModel : ViewModelBase
    {
        public ObservableCollection<ScreeningDisplay> AllScreenings { get; set; }

        public ICommand SelectScreeningCommand { get; set; }

        public ScreeningsViewModel(IEnumerable<Movie> movies)
        {
            // Flatten all screenings and sort by date/time
            AllScreenings = new ObservableCollection<ScreeningDisplay>(
                movies.SelectMany(m => m.Screenings.Select(s => new ScreeningDisplay
                {
                    MovieTitle = m.Title,
                    Date = s.Date,
                    Time = s.Time,
                    Hall = s.Hall,
                    OriginalScreening = s
                }))
                .OrderBy(s => DateTime.Parse(s.Date + " " + s.Time))
            );

            SelectScreeningCommand = new RelayCommand<ScreeningDisplay>(screening =>
            {
                var win = new SeatSelectionWindow(screening.OriginalScreening);
                win.Show();
            });
        }
    }

    public class ScreeningDisplay
    {
        public string MovieTitle { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Hall { get; set; }

        public Screening OriginalScreening { get; set; }
    }
}
