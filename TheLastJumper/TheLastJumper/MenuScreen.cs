/* Gonzalo Martinez Font - The Last Jumper 2018
 * 
 * V0.11: Changed the font color and added the controls of this screen.
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
        protected Image controls;
        protected short iconYPos;

        public MenuScreen(Hardware hardware) : base(hardware)
        {
            font = new Font("gameData/AgencyFB.ttf", 70);
            imgMenu = new Image("gameData/menuTitle.png", 800, 600);
            menuIcon = new Image("gameData/menuIcon.png", 59, 60);
            controls = new Image("gameData/controls.png", 256, 546);
            ChosenOption = 1;
            iconYPos = 220;
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
                hardware.DrawSprite(controls, 520, 540, 0, 0, 256, 60);
                hardware.WriteText("Play", 250, 220, 204, 255, 153, font);
                hardware.WriteText("Load level", 250, 290, 204, 255, 153, 
                    font);
                hardware.WriteText("Scoreboard", 250, 360, 204, 255, 153, 
                    font);
                hardware.WriteText("Credits", 250, 430, 204, 255, 153, font);
                hardware.WriteText("Exit", 250, 500, 204, 255, 153, font);

                keyPressed = hardware.KeyPressed();

                if (keyPressed == Hardware.KEY_UP && ChosenOption > 1)
                {
                    ChosenOption--;
                    iconYPos -= 70;
                }
                else if (keyPressed == Hardware.KEY_DOWN && ChosenOption < 5)
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
