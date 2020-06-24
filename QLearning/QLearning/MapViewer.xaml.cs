using System;
using System.Collections.Generic;
using System.Text;
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
    /// Interaction logic for MapViewer.xaml
    /// </summary>
    public partial class MapViewer : UserControl
    {
        public MapViewer()
        {
            InitializeComponent();
        }

        static int RECTWIDTH = 40;
        static int RECTHEIGHT = 30;

        public void DrawMap(QLearningNode[,] map, int width, int height, int currentPosition)
        {
            int nextX = 0;
            int nextY = 0;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    nextX = j * RECTWIDTH;
                    nextY = i * RECTHEIGHT;
                    var currentNode = map[j, i];
                    var spaceRect = this.GetSpaceRect(currentNode, currentPosition == currentNode.Id);

                    MapCanvas.Children.Add(spaceRect);
                    Canvas.SetTop(spaceRect, nextY);
                    Canvas.SetLeft(spaceRect, nextX);
                }
            }
        }

        private Rectangle GetSpaceRect(QLearningNode qLearningNode, bool currentPosition)
        {
            SolidColorBrush color = Brushes.Transparent;
            if (currentPosition)
                color = Brushes.Blue;
            else
                switch (qLearningNode.Reward)
                {
                    case Rewards.GOAL:
                        color = Brushes.Green;
                        break;
                    case Rewards.NORMALSPACE:
                        color = Brushes.LightGray;
                        break;
                    case Rewards.OBSTACLE:
                        color = Brushes.Black;
                        break;
                    default:
                        break;
                }

            

            return new Rectangle()
            {
                Width = RECTWIDTH,
                Height = RECTHEIGHT,
                Fill = color,
                StrokeThickness = 1,
                Stroke= new SolidColorBrush(Colors.Black),
            };
        }
    }
}
