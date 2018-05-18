/* Gonzalo Martinez Font - The Last Jumper 2018
 * 
 * V0.03: Adding the sprite of the blocks and a method to set the type of 
 * block to draw and get the coordinates from the image along with some 
 * new necessary attributes. Implemented another constructor as well for a 
 * better creation of blocks.
 * 
 * V0.01: Basic skeleton of the Block class.
 */

namespace TheLastJumper
{
    class Block : StaticSprite
    {
        public const byte SPRITE_WIDTH = 60;
        public const byte SPRITE_HEIGHT = 60;
        public static Image spriteBlock = 
            new Image("gameData/foresttiles01.png", 600, 600);
        public short XToDraw { get; set; }
        public short YToDraw { get; set; }
        protected byte type;

        public Block(byte type)
        {
            XToDraw = 60;
            YToDraw = 60;
            this.type = type;
            SetType();
        }

        // Another constructor in order to create and set blocks in Level class
        public Block(float X, float Y, byte type) : this(type)
        {
            this.X = X;
            this.Y = Y;
        }

        // Set the type of block to draw
        public void SetType()
        {
            switch(type)
            {
                case 0:
                    XToDraw = 60;
                    YToDraw = 60; 
                    break;
                case 1:
                    XToDraw = 0;
                    YToDraw = 60;
                    break;
                case 2:
                    XToDraw = 0;
                    YToDraw = 120;
                    break;
                case 3:
                    XToDraw = 0;
                    YToDraw = 180;
                    break;
                case 4:
                    XToDraw = 0;
                    YToDraw = 240;
                    break;
                case 5:
                    XToDraw = 0;
                    YToDraw = 300;
                    break;
                case 6:
                    XToDraw = 0;
                    YToDraw = 240;
                    break;
            }
        }
    }
}
