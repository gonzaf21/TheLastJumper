/* Gonzalo Martinez Font - The Last Jumper 2018
 * 
 * V0.01: Class Screen created. It contains a hardware object to use it
 * in the constructor and a virtual method Show() for the classes that inherit
 * from this.
 */

namespace TheLastJumper
{
    class Screen
    {
        protected Hardware hardware;

        public Screen(Hardware hardware)
        {
            this.hardware = hardware;
        }

        public virtual void Show() { }
    }
}
