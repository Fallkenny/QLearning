using System;
using System.Collections.Generic;
using System.Text;

namespace QLearning
{
    public class QLearningNode // representa o nó
    {
        public QLearningNode(int reward, int id, int x, int y)
        {
            Reward = reward;
            Id = id;
            X = x;
            Y = y;
        }
        public int Reward { get; set; }
        public int Id { get; set; }
        public int X { get; }
        public int Y { get; }
    }
}
