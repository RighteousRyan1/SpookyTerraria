using Microsoft.Xna.Framework;
using SpookyTerraria.OtherItems.Tools.ToolHeads;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpookyTerraria.OtherItems.Tools
{
	public class StoneSpear : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Stone Spear");
		}
		public override void SetDefaults()
		{
			item.damage = 4;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useAnimation = 24;
			item.useTime = 28;
			item.shootSpeed = 2.5f;
			item.knockBack = 1.5f;
			item.width = 32;
			item.height = 32;
			item.rare = ItemRarityID.White;
			item.value = Item.sellPrice(copper: 60);

			item.melee = true;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.autoReuse = true;

			item.UseSound = SoundID.Item1;
			item.shoot = ModContent.ProjectileType<StoneSpearUsed>();
		}

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[item.shoot] < 1;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<StoneSpearHead>());
            recipe.AddIngredient(ModContent.ItemType<Stick>());
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}
