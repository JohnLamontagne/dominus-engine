using Dominus_Core.ScreenManagement;
using Dominus_Core.Utilities;
using Dominus_Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using XNAGameConsole;

namespace Dominus_Core
{
    public abstract class DominusGame : Game
    {
        protected GraphicsDeviceManager Graphics;
        protected GameConsole GameConsole;
        protected SpriteBatch SpriteBatch;
        protected TextWriter Writer;
        protected ScreenManager ScreenManager;

        public DominusGame()
        {
            this.Content.RootDirectory = "Content";

            this.IsMouseVisible = true;

            // Initialize our graphics device manager.
            this.Graphics = new GraphicsDeviceManager(this);
            this.Graphics.PreferredBackBufferWidth = 800;
            this.Graphics.PreferredBackBufferHeight = 600;
            this.Graphics.ApplyChanges();

            this.ScreenManager = new ScreenManager();
        }

        protected override void LoadContent()
        {
            this.SpriteBatch = new SpriteBatch(this.Graphics.GraphicsDevice);

            this.GameConsole = new GameConsole(this, this.SpriteBatch);

            // Create the game console.
            this.GameConsole.Options.PastCommandColor = Color.Green;
            this.GameConsole.Options.PastCommandOutputColor = Color.Red;

            // Create the stream writer for Win32 console redirection.
            this.Writer = new XNAConsoleStreamWriter(this.GameConsole);

            // Redirect Win32 console output to the game console.
            Console.SetOut(this.Writer);

            base.LoadContent();

            this.InitalizeScreens();
        }

        protected abstract void InitalizeScreens();

        protected override void Update(GameTime gameTime)
        {
            // Update the InputHelper.
            InputHelper.GetHelper().Update(gameTime);

            this.ScreenManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.Graphics.GraphicsDevice.Clear(Color.Black);

            this.ScreenManager.Draw(this.SpriteBatch);


            base.Draw(gameTime);
        }
    }
}