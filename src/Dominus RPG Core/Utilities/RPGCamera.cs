using Dominus_RPG_Core.World.Entities;
using Dominus_Utilities;
using Microsoft.Xna.Framework;
using System;

namespace Dominus_RPG_Core.Utilities
{
    internal class RPGCamera : Camera
    {
        public RPGCamera(Rectangle bounds)
            : base(bounds)
        {
        }

        public void SetEntityTarget(IEntity entity)
        {
            entity.Moved += entity_Moved;
        }

        private void entity_Moved(object sender, EventArgs e)
        {
            this.Move((e as EntityMovedEventArgs).Distance);
        }
    }
}