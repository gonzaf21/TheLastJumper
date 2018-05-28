/* Gonzalo Martinez Font - The Last Jumper 2018
 * 
 * V0.11: Changed the background, the colors and displayed the controls.
 * 
 * V0.10: Implemented some changes to show an image and text.
 * 
 * V0.01: Basic skeleton of the CreditsScreen class.
 */

namespace TheLastJumper
{
    class CreditsScreen : Screen
    {
        protected Image imgCredits;
        protected Image controls;
        protected Font font;

        public CreditsScreen(Hardware hardware) : base(hardware)
        {
            font = new Font("gameData/AgencyFB.ttf", 70);
            imgCredits = new Image("gameData/creditsImg.png", 800, 600);
            controls = new Image("gameData/controls.png", 256, 546);
        }

        public override void Show()
        {
            int keyPressed;

            do
            {
                keyPressed = hardware.KeyPressed();

                hardware.ClearScreen();

                hardware.DrawImage(imgCredits);
                hardware.DrawSprite(controls, 700, 540, 0, 115, 60, 50);
                hardware.WriteText("CREDITS", 320, 10, 0, 51, 102, font);
                hardware.WriteText("---------", 320, 40, 0, 51, 102, font);
                hardware.WriteText("Gonzalo Martinez Font", 150, 300, 0, 153,
                    204, font);
                hardware.WriteText("Copyright - Trikamo TM. 2018", 50, 400,
                    0, 153, 204, font);
                hardware.WriteText("Private property", 
                    205, 500, 0, 153, 204, font);

                hardware.UpdateScreen();
            } while (keyPressed != Hardware.KEY_ESC);
        }
    }
}
