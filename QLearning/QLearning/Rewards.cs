using System;
using System.Collections.Generic;
using System.Text;

namespace QLearning
{
    public static class Rewards
    {
        public static int VOID = -1000;
        public static int NORMALSPACE = 1;
        public static int OBSTACLE = -100;
        public static int GOAL = 100;
    }
}
