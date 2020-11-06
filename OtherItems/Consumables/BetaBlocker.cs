using System;
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
			Tooltip.SetDefault("Resets your heartrate back to 80\n'Works like a charm'");
		}
        public override void SetDefaults()
        {
            item.UseSound = SoundID.Item2;
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.useTurn = true;
            item.useAnimation = 12;
            item.useTime = 12;
            item.maxStack = 30;
            item.consumable = true;
            item.width = 20;
            item.height = 20;
            item.value = 3213;
            item.rare = ItemRarityID.Green;
        }
        public override bool CanUseItem(Player player)
        {
            return player.GetModPlayer<SpookyPlayer>().heartRate != 80 && player.GetModPlayer<SpookyPlayer>().heartRate != 60;
        }
        public override bool UseItem(Player player)
        {
            if (player.itemAnimation == 1)
            {
                item.stack--;
            }
            if (player.ZoneBeach)
            {
                player.GetModPlayer<SpookyPlayer>().heartRate = 60;
            }
            else if (!player.ZoneBeach)
            {
                player.GetModPlayer<SpookyPlayer>().heartRate = 80;
            }
            return base.CanUseItem(player);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Daybloom, 1);
            recipe.AddIngredient(ItemID.Seed, 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}