using CinemaApp.Model;
using System.Windows;
using System.Windows.Input;

namespace CinemaApp.View
{
    public partial class ScreeningsWindow : Window
    {
        public Movie Movie { get; }

        public ScreeningsWindow(Movie movie)
        {
            InitializeComponent();
            Movie = movie;
            DataContext = movie;
        }

        private void Screening_Click(object sender, RoutedEventArgs e)
        {
            var screening = (sender as FrameworkElement)?.DataContext as Screening;
            if (screening == null) return;

            //var seatWindow = new SeatReservationWindow(Movie, screening);
            //seatWindow.Show();
            Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void WindowDrag(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
                DragMove();
        }
    }

}
