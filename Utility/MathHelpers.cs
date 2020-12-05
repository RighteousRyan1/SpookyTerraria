using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using SpookyTerraria.OtherItems;
using Microsoft.Xna.Framework.Audio;
using System.Runtime.Caching;
using System;

namespace SpookyTerraria.Utilities
{
    public class MathUtils
    {
        /// <summary>
        /// Finds the remanider of <paramref name="i"/> and <paramref name="j"/>, (j - i)
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public static int FindRemainder(int i, int j)
        {
            return j - i;
        }
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
        public static int Clamp(float value, int x, int y)
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
        public static int Clamp(double value, int x, int y)
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
        public static int Clamp(byte value, int x, int y)
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
    public static class AudioHelper
    {
        public static void StopSound(this Mod mod, Vector2 pos, string fileName, Player player)
        {
        }
        public static SoundEffectInstance StartSound(this Mod mod, Vector2 position, string fileName)
        => Main.PlaySound(SoundLoader.customSoundType, (int)position.X, (int)position.Y, mod.GetSoundSlot(SoundType.Custom, $"Sounds/Custom/{fileName}"));
    }
    public class OceanAmbience : ModSound
    {
        // ... Wtf
        public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan, SoundType type)
        {
            type = SoundType.Custom;

            return base.PlaySound(ref soundInstance, volume, pan, type);
        }
    }
}