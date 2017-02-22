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
        KeyboardState oldKState;

        public Menu(KeyboardState mainKState, KeyboardState mainOldKState)
        {
            selectionIndex = 0;
            kState = mainKState;
            oldKState = mainOldKState;
        }

        public void Input()
        {
            if(kState.IsKeyDown(Keys.Up) && oldKState.IsKeyUp(Keys.Up))
            {
                if (selectionIndex != 0)
                {
                    selectionIndex -= 1;
                }
                else
                {
                    selectionIndex = 0;
                    //maybe play a sound indicating you cant go higher in the menu
                }
            }

            if (kState.IsKeyDown(Keys.Down) && oldKState.IsKeyUp(Keys.Down))
            {
                if (selectionIndex != 1)
                {
                    selectionIndex += 1;
                }
                else
                {
                    selectionIndex = 1;
                    //maybe play a sound indicating you cant go lower in the menu
                }
            }
        }
    }
}
