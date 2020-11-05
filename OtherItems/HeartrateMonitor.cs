using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;

namespace SpookyTerraria.OtherItems
{
	public class HeartrateMonitor : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Heartrate Monitor");
			Tooltip.SetDefault("Measures your heartrate\nThe higher your heartrate, the faster you move, but your life regen weakens");
		}

        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 40;
            item.rare = ItemRarityID.Red;
            item.maxStack = 1;
            item.scale = 1f;
        }
        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<SpookyPlayer>().accHeartMonitor = true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.player[Main.myPlayer];
            if (player.GetModPlayer<SpookyPlayer>().heartRate <= 100)
            {
                tooltips.Add(new TooltipLine(mod, "Blah.", "hoot")
                {
                    isModifier = true,
                    text = $"Your BPM: {player.GetModPlayer<SpookyPlayer>().heartRate}"

                });
            }
            if (player.GetModPlayer<SpookyPlayer>().heartRate > 100)
            {
                tooltips.Add(new TooltipLine(mod, "Blah.", "hoot")
                {
                    overrideColor = Color.IndianRed,
                    text = $"Your BPM: {player.GetModPlayer<SpookyPlayer>().heartRate}"

                });
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}