using Dominus_RPG_Core;
using Dominus_RPG_Core.ScreenManagement;
using Dominus_RPG_Core.World.WorldStructure;
using Dominus_Utilities;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace Main_Test_Project.Screens
{
    internal class GameScreen : RPGScreen
    {
        private bool _doneLoading;

        public GameScreen(ContentManager content, RPGGameProperties properties)
            : base(content, properties)
        {
            // this.GUIHandler.Load(@"C:\Users\General\Documents\GitHub\dominus-engine\Test Projects\Main test project\Main Test Project\Main Test Project\Main Test ProjectContent\b.xml", content);
        }

        protected override void Initalize()
        {
            new Thread(x =>
               {
                   Map map = Map.Load(@"C:\Users\General\Desktop\untitled.tmx", this.Content);
                   this.ChangeMap(map);
                   _doneLoading = true;
                   Console.WriteLine("DONE");
               }).Start();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}