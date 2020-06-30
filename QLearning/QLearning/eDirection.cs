using System;
using System.Collections.Generic;
using System.Text;

namespace QLearning
{
    public enum eDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    public static class DirectionExtension
    {
        public static string Arrow(this eDirection eDirection)
        {
            //↑↓→←
            switch (eDirection)
            {
                case eDirection.Down:
                    return "↓";
                case eDirection.Up:
                    return "↑";
                case eDirection.Left:
                    return "←";                    
                case eDirection.Right:
                default:
                    return "→";
            }
        }
    }
}
