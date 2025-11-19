using CinemaApp.Model;
using System.Windows;
using System.Windows.Controls;
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
            Button btn = (Button)sender;
            Screening screening = (Screening)btn.DataContext;

            SeatSelectionWindow win = new SeatSelectionWindow(screening);
            Close();
            win.ShowDialog();
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
