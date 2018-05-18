/* Gonzalo Martinez Font - The Last Jumper 2018
 * 
 * V0.04 : Added some changes to add the intro and menu screens.
 *
 * V0.03: Some changes to prepare the transition between screens.
 * 
 * V0.01: First version of the GameController class. Basic skeleton to change 
 * between the different screens that the game will have.
 */

namespace TheLastJumper
{
    class GameController
    {
        public const short SCREEN_WIDTH = 800;
        public const short SCREEN_HEIGHT = 600;

        public void Start()
        {
            Hardware hardware = new Hardware(SCREEN_WIDTH, SCREEN_HEIGHT,
                24, false);

            WelcomeScreen welcome = new WelcomeScreen(hardware);
            welcome.Show();
            MenuScreen menu = new MenuScreen(hardware);

            do
            {
                menu.Show();
                GameScreen game = new GameScreen(hardware);
                game.Show();

            } while (!hardware.IsKeyPressed(Hardware.KEY_ESC));
        }
    }
}
