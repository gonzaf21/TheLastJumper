/* Gonzalo Martinez Font - The Last Jumper 2018
 * 
 * V0.04: Implement the background.
 * 
 * V0.01: Basic skeleton of the WelcomeScreen class.
 */
using System.Threading;
namespace TheLastJumper
{
    class WelcomeScreen : Screen
    {
        protected Image imgWelcome;
        protected Font font;

        public WelcomeScreen(Hardware hardware) : base(hardware)
        {
            imgWelcome = new Image("gameData/esbr.png", 1024, 768);
        }

        public override void Show()
        {
            hardware.DrawImage(imgWelcome);
            hardware.UpdateScreen();
            Thread.Sleep(3000);
        }
    }
}
