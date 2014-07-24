using Dominus_Core.Graphics.GUI.Widgets;
using Dominus_Core.ScreenManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Main_Test_Project.Screens
{
    internal class MainMenuScreen : Screen
    {
        private SpriteFont _mainFont;

        public MainMenuScreen(ContentManager content)
            :base(content)
        {
            _mainFont = content.Load<SpriteFont>("menufont");

            this.InitalizeGUI();
        }

        private void InitalizeGUI()
        {
            this.GUIHandler.Load(@"C:\Users\John\Documents\GitHub\dominus-engine\Test Projects\Main test project\Main Test Project\Main Test Project\Main Test ProjectContent\TehTest.xml", this.Content);

            var buttonLogin = this.GUIHandler.GetWidget<Button>("btnTest");
            buttonLogin.ButtonClicked += buttonLogin_ButtonClicked;
        }

        void buttonLogin_ButtonClicked(object sender, EventArgs e)
        {
            Console.WriteLine("IT WORKS!!!");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}