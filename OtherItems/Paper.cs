using SpookyTerraria.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace SpookyTerraria.OtherItems
{
	public class Paper : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Page");

            DisplayName.AddTranslation(GameCulture.Spanish, "Página");
            DisplayName.AddTranslation(GameCulture.French, "Page");
            DisplayName.AddTranslation(GameCulture.German, "Seite");
            DisplayName.AddTranslation(GameCulture.Italian, "Pagina");
            Tooltip.SetDefault("A token for your victory");

            Tooltip.AddTranslation(GameCulture.Spanish, "Una muestra de tu victoria");
            Tooltip.AddTranslation(GameCulture.French, "Un gage pour votre victoire");
            Tooltip.AddTranslation(GameCulture.German, "Ein Zeichen für Ihren Sieg");
            Tooltip.AddTranslation(GameCulture.Spanish, "Un segno per la tua vittoria");
        }
        public override void UpdateInventory(Player player)
        {
            item.favorited = true;
        }
        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 40;
            item.rare = ItemRarityID.White;
            item.maxStack = 8;
            // item.createTile = ModContent.TileType<PageTile>();
        }
        public override bool OnPickup(Player player)
        {
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Action/PagePickup"), player.Center);
            return true;
        }
    }
}