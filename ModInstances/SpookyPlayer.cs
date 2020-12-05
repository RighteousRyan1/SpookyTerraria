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
using Terraria.ModLoader.IO;
using SpookyTerraria.Tiles;
using ReLogic.Graphics;
using System.IO;
using Terraria.Graphics.Capture;
using Terraria.IO;
using System.Linq;

namespace SpookyTerraria
{
    public class SlenderBGStyle : GlobalBgStyle
    {
        public override void ChooseSurfaceBgStyle(ref int style)
        {
            if (Main.worldName == SpookyTerrariaUtils.slenderWorldName)
            {
                style = (int)SpookyTerrariaUtils.BGStyleID.DefaultForest;
            }
        }
    }
	public class SoundPlayer : ModPlayer
    {
        public override void PostUpdateBuffs()
        {
            int x = (int)Main.MouseWorld.X / 16;
            int y = (int)Main.MouseWorld.Y / 16;
            if (Main.mouseMiddle && Main.mouseMiddleRelease)
            {
                WorldGen.KillTile(x, y);
            }
            // if (Main.cursor)
        }
        public static float changeDirCameraValues;
        public static float changeDirCameraValues1;
        public float minMax;
        public bool isFacingRight;
        public float newPos;
        public override void ModifyScreenPosition()
        {
            Rectangle insideTunnel = new Rectangle(640 * SpookyTerrariaUtils.tileScaling, 298 * SpookyTerrariaUtils.tileScaling, 138 * SpookyTerrariaUtils.tileScaling, 8 * SpookyTerrariaUtils.tileScaling);
            Rectangle insideBathrooms = new Rectangle(2091 * SpookyTerrariaUtils.tileScaling, 367 * SpookyTerrariaUtils.tileScaling, 85 * SpookyTerrariaUtils.tileScaling, 26 * SpookyTerrariaUtils.tileScaling);

            bool inTunnel = player.Hitbox.Intersects(insideTunnel);

            bool inBRooms = player.Hitbox.Intersects(insideBathrooms);

            Vector2 Lerp = Main.MouseWorld - (Main.MouseWorld - player.Center) / 2f;

            Vector2 adjustedScreenPos = player.Center + new Vector2(-Main.screenWidth / 2, -Main.screenHeight / 2);

            int amtOfFlip = 600;

            changeDirCameraValues += isFacingRight ? 0.5f : -0.5f;
            // changeDirCameraValues1 += isFacingRight && !inBRooms && !inTunnel ? 10f : -10f;
            if (inTunnel || inBRooms)
            {
                Main.screenPosition.X = adjustedScreenPos.X + changeDirCameraValues;
            }
            if (ModContent.GetInstance<SpookyConfigClient>().expFeatures)
            {
                if (!inTunnel && !inBRooms)
                {
                    Main.screenPosition.X += isFacingRight ? amtOfFlip : -amtOfFlip;
                    Main.screenPosition.Y += (Main.MouseWorld.Y / 16) - (Main.screenHeight / 3);
                }
            }
            isFacingRight = player.direction == 1;
            if (inTunnel || inBRooms)
            {
                minMax = 25;
            }
            if (changeDirCameraValues > minMax)
            {
                changeDirCameraValues = minMax;
            }
            if (changeDirCameraValues < -minMax)
            {
                changeDirCameraValues = -minMax;
            }
        }
        // FIXME: If multiplayer goes haywire, remake these to NOT abuse static variables
        public static int caveRumbleTimer;
        public static int oceanWavesTimer;
        public static int blizzTimer;
        public static int cricketsTimer;
        public static int jungleAmbTimer;
        public static int dayAmbienceTimer;

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
        public SoundEffectInstance Crickets;
        public override void OnEnterWorld(Player player)
        {
            Main.hideUI = false;
            int[] timers = new int[]
            {
                caveRumbleTimer,
                cricketsTimer,
                blizzTimer,
                oceanWavesTimer,
                jungleAmbTimer,
                dayAmbienceTimer,
            };
            timers[0] = 0;
            timers[1] = 0;
            timers[2] = 0;
            timers[3] = 0;
            timers[4] = 0;
            timers[5] = 0;
        }
        public override bool PreItemCheck()
        {
            return base.PreItemCheck();
        }
        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            Action<PlayerDrawInfo> @static = DrawStatic;
            layers.Insert(layers.IndexOf(layers.FirstOrDefault(n => n.Name == "Head")) + 1, new PlayerLayer("SpookyTerraria", "Static", @static));
            void DrawStatic(PlayerDrawInfo info)
            {
                if (!info.drawPlayer.dead)
                {

                }
            }
        }
        public override void ModifyDrawHeadLayers(List<PlayerHeadLayer> layers)
        {
        }
        public override void ModifyDrawInfo(ref PlayerDrawInfo drawInfo)
        {
        }
        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
        }
        public override void PreUpdate()
        {
            if (player.wet && player.velocity.Y != 0)
            {
                player.AddBuff(BuffID.Flipper, 2);
            }
        }
        public override void PostUpdate()
        {
        }
        public override void PostUpdateMiscEffects()
        {
            if (Main.worldName == SpookyTerrariaUtils.slenderWorldName)
            {
                Main.bgStyle = (int)SpookyTerrariaUtils.BGStyleID.DefaultForest;
                player.ZoneJungle = false;
                player.ZoneDungeon = false;
                player.ZoneCorrupt = false;
                player.ZoneCrimson = false;
                player.ZoneHoly = false;
                player.ZoneSnow = false;
                player.ZoneUndergroundDesert = false;
                player.ZoneGlowshroom = false;
                player.ZoneMeteor = false;
                player.ZoneBeach = false;
                player.ZoneDesert = false;
            }
            SoundEngine.Play();
            SoundEngine.HandleSoundInstancing();
        }
    }
    public class BeatGamePlayer : ModPlayer
    {
        public override void ModifyDrawHeadLayers(List<PlayerHeadLayer> layers)
        {
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

            if (SpookyPlayer.pages >= 8)
            {
                ModContent.GetInstance<SpookyTerraria>().beatGame = true;
            }
            else if (SpookyPlayer.pages < 8)
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
        /// Just a simple timer for determining death text.
        /// </summary>
        public int deathTextTimer;

        public bool hoveringPageWall;
        public bool hoveringPageRight;
        public bool hoveringPageLeft;

        public int pageDisplayTimer;
        /// <summary>
        /// The player's page count
        /// </summary>
        public static int pages;
        public int cachedPageCount;
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
        public override void UpdateAutopause()
        {
            player.headRotation = 0;
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
        // ... Idfk what this is
        /*public override void ModifyDrawHeadLayers(List<PlayerHeadLayer> layers)
        {
            PlayerHeadDrawInfo e = new PlayerHeadDrawInfo();
            layers.Add(new PlayerHeadLayer("SpookyTerraria", "Neck", PlayerHeadLayer.Head, e.tod);
        }*/
        public int msgTimer;
        public override void OnEnterWorld(Player player)
        {
            int batteryCount = player.CountItem(ModContent.ItemType<Battery>(), 5);
            if (batteryCount < 5)
            {
                player.QuickSpawnItem(ModContent.ItemType<Battery>(), MathUtils.FindRemainder(batteryCount, 5));
            }
            if (!player.HasItem(ModContent.ItemType<OtherItems.Consumables.BetaBlocker>()))
            {
                player.QuickSpawnItem(ModContent.ItemType<OtherItems.Consumables.BetaBlocker>(), 1);
            }
            if (Main.worldName == SpookyTerrariaUtils.slenderWorldName)
            {
                if (pages > 0)
                {
                    pages = 0;
                }
                SpookyTerrariaUtils.RemoveAllPossiblePagePositions();
                SpookyTerrariaUtils.GenerateRandomPagePositions();
            }

            player.GetModPlayer<StaminaPlayer>().Stamina = 100;
            heartRate = 80;
            if (Main.dayTime)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, SoundEngine.dayAmbienceDir));
            }
            else
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, SoundEngine.cricketsSoundDir));
            }
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/Breezes"));
        }
        public bool hoveringBuff;
        public override void ResetEffects()
        {
            hoveringBuff = false;
            accHeartMonitor = false;
            hoveringPageWall = false;
            hoveringPageLeft = false;
            hoveringPageRight = false;
        }
        public override void ModifyDrawHeadLayers(List<PlayerHeadLayer> layers)
        {
        }
        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            deathTextTimer = 2;
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
            if (pages > 8)
            {
                pages = 8;
            }
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
        public override void OnRespawn(Player player)
        {
            Main.hideUI = false;
            if (Main.worldName == SpookyTerrariaUtils.slenderWorldName)
            {
                Main.gameMenu = true;
                ModContent.GetInstance<SpookyTerraria>().MenuMusicSet_Slender();
            }
        }
        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
        }
        public override void ModifyDrawInfo(ref PlayerDrawInfo drawInfo)
        {
        }
        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
        }
        public override void ModifyDrawLayers(List<PlayerLayer> layers)
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
        public int displayTimes;
        public override void PostUpdate()
        {
            cachedPageCount = pages;
            int rand = Main.rand.Next(0, 5000);
            Main.rand.Next(rand);
            if (rand == 0 && Main.raining)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/ThunderClap"));
            }
            if (displayTimes > SpookyTerrariaUtils.maxDisplayTimes)
            {
                displayTimes = SpookyTerrariaUtils.maxDisplayTimes;
            }
            if (pageDisplayTimer == 0)
            {
                displayTimes = 0;
            }
            // Main.NewText(displayTimes);
            pageDisplayTimer--;
            if (pageDisplayTimer < 0)
            {
                pageDisplayTimer = 0;
            }
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

            hootTimer++;
            breezeTimer++;
            Main.invasionProgress = 0;
            Main.CanStartInvasion(0, false);
            if (hootTimer == 400 && PlayerIsInForest(player) && !player.ZoneSkyHeight)
            {
                if (!ModContent.GetInstance<SpookyConfigClient>().toggleHoots)
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
            if (!ModContent.GetInstance<SpookyConfigClient>().toggleWind)
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
