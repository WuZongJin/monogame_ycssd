using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monogame_ycssd.Input
{
    public class KeyboardObject
    {

        #region Variables
        private static KeyboardState _previousKeyboardState, _currentKeyBoardState;
        private static Keys _up;
        private static Keys _down;
        private static Keys _right;
        private static Keys _left;
        private static Keys _changeweapon;
        private static Keys _skill;
        private static Keys _roll;
        #endregion

        #region Method
        public static void Init()
        {
            _up = Keys.W;
            _down = Keys.S;
            _right = Keys.D;
            _left = Keys.A;
            _changeweapon = Keys.Q;
            _skill = Keys.E;
            _roll = Keys.Space;
        }

        public static bool KeyUpPressed()
        {
            return (_currentKeyBoardState.IsKeyDown(_up));
        }
        public static bool KeyDownPressed()
        {
            return (_currentKeyBoardState.IsKeyDown(_down));
        }
        public static bool KeyRightPressed()
        {
            return (_currentKeyBoardState.IsKeyDown(_right));
        }
        public static bool KeyLeftPressed()
        {
            return (_currentKeyBoardState.IsKeyDown(_left));
        }

        public static bool KeyChangeWeapon()
        {
            return (_currentKeyBoardState.IsKeyUp(_changeweapon));
        }
        public static bool KeySkill()
        {
            return (_currentKeyBoardState.IsKeyDown(_skill)
                && !_previousKeyboardState.IsKeyDown(_skill));
        }
        public static bool KeyRoll()
        {
            return (_currentKeyBoardState.IsKeyDown(_roll)
                && !_previousKeyboardState.IsKeyDown(_roll));
        }

        public static void Update()
        {
            _previousKeyboardState = _currentKeyBoardState;
            _currentKeyBoardState = Keyboard.GetState();
        }
        #endregion

    }
}
