using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QLearning
{
    class QLearningAlgorithm
    {
        public QLearningNode CurrentState { get; private set; }
        public int GoalID { get; private set; }
        Dictionary<int, List<MapAction>> QTable { get; set; }

        public Random Random { get; set; } = new Random();
        public QLearningNode[,] Map;

        

        public void Initialize()
        {
            var scenario = new Scenario();
            QTable = new Dictionary<int, List<MapAction>>();
            for (int y = 0; y < scenario.MapHeight; y++)
                for (int x = 0; x < scenario.MapWidth; x++)
                {
                    var id = scenario.Map[x, y].Id;

                    var actions = new List<MapAction>();
                    if (x > 0)
                        actions.Add(new MapAction(eDirection.Left));
                    if (x < scenario.MapWidth - 1)
                        actions.Add(new MapAction(eDirection.Right));
                    if (y > 0)
                        actions.Add(new MapAction(eDirection.Up));
                    if (y < scenario.MapHeight - 1)
                        actions.Add(new MapAction(eDirection.Down));

                    QTable.Add(id, actions);
                }
            this.Map = scenario.Map;
            this.CurrentState = scenario.StartPosition;
        }

        public void RunEpisode()
        {
            var performedDirections = new List<eDirection>();
            do
            {
                var possibleActions = QTable[CurrentState.Id];
                var allActionsHaveSameReward = possibleActions.Select(action => action.Reward).Distinct().Count() == 1;
                MapAction action;
                if (Random.Next(0, 100) > 30 && !allActionsHaveSameReward)
                    action = possibleActions.OrderByDescending(act => act.Reward).FirstOrDefault();
                else
                    action = possibleActions.ElementAt(Random.Next(0, possibleActions.Count));
                performedDirections.Add(action.Direction);
                var nextState = TransitionFunction(CurrentState, action);
                CurrentState = nextState;
            }
            while (CurrentState.Reward != Rewards.GOAL);
        }

        private QLearningNode TransitionFunction(QLearningNode currentState, MapAction action)
        {
            var next = GetNextState(currentState, action);

            var nextStateMaxPossibleReward = QTable[next.Id].Max(act => act.Reward);
            action.Reward = next.Reward + 0.5 * nextStateMaxPossibleReward;
            return next;
        }

        private QLearningNode GetNextState(QLearningNode currentState, MapAction action)
        {
            switch (action.Direction)
            {
                case eDirection.Down:
                    return Map[currentState.X, CurrentState.Y + 1];

                case eDirection.Left:
                    return Map[currentState.X - 1, CurrentState.Y];

                case eDirection.Right:
                    return Map[currentState.X + 1, CurrentState.Y];

                case eDirection.Up:
                default:
                    return Map[currentState.X, CurrentState.Y-1];

            }
        }
    }
}
