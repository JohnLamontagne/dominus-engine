using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Dominus_GUI_Editor.TypeConverters
{
    public class Texture2DEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context,
                                            IServiceProvider provider,
                                            object value)
        {
            IWindowsFormsEditorService editorService = null;

            if (provider != null)
            {
                editorService = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            }

            if (editorService != null)
            {
                // Pop up an Open File dialog
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Title = "Open Texture...";
                openFile.Multiselect = false;
                openFile.AddExtension = true;
                openFile.CheckFileExists = true;
                openFile.Filter = "Valid Texture Formats (*.bmp, *.png, *.dds, *.jpg)|*.bmp;*.png;*.dds;*.jpg";
                openFile.ValidateNames = true;
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    // Access a global GraphicsDevice
                    GraphicsDevice graphicsDevice = Program.MainForm.GraphicsDevice;

                    using (var fileStream = new FileStream(openFile.FileName, FileMode.Open))
                    {
                        // Load up the texture
                        Texture2D texture = Texture2D.FromStream(graphicsDevice, fileStream);
                        texture.Name = openFile.FileName;
                        value = texture;
                    }

                }
            }

            return value;
        }
    }
}
