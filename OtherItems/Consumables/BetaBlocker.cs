using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpookyTerraria.OtherItems.Consumables
{
	public class BetaBlocker : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Beta-Blocker");
			Tooltip.SetDefault("Resets your heartrate back to 80");
		}
        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 40;
            item.rare = ItemRarityID.Green;
            item.maxStack = 30;
        }
        public override bool UseItem(Player player)
        {
            if (player.ZoneBeach)
            {
                player.GetModPlayer<SpookyPlayer>().heartRate = 60;
            }
            else if (!player.ZoneBeach)
            {
                player.GetModPlayer<SpookyPlayer>().heartRate = 80;
            }
            return player.GetModPlayer<SpookyPlayer>().heartRate > 80;
        }
    }
}