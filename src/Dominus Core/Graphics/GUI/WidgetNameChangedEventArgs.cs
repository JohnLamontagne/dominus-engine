using System;

namespace Dominus_Core.Graphics.GUI
{
    public class WidgetNameChangedEventArgs : EventArgs
    {
        private readonly string _newName;
        private readonly string _oldName;

        public string NewName { get { return _newName; } }

        public string OldName { get { return _oldName; } }

        public WidgetNameChangedEventArgs(string newName, string oldName)
        {
            _newName = newName;
            _oldName = oldName;
        }
    }
}