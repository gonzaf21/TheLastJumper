/* Gonzalo Martinez Font - The Last Jumper 2018
 * 
 * V0.10: Implemented this class in game.
 * 
 * V0.06: Basic skeleton of the Collectible class.
 */

namespace TheLastJumper
{
    class Collectible : StaticSprite
    {
        public const byte SPRITE_WIDTH = 32;
        public const byte SPRITE_HEIGHT = 32;
        public static Image SpriteCollectible =
            new Image("gameData/collectible.png", 140, 32);
        public short XToDraw { get; set; }
        public short YToDraw { get; set; }

        public Collectible(float x, float y)
        {
            X = x;
            Y = y;
            HitboxHeight = 16;
            HitboxHeight = 16;
            XToDraw = 0;
            YToDraw = 0;
        }
    }
}
