using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.ModLoader;

namespace SpookyTerraria.Utilities
{
    public class TransparencyHelper
    {
        public const short Visible = 0;
        public const short SlightlyTransparent = 15;
        public const short SomewhatTransparent = 30;
        public const short DecentlyTransparent = 60;
        public const short SemiTransparent = 100;
        public const short MostlyTransparent = 125;
        public const short HighlyTransparent = 155;
        public const short SuperTransparent = 180;
        public const short MegaTransparent = 205;
        public const short NearlyInvisible = 230;
        public const short Invisible = 255;
    }
    public class SlenderMusicID
    {
        public const short TheEightPages = 0;
        public const short SlendersShadow = 1;
        public const short SeventhStreet = 2;
    }
    public class PageID
    {
        public const short Follows = 1;
        public const short CantRun = 2;
        public const short Trees = 3;
        public const short LeaveMeAlone = 4;
        public const short AlwaysWatchesNoEyes = 5;
        public const short HelpMe = 6;
        public const short DontLook = 7;
        public const short NoX12 = 8;
    }
    public class TimeCreator
    {
        public static int AddSeconds(int seconds)
        {
            return seconds * 60;
        }
        public static int AddMinutes(int minutes)
        {
            return minutes * 60 * 60;
        }
        public static int AddHours(int hours)
        {
            return hours * 60 * 60 * 60;
        }
    }
    public class AmbienceHelper
    {
        public static AmbienceHelper Instance = new AmbienceHelper();
        public AmbienceHelper() { ModContent.GetInstance<AmbienceHelper>(); }
        public SoundEffect soundCrickets;
        public SoundEffectInstance cricketsInstance;

        public SoundEffect soundBlizz;
        public SoundEffectInstance blizzInstance;

        public SoundEffect soundCaves;
        public SoundEffectInstance cavesInstance;

        public SoundEffect soundJungle;
        public SoundEffectInstance jungleInstance;

        public SoundEffect soundBeach;
        public SoundEffectInstance beachInstance;

        public SoundEffect soundDayAmbience;
        public SoundEffectInstance dayAmbienceInstance;

        public Mod Mod => ModContent.GetInstance<SpookyTerraria>();

        public int caveRumbleTimer;
        public int oceanWavesTimer;
        public int blizzTimer;
        public int cricketsTimer;
        public int jungleAmbTimer;
        public int dayAmbienceTimer;

