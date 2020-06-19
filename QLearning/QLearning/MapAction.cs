using System;
using System.Collections.Generic;
using System.Text;

namespace QLearning
{
    class MapAction
    {
        public MapAction(eDirection direction)
        {
            Direction = direction;
        }

        public double Reward { get; set; } = 0;

        public eDirection Direction { get; set; }
    }
}
