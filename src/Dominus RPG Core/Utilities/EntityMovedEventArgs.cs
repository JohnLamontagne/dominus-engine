using Microsoft.Xna.Framework;
using System;

namespace Dominus_RPG_Core.Utilities
{
    public class EntityMovedEventArgs : EventArgs
    {
        private readonly Vector2 _distance;

        public Vector2 Distance { get { return _distance; } }

        public EntityMovedEventArgs(Vector2 distance)
        {
            _distance = distance;
        }
    }
}
