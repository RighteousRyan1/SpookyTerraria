using Microsoft.Xna.Framework;
using SpookyTerraria.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace SpookyTerraria.OtherItems
{
	public class Battery : ModItem
	{
		public override void SetStaticDefaults() 
		{
            DisplayName.SetDefault("Flashlight Battery");

            DisplayName.AddTranslation(GameCulture.Spanish, "Batería");
            DisplayName.AddTranslation(GameCulture.French, "Batterie");
            DisplayName.AddTranslation(GameCulture.German, "Batterie");
            DisplayName.AddTranslation(GameCulture.Italian, "Batteria");
            Tooltip.SetDefault("Used for your flashlight");

            Tooltip.AddTranslation(GameCulture.Spanish, "Usado para tu linterna");
            Tooltip.AddTranslation(GameCulture.French, "Utilisé pour votre lampe de poche");
            Tooltip.AddTranslation(GameCulture.German, "Wird für Ihre Taschenlampe verwendet");
            Tooltip.AddTranslation(GameCulture.Spanish, "Usato per la tua torcia");
        }
        public override void UpdateInventory(Player player)
        {

        }
        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 40;
            item.maxStack = 5;
            item.rare = ItemRarityID.White;
            item.scale = 0.4f;
        }
        public override bool CanPickup(Player player)
        {
            return player.CountItem(ModContent.ItemType<Battery>()) < 5;
        }
        /*public override void OnCraft(Recipe recipe)
        {
            Player player = Main.player[Main.myPlayer];
            if (player.CountItem(ModContent.ItemType<Battery>()) >= 5)
            {
                item.TurnToAir();
                player.QuickSpawnItem(ItemID.SilverBar);
                player.QuickSpawnItem(ItemID.IronBar);
            }
        }*/
        /*public override void AddRecipes()
        {
            ModRecipe _ = new ModRecipe(mod); // I am rebel :pog:
            _.AddRecipeGroup(RecipeGroupID.IronBar, 1);
            _.AddIngredient(ItemID.SilverBar, 1);
            _.AddTile(TileID.Tables);
            _.SetResult(this);
            _.AddRecipe();

            _ = new ModRecipe(mod);
            _.AddRecipeGroup(RecipeGroupID.IronBar, 1);
            _.AddIngredient(ItemID.TungstenBar, 1);
            _.AddTile(TileID.Tables);
            _.SetResult(this);
            _.AddRecipe();
        }*/
    }
}