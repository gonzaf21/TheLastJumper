/* Gonzalo Martinez Font - The Last Jumper 2018
 * 
 * V0.07: Implemented constructor and method to choose what type of trap
 * can be drawn.
 * 
 * V0.06: Basic skeleton of the Trap class.
 */

namespace TheLastJumper
{
    class Trap : StaticSprite
    {
        public const byte SPRITE_WIDTH = 60;
        public const byte SPRITE_HEIGHT = 60;
        public static Image SpriteTrap = 
            new Image("gameData/spikes.png", 60, 60);
        public short XToDraw { get; set; }
        public short YToDraw { get; set; }

        public Trap(float x, float y, char type)
        {
            X = x;
            Y = y;
            HitboxHeight = 15;
            HitboxHeight = 50;
            SetTypeOfTrap(type);
        }

        public void SetTypeOfTrap(char type)
        {
            switch(type)
            {
                // Spikes
                case 's':
                    XToDraw = 0;
                    YToDraw = 0;
                    break;
                // Lava (Sprite not in that image yet)
                case 'l':
                    XToDraw = 60;
                    YToDraw = 0;
                    break;
                default:
                    XToDraw = 0;
                    YToDraw = 0;
                    break;
            }
        }
    }
}
