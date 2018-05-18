/* Gonzalo Martinez Font - The Last Jumper 2018
 * 
 * V0.02: CollidesWith() added from character so every sprite can use this
 * method. Changed the type of the coordinates to improve the movement.
 * 
 * V0.01: Creation of the Sprite class. It contains the x and y coordinates of
 * a sprite and the method MoveTo() to change this with the desired coordinates.
 */

namespace TheLastJumper
{
    class Sprite
    {
        public float X { get; set; }
        public float Y { get; set; }
        public short SpriteX { get; set; }
        public short SpriteY { get; set; }

        public void MoveTo(float x, float y)
        {
            X = x;
            Y = y;
        }

        public void CollidesWith(float x, float y, short width, short height)
        {
            //TODO
        }
    }
}
