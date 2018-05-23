/* Gonzalo Martinez Font - The Last Jumper 2018
 * 
 * V0.08: Some changes in the implementation of this class: Added a method 
 * to get a list of the names of the level completed and started to draw
 * and write things.
 * 
 * V0.01: Basic skeleton of the LoadScreen class.
 */

using System;
using System.IO;
using System.Collections.Generic;

namespace TheLastJumper
{
    class LoadScreen : Screen
    {
        protected Font font;
        protected List<string> levelsCleared;
        protected Image imageBG;

        public LoadScreen(Hardware hardware) : base(hardware)
        {
            font = new Font("gameData/AgencyFB.txt", 30);
            levelsCleared = LoadSelectableLevels();
            imageBG = new Image("gameData/loadImg.png", 800, 600);
        }

        public override void Show()
        {
            int totalLevels = levelsCleared.Count;
            short numLevel = 0;
            short posXText = 20;
            short posYText = 20;

            hardware.ClearScreen();
            hardware.DrawImage(imageBG);

            for(int i = 0; i < totalLevels; i++)
            {
                posXText += 40;
                if (posXText >= 600)
                    posYText += 40;

                hardware.WriteText("Level " + numLevel, posXText, posYText, 
                    255, 0, 0, font);
            }

            hardware.UpdateScreen();
            do
            {
                // TODO
                hardware.UpdateScreen();
            } while (!hardware.IsKeyPressed(Hardware.KEY_SPACE));
        }

        // Method to read the levels cleared from the file
        public List<string> LoadSelectableLevels()
        {
            List<string> levelsC = new List<string>();
            string fileName = "gameData/levels/levelsCleared.txt";

            if (!File.Exists(fileName))
                Console.WriteLine("Can't load the levels cleared!");
            else
            {
                try
                {
                    StreamReader fileReader = File.OpenText(fileName);
                    string line;

                    do
                    {
                        line = fileReader.ReadLine();
                        if (line != null)
                        {
                            levelsC.Add(line);
                        }
                    } while (line != null);

                    fileReader.Close();
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("File not found.");
                }
                catch (PathTooLongException)
                {
                    Console.WriteLine("The path is too long...");
                }
                catch (IOException e)
                {
                    Console.WriteLine("IO error... --> " + e.Message);
                }
                catch (Exception e)
                {
                    // A log file will be used in the end
                    Console.WriteLine("My bad! An error ocurred --> " +
                        e.Message);
                }
            }

            return levelsC;
        }
    }
}
