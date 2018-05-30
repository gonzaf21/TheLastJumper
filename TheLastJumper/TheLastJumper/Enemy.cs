/* Gonzalo Martinez Font - The Last Jumper 2018
 * 
 * V0.13: New method to set the character and use it for the movement.
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
        public const byte SPRITE_WIDTH = 60;
        public const byte SPRITE_HEIGHT = 60;
        public bool IsDead { get; set; }
        public bool Collided { get; set; }
        protected static Character character;

        public Enemy(float x, float y)
        {
            X = x;
            Y = y;
        }

        public virtual void MoveEnemy()
        {
        }

        public virtual void DrawEnemy(Level l)
        {
        }

        // To set the character and use its coordinates
        public static void SetCharacter(Character c)
        {
            character = c;
        }
    }
}
