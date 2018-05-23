/* Gonzalo Martinez Font - The Last Jumper 2018
 * 
 * V0.08: Added constants for the value of the keys that will be used in 
 * the input of the scoreboard screen.
 * 
 * V0.02: Added method UpdateDeltaTime() to calculate the time between frames.
 * Added attribute deltaTime on this class to use it on other classes.
 * 
 * V0.01: Class Hardware imported from other projects.
 */

using System;
using Tao.Sdl;

namespace TheLastJumper
{
    class Hardware
    {
        // Keys to use in game
        public const int KEY_ESC = Sdl.SDLK_ESCAPE;
        public const int KEY_UP = Sdl.SDLK_UP;
        public const int KEY_DOWN = Sdl.SDLK_DOWN;
        public const int KEY_LEFT = Sdl.SDLK_LEFT;
        public const int KEY_RIGHT = Sdl.SDLK_RIGHT;
        public const int KEY_SPACE = Sdl.SDLK_SPACE;
        public const int KEY_SHIFT = Sdl.SDLK_LSHIFT;
        public const int KEY_ENTER = Sdl.SDLK_KP_ENTER;
        public const int KEY_A = Sdl.SDLK_a;
        public const int KEY_B = Sdl.SDLK_b;
        public const int KEY_C = Sdl.SDLK_c;
        public const int KEY_D = Sdl.SDLK_d;
        public const int KEY_E = Sdl.SDLK_e;
        public const int KEY_F = Sdl.SDLK_f;
        public const int KEY_G = Sdl.SDLK_g;
        public const int KEY_H = Sdl.SDLK_h;
        public const int KEY_I = Sdl.SDLK_i;
        public const int KEY_J = Sdl.SDLK_j;
        public const int KEY_K = Sdl.SDLK_k;
        public const int KEY_L = Sdl.SDLK_l;
        public const int KEY_M = Sdl.SDLK_m;
        public const int KEY_N = Sdl.SDLK_n;
        public const int KEY_O = Sdl.SDLK_o;
        public const int KEY_P = Sdl.SDLK_p;
        public const int KEY_Q = Sdl.SDLK_q;
        public const int KEY_R = Sdl.SDLK_r;
        public const int KEY_S = Sdl.SDLK_s;
        public const int KEY_T = Sdl.SDLK_t;
        public const int KEY_U = Sdl.SDLK_u;
        public const int KEY_V = Sdl.SDLK_v;
        public const int KEY_W = Sdl.SDLK_w;
        public const int KEY_X = Sdl.SDLK_x;
        public const int KEY_Y = Sdl.SDLK_y;
        public const int KEY_Z = Sdl.SDLK_z;

        public float DeltaTime { get; set; }

        short screenWidth;
        short screenHeight;
        short colorDepth;
        IntPtr screen;

        public Hardware(short width, short height, short depth, 
            bool fullScreen)
        {
            screenWidth = width;
            screenHeight = height;
            colorDepth = depth;

            int flags = Sdl.SDL_HWSURFACE | Sdl.SDL_DOUBLEBUF | 
                Sdl.SDL_ANYFORMAT;
            if (fullScreen)
                flags = flags | Sdl.SDL_FULLSCREEN;

            Sdl.SDL_Init(Sdl.SDL_INIT_EVERYTHING);
            screen = Sdl.SDL_SetVideoMode(screenWidth, screenHeight, 
                colorDepth, flags);
            Sdl.SDL_Rect rect = new Sdl.SDL_Rect(0, 0, screenWidth, 
                screenHeight);
            Sdl.SDL_SetClipRect(screen, ref rect);

            SdlTtf.TTF_Init();
        }

        ~Hardware()
        {
            Sdl.SDL_Quit();
        }

