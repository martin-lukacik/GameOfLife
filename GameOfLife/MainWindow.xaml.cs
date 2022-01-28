using System;
using System.Diagnostics;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GameOfLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Game game = new Game();

        Timer gameTimer = new Timer();

        int targetFPS = 60;
        long currentFPS = 0;

        Stopwatch fpsWatch = Stopwatch.StartNew();
        Label fpsCounter = new Label();
        Slider fpsSlider = new Slider();

        public MainWindow()
        {
            InitializeComponent();

            game.LoadFile(@"pattern.txt");
            game.UpdateUI(canvas);

            InitFPSCounter();
            UpdateFPSCounter();

            StartTicking();
        }

        private void InitFPSCounter()
        {
            canvas.Children.Add(fpsCounter);
            canvas.Children.Add(fpsSlider);

            fpsCounter.Foreground = Brushes.White;
            fpsCounter.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

            fpsSlider.Value = targetFPS;
            fpsSlider.Minimum = 1;
            fpsSlider.Maximum = 144;
            fpsSlider.Width = 100;
            fpsSlider.TickFrequency = 1;
            fpsSlider.ValueChanged += FpsSlider_ValueChanged;

            Canvas.SetLeft(fpsSlider, fpsCounter.DesiredSize.Width * 7);
            Canvas.SetTop(fpsSlider, 5);
        }

        private void UpdateFPSCounter()
        {
            fpsWatch.Stop();
            currentFPS = fpsWatch.ElapsedMilliseconds;
            fpsWatch = Stopwatch.StartNew();

            fpsCounter.Content = "Set FPS\t" + targetFPS + "\nFPS\t" + Math.Ceiling(1000.0 / currentFPS);
        }

        private void FpsSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            targetFPS = (int) fpsSlider.Value;
            gameTimer.Interval = 1000 / targetFPS;
            UpdateFPSCounter();
        }

        private void StartTicking()
        {
            gameTimer.Elapsed += Tick;
            gameTimer.Interval = 1000 / targetFPS;
            gameTimer.Start();
        }

        private void Tick(object sender, ElapsedEventArgs e)
        {
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
            double cellSize = Math.Ceiling(ActualWidth / game.Columns);

            game.SetCellSize(cellSize, padding: 0.0);
        }
    }
}
