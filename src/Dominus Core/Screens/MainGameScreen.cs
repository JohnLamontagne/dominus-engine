using Dominus_Core.Effects.Particles;
using Dominus_Core.Utilities;
using Dominus_Core.World.WorldStructure;
using Dominus_Utilities;
using GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Dominus_Core.Screens
{
    class MainGameScreen : GameScreen
    {

        private WorldViewHandler _worldHandler;
        private Camera _camera;

        public MainGameScreen()
        {
            _camera = new Camera();


            _worldHandler = new WorldViewHandler();
        }

        public override void Activate(bool instancePreserved)
        {
            if (!instancePreserved)
            {
                var worldView = _worldHandler.AddWorldView(new WorldView(_camera));

                worldView.Active = true;
                worldView.CanRender = true;

                var content = ScreenManager.Game.Content;

                ParticleEmitter defaultEmitter = new ColorDirectionalParticleEmitter(new Texture2D[] { content.Load<Texture2D>("Player") }, 20000, new Vector2(500, 500), true, true);

                worldView.AddGameObject(defaultEmitter, "aPart");
                defaultEmitter.Initialize();
                defaultEmitter.Emitting = true;

            }

            base.Activate(instancePreserved);
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (this.IsActive)
            {

                // Update the game world.
                _worldHandler.Update(gameTime);

                _worldHandler.GetWorldView(0).GetGameObject<ColorDirectionalParticleEmitter>("aPart").Position = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

            }

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            if (this.IsActive)
            {
                this.ScreenManager.SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, _camera.GetTransformation(this.ScreenManager.GraphicsDevice));

                this.ScreenManager.SpriteBatch.DrawString(this.ScreenManager.Font, "Game", new Vector2(0, 0), Color.Red);

                _worldHandler.Draw(this.ScreenManager.SpriteBatch);

                this.ScreenManager.SpriteBatch.End();


            }

            base.Draw(gameTime);
        }
    }
}
