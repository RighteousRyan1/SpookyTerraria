using Terraria.ModLoader;
using Terraria;
using SpookyTerraria.NPCs;

namespace SpookyTerraria.ModIntances
{
    public class CountStalkersWorld : ModWorld
	{
        public override void PostUpdate()
        {
            for (int index = 0; index < Main.maxNPCs; index++)
            {
                NPC npc = Main.npc[index];
                if (npc.type == ModContent.NPCType<Stalker>() && npc.active)
                {
                    Main.player[Main.myPlayer].GetModPlayer<SpookyPlayer>().stalkerConditionMet = true;
                }
            }
        }
    }
}
