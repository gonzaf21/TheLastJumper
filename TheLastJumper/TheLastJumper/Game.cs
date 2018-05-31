/* Gonzalo Martinez Font - The Last Jumper 2018
 * 
 * V0.14: Added the languages option.
 * 
 * V0.01: Class Game created. An object GameController is created and
 * it runs the method Start() from it.
 */

namespace TheLastJumper
{
    class Game
    {
        protected static string[] languagesList = { "eng", "esp" };
        public static string language;
        protected static int count = 0;

        static void Main(string[] args)
        {
            language = languagesList[count];

            GameController controller = new GameController();
            controller.Start();
        }

        // Setting the next language from the languages available in the array
        public static void SetNextLanguage()
        {
            count++;
            if(languagesList.Length - 1 >= count)
                language = languagesList[count];
            else
            {
                count = 0;
                language = languagesList[count];
            }
        }
    }
}
