using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace AnimatedSorting
{
    public class Sorter
    {
        private readonly List<Cube> cubes;
        private readonly Canvas canvas;
        private const int CubeSize = 40;
        private const int Spacing = 10;
        private const int Duration = 500;

        public Sorter(List<Cube> cubes, Canvas canvas)
        {
            this.cubes = cubes;
            this.canvas = canvas;
        }

        public async Task BubbleSortAsync()
        {
            int n = cubes.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (cubes[j].Value > cubes[j + 1].Value)
                    {
                        await SwapCubesAsync(j, j + 1);
                    }
                }
            }
        }

        private async Task SwapCubesAsync(int i, int j)
        {
            var cubeA = cubes[i];
            var cubeB = cubes[j];

            double posA = Canvas.GetLeft(cubeA.Rectangle);
            if (double.IsNaN(posA)) posA = 0;

            double posB = Canvas.GetLeft(cubeB.Rectangle);
            if (double.IsNaN(posB)) posB = 0;

            var animA = new DoubleAnimation(posB, TimeSpan.FromMilliseconds(Duration));
            var animB = new DoubleAnimation(posA, TimeSpan.FromMilliseconds(Duration));

            cubeA.Rectangle.BeginAnimation(Canvas.LeftProperty, animA);
            cubeB.Rectangle.BeginAnimation(Canvas.LeftProperty, animB);
            cubeA.Label.BeginAnimation(Canvas.LeftProperty, animA);
            cubeB.Label.BeginAnimation(Canvas.LeftProperty, animB);

            await Task.Delay(Duration);

            cubes[i] = cubeB;
            cubes[j] = cubeA;
        }
    }
}
