using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using SpookyTerraria.OtherItems;

namespace SpookyTerraria.Tiles
{
	public class PageTile : ModTile
	{
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            drop = ModContent.ItemType<Paper>();
            AddMapEntry(new Color(255, 255, 255));
            soundType = SoundID.Tink;
            minPick = 50;
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b) // Modify the light that this tile produces, if any at all
		{
			r = 0.01f;
			g = 0.01f;
			b = 0.01f;
		}
	}
}