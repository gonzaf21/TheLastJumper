/* Gonzalo Martinez Font - The Last Jumper 2018
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

        // The enemy follows the player
        public override void MoveEnemy()
        {
            do
            {
                
                // Collision detection and moving right or left placeholder
                Animate(SpriteMovement.LEFT);
                Animate(SpriteMovement.RIGHT);

                // TODO
            } while (!IsDead);
        }
    }
}
