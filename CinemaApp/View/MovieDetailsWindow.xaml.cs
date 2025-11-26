using CinemaApp.Model;
using System.Windows;

namespace CinemaApp.View
{
    public partial class MovieDetailsWindow : Window
    {

        private Movie _movie;
        private UserAccountModel _userModel;

        public MovieDetailsWindow(UserAccountModel userModel, Movie movie)
        {
            InitializeComponent();
            _userModel = userModel;
            _movie = movie;
            DataContext = movie;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ViewScreenings_Click(object sender, RoutedEventArgs e)
        {
            var screeningsWindow = new ScreeningsWindow(_userModel, _movie);
            screeningsWindow.Show();
            this.Close();
        }

        private void WindowDrag(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
                DragMove();
        }

    }
}
