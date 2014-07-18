using Dominus_Core.Graphics.GUI.Widgets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominus_Core.Graphics.GUI
{
    public class WidgetDictionary : IEnumerable
    {
        private readonly Dictionary<string, IWidget> _dictionary;

        public WidgetDictionary()
        {
            _dictionary = new Dictionary<string, IWidget>();
        }

        public void AddWidget(Dominus_Core.Graphics.GUI.Widgets.IWidget value)
        {
            _dictionary.Add(value.Name, value);

            value.NameChanged += value_NameChanged;
        }

        public IWidget GetWidget(string key)
        {
            return _dictionary[key];
        }

        public IWidget[] GetWidgets()
        {
            var widgets = new IWidget[_dictionary.Values.Count];
            _dictionary.Values.CopyTo(widgets, 0);

            return widgets;
        }

        public void Clear()
        {
            _dictionary.Clear();
        }

        public void Remove(string key)
        {
            _dictionary.Remove(key);
        }

        private void value_NameChanged(object sender, EventArgs e)
        {

            var widget = sender as IWidget;
            var widgetNameChangedEventArgs = e as WidgetNameChangedEventArgs;

            _dictionary.Remove(widgetNameChangedEventArgs.OldName);
            _dictionary.Add(widgetNameChangedEventArgs.NewName, widget);
        }

        public IEnumerator GetEnumerator()
        {
            return this.GetWidgets().GetEnumerator();
        }
    }
}