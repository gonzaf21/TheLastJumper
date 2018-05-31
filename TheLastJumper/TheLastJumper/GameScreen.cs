/* Gonzalo Martinez Font - The Last Jumper 2018
 * 
 * V0.13: Some changes to draw the enemies and the collision of the character
 * with them.
 * 
 * V0.12: Added the call to the method to save the score and changed the
 * visibility of some attributes of this class.
 * 
 * V0.11: Added a timer to tell the time passed, improved the collisions, 
 * added points and time in game and displayed an image of the controls.
 * 
 * V0.10: Changes to draw collectibles, the exit door and enemies in game, as
 * well as adding another constructor to load levels in a better way.
 * 
 * V0.08: Minor changes and adding the credits screen after completing all 
 * levels. Changed the font.
 * 
 * V0:07: Setting and drawing the traps from the level, minor corrections
 * on death of the player to use it with these traps. Some minor changes and
 * additions.
 * 
 * V0.06: Changes to fix the collision system. Addition to set the level
 * into the character's own object level so the level scroll calculations
 * can be done in its movemente method. Moved the character's own collision
 * method before the call of the movement method in order to fix the collision
 * detection. Death addition and implementation of the transition between 
 * levels.
 * 
 * V0.05: Added a call to CollidesWith() to calculate collisions between 
 * character and the blocks.
 * 
 * V0.03: Some changes to test the loading of levels and using the blocks list
 * from levels, as well as setting the X and Y of the level to the player as 
 * a beginning of the scroll in the screen.
 * 
 * V0.02: Added the call to UpdateDeltaTime() to calculate the time between 
 * frames and two float variables so it can be calculated.
 * 
 * V0.01: First version of the GameScreen class. Setting the game loop
 * and loading basic sprites, moving the character with its sprites animated
 * and drawing a text.
 */

using Tao.Sdl;
using System;
using System.Threading;

namespace TheLastJumper
{
    class GameScreen : Screen
    {
        protected Image background;
        protected Character character;
        protected Font font18;
        protected float previousTime;
        protected float currentTime;
        protected Level level;
        protected bool gameOver;
        public static short numOfTheLevel;
        protected Image doorImg;
        protected Image controls;
        public static int points;
        protected int time;
        protected IntPtr textPoints, textTime;

        public GameScreen(Hardware hardware, short numLevel) : base(hardware)
        {
            background = new Image("gameData/forestbg2x.png", 2553, 1487);
            controls = new Image("gameData/controls.png", 256, 1092);
            Level.SetLevels();
            numOfTheLevel = numLevel;
            level = new Level(Level.GetLevelName(numOfTheLevel));
            character = new Character(hardware);
            character.Level = level;
            character.MoveTo(level.XStart, level.YStart);
            font18 = new Font("gameData/AgencyFB.ttf", 28);
            previousTime = 0;
            gameOver = false;
            doorImg = new Image("gameData/door.png", 200, 267);
            points = 0;
            time = 0;
            textTime = SdlTtf.TTF_RenderText_Solid(font18.GetFontType(),
                "TIME: ", hardware.red);
            textPoints = SdlTtf.TTF_RenderText_Solid(font18.GetFontType(),
                "POINTS:", hardware.red);
        }
        
        // Method to update the time with the timer
        protected void CountTime(Object o)
        {
            time++;
        }

        public override void Show()
        {
            // First time initialized
            currentTime = Sdl.SDL_GetTicks();
            Timer timer = new Timer(this.CountTime, null, 1000, 1000);
            do
            {
                // Clearing the screen
                hardware.ClearScreen();

                // Draw sprites
                hardware.DrawSprite(background, 0, 0, level.XMap, level.YMap, 
                    GameController.SCREEN_WIDTH, GameController.SCREEN_HEIGHT);

                hardware.DrawSprite(doorImg, (short)(level.XEnd - level.XMap),
                    (short)(level.YEnd - level.YMap), 0, 0,
                    60, 60);

                hardware.DrawSprite(character.SpriteSheet,(short)(character.X -
                    level.XMap), (short)(character.Y - level.YMap), 
                    character.SpriteX, character.SpriteY, 
                    Character.SPRITE_WIDTH, Character.SPRITE_HEIGHT);

                if(Game.language == "eng")
                    hardware.DrawSprite(controls, 540, 10, 0, 441, 256, 105);
                else if(Game.language == "esp")
                    hardware.DrawSprite(controls, 540, 10, 0, 986, 256, 105);

                // Points and time
                hardware.WriteTextRender(textPoints, 70, 10);
                hardware.WriteTextRender(textTime, 200, 10);

                textTime = SdlTtf.TTF_RenderText_Solid(font18.GetFontType(),
                    "TIME: " + time, hardware.red);
                textPoints = SdlTtf.TTF_RenderText_Solid(font18.GetFontType(),
                    "POINTS:" + points, hardware.red);

                // Drawing the blocks, traps, collectibles and enemies 
                // from the level.
                foreach(Block b in level.Blocks)
                {
                    hardware.DrawSprite(Block.SpriteBlock, (short)(b.X - 
                        level.XMap), (short)(b.Y - level.YMap), b.XToDraw, 
                        b.YToDraw, Block.SPRITE_WIDTH, Block.SPRITE_HEIGHT);
                }

                foreach(Trap t in level.Traps)
                {
                    hardware.DrawSprite(Trap.SpriteTrap, (short)(t.X - 
                        level.XMap), (short)(t.Y - level.YMap), t.XToDraw,
                        t.YToDraw, Trap.SPRITE_WIDTH, Trap.SPRITE_HEIGHT);
                }

                foreach (Collectible c in level.Collectibles)
                {
                    hardware.DrawSprite(Collectible.SpriteCollectible, 
                        (short)(c.X - level.XMap), (short)(c.Y - level.YMap), 
                        c.XToDraw, c.YToDraw, Collectible.SPRITE_WIDTH, 
                        Collectible.SPRITE_HEIGHT);
                }

                // Drawing the enemies
                foreach (Enemy e in level.Enemies)
                {
                    if (e is GroundEnemy)
                    {
                        hardware.DrawSprite(GroundEnemy.SpriteGroundEnemy,
                        (short)(((GroundEnemy)(e)).X - level.XMap),
                        (short)(((GroundEnemy)(e)).Y - level.YMap),
                        0, 0, GroundEnemy.SPRITE_WIDTH,
                        GroundEnemy.SPRITE_HEIGHT);
                    }
                    else if (e is FlyingEnemy)
                    {
                        hardware.DrawSprite(FlyingEnemy.SpriteFlyingEnemy,
                        (short)(e.X - level.XMap), (short)(e.Y - level.YMap),
                        0, 0, FlyingEnemy.SPRITE_WIDTH,
                        FlyingEnemy.SPRITE_HEIGHT);
                    }
                }

                // Updating the screen
                hardware.UpdateScreen();

                // Collisions with blocks
                foreach (Block b in level.Blocks)
                {
                    if (character.CollidesWith(b.X, b.Y, Block.SPRITE_WIDTH,
                        Block.SPRITE_HEIGHT))
                    {
                        character.IsMovingLeft = false;
                        character.IsMovingRight = false;
                        character.IsOver = false;

                        if(b.X > character.X)
                        {
                            character.IsMovingLeft = true;
                            if(character.IsJumping)
                            {
                                character.IsJumping = false;
                                character.IsFalling = false;
                                character.OnTheWall = true;
                            }
                        }
                        else if(b.X < character.X)
                        {
                            character.IsMovingRight = true;
                            if(character.IsJumping)
                            {
                                character.IsJumping = false;
                                character.IsFalling = false;
                                character.OnTheWall = true;
                            }
                        }
                    }
                    else if(character.CollidesWith(b.X, b.Y, 
                       Block.SPRITE_WIDTH, Block.SPRITE_HEIGHT) 
                       && b.Y <= character.Y + Character.SPRITE_HEIGHT)
                    {
                        character.IsFalling = false;
                        character.IsOver = true;
                        character.MoveTo(character.X, b.Y -
                            Character.SPRITE_HEIGHT);
                    }
                }

                // Enemies colliding with blocks
                foreach(Block b in level.Blocks)
                {
                    foreach(Enemy e in level.Enemies)
                    {
                        if (e.CollidesWith(b.X, b.Y, Block.SPRITE_WIDTH,
                            Block.SPRITE_HEIGHT))
                            e.Collided = true;
                    }
                }

                // Collision of enemies with the character
                foreach (Enemy e in level.Enemies)
                {
                    if (e.CollidesWith(character.X, character.Y,
                        Character.SPRITE_WIDTH, Character.SPRITE_HEIGHT))
                        character.IsDead = true;
                }
                

                // Traps that kill the character
                foreach (Trap t in level.Traps)
                {
                    if (character.CollidesWith(t.X, t.Y - t.HitboxHeight,
                        Trap.SPRITE_WIDTH, Trap.SPRITE_HEIGHT))
                        character.IsDead = true;
                }

                // Picks the collectibles and adds the score to the player
                for(int i = 0; i < level.Collectibles.Count; i++)
                {
                    if(character.CollidesWith(level.Collectibles[i].X,
                        level.Collectibles[i].Y, Collectible.SPRITE_WIDTH,
                        Collectible.SPRITE_HEIGHT))
                    {
                        points += 50;
                        level.Collectibles.RemoveAt(i);
                    }
                }

                // Move character and enemies
                character.MoveCharacter();

                Enemy.SetCharacter(character);
                foreach(Enemy e in level.Enemies)
                {
                    e.MoveEnemy();
                }

                // Set the value of the booleans of movement after collisions
                character.IsMovingLeft = true;
                character.IsMovingRight = true;
                character.IsOver = false;

                // Character's death
                if (character.IsDead)
                {
                    hardware.WriteText("You are dead...", (short)character.X,
                        (short)character.Y, 255, 0, 0, font18);
                    Thread.Sleep(100);
                    character.IsDead = false;
                    character.IsFalling = false;
                    character.IsJumping = false;
                    character.MoveTo(level.XStart, level.YStart);

                    // Setting the scroll at the beginning of the level
                    level.XMap = (short)(level.XStart - 
                        (2 * Block.SPRITE_WIDTH));
                    level.YMap = level.YStart;
                }
                
                // Transition between levels
                if (character.CollidesWith(level.XEnd, level.YEnd,
                    Block.SPRITE_WIDTH, Block.SPRITE_HEIGHT))
                {
                    // Saving the progression of the levels
                    level.SaveLevel(numOfTheLevel);

                    // Saves the player's name and score
                    ScoreBoard score = new ScoreBoard(hardware);
                    score.EnterScore();

                    numOfTheLevel++;

                    // Loading the new level
                    level = new Level(Level.GetLevelName(numOfTheLevel));
                    character.Level = level;
                    character.MoveTo(level.XStart, level.YStart);
                }

                // End of the game, all levels cleared
                if(gameOver)
                {
                    numOfTheLevel = 0;
                    // Show the credits after finishing the game
                    CreditsScreen credits = new CreditsScreen(hardware);
                    credits.Show();
                }

                // Update delta time
                hardware.UpdateDeltaTime(ref currentTime, ref previousTime);

                //Sleep threading (It was disabled meanwhile)
                Thread.Sleep(4);

            } while (!hardware.IsKeyPressed(Hardware.KEY_ESC));
        }
    }
}
