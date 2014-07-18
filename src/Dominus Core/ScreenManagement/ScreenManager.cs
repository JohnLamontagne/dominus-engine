using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominus_Core.ScreenManagement
{
    public sealed class ScreenManager
    {
        private readonly Dictionary<string, Screen> _screens;
        private Screen _activeScreen;

        public Screen ActiveScreen { get { return _activeScreen; } }

        public ScreenManager()
        {
            _screens = new Dictionary<string, Screen>();
        }

        public void AddScreen(Screen screen, string name)
        {
            _screens.Add(name, screen);
        }

        public void RemoveScreen(string name)
        {
            _screens.Remove(name);
        }

        public void RemoveScreen(Screen screen)
        {
            var screenName = _screens.FirstOrDefault(x => x.Value == screen).Key;

            this.RemoveScreen(screenName);
        }

        public T GetScreen<T>(string screenName) where T : Screen
        {
            Screen value;

            if (_screens.TryGetValue(screenName, out value))
            {
                if (value.GetType() == typeof(T))
                {
                    return (T)value;
                }
            }

            return default(T);
        }

        public void SetActiveScreen(string screenName)
        {
            _activeScreen = _screens[screenName];
        }

        internal void Update(GameTime gameTime)
        {
            if (_activeScreen != null) _activeScreen.Update(gameTime);

            foreach (var screen in _screens.Values)
                if (screen.SilentUpdate)
                    screen.Update(gameTime);
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            if (_activeScreen != null) _activeScreen.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}