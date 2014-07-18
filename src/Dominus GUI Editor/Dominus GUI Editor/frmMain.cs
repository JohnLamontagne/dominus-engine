using Dominus_Core.Graphics.GUI.Widgets;
using Dominus_GUI_Editor.GUI;
using Dominus_GUI_Editor.GUI.Widgets;
using Dominus_Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.ComponentModel;
using System.Windows.Forms;

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
                    var buttonTexture = this.guiDisplay.Content.Load<Texture2D>("defaultButton");
                    buttonTexture.Name = "defaultButton";
                    var button = new EditorButton(buttonTexture, this.guiDisplay.Content.Load<SpriteFont>("menufont"));
                    button.Text = "Text";
                    button.Position = new Vector2(e.X, e.Y);
                    button.Name = "button" + (_guiHandler.GetWidgets().Length - 1);

                    PropertyDescriptor descriptor = TypeDescriptor.GetProperties(button.GetType())["SpouseName"];

                    _guiHandler.AddWidget(button);

                    this.widgetPropertyGrid.SelectedObject = _guiHandler.GetWidget<Dominus_Core.Graphics.GUI.Widgets.Button>(button.Name);

                    lstControls.SelectedItem = null;
                }
            }
        }

        private void guiDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseDown = true;

            if (_guiHandler == null || _guiHandler.GetWidgets().Length < 0) return;

            // Always consider the active widget first.
            if (_activeWidget != null && _activeWidget.Contains(new Point(e.X, e.Y)))
            {
                // Update the mouse offset.
                _mouseOffset = _activeWidget.Position - new Vector2(e.X, e.Y);

                return;
            }

            // Find the widget that you clicked on and select it.
            foreach (var widget in _guiHandler.GetWidgets())
            {
                if (widget.Contains(new Point(e.X, e.Y)))
                {
                    widget.Active = true;

                    _activeWidget = widget;

                    this.widgetPropertyGrid.SelectedObject = widget;

                    _mouseOffset = widget.Position - new Vector2(e.X, e.Y);

                    break;
                }
            }
        }

        private void guiDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mouseDown)
            {
                if (_activeWidget != null)
                {
                    // Update the selected wideget relative to our mouse.
                    _activeWidget.Position = new Vector2(e.X, e.Y) + _mouseOffset;
                }
            }
        }

        private void guiDisplay_MouseUp(object sender, MouseEventArgs e)
        {
            // Our mouse isn't down anymore.
            _mouseDown = false;

            // Reset the mouse offset relative to the selected widget.
            _mouseOffset = Vector2.Zero;
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".xml";
            openFileDialog.Filter = "XML Files (*.xml)|*.xml";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _guiHandler = new GUIHandler();

                _guiHandler.Load(openFileDialog.FileName, this.guiDisplay.Content);

                this.guiDisplay.GUIHandler = _guiHandler;
            }
        }

        private void lstControls_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void guiDisplay_Click(object sender, EventArgs e)
        {
        }
    }
}