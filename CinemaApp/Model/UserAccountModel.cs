using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Model
{
    public class UserAccountModel
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public byte[] ProfilePicture { get; set; }
        public bool Admin { get; set; }

        private List<Reservation> reservations;

        public UserAccountModel()
        {
            reservations = new List<Reservation>();
        }

        public void AddReservation(Reservation reservation)
        {
            reservations.Add(reservation);
        }

        public List<Reservation> GetReservations()
        {
            return reservations;
        }
    }
}
