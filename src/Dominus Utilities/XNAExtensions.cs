using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace Dominus_Utilities
{
    public static class XnaExtensions
    {
        public static Vector2 FromString(this Vector2 vector2, string value)
        {
            value = value.TrimStart('{').TrimEnd('}');

            var components = value.Split(' ');

            // Remove the X:/Y:
            components[0] = components[0].Remove(0, 2);
            components[1] = components[1].Remove(0, 2);

            return new Vector2(float.Parse(components[0]), float.Parse(components[1]));
        }

        public static Color FromString(this Color color, string value)
        {
            value = value.TrimStart('{').TrimEnd('}');

            var components = value.Split(' ');

            components[0] = components[0].Remove(0, 2);
            components[1] = components[1].Remove(0, 2);
            components[2] = components[2].Remove(0, 2);
            components[3] = components[3].Remove(0, 2);

            return new Color(float.Parse(components[0]), float.Parse(components[1]), float.Parse(components[2]), float.Parse(components[3]));
        }


    }
}