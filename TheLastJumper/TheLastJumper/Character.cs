﻿/* Gonzalo Martinez Font - The Last Jumper 2018
 * 
 * V0.05: Some changes to the jump phisics and added the sprite of the movement
 * to the left of the character.
 * 
 * V0.03: Little changes to the jump.
 * 
 * V0.02: Added gravity, speed in x-axis and y-axis and implementation of 
 * the delta time with all of this in the movement and jump.
 * 
 * V0.01: First version of the Character class. With the attributes necessary
 * and the movement implemented, as well as the setup of the sprites.
 */

namespace TheLastJumper
{
    class Character : MovableSprite
    {
        public const byte STEP_LENGTH = 5;
        public const byte SPRITE_WIDTH = 64;
        public const byte SPRITE_HEIGHT = 64;
        public const byte MAX_VERTICAL_SPEED = 35;

        public bool IsDead { get; set; }
        public bool IsJumping { get; set; }
        public bool IsOver { get; set; }
        public bool IsFalling { get; set; }
        public bool IsMoving { get; set; }
        public Image SpriteSheet { get; set; }
        public float SpeedX { get; set; }
        public float SpeedY { get; set; }

        protected Hardware hardware;
        
        public Character(Hardware hardware)
        {
            SpriteSheet = 
                new Image("gameData/platformer_sprites.png", 512, 576);
            this.hardware = hardware;
            IsDead = false;
            IsFalling = false;
            IsJumping = false;
            IsOver = false;
            IsMoving = true;
            HitboxWidth = 40;
            HitboxHeight = 40;
            SpeedX = 0;
            SpeedY = 0;

            // Setting the appropiate sprites
            SpriteXCoordinates[(int)MovableSprite.SpriteMovement.LEFT] =
                new int[] { 0, 64, 128, 192, 256, 320, 384, 448 };
            SpriteYCoordinates[(int)MovableSprite.SpriteMovement.LEFT] =
                new int[] { 576, 576, 576, 576, 576, 576, 576, 576 };

            SpriteXCoordinates[(int)MovableSprite.SpriteMovement.UP] =
                new int[] { 128, 192, 256, 320, 384, 448, 0, 64 };
            SpriteYCoordinates[(int)MovableSprite.SpriteMovement.UP] =
                new int[] { 320, 320, 320, 320, 320, 320, 384, 384 };

            SpriteXCoordinates[(int)MovableSprite.SpriteMovement.RIGHT] =
                new int[] { 256, 320, 384, 448, 0, 64, 128, 192 };
            SpriteYCoordinates[(int)MovableSprite.SpriteMovement.RIGHT] =
                new int[] { 0, 0, 0, 0, 64, 64, 64, 64 };

            SpriteXCoordinates[(int)MovableSprite.SpriteMovement.DOWN] =
                new int[] { 128, 192, 256, 320, 384, 448, 0, 64 };
            SpriteYCoordinates[(int)MovableSprite.SpriteMovement.DOWN] =
                new int[] { 320, 320, 320, 320, 320, 320, 384, 384 };

            SpriteXCoordinates[(int)MovableSprite.SpriteMovement.STOOD] =
                new int[] { 0 };
            SpriteYCoordinates[(int)MovableSprite.SpriteMovement.STOOD] =
                new int[] { 512 };

            UpdateSpriteCoordinates();
        }

        public void MoveCharacter()
        {
            bool leftPressed = hardware.IsKeyPressed(Hardware.KEY_LEFT);
            bool rightPressed = hardware.IsKeyPressed(Hardware.KEY_RIGHT);
            bool upPressed = hardware.IsKeyPressed(Hardware.KEY_SPACE);
            bool resetPressed = hardware.IsKeyPressed(Hardware.KEY_SHIFT);
            float gravity = 1.8f;

            // Jumping calculations
            if (IsJumping)
            {
                this.Animate(MovableSprite.SpriteMovement.UP);
                if (SpeedY > -MAX_VERTICAL_SPEED)
                {
                    SpeedY += -gravity * (hardware.DeltaTime / 10);
                    this.Y += SpeedY;

                    if (rightPressed)
                    {
                        SpeedX += STEP_LENGTH * (hardware.DeltaTime / 30);
                        this.X += SpeedX;
                    }
                    else if (leftPressed)
                    {
                        SpeedX += STEP_LENGTH * (hardware.DeltaTime / 30);
                        this.X -= SpeedX;
                    }
                }
                else
                {
                    IsJumping = false;
                    IsFalling = true;
                }
            }

            // Falling calculations
            if (IsFalling)
            {
                this.Animate(MovableSprite.SpriteMovement.DOWN);
                SpeedY += STEP_LENGTH * (hardware.DeltaTime / 10);
                if (this.Y < GameController.SCREEN_HEIGHT - SPRITE_HEIGHT)
                {
                    this.Y += SpeedY;
                    if (rightPressed)
                    {
                        SpeedX += STEP_LENGTH * (hardware.DeltaTime / 30);
                        this.X += SpeedX;
                    }
                    else if (leftPressed)
                    {
                        SpeedX += STEP_LENGTH * (hardware.DeltaTime / 30);
                        this.X -= SpeedX;
                    }
                }
                else if (this.Y + SPRITE_HEIGHT >= 
                    GameController.SCREEN_HEIGHT)
                {
                    IsFalling = false;
                    SpeedY = 0;
                    SpeedX = 0;
                    this.MoveTo(this.X,
                        GameController.SCREEN_HEIGHT - SPRITE_HEIGHT);
                }
            }

            // Checking jump
            if (upPressed && !IsJumping)
                IsJumping = true;

            // Left movement
            if (leftPressed && IsMoving)
            {
                this.Animate(MovableSprite.SpriteMovement.LEFT);
                SpeedX = STEP_LENGTH * (hardware.DeltaTime / 30);
                if (this.X + SpeedX > 0)
                {
                    this.X -= SpeedX;
                }
            }

            // Right movement
            if (rightPressed && IsMoving)
            {
                this.Animate(MovableSprite.SpriteMovement.RIGHT);
                SpeedX = STEP_LENGTH * (hardware.DeltaTime / 30);
                if (this.X + SpeedX < 800 - HitboxWidth)
                {
                    this.X += SpeedX;
                }
            }

            // To reset the position of the player  to a visible point of the
            // screen while testing the movement.
            if (resetPressed)
                this.MoveTo(50, 536);
        }
    }
}
