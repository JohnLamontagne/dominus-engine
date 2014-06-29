using Dominus_Graphics.GUI.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dominus_GUI_Editor.Controls
{
    internal class GUIDisplay : WinFormsGraphicsDevice.GraphicsDeviceControl
    {
        private SpriteBatch _spriteBatch;

        public ContentManager Content { get; private set; }

        public GUIHandler GUIHandler { get; set; }

        protected override void Initialize()
        {
            _spriteBatch = new SpriteBatch(this.GraphicsDevice);

            this.Content = new ContentManager(this.Services, "Content");

            Application.Idle += delegate { Invalidate(); };
        }

        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            if (this.GUIHandler != null)
                this.GUIHandler.Draw(_spriteBatch);

            _spriteBatch.End();
        }
    }
}