/* Gonzalo Martinez Font - The Last Jumper 2018
 * 
 * V0.10: Implemented this class that inherits from the enemy and use it
 * to show it in game.
 */

namespace TheLastJumper
{
    class FlyingEnemy : Enemy
    {
        public const byte SPRITE_WIDTH = 50;
        public const byte SPRITE_HEIGHT = 50;
        public static Image SpriteFlyingEnemy =
            new Image("gameData/enemy2.png", 920, 920);

        public FlyingEnemy(short x, short y) : base(x, y)
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

        public void MoveEnemy()
        {
            do
            {
                // Collision detection and moving right or left placeholder
                Animate(SpriteMovement.LEFT);
                Animate(SpriteMovement.RIGHT);
            } while (!IsDead);
        }
    }
}
