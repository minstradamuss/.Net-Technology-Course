using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AnimatedSorting
{
    public partial class MainWindow : Window
    {
        private List<Cube> cubes = new List<Cube>();
        private const int CubeSize = 40;
        private const int Spacing = 10;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                string[] lines = File.ReadAllLines(ofd.FileName);
                var numbers = lines.SelectMany(line => line.Split(' ', ',', ';'))
                                   .Where(x => int.TryParse(x, out _))
                                   .Select(int.Parse)
                                   .ToList();
                DisplayCubes(numbers);
            }
        }

        private void DisplayCubes(List<int> values)
        {
            canvas.Children.Clear();
            cubes.Clear();
            for (int i = 0; i < values.Count; i++)
            {
                var cube = new Cube(values[i]);
                cubes.Add(cube);

                double left = i * (CubeSize + Spacing);
                double top = 100;

                Canvas.SetLeft(cube.Rectangle, left);
                Canvas.SetTop(cube.Rectangle, top);
                Canvas.SetLeft(cube.Label, left);
                Canvas.SetTop(cube.Label, top);

                canvas.Children.Add(cube.Rectangle);
                canvas.Children.Add(cube.Label);
            }
        }

        private async void SortButton_Click(object sender, RoutedEventArgs e)
        {
            Sorter sorter = new Sorter(cubes, canvas);
            await sorter.BubbleSortAsync();
        }
    }
}