using Dominus_Core.Graphics.GUI.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;

namespace Dominus_GUI_Editor.GUI.Widgets
{
    class EditorButton : Button
    {

        public override string Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                base.Name = value;
            }
        }

        [Browsable(false)]
        public override bool Active { get { return base.Active; } set { base.Active = value; } }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        public override Vector2 Position
        {
            get
            {
                return base.Position;
            }
            set
            {
                base.Position = value;
            }
        }

        public override Vector2 Scale
        {
            get
            {
                return base.Scale;
            }
            set
            {
                base.Scale = value;
            }
        }

        [EditorAttribute(typeof(Texture2DEditor), typeof(UITypeEditor))]
        [Browsable(true)]
        public override Texture2D IdleTexture
        {
            get
            {
                return base.IdleTexture;
            }
            set
            {
                base.IdleTexture = value;
            }
        }

        [EditorAttribute(typeof(Texture2DEditor), typeof(UITypeEditor))]
        [Browsable(true)]
        public override Texture2D HoverTexture
        {
            get
            {
                return base.HoverTexture;
            }

            set
            {
                base.HoverTexture = value;
            }
        }

        [EditorAttribute(typeof(Texture2DEditor), typeof(UITypeEditor))]
        [Browsable(true)]
        public override Texture2D MouseDownTexture
        {
            get
            {
                return base.MouseDownTexture;
            }

            set
            {
                base.MouseDownTexture = value;
            }
        }

        public EditorButton(Texture2D idleTexture, SpriteFont font)
            : base(idleTexture, font)
        {

        }

        public EditorButton(Button button)
            : base(button.IdleTexture, button.Font)
        {
            this.Name = button.Name;
            this.HoverTexture = button.HoverTexture;
            this.MouseDownTexture = button.MouseDownTexture;
            this.Scale = button.Scale;
            this.Position = button.Position;
            this.Text = button.Text;
            this.Visible = button.Visible;
            this.ForeColor = button.ForeColor;
        }

    }
}
