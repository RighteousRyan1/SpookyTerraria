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
            if (Main.worldName == SpookyTerrariaUtils.slenderWorldName)
            {
                ModTile n = new ModTile {mineResist = float.MaxValue, minPick = int.MaxValue};
            }
        }
        public override bool KillSound(int i, int j, int type)  => Main.worldName == SpookyTerrariaUtils.slenderWorldName;
        
        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
        }
        public override bool CreateDust(int i, int j, int type, ref int dustType) => Main.worldName == SpookyTerrariaUtils.slenderWorldName;
        
        public override void NumDust(int i, int j, int type, bool fail, ref int num)
        {
            if (Main.worldName == SpookyTerrariaUtils.slenderWorldName)
                num = 0;
        }
        public override bool CanPlace(int i, int j, int type) => Main.worldName == SpookyTerrariaUtils.slenderWorldName;
        
        public override bool CanExplode(int i, int j, int type) => Main.worldName == SpookyTerrariaUtils.slenderWorldName;
        
        public override bool CanKillTile(int i, int j, int type, ref bool blockDamaged) => Main.worldName == SpookyTerrariaUtils.slenderWorldName;
        
        public override void PostDraw(int i, int j, int type, SpriteBatch spriteBatch)
        {
        }
    }
}
