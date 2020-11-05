using SpookyTerraria.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpookyTerraria.OtherItems
{
	public class Paper : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Page");
			Tooltip.SetDefault("A token for your victory");
		}
        public override void UpdateInventory(Player player)
        {
            if (item.stack > 300)
            {
                item.stack = 300;
            }
            item.favorited = true;
        }
        public override void PostUpdate()
        {
            item.TurnToAir();
        }
        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 40;
            item.rare = ItemRarityID.White;
            item.maxStack = 300;
            item.createTile = ModContent.TileType<PageTile>();
        }
    }
}