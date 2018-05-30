/* Gonzalo Martinez Font - The Last Jumper 2018
 * 
 * V0.13: Fixed some bugs with the save system.
 * 
 * V0.11: Added new traps with different orientation and a method to 
 * read the level files and load them on the array rather than having them
 * static and predefined.
 * 
 * v0.10: Added enemies and collectibles.
 * 
 * V0.08: Finished the saving of the levels on a file and only writing the 
 * names of the level to check them in the Load Screen.
 * 
 * V0.07: Adding the list of traps to use it, creating and loading them. Also,
 * set the origin of the view from a letter in the file readed.
 * Beginning of the save system for the levels cleared, so changes on the
 * way of getting the name of the levels were made.
 * 
 * V0.06: Adding the list of the enemies for the upcoming versions.
 * 
 * V0.03: Reading from a file and setting the different elements of the level 
 * based on this.
 * 
 * V0.01: Basic skeleton of the Level class.
 */

using System;
using System.IO;
using System.Collections.Generic;

namespace TheLastJumper
{
    class Level
    {
        public short Width { get; set; }
        public short Height { get; set; }
        public short XMap { get; set; }
        public short YMap { get; set; }
        public short XStart { get; set; }
        public short YStart { get; set; }
        public short XEnd { get; set; }
        public short YEnd { get; set; }
        public List<Block> Blocks { get; set; }
        public List<Enemy> Enemies { get; set; }
        public List<Trap> Traps { get; set; }
        public List<Collectible> Collectibles { get; set; }
        protected static string[] levelNames;

        public Level(string levelFile)
        {
            Blocks = new List<Block>();
            Traps = new List<Trap>();
            Enemies = new List<Enemy>();
            Collectibles = new List<Collectible>();
            XMap = 0;
            YMap = 0;
            LoadLevel(levelFile);
        }

