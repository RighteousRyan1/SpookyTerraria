using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using SpookyTerraria.Utilities;
using Microsoft.Xna.Framework.Graphics;

namespace SpookyTerraria.NPCs
{
    // TODO: Multiple stalker spawns
    // TODO: Heartrate fix when near stalker
    public class Stalker : ModNPC
    {
        public override string Texture => "SpookyTerraria/NPCs/Stalker1";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
            Main.npcFrameCount[npc.type] = 5;
        }
        public override void SetDefaults()
        {
            npc.width = 42;
            npc.height = 110;
            npc.damage = 1;
            npc.defense = 6;
            npc.lifeMax = 50;
            npc.dontTakeDamage = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 60f;
            npc.knockBackResist = 0f;
            npc.alpha = 200;
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
            return Main.worldName != SpookyTerrariaUtils.slenderWorldName && !spawnInfo.player.GetModPlayer<SpookyPlayer>().stalkerConditionMet && !ModContent.GetInstance<SpookyTerraria>().beatGame && spawnInfo.player.townNPCs < 4 ? .25f : 0f;
        }
        public override void PostAI()
        {
            if (Main.worldName == SpookyTerrariaUtils.slenderWorldName)
            {
                npc.active = false;
            }
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
        }
        public override void AI()
        {
            // NOTE: 72 per frame
            int defaultFrame = 144;
            npc.ai[1]++;
            npc.TargetClosest(false);
            Player player = Main.player[npc.target];
            float distance = npc.Distance(player.Center);
            if (distance <= 500f && distance > 300f)
            {
                npc.frame.Y = defaultFrame;
            }
            if (distance <= 300f && distance > 200f)
            {
                npc.frame.Y = defaultFrame * 2;
            }
            if (distance <= 200f && distance > 100f)
            {
                npc.frame.Y = defaultFrame * 3;
            }
            if (distance <= 100f)
            {
                npc.frame.Y = defaultFrame * 4;
            }
            if (distance > 500f)
            {
                npc.frame.Y = 0;
            }
            if (npc.ai[0] >= 120)
            {
                for (int i = 0; i < 8; i++)
                {
                    Gore.NewGore(new Vector2(Main.rand.Next(0, npc.width), Main.rand.Next(0, npc.height)), new Vector2(Main.rand.Next(-4, 4), Main.rand.Next(-4, 4)), Main.rand.Next(GoreID.ChimneySmoke1, GoreID.ChimneySmoke3));
                }
                npc.active = false;
            }
            if (npc.ai[1] > 1200)
            {
                npc.active = false;
            }
            // Main.NewText(distance);
            if (distance > 2000f)
            {
                npc.active = false;
            }
            if (player.dead)
            {
                npc.active = false;
            }
        }
    }
}