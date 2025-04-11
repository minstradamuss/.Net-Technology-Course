using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AnimatedSorting
{
    public class Cube
    {
        public int Value { get; }
        public Rectangle Rectangle { get; }
        public Label Label { get; }

        public Cube(int value)
        {
            Value = value;

            Rectangle = new Rectangle
            {
                Width = 40,
                Height = 40,
                Fill = new SolidColorBrush(Color.FromRgb((byte)(value * 10 % 255), 100, 200)),
                Stroke = Brushes.Black,
                RadiusX = 5,
                RadiusY = 5
            };

            Label = new Label
            {
                Content = value.ToString(),
                Width = 40,
                Height = 40,
                HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                Padding = new Thickness(0),
                FontWeight = FontWeights.Bold,
                FontSize = 14
            };
        }
    }
}