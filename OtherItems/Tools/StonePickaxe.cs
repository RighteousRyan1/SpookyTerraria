using SpookyTerraria.OtherItems.Tools.ToolHeads;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpookyTerraria.OtherItems.Tools
{
	public class StonePickaxe : ModItem
	{
        public override void SetDefaults()
        {
            item.damage = 1;
            item.melee = true;
            item.width = 22;
            item.height = 22;
            item.useTime = 12;
            item.useAnimation = 18;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 3;
            item.rare = ItemRarityID.White;
            item.autoReuse = true;
            item.pick = 25;
        }
        public override void AddRecipes() // FIXME: Add Recipe lol
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<StonePickaxeHead>());
            recipe.AddIngredient(ModContent.ItemType<Stick>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}