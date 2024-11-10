using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

namespace WPF03
{

    public partial class MainWindow : Window
    {
        Timer timer;

        private DispatcherTimer timer2 = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(1),
            IsEnabled = true,
        };
        public MainWindow()
        {
            InitializeComponent();
            slider.Value = 0.5;
            player.Volume = slider.Value;
            timer2.Tick += Timer_Tick;

            textBlock.Text = Path.GetFileName(player.Source.ToString());

        }
        private void Timer_Tick(object? sender, EventArgs e)
        {
            position.Value = player.Position / player.NaturalDuration.TimeSpan;
        }

        private void position_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            player.Position = position.Value * player.NaturalDuration.TimeSpan;
            timer2.Start();
        }

        private void position_DragStarted(object sender, DragStartedEventArgs e)
        {
            timer2.Stop();
        }
        private void ButtonPlay_Pause_Click(object sender, RoutedEventArgs e)
        {
            if ((string)buttonPlay.Content == "Play")
            {
                player.Play();
                buttonPlay.Content = "Pause";
            }
            else
            {
                player.Pause();
                buttonPlay.Content = "Play";
            }
        }

        private void volume_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = true;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = false;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (popup.IsOpen)
                player.Volume = slider.Value;
        }

        private void player_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show("Не удаётся воспроизвести файл. Попробуйте ввести другой путь");
        }

    }
}
