using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CinemaApp.Model;

namespace CinemaApp.ViewModel
{
    public class ReservationsViewModel : ViewModelBase
    {
        public ObservableCollection<ReservationDisplay> Reservations { get; set; }

        private UserAccountModel _currentUser;
        private IEnumerable<UserAccountModel> _users;

        public bool IsAdmin { get; }

        public ReservationsViewModel(UserAccountModel currentUser, IEnumerable<UserAccountModel> allUsers)
        {
            _currentUser = currentUser;
            _users = allUsers;

            // Determine if current user is admin
            IsAdmin = allUsers.First(u => u.Username == currentUser.Username).Admin;

            var list = new List<ReservationDisplay>();

            foreach (var user in allUsers)
            {
                foreach (var r in user.GetReservations())
                {
                    list.Add(new ReservationDisplay
                    {
                        ReservationId = r.ID,
                        Date = r.Screening.Date,
                        Time = r.Screening.Time,
                        Hall = r.Screening.Hall,
                        MovieName = r.Screening.MovieTitle, // You should add MovieTitle to Screening class
                        Username = user.Username,
                        IsAdmin = IsAdmin
                    });
                }
            }

            // Filter if not admin
            if (!IsAdmin)
                list = list.Where(r => r.Username == _currentUser.Username).ToList();

            Reservations = new ObservableCollection<ReservationDisplay>(
                list.OrderBy(r => r.Date).ThenBy(r => r.Time)
            );
        }
    }

    public class ReservationDisplay
    {
        public string ReservationId { get; set; }
        public string MovieName { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Hall { get; set; }

        public string Username { get; set; } // Only visible for admins
        public bool IsAdmin { get; set; }
    }
}
