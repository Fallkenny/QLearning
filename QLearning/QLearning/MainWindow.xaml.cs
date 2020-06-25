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
        public MainWindow()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(1);
            timer.Start();
            InitializeComponent();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (_algorithm == null)
            {
                _algorithm = new QLearningAlgorithm();
                _algorithm.Initialize();
            }
            this._episodeLabel.Content = $"Episódio: {_algorithm.Episodes.ToString() }";

            _algorithm.DoNextStep();
            this.MapViewer.DrawMap(_algorithm.Map, 12, 10, _algorithm.CurrentState.Id);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _algorithm.CurrentState = _algorithm.StartState;
        }
    }
}
