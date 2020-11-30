using Terraria.Graphics.Effects;
using Microsoft.Xna.Framework;
using SpookyTerraria.NPCs;
using SpookyTerraria.OtherItems;
using SpookyTerraria.TownNPCSpawnBooks;
using System.Collections.Generic;
using System.ComponentModel;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using SpookyTerraria.ModIntances;
using SpookyTerraria.Buffs;
using Microsoft.Xna.Framework.Audio;
using SpookyTerraria.Utilities;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using System;

namespace SpookyTerraria
{
	public class SoundPlayer : ModPlayer
    {
        // FIXME: If multiplayer goes haywire, remake these to NOT abuse static variables
        public static int caveRumbleTimer;
        public static int oceanWavesTimer;
        public static int blizzTimer;
        public static int cricketsTimer;
        public static bool PlayerIsInForest(Player player)
        {
            return !player.ZoneJungle
                && !player.ZoneDungeon
                && !player.ZoneCorrupt
                && !player.ZoneCrimson
                && !player.ZoneHoly
                && !player.ZoneSnow
                && !player.ZoneUndergroundDesert
                && !player.ZoneGlowshroom
                && !player.ZoneMeteor
                && !player.ZoneBeach
                && !player.ZoneDesert
                && player.ZoneOverworldHeight;
        }
        /*public static byte[] i = new byte[] { 1, 2, 3 };
        public static SoundEffect soundEffect = new SoundEffect(i, 1, AudioChannels.Mono);
        public static SoundEffectInstance wtf = soundEffect.CreateInstance();*/
        public SoundEffectInstance Crickets;
        public override void OnEnterWorld(Player player)
        {
        }
        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (!Main.hasFocus)
            {
                player.GetModPlayer<SpookyPlayer>().hootTimer = 0;
                player.GetModPlayer<SpookyPlayer>().breezeTimer = 0;
                oceanWavesTimer = 0;
                cricketsTimer = 0;
                blizzTimer = 0;
                caveRumbleTimer = 0;
            }
        }
        public override void PreUpdate()
        {
            Tile mouseTile = Framing.GetTileSafely(Main.MouseWorld.ToWorldCoordinates() / 16);
            if (mouseTile != null)
            {
            }

            if (player.wet && player.velocity.Y != 0)
            {
                player.AddBuff(BuffID.Flipper, 2);
            }
        }
        public override void PostUpdate()
        {
        }
        // FIXME: I PLAN ON MOVING THIS ENTIRE SYSTEM TO MUSIC IF I CANNOT GET IT TO WORK
        public override void PostUpdateMiscEffects()
        {
            SoundEngine.Play();
        }
    }
    public class BeatGamePlayer : ModPlayer
    {
        public override void ModifyDrawHeadLayers(List<PlayerHeadLayer> layers)
        {
            // layers.RemoveAt()
        }
        public override void ModifyDrawInfo(ref PlayerDrawInfo drawInfo)
        {

        }
        public override void PostUpdate()
        {
            // NOt chaing thanks: 1
            // Not change frames in order: 5, 6, 1, 12
            // walking5, walking6, walking1, walking9, walking10, walking11, 
            // player.bodyFrame.Y = player.bodyFrame.Height * (int)SpookyTerrariaUtils.BodyFrames.walking13;
            if (player.CountItem(ModContent.ItemType<Paper>()) >= 8)
            {
                ModContent.GetInstance<SpookyTerraria>().beatGame = true;
            }
            else if (player.CountItem(ModContent.ItemType<Paper>()) < 8)
            {
                ModContent.GetInstance<SpookyTerraria>().beatGame = false;
            }
        }
        public override void UpdateBiomeVisuals()
        {
            player.ManageSpecialBiomeVisuals("SpookyTerraria:BlackSky", !ModContent.GetInstance<SpookyTerraria>().beatGame && !Main.gameMenu);
            // player.ManageSpecialBiomeVisuals("SpookyTerraria:Darkness", player.HasBuff(ModContent.BuffType<Shrouded_3>()));
        }
    }
    public class SpookyPlayer : ModPlayer
    {
        /// <summary>
        /// The player's page count
        /// </summary>
        public static int pages;
        /// <summary>
        /// Determines whether or not the player has a buff
        /// </summary>
        public bool hasBuff;
        /// <summary>
        /// The player has more than 11 buffs at once.
        /// </summary>
        public bool hasGreaterThan11Buffs;
        /// <summary>
        /// [Deprecated] [Unused]
        /// </summary>
        public bool fallingTooFast;
        /// <summary>
        /// Used Directly for the fists, this determines the first phase of a punch
        /// </summary>
        public bool punchPhase1;
        public bool punchPhase2;
        public bool punchPhase3;
        public bool punchPhase4;
        public bool punchingUp;
        public bool punchingNeutral;
        public bool punchingDown;
        public bool punchingCharged;
        public bool punchingLight;
        /// <summary>
        /// Determines whether or not the player has equipped the heartrate monitor
        /// </summary>
        public bool accHeartMonitor;
        /// <summary>
        /// Decrementation timer for heartbeats
        /// </summary>
        public int hbDecTimer;
        /// <summary>
        /// Incrementation timer for heartbeats
        /// </summary>
        public int hbIncTimer;
        /// <summary>
        /// The player's heartrate
        /// </summary>
        public int heartRate;
        /// <summary>
        /// Is > 0 stalkers alive?
        /// </summary>
        public bool stalkerConditionMet;
        /// <summary>
        /// Time for tree breezes
        /// </summary>
        public int breezeTimer;
        /// <summary>
        /// Time for owl hoots
        /// </summary>
        public int hootTimer; // Time to hoot
        /// <summary>
        /// Obsolete and deprecated
        /// </summary>
        public int stepTimer;
        /// <summary>
        /// Is the player in the forest
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static bool PlayerIsInForest(Player player)
        {
            return !player.ZoneJungle
                && !player.ZoneDungeon
                && !player.ZoneCorrupt
                && !player.ZoneCrimson
                && !player.ZoneHoly
                && !player.ZoneSnow
                && !player.ZoneUndergroundDesert
                && !player.ZoneGlowshroom
                && !player.ZoneMeteor
                && !player.ZoneBeach
                && !player.ZoneDesert
                && player.ZoneOverworldHeight;
        }
        /// <summary>
        /// Is the player underground?
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static bool PlayerUnderground(Player player)
        {
            return !player.ZoneJungle
                && !player.ZoneDungeon
                && !player.ZoneCorrupt
                && !player.ZoneCrimson
                && !player.ZoneHoly
                && !player.ZoneSnow
                && !player.ZoneUndergroundDesert
                && !player.ZoneGlowshroom
                && !player.ZoneMeteor
                && !player.ZoneBeach
                && !player.ZoneDesert
                && player.Center.Y >= Main.rockLayer * 16;
        }
        public override void SetupStartInventory(IList<Item> items, bool mediumcoreDeath)
        {
            items.Clear();
            Item item1 = new Item();
            item1.SetDefaults(ModContent.ItemType<Flashlight.Flashlight>(), false);
            Item item2 = new Item();
            item2.SetDefaults(ModContent.ItemType<Fists.Fists>(), false);
            Item item3 = new Item();
            item3.SetDefaults(ModContent.ItemType<HeartrateMonitor>(), false);
            Item item4 = new Item();
            item4.SetDefaults(ModContent.ItemType<Battery>(), false);
            item4.stack = 5;
            items.Add(item1);
            items.Add(item2);
            items.Add(item3);
            items.Add(item4);
        }
        public int msgTimer;
        public override void OnEnterWorld(Player player)
        {
            player.GetModPlayer<StaminaPlayer>().Stamina = 100;
            heartRate = 80;
            if (!ModContent.GetInstance<SpookyTerraria>().beatGame)
            {
                if (player.ZoneRockLayerHeight)
                {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/Biome/CaveRumble"));
                }
                if (player.ZoneBeach)
                {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/Biome/OceanAmbience"));
                }
                if (player.ZoneSnow)
                {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/Biome/SnowAmbience"));
                }
                if (PlayerIsInForest(player))
                {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/Biome/ForestAmbience"));
                }
            }
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/Breezes"));
        }
        public bool hoveringBuff;
        public override void ResetEffects()
        {
            hoveringBuff = false;
            accHeartMonitor = false;
            hasBuff = false;
            hasGreaterThan11Buffs = false;
        }
        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (damageSource.SourceNPCIndex <= -1)
            {
                return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
            }
            if (damageSource.SourceNPCIndex > -1)
            {
                if (Main.npc[damageSource.SourceNPCIndex].type == ModContent.NPCType<Stalker>())
                {
                    switch (Main.rand.Next(1, 6))
                    {
                        case 1:
                            damageSource = PlayerDeathReason.ByCustomReason($"{player.name} was stalked away by Stalker."); break;
                        case 2:
                            damageSource = PlayerDeathReason.ByCustomReason($"A stalker has just mangled {player.name} to death."); break;
                        case 3:
                            damageSource = PlayerDeathReason.ByCustomReason($"{player.name} walked into inevitable doom."); break;
                        case 4:
                            damageSource = PlayerDeathReason.ByCustomReason($"{player.name} withered away from the Stalker's deadly gleam."); break;
                        case 5:
                            damageSource = PlayerDeathReason.ByCustomReason($"The Stalker has consumed {player.name} as his prey."); break;
                    }
                }
            }
            return true;
        }
        public Vector2 facingRightToFistOffset;
        public Vector2 facingLeftToFistOffset;
        public override void PreUpdate()
        {
            /*
            if (punchPhase1 && punchingCharged && player.itemAnimation != 0)
            {
                facingRightToFistOffset = new Vector2(player.MountedCenter.X - 7, player.MountedCenter.Y + 5);
                facingLeftToFistOffset = new Vector2(player.MountedCenter.X + 7, player.MountedCenter.Y + 5);
                Dust.NewDust(player.direction == 1 ? facingRightToFistOffset : facingLeftToFistOffset, 15, 15, 20, 0f, 0f, 0, new Color(0, 242, 255), 0.46f);            
            }
            else if (punchPhase2 && punchingCharged && player.itemAnimation != 0)
            {
                facingRightToFistOffset = new Vector2(player.MountedCenter.X - 4, player.MountedCenter.Y + 5);
                facingLeftToFistOffset = new Vector2(player.MountedCenter.X + 4, player.MountedCenter.Y + 5);
                Dust.NewDust(player.direction == 1 ? facingRightToFistOffset : facingLeftToFistOffset, 15, 15, 20, 0f, 0f, 0, new Color(0, 242, 255), 0.46f);
            }
            else if (punchPhase4 && punchingCharged && player.itemAnimation != 0)
            {
                facingRightToFistOffset = new Vector2(player.MountedCenter.X - 7, player.MountedCenter.Y + 5);
                facingLeftToFistOffset = new Vector2(player.MountedCenter.X + 10, player.MountedCenter.Y);
                Dust.NewDust(player.direction == 1 ? facingRightToFistOffset : facingLeftToFistOffset, 15, 15, 20, 0f, 0f, 0, new Color(0, 242, 255), 0.46f);
            }
            */
            // ^ Maybe something for a new mod ;-;
            // player.bodyFrame.Y = player.bodyFrame.Height * (int)SpookyTerrariaUtils.BodyFrames.walking1;
            // 109 == flipper
            // 131 == turtle
            // 168 == fishron mount

            Main.soundInstanceMenuTick.Volume = 0f;
            Main.soundInstanceMenuOpen.Volume = 0f;
            Main.soundInstanceMenuClose.Volume = 0f;
            // TODO: Do something with this shit bro
            bool hasShroudedIIIRequirement = Lighting.GetSubLight(player.Top).X == 0f;
            bool hasShroudedIIRequirement = Lighting.GetSubLight(player.Top).X > 0f
                && Lighting.GetSubLight(player.Top).X < 0.05f;
            bool hasShroudedIRequirement = Lighting.GetSubLight(player.Top).X >= 0.05f
                && Lighting.GetSubLight(player.Top).X < 0.1f;
            if (hasShroudedIIIRequirement && !hasShroudedIIRequirement && hasShroudedIIIRequirement)
            {
                player.AddBuff(ModContent.BuffType<Shrouded_3>(), 2, false);
            }
            if (hasShroudedIIRequirement && !hasShroudedIIIRequirement && !hasShroudedIRequirement)
            {
                player.AddBuff(ModContent.BuffType<Shrouded_2>(), 2, false);
            }
            if (!hasShroudedIIRequirement && !hasShroudedIIIRequirement && hasShroudedIRequirement)
            {
                player.AddBuff(ModContent.BuffType<Shrouded_1>(), 2, false);
            }
        }
        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
        }
        /// <summary>
        /// Clamped value of player.headRotation
        /// </summary>
        float MinMax;


        public override void ModifyDrawInfo(ref PlayerDrawInfo drawInfo)
        {
        }
        // This is being moved to BetterMechanics, if need be, it will be a dependency?
        public void IncrementHeartRate()
        {
            for (int index = 0; index < Main.maxNPCs; index++)
            {
                NPC npc = Main.npc[index];
                if (npc.type == ModContent.NPCType<Stalker>() && npc.active)
                {
                    stalkerConditionMet = true;
                    hbDecTimer = 0;
                    hbIncTimer++;
                    float distnaceToStalker = player.Distance(npc.Center);
                    if (distnaceToStalker >= 750f && hbIncTimer >= 50) // Slow heartrate increase
                    {
                        heartRate++;
                        hbIncTimer = 0;
                    }
                    if (distnaceToStalker < 750f && hbIncTimer >= 25) // Semi-Fast heartrate increase
                    {
                        heartRate++;
                        hbIncTimer = 0;
                    }
                    if (distnaceToStalker < 300f && hbIncTimer >= 12) // FAST INCREASE IN HEARTRATE!
                    {
                        heartRate++;
                        hbIncTimer = 0;
                    }
                    if (distnaceToStalker < 100f && hbIncTimer >= 8) // SANIK!
                    {
                        heartRate++;
                        hbIncTimer = 0;
                    }
                    if (heartRate > 240) // Kill the player
                    {
                        switch (Main.rand.Next(1, 5))
                        {
                            case 1:
                                player.KillMe(PlayerDeathReason.ByCustomReason($"{player.name}'s heartrate got too high."), player.statLifeMax2 + 50, 0, false);
                                break;
                            case 2:
                                player.KillMe(PlayerDeathReason.ByCustomReason($"{player.name}'s heart pounded out of their own chest."), player.statLifeMax2 + 50, 0, false);
                                break;
                            case 3:
                                player.KillMe(PlayerDeathReason.ByCustomReason($"{player.name} exposed themselves for too long."), player.statLifeMax2 + 50, 0, false);
                                break;
                            case 4:
                                player.KillMe(PlayerDeathReason.ByCustomReason($"The BPM of {player.name} reached {heartRate}! That's no good!"), player.statLifeMax2 + 50, 0, false);
                                break;
                        }
                        heartRate = 0;
                    }
                }
            }
        }
        /// <summary>
        /// (Sadly) extremely hardcoded heartrate decrementation. Remaking of this soon?
        /// </summary>
        public void DecrementHeartRate()
        {
            for (int index = 0; index < Main.maxNPCs; index++)
            {
                NPC npc = Main.npc[index];
                if (npc.type == ModContent.NPCType<Stalker>() && !npc.active)
                {
                    stalkerConditionMet = false;
                    hbIncTimer = 0;
                    hbDecTimer++;
                    if (!player.ZoneBeach)
                    {
                        if (hbDecTimer >= 20)
                        {
                            heartRate--;
                            hbDecTimer = 0;
                        }
                        if (heartRate < 80)
                        {
                            heartRate = 80;
                        }
                    }
                    if (player.ZoneBeach)
                    {
                        if (hbDecTimer >= 15)
                        {
                            heartRate--;
                            hbDecTimer = 0;
                        }
                        if (heartRate > 60 && hbDecTimer >= 15)
                        {
                            heartRate--;
                        }
                        if (heartRate < 60)
                        {
                            heartRate = 60;
                        }
                    }
                }
            }
        }
        public override void PostUpdate()
        {
            if (msgTimer < 301)
            {
                msgTimer++;
            }
            //byte[] i = new byte[] { 1, 2, 3 };
            //Microsoft.Xna.Framework.Audio.SoundEffect Poop = new Microsoft.Xna.Framework.Audio.SoundEffect(i, 1, Microsoft.Xna.Framework.Audio.AudioChannels.Stereo);
            //poop
            Mod ST = ModLoader.GetMod("SpookyTerraria");
            if (msgTimer == 120)
            {
                Main.NewText($"Spooky Terraria is on Alpha version {ST.Version}.", Color.DarkGray);
            }
            if (msgTimer == 300)
            {
                Main.NewText($"Also, in participation in the alpha version of this mod, I kindly ask you to report any bugs to my discord server (https://discord.gg/pT2BzSG).", Color.DarkGray);
            }
            IncrementHeartRate(); // Increment Appropriately
            DecrementHeartRate(); // Decrement Appropriately

            // TODO: Maybe add player.wet here
            // Ambience
            hootTimer++;
            breezeTimer++;
            Main.invasionProgress = 0;
            Main.CanStartInvasion(0, false);
            if (hootTimer == 400 && PlayerIsInForest(player) && !player.ZoneSkyHeight)
            {
                if (ModContent.GetInstance<SpookyConfigClient>().toggleHoots)
                {
                    if (!ModContent.GetInstance<SpookyTerraria>().beatGame)
                    {
                        if (Main.rand.NextFloat() < 0.35f)
                        {
                            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/HootOne"));
                        }
                        else if (Main.rand.NextFloat() < 0.35f)
                        {
                            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/HootTwo"));
                        }
                        hootTimer = 0;
                    }
                }
            }
            // I really need wolves howling in the snow
            if (ModContent.GetInstance<SpookyConfigClient>().toggleWind)
            {
                if (breezeTimer >= 2650)
                {
                    if (Main.rand.NextFloat() < 0.35f)
                    {
                        Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/Breezes"));
                    }
                    breezeTimer = 0;
                }
            }
            // Main.NewText(stalkerConditionMet);
            // Main.NewText($"{hbDecTimer} <= Decrement, {hbIncTimer} <= Increment");
        }
        /// <summary>
        /// Is the player on a grassy tile?
        /// </summary>
        public bool isOnGrassyTile = false;
        /// <summary>
        /// Is the player on a wood tile?
        /// </summary>
        public bool isOnWoodTile = false;
        /// <summary>
        /// Is the player on a wood tile?
        /// </summary>
        public bool isOnStoneTile = false;
        public override void NaturalLifeRegen(ref float regen)
        {
            regen = 4 - (heartRate - 80) * 0.02f;
            float reverse = player.statLifeMax2 / 400f * 0.85f + 0.15f;
            regen /= reverse;
        }
        public override void PostUpdateRunSpeeds()
        {
            SpookyTerrariaUtils.HandleMaxRunSpeeds(4);

            // Handle stamina, etc
            bool staminaIsZero = player.GetModPlayer<StaminaPlayer>().Stamina == 0; // Stamina is zero.
            bool sprintHandling = SpookyTerraria.Sprint.Current && player.velocity.X != 0 && player.velocity.Y == 0; // Is the player holding shift, etc.
            bool tooLowStamina = player.velocity.X != 0 && !SpookyTerraria.Sprint.Current && player.GetModPlayer<StaminaPlayer>().Stamina <= 25 && player.GetModPlayer<StaminaPlayer>().Stamina > 0;
            bool notJumpingAndNotLowStaminaAndStaminaIsGreaterThan25 = player.velocity.X != 0 && player.velocity.Y == 0 && !SpookyTerraria.Sprint.Current && player.GetModPlayer<StaminaPlayer>().Stamina > 25;
            if (!player.wet)
            {
                if (notJumpingAndNotLowStaminaAndStaminaIsGreaterThan25 && !player.mount.Active)
                {
                    player.maxRunSpeed = 1.5f * (heartRate / 80f);
                    player.accRunSpeed = 1.5f * (heartRate / 80f);
                }
                if (sprintHandling && !staminaIsZero && !player.mount.Active)
                {
                    player.maxRunSpeed = 1.5f * (heartRate / 80f) * 2f;
                    player.accRunSpeed = 1.5f * (heartRate / 80f) * 2f;
                }
                else if (!tooLowStamina && !player.mount.Active)
                {
                    player.maxRunSpeed = 1.5f * (heartRate / 80f);
                    player.accRunSpeed = 1.5f * (heartRate / 80f);
                }
                if (tooLowStamina && !player.mount.Active)
                {
                    player.maxRunSpeed = 1.5f * (heartRate / 80f) / 2f;
                    player.accRunSpeed = 1.5f * (heartRate / 80f) / 2f; // Fix the sprinting 
                }
            }
            // Main.NewText($"{tooLowStamina} (Stamina <= 25%) {sprintHandling} (Sprinting) {notJumpingAndNotLowStaminaAndStaminaIsGreaterThan25} (No Sprint or Low Stamina) {staminaIsZero} (Stamina is Zero)");
            if (ModLoader.GetMod("TerrariaOverhaul") == null)
            {
                SpookyTerrariaUtils instance = new SpookyTerrariaUtils();
                // x.PlayStepSound("Sounds/Custom/Ambient/HootOne", "Sounds/Custom/Ambient/HootTwo", "Sounds/Custom/Ambient/HootOne", "Sounds/Custom/Ambient/HootTwo", "Sounds/Custom/Ambient/HootOne", "Sounds/Custom/Ambient/HootTwo");
                instance.PlayStepSound();
            }
        }
        public int lightTimer;
        public override void PostUpdateMiscEffects()
        {
            Player.jumpSpeed *= (float)player.GetModPlayer<StaminaPlayer>().Stamina / 100;
            Item item = player.HeldItem;
            if (item.type == ModContent.ItemType<Flashlight.Flashlight>())
            {
                lightTimer++;
                if (lightTimer == 1)
                {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Items/FlashLightClick"), player.Center);
                }
            }
            else
            {
                lightTimer = 0;
            }
        }
    }
}
