using Dominus_Graphics.GUI;
using Dominus_Graphics.GUI.Widgets;
using GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using XNAGameConsole;

namespace Dominus_Core.Screens
{
    public class MainMenuScreen : GameScreen
    {
        private GUIHandler _guiHandler;

        private Texture2D _menuBackground;




        public MainMenuScreen()
        {
            _guiHandler = new GUIHandler(this.ScreenManager.Font);
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            base.HandleInput(gameTime, input);
        }

        public override void Activate(bool instancePreserved)
        {
            if (!instancePreserved)
            {



                SpriteFont font = this.ScreenManager.Game.Content.Load<SpriteFont>("menuFont");

                _menuBackground = this.ScreenManager.Game.Content.Load<Texture2D>("menuBackground");

                var btnPlay = new Button(this.ScreenManager.Game.Content.Load<Texture2D>("idleButton"), this.ScreenManager.Font);
                btnPlay.Text = "Login";
                btnPlay.Position = new Vector2(300, 200);
                btnPlay.HoverTexture = this.ScreenManager.Game.Content.Load<Texture2D>("hoverButton");
                btnPlay.MouseDownTexture = btnPlay.HoverTexture;
                btnPlay.ButtonClicked += playButton_ButtonClicked;

                var btnRegister = new Button(this.ScreenManager.Game.Content.Load<Texture2D>("idleButton"), this.ScreenManager.Font);
                btnRegister.Text = "Register";
                btnRegister.Position = new Vector2(300, 275);
                btnRegister.HoverTexture = this.ScreenManager.Game.Content.Load<Texture2D>("hoverButton");
                btnRegister.MouseDownTexture = btnRegister.HoverTexture;
                btnRegister.ButtonClicked += registerButton_ButtonClicked;

                var btnExit = new Button(this.ScreenManager.Game.Content.Load<Texture2D>("idleButton"), this.ScreenManager.Font);
                btnExit.Text = "Exit";
                btnExit.Position = new Vector2(300, 350);
                btnExit.HoverTexture = this.ScreenManager.Game.Content.Load<Texture2D>("hoverButton");
                btnExit.MouseDownTexture = btnExit.HoverTexture;
                btnExit.ButtonClicked += btnExit_ButtonClicked;


                var txtLogin = new Textbox(this.ScreenManager.Game.Content.Load<Texture2D>("blankUsername"), font, this.ScreenManager.GraphicsDevice, 16);
                txtLogin.ForeColor = Color.White;
                txtLogin.Position = new Vector2(300, 200);
                txtLogin.TextOffset = new Vector2(8, 22);
                txtLogin.Hide();

                var txtPassword = new Textbox(this.ScreenManager.Game.Content.Load<Texture2D>("blankPassword"), font, this.ScreenManager.GraphicsDevice, 16);
                txtPassword.ForeColor = Color.White;
                txtPassword.Position = new Vector2(300, 280);
                txtPassword.TextOffset = new Vector2(8, 22);
                txtPassword.PasswordCharacter = '*';
                txtPassword.Hide();

                var lblLogin = new Label(this.ScreenManager.Font);
                lblLogin.Text = "Login: ";
                lblLogin.Position = new Vector2(210, 220);
                lblLogin.ForeColor = Color.White;
                lblLogin.Hide();

                var lblPassword = new Label(this.ScreenManager.Font);
                lblPassword.Text = "Password: ";
                lblPassword.Position = new Vector2(165, 300);
                lblPassword.ForeColor = Color.White;
                lblPassword.Hide();

                var btnSubmitLogin = new Button(this.ScreenManager.Game.Content.Load<Texture2D>("idleButton"), this.ScreenManager.Font);
                btnSubmitLogin.Text = "Submit";
                btnSubmitLogin.Position = new Vector2(300, 400);
                btnSubmitLogin.HoverTexture = this.ScreenManager.Game.Content.Load<Texture2D>("hoverButton");
                btnSubmitLogin.MouseDownTexture = btnSubmitLogin.HoverTexture;
                btnSubmitLogin.ButtonClicked += btnSubmitLogin_ButtonClicked;
                btnSubmitLogin.Visible = false;

                _guiHandler.AddWidget(txtLogin, "txtLogin");
                _guiHandler.AddWidget(txtPassword, "txtPassword");
                _guiHandler.AddWidget(btnPlay, "btnPlay");
                _guiHandler.AddWidget(lblLogin, "lblLogin");
                _guiHandler.AddWidget(lblPassword, "lblPassword");
                _guiHandler.AddWidget(btnRegister, "btnRegister");
                _guiHandler.AddWidget(btnExit, "btnExit");
                _guiHandler.AddWidget(btnSubmitLogin, "btnSubmitLogin");
            }

            base.Activate(instancePreserved);
        }

        void btnSubmitLogin_ButtonClicked(object sender, EventArgs e)
        {
            Console.WriteLine("Sending login request... NOT!");
        }

        void btnExit_ButtonClicked(object sender, EventArgs e)
        {
            if (_guiHandler.GetWidget<Button>("btnPlay").Visible)
                this.ScreenManager.Game.Exit();
            else
            {
                _guiHandler.GetWidget<Textbox>("txtLogin").Hide();
                _guiHandler.GetWidget<Label>("lblLogin").Hide();
                _guiHandler.GetWidget<Textbox>("txtPassword").Hide();
                _guiHandler.GetWidget<Label>("lblPassword").Hide();
                _guiHandler.GetWidget<Button>("btnPlay").Visible = true;
                _guiHandler.GetWidget<Button>("btnRegister").Visible = true;
                _guiHandler.GetWidget<Button>("btnSubmitLogin").Visible = false;

                var btnExit = _guiHandler.GetWidget<Button>("btnExit");
                btnExit.Text = "Exit";
                btnExit.Position = new Vector2(300, 350);
            }
        }

        void registerButton_ButtonClicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.openanimus.com");
        }

        private void playButton_ButtonClicked(object sender, EventArgs e)
        {
            _guiHandler.GetWidget<Textbox>("txtLogin").Show();
            _guiHandler.GetWidget<Label>("lblLogin").Show();
            _guiHandler.GetWidget<Textbox>("txtPassword").Show();
            _guiHandler.GetWidget<Label>("lblPassword").Show();
            _guiHandler.GetWidget<Button>("btnPlay").Visible = false;
            _guiHandler.GetWidget<Button>("btnRegister").Visible = false;
            _guiHandler.GetWidget<Button>("btnSubmitLogin").Visible = true;

            var btnExit = _guiHandler.GetWidget<Button>("btnExit");
            btnExit.Position = new Vector2(btnExit.Position.X, 500);
            btnExit.Text = "Back";
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (this.IsActive)
            {
                _guiHandler.Update(gameTime);
            }

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            if (this.IsActive)
            {
                this.ScreenManager.SpriteBatch.Begin();

                this.ScreenManager.SpriteBatch.Draw(_menuBackground, Vector2.Zero, Color.White);

                _guiHandler.Draw(this.ScreenManager.SpriteBatch);

                this.ScreenManager.SpriteBatch.End();
            }

            base.Draw(gameTime);
        }

        public override void Unload()
        {
            base.Unload();
        }
    }
}