using SpookyTerraria.OtherItems.Tools.ToolHeads;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpookyTerraria.OtherItems.Tools
{
	public class WoodenAxe : ModItem
	{
        public override void SetDefaults()
        {
            item.damage = 3;
            item.melee = true;
            item.width = 5;
            item.height = 5;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 3;
            item.rare = ItemRarityID.White;
            item.autoReuse = true;
			item.axe = 3;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<WoodenAxeHead>());
            recipe.AddIngredient(ModContent.ItemType<Stick>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}