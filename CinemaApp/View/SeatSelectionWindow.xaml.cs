using CinemaApp.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Linq;

namespace CinemaApp.View
{
    public partial class SeatSelectionWindow : Window
    {
        private Screening _screening;

        // Selected seats by user
        private HashSet<string> selectedSeats = new HashSet<string>();

        public SeatSelectionWindow(Screening screening)
        {
            InitializeComponent();
            _screening = screening;

            // Ensure reserved seats list always exists
            if (_screening.ReservedSeats == null)
                _screening.ReservedSeats = new HashSet<string>();

            BuildSeats();
        }

        private void WindowDrag(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BuildSeats()
        {
            SeatGrid.ItemsSource = null;

            int rows = _screening.Hall == "Hall 1" ? 5 : 3;
            int cols = 10;

            List<UIElement> rowPanels = new List<UIElement>();

            for (int r = 0; r < rows; r++)
            {
                string rowLetter = ((char)('A' + r)).ToString();

                StackPanel row = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, _screening.Hall == "Hall 1" ? 8 : 4, 0, 0)
                };

                for (int c = 1; c <= cols; c++)
                {
                    string seatId = rowLetter + c;

                    bool isReserved = _screening.ReservedSeats.Contains(seatId);

                    Border seat = new Border
                    {
                        Width = 40,
                        Height = 40,
                        CornerRadius = new CornerRadius(8),
                        Background = new SolidColorBrush(
                            isReserved ? Colors.Red : Color.FromRgb(61, 61, 64)
                        ),
                        Margin = new Thickness(4),
                        Tag = seatId,
                        Child = new TextBlock
                        {
                            Text = seatId,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            Foreground = Brushes.White,
                            FontSize = 14,
                            FontWeight = FontWeights.Bold
                        }
                    };

                    if (!isReserved)
                    {
                        seat.MouseLeftButtonDown += Seat_Click;
                        seat.MouseEnter += Seat_HoverEnter;
                        seat.MouseLeave += Seat_HoverLeave;
                    }

                    row.Children.Add(seat);
                }

                rowPanels.Add(row);
            }

            SeatGrid.ItemsSource = rowPanels;
        }

        private void Seat_HoverEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Border seat = sender as Border;

            if (selectedSeats.Contains((string)seat.Tag))
                return;

            AnimateSeat(seat, Colors.MediumSlateBlue, 1.08);
        }

        private void Seat_HoverLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Border seat = sender as Border;

            if (selectedSeats.Contains((string)seat.Tag))
                return;

            AnimateSeat(seat, Color.FromRgb(61, 61, 64), 1.0);
        }

        private void Seat_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Border seat = sender as Border;
            string id = (string)seat.Tag;

            if (selectedSeats.Contains(id))
            {
                selectedSeats.Remove(id);
                AnimateSeat(seat, Color.FromRgb(34, 34, 34), 1.0);
            }
            else
            {
                selectedSeats.Add(id);
                AnimateSeat(seat, Colors.Orange, 1.12);
            }
        }

        private void AnimateSeat(Border seat, Color color, double scale)
        {
            var anim = new ColorAnimation
            {
                To = color,
                Duration = TimeSpan.FromMilliseconds(150)
            };

            ((SolidColorBrush)seat.Background).BeginAnimation(SolidColorBrush.ColorProperty, anim);

            var scaleTransform = seat.RenderTransform as ScaleTransform;
            if (scaleTransform == null)
            {
                scaleTransform = new ScaleTransform(1, 1);
                seat.RenderTransform = scaleTransform;
                seat.RenderTransformOrigin = new Point(0.5, 0.5);
            }

            var scaleAnim = new DoubleAnimation
            {
                To = scale,
                Duration = TimeSpan.FromMilliseconds(150)
            };

            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnim);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnim);
        }

        private void Reserve_Click(object sender, RoutedEventArgs e)
        {
            // Save selected seats as reserved
            foreach (var seat in selectedSeats)
            {
                if (!_screening.ReservedSeats.Contains(seat))
                    _screening.ReservedSeats.Add(seat);
            }

            MessageBox.Show(
                "Reserved seats:\n" + string.Join(", ", selectedSeats),
                "Success",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );

            Close();
        }
    }
}
