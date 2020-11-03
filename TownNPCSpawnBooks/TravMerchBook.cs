using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using System.Linq;
using Microsoft.Xna.Framework;

namespace SpookyTerraria.TownNPCSpawnBooks
{
	public class TravMerchBook : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Merchant Spawn Book");
			Tooltip.SetDefault("Upon use, spawns the Travelling Merchant (Goes away as soon as you leave him)");
		}

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 40;
            item.useTime = 45;
            item.useAnimation = 45;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.rare = ItemRarityID.Orange;
            item.UseSound = SoundID.Item77;
            item.autoReuse = false;
            item.consumable = true;
            item.maxStack = 3;
            item.scale = 0.75f;
        }
        public override void HoldItem(Player player)
        {
            if (player.itemAnimation == 1)
            {
                NPC.NewNPC((int)player.Center.X, (int)player.Center.Y - 30, NPCID.TravellingMerchant);
                if (ItemLoader.ConsumeItem(item, player) && item.stack > 0)
                {
                    item.stack--;
                }
            }
        }
        public override bool CanUseItem(Player player)
        {
            int tmIndex = NPC.FindFirstNPC(NPCID.TravellingMerchant);
            return tmIndex < 0;
        }
    }
}