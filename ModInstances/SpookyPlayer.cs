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
        public override void ModifyDrawInfo(ref PlayerDrawInfo drawInfo)
        {
            player.fullRotationOrigin = player.Hitbox.Size() / 2;
        }
        public override void PreUpdate()
        {
            if (player.wet)
            {
                player.AddBuff(BuffID.Flipper, 2);
            }
        }
        public override void PostUpdate()
        {
            // 109 == flipper
            // 131 == turtle
            // 168 == fishron mount
            player.ChangeDir(player.wet && player.velocity.X > 0 ? 1 : -1);
            if (player.wet && !player.mount.Active)
            {
                player.fullRotation = player.velocity.ToRotation() + MathHelper.PiOver2;
            }
            else if (player.wet && player.mount.Active)
            {
                player.fullRotation = 0;
            }
            else if (!player.wet && !player.mount.Active)
            {
                player.fullRotation = 0;
            }
        }
        // FIXME: I PLAN ON MOVING THIS ENTIRE SYSTEM TO MUSIC IF I CANNOT GET IT TO WORK
        public override void PostUpdateMiscEffects()
        {
            SoundEngine.Play();
        }
    }
    public class BeatGamePlayer : ModPlayer
    {
        public override void PostUpdate()
        {
            if (player.CountItem(ModContent.ItemType<Paper>()) >= 300)
            {
                ModContent.GetInstance<SpookyTerraria>().beatGame = true;
            }
            else if (player.CountItem(ModContent.ItemType<Paper>()) < 300)
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
        public override void ResetEffects()
        {
            accHeartMonitor = false;
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
        public override void PreUpdate()
        {

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
            if (!Main.gameMenu)
            {
                if (!Main.player[1].active)
                {
                    float i = player.headRotation;
                    MinMax = Utils.Clamp(i - 0.5f, -1f, 1f);
                    if (ModLoader.GetMod("TerrariaOverhaul") == null)
                    {
                        if (player.direction == 1)
                        {
                            player.headRotation = -MinMax * (Main.MouseWorld - player.Center).ToRotation();
                        }
                        if (player.direction == -1)
                        {
                            player.headRotation = -MinMax * (-Main.MouseWorld - -player.Center).ToRotation();
                        }
                    }
                }
            }
        }
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
            if (player.direction == -1)
            {
                MathHelper.Clamp(player.headRotation, -5.5f, -1f);
            }
            else if (player.direction == 1)
            {
                MathHelper.Clamp(player.headRotation, -1f, 1f); // Why no clamp value????
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

            // TODO: Maybe add player.wet here
            if (!Main.dedServ)
            {
                if (Main.MouseWorld.X > player.Center.X)
                {
                    player.ChangeDir(1);
                }
                else
                {
                    player.ChangeDir(-1);
                }
            }
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
