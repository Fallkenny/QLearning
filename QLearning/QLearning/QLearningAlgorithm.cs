﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static QLearning.MainWindow;

namespace QLearning
{
    class QLearningAlgorithm
    {
        public QLearningNode CurrentState { get; set; }
        public int GoalID { get; private set; }
        Dictionary<int, List<MapAction>> QTable { get; set; }
        private const string IN_PROGRESS = "Calculando...";
        public string BestPath { get; set; } = IN_PROGRESS;
        public Random Random { get; set; } = new Random();
        public int Episodes { get; private set; } = 1;
        public int Moves { get; private set; } = 0;
        public QLearningNode[,] Map;
        public QLearningNode StartState { get; private set; }
        public bool ReachedGlobalMaximum { get; set; } = false;
        public int EqualPathCalculations { get; private set; }

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
            this.StartState = scenario.StartPosition;
            this.CurrentState = scenario.StartPosition;
        }

        public void DoNextStep()
        {
            if (CurrentState.Reward != Rewards.GOAL)
            {
                var possibleActions = QTable[CurrentState.Id].OrderBy(x => Guid.NewGuid()).ToList();

                var allActionsHaveSameReward = possibleActions.Select(action => action.Reward).Distinct().Count() == 1;
                MapAction action;
                if (Random.Next(0, 100) > 30 && !allActionsHaveSameReward)
                    action = possibleActions.OrderByDescending(act => act.Reward).FirstOrDefault();
                else
                    action = possibleActions.ElementAt(Random.Next(0, possibleActions.Count));
                var nextState = TransitionFunction(CurrentState, action);
                Moves++;
                CurrentState = nextState;
            }
            else
            {
                this.CurrentState = StartState;
                this.Episodes++;
                if (Episodes >= 20 || Episodes >= 20 && Episodes % 10 == 0)
                {
                    var previousPath = this.BestPath;
                    this.BestPath = this.CalculateBestPath();
                    if (previousPath.Equals(this.BestPath))
                        EqualPathCalculations++;

                    if (EqualPathCalculations > 200)
                        this.ReachedGlobalMaximum = true;
                }
            }
        }

        private string CalculateBestPath()
        {
            var state = StartState;
            var path = string.Empty;
            var currentReward = 1;
            var iteractions = 0;
            do
            {
                iteractions++;
                var possibleActions = QTable[state.Id].ToList();
                var action = possibleActions.OrderByDescending(act => act.Reward).FirstOrDefault();
                path += action.Direction.Arrow();
                var nextState = TransitionFunction(state, action);
                currentReward = nextState.Reward;
                state = nextState;
            }
            while (currentReward != Rewards.GOAL && iteractions < 500);

            return path.Length < 500 ? path : IN_PROGRESS;
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
                    return Map[currentState.X, currentState.Y + 1];

                case eDirection.Left:
                    return Map[currentState.X - 1, currentState.Y];

                case eDirection.Right:
                    return Map[currentState.X + 1, currentState.Y];

                case eDirection.Up:
                default:
                    return Map[currentState.X, currentState.Y - 1];
            }
        }
    }
}
