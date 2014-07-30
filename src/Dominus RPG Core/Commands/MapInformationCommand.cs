using Dominus_RPG_Core.ScreenManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNAGameConsole;

namespace Dominus_RPG_Core.Commands
{
    public class MapInformationCommand : IConsoleCommand
    {
        private RPGScreen _rpgScreen;

        public string Description
        {
            get { return "Displays information about the current map."; }
        }

        public MapInformationCommand(RPGScreen rpgScreen)
        {
            _rpgScreen = rpgScreen;
        }

        public string Execute(string[] arguments)
        {
            if (_rpgScreen.CurrentMap != null)
            {
                foreach (var layer in _rpgScreen.CurrentMap.GetLayers())
                {
                    if (layer.LayerType == World.WorldStructure.LayerTypes.ObjectLayer)
                    {
                        foreach (var entry in layer.GetGameObjects())
                        {
                            Console.WriteLine("Layer GameObject: " + entry.Key + " of type: " + entry.Value.GetType());
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("The map is not initalized!");
            }

            return "";
        }

        public string Name
        {
            get { return "MapInformation"; }
        }
    }
}