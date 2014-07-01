using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Dominus_GUI_Editor.GUI.Widgets
{
    interface IEditorWidget
    {
        void Save(XmlWriter xmlWriter);
    }
}
