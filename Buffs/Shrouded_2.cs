using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpookyTerraria.Buffs
{
    public class Shrouded_2 : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Shrouded II");
            Description.SetDefault("The darkness dances with you");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            canBeCleared = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
        }
    }
}