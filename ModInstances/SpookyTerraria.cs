using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using SpookyTerraria.ModIntances;
using System.Linq;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Audio;

namespace SpookyTerraria
{
    public class SpookyTerraria : Mod
    {
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
            if (!Main.dedServ)
            {
                var x = Filters.Scene;
                Filters.Scene["SpookyTerraria:BlackSky"] = new Filter(new ScreenShaderData("FilterTower").UseColor(0.0f, 0.0f, 0.0f).UseOpacity(0f), EffectPriority.Medium);
                SkyManager.Instance["SpookyTerraria:BlackSky"] = new BlackSky();
            }
        }
        public int oldManTimer;
        public override void PostUpdateEverything()
        {
            Player myPlayer = Main.player[Main.myPlayer];
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
                    Main.NewText("The Old Man has returned.", Microsoft.Xna.Framework.Color.DarkGoldenrod);
                }
            }
            Main.slimeRain = false;
            Main.invasionSize = 0;
            Main.invasionProgress = 0;
            Main.dayTime = false;
            Main.time = 1800;
        }
        public override void ModifyLightingBrightness(ref float scale)
        {
            Player player = Main.player[Main.myPlayer];
            if (player.ZoneRockLayerHeight)
            {
                scale = ModContent.GetInstance<SpookyConfigServer>().lightingScale * 0.8f;
            }
            if (player.ZoneOverworldHeight)
            {
                scale = ModContent.GetInstance<SpookyConfigServer>().lightingScale;
            }
        }
        public override void ModifySunLightColor(ref Color tileColor, ref Color backgroundColor)
        {
            backgroundColor = Color.Black;
        }
        public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
            Player player = Main.player[Main.myPlayer];
            if (PlayerIsInForest(player) && !player.ZoneSkyHeight)
            {
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/ForestAmbience");
                priority = MusicPriority.BossHigh;
            }
            if (player.ZoneBeach && !player.ZoneSkyHeight)
            {
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/OceanAmbience");
                priority = MusicPriority.BossHigh;
            }
            if (player.ZoneSkyHeight)
            {
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Nothingness");
                priority = MusicPriority.BossHigh;
            }
            if (PlayerUnderground(player))
            {
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/CaveRumble");
                priority = MusicPriority.BossHigh;
            }
            if (player.ZoneOverworldHeight && player.ZoneSnow)
            {
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/SnowAmbience");
                priority = MusicPriority.BossHigh;
            }
            if (!PlayerUnderground(player) && (player.ZoneDirtLayerHeight || player.ZoneUndergroundDesert || player.ZoneUnderworldHeight || player.ZoneSkyHeight || player.ZoneCorrupt || player.ZoneCrimson || player.ZoneHoly || player.ZoneJungle || (player.ZoneDesert && !player.ZoneBeach) || player.ZoneDungeon || player.ZoneGlowshroom || player.ZoneMeteor || player.ZoneRain || player.ZoneOldOneArmy || player.ZoneSandstorm))
            {
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Nothingness");
                priority = MusicPriority.BossHigh;
            }
        }
    }
}