        // Loading a level and setting the obstacles, blocks and enemies that
        // contains in the correct position.
        public void LoadLevel(string levelFile)
        {
            if(!File.Exists(levelFile))
                Console.WriteLine("This level doesn't exist, so it couldn't" + 
                    "be loaded...");
            else
            {
                try
                {
                    StreamReader fileReader = File.OpenText(levelFile);
                    string line;
                    int numHeight = 0;
                    int numWidth = 0;
                    do
                    {
                        line = fileReader.ReadLine();
                        if(line != null)
                        {
                            numWidth = 0;
                            string[] elements = line.Split(',');
                            for(int i = 0; i < elements.Length; i++)
                            {
                                if (elements[i] == "B")
                                {
                                    Blocks.Add(
                                        new Block(i * Block.SPRITE_WIDTH,
                                        numHeight * Block.SPRITE_HEIGHT, 0));
                                }
                                else if (elements[i] == "S")
                                {
                                    XStart = (short)(i * Block.SPRITE_WIDTH);
                                    YStart = (short)(numHeight * 
                                        Block.SPRITE_HEIGHT);
                                }
                                else if(elements[i] == "E")
                                {
                                    XEnd = (short)(i * Block.SPRITE_WIDTH);
                                    YEnd = (short)(numHeight *
                                        Block.SPRITE_HEIGHT);
                                }
                                else if(elements[i] == "T")
                                {
                                    Traps.Add(new Trap(i * Block.SPRITE_WIDTH,
                                        numHeight * Block.SPRITE_HEIGHT, 's'));
                                }
                                else if (elements[i] == "Q")
                                {
                                    Traps.Add(new Trap(i * Block.SPRITE_WIDTH,
                                        numHeight * Block.SPRITE_HEIGHT, 'l'));
                                }
                                else if (elements[i] == "W")
                                {
                                    Traps.Add(new Trap(i * Block.SPRITE_WIDTH,
                                        numHeight * Block.SPRITE_HEIGHT, 'w'));
                                }
                                else if (elements[i] == "Y")
                                {
                                    Traps.Add(new Trap(i * Block.SPRITE_WIDTH,
                                        numHeight * Block.SPRITE_HEIGHT, 'y'));
                                }
                                else if(elements[i] == "X")
                                {
                                    // Setting the origin view of the map
                                    XMap = (short)(i * Block.SPRITE_WIDTH);
                                    YMap = (short)(numHeight *
                                        Block.SPRITE_HEIGHT);
                                    Blocks.Add(
                                        new Block(i * Block.SPRITE_WIDTH,
                                        numHeight * Block.SPRITE_HEIGHT, 0));
                                }
                                else if(elements[i] == "C")
                                {
                                    Collectibles.Add(
                                        new Collectible(i * Block.SPRITE_WIDTH,
                                        numHeight * Block.SPRITE_HEIGHT));
                                }
                                else if(elements[i] == "F")
                                {
                                    Enemies.Add(new GroundEnemy(
                                        (short)(i * Enemy.SPRITE_WIDTH), 
                                        (short)(numHeight * 
                                        Block.SPRITE_HEIGHT)));
                                }
                                else if(elements[i] == "V")
                                {
                                    Enemies.Add(new FlyingEnemy(
                                        (short)(i * Enemy.SPRITE_WIDTH),
                                        (short)(numHeight *
                                        Block.SPRITE_HEIGHT)));
                                }
                                numWidth++;
                            }
                            numHeight++;
                        }
                    } while (line != null);

                    Width = (short)(numWidth * Block.SPRITE_WIDTH);
                    Height = (short)(numHeight * Block.SPRITE_HEIGHT);

                    fileReader.Close();    
                }
                catch(FileNotFoundException)
                {
                    Console.WriteLine("File of the level not found.");
                }
                catch(PathTooLongException)
                {
                    Console.WriteLine("The path to search this level is too" +
                        " long...");
                }
                catch(IOException e)
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

        // Method to save the progression of the levels cleared
        public void SaveLevel(short levelNum)
        {
            try
            {
                // Checking first the levels written on the file
                StreamReader fileReader = File.OpenText(
                    "gameData/levels/levelsCleared.txt");
                List<string> auxLevels = new List<string>();
                bool check = false;
                string line;

                do
                {
                    line = fileReader.ReadLine();
                    if (line != null)
                        auxLevels.Add(line);
                } while (line != null);

                fileReader.Close();

                // Comparing the level names
                foreach(string s in auxLevels)
                {
                    if (s == levelNames[levelNum].Substring(
                    levelNames[levelNum].LastIndexOf("/") + 1,
                    levelNames[levelNum].LastIndexOf(".") -
                    (levelNames[levelNum].LastIndexOf("/") + 1)))
                    {
                        check = true;
                    }
                }

                if(!check)
                {
                    StreamWriter fileWriter = File.AppendText(
                        "gameData/levels/levelsCleared.txt");
                    int pos = levelNames[levelNum].LastIndexOf("/") + 1;
                    fileWriter.WriteLine(
                        (levelNames[levelNum]).Substring(pos,
                            levelNames[levelNum].LastIndexOf(".") - pos));
                    fileWriter.Close();
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found.");
            }
            catch (PathTooLongException)
            {
                Console.WriteLine("The path to save this level is too" +
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

        // To get the path name of the level to load
        public static string GetLevelName(short num)
        {
            return levelNames[num];
        }

        // To set the array of levels
        public static void SetLevels()
        {
            string[] allFiles = Directory.GetFiles("gameData/levels");
            levelNames = new string[allFiles.Length];
            for (int i = 0; i < allFiles.Length; i++)
            {
                if (allFiles[i].Contains("_") && allFiles[i].EndsWith(".txt"))
                {
                    int pos = allFiles[i].LastIndexOf("\\") + 1;
                    levelNames[i] = "gameData/levels/" +
                        allFiles[i].Substring(pos);
                }
            }
        }
    }
}
