/* Gonzalo Martinez Font - The Last Jumper 2018
 * 
 * V0.13: Some changes in movement.
 * 
 * V0.12: Changed the sprite width and height so they are on the inherited 
 * class.
 * 
 * V0.11: Changed the spritesheet.
 * 
 * V0.10: Implemented this class that inherits from the enemy and use it
 * to show it in game.
 */

namespace TheLastJumper
{
    class GroundEnemy : Enemy
    {
        public static Image SpriteGroundEnemy =
            new Image("gameData/enemyground.png", 480, 180);

        public GroundEnemy(float x, float y) : base(x, y)
        {
            HitboxHeight = 50;
            HitboxWidth = 50;
            Collided = false;

            SpriteXCoordinates[(int)MovableSprite.SpriteMovement.LEFT] =
                new int[] { 0, 64, 128, 192, 256, 320, 384, 448 };
            SpriteYCoordinates[(int)MovableSprite.SpriteMovement.LEFT] =
                new int[] { 576, 576, 576, 576, 576, 576, 576, 576 };

            SpriteXCoordinates[(int)MovableSprite.SpriteMovement.RIGHT] =
                new int[] { 0, 64, 128, 192, 256, 320, 384, 448 };
            SpriteYCoordinates[(int)MovableSprite.SpriteMovement.RIGHT] =
                new int[] { 576, 576, 576, 576, 576, 576, 576, 576 };

            UpdateSpriteCoordinates();
        }

        // The enemy is constantly patrolling an area
        public override void MoveEnemy()
        {
            short speedX = 4;
            do
            {
                while(!Collided && speedX > 0)
                {
                    X += speedX;
                    Animate(MovableSprite.SpriteMovement.LEFT);
                }

                if(Collided && speedX > 0)
                {
                    speedX = (short)(-1 * speedX);
                    Collided = false;
                }

                while(!Collided && speedX < 0)
                {
                    X += speedX;
                    Animate(SpriteMovement.RIGHT);
                }            
            } while (!IsDead);
        } 
    }
}
