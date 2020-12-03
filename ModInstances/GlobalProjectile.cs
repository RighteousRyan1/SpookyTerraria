using Microsoft.Xna.Framework;
using SpookyTerraria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpookyTerraria.ModIntances
{
    public class Instances_GlobalProjectile : GlobalProjectile
    {
        public override void PostAI(Projectile projectile)
        {
            if (Main.worldName == SpookyTerrariaUtils.slenderWorldName)
            {
                if (projectile.type == ProjectileID.FallingStar)
                {
                    projectile.active = false;
                    projectile.Kill();
                }
            }
            if (projectile.type == ProjectileID.Tombstone || 
                projectile.type == ProjectileID.Obelisk|| 
                projectile.type == ProjectileID.CrossGraveMarker|| 
                projectile.type == ProjectileID.GraveMarker|| 
                projectile.type == ProjectileID.Gravestone|| 
                projectile.type == ProjectileID.RichGravestone1|| 
                projectile.type == ProjectileID.RichGravestone2|| 
                projectile.type == ProjectileID.RichGravestone3 ||
                projectile.type == ProjectileID.RichGravestone4 ||
                projectile.type == ProjectileID.RichGravestone5 ||
                projectile.type == ProjectileID.Headstone)
            {
                projectile.active = false;
            }
        }
    }
}
