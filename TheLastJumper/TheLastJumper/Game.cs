/* Gonzalo Martinez Font - The Last Jumper 2018
 * 
 * V0.01: Class Game created. An object GameController is created and
 * it runs the method Start() from it.
 */

namespace TheLastJumper
{
    class Game
    {
        static void Main(string[] args)
        {
            GameController controller = new GameController();
            controller.Start();
        }
    }
}
