using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpookyTerraria.Buffs
{
    public class Shrouded_1 : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Shrouded I");
            Description.SetDefault("You are dwindling with the darkness");
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