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
        public int SelectionIndex { get { return selectionIndex; } }
        KeyboardState kState;
        KeyboardState oldKState;
        public Menu()
        {
            selectionIndex = 0;
        }

        public void Input()
        {
            kState = Keyboard.GetState();
            if(kState.IsKeyDown(Keys.Up) && oldKState.IsKeyUp(Keys.Up))
            {
                if (selectionIndex != 0)//0 is top of menu
                {
                    selectionIndex -= 1;
                }
                else if (selectionIndex < 0)
                {
                    selectionIndex = 0;
                    //maybe play a sound indicating you cant go higher in the menu
                }
            }

            if (kState.IsKeyDown(Keys.Down) && oldKState.IsKeyUp(Keys.Down))
            {
                selectionIndex += 1;
            }
            //else if (selectionIndex > maxValue)
            //{
            //    selectionIndex = maxValue;
            //}

            //not gonna check for too high values, as we might need to add more menu options
            //When we're sure we don't need more menu options we can uncomment the code

            //check for selection input, i.e. enter press
            


            oldKState = kState;//oldKState should be last called always
        }

        
    }
}