        // Draws an image in its current coordinates
        public void DrawImage(Image img)
        {
            Sdl.SDL_Rect source = new Sdl.SDL_Rect(0, 0, img.ImageWidth,
                img.ImageHeight);
            Sdl.SDL_Rect target = new Sdl.SDL_Rect(img.X, img.Y,
                img.ImageWidth, img.ImageHeight);
            Sdl.SDL_BlitSurface(img.ImagePtr, ref source, screen, ref target);
        }

        // Draws a sprite from a sprite sheet in the specified X and Y 
        // position of the screen.
        // The sprite to be drawn is determined by the x and y coordinates 
        // within the image, and the width and height to be cropped
        public void DrawSprite(Image image, short xScreen, short yScreen, 
            short x, short y, short width, short height)
        {
            Sdl.SDL_Rect src = new Sdl.SDL_Rect(x, y, width, height);
            Sdl.SDL_Rect dest = new Sdl.SDL_Rect(xScreen, yScreen, width, height);
            Sdl.SDL_BlitSurface(image.ImagePtr, ref src, screen, ref dest);
        }

        // Update screen
        public void UpdateScreen()
        {
            Sdl.SDL_Flip(screen);
        }

        // Detects if the user presses a key and returns the code of the 
        // key pressed
        public int KeyPressed()
        {
            int pressed = -1;

            Sdl.SDL_PumpEvents();
            Sdl.SDL_Event keyEvent;
            if (Sdl.SDL_PollEvent(out keyEvent) == 1)
            {
                if (keyEvent.type == Sdl.SDL_KEYDOWN)
                {
                    pressed = keyEvent.key.keysym.sym;
                }
            }

            return pressed;
        }

        // Checks if a given key is now being pressed
        public bool IsKeyPressed(int key)
        {
            bool pressed = false;
            Sdl.SDL_PumpEvents();
            Sdl.SDL_Event evt;
            Sdl.SDL_PollEvent(out evt);
            int numKeys;
            byte[] keys = Sdl.SDL_GetKeyState(out numKeys);
            if (keys[key] == 1)
                pressed = true;
            return pressed;
        }

        // Clears the screen
        public void ClearScreen()
        {
            Sdl.SDL_Rect source = new Sdl.SDL_Rect(0, 0, screenWidth, 
                screenHeight);
            Sdl.SDL_FillRect(screen, ref source, 0);
        }

        // Clears the bottom of the screen
        public void ClearBottom()
        {
            Sdl.SDL_Rect source = new Sdl.SDL_Rect(0, 
                GameController.SCREEN_HEIGHT,
                screenWidth, (short)(screenHeight - 
                GameController.SCREEN_HEIGHT));
            Sdl.SDL_FillRect(screen, ref source, 0);
        }

        // Writes a text with the specified coordinates and color
        public void WriteText(string txt,
            short x, short y, byte r, byte g, byte b, Font f)
        {
            Sdl.SDL_Color color = new Sdl.SDL_Color(r, g, b);
            IntPtr textAsImage = SdlTtf.TTF_RenderText_Solid(
                f.GetFontType(), txt, color);
            if (textAsImage == IntPtr.Zero)
                Environment.Exit(5);

            Sdl.SDL_Rect src = new Sdl.SDL_Rect(0, 0, screenWidth, 
                screenHeight);
            Sdl.SDL_Rect dest = new Sdl.SDL_Rect(x, y, screenWidth, 
                screenHeight);

            Sdl.SDL_BlitSurface(textAsImage, ref src, screen, ref dest);

            Sdl.SDL_FreeSurface(textAsImage);
        }

        // Writes a line in the specified coordinates, with the specified 
        // color and alpha
        public void DrawLine(short x, short y, short x2, short y2, 
            byte r, byte g, byte b, byte alpha)
        {
            SdlGfx.lineRGBA(screen, x, y, x2, y2, r, g, b, alpha);
        }

        // Method to calculate the time between each frame
        public void UpdateDeltaTime(ref float currentTime, 
            ref float previousTime)
        {
            previousTime = currentTime;
            currentTime = Sdl.SDL_GetTicks();
            DeltaTime = currentTime - previousTime;
        }
    }
}
