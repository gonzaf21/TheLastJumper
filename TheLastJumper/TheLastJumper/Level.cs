/* Gonzalo Martinez Font - The Last Jumper 2018
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

        public Level(string levelFile)
        {
            Blocks = new List<Block>();
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
                    int numWidth;
                    do
                    {
                        numWidth = 0;
                        line = fileReader.ReadLine();
                        if(line != null)
                        {
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
                                    // Setting the origin view of the map
                                    XMap = (short)(i * Block.SPRITE_WIDTH);
                                    YMap = (short)(numHeight *
                                        Block.SPRITE_HEIGHT);
                                }
                                else if(elements[i] == "E")
                                {
                                    XEnd = (short)(i * Block.SPRITE_WIDTH);
                                    YEnd = (short)(numHeight *
                                        Block.SPRITE_HEIGHT);
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
        public void SaveLevel()
        {
            // TODO
        }
    }
}
