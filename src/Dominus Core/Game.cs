using Dominus_Core.Screens;
using Dominus_Core.Utilities;
using Dominus_Core.World.Entities;
using Dominus_Core.World.WorldStructure;
using Dominus_Utilities;
using GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominus_Core
{
    public class DominusEngineGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private ScreenManager _screenManager;
        private ScreenFactory _screenFactory;

        public DominusEngineGame()
        {
            Content.RootDirectory = "Content";

            this.IsMouseVisible = true;

            _graphics = new GraphicsDeviceManager(this);

            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();

            _screenFactory = new ScreenFactory();
            Services.AddService(typeof(IScreenFactory), _screenFactory);

            _screenManager = new ScreenManager(this);
            Components.Add(_screenManager);

            this.InitializeScreens();
        }

        private void InitializeScreens()
        {
            _screenManager.AddScreen(new MainMenuScreen(), null);
        }

        protected override void Update(GameTime gameTime)
        {
            InputHelper.GetHelper().Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
        }
    }
}