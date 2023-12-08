using System;
using System.Windows;
using System.Windows.Threading;

namespace IClocker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private TimeSpan elapsedTime;
        private DateTime lastStartTime;

        public MainWindow()
        {
            InitializeComponent();
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            timer = new();
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            elapsedTime = DateTime.Now - lastStartTime;
            UpdateElapsedTimeLabel();
        }

        private void UpdateElapsedTimeLabel()
        {
            lblElapsedTime.Content = $"{elapsedTime.Hours:D2}:{elapsedTime.Minutes:D2}:{elapsedTime.Seconds:D2}.{elapsedTime.Milliseconds:D3}";
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            lastStartTime = DateTime.Now;
            timer.Start();
            btnPauseResume.Content = "PAUSE";
        }

        private void PauseResume_Click(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
                btnPauseResume.Content = "RESUME";
            }
            else
            {
                lastStartTime = DateTime.Now - elapsedTime;
                timer.Start();
                btnPauseResume.Content = "PAUSE";
            }
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            elapsedTime = TimeSpan.Zero;
            UpdateElapsedTimeLabel();
            btnPauseResume.Content = "PAUSE";
        }
    }
}
