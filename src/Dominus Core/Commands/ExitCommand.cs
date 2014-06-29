using Microsoft.Xna.Framework;
using System;
using XNAGameConsole;

namespace Dominus_Graphics.Commands
{
    class ExitCommand : IConsoleCommand
    {
        private Game _game;

        public string Description
        {
            get { return "Terminates the game."; }
        }

        public string Name
        {
            get { return "exit"; }
        }

        public ExitCommand(Game game)
        {
            _game = game;
        }

        public string Execute(string[] arguments)
        {
            _game.Exit();

            // It doesn't matter if we return anything...
            return "";
        }


    }
}
