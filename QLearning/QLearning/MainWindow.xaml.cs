using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace QLearning
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        QLearningAlgorithm _algorithm;
        DispatcherTimer _timer;
        private bool _canRun;

        public MainWindow()
        {
            _timer = new DispatcherTimer();
            _timer.Tick += new EventHandler(timer_Tick);
            _timer.Interval = new TimeSpan(1);
            _timer.Start();
            InitializeComponent();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            _timer.Interval = new TimeSpan((int.Parse((_comboTime.SelectedItem as ComboBoxItem).Tag as string)));

            if (_algorithm == null)
            {
                _algorithm = new QLearningAlgorithm();
                _algorithm.Initialize();
                this.MapViewer.DrawMap(_algorithm.Map, 12, 10, _algorithm.CurrentState.Id);
            }

            if (!_canRun)
                return;

            this._episodeLabel.Content = $"Episódio: {_algorithm.Episodes.ToString() }";
            this._movesLabel.Content = $"Movimentos: {_algorithm.Moves.ToString() }";
            var path = _algorithm.BestPath;

            this._bestPathLabel.Content = string.Empty;
            for (int i = 0; i < path.Length; i++)
            {
                if (i == 15)
                    this._bestPathLabel.Content += "\n";
                this._bestPathLabel.Content += $"{path[i]}";
            }
            if (_algorithm.ReachedGlobalMaximum)
                this._bestPathLabel.Foreground = Brushes.Green;
            else
                this._bestPathLabel.Foreground = Brushes.Black;

            _algorithm.DoNextStep();
            this.MapViewer.DrawMap(_algorithm.Map, 12, 10, _algorithm.CurrentState.Id);
        }

        private void play_Click(object sender, RoutedEventArgs e)
        {
            this._canRun = true;
        }

        private void pause_Click(object sender, RoutedEventArgs e)
        {
            this._canRun = false;
        }

        private void restart_Click(object sender, RoutedEventArgs e)
        {
            this._algorithm = null;
            this._canRun = false;
            this._bestPathLabel.Content = string.Empty;
        }

        private void _sliderRandom_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var newValue = (int)e.NewValue;
            _randomValue.Content = $"{newValue}%";

            if (_algorithm != null)
                _algorithm.CurrentRandom = newValue;
        }
    }
}
