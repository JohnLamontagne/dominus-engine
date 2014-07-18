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

            return texture2D;
        }
    }
}