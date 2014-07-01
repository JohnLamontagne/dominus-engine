using Dominus_Graphics.GUI;
using Dominus_GUI_Editor.GUI.Widgets;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Dominus_GUI_Editor
{
    internal class GUIHandler : Dominus_Graphics.GUI.GUIHandler
    {
        public GUIHandler(SpriteFont mainFont)
            : base(mainFont)
        {

        }

        internal void Save(string filePath)
        {
            var xmlSettings = new XmlWriterSettings();
            xmlSettings.Indent = true;

            var xmlWriter = XmlWriter.Create(filePath, xmlSettings);
            xmlWriter.WriteStartDocument();

            xmlWriter.WriteStartElement("Widgets");

            foreach (IEditorWidget widget in this.GetWidgets())
            {
                xmlWriter.WriteStartElement("Widget");
                xmlWriter.WriteAttributeString("Type", widget.GetType().BaseType.ToString());
                xmlWriter.WriteAttributeString("Name", this.GetName(widget as IWidget));
                widget.Save(xmlWriter);
                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
        }
    }
}