using Dominus_Core.Screens;
using Dominus_Core.Utilities;
using Dominus_Core.World.Entities;
using Dominus_Core.World.WorldStructure;
using Dominus_Graphics.Utilities;
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
using System.IO;
using System.Linq;
using XNAGameConsole;

namespace Dominus_Core
{
    public class DominusEngineGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private ScreenManager _screenManager;
        private ScreenFactory _screenFactory;
        private GameConsole _gameConsole;
        protected TextWriter _writer;

        public DominusEngineGame()
        {
            Content.RootDirectory = "Content";

            this.IsMouseVisible = true;

            // Initialize our graphics device manager.
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
            // Add the main menu screen.
            _screenManager.AddScreen(new MainMenuScreen(), null);
        }

        protected override void LoadContent()
        {
            // Create the game console.
            _gameConsole = new GameConsole(this, _screenManager.SpriteBatch);
            _gameConsole.Options.PastCommandColor = Color.Green;
            _gameConsole.Options.PastCommandOutputColor = Color.Red;

            // Create the stream writer for Win32 console redirection.
            _writer = new XNAConsoleStreamWriter(_gameConsole);

            // Redirect Win32 console output to the game console.
            Console.SetOut(_writer);

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            // Update the InputHelper.
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