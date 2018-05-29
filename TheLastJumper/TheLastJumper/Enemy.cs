/* Gonzalo Martinez Font - The Last Jumper 2018
 * 
 * V0.12: Added the virtual method of movement so the classes that inherit
 * from this can override it if needed.
 * 
 * V0.10: Implemented this class to show it in game.
 * 
 * V0.01: Basic skeleton of the Enemy class.
 */

namespace TheLastJumper
{
    class Enemy : MovableSprite
    {
        public const byte SPRITE_WIDTH = 50;
        public const byte SPRITE_HEIGHT = 50;
        public bool IsDead { get; set; }

        public Enemy(float x, float y)
        {
            X = x;
            Y = y;
        }

        public virtual void MoveEnemy()
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
