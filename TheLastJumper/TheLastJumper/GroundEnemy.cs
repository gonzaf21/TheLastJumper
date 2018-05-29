/* Gonzalo Martinez Font - The Last Jumper 2018
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

        public GroundEnemy(short x, short y) : base(x, y)
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
