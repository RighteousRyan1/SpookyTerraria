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

        public override void PostUpdateMiscEffects()
        {
            caveRumbleTimer++;
            cricketsTimer++;
            blizzTimer++;
            oceanWavesTimer++;
            if (player.ZoneRockLayerHeight && caveRumbleTimer == 1680)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/Biome/CaveRumble"));
                caveRumbleTimer = 0;
            }
            if (player.ZoneBeach && oceanWavesTimer == 9060)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/Biome/OceanAmbience"));
                oceanWavesTimer = 0;
            }
            if (player.ZoneBeach && !player.ZoneDirtLayerHeight && blizzTimer == 6060)
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

        /*
        [Label("Toggle Note Collecting")]
        [DefaultValue(false)]
        [Tooltip("The objective of the game normally is to collect 300 notes from the form of blocks. If this is on, then your objective is to beat the Moon Lord")]
        public bool normalProgression;
        */

        [Label("Modify Spawn Pool")]
        [DefaultValue(false)]
        [Tooltip("Removes all spawns besides Spooky Terraria NPCs. For when you want to feel isolated.")]
        public bool excludeAllNPCsBesideCreepyTerrariaOnesFromSpawnPool;
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
        }
    }
    public class SpookyNPCs : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public override bool CloneNewInstances => true;
        public int heartBeatTimer;

        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            Player player = Main.player[Main.myPlayer];
            if (ModContent.GetInstance<SpookyTerraria>().beatGame)
            {
                pool.Clear(); // No more enemy spawns!.. Because you beat the game, numnut
            }
            if (!ModContent.GetInstance<SpookyConfigServer>().excludeAllNPCsBesideCreepyTerrariaOnesFromSpawnPool)
            {
                return;
            }
            if (ModContent.GetInstance<SpookyConfigServer>().excludeAllNPCsBesideCreepyTerrariaOnesFromSpawnPool)
            {
                pool.Clear();

                foreach (var SpookyNPCs in Lists_SpookyNPCs.SpookyNPCs)
                {
                    pool.Add(SpookyNPCs, 1f);
                }
            }
        }
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
        public override void GetChat(NPC npc, ref string chat)
        {
            Player player = Main.player[Main.myPlayer];
            if (npc.type == NPCID.ArmsDealer)
            {
                switch (Main.rand.Next(5))
                {
                    case 1:
                        chat = "Why in the world is it so damn dark? Good thing I got guns with me.";
                        break;
                    case 2 when player.CountItem(ModContent.ItemType<Paper>()) >= 75:
                        chat = "What is this rumbling? I'm getting creeped out...";
                        break;
                    case 3:
                        chat = "Why did the music stop?";
                        break;
                    case 4:
                        chat = "Just buy my guns. That's all I gotta say.";
                        break;
                }
            }
            if (npc.type == NPCID.Dryad)
            {
                int armsDealerIndex = NPC.FindFirstNPC(NPCID.ArmsDealer);
                switch (Main.rand.Next(5))
                {
                    case 1:
                        chat = "The sounds of nature are calming, but the immense darkness frightens me...";
                        break;
                    case 2 when player.CountItem(ModContent.ItemType<Paper>()) >= 75:
                        chat = "Is this what goes bump in the night?";
                        break;
                    case 3:
                        chat = "Nature can sometimes get freaky.";
                        break;
                    case 4 when armsDealerIndex > -1:
                        chat = $"{Main.npc[armsDealerIndex].GivenName}? Pfft. What a clown. I hate him.";
                        break;
                }
            }
            if (npc.type == NPCID.Merchant)
            {
                switch (Main.rand.Next(5))
                {
                    case 1:
                        chat = "The sun is low! My prices are not.";
                        break;
                    case 2 when player.CountItem(ModContent.ItemType<Paper>()) >= 75:
                        chat = "What is this feeling? It's Like I'm being watched.";
                        break;
                    case 3:
                        chat = "My venilated armor is feeling quite hot right now!";
                        break;
                    case 4:
                        chat = $"Hey {player.name}, buy these arrows and kill whatever is lurking around here.";
                        break;
                }
            }
            if (npc.type == NPCID.Nurse)
            {
                switch (Main.rand.Next(5))
                {
                    case 1:
                        chat = "Yeah, yeah. Let me heal you. Pay up first.";
                        break;
                    case 2 when player.CountItem(ModContent.ItemType<Paper>()) >= 75:
                        chat = "Oh? Baby want his milk? Is the rumbling too scary-wary for you?";
                        break;
                    case 3:
                        chat = "Ugh.. What now?";
                        break;
                    case 4:
                        chat = "This darkness makes me feel at home.";
                        break;
                }
            }
            if (npc.type == NPCID.Guide)
            {
                switch (Main.rand.Next(5))
                {
                    case 1:
                        chat = "This darkness is not for the light hearted. For what I know, I have heard things that lurk around in the darkness here.";
                        break;
                    case 2 when player.CountItem(ModContent.ItemType<Paper>()) >= 75:
                        chat = "When you hear rumbling, you know that you are being watched.";
                        break;
                    case 3:
                        chat = "This ambience of crickets and wind blowing against the trees is calming, but the dark sky immediately revokes all of that.";
                        break;
                    case 4:
                        chat = $"What's up, {player.name}! I'm just sitting around thinking about how we might die.";
                        break;
                }
            }
            if (npc.type == NPCID.Demolitionist)
            {
                switch (Main.rand.Next(5))
                {
                    case 1:
                        chat = "It's kinda hard to see when it is this dark. Good thing the fuses on my explosives can change that!";
                        break;
                    case 2 when player.CountItem(ModContent.ItemType<Paper>()) >= 75:
                        chat = "I'm pretty sure this rumbling isn't from one of my babies.";
                        break;
                    case 3:
                        chat = "I gotta say, while caving I have never witnessed such darkness.";
                        break;
                    case 4:
                        chat = "Is it just me or do these trees look more slender to you?";
                        break;
                }
            }
            if (npc.type == NPCID.Wizard)
            {
                switch (Main.rand.Next(5))
                {
                    case 1:
                        chat = "Wanna die? No? Ok.";
                        break;
                    case 2 when player.CountItem(ModContent.ItemType<Paper>()) >= 75:
                        chat = "Want me to stop this rumbling? Yes? Too bad.";
                        break;
                    case 3:
                        chat = "My pure wizardry is too much for this world to handle.";
                        break;
                    case 4:
                        chat = "My magic ball can read when you all die!";
                        break;
                }
            }
            if (npc.type == NPCID.Mechanic)
            {
                switch (Main.rand.Next(5))
                {
                    case 1:
                        chat = "Why is all the wifi down?";
                        break;
                    case 2 when player.CountItem(ModContent.ItemType<Paper>()) >= 75:
                        chat = "Engineering gets more and more depressing every day, especially when you feel like rumbles will just tear it all down.";
                        break;
                    case 3:
                        chat = "Damn these crickets! They are so annoying when someone is trying to work!";
                        break;
                    case 4 when player.GetModPlayer<SpookyPlayer>().heartRate >= 100:
                        chat = $"Hurry it up {player.name}. I mean, I can already tell you are in a hurry because your heartrate is {player.GetModPlayer<SpookyPlayer>().heartRate}";
                        break;
                }
            }
            if (npc.type == NPCID.Stylist)
            {
                switch (Main.rand.Next(5))
                {
                    case 1:
                        chat = "These scissors can come handy in may ways...";
                        break;
                    case 2 when player.CountItem(ModContent.ItemType<Paper>()) >= 75:
                        chat = "This is a very uncertain feeling...";
                        break;
                    case 3:
                        chat = "You want a haircut? Are you sure you can trust me?";
                        break;
                    case 4:
                        chat = "I can see it in the headlines: 'Local Hairstylist Murders Patient'.";
                        break;
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
                    if (heartBeatTimer == 50 && npc.Distance(player.Center) >= 750f)
                    {
                        Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/HeartBeat"), npc.Center);
                        heartBeatTimer = 2;
                    }
                    if (heartBeatTimer == 40 && npc.Distance(player.Center) < 750f)
                    {
                        Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/HeartBeat"), npc.Center);
                        heartBeatTimer = 2;
                    }
                    if (heartBeatTimer == 30 && npc.Distance(player.Center) < 300f)
                    {
                        Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/HeartBeat"), npc.Center);
                        heartBeatTimer = 2;
                    }
                    if (heartBeatTimer == 25 && npc.Distance(player.Center) < 100f)
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
        public bool accHeartMonitor;

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
            item3.SetDefaults(ModContent.ItemType<HeartrateMonitor>(), false);
            items.Add(item1);
            items.Add(item2);
            items.Add(item3);
        }
        public override void OnEnterWorld(Player player)
        {
            Mod ST = ModLoader.GetMod("SpookyTerraria");
            Main.NewText($"Spooky Terraria is on Alpha version {ST.Version}.", Color.DarkGray);
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
        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
        }
        public override void ModifyDrawInfo(ref PlayerDrawInfo drawInfo)
        {
            if (!Main.gameMenu)
            {
                if (!Main.player[1].active)
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
            Main.NewText(hbIncTimer + ", " + hbDecTimer + ", " + stalkerConditionMet);
            for (int index = 0; index < Main.maxNPCs; index++)
            {
                NPC npc = Main.npc[index];
                if (npc.type == ModContent.NPCType<Stalker>() && npc.active)
                {
                    stalkerConditionMet = true;
                    hbDecTimer = 0;
                    hbIncTimer++;
                    float distnaceToStalker = player.Distance(npc.Center);
                    if (distnaceToStalker >= 750f && hbIncTimer == 50) // Slow heartrate increase
                    {
                        heartRate++;
                        hbIncTimer = 0;
                    }
                    if (distnaceToStalker < 750f && hbIncTimer == 25) // Semi-Fast heartrate increase
                    {
                        heartRate++;
                        hbIncTimer = 0;
                    }
                    if (distnaceToStalker < 300f && hbIncTimer == 12) // FAST INCREASE IN HEARTRATE!
                    {
                        heartRate++;
                        hbIncTimer = 0;
                    }
                    if (distnaceToStalker < 100f && hbIncTimer == 5) // SANIK!
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
                else if (npc.type == ModContent.NPCType<Stalker>() && !npc.active)
                {
                    stalkerConditionMet = false;
                    hbIncTimer = 0;
                    hbDecTimer++;
                    if (!player.ZoneBeach)
                    {
                        if (hbDecTimer == 20)
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
                        if (hbDecTimer == 15)
                        {
                            heartRate--;
                            hbDecTimer = 0;
                        }
                        if (heartRate > 60 && hbDecTimer == 15)
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
                if (breezeTimer == 2650)
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
                player.maxRunSpeed = 1.5f * (heartRate / 80f);
                player.accRunSpeed = 1.5f * (heartRate / 80f);
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