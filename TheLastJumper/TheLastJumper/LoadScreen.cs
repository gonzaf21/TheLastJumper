/* Gonzalo Martinez Font - The Last Jumper 2018
 * 
 * V0.09: Ability to select and display the level that is going to load.
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
using Tao.Sdl;

namespace TheLastJumper
{
    class LoadScreen : Screen
    {
        public short NumLevel { get; set; }
        protected Font font;
        protected List<string> levelsCleared;
        protected Image imageBG;

        public LoadScreen(Hardware hardware) : base(hardware)
        {
            font = new Font("gameData/AgencyFB.ttf", 60);
            levelsCleared = LoadSelectableLevels();
            imageBG = new Image("gameData/loadImg.png", 800, 600);
            NumLevel = 0;
        }

        public override void Show()
        {
            int totalLevels = levelsCleared.Count;
            short posXText = 20;
            short posYText = 20;
            int key;
            IntPtr[] levelNames = new IntPtr[levelsCleared.Count];

            /*
            for (int i = 0; i < totalLevels; i++)
            {
                if (posXText >= 600)
                {
                    posYText += 40;
                    posXText = 20;
                }
                hardware.WriteText(levelsCleared[i], posXText, posYText,
                    255, 0, 0, font);
                posXText += 100;
            }*/

            do
            {
                hardware.ClearScreen();
                hardware.DrawImage(imageBG);

                for (int i = 0; i < totalLevels; i++)
                {
                    if (posXText >= 600)
                    {
                        posYText += 40;
                        posXText = 20;
                    }
                    levelNames[i] = SdlTtf.TTF_RenderText_Solid(
                        font.GetFontType(), levelsCleared[NumLevel],
                        hardware.red);
                    hardware.WriteTextRender(levelNames[i], posXText,
                        posYText);
                    posXText += 100;
                }


                key = hardware.KeyPressed();

                if (key == Hardware.KEY_RIGHT && NumLevel < 
                    levelsCleared.Count - 1)
                {
                    NumLevel++;
                    
                    levelNames[NumLevel] = SdlTtf.TTF_RenderText_Solid(
                        font.GetFontType(), levelsCleared[NumLevel], 
                        hardware.white);

                    // Send the level selected to be loaded
                }

                if(key == Hardware.KEY_LEFT && NumLevel > 0)
                {
                    NumLevel--;
                    levelNames[NumLevel] = SdlTtf.TTF_RenderText_Solid(
                        font.GetFontType(), levelsCleared[NumLevel],
                        hardware.white);
                }            

                hardware.UpdateScreen();

            } while (key != Hardware.KEY_ESC);
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
