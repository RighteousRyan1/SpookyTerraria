using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpookyTerraria.OtherItems.Tools.ToolHeads
{
	public class WoodenAxeHead : ModItem
	{
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 22;
            item.rare = ItemRarityID.White;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood, 8);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}