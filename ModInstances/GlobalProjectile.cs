using System.Linq;
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
                if (projectile.type == ProjectileID.FallingStar)
                {
                    projectile.active = false;
                    projectile.Kill();
                }

            var tombstones = new[]
            {
                ProjectileID.Tombstone,
                ProjectileID.Obelisk,
                ProjectileID.CrossGraveMarker,
                ProjectileID.GraveMarker,
                ProjectileID.Gravestone,
                ProjectileID.RichGravestone1,
                ProjectileID.RichGravestone2,
                ProjectileID.RichGravestone3,
                ProjectileID.RichGravestone4,
                ProjectileID.RichGravestone5,
                ProjectileID.Headstone
            };

            if (tombstones.Any(x => x == projectile.type))
                projectile.active = false;
        }
    }
}