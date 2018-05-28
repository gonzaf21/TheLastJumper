/* Gonzalo Martinez Font - The Last Jumper 2018
 * 
 * V0.11: Changed the way of loading the level with the same constructor.
 * 
 * V0.10: Added the option to load a selected level.
 * 
 * V0.09: Fixed the menu so we can work on the load screen.
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
            bool exit = false;

            WelcomeScreen welcome = new WelcomeScreen(hardware);
            welcome.Show();
            MenuScreen menu = new MenuScreen(hardware);

            do
            {
                menu.Show();

                switch (menu.ChosenOption)
                {
                    case 1:
                        GameScreen game = new GameScreen(hardware, 0);
                        game.Show();
                        break;

                    case 2:
                        LoadScreen load = new LoadScreen(hardware);
                        load.Show();

                        // To load the selected level
                        GameScreen gameLoaded = new GameScreen(hardware,
                            load.NumLevel);
                        gameLoaded.Show();
                        break;

                    case 3:
                        ScoreBoard score = new ScoreBoard(hardware);
                        score.Show();
                        break;

                    case 4:
                        CreditsScreen credits = new CreditsScreen(hardware);
                        credits.Show();
                        break;                    

                    default:
                        exit = true;
                        break;
                }
            } while (!exit);
        }
    }
}
