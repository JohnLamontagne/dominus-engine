using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominus_Utilities
{
    public class InputHelper
    {
        #region Singleton

        // Singleton
        private static InputHelper _inputHelper;

        public static InputHelper GetHelper()
        {
            return _inputHelper ?? (_inputHelper = new InputHelper());
        }

        #endregion Singleton

        private char _characterPressed;

        private KeyboardState _lastKeyState;

        public void Update(GameTime gameTime)
        {
            var keyState = Keyboard.GetState();

            if (_lastKeyState.GetPressedKeys().Length == 0)
            {
                _lastKeyState = keyState;
                return;
            }

            this.CheckForAlphabeticalInput(keyState);


            // We have to check for symbol (shift + N) before we check for numbers so that numbers aren't given priority over symbols.
            if (_characterPressed == '\0')
                this.CheckForSpecialInput(keyState);



            if (_characterPressed == '\0')
                this.CheckForNumericalInput(keyState);


            _lastKeyState = keyState;
        }

        private void CheckForSpecialInput(KeyboardState keyState)
        {
            if (this.IsKeyDown(Keys.Space))
            {
                _characterPressed = ' ';
            }
            else if (this.IsKeyDown(Keys.Back))
            {
                _characterPressed = '\b';
            }
            else if ((keyState.IsKeyDown(Keys.LeftShift) || keyState.IsKeyDown(Keys.LeftShift)))
            {
                if (this.IsKeyDown(Keys.D1))
                    _characterPressed = '!';
                else if (this.IsKeyDown(Keys.D2))
                    _characterPressed = '@';
                else if (this.IsKeyDown(Keys.D3))
                    _characterPressed = '#';
                else if (this.IsKeyDown(Keys.D4))
                    _characterPressed = '$';
                else if (this.IsKeyDown(Keys.D5))
                    _characterPressed = '%';
                else if (this.IsKeyDown(Keys.D6))
                    _characterPressed = '^';
                else if (this.IsKeyDown(Keys.D7))
                    _characterPressed = '&';
                else if (this.IsKeyDown(Keys.D8))
                    _characterPressed = '*';
                else if (this.IsKeyDown(Keys.D9))
                    _characterPressed = '(';
                else if (this.IsKeyDown(Keys.D0))
                    _characterPressed = ')';
            }
        }

        private void CheckForNumericalInput(KeyboardState keyState)
        {
            for (int i = (int)Keys.D0; i < (int)Keys.D9; i++)
            {
                if (this.IsKeyDown((Keys)i))
                {
                    _characterPressed = (char)((Keys)i);
                    break;
                }
            }

            if (keyState.IsKeyDown(Keys.LeftShift) || keyState.IsKeyDown(Keys.RightShift))
            {
                _characterPressed = char.ToUpper(_characterPressed);
            }
            else
            {
                _characterPressed = char.ToLower(_characterPressed);
            }
        }

        private void CheckForAlphabeticalInput(KeyboardState keyState)
        {
            for (int i = (int)Keys.A; i < (int)Keys.Z; i++)
            {
                if (this.IsKeyDown((Keys)i))
                {
                    _characterPressed = (char)((Keys)i);
                    break;
                }
            }

            if (keyState.IsKeyDown(Keys.LeftShift) || keyState.IsKeyDown(Keys.RightShift))
            {
                _characterPressed = char.ToUpper(_characterPressed);
            }
            else
            {
                _characterPressed = char.ToLower(_characterPressed);
            }

        }

        public char GetCharacterPressed()
        {
            var charPressed = _characterPressed;

            _characterPressed = '\0';

            return charPressed;
        }

        private bool IsKeyDown(Keys key)
        {
            var keyState = Keyboard.GetState();

            return (keyState.IsKeyUp(key) && _lastKeyState.IsKeyDown(key));
        }
    }
}