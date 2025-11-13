using CinemaApp.Model;
using System.Windows;

namespace CinemaApp.View
{
    public partial class MovieDetailsWindow : Window
    {
        public MovieDetailsWindow(Movie movie)
        {
            InitializeComponent();
            DataContext = movie;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ViewScreenings_Click(object sender, RoutedEventArgs e)
        {
            // In future: navigate to screening view
            MessageBox.Show($"Navigating to screenings for {((Movie)DataContext).Title}",
                            "CinemaApp", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void WindowDrag(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
                DragMove();
        }

    }
}
