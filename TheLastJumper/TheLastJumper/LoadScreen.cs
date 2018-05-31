/* Gonzalo Martinez Font - The Last Jumper 2018
 * 
 * V0.11: Draw the keys that the user can use on this screen.
 * 
 * V0.09: Ability to select and display all the levels cleared by reading
 * from the file and changing between them to load the one we want.
 * Added DrawInColor method.
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
        protected Image controls;

        public LoadScreen(Hardware hardware) : base(hardware)
        {
            font = new Font("gameData/AgencyFB.ttf", 50);
            levelsCleared = LoadSelectableLevels();
            imageBG = new Image("gameData/loadImg.png", 800, 600);
            controls = new Image("gameData/controls.png", 256, 1092);
            NumLevel = 0;
        }

        public override void Show()
        {
            int totalLevels = levelsCleared.Count;
            short posXText = 250;
            short posYText = 20;
            int key;
            IntPtr[] levelNames = new IntPtr[totalLevels];
            short[] posX = new short[totalLevels];
            short[] posY = new short[totalLevels];

            // Setting the texts and its coordinates in different arrays
            for (int i = 0; i < totalLevels; i++)
            {
                if (posXText + 50 >= 600)
                {
                    posYText += 80;
                    posXText = 250;
                }

                posX[i] = posXText;
                posY[i] = posYText;

                DrawInColor((short)i, levelNames, "red");

                posXText += 230;
            }

            do
            {
                hardware.ClearScreen();
                hardware.DrawImage(imageBG);
                hardware.DrawSprite(controls, 520, 540, 0, 60, 256, 50);
                
                for(int i = 0; i < totalLevels; i++)
                {
                    hardware.WriteTextRender(levelNames[i], posX[i],
                        posY[i]);
                }

                // Setting the first level displayed as selected
                DrawInColor(NumLevel, levelNames, "white");

                key = hardware.KeyPressed();

                if (key == Hardware.KEY_RIGHT && NumLevel < 
                    levelsCleared.Count - 1)
                {
                    NumLevel++;
                    DrawInColor(NumLevel, levelNames, "white");
                    DrawInColor((short)(NumLevel - 1), levelNames, "red");
                }

                if (key == Hardware.KEY_LEFT && NumLevel > 0)
                {
                    NumLevel--;
                    DrawInColor(NumLevel, levelNames, "white");
                    DrawInColor((short)(NumLevel + 1), levelNames, "red");
                }

                hardware.UpdateScreen();

            } while (key != Hardware.KEY_SPACE);
        }

        // Draws the text of the level selected in the chosen color
        public void DrawInColor(short num, IntPtr[] levelNames, string color)
        {
            if (color == "red")
            {
                levelNames[num] = SdlTtf.TTF_RenderText_Solid(
                font.GetFontType(), levelsCleared[num].Substring(3),
                hardware.red);
            }
            else if(color == "white")
            {
                levelNames[num] = SdlTtf.TTF_RenderText_Solid(
                font.GetFontType(), levelsCleared[num].Substring(3),
                hardware.white);
            }
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