        public void InitializeSoundInstances()
        {
            soundCrickets = Mod.GetSound(SoundEngine.cricketsSoundDir);
            soundBeach = Mod.GetSound(SoundEngine.wavesSoundDir);
            soundBlizz = Mod.GetSound(SoundEngine.blizzardSoundDir);
            soundJungle = Mod.GetSound(SoundEngine.jungleAmbienceDir);
            soundDayAmbience = Mod.GetSound(SoundEngine.dayAmbienceDir);
            soundCaves = Mod.GetSound(SoundEngine.rumblesSoundDir);
            cricketsInstance = soundCrickets.CreateInstance();
            cavesInstance = soundCaves.CreateInstance();
            beachInstance = soundBeach.CreateInstance();
            jungleInstance = soundJungle.CreateInstance();
            dayAmbienceInstance = soundDayAmbience.CreateInstance();
            blizzInstance = soundBlizz.CreateInstance();
        }
        public static void ReadLastBiome()
        {
            if (Main.GameUpdateCount % 2 == 0)
            {

            }
        }
        public void HandleAmbiences()
        {
            ForestAmbience(Main.LocalPlayer);
            UndergroundAmbience(Main.LocalPlayer);
            OceanAmbience(Main.LocalPlayer);
            JungleAmbience(Main.LocalPlayer);
            DayForestAmbience(Main.LocalPlayer);
            SnowAmbience(Main.LocalPlayer);
        }
        protected void ForestAmbience(Player player)
        {
            if (player.ZoneForest())
            {

            }
        }
        protected void UndergroundAmbience(Player player)
        {
            if (player.ZoneRockLayerHeight)
            {

            }
        }
        protected void OceanAmbience(Player player)
        {
            if (player.ZoneBeach)
            {

            }
        }
        protected void JungleAmbience(Player player)
        {
            if (player.ZoneBeach)
            {

            }
        }
        protected void DayForestAmbience(Player player)
        {
            if (player.ZoneForest() && Main.dayTime)
            {

            }
        }
        public void SnowAmbience(Player player)
        {
            if (player.ZoneSnow && !player.ZoneDirtLayerHeight && !player.ZoneRockLayerHeight)
            {
                blizzTimer++;
                Main.NewText(blizzTimer);
                if (blizzTimer == 1)
                {
                    /*blizzInstance.IsLooped = true;
                    blizzInstance.Play();*/
                }

                cricketsInstance?.Stop();
                beachInstance?.Stop();
                cavesInstance?.Stop();
                jungleInstance?.Stop();
                dayAmbienceInstance?.Stop();

                oceanWavesTimer = 0;
                caveRumbleTimer = 0;
                cricketsTimer = 0;
                jungleAmbTimer = 0;
                dayAmbienceTimer = 0;
            }
            if (blizzInstance == null)
            {
                Main.NewText("Is null");
            }
        }
        /*
            Player player = Main.player[Main.myPlayer];

            if (Main.worldName == SpookyTerrariaUtils.slenderWorldName)
            {
                oceanWavesTimer = 0;
                StopAmbientSound(wavesSoundDir);
            }

            caveRumbleTimer++;
            cricketsTimer++;
            blizzTimer++;
            oceanWavesTimer++;
            jungleAmbTimer++;
            dayAmbienceTimer++;

            if (player.ZoneSkyHeight || player.ZoneUnderworldHeight)
            {
                StopAmbientSound(jungleAmbienceDir);
                StopAmbientSound(rumblesSoundDir);
                StopAmbientSound(blizzardSoundDir);
                StopAmbientSound(wavesSoundDir);
                StopAmbientSound(dayAmbienceDir);
                StopAmbientSound(jungleAmbienceDir);
                StopAmbientSound(cricketsSoundDir);
                cricketsTimer = 0;
                caveRumbleTimer = 0;
                oceanWavesTimer = 0;
                blizzTimer = 0;
                jungleAmbTimer = 0;
                oceanWavesTimer = 0;
                jungleAmbTimer = 0;
                dayAmbienceTimer = 0;
            }
            if (Main.gameMenu)
            {
                StopAllAmbientSounds();
            }
            if (player.ZoneCorrupt || player.ZoneCrimson)
            {
                StopAmbientSound(jungleAmbienceDir);
                StopAmbientSound(rumblesSoundDir);
                StopAmbientSound(blizzardSoundDir);
                StopAmbientSound(wavesSoundDir);
                oceanWavesTimer = 0;
                blizzTimer = 0;
                jungleAmbTimer = 0;
                oceanWavesTimer = 0;
                if (Main.dayTime)
                {
                    StopAmbientSound(cricketsSoundDir);
                    cricketsTimer = 0;
                }
                else
                {
                    StopAmbientSound(dayAmbienceDir);
                    dayAmbienceTimer = 0;
                }
                if (!player.ZoneDirtLayerHeight && !player.ZoneRockLayerHeight)
                {
                    StopAmbientSound(wavesSoundDir);
                    caveRumbleTimer = 0;
                }
            }
            if (player.ZoneDesert && !player.ZoneBeach)
            {
                StopAmbientSound(cricketsSoundDir);
                StopAmbientSound(jungleAmbienceDir);
                StopAmbientSound(rumblesSoundDir);
                StopAmbientSound(blizzardSoundDir);
                StopAmbientSound(wavesSoundDir);
                oceanWavesTimer = 0;
                blizzTimer = 0;
                jungleAmbTimer = 0;
                caveRumbleTimer = 0;
                if (Main.dayTime)
                {
                    cricketsTimer = 0;
                }
                else
                {
                    dayAmbienceTimer = 0;
                }
            }
            if (player.ZoneJungle && (player.ZoneOverworldHeight || player.ZoneDirtLayerHeight))
            {
                StopAmbientSound(cricketsSoundDir);
                StopAmbientSound(rumblesSoundDir);
                StopAmbientSound(blizzardSoundDir);
                StopAmbientSound(wavesSoundDir);
                caveRumbleTimer = 0;
                oceanWavesTimer = 0;
                cricketsTimer = 0;
                blizzTimer = 0;
                dayAmbienceTimer = 0;
            }
            else if (player.ZoneJungle && player.ZoneRockLayerHeight)
            {
                StopAmbientSound(rumblesSoundDir);
                StopAmbientSound(blizzardSoundDir);
                StopAmbientSound(wavesSoundDir);
                StopAmbientSound(dayAmbienceDir);
                StopAmbientSound(cricketsSoundDir);
                oceanWavesTimer = 0;
                cricketsTimer = 0;
                blizzTimer = 0;
                dayAmbienceTimer = 0;
                jungleAmbTimer = 0;
            }
            if (player.ZoneRockLayerHeight) // Rock Layer
            {
                StopAmbientSound(cricketsSoundDir);
                StopAmbientSound(blizzardSoundDir);
                StopAmbientSound(wavesSoundDir);
                StopAmbientSound(breezeSoundDir);
                StopAmbientSound(jungleAmbienceDir);
                StopAmbientSound(dayAmbienceDir);
                // ModifyAmbientAttribute(breezeSoundDir, 0.4f, 1f, 0.9f);
                oceanWavesTimer = 0;
                cricketsTimer = 0;
                blizzTimer = 0;
                dayAmbienceTimer = 0;
                jungleAmbTimer = 0;
            }
            if (player.ZoneDirtLayerHeight)
            {
                StopAmbientSound(wavesSoundDir);
                StopAmbientSound(rumblesSoundDir);
                oceanWavesTimer = 0;
                caveRumbleTimer = 0;
            }
            string unwantedWorldName = "Slender: The 8 Pages";
            if (player.ZoneBeach && !player.ZoneDesert && !player.ZoneDirtLayerHeight && !player.ZoneRockLayerHeight) // Beach
            {
                if (Main.worldName != unwantedWorldName || Main.worldName != unwantedWorldName.ToUpper() || Main.worldName != unwantedWorldName.ToLower())
                {
                    StopAmbientSound(cricketsSoundDir);
                    cricketsTimer = 0;
                }
                StopAmbientSound(blizzardSoundDir);
                StopAmbientSound(rumblesSoundDir);
                StopAmbientSound(jungleAmbienceDir);
                StopAmbientSound(dayAmbienceDir);
                // ModifyAmbientAttribute(breezeSoundDir, 0.4f, 1f, 0.9f);
                caveRumbleTimer = 0;
                blizzTimer = 0;
                jungleAmbTimer = 0;
                dayAmbienceTimer = 0;
                // Checked
            }
            if (player.ZoneSnow && !player.ZoneDirtLayerHeight && !player.ZoneRockLayerHeight) // Snow
            {
                StopAmbientSound(cricketsSoundDir);
                StopAmbientSound(wavesSoundDir);
                StopAmbientSound(rumblesSoundDir);
                StopAmbientSound(jungleAmbienceDir);
                StopAmbientSound(dayAmbienceDir);
                ModifyAmbientAttribute(0.4f, 0.2f, 0.98f);
                oceanWavesTimer = 0;
                caveRumbleTimer = 0;
                cricketsTimer = 0;
                jungleAmbTimer = 0;
                dayAmbienceTimer = 0;
                // checked
            }
            if (player.ZoneForest() && !Main.dayTime && !player.ZoneRockLayerHeight) // Forest
            {
                StopAmbientSound(blizzardSoundDir);
                StopAmbientSound(wavesSoundDir);
                StopAmbientSound(rumblesSoundDir);
                StopAmbientSound(dayAmbienceDir);
                StopAmbientSound(jungleAmbienceDir);
                oceanWavesTimer = 0;
                caveRumbleTimer = 0;
                blizzTimer = 0;
                jungleAmbTimer = 0;
                dayAmbienceTimer = 0;
            }
            if (player.ZoneForest() && Main.dayTime && !player.ZoneRockLayerHeight) // Forest
            {
                StopAmbientSound(blizzardSoundDir);
                StopAmbientSound(wavesSoundDir);
                StopAmbientSound(rumblesSoundDir);
                StopAmbientSound(cricketsSoundDir);
                StopAmbientSound(jungleAmbienceDir);
                oceanWavesTimer = 0;
                caveRumbleTimer = 0;
                blizzTimer = 0;
                jungleAmbTimer = 0;
                cricketsTimer = 0;
            }
         */
    }
}