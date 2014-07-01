using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;

namespace Dominus_Graphics.GUI
{
    public class GUIHandler
    {
        private readonly Dictionary<string, IWidget> _widgets;
        private readonly SpriteFont _mainFont;

        private IWidget _activeWidget;

        public GUIHandler(SpriteFont mainFont)
        {
            _widgets = new Dictionary<string, IWidget>();
            _mainFont = mainFont;
        }

        public void AddWidget(IWidget widget, string id)
        {
            if (_widgets.Keys.Contains<string>(id))
            {
                Console.WriteLine("Error adding widget <{0}>: identifer already exists!", widget.ToString());
                return;
            }
            _widgets.Add(id, widget);
        }

        public string GetName(IWidget widget)
        {
            return _widgets.FirstOrDefault(x => x.Value == widget).Key;
        }

        public T GetWidget<T>(string id) where T : IWidget
        {
            IWidget value;

            if (_widgets.TryGetValue(id, out value))
            {
                if (value.GetType() == typeof(T))
                {
                    return (T)value;
                }
            }

            return default(T);
        }

        public IWidget[] GetWidgets()
        {
            var widgets = new IWidget[_widgets.Values.Count];
            _widgets.Values.CopyTo(widgets, 0);

            return widgets;
        }

        public void RemoveWidget(string id)
        {
            _widgets.Remove(id);
        }

        public void ClearWidgets()
        {
            _widgets.Clear();
        }

        public void Update(GameTime gameTime)
        {
            foreach (var widget in _widgets.Values)
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

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var widget in _widgets.Values)
                widget.Draw(spriteBatch);
        }

        public void Load(string filePath, ContentManager content)
        {
            this.ClearWidgets();

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);

            foreach (XmlNode node in xmlDoc.SelectNodes("Widgets/Widget"))
            {
                var type = Type.GetType(node.Attributes["Type"].Value);

                var widget = Activator.CreateInstance(type, true) as IWidget;

                widget.Load(content, _mainFont, node);
            }
        }
    }
}