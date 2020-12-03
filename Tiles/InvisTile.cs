using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using SpookyTerraria.OtherItems;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;

namespace SpookyTerraria.Tiles
{
	public class InvisTile : ModTile
	{
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = false;
            Main.tileLighted[Type] = false;
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
        public override bool CanExplode(int i, int j)
        {
            return false;
        }
        public override bool CreateDust(int i, int j, ref int type)
        {
            return false;
        }
	}
}