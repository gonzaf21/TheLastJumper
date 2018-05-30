/* Gonzalo Martinez Font - The Last Jumper 2018
 * 
 * V0.11: Fixed some scroll bugs and improved the movement. Added the first 
 * version of the walljumping and deleted the alternate method to work with
 * the collisions.
 * 
 * V0.07: Fixed the scroll issues: it was a problem with the reading of the
 * level and some conditions that were using the width and height of the
 * gamecontroller.
 * 
 * V0.06: Added new attributtes to check and stop the movement after a 
 * collision is detected. Attribute Level included to use it for the scroll
 * with the movement of the character.
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
        public const byte MAX_VERTICAL_SPEED = 40;

        public bool IsDead { get; set; }
        public bool IsJumping { get; set; }
        public bool IsOver { get; set; }
        public bool IsFalling { get; set; }
        public bool IsMovingLeft { get; set; }
        public bool IsMovingRight { get; set; }
        public bool OnTheWall { get; set; }
        public Image SpriteSheet { get; set; }
        public float SpeedX { get; set; }
        public float SpeedY { get; set; }
        public Level Level { get; set; }

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
            IsMovingLeft = true;
            IsMovingRight = true;
            OnTheWall = false;
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

            SpriteXCoordinates[(int)MovableSprite.SpriteMovement.WALL] =
                new int[] { 256 };
            SpriteYCoordinates[(int)MovableSprite.SpriteMovement.WALL] =
                new int[] { 320 };

            SpriteXCoordinates[(int)MovableSprite.SpriteMovement.FALL] =
                new int[] { 64 };
            SpriteYCoordinates[(int)MovableSprite.SpriteMovement.FALL] =
                new int[] { 320 };

            UpdateSpriteCoordinates();
        }

        public void MoveCharacter()
        {
            bool leftPressed = hardware.IsKeyPressed(Hardware.KEY_LEFT);
            bool rightPressed = hardware.IsKeyPressed(Hardware.KEY_RIGHT);
            bool upPressed = hardware.IsKeyPressed(Hardware.KEY_SPACE);
            bool resetPressed = hardware.IsKeyPressed(Hardware.KEY_SHIFT);
            float gravity = 1.8f;

            // Walljumping
            if(OnTheWall)
            {
                this.Animate(MovableSprite.SpriteMovement.WALL);
                SpeedY = 1 * (hardware.DeltaTime / 10);
                if(this.Y + SpeedY < Level.Height - 
                    (SPRITE_HEIGHT + Block.SPRITE_HEIGHT))
                {
                    this.Y += SpeedY;
                    if(upPressed)
                    {
                        IsJumping = true;
                        OnTheWall = false;
                    }
                }
                else
                {
                    OnTheWall = false;
                    IsFalling = true;
                }
            }

            // Jumping calculations
            if (IsJumping)
            {
                this.Animate(MovableSprite.SpriteMovement.UP);
                if (SpeedY > -MAX_VERTICAL_SPEED)
                {
                    SpeedY += -gravity * (hardware.DeltaTime / 10);
                    this.Y += SpeedY;
                    
                    if (rightPressed && IsMovingRight)
                    {
                        SpeedX = STEP_LENGTH * (hardware.DeltaTime / 30);
                        this.X += SpeedX;
                        // Level scroll
                        if (Level.XMap < Level.Width - 
                            GameController.SCREEN_WIDTH)
                            Level.XMap += (short)SpeedX;
                    }
                    else if (leftPressed && IsMovingLeft)
                    {
                        SpeedX = STEP_LENGTH * (hardware.DeltaTime / 30);
                        this.X -= SpeedX;
                        // Level scroll
                        if (Level.XMap > 0)
                            Level.XMap -= (short)SpeedX;
                    }
                }
                else
                {
                    IsJumping = false;
                    IsFalling = true;
                }
            }

            // Falling calculations
            if (IsFalling && !IsOver)
            {
                this.Animate(MovableSprite.SpriteMovement.DOWN);
                SpeedY += STEP_LENGTH * (hardware.DeltaTime / 10);
                if (this.Y + SpeedY < Level.Height - 
                    (SPRITE_HEIGHT + Block.SPRITE_HEIGHT))
                {
                    this.Y += SpeedY;
                    
                    if (rightPressed && IsMovingRight)
                    {
                        SpeedX = STEP_LENGTH * (hardware.DeltaTime / 30) * 
                            gravity;
                        this.X += SpeedX;
                        // Level scroll
                        if (Level.XMap < Level.Width - 
                            GameController.SCREEN_WIDTH)
                            Level.XMap += (short)SpeedX;
                    }
                    else if (leftPressed && IsMovingLeft)
                    {
                        SpeedX = STEP_LENGTH * (hardware.DeltaTime / 30) * 
                            gravity;
                        this.X -= SpeedX;
                        // Level scroll
                        if (Level.XMap > 0)
                            Level.XMap -= (short)SpeedX;
                    }
                }
                else
                {
                    IsFalling = false;
                    SpeedY = 0;
                    MoveTo(X, (Level.Height - (Block.SPRITE_HEIGHT 
                        + SPRITE_HEIGHT)));
                    this.Animate(MovableSprite.SpriteMovement.FALL);
                }
            }

            // Checking if its over something
            if (IsOver)
            {
                IsJumping = false;
                IsFalling = false;
            }

            // Checking jump
            if (upPressed && !IsJumping)
                IsJumping = true;

            // Left movement
            if (leftPressed && IsMovingLeft)
            {
                this.Animate(MovableSprite.SpriteMovement.LEFT);
                SpeedX = STEP_LENGTH * (hardware.DeltaTime / 30) * gravity;
                if (this.X + SpeedX > 0)
                {
                    this.X -= SpeedX;
                    // Level scroll
                    if (Level.XMap > 0)
                        Level.XMap -= (short)SpeedX;
                }
            }

            // Right movement
            if (rightPressed && IsMovingRight)
            {
                this.Animate(MovableSprite.SpriteMovement.RIGHT);
                SpeedX = STEP_LENGTH * (hardware.DeltaTime / 30) * gravity;
                if (this.X + SpeedX < Level.Width - HitboxWidth)
                {
                    this.X += SpeedX;
                    // Level scroll
                    if (Level.XMap < Level.Width - GameController.SCREEN_WIDTH)
                        Level.XMap += (short)SpeedX;
                }
            }

            // To reset the position of the player and the view of the game 
            // to the starting point of the level.
            if (resetPressed)
            {
                this.MoveTo(Level.XStart, Level.YStart);
                Level.XMap = (short)(Level.XStart - (2 * Block.SPRITE_WIDTH));
                Level.YMap = Level.YStart;
            }

            // Scroll view adjustments
            if (Level.XMap < 0)
            {
                Level.XMap = 0;
            }                
            else if (Level.XMap > Level.Width - GameController.SCREEN_WIDTH)
            {
                Level.XMap = (short)(Level.Width - 
                    GameController.SCREEN_WIDTH);
            }

            if (Level.YMap < 0)
            {
                Level.YMap = 0;
            }
            else if (Level.YMap > Level.Height - GameController.SCREEN_HEIGHT)
            {
                Level.YMap = (short)(Level.Height -
                    GameController.SCREEN_HEIGHT);
            }
            else if (this.Y <= Level.Height / 2)
            {
                Level.YMap += (short)SpeedY;
            }

            // Character limitations (This can change in future versions)   
            if (this.X < 0)
            {
                this.X = 0;
            }
            else if (this.X > Level.Width - SPRITE_WIDTH)
            {
                this.X = (short)(Level.Width - SPRITE_WIDTH);
            }
        }
    }
}
