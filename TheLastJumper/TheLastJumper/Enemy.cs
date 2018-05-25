/* Gonzalo Martinez Font - The Last Jumper 2018
 * 
 * V0.10: Implemented this class to show it in game.
 * 
 * V0.01: Basic skeleton of the Enemy class.
 */

namespace TheLastJumper
{
    class Enemy : MovableSprite
    {
        public bool IsDead { get; set; }

        public Enemy(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
}
