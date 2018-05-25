/* Gonzalo Martinez Font - The Last Jumper 2018
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
        protected Font font;

        public CreditsScreen(Hardware hardware) : base(hardware)
        {
            font = new Font("gameData/AgencyFB.ttf", 70);
            imgCredits = new Image("gameData/creditsImg.png", 800, 600);
        }

        public override void Show()
        {
            int keyPressed;

            do
            {
                keyPressed = hardware.KeyPressed();

                hardware.ClearScreen();

                hardware.DrawImage(imgCredits);
                hardware.WriteText("Gonzalo Martinez Font", 150, 20, 204, 0,
                    0, font);
                hardware.WriteText("Copyright - GMF Company 2018", 10, 250,
                    204, 0, 0, font);
                hardware.WriteText("Private property...", 
                    10, 500, 204, 0, 0, font);

                hardware.UpdateScreen();
            } while (keyPressed != Hardware.KEY_ESC);
        }
    }
}
