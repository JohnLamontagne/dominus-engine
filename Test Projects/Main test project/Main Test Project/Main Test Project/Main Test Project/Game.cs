using Dominus_Core;
using Main_Test_Project.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Main_Test_Project
{
    internal class Game : DominusGame
    {
        public Game()
        {

        }

        protected override void LoadContent()
        {
            base.LoadContent();

            this.GameConsole.Options.OpenOnWrite = false;
        }

        protected override void InitalizeScreens()
        {
            this.ScreenManager.AddScreen(new MainMenuScreen(this.Content), "MainMenu");
            this.ScreenManager.SetActiveScreen("MainMenu");
        }
    }
}