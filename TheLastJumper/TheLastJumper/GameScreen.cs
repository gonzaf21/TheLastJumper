﻿/* Gonzalo Martinez Font - The Last Jumper 2018
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

        public GameScreen(Hardware hardware) : base(hardware)
        {
            // TODO: this is just for test purposes
            background = new Image("gameData/forestbg2x.png", 1280, 960);
            level = new Level("gameData/levels/leveltest.txt");
            character = new Character(hardware);
            character.MoveTo(level.XStart, level.YStart);
            font40 = new Font("gameData/chargen.ttf", 40);
            font18 = new Font("gameData/chargen.ttf", 18);
            previousTime = 0;
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
                
                hardware.DrawSprite(background, 0, 0, 0, 0, 
                    GameController.SCREEN_WIDTH, GameController.SCREEN_HEIGHT);

                hardware.DrawSprite(character.SpriteSheet,(short)(character.X),
                    (short)(character.Y), character.SpriteX, character.SpriteY, 
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

                // Drawing the blocks from the level
                foreach(Block b in level.blocks)
                {
                    hardware.DrawSprite(Block.spriteBlock, (short)b.X, 
                        (short)b.Y, b.XToDraw, b.YToDraw, Block.SPRITE_WIDTH, 
                        Block.SPRITE_HEIGHT);
                }

                // Updating the screen
                hardware.UpdateScreen();

                // Move character
                character.MoveCharacter();

                // Update delta time
                hardware.UpdateDeltaTime(ref currentTime, ref previousTime);

                //Sleep threading
                //Thread.Sleep(10);

            } while (!hardware.IsKeyPressed(Hardware.KEY_ESC));
        }
    }
}