using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SpookyTerraria.Utilities
{
    public class Ryan
    {
        public static bool Smart;
        public static bool Dumb;
        public static void Main()
        {
            if (Dumb)
            {
                Terraria.Main.NewText("Haha, ryan is dumb!");
            }
        }
    }
    public class MathHelpers
    {
        /// <summary>
        /// Clamps values. If the value you set is higher than x, return < x. If the int is greater than y, return < y
        /// </summary>
        /// <returns>The clamped value</returns>
        public static int Clamp(int value, int x, int y)
        {
            if (value > x)
            {
                return x < y ? x : y;
            }
            if (value > y && value < x)
            {
                return y > x ? y : x;
            }
            return x >> y;
        }
        
    }
}