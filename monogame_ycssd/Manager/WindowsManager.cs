using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monogame_ycssd.Manager
{
    public class WindowsManager
    {
        #region Variables
        private static WindowsManager instance;

        private static int _screenWidth = 1200;
        private static int _screenHeight = 900;
        #endregion

        #region Properties
        public int ScreenWidth
        {
            get { return _screenWidth; }
        }
        public int ScreenHeight
        {
            get { return _screenHeight; }
        }
        #endregion

        #region Method  
        public static WindowsManager GetInstance()
        {
            if (instance != null)
            {
                return instance;
            }
            return instance = new WindowsManager();
        }
        #endregion

    }
}
