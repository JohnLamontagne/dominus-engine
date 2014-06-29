using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominus_GUI_Editor
{
    internal class GUIHandler : Dominus_Graphics.GUI.GUIHandler
    {

        internal void Save(string filePath)
        {
            foreach (var widget in this.GetWidgets())
            {

            }
        }

        internal void Load(string filePath)
        {
            this.ClearWidgets();
        }
    }
}
