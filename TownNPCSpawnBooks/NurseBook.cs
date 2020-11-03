using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using System.Linq;

namespace SpookyTerraria.TownNPCSpawnBooks
{
	public class NurseBook : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nurse Spawn Book");
			Tooltip.SetDefault("Upon use, spawns the Nurse");
		}

        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 40;
            item.useTime = 45;
            item.useAnimation = 45;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.rare = ItemRarityID.Orange;
            item.UseSound = SoundID.Item77;
            item.autoReuse = false;
            item.consumable = true;
            item.maxStack = 3;
        }
        public override void HoldItem(Player player)
        {
            if (player.itemAnimation == 1)
            {
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.Nurse);
                if (ItemLoader.ConsumeItem(item, player) && item.stack > 0)
                {
                    item.stack--;
                }
            }
        }
        public override bool CanUseItem(Player player)
        {
            int nurseIndex = NPC.FindFirstNPC(NPCID.Nurse);
            return nurseIndex < 0;
        }
    }
}