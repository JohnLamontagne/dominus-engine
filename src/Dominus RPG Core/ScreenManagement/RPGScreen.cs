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
        private readonly Dictionary<string, IEntity> _entities;
        private RPGCamera _camera;
        private SpriteBatch _mapSpriteBatch;
        private GraphicsDevice _graphicsDevice;

        public Map CurrentMap { get; protected set; }

        public RPGScreen(ContentManager content, RPGGameProperties properties)
            : base(content)
        {
            _entities = new Dictionary<string, IEntity>();

            _graphicsDevice = (content.ServiceProvider.GetService(typeof(IGraphicsDeviceService)) as IGraphicsDeviceService).GraphicsDevice;
            _mapSpriteBatch = new SpriteBatch(_graphicsDevice);

            _camera = new RPGCamera(new Rectangle(0, 0, _graphicsDevice.DisplayMode.Width, _graphicsDevice.DisplayMode.Height));

            this.Initalize();
            this.InitalizePlayer(content, properties);
        }

        /// <summary>
        /// Initalizes the player.
        /// Note: Invoked after the Initalize method.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="properties"></param>
        protected virtual void InitalizePlayer(ContentManager content, RPGGameProperties properties)
        {
            // Add the main player.
            Player player = new Player(ContentManagerUtilities.LoadTexture2D(content, properties.PlayerTexturePath));
            player.Speed = 1f;
            _entities.Add("MainPlayer", player);

            _camera.SetEntityTarget(player);
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
            this.GetEntity<Player>("MainPlayer").Bounds = _camera.Bounds;

            this.ProcessMapSpawns();
        }

        /// <summary>
        /// Populates the screen with any entities or game objects specified by the map's spawn information.
        /// </summary>
        protected virtual void ProcessMapSpawns()
        {
            foreach (var gameObjectEntry in this.CurrentMap.SpawnInformation.GetGameObjects())
            {
                this.AddGameObject(gameObjectEntry.Value, gameObjectEntry.Key);
            }
        }

        public void AddEntity(IEntity entity, string name)
        {
            _entities.Add(name, entity);
        }

        public void RemoveEntity(string name)
        {
            _entities.Remove(name);
        }

        public void RemoveEntity(IEntity entity)
        {
            var name = _entities.FirstOrDefault(x => x.Value == entity).Key;

            this.RemoveGameObject(name);
        }

        public IEntity[] GetEntities()
        {
            var values = new IEntity[_entities.Count];

            _entities.Values.CopyTo(values, 0);

            return values;
        }

        public T GetEntity<T>(string name) where T : IEntity
        {
            IEntity value;

            if (_entities.TryGetValue(name, out value))
            {
                if (value.GetType() == typeof(T))
                {
                    return (T)value;
                }
            }

            return default(T);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _mapSpriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, _camera.GetTransformation(_graphicsDevice));

            if (this.CurrentMap != null)
                this.CurrentMap.Draw(_mapSpriteBatch);

            foreach (var entity in _entities.Values)
                entity.Draw(_mapSpriteBatch);

            foreach (var gameObject in this.GetGameObjects())
                gameObject.Draw(_mapSpriteBatch);

            _mapSpriteBatch.End();

            this.GUIHandler.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var entity in _entities.Values)
                entity.Update(gameTime);

            foreach (var gameObject in this.GetGameObjects())
            {
                gameObject.Update(gameTime);
            }

            this.GUIHandler.Update(gameTime);
        }
    }
}