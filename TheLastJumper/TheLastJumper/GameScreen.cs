/* Gonzalo Martinez Font - The Last Jumper 2018
 * 
 * V0.08: Minor changes and adding the credits screen after completing all 
 * levels.
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
using System.Threading;

namespace TheLastJumper
{
    class GameScreen : Screen
    {
        protected Image background;
        protected Character character;
        protected Font font40;
        protected Font font18;
        protected float previousTime;
        protected float currentTime;
        protected Level level;
        protected bool gameOver;
        protected short numOfTheLevel;

        public GameScreen(Hardware hardware) : base(hardware)
        {
            // TODO: this is just for test purposes
            background = new Image("gameData/forestbg2x.png", 2553, 1487);
            numOfTheLevel = 0;
            level = new Level(Level.GetLevelName(numOfTheLevel));
            character = new Character(hardware);
            character.Level = level;
            character.MoveTo(level.XStart, level.YStart);
            font40 = new Font("gameData/chargen.ttf", 40);
            font18 = new Font("gameData/chargen.ttf", 18);
            previousTime = 0;
            gameOver = false;
        }

        public override void Show()
        {
            // First time initialized
            currentTime = Sdl.SDL_GetTicks();
            do
            {
                // Clearing the screen
                hardware.ClearScreen();

                // Draw sprites
                hardware.DrawSprite(background, 0, 0, level.XMap, level.YMap, 
                    GameController.SCREEN_WIDTH, GameController.SCREEN_HEIGHT);

                hardware.DrawSprite(character.SpriteSheet,(short)(character.X -
                    level.XMap), (short)(character.Y - level.YMap), 
                    character.SpriteX, character.SpriteY, 
                    Character.SPRITE_WIDTH, Character.SPRITE_HEIGHT);

                // Testing the text and the drawing of blocks from an image
                hardware.WriteText("LArrow -> Move Left", 80, 65, 255, 0,
                    0, font18);
                hardware.WriteText("RArrow -> Move Right", 80, 95, 255, 0,
                    0, font18);
                hardware.WriteText("Space -> Jump", 80, 125, 255, 0,
                    0, font18);
                hardware.WriteText("Esc -> Exit Game", 80, 155, 255, 0,
                    0, font18);
                hardware.WriteText("Shift -> Reset position", 80, 185, 255, 0,
                    0, font18);
                hardware.WriteText("The Last Jumper", 250, 140, 153, 0, 
                    153, font40);

                // Drawing the blocks and traps from the level
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

                // Updating the screen
                hardware.UpdateScreen();

                // Collisions
                foreach (Block b in level.Blocks)
                {
                    character.CollidesWithMovement(b.X, b.Y, Block.SPRITE_WIDTH,
                        Block.SPRITE_HEIGHT);
                }

                foreach(Trap t in level.Traps)
                {
                    if (character.CollidesWith(t.X, t.Y - t.HitboxHeight,
                        Trap.SPRITE_WIDTH, Trap.SPRITE_HEIGHT))
                        character.IsDead = true;
                }

                // Move character
                character.MoveCharacter();

                // Set the value of the booleans of movement after collisions
                character.IsMovingLeft = true;
                character.IsMovingRight = true;

                // Character's death
                if (character.IsDead)
                {
                    hardware.WriteText("You are dead...", (short)character.X,
                        (short)character.Y, 255, 0, 0, font40);
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
                if (character.X + character.HitboxWidth >= level.XEnd && 
                    character.X <= level.XEnd && character.Y + 
                    character.HitboxHeight >= level.YEnd && character.Y <=
                    level.YEnd)
                {
                    // Saving the progression of the levels
                    level.SaveLevel(numOfTheLevel);
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
