using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using SpookyTerraria.ModIntances;
using SpookyTerraria.NPCs;
using SpookyTerraria.OtherItems;
using System.Collections.Generic;
using System.Linq;
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
        public bool beatGame;
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) // Vanilla: Info Accessories Bar
        {
            int miniMapIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Map / Minimap"));
            if (miniMapIndex > -1)
            {
                layers.RemoveAt(miniMapIndex);
            }
            Player player = Main.player[Main.myPlayer];
            for (int textIndex = 0; textIndex < 1; textIndex++)
            {
                int xOffset = 0;
                int yOffset = 0;
                if (textIndex == 0)
                {
                    xOffset = -2;
                }
                if (textIndex == 1)
                {
                    xOffset = 2;
                }
                if (textIndex == 2)
                {
                    yOffset = -2;
                }
                if (textIndex == 3)
                {
                    yOffset = 2;
                }
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
                                    new Vector2(Main.screenWidth + xOffset - 150f,  yOffset + 5f),
                                    player.GetModPlayer<SpookyPlayer>().heartRate < 100 ? new Color(93, 199, 90) : Color.IndianRed);
                            }
                            return true;
                        },
                        InterfaceScaleType.UI)
                    );
                }
            }
        }
        public SpookyTerraria() { ModContent.GetInstance<SpookyTerraria>(); }
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
        public override void Load()
        {
            // Shader initialization

            if (!Main.dedServ)
            {
                Ref<Effect> Darkness = new Ref<Effect>(GetEffect("Effects/Darkness"));
                Filters.Scene["Darkness"] = new Filter(new ScreenShaderData(Darkness, "Darkness"), EffectPriority.VeryHigh);
            }


            /*On.Terraria.UI.ItemSlot.SellOrTrash += NoSellForPapers;
            On.Terraria.Chest.UsingChest += PagesCannotBePutInChests;*/
            if (!Main.dedServ)
            {
                Filters.Scene["SpookyTerraria:BlackSky"] = new Filter(new ScreenShaderData("FilterTower").UseColor(0.0f, 0.0f, 0.0f).UseOpacity(0f), EffectPriority.Medium);
                SkyManager.Instance["SpookyTerraria:BlackSky"] = new BlackSky();
            }
        }

        /*private void NoSellForPapers(On.Terraria.UI.ItemSlot.orig_SellOrTrash orig, Item[] inv, int context, int slot)
        {
            // Do not allow selling or trashing papers...
        }*/

        /*private int PagesCannotBePutInChests(On.Terraria.Chest.orig_UsingChest orig, int i)
        {
            int j;
            for (j = 0; j < Main.maxInventory; j++)
            {
                return 0;
            }
            return orig(i);
        }*/
        public int oldManTimer;
        public override void PostUpdateEverything()
        {

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
            Main.mapEnabled = false;
            Main.mapStyle = 1;
            player.controlMap = false;
            player.mapFullScreen = false;
            player.releaseMapFullscreen = false;
            player.releaseMapStyle = false;
            player.mapAlphaDown = false;
            player.mapAlphaUp = false;
            player.mapStyle = false;
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
            Player player = Main.player[Main.myPlayer];
            bool pageReqMetTier1 = player.CountItem(ModContent.ItemType<Paper>()) >= 75 && player.CountItem(ModContent.ItemType<Paper>()) < 150;
            bool pageReqMetTier2 = player.CountItem(ModContent.ItemType<Paper>()) >= 150 && player.CountItem(ModContent.ItemType<Paper>()) < 200;
            bool pageReqMetTier3 = player.CountItem(ModContent.ItemType<Paper>()) >= 200 && player.CountItem(ModContent.ItemType<Paper>()) < 250;
            bool pageReqMetTier4 = player.CountItem(ModContent.ItemType<Paper>()) >= 250 && player.CountItem(ModContent.ItemType<Paper>()) < 300;
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