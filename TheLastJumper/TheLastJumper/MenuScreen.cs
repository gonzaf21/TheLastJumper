/* Gonzalo Martinez Font - The Last Jumper 2018
 * 
 * V0.04: Implent the background, loop of the chosen option and write text.
 * 
 * V0.01: Basic skeleton of the MenuScreen class.
 */
using System;
using Tao.Sdl;

namespace TheLastJumper
{
    class MenuScreen : Screen
    {
        protected Image imgMenu;
        protected Font font;
        public int chosenOption = 1;

        public MenuScreen(Hardware hardware) : base(hardware)
        {
            font = new Font("gameData/chargen.ttf",18);
            imgMenu = new Image("gameData/menuTitle.png",800,600);
        }

        public override void Show()
        {
            bool spaceEnter = false;
            do
            {
                hardware.ClearScreen();
                hardware.DrawImage(imgMenu);
                hardware.WriteText("Play", 398,250, 255, 0, 0, font);
                hardware.WriteText("Load level", 398, 280, 255, 0, 0, font);
                hardware.WriteText("Credit", 398, 310, 255, 0, 0, font);
                hardware.WriteText("Exit", 398, 330, 255, 0, 0, font);
                hardware.UpdateScreen();

                


                int keyPressed = hardware.KeyPressed();

                if(keyPressed == Hardware.KEY_UP && chosenOption > 1)
                {
                    chosenOption--;
                }
                else if(keyPressed == Hardware.KEY_DOWN && chosenOption < 4)
                {
                    chosenOption++;
                }
                else if (keyPressed == Hardware.KEY_SPACE)
                {
                    spaceEnter = true;
                }

            } while (!spaceEnter);
        }

        public int ChooseOption()
        {
            return chosenOption;
        }
    }
}
