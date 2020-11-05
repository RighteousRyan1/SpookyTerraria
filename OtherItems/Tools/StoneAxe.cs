using Microsoft.Xna.Framework;
using SpookyTerraria.OtherItems.Tools.ToolHeads;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpookyTerraria.OtherItems.Tools
{
	public class StoneAxe : ModItem
	{
        public override void SetDefaults()
        {
            item.damage = 5;
            item.melee = true;
            item.width = 22;
            item.height = 22;
            item.useTime = 12;
            item.useAnimation = 16;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 3;
            item.rare = ItemRarityID.White;
            item.autoReuse = true;
            item.axe = 5;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<StoneAxeHead>());
            recipe.AddIngredient(ModContent.ItemType<Stick>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}