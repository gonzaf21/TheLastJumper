/* Gonzalo Martinez Font - The Last Jumper 2018
 * 
 * V0.14: Added different languages in texts.
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
            controls = new Image("gameData/controls.png", 256, 1092);
        }

        public override void Show()
        {
            int keyPressed;
            string[] texts = { "CREDITS", "---------", "Private property",
                "CRÉDITOS", "-----------", "Propiedad privada"};

            do
            {
                keyPressed = hardware.KeyPressed();

                hardware.ClearScreen();

                hardware.DrawImage(imgCredits);
                hardware.DrawSprite(controls, 700, 540, 0, 115, 60, 50);
                DrawTexts(Game.language, texts);
                
                hardware.WriteText("Gonzalo Martinez Font", 150, 300, 0, 153,
                    204, font);
                hardware.WriteText("Copyright - Trikamo TM. 2018", 50, 400,
                    0, 153, 204, font);

                hardware.UpdateScreen();
            } while (keyPressed != Hardware.KEY_ESC);
        }

        public void DrawTexts(string l, string[] texts)
        {
            if(l == "eng")
            {
                hardware.WriteText(texts[0], 320, 10, 0, 51, 102, font);
                hardware.WriteText(texts[1], 320, 40, 0, 51, 102, font);
                hardware.WriteText(texts[2], 205, 500, 0, 153, 204, font);
            }
            else if (l == "esp")
            {
                hardware.WriteText(texts[3], 320, 10, 0, 51, 102, font);
                hardware.WriteText(texts[4], 320, 40, 0, 51, 102, font);
                hardware.WriteText(texts[5], 205, 500, 0, 153, 204, font);
            }
        }
    }
}
