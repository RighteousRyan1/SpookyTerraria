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

namespace SpookyTerraria
{
	public class SoundPlayer : ModPlayer
    {

        public int caveRumbleTimer;
        public int oceanWavesTimer;
        public int blizzTimer;
        public int cricketsTimer;
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
                player.GetModPlayer<SpookyPlayer>().breezeTimer = 0;
                oceanWavesTimer = 0;
                cricketsTimer = 0;
                blizzTimer = 0;
                caveRumbleTimer = 0;
            }
        }
        public override void PostUpdate()
        {
        }
        // FIXME: I PLAN ON MOVING THIS ENTIRE SYSTEM TO MUSIC IF I CANNOT GET IT TO WORK
        public override void PostUpdateMiscEffects()
        {
            Main.soundInstanceMenuTick.Stop();
            SoundEffectInstance breezeSounds;
            breezeSounds = Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, $"Sounds/Custom/Ambience/Breezes"));
            SoundEffectInstance crickets;
            crickets = Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, $"Sounds/Custom/Ambience/Biome/ForestAmbience"));
            SoundEffectInstance waves;
            waves = Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, $"Sounds/Custom/Ambience/Biome/OceanAmbience"));
            SoundEffectInstance blizz;
            blizz = Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, $"Sounds/Custom/Ambience/Biome/SnowAmbience"));
            SoundEffectInstance cavesSound;
            cavesSound = Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, $"Sounds/Custom/Ambience/Biome/CaveRumble"));
            // Use SoundEffectInstance Eventually
            caveRumbleTimer++;
            cricketsTimer++;
            blizzTimer++;
            oceanWavesTimer++;
            // Main.NewText($"CaveRumble: {caveRumbleTimer}, OceanWaves: {oceanWavesTimer}, Crickets: {cricketsTimer}, Blizzard: {blizzTimer}");
            if (Main.gameMenu)
            {
                cavesSound.Stop();
                crickets.Stop();
                breezeSounds.Stop();
                blizz.Stop();
                waves.Stop();
            }
            if (player.ZoneRockLayerHeight) // Rock Layer
            {
                waves.Stop();
                crickets.Stop();
                breezeSounds.Stop();
                blizz.Stop();
                oceanWavesTimer = 0;
                cricketsTimer = 0;
                blizzTimer = 0;
                GeneralHelpers.ResetTimer(oceanWavesTimer);
                GeneralHelpers.ResetTimer(cricketsTimer);
                GeneralHelpers.ResetTimer(blizzTimer);
                // Checked
            }
            if (player.ZoneBeach) // Beach
            {
                crickets.Stop();
                breezeSounds.Stop();
                blizz.Stop();
                cavesSound.Stop();
                cricketsTimer = 0;
                caveRumbleTimer = 0;
                blizzTimer = 0;
                GeneralHelpers.ResetTimer(caveRumbleTimer);
                GeneralHelpers.ResetTimer(cricketsTimer);
                GeneralHelpers.ResetTimer(blizzTimer);
                // Checked
            }
            if (player.ZoneDirtLayerHeight)
            {
                oceanWavesTimer = 0;
                caveRumbleTimer = 0;
            }
            if (player.ZoneSnow && !player.ZoneDirtLayerHeight && !player.ZoneRockLayerHeight) // Snow
            {
                waves.Stop();
                crickets.Stop();
                breezeSounds.Stop();
                cavesSound.Stop();
                oceanWavesTimer = 0;
                caveRumbleTimer = 0;
                cricketsTimer = 0;
                GeneralHelpers.ResetTimer(oceanWavesTimer);
                GeneralHelpers.ResetTimer(cricketsTimer);
                GeneralHelpers.ResetTimer(blizzTimer);
                // checked
            }
            if (PlayerIsInForest(player)) // Forest
            {
                oceanWavesTimer = 0;
                caveRumbleTimer = 0;
                blizzTimer = 0;
                GeneralHelpers.ResetTimer(oceanWavesTimer);
                GeneralHelpers.ResetTimer(caveRumbleTimer);
                GeneralHelpers.ResetTimer(blizzTimer);
                /// checked
            }
            if (player.ZoneRockLayerHeight && caveRumbleTimer == 1680)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/Biome/CaveRumble"));
                caveRumbleTimer = 0;
            }
            if (caveRumbleTimer == 2)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/Biome/CaveRumble"));
            }
            if (oceanWavesTimer == 2)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/Biome/OceanAmbience"));
            }
            if (blizzTimer == 2)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/Biome/SnowAmbience"));
            }
            if (cricketsTimer == 2)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/Biome/ForestAmbience"));
            }
            if (player.ZoneBeach && oceanWavesTimer == 9060)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/Biome/OceanAmbience"));
                oceanWavesTimer = 0;
            }
            if (player.ZoneSnow && !player.ZoneDirtLayerHeight && !player.ZoneRockLayerHeight && blizzTimer == 6060)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/Biome/SnowAmbience"));
                blizzTimer = 0;
            }
            if (PlayerIsInForest(player) && cricketsTimer == 2700)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/Biome/ForestAmbience"));
                cricketsTimer = 0;
            }
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
            // Handle stamina, etc
            bool staminaIsZero = player.GetModPlayer<StaminaPlayer>().Stamina == 0; // Stamina is zero.
            bool sprintHandling = SpookyTerraria.Sprint.Current && player.velocity.X != 0 && player.velocity.Y == 0; // Is the player holding shift, etc.
            bool tooLowStamina = player.velocity.X != 0 && !SpookyTerraria.Sprint.Current && player.GetModPlayer<StaminaPlayer>().Stamina <= 25 && player.GetModPlayer<StaminaPlayer>().Stamina > 0;
            bool notJumpingAndNotLowStaminaAndStaminaIsGreaterThan25 = player.velocity.X != 0 && player.velocity.Y == 0 && !SpookyTerraria.Sprint.Current && player.GetModPlayer<StaminaPlayer>().Stamina > 25;
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
            // Main.NewText($"{tooLowStamina} (Stamina <= 25%) {sprintHandling} (Sprinting) {notJumpingAndNotLowStaminaAndStaminaIsGreaterThan25} (No Sprint or Low Stamina) {staminaIsZero} (Stamina is Zero)");
            if (ModLoader.GetMod("TerrariaOverhaul") == null)
            {
                if (player.legFrame.Y == player.legFrame.Height * 9 && isOnGrassyTile) // Check for step timer
                {
                    stepTimer = 40;
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Action/GrassStep1"), player.Bottom); // Play sound
                }
                if (player.legFrame.Y == player.legFrame.Height * 16 && isOnGrassyTile)
                {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Action/GrassStep2"), player.Bottom); // Play sound
                    stepTimer = 0;
                }
                if (player.legFrame.Y == player.legFrame.Height * 9 && isOnStoneTile) // Check for step timer
                {
                    stepTimer = 40;
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Action/StoneStep1"), player.Bottom); // Play sound
                }
                if (player.legFrame.Y == player.legFrame.Height * 16 && isOnStoneTile)
                {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Action/StoneStep2"), player.Bottom); // Play sound
                    stepTimer = 0;
                }
                if (player.legFrame.Y == player.legFrame.Height * 9 && isOnWoodTile) // Check for step timer
                {
                    stepTimer = 40;
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Action/WoodStep1"), player.Bottom); // Play sound
                }
                if (player.legFrame.Y == player.legFrame.Height * 16 && isOnWoodTile)
                {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Action/WoodStep2"), player.Bottom); // Play sound
                    stepTimer = 0;
                }
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
