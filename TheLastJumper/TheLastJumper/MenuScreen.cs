/* Gonzalo Martinez Font - The Last Jumper 2018
 * 
 * V0.08: Added icon to draw when moving between options, changed the font, 
 * changed the ChosenOption to be a public attribute.
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
        public int ChosenOption { get; set; }
        protected Image imgMenu;
        protected Font font;
        protected Image menuIcon;
        protected short iconYPos;

        public MenuScreen(Hardware hardware) : base(hardware)
        {
            font = new Font("gameData/AgencyFB.ttf", 70);
            imgMenu = new Image("gameData/menuTitle.png", 800, 600);
            menuIcon = new Image("gameData/menuIcon.png", 59, 60);
            ChosenOption = 1;
            iconYPos = 250;
        }

        public override void Show()
        {
            bool spacePressed = false;
            int keyPressed;

            do
            {
                // Drawing the basic text 
                hardware.ClearScreen();

                hardware.DrawImage(imgMenu);
                hardware.DrawSprite(menuIcon, 180, iconYPos, 0, 0, 59, 60);
                hardware.WriteText("Play", 250, 250, 204, 0, 0, font);
                hardware.WriteText("Load level", 250, 320, 204, 0, 0, font);
                hardware.WriteText("Credits", 250, 390, 204, 0, 0, font);
                hardware.WriteText("Exit", 250, 460, 204, 0, 0, font);

                keyPressed = hardware.KeyPressed();

                if (keyPressed == Hardware.KEY_UP && ChosenOption > 1)
                {
                    ChosenOption--;
                    iconYPos -= 70;
                }
                else if (keyPressed == Hardware.KEY_DOWN && ChosenOption < 4)
                {
                    ChosenOption++;
                    iconYPos += 70;
                }
                else if (keyPressed == Hardware.KEY_SPACE)
                {
                    spacePressed = true;
                }

                hardware.UpdateScreen();

            } while (!spacePressed);
        }
    }
}
