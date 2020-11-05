using Microsoft.Xna.Framework;
using SpookyTerraria.OtherItems.Tools.ToolHeads;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpookyTerraria.OtherItems.Tools
{
	public class StoneHammer : ModItem
	{
        public override void SetDefaults()
        {
            item.damage = 3;
            item.melee = true;
            item.width = 5;
            item.height = 5;
            item.useTime = 13;
            item.useAnimation = 26;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 3;
            item.rare = ItemRarityID.White;
            item.autoReuse = true;
            item.hammer = 25;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<StoneHammerHead>());
            recipe.AddIngredient(ModContent.ItemType<Stick>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}