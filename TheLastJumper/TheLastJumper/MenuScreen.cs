/* Gonzalo Martinez Font - The Last Jumper 2018
 * 
 * V0.14: Added option to change between languages with the L key.
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
        protected Font fontSmall;
        protected Image menuIcon;
        protected Image controls;
        protected short iconYPos;

        public MenuScreen(Hardware hardware) : base(hardware)
        {
            font = new Font("gameData/AgencyFB.ttf", 70);
            fontSmall = new Font("gameData/AgencyFB.ttf", 40);
            imgMenu = new Image("gameData/menuTitle.png", 800, 600);
            menuIcon = new Image("gameData/menuIcon.png", 59, 60);
            controls = new Image("gameData/controls.png", 256, 1092);
            ChosenOption = 1;
            iconYPos = 220;
        }

        public override void Show()
        {
            bool spacePressed = false;
            int keyPressed;
            string[] texts = { "Play", "Load Level", "Scoreboard", "Credits",
                "Exit", "L = Language", "Jugar", "Cargar nivel",
                "Marcadores", "Créditos", "Salir", "L = Lenguaje"};

            do
            {
                // Drawing the basic text 
                hardware.ClearScreen();

                hardware.DrawImage(imgMenu);
                hardware.DrawSprite(menuIcon, 180, iconYPos, 0, 0, 59, 60);                

                DrawTexts(Game.language, texts);

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
                else if(keyPressed == Hardware.KEY_L)
                {
                    Game.SetNextLanguage();
                }
                
                hardware.UpdateScreen();

            } while (!spacePressed);
        }

        public void DrawTexts(string l, string[] texts)
        {
            if(l == "eng")
            {
                hardware.DrawSprite(controls, 520, 540, 0, 0, 256, 60);
                hardware.WriteText(texts[0], 250, 220, 204, 255, 153, font);
                hardware.WriteText(texts[1], 250, 290, 204, 255, 153,
                    font);
                hardware.WriteText(texts[2], 250, 360, 204, 255, 153,
                    font);
                hardware.WriteText(texts[3], 250, 430, 204, 255, 153, font);
                hardware.WriteText(texts[4], 250, 500, 204, 255, 153, font);
                hardware.WriteText(texts[5], 555, 490, 255, 255, 255, 
                    fontSmall);
            }
            else if(l == "esp")
            {
                hardware.DrawSprite(controls, 520, 540, 0, 544, 256, 60);
                hardware.WriteText(texts[6], 250, 220, 204, 255, 153, font);
                hardware.WriteText(texts[7], 250, 290, 204, 255, 153,
                    font);
                hardware.WriteText(texts[8], 250, 360, 204, 255, 153,
                    font);
                hardware.WriteText(texts[9], 250, 430, 204, 255, 153, font);
                hardware.WriteText(texts[10], 250, 500, 204, 255, 153, font);
                hardware.WriteText(texts[11], 555, 490, 255, 255, 255, 
                    fontSmall);
            }
        }
    }
}
