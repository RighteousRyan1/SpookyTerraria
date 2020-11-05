using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpookyTerraria.NPCs;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace SpookyTerraria.ModIntances
{
    public class Lists_SpookyNPCs
    {
        public static List<int> SpookyNPCs = new List<int>
        {
            ModContent.NPCType<Stalker>()
        };
    }
}