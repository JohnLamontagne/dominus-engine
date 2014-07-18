using Dominus_Core.Graphics.GUI.Widgets;
using Dominus_GUI_Editor.GUI.Widgets;
using Dominus_Utilities;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Dominus_GUI_Editor.GUI
{
    internal class GUIHandler : Dominus_Core.Graphics.GUI.GUIHandler
    {
        public override void Load(string filePath, ContentManager content)
        {
            base.Load(filePath, content);

            var newWidgets = new Dictionary<string, IWidget>();

            foreach (var value in this.GetWidgets())
            {
                if (value.GetType() == typeof(Button))
                {
                    var editorButton = new EditorButton(value as Button);
                    newWidgets.Add(value.Name, editorButton);
                }
            }

            this.ClearWidgets();

            foreach (var entry in newWidgets)
            {
                this.AddWidget(entry.Value);
            }
        }
    }
}