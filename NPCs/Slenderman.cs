using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using SpookyTerraria.Utilities;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SpookyTerraria.NPCs
{
    public class Slenderman : ModNPC
    {
        public override bool CloneNewInstances => true;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
            Main.npcFrameCount[npc.type] = 7;
        }
        public override void SetDefaults()
        {
            npc.width = 42;
            npc.height = 75;
            npc.damage = 1;
            npc.defense = 6;
            npc.lifeMax = 50;
            npc.dontTakeDamage = true;
            npc.HitSound = SoundID.NPCHit6;
            npc.DeathSound = SoundID.NPCDeath6;
            npc.value = 60f;
            npc.knockBackResist = 0f;
            npc.noTileCollide = true;
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            damage = target.statLifeMax2 + 50;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (target.statLife <= 0)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Action/Jumpscare"), target.Center);
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return !ModContent.GetInstance<SpookyTerraria>().beatGame && Main.player[Main.myPlayer].townNPCs < 4 ? .25f : 0f;
        }
        public override void PostAI()
        {
            Player player = Main.LocalPlayer;
            bool facingTowardsSlendermanLeft = npc.Center.X < player.Center.X && player.direction == -1;
            bool facingTowardsSlendermanRight = npc.Center.X > player.Center.X && player.direction == 1;
            bool noLight = Lighting.GetSubLight(npc.Top).X < 0.05f;
            if (noLight)
            {
                npc.alpha += 3;
            }
            else
            {
                npc.alpha -= 3;
            }
            if (!facingTowardsSlendermanLeft && !facingTowardsSlendermanRight)
            {
                npc.alpha += 3;
            }
            npc.alpha = Utils.Clamp(npc.alpha, TransparencyHelper.Visible, TransparencyHelper.Invisible);
        }
        public Vector2 destination;
        public int timerUntilteleportation;
        public override void AI()
        {
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            npc.spriteDirection = -npc.direction;
            if (npc.Center.Y >= player.Center.Y)
            {
                destination = player.Center - new Vector2(0, 150);
            }
            else if (npc.Center.Y < player.Center.Y)
            {
                destination = player.Center - new Vector2(0, 0);
            }
            if (SpookyPlayer.Pages == 0)
            {
                npc.active = false;
            }
            if (SpookyPlayer.Pages == 1)
            {
                npc.velocity = (destination - npc.Center) / 500;
            }
            if (SpookyPlayer.Pages == 2)
            {
                npc.velocity = (destination - npc.Center) / 450;
            }
            if (SpookyPlayer.Pages == 3)
            {
                npc.velocity = (destination - npc.Center) / 300;
            }
            if (SpookyPlayer.Pages == 4)
            {
                npc.velocity = (destination - npc.Center) / 250;
            }
            if (SpookyPlayer.Pages == 5)
            {
                npc.velocity = (destination - npc.Center) / 150;
            }
            if (SpookyPlayer.Pages == 6)
            {
                npc.velocity = (destination - npc.Center) / 75;
            }
            if (SpookyPlayer.Pages == 7)
            {
                npc.velocity = (destination - npc.Center) / 25;
            }
            if (SpookyPlayer.Pages == 8)
            {
                npc.active = false;
            }
            npc.ai[0]++;
            if (npc.ai[0] == 4)
            {
                npc.frame.Y += 104;
                npc.ai[0] = 0;
            }
            // 728
            if (npc.frame.Y >= 728)
            {
                npc.frame.Y = 0;
            }
            float distance = npc.Distance(player.Center);
            if (distance < 500f)
            {
                timerUntilteleportation++;
                if (timerUntilteleportation >= 1800)
                {
                    int rand = Main.rand.Next(-1800, 1800);
                    npc.Center -= new Vector2(rand, rand);
                    timerUntilteleportation = 0;
                }
            }
            if (distance > 2000f)
            {
                bool isRightOfPlayer = npc.Center.X > player.Center.X;
                npc.Center += isRightOfPlayer ? new Vector2(Main.rand.Next(-1500, -500), 0) : new Vector2(Main.rand.Next(500, 1500), 0);
            }
            if (player.dead)
            {
                npc.active = false;
            }
        }
	}
}