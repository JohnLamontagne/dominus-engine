using Dominus_Graphics.GUI.Widgets;
using Dominus_GUI_Editor.TypeConverters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Xml;

namespace Dominus_GUI_Editor.GUI.Widgets
{
    public class EditorButton : Button, IEditorWidget
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

        public void Save(XmlWriter xmlWriter)
        {
            xmlWriter.WriteElementString("Position", this.Position.ToString());
            xmlWriter.WriteElementString("Scale", this.Scale.ToString());
            xmlWriter.WriteElementString("Text", this.Text);
            xmlWriter.WriteElementString("ForeColor", this.ForeColor.ToString());
            xmlWriter.WriteElementString("Visible", this.Visible.ToString());
            //xmlWriter.WriteElementString("Font", this.Font.)

            // Write the texture information.
            xmlWriter.WriteStartElement("Textures");
            xmlWriter.WriteElementString("HoverTexture", this.HoverTexture == null ? "naught" : this.HoverTexture.Name);
            xmlWriter.WriteElementString("IdleTexture", this.IdleTexture == null ? "naught" : this.IdleTexture.Name);
            xmlWriter.WriteElementString("MouseDownTexture", this.MouseDownTexture == null ? "naught" : this.MouseDownTexture.Name);
            xmlWriter.WriteEndElement();
        }
    }
}