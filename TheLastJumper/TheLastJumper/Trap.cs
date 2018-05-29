/* Gonzalo Martinez Font - The Last Jumper 2018
 * 
 * V0.11: Added new orientation of traps and changed the spritesheet.
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
            new Image("gameData/traps.png", 240, 180);
        public short XToDraw { get; set; }
        public short YToDraw { get; set; }

        public Trap(float x, float y, char type)
        {
            X = x;
            Y = y;
            HitboxHeight = 50;
            HitboxWidth = 50;
            SetTypeOfTrap(type);
        }

        public void SetTypeOfTrap(char type)
        {
            switch(type)
            {
                // Spikes floor
                case 's':
                    XToDraw = 0;
                    YToDraw = 0;
                    break;
                // Spikes wall-left
                case 'l':
                    XToDraw = 120;
                    YToDraw = 0;
                    break;
                // Spikes wall-right
                case 'w':
                    XToDraw = 180;
                    YToDraw = 0;
                    break;
                // Spikes ceiling
                case 'y':
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
