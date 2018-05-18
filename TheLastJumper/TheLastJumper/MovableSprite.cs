/* Gonzalo Martinez Font - The Last Jumper 2018
 * 
 * V0.01: First version of the MovableSprite class. This class is very similar
 * to the one used in another project but with some changes.
 */

namespace TheLastJumper
{
    class MovableSprite : Sprite
    {
        const byte MOVEMENTS = 8;
        const byte SPRITE_CHANGE = 8;

        public enum SpriteMovement { LEFT, UP, RIGHT, DOWN, STOOD };
        public SpriteMovement CurrentDirection { get; set; }
        public byte CurrentSprite { get; set; }
        public int[][] SpriteXCoordinates = new int[MOVEMENTS][];
        public int[][] SpriteYCoordinates = new int[MOVEMENTS][];
        protected byte spriteChange;
        
        public MovableSprite()
        {
            CurrentSprite = 0;
            /* The current direction at the beginning is a sprite where the
             * character is standing with a single movement.
             */
            CurrentDirection = SpriteMovement.STOOD;
            spriteChange = 0;
        }

        public void Animate(SpriteMovement movement)
        {
            if(movement != CurrentDirection)
            {
                CurrentDirection = movement;
                CurrentSprite = 0;
                spriteChange = 0;
            }
            else
            {
                spriteChange++;
                if(spriteChange >= SPRITE_CHANGE)
                {
                    spriteChange = 0;
                    CurrentSprite = (byte)((CurrentSprite + 1) % 
                        SpriteXCoordinates[(int)CurrentDirection].Length);
                }
            }
            UpdateSpriteCoordinates();
        }

        public void UpdateSpriteCoordinates()
        {
            SpriteX = (short)(SpriteXCoordinates
                [(int)CurrentDirection][CurrentSprite]);
            SpriteY = (short)(SpriteYCoordinates
                [(int)CurrentDirection][CurrentSprite]);
        }
    }
}
