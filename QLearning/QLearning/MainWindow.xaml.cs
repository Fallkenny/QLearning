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
            _algorithm = new QLearningAlgorithm();
            //_algorithm.Changed += UpdateMapOnScreen;
            _algorithm.Initialize();
            CompositionTarget.Rendering += CompositionTarget_Rendering; ;
            InitializeComponent();
            this.MapViewer.DrawMap(_algorithm.Map, 12, 10, _algorithm.CurrentState.Id);
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            this.MapViewer.DrawMap(_algorithm.Map, 12, 10, _algorithm.CurrentState.Id);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(()=>_algorithm.RunEpisode());
            //this.MapViewer.DrawMap(_algorithm.Map, 12, 10, _algorithm.CurrentState.Id);
        }

        

        public void UpdateMapOnScreen(int currentState)
        {
            
            //Thread.Sleep(1000);
        }

        public delegate void UpdateMap(int currentState);
    }
}
