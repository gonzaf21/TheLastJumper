/* Gonzalo Martinez Font - The Last Jumper 2018
 * 
 * V0.11: Added the struct for future versions, changed the colors and image 
 * and displayed the possible keys to use on this screen. 
 * 
 * V0.10: Second implementation of the class with some changes to show an image
 * and some text.
 * 
 * V0.01: Basic skeleton of the ScoreboardScreen class.
 */

using System.Collections.Generic;

namespace TheLastJumper
{
    struct infoPlayer
    {
        public string playerName;
        public int playerScore;
    }

    class ScoreBoard : Screen
    {
        protected Image imgScore;
        protected Image controls;
        protected Font font;
        protected List<infoPlayer> info;

        public ScoreBoard(Hardware hardware) : base(hardware)
        {
            font = new Font("gameData/AgencyFB.ttf", 70);
            imgScore = new Image("gameData/scoreImg.png", 800, 600);
            controls = new Image("gameData/controls.png", 256, 546);
            info = new List<infoPlayer>();
        }

        public override void Show()
        {
            int keyPressed;

            do
            {
                keyPressed = hardware.KeyPressed();

                hardware.ClearScreen();

                hardware.DrawImage(imgScore);
                hardware.DrawSprite(controls, 700, 540, 0, 115, 60, 50);
                hardware.WriteText("SCORES", 320, 10, 255, 204, 0, font);
                hardware.WriteText("---------", 320, 40, 255, 204, 0, font);
                hardware.WriteText("GMF - 420 points", 150, 100, 204, 0,
                    0, font);
                hardware.WriteText("VTA - 21 points", 150, 200, 204, 0,
                    0, font);
                hardware.WriteText("MGA - 1100 points", 150, 300, 204, 0,
                    0, font);

                hardware.UpdateScreen();
            } while (keyPressed != Hardware.KEY_ESC);
        }
    }
}
