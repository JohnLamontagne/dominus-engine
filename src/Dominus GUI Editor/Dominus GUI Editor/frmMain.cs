using Dominus_Graphics.GUI;
using Dominus_GUI_Editor.GUI.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Dominus_GUI_Editor
{
    public partial class frmMain : Form
    {
        private string _filePath;
        private GUIHandler _guiHandler;
        private bool _mouseDown;
        private IWidget _activeWidget;

        /// <summary>
        /// Used for dragging the widgets around the screen, it's The offset of the mouse position and the widget positin.
        ///
        /// </summary>
        private Vector2 _mouseOffset;

        public GraphicsDevice GraphicsDevice { get { return this.guiDisplay.GraphicsDevice; } }

        public frmMain()
        {
            InitializeComponent();

            this.guiDisplay.GUIHandler = _guiHandler;

            this.scrlX.Maximum = Math.Abs(int.Parse(this.txtWidth.Text) - this.guiDisplay.Width);
            this.scrlY.Maximum = Math.Abs(int.Parse(this.txtHeight.Text) - this.guiDisplay.Height);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(_filePath))
            {
                if (_guiHandler != null)
                    _guiHandler.Save(_filePath);
            }
            else
            {
                var savefileDialog = new SaveFileDialog();

                savefileDialog.DefaultExt = ".xml";
                savefileDialog.Filter = "XML Files (*.xml)|*.xml";
                savefileDialog.RestoreDirectory = true;

                if (savefileDialog.ShowDialog() == DialogResult.OK)
                {
                    _filePath = savefileDialog.FileName;

                    if (_guiHandler != null)
                        _guiHandler.Save(_filePath);
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var savefileDialog = new SaveFileDialog();

            savefileDialog.DefaultExt = ".xml";
            savefileDialog.Filter = "XML Files (*.xml)|*.xml";

            savefileDialog.RestoreDirectory = true;

            if (savefileDialog.ShowDialog() == DialogResult.OK)
            {
                _filePath = savefileDialog.FileName;

                if (_guiHandler != null)
                    _guiHandler.Save(_filePath);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _guiHandler = new GUIHandler();
            this.guiDisplay.GUIHandler = _guiHandler;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_guiHandler == null) return;

            if (!String.IsNullOrEmpty(_filePath))
            {
                if (_guiHandler != null)
                    _guiHandler.Save(_filePath);
            }
            else
            {
                var savefileDialog = new SaveFileDialog();

                savefileDialog.DefaultExt = ".xml";
                savefileDialog.Filter = "XML Files (*.xml)|*.xml";
                savefileDialog.RestoreDirectory = true;

                if (savefileDialog.ShowDialog() == DialogResult.OK)
                {
                    _filePath = savefileDialog.FileName;

                    if (_guiHandler != null)
                        _guiHandler.Save(_filePath);
                }
            }
        }

        private void guiDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            if (lstControls.SelectedItem == null)
            {

            }
            else if (_guiHandler != null)
            {
                if (lstControls.SelectedItem.ToString() == "Button")
                {
                    var button = new EditorButton(this.guiDisplay.Content.Load<Texture2D>("defaultButton"), this.guiDisplay.Content.Load<SpriteFont>("menufont"));
                    button.Text = "Text";
                    button.Position = new Vector2(e.X, e.Y);

                    _guiHandler.AddWidget(button, "button" + _guiHandler.GetWidgets().Length);

                    this.widgetPropertyGrid.SelectedObject = _guiHandler.GetWidget<EditorButton>("button" + (_guiHandler.GetWidgets().Length - 1));

                    lstControls.SelectedItem = null;
                }
            }
        }

        private void guiDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseDown = true;

            if (_guiHandler == null || _guiHandler.GetWidgets().Length < 0) return;

            // Find the widget that you clicked on and select it.
            foreach (var widget in _guiHandler.GetWidgets())
            {
                if (widget.Contains(new Microsoft.Xna.Framework.Point(e.X, e.Y)))
                {
                    _activeWidget = widget;
                    this.widgetPropertyGrid.SelectedObject = widget;

                    _mouseOffset = widget.Position - new Vector2(e.X, e.Y);
                }
            }
        }

        private void guiDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mouseDown)
            {
                if (_activeWidget != null)
                {
                    if (_activeWidget.GetType() == typeof(EditorButton))
                    {
                        var widget = _activeWidget as EditorButton;

                        widget.Position = new Vector2(e.X, e.Y) + _mouseOffset;
                    }
                }
            }
        }

        private void guiDisplay_MouseUp(object sender, MouseEventArgs e)
        {
            _mouseDown = false;
            _activeWidget = null;
        }
    }
}