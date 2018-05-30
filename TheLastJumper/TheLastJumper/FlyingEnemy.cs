/* Gonzalo Martinez Font - The Last Jumper 2018
 * 
 * V0.13: Some changes in the movement, spritesheet and drawing this type of
 * enemy with the correct image.
 * 
 * V0.12: Changed the sprite width and height so they are on the inherited 
 * class.
 * 
 * V0.11: Changed the spritesheet of this enemy.
 * 
 * V0.10: Implemented this class that inherits from the enemy and use it
 * to show it in game.
 */

namespace TheLastJumper
{
    class FlyingEnemy : Enemy
    {
        public static Image SpriteFlyingEnemy =
            new Image("gameData/enemyair.png", 600, 120);

        public FlyingEnemy(float x, float y) : base(x, y)
        {
            HitboxHeight = 50;
            HitboxWidth = 50;
            Collided = false;
            IsDead = false;

            SpriteXCoordinates[(int)MovableSprite.SpriteMovement.LEFT] =
                new int[] { 0, 60, 120, 180, 240, 300, 360, 420, 480, 540 };
            SpriteYCoordinates[(int)MovableSprite.SpriteMovement.LEFT] =
                new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            SpriteXCoordinates[(int)MovableSprite.SpriteMovement.RIGHT] =
                new int[] { 0, 60, 120, 180, 240, 300, 360, 420, 480, 540 };
            SpriteYCoordinates[(int)MovableSprite.SpriteMovement.RIGHT] =
                new int[] { 60, 60, 60, 60, 60, 60, 60, 60, 60, 60 };

            //UpdateSpriteCoordinates();
        }

        // The enemy follows the player
        public override void MoveEnemy()
        {
            short speedX = 2;
            short speedY = 2;
            short radius = 50;
            
            do
            {
                while(!Collided)
                {
                    if(character.X + radius >= X)
                    {
                        while(X + SPRITE_WIDTH <= character.X)
                        {
                            if (Y > character.Y)
                                Y -= speedY;
                            else if (Y < character.Y)
                                Y += speedY;

                            X += speedX;
                        }
                    }
                    else if(character.X - radius <= X)
                    {
                        while (X >= character.X + Character.SPRITE_WIDTH)
                        {
                            if (Y > character.Y)
                                Y -= speedY;
                            else if (Y < character.Y)
                                Y += speedY;

                            X -= speedX;
                        }
                    }
                }
            } while (!IsDead);
        }

        public override void DrawEnemy(Level l)
        {
            Hardware h = new Hardware(800, 600, 24, false);
            h.DrawSprite(SpriteFlyingEnemy, (short)(X - l.XMap),
                (short)(Y - l.YMap), 0, 0, SPRITE_WIDTH, SPRITE_HEIGHT);
        }
    }
}
