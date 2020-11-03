using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpookyTerraria.Fists
{
	public class Fists : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("'The bare necessities'");
		}
        public override void SetDefaults()
        {
            item.damage = 3;
            item.melee = true;
            item.width = 5;
            item.height = 5;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 3;
            item.value = 0;
            item.rare = ItemRarityID.Blue;
            item.autoReuse = true;
            item.noUseGraphic = true;
            item.holdStyle = ItemHoldStyleID.HoldingOut;
            item.useTurn = true;
            item.pick = 10;
            item.axe = 2;
        }
        public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            hitbox.Width = 8;
            hitbox.Height = 8;
            hitbox.X += 10;
            hitbox.Y += 10;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}