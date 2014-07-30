using Dominus_Core;
using Dominus_Core.ScreenManagement;
using Dominus_RPG_Core.Utilities;
using Dominus_RPG_Core.World.Entities;
using Dominus_RPG_Core.World.WorldStructure;
using Dominus_Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Dominus_RPG_Core.ScreenManagement
{
    public class RPGScreen : Screen
    {
        private RPGCamera _camera;
        private SpriteBatch _mapSpriteBatch;
        private GraphicsDevice _graphicsDevice;

        public Map CurrentMap { get; protected set; }

        public RPGScreen(ContentManager content)
            : base(content)
        {
            _graphicsDevice = (content.ServiceProvider.GetService(typeof(IGraphicsDeviceService)) as IGraphicsDeviceService).GraphicsDevice;
            _mapSpriteBatch = new SpriteBatch(_graphicsDevice);

            _camera = new RPGCamera(new Rectangle(0, 0, _graphicsDevice.DisplayMode.Width, _graphicsDevice.DisplayMode.Height));

            this.Initalize();
        }

        /// <summary>
        /// Initalizes any additional required game components and content.
        /// </summary>
        protected virtual void Initalize()
        {
        }

        public virtual void ChangeMap(Map map)
        {
            // Change map logic.
            this.CurrentMap = map;
            _camera.Bounds = new Rectangle(0, 0, this.CurrentMap.Size.X * 32, this.CurrentMap.Size.Y * 32);

            var player = new Player(ContentManagerUtilities.LoadTexture2D(this.Content, DominusRPGGame.Properties.PlayerTexturePath));
            player.Bounds = _camera.Bounds;
            player.Speed = .5f;
            this.CurrentMap.GetLayers()[3].GetGameObjects().Add("MainPlayer", player);
            _camera.SetEntityTarget(player);
        }

        // Removes the specified game object from the screen.
        public override void RemoveGameObject(string name)
        {
            base.RemoveGameObject(name);
        }

        // Removes the specified game object from the screen.
        public override void RemoveGameObject(Dominus_Core.IGameObject value)
        {
            base.RemoveGameObject(value);
        }

        /// <summary>
        /// Gets the game objects from this screen, completely independent of the map layers.
        /// </summary>
        /// <returns></returns>
        public override OrderedDictionary<string, IGameObject> GetGameObjects()
        {
            return base.GetGameObjects();
        }

        /// <summary>
        /// Gets a game object from this screen, completely independent of the map layers.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public override T GetGameObject<T>(string name)
        {
            return base.GetGameObject<T>(name);
        }

        /// <summary>
        /// Adds a game object to this screen, completely independent of the map layers.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="name"></param>
        public override void AddGameObject(Dominus_Core.IGameObject gameObject, string name)
        {
            base.AddGameObject(gameObject, name);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _mapSpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, _camera.GetTransformation(_graphicsDevice));

            if (this.CurrentMap != null)
            {
                this.CurrentMap.Draw(spriteBatch);
            }

            foreach (var gameObject in this.GetGameObjects().Values)
                gameObject.Draw(_mapSpriteBatch);

            _mapSpriteBatch.End();

            this.GUIHandler.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            if (this.CurrentMap != null)
            {
                this.CurrentMap.Update(gameTime);
            }

            foreach (var gameObject in this.GetGameObjects().Values)
            {
                gameObject.Update(gameTime);
            }

            this.GUIHandler.Update(gameTime);
        }
    }
}