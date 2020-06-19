using System;
using System.Collections.Generic;
using System.Text;

namespace QLearning
{
    class Scenario
    {
        public Scenario()
        {
            this.PopulateMapRewards();
        }

        public int MapHeight { get; set; } = 10;

        public int MapWidth { get; set; } = 12;

        public QLearningNode StartPosition { get; set; }

        public QLearningNode EndPosition { get; set; }

        public QLearningNode[,] Map { get; set; } = new QLearningNode[12, 10];

        public void PopulateMapRewards()
        {
            int index = 1;
            for (int j = 0; j < MapHeight; j++)
                for (int i = 0; i < MapWidth; i++)
                    Map[i, j] = new QLearningNode(Rewards.NORMALSPACE, index++, i, j);

            Map[4, 0].Reward =
            Map[11, 0].Reward =
            Map[1, 1].Reward =
            Map[11, 1].Reward =
            Map[0, 2].Reward =
            Map[2, 2].Reward =
            Map[6, 2].Reward =
            Map[8, 2].Reward =
            Map[9, 2].Reward =
            Map[1, 3].Reward =
            Map[8, 3].Reward =
            Map[6, 5].Reward =
            Map[6, 8].Reward = Rewards.OBSTACLE;

            Map[11, 4].Reward = Rewards.GOAL;
            this.EndPosition = Map[11, 4];

            this.StartPosition = Map[4, 9];

            for (int y = 5; y < 10; y++)
                for (int x = 0; x < 4; x++)
                    Map[x, y].Reward = Rewards.VOID;

            for (int x = 8; x < 12; x++)
                for (int y = 5; y < 10; y++)
                    Map[x, y].Reward = Rewards.VOID;
        }
    }
}
