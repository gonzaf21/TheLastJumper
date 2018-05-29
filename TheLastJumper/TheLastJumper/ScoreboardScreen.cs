/* Gonzalo Martinez Font - The Last Jumper 2018
 * 
 * V0.12:
 * 
 * V0.11: Added the struct for future versions, changed the colors and image 
 * and displayed the possible keys to use on this screen. 
 * 
 * V0.10: Second implementation of the class with some changes to show an image
 * and some text.
 * 
 * V0.01: Basic skeleton of the ScoreboardScreen class.
 */

using System;
using System.IO;
using System.Collections.Generic;

namespace TheLastJumper
{
    struct InfoPlayer
    {
        public string playerName;
        public int playerScore;
        public string level;
    }

    class ScoreBoard : Screen
    {
        protected Image imgScore;
        protected Image controls;
        protected Font font;
        protected List<InfoPlayer> info;

        public ScoreBoard(Hardware hardware) : base(hardware)
        {
            font = new Font("gameData/AgencyFB.ttf", 70);
            imgScore = new Image("gameData/scoreImg.png", 800, 600);
            controls = new Image("gameData/controls.png", 256, 546);
            info = new List<InfoPlayer>();
        }

        public override void Show()
        {
            int keyPressed;

            do
            {
                keyPressed = hardware.KeyPressed();

                hardware.ClearScreen();

                hardware.DrawImage(imgScore);
                hardware.DrawSprite(controls, 700, 540, 0, 115, 60, 50);
                hardware.WriteText("SCORES", 320, 10, 255, 204, 0, font);
                hardware.WriteText("---------", 320, 40, 255, 204, 0, font);
                hardware.WriteText("GMF - 420 points", 150, 100, 204, 0,
                    0, font);
                hardware.WriteText("VTA - 21 points", 150, 200, 204, 0,
                    0, font);
                hardware.WriteText("MGA - 1100 points", 150, 300, 204, 0,
                    0, font);

                hardware.UpdateScreen();
            } while (keyPressed != Hardware.KEY_ESC);
        }

        public void LoadScore()
        {
            string fileName = "gameData/levels/scores.txt";
            if (!File.Exists(fileName))
                Console.WriteLine("Can't find the scores file!");
            else
            {
                try
                {
                    StreamReader fileReader = File.OpenText(fileName);
                    string line;

                    do
                    {
                        line = fileReader.ReadLine();
                        if(line != null)
                        {
                            string[] split = line.Split(';');
                            
                            InfoPlayer infoP;
                            infoP.playerName = split[0];
                            infoP.playerScore = Convert.ToInt32(split[1]);
                            infoP.level = split[2];

                            info.Add(infoP);
                        }
                    } while (line != null);

                    fileReader.Close();
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("File of the scores not found.");
                }
                catch (PathTooLongException)
                {
                    Console.WriteLine("The path to search this file is too" +
                        " long...");
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
        }

        public void SaveScore(string newInfo)
        {
            string fileName = "gameData/levels/scores.txt";
            string[] splitInfo = newInfo.Split(';');

            try
            {
                StreamWriter fileWriter = File.AppendText(fileName);

                // Checks that the player and level is not already in the list
                for(int i = 0; i < info.Count; i++)
                {
                    if (info[i].playerName != splitInfo[0]
                        && info[i].level != splitInfo[2])
                    {
                        fileWriter.WriteLine(splitInfo[0] + ";" +
                            splitInfo[1] + ";" + splitInfo[2]);
                    }
                    else if(info[i].playerName == splitInfo[0]
                        && info[i].level == splitInfo[2]
                        && info[i].playerScore < Convert.ToInt32(splitInfo[1]))
                    {
                        info.RemoveAt(i);
                        fileWriter.WriteLine(splitInfo[0] + ";" +
                            splitInfo[1] + ";" + splitInfo[2]);
                    }
                }
                fileWriter.Close();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File of the scores not found.");
            }
            catch (PathTooLongException)
            {
                Console.WriteLine("The path to search this file is too" +
                    " long...");
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
    }
}
