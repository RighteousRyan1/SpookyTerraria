using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using System.Linq;

namespace SpookyTerraria.TownNPCSpawnBooks
{
	public class WitDocBook : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Witch Doctor Spawn Book");
			Tooltip.SetDefault("Upon use, spawns the Witch Doctor");
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
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.WitchDoctor);
                if (ItemLoader.ConsumeItem(item, player) && item.stack > 0)
                {
                    item.stack--;
                }
            }
        }
        public override bool CanUseItem(Player player)
        {
            int witDocIndex= NPC.FindFirstNPC(NPCID.WitchDoctor);
            return witDocIndex < 0;
        }
    }
}