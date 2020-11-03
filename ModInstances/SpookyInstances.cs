using Terraria.ID;
using Microsoft.Xna.Framework;
using SpookyTerraria.Flashlight;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Audio;
using Terraria.ModLoader.Config;
using System.ComponentModel;
using SpookyTerraria.NPCs;
using System.Collections.Generic;
using Terraria.DataStructures;
using SpookyTerraria.TownNPCSpawnBooks;

namespace SpookyTerraria
{
    public class SpookyConfigClient : ModConfig
    {
        [Label("Spooky Terraria Settings (Client Side)")]
        public override ConfigScope Mode => ConfigScope.ClientSide;
        [Header("Ambience Settings")]
        [Label("Toggle Winds")]
        [DefaultValue(true)]
        [Tooltip("Winds no longer blow...")]
        public bool toggleWind;

        [Label("Toggle Owl Hoots")]
        [DefaultValue(true)]
        [Tooltip("Owls no longer hoot.")]
        public bool toggleHoots;
    }
    public class SpookyConfigServer : ModConfig
    {
        [Label("Spooky Terraria Settings (Server Side)")]
        public override ConfigScope Mode => ConfigScope.ServerSide;
        [Header("Spookiness Settings")]
        [Label("Light Scalar")]
        [DefaultValue(0.9f)]
        [Range(0f, 1f)]
        public float lightingScale;

        [Label("Toggle Note Collecting")]
        [DefaultValue(false)]
        [Tooltip("The objective of the game normally is to collect 300 notes from the form of blocks. If this is on, then your objective is to beat the Moon Lord")]
        public bool normalProgression;
    }
    public class SpookyNPCs : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public override bool CloneNewInstances => true;
        public int heartBeatTimer;
        public override void NPCLoot(NPC npc)
        {
            bool downedBossAny = NPC.downedBoss1 || NPC.downedBoss2 || NPC.downedBoss3 || NPC.downedSlimeKing;
            if (Main.rand.NextFloat() < 0.04f)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<GuideBook>());
            }
            else if (Main.rand.NextFloat() < 0.04f)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<MerchantBook>());
            }
            else if (Main.rand.NextFloat() < 0.04f)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<NurseBook>());
            }
            else if (Main.rand.NextFloat() < 0.04f)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<ArmsDealBook>());
            }
            else if (Main.rand.NextFloat() < 0.04f)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<DyeTraderBook>());
            }
            else if (Main.rand.NextFloat() < 0.04f)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<PainterBook>());
            }
            else if (Main.rand.NextFloat() < 0.04f)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<DemoBook>());
            }
            if (downedBossAny)
            {
                if (Main.rand.NextFloat() < 0.04f)
                {
                    Item.NewItem(npc.getRect(), ModContent.ItemType<DryadBook>());
                }
                else if (Main.rand.NextFloat() < 0.04f)
                {
                    Item.NewItem(npc.getRect(), ModContent.ItemType<StylistBook>());
                }
                else if (Main.rand.NextFloat() < 0.04f)
                {
                    Item.NewItem(npc.getRect(), ModContent.ItemType<AnglerBook>());
                }
                else if (Main.rand.NextFloat() < 0.04f)
                {
                    Item.NewItem(npc.getRect(), ModContent.ItemType<TaxCollectBook>());
                }
                else if (Main.rand.NextFloat() < 0.04f)
                {
                    Item.NewItem(npc.getRect(), ModContent.ItemType<TravMerchBook>());
                }
                else if (Main.rand.NextFloat() < 0.04f)
                {
                    Item.NewItem(npc.getRect(), ModContent.ItemType<WitDocBook>());
                }
                else if (Main.rand.NextFloat() < 0.04f)
                {
                    Item.NewItem(npc.getRect(), ModContent.ItemType<PartyGirlBook>());
                }
                else if (Main.rand.NextFloat() < 0.04f)
                {
                    Item.NewItem(npc.getRect(), ModContent.ItemType<ClothierBook>());
                }
            }
        }
        public override void PostAI(NPC npc)
        {
            Player player = Main.player[Main.myPlayer];
            if (npc.type == ModContent.NPCType<Stalker>())
            {
                if (!player.dead)
                {
                    heartBeatTimer++;
                    if (heartBeatTimer == 1)
                    {
                        Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/HeartBeat"), npc.Center);
                    }
                    if (heartBeatTimer == 40)
                    {
                        Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/HeartBeat"), npc.Center);
                        heartBeatTimer = 2;
                    }
                }
            }
        }
    }
	public class SpookyPlayer : ModPlayer
    {
        public int hbDecTimer;
        public int hbIncTimer;
        public int heartRate;
        public bool stalkerConditionMet;
        public int breezeTimer;
        public int hootTimer; // Time to hoot
        public int stepTimer;
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
            item3.SetDefaults(ModContent.ItemType<OtherItems.HeartrateMonitor>(), false);
            items.Add(item1);
            items.Add(item2);
            items.Add(item3);
        }
        public override void UpdateBiomeVisuals()
        {
            player.ManageSpecialBiomeVisuals("SpookyTerraria:BlackSky", !Main.gameMenu);
        }
        public override void OnEnterWorld(Player player)
        {
            player.GetModPlayer<SpookyPlayer>().heartRate = 80;
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/Breezes"));
        }
        public override void ResetEffects()
        {
            stalkerConditionMet = false;
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
        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
        }
        public override void ModifyDrawInfo(ref PlayerDrawInfo drawInfo)
        {
            if (!Main.gameMenu)
            {
                if (Main.netMode != NetmodeID.Server)
                {
                    if (ModLoader.GetMod("TerrariaOverhaul") == null)
                    {
                        if (player.direction == 1)
                        {
                            player.headRotation = (Main.MouseWorld - player.Center).ToRotation();
                        }
                        if (player.direction == -1)
                        {
                            player.headRotation = (Main.MouseWorld - player.Center).ToRotation() - MathHelper.Pi;
                        }
                    }
                }
            }
        }
        public override void PostUpdate()
        {
            if (player.dead)
            {
                player.GetModPlayer<SpookyPlayer>().heartRate = 80;
            }
            for (int index = 0; index < Main.maxNPCs; index++)
            {
                NPC npc = Main.npc[index];
                if (npc.type == ModContent.NPCType<Stalker>() && npc.active)
                {
                    hbIncTimer++;
                    float distnaceToStalker = player.Distance(npc.Center);
                    if (distnaceToStalker >= 750f && hbIncTimer == 50) // Slow heartrate increase
                    {
                        player.GetModPlayer<SpookyPlayer>().heartRate++;
                        hbIncTimer = 0;
                    }
                    if (distnaceToStalker < 750f && hbIncTimer == 25) // Semi-Fast heartrate increase
                    {
                        player.GetModPlayer<SpookyPlayer>().heartRate++;
                        hbIncTimer = 0;
                    }
                    if (distnaceToStalker < 300f && hbIncTimer == 12) // FAST INCREASE IN HEARTRATE!
                    {
                        player.GetModPlayer<SpookyPlayer>().heartRate++;
                        hbIncTimer = 0;
                    }
                    if (player.GetModPlayer<SpookyPlayer>().heartRate > 240) // Kill the player
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
                    }
                }
                else if (npc.type == ModContent.NPCType<Stalker>() && !npc.active)
                {
                    hbDecTimer++;
                    if (!player.ZoneBeach)
                    {
                        if (hbDecTimer == 20)
                        {
                            player.GetModPlayer<SpookyPlayer>().heartRate--;
                            hbDecTimer = 0;
                        }
                        if (player.GetModPlayer<SpookyPlayer>().heartRate < 80)
                        {
                            player.GetModPlayer<SpookyPlayer>().heartRate = 80;
                        }
                    }
                    if (player.ZoneBeach)
                    {
                        if (hbDecTimer == 15)
                        {
                            player.GetModPlayer<SpookyPlayer>().heartRate--;
                            hbDecTimer = 0;
                        }
                        if (player.GetModPlayer<SpookyPlayer>().heartRate > 60 && hbDecTimer == 15)
                        {
                            player.GetModPlayer<SpookyPlayer>().heartRate--;
                        }
                        if (player.GetModPlayer<SpookyPlayer>().heartRate < 60)
                        {
                            player.GetModPlayer<SpookyPlayer>().heartRate = 60;
                        }
                    }
                }
            }
            if (Main.MouseWorld.X > player.Center.X)
            {
                player.ChangeDir(1);
            }
            else
            {
                player.ChangeDir(-1);
            }
            // Stalkers

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.type == ModContent.NPCType<Stalker>())
                {
                    if (npc.active)
                    {
                        stalkerConditionMet = true;
                    }
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
            // I really need wolves howling in the snow
            if (ModContent.GetInstance<SpookyConfigClient>().toggleWind)
            {
                if (breezeTimer == 2650)
                {

                    if (Main.rand.NextFloat() < 0.35f)
                    {
                        Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/Breezes"));
                    }
                    breezeTimer = 0;
                }
            }
        }
        public override void PreUpdate()
        {
        }
        public bool isOnGrassyTile = false;
        public bool isOnWoodTile = false;
        public bool isOnStoneTile = false;
        public override void NaturalLifeRegen(ref float regen)
        {
            regen = 4 - (heartRate - 80) * 0.02f;
            float reverse = player.statLifeMax2 / 400f * 0.85f + 0.15f;
            regen /= reverse;
        }
        public override void PostUpdateRunSpeeds()
        {
            if (player.velocity.Y == 0)
            {
                player.maxRunSpeed = 1.5f * (heartRate / 80);
                player.accRunSpeed = 1.5f * (heartRate / 80);
            }
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
            Player.jumpSpeed *= 0.8f;
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
    public class CurrentTile : GlobalTile
    {
        public override bool Drop(int i, int j, int type)
        {
            return base.Drop(i, j, type);
        }
        public override bool KillSound(int i, int j, int type)
        {
            return base.KillSound(i, j, type);
        }
        public override void FloorVisuals(int type, Player player)
        {
            if (type == TileID.Grass || type == TileID.Dirt || type == TileID.BlueMoss || type == TileID.BrownMoss
                 || type == TileID.GreenMoss
                  || type == TileID.LavaMoss
                   || type == TileID.LongMoss
                    || type == TileID.PurpleMoss
                     || type == TileID.RedMoss
                      || type == TileID.Silt
                       || type == TileID.Slush
                        || type == TileID.JungleGrass
                         || type == TileID.Mud
                          || type == TileID.CorruptGrass
                           || type == TileID.FleshGrass
                            || type == TileID.HallowedGrass
                             || type == TileID.LivingMahoganyLeaves
                              || type == TileID.LeafBlock
                               || type == TileID.MushroomGrass
                               || type == TileID.ClayBlock
                               || type == TileID.Ash
                               || type == TileID.Sand
                               || type == TileID.Ebonsand
                               || type == TileID.Pearlsand
                               || type == TileID.Crimsand
                               || type == TileID.Cloud
                               || type == TileID.Asphalt
                               || type == TileID.RainCloud
                               || type == TileID.HoneyBlock
                               || type == TileID.CrispyHoneyBlock
                               || type == TileID.PumpkinBlock
                               || type == TileID.SnowBlock)
            {
                player.GetModPlayer<SpookyPlayer>().isOnGrassyTile = true;
            }
            else
            {
                player.GetModPlayer<SpookyPlayer>().isOnGrassyTile = false;
            }
            // TODO: Include ore bricks later, as well as the ore themselves
            if (type == TileID.Stone || type == TileID.StoneSlab || type == TileID.ActiveStoneBlock || type == TileID.GrayBrick
                 || type == TileID.IceBlock
                  || type == TileID.HallowedIce
                   || type == TileID.CorruptIce
                    || type == TileID.FleshIce
                     || type == TileID.PinkDungeonBrick
                      || type == TileID.GreenDungeonBrick
                       || type == TileID.BlueDungeonBrick
                        || type == TileID.Granite
                         || type == TileID.Marble
                          || type == TileID.MarbleBlock
                           || type == TileID.GraniteBlock
                            || type == TileID.Diamond
                             || type == TileID.DiamondGemspark
                              || type == TileID.DiamondGemsparkOff
                               || type == TileID.Ruby
                               || type == TileID.RubyGemspark
                               || type == TileID.RubyGemsparkOff
                               || type == TileID.Topaz
                               || type == TileID.TopazGemspark
                               || type == TileID.TopazGemsparkOff
                               || type == TileID.Sapphire
                               || type == TileID.SapphireGemspark
                               || type == TileID.SapphireGemsparkOff
                               || type == TileID.Amethyst
                               || type == TileID.AmethystGemspark
                               || type == TileID.AmethystGemsparkOff
                               || type == TileID.Emerald
                               || type == TileID.EmeraldGemspark
                               || type == TileID.EmeraldGemsparkOff
                               || type == TileID.AmberGemspark
                               || type == TileID.AmberGemsparkOff
                               || type == TileID.LihzahrdBrick
                               || type == TileID.Ebonstone
                               || type == TileID.EbonstoneBrick
                               || type == TileID.FleshBlock
                               || type == TileID.Crimstone
                               || type == TileID.CrimsonSandstone
                               || type == TileID.CorruptHardenedSand
                               || type == TileID.CorruptSandstone
                               || type == TileID.CrimsonHardenedSand
                               || type == TileID.HardenedSand
                               || type == TileID.Sandstone
                               || type == TileID.HallowSandstone
                               || type == TileID.SandstoneBrick
                               || type == TileID.SandStoneSlab
                               || type == TileID.HallowHardenedSand
                               || type == TileID.Sunplate
                               || type == TileID.Obsidian
                               || type == TileID.Pearlstone
                               || type == TileID.PearlstoneBrick
                               || type == TileID.Mudstone
                               || type == TileID.IridescentBrick
                               || type == TileID.CobaltBrick
                               || type == TileID.MythrilBrick
                               || type == TileID.MythrilAnvil
                               || type == TileID.Adamantite
                               || type == TileID.Mythril
                               || type == TileID.Cobalt
                               || type == TileID.Titanium
                               || type == TileID.Titanstone
                               || type == TileID.Palladium
                               || type == TileID.MetalBars
                               || type == TileID.LunarOre
                               || type == TileID.ObsidianBrick
                               || type == TileID.SnowBrick
                               || type == TileID.GreenCandyCaneBlock
                               || type == TileID.CandyCaneBlock
                               || type == TileID.GrayStucco
                               || type == TileID.GreenStucco
                               || type == TileID.RedStucco
                               || type == TileID.YellowStucco
                               || type == TileID.Copper
                               || type == TileID.CopperBrick
                               || type == TileID.Tin
                               || type == TileID.TinBrick
                               || type == TileID.Silver
                               || type == TileID.SilverBrick
                               || type == TileID.Tungsten
                               || type == TileID.TungstenBrick
                               || type == TileID.Iron
                               || type == TileID.Lead
                               || type == TileID.Gold
                               || type == TileID.GoldBrick
                               || type == TileID.Platinum
                               || type == TileID.PlatinumBrick
                               || type == TileID.Hellstone
                               || type == TileID.HellstoneBrick)

            {
                player.GetModPlayer<SpookyPlayer>().isOnStoneTile = true;
            }
            else
            {
                player.GetModPlayer<SpookyPlayer>().isOnStoneTile = false;
            }
            if (!player.GetModPlayer<SpookyPlayer>().isOnStoneTile && !player.GetModPlayer<SpookyPlayer>().isOnGrassyTile)
            {
                player.GetModPlayer<SpookyPlayer>().isOnWoodTile = true;
            }
            else
            {
                player.GetModPlayer<SpookyPlayer>().isOnWoodTile = false;
            }
        }
    }
}