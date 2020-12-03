using Microsoft.Xna.Framework.Graphics;
using SpookyTerraria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace SpookyTerraria.ModIntances
{
	public class Instance_GlobalTile : GlobalTile
	{
        public override void SetDefaults()
        {
            bool slenderWorld = Main.worldName == SpookyTerrariaUtils.slenderWorldName;
            if (slenderWorld)
            {
                ModTile n = new ModTile();
                n.mineResist = float.MaxValue;
                n.minPick = int.MaxValue;
            }
        }
        public override bool KillSound(int i, int j, int type)
        {
            bool slenderWorld = Main.worldName == SpookyTerrariaUtils.slenderWorldName;
            return slenderWorld ? false : true;
        }
        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
        }
        public override bool CreateDust(int i, int j, int type, ref int dustType)
        {
            bool slenderWorld = Main.worldName == SpookyTerrariaUtils.slenderWorldName;
            return slenderWorld ? false : true;
        }
        public override void NumDust(int i, int j, int type, bool fail, ref int num)
        {
            bool slenderWorld = Main.worldName == SpookyTerrariaUtils.slenderWorldName;
            if (slenderWorld)
            {
                num = 0;
            }
        }
        public override bool CanPlace(int i, int j, int type)
        {
            bool slenderWorld = Main.worldName == SpookyTerrariaUtils.slenderWorldName;
            return slenderWorld ? false : true;
        }
        public override bool CanExplode(int i, int j, int type)
        {
            bool slenderWorld = Main.worldName == SpookyTerrariaUtils.slenderWorldName;
            return slenderWorld ? false : true;
        }
        public override bool CanKillTile(int i, int j, int type, ref bool blockDamaged)
        {
            bool slenderWorld = Main.worldName == SpookyTerrariaUtils.slenderWorldName;
            return slenderWorld ? false : true;
        }
        public override void PostDraw(int i, int j, int type, SpriteBatch spriteBatch)
        {
        }
    }
}
