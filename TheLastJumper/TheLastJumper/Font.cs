/* Gonzalo Martinez Font - The Last Jumper 2018
 * 
 * V0.01: Class Font imported from other projects.
 */

using System;
using Tao.Sdl;

namespace TheLastJumper
{
    class Font
    {
        IntPtr fontType;

        public Font(string fileName, int fontSize)
        {
            fontType = SdlTtf.TTF_OpenFont(fileName, fontSize);
            if (fontType == IntPtr.Zero)
            {
                Console.WriteLine("Font type not found");
                Environment.Exit(2);
            }
        }

        public IntPtr GetFontType()
        {
            return fontType;
        }
    }
}