/* Gonzalo Martinez Font - The Last Jumper 2018
 * 
 * V0.10: Second implementation of the class with some changes to show an image
 * and some text.
 * 
 * V0.01: Basic skeleton of the ScoreboardScreen class.
 */

namespace TheLastJumper
{
    class ScoreBoard : Screen
    {
        protected Image imgScore;
        protected Font font;
        protected string playerName;
        protected int playerScore;

        public ScoreBoard(Hardware hardware) : base(hardware)
        {
            font = new Font("gameData/AgencyFB.ttf", 70);
            imgScore = new Image("gameData/scoreImg.png", 800, 600);
        }

        public override void Show()
        {
            int keyPressed;

            do
            {
                keyPressed = hardware.KeyPressed();

                hardware.ClearScreen();

                hardware.DrawImage(imgScore);
                hardware.WriteText("GMF - 420 points", 150, 20, 204, 0,
                    0, font);
                hardware.WriteText("VTA - 21 points", 150, 120, 204, 0,
                    0, font);
                hardware.WriteText("MGA - 1100 points", 150, 220, 204, 0,
                    0, font);

                hardware.UpdateScreen();
            } while (keyPressed != Hardware.KEY_ESC);
        }
    }
}
