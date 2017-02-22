using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GDAPS2Game
{
    class Menu
    {
        private int selectionIndex;//determines which button is selected. 0=play/resume game, 1=exit
        KeyboardState kState;


        public Menu(KeyboardState kState)
        {
            selectionIndex = 0;
        }

        public void Input()
        {
            
        }
    }
}
