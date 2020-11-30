using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using SpookyTerraria.ModIntances;
using SpookyTerraria.NPCs;
using SpookyTerraria.OtherItems;
using SpookyTerraria.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Audio;
using Terraria.UI;

namespace SpookyTerraria
{
    public class SpookyTerraria : Mod
    {
        private bool stopTitleMusic;
        private ManualResetEvent titleMusicStopped;
        private int customTitleMusicSlot;
        public SpookyTerraria() { ModContent.GetInstance<SpookyTerraria>(); }
        private void ChangeMenuMusic(ILContext il)
        {
            ILCursor ilcursor = new ILCursor(il);
            ILCursor ilcursor2 = ilcursor;
            MoveType moveType = MoveType.After;
            Func<Instruction, bool>[] array = new Func<Instruction, bool>[1];
            array[0] = (Instruction i) => ILPatternMatchingExt.MatchLdfld<Main>(i, "newMusic");
            ilcursor2.GotoNext(moveType, array);
            ilcursor.EmitDelegate<Func<int, int>>(delegate (int newMusic)
            {
                if (newMusic != 6)
                {
                    return newMusic;
                }
                return customTitleMusicSlot;
            });
        }
        private void MenuMusicSetSpooky()
        {
            customTitleMusicSlot = GetSoundSlot(SoundType.Music, "Sounds/Music/MainMenu/Spooky");
            IL.Terraria.Main.UpdateAudio += new ILContext.Manipulator(ChangeMenuMusic);
        }
        private void MenuMusicSetSpookyRemix()
        {
            customTitleMusicSlot = GetSoundSlot(SoundType.Music, "Sounds/Music/MainMenu/SpookRemix");
            IL.Terraria.Main.UpdateAudio += new ILContext.Manipulator(ChangeMenuMusic);
        }
        public bool beatGame;
        public static ModHotKey Sprint;
        public override void PostSetupContent()
        {
            if (ModLoader.GetMod("TerrariaOverhaul") == null && ModLoader.GetMod("ShovelKnightMusic") == null)
            {
                if (Main.rand.Next(1, 3) == 1)
                {
                    MenuMusicSetSpooky();
                }
                else
                {
                    MenuMusicSetSpookyRemix();
                }
            }
            SpookyConfigClient recievedInstance = ModContent.GetInstance<SpookyConfigClient>();
            if (recievedInstance.betterUI)
            {
                SpookyTerrariaUtils.ModifyUITextures();
            }
        }
        public override void Load()
        {
            Mod spooky = ModLoader.GetMod("SpookyTerraria");
            Main.versionNumber = $"Terraria {Main.versionNumber}\n{spooky.Name} v{spooky.Version}";

            Sprint = RegisterHotKey("Sprint", "LeftShift");

            // Shader initialization

            if (!Main.dedServ)
            {
                Ref<Effect> Darkness = new Ref<Effect>(GetEffect("Effects/Darkness"));
                Filters.Scene["Darkness"] = new Filter(new ScreenShaderData(Darkness, "Darkness"), EffectPriority.VeryHigh);
            }
            if (!Main.dedServ)
            {
                Filters.Scene["SpookyTerraria:BlackSky"] = new Filter(new ScreenShaderData("FilterTower").UseColor(0.0f, 0.0f, 0.0f).UseOpacity(0f), EffectPriority.Medium);
                SkyManager.Instance["SpookyTerraria:BlackSky"] = new BlackSky();
            }
        }
        public override void Unload()
        {
            ManualResetEvent manualResetEvent = titleMusicStopped;
            if (manualResetEvent != null)
            {
                manualResetEvent.Set();
            }
            titleMusicStopped = null;
            // Wtf...
            Main.versionNumber = $"v1.3.5.3";
            SpookyTerrariaUtils.ReturnTexturesToDefaults();

            // Main.inventoryTickOffTexture = GetTexture("Terraria/Images/X");
            // Main.inventoryTickOffTexture = GetTexture("Terraria/Images/X");
            Main.chatBackTexture = Main.instance.OurLoad<Texture2D>("Images" + Path.DirectorySeparatorChar + "Chat_Back");
            Main.inventoryBackTexture = Main.instance.OurLoad<Texture2D>("Images" + Path.DirectorySeparatorChar + "Inventory_Back");
            Sprint = null;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(this);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 5);
            recipe.AddIngredient(ItemID.Torch);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(ItemID.CarriageLantern);
            recipe.AddRecipe();
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) // Vanilla: Info Accessories Bar
        {
            int miniMapIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Map / Minimap"));
            if (miniMapIndex > -1)
            {
                if (ModLoader.GetMod("SpookyTerraria") != null)
                {
                    layers.RemoveAt(miniMapIndex);
                }
            }
            Player player = Main.player[Main.myPlayer];
            string heartRateText = $"BPM: {player.GetModPlayer<SpookyPlayer>().heartRate}";
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Info Accessories Bar"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "SpookyTerraria: BPM",
                    delegate
                    {
                        if (player.GetModPlayer<SpookyPlayer>().accHeartMonitor)
                        {
                            Utils.DrawBorderString(Main.spriteBatch,
                                heartRateText,
                                new Vector2(Main.screenWidth - 375f, 5f),
                                player.GetModPlayer<SpookyPlayer>().heartRate < 100 ? new Color(93, 199, 90) : Color.IndianRed);
                        }
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
            string staminaPercent = $"Stamina: {player.GetModPlayer<StaminaPlayer>().Stamina}%";
            int index = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Info Accessories Bar"));
            if (index != -1)
            {
                layers.Insert(index, new LegacyGameInterfaceLayer(
                    "SpookyTerraria: Stamina",
                    delegate
                    {
                        Utils.DrawBorderString(Main.spriteBatch,
                        staminaPercent,
                        new Vector2(Main.screenWidth - 415f, 30f),
                        Color.Crimson);
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
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
            return !player.ZoneOverworldHeight
                && !player.ZoneDirtLayerHeight
                && player.ZoneRockLayerHeight;
        }
        public override void PreSaveAndQuit()
        {
            Player player = Main.player[Main.myPlayer];
            Main.mapEnabled = true;
            Main.mapStyle = 3;
            player.controlMap = true;
            player.mapFullScreen = false;
            player.releaseMapFullscreen = true;
            player.releaseMapStyle = true;
            player.mapAlphaDown = true;
            player.mapAlphaUp = true;
            player.mapStyle = true;
            int soundSlot = GetSoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/Breezes");
            if (Utils.IndexInRange(Main.music, soundSlot))
            {
                Music music = Main.music[soundSlot];
                if (music != null && music.IsPlaying)
                {
                    Main.music[soundSlot].Stop(AudioStopOptions.Immediate);
                }
            }
            base.PreSaveAndQuit();
        }
        public override void Close()
        {
            int soundSlot2 = GetSoundSlot((SoundType)51, "Sounds/Music/MainMenu/SpookRemix");
            int soundSlot1 = GetSoundSlot((SoundType)51, "Sounds/Music/MainMenu/Spooky");
            if (Utils.IndexInRange(Main.music, soundSlot1))
            {
                Music musicIndex = Main.music[soundSlot1];
                if (musicIndex != null && musicIndex.IsPlaying)
                {
                    Main.music[soundSlot1].Stop(AudioStopOptions.Immediate);
                }
            }
            if (Utils.IndexInRange(Main.music, soundSlot2))
            {
                Music musicIndex = Main.music[soundSlot2];
                if (musicIndex != null && musicIndex.IsPlaying)
                {
                    Main.music[soundSlot2].Stop(AudioStopOptions.Immediate);
                }
            }
            SpookyTerrariaUtils.ReturnTexturesToDefaults();
            base.Close();
        }
        /// <summary>
        /// Timer until the old man respawns
        /// </summary>
        public int oldManTimer;
        public override void MidUpdateDustTime()
        {
            Main.soundInstanceMenuTick.Volume = 0f;
            Main.soundInstanceMenuOpen.Volume = 0f;
            Main.soundInstanceMenuClose.Volume = 0f;
        }
        public override void PostUpdateEverything()
        {
            Main.soundInstanceMenuTick.Volume = 0f;
            Main.soundInstanceMenuOpen.Volume = 0f;
            Main.soundInstanceMenuClose.Volume = 0f;
            Player player = Main.player[Main.myPlayer];
            if (Main.netMode != NetmodeID.Server)
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.type == ModContent.NPCType<Stalker>())
                    {
                        Filters.Scene["Darkness"].GetShader().UseIntensity(player.Distance(npc.Center) / 8);
                    }
                }
            }
            int oldManIndex = NPC.FindFirstNPC(NPCID.OldMan);
            if (oldManIndex > -1)
            {
                oldManTimer = 0;
            }
            if (oldManIndex < 0)
            {
                oldManTimer++;
                if (oldManTimer > 30000)
                {
                    NPC.NewNPC(Main.dungeonX * 16 + 8, Main.dungeonY * 16, NPCID.OldMan);
                    Main.NewText("The Old Man has returned.", Color.DarkGoldenrod);
                    oldManTimer = 0;
                }
            }
            Main.slimeRain = false;
            Main.invasionSize = 0;
            Main.invasionProgress = 0;
            if (!beatGame)
            {
                Main.dayTime = false;
                Main.time = 1800;
            }
            else
            {
                Main.dayTime = true;
                Main.time = 18000;
            }
        }
        public override void ModifyLightingBrightness(ref float scale)
        {
            Player player = Main.player[Main.myPlayer];
            if ((player.ZoneRockLayerHeight || player.ZoneDirtLayerHeight) && !ModContent.GetInstance<SpookyTerraria>().beatGame)
            {
                scale = ModContent.GetInstance<SpookyConfigServer>().lightingScale * 0.8f;
            }
            if (player.ZoneOverworldHeight && !ModContent.GetInstance<SpookyTerraria>().beatGame)
            {
                scale = ModContent.GetInstance<SpookyConfigServer>().lightingScale;
            }
        }
        public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
            if (stopTitleMusic || (!Main.gameMenu && customTitleMusicSlot != 6 && Main.ActivePlayerFileData != null && Main.ActiveWorldFileData != null))
            {
                music = 6;
                stopTitleMusic = true;
                customTitleMusicSlot = 6;
                Music music2 = GetMusic("Sounds/Music/MainMenu/Spooky");
                if (music2.IsPlaying)
                {
                    music2.Stop(AudioStopOptions.Immediate);
                }
                Music music1 = GetMusic("Sounds/Music/MainMenu/SpookRemix");
                if (music2.IsPlaying)
                {
                    music2.Stop(AudioStopOptions.Immediate);
                }
                if (music1.IsPlaying)
                {
                    music1.Stop(AudioStopOptions.Immediate);
                }
                titleMusicStopped?.Set();
                stopTitleMusic = false;
            }
            Player player = Main.player[Main.myPlayer];
            bool pageReqMetTier1 = player.CountItem(ModContent.ItemType<Paper>()) >= 1 && player.CountItem(ModContent.ItemType<Paper>()) < 3;
            bool pageReqMetTier2 = player.CountItem(ModContent.ItemType<Paper>()) >= 3 && player.CountItem(ModContent.ItemType<Paper>()) < 5;
            bool pageReqMetTier3 = player.CountItem(ModContent.ItemType<Paper>()) >= 5 && player.CountItem(ModContent.ItemType<Paper>()) < 7;
            bool pageReqMetTier4 = player.CountItem(ModContent.ItemType<Paper>()) >= 7 && player.CountItem(ModContent.ItemType<Paper>()) < 8;

            if (pageReqMetTier1)
            {
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Slender/MusicStage1");
                priority = MusicPriority.BossHigh;
            }
            if (pageReqMetTier2)
            {
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Slender/MusicStage2");
                priority = MusicPriority.BossHigh;
            }
            if (pageReqMetTier3)
            {
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Slender/MusicStage3");
                priority = MusicPriority.BossHigh;
            }
            if (pageReqMetTier4)
            {
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Slender/MusicStage4");
                priority = MusicPriority.BossHigh;
            }
            if ((PlayerIsInForest(player) || player.ZoneSnow || player.ZoneBeach || player.ZoneCorrupt || player.ZoneCrimson || PlayerUnderground(player) || player.ZoneDirtLayerHeight || player.ZoneUndergroundDesert || player.ZoneUnderworldHeight || player.ZoneSkyHeight || player.ZoneCorrupt || player.ZoneCrimson || player.ZoneHoly || player.ZoneJungle || (player.ZoneDesert && !player.ZoneBeach) || player.ZoneDungeon || player.ZoneGlowshroom || player.ZoneMeteor || player.ZoneRain || player.ZoneOldOneArmy || player.ZoneSandstorm) && !pageReqMetTier1 && !pageReqMetTier2 && !pageReqMetTier3 && !pageReqMetTier4)
            {
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Nothingness");
                priority = MusicPriority.BossMedium;
            }
        }
    }
}