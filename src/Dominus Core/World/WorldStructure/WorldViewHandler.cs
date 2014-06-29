using Dominus_Core.Utilities;
using Dominus_Core.World.Entities;

using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Dominus_Core.World.WorldStructure
{
    public sealed class WorldViewHandler
    {
        private readonly List<WorldView> _worldViews;

        public WorldViewHandler()
        {
            _worldViews = new List<WorldView>();
        }

        public WorldView AddWorldView(WorldView worldView)
        {
            _worldViews.Add(worldView);

            return worldView;
        }

        public void RemoveWorldView(int index)
        {
            _worldViews.RemoveAt(index);
        }

        public void ClearWorldViews()
        {
            foreach (var worldView in _worldViews)
                worldView.Unload();

            _worldViews.Clear();
        }

        public WorldView GetWorldView(int index)
        {
            return _worldViews[index];
        }

        public void Update(GameTime gameTime)
        {
            foreach (var worldView in _worldViews)
                if (worldView.Active) worldView.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var worldView in _worldViews)
                if (worldView.CanRender) worldView.Draw(spriteBatch);
        }
    }
}