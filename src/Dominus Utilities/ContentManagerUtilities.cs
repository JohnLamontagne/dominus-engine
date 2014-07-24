using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace Dominus_Utilities
{
    public static class ContentManagerUtilities
    {
        public static Texture2D LoadTexture2D(ContentManager content, string filePath)
        {
            var graphicsDevice = (content.ServiceProvider.GetService(typeof(IGraphicsDeviceService)) as IGraphicsDeviceService).GraphicsDevice;

            Texture2D texture2D;

            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                texture2D = Texture2D.FromStream(graphicsDevice, fileStream);
                texture2D.Name = filePath;
            }

            Color[] data = new Color[texture2D.Width * texture2D.Height];
            texture2D.GetData(data);
            for (int i = 0; i != data.Length; ++i)
                data[i] = Color.FromNonPremultiplied(data[i].ToVector4());
            texture2D.SetData(data);

            return texture2D;
        }

        public static Texture2D LoadTexture2D(ContentManager content, Stream stream)
        {
            var graphicsDevice = (content.ServiceProvider.GetService(typeof(IGraphicsDeviceService)) as IGraphicsDeviceService).GraphicsDevice;

            Texture2D texture2D;

            texture2D = Texture2D.FromStream(graphicsDevice, stream);
            texture2D.Name = "Unknown";

            Color[] data = new Color[texture2D.Width * texture2D.Height];
            texture2D.GetData(data);
            for (int i = 0; i != data.Length; ++i)
                data[i] = Color.FromNonPremultiplied(data[i].ToVector4());
            texture2D.SetData(data);

            return texture2D;
        }
    }
}