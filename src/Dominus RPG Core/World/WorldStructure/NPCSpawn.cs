using Microsoft.Xna.Framework;
using System;

namespace Dominus_RPG_Core.World.WorldStructure
{
   public class NPCSpawn
    {
       private string _filePath;
       private Point _location;

       public string NpcInformationFile { get { return _filePath; } }

       public Point Location { get { return _location; } }

       public NPCSpawn(string npcsInfoFile, Point location)
       {
           _filePath = npcsInfoFile;
           _location = location;
       }
    }
}
