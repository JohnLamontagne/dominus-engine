using Dominus_Graphics.GUI.Widgets;
using Dominus_GUI_Editor.TypeConverters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Dominus_GUI_Editor.GUI.Widgets
{
    public class EditorButton : Button
    {

        public EditorButton(Texture2D texture, SpriteFont font)
            : base(texture, font)
        {

        }

        [Browsable(false)]
        public Vector2 TrueDimensions
        {
            get
            {
                if (this.IdleTexture != null)
                    return new Vector2(this.IdleTexture.Width * this.Scale.X, this.IdleTexture.Height * this.Scale.Y);
                else
                    return new Vector2(0, 0);
            }

        }

        [Browsable(false)]
        public override bool Active
        {
            get
            {
                return base.Active;
            }
            set
            {
                base.Active = value;
            }
        }

        public override SpriteFont Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
            }
        }

        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
            }
        }

        [EditorAttribute(typeof(Texture2DEditor), typeof(System.Drawing.Design.UITypeEditor))]
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

        [EditorAttribute(typeof(Texture2DEditor), typeof(System.Drawing.Design.UITypeEditor))]
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

        [EditorAttribute(typeof(Texture2DEditor), typeof(System.Drawing.Design.UITypeEditor))]
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

        public override bool Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                base.Visible = value;
            }
        }
    }
}
