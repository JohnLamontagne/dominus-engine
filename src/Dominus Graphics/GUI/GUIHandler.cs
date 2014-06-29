using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Dominus_Graphics.GUI
{
    public class GUIHandler
    {
        private readonly Dictionary<string, IWidget> _widgets;

        private IWidget _activeWidget;

        public GUIHandler()
        {
            _widgets = new Dictionary<string, IWidget>();
        }

        public void AddWidget(IWidget widget, string id)
        {
            _widgets.Add(id, widget);
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

        public void LoadGUI(string configFilePath)
        {

        }
    }
}