using Dominus_Core.Graphics.GUI.Widgets;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Dominus_Core.Graphics.GUI
{
    public class GUIHandler
    {
        private readonly WidgetDictionary _widgets;

        private IWidget _activeWidget;

        public GUIHandler()
        {
            _widgets = new WidgetDictionary();
        }

        public void AddWidget(IWidget widget)
        {
            _widgets.AddWidget(widget);
        }

        public T GetWidget<T>(string id) where T : IWidget
        {
            IWidget value = _widgets.GetWidget(id);

            if (value.GetType() == typeof(T))
            {
                return (T)value;
            }

            return default(T);
        }

        public IWidget[] GetWidgets()
        {
            return _widgets.GetWidgets();
        }

        public void RemoveWidget(string id)
        {
            _widgets.Remove(id);
        }

        public void ClearWidgets()
        {
            _widgets.Clear();
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var widget in _widgets.GetWidgets())
            {
                widget.Update(gameTime);

                if (widget.Active && _activeWidget != widget)
                {
                    if (_activeWidget != null)
                        _activeWidget.Active = false;

                    _activeWidget = widget;

                    Console.WriteLine("Active widget: " + _activeWidget.ToString());
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (var widget in _widgets.GetWidgets())
            {
                if (widget != _activeWidget)
                    widget.Draw(spriteBatch);
            }

            // Active widget is always on top.
            if (_activeWidget != null)
                _activeWidget.Draw(spriteBatch);
        }

        public virtual void Load(string filePath, ContentManager content)
        {
            this.ClearWidgets();

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);

            foreach (XmlNode node in xmlDoc.SelectNodes("Widgets/Widget"))
            {
                var type = Type.GetType(node.Attributes["Type"].Value);

                var widget = Activator.CreateInstance(type, true) as IWidget;

                widget.Load(content, node);

                widget.Name = node.Attributes["Name"].Value;

                this.AddWidget(widget);
            }
        }

        public virtual void Save(string filePath)
        {
            var xmlSettings = new XmlWriterSettings();
            xmlSettings.Indent = true;

            var xmlWriter = XmlWriter.Create(filePath, xmlSettings);
            xmlWriter.WriteStartDocument();

            xmlWriter.WriteStartElement("Widgets");

            foreach (IWidget widget in this.GetWidgets())
            {
                xmlWriter.WriteStartElement("Widget");
                xmlWriter.WriteAttributeString("Type", widget.GetType().BaseType.ToString());
                xmlWriter.WriteAttributeString("Name", widget.Name);
                widget.Save(xmlWriter);
                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
        }
    }
}