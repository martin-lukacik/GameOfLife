using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GameOfLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Game game = new Game();

        int targetFPS = 5;
        long currentFPS = 0;


        Label fpsCounter = new Label();
        Slider fpsSlider = new Slider();

        public MainWindow()
        {
            InitializeComponent();

            game.LoadFile(@"\Patterns\pattern02.txt");

            game.UpdateUI(canvas);

            fpsCounter.Foreground = Brushes.White;
            canvas.Children.Add(fpsCounter);

            fpsSlider.Value = targetFPS;
            fpsSlider.Minimum = 1;
            fpsSlider.Maximum = 144;
            fpsSlider.Width = 100;
            fpsSlider.TickFrequency = 1;
            fpsSlider.ValueChanged += FpsSlider_ValueChanged;

            fpsCounter.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            Canvas.SetLeft(fpsSlider, fpsCounter.DesiredSize.Width * 7);
            Canvas.SetTop(fpsSlider, 5);
            canvas.Children.Add(fpsSlider);

            StartTicking();
            UpdateFPSCounter();
        }

        private void UpdateFPSCounter()
        {
            fpsCounter.Content = "Set FPS\t" + targetFPS + "\nFPS\t" + Math.Ceiling(1000.0 / currentFPS);
        }

        private void FpsSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            targetFPS = (int) fpsSlider.Value;
            timer.Interval = 1000 / targetFPS;
            UpdateFPSCounter();
        }

        Timer timer = new Timer();
        private void StartTicking()
        {
            timer.Elapsed += Tick;
            timer.Interval = 1000 / targetFPS;
            timer.Start();
        }

        System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
        private void Tick(object sender, ElapsedEventArgs e)
        {
            watch.Stop();
            currentFPS = watch.ElapsedMilliseconds;
            watch = System.Diagnostics.Stopwatch.StartNew();
            try {
                Dispatcher.Invoke(() => {
                    UpdateFPSCounter();

                    game.UpdateNeighbourCounts();

                    game.NextGeneration();
                });
            } catch (Exception) { }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double cellSize = Math.Floor(ActualWidth / game.Columns);

            game.SetCellSize(cellSize, padding: 0.0);
        }
    }
}
