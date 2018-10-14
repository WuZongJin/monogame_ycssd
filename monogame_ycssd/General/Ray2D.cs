using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monogame_ycssd.General
{
    public struct Ray2D
    {
        public Vector2 Position;
        public Vector2 Direction;

        #region Properties
    
        #endregion

        public Ray2D(Vector2 position,Vector2 direction)
        {
            Position = position;
            Direction = direction;
        }

        #region Method
        public bool Collision(Rectangle rectangle)
        {
            Vector2 temp = Position + Direction;
            if (rectangle.Contains(temp))
            {
                return true;
            }
            return false;
        }

        #endregion
    }
}
