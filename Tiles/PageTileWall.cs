using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using SpookyTerraria.OtherItems;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.ObjectData;
using Terraria.DataStructures;

namespace SpookyTerraria.Tiles
{
	public class PageTileWall : ModTile
	{
        // ambrose, dont worry about this tile
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileSolid[Type] = false;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = false;
            Main.tileLighted[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.Origin = new Point16(1, 1);
            drop = ModContent.ItemType<Paper>();
            AddMapEntry(new Color(255, 255, 255));
            minPick = int.MaxValue;
            mineResist = float.MaxValue;
        }
        public override bool KillSound(int i, int j)
        {
            return false;
        }
        public override bool Slope(int i, int j)
        {
            return false;
        }
        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return false;
        }
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
        }
        public override bool CanExplode(int i, int j)
        {
            return false;
        }
        public override void MouseOver(int i, int j)
        {
            Main.player[Main.myPlayer].GetModPlayer<SpookyPlayer>().hoveringPageWall = true;
        }
        public override bool Dangersense(int i, int j, Player player)
        {
            return true;
        }
        public override bool CreateDust(int i, int j, ref int type)
        {
            return false;
        }
        public override bool NewRightClick(int i, int j)
        {
            WorldGen.KillTile(i, j);
            return false;
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b) // Modify the light that this tile produces, if any at all
		{
			r = 0.02f;
			g = 0.02f;
			b = 0.02f;
		}
	}
}