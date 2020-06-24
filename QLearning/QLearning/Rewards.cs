using System;
using System.Collections.Generic;
using System.Text;

namespace QLearning
{
    public static class Rewards
    {
        public const int VOID = -1000;
        public const int NORMALSPACE = 1;
        public const int OBSTACLE = -100;
        public const int GOAL = 100;
    }
}
