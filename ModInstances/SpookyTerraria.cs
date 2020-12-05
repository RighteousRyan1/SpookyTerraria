using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using SpookyTerraria.ModIntances;
using SpookyTerraria.NPCs;
using SpookyTerraria.OtherItems;
using SpookyTerraria.Tiles;
using SpookyTerraria.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using Terraria;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Audio;
using Terraria.UI;
using ReLogic.Graphics;
using System.Net.NetworkInformation;
using Terraria.IO;
using Terraria.Graphics.Capture;
using System.Reflection;

namespace SpookyTerraria
{
    public class SlenderMapDownloadMF : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "slenderMapMF";

        public override string Usage
            => "/slenderMapMF";

        public override string Description
            => "download the official terraria slender map, made by Ryan (RighteousRyan) from Mediafire. Download all files from all pages.";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            Main.NewText("Opened a link to the map! From there, click the 'Download' button.", Color.Green);
            Process.Start("http://www.mediafire.com/file/yzdqeips85ppk1a/Slender_The_8_Pages.twld/file");
            Process.Start("http://www.mediafire.com/file/1hq8vc6h55hsqjw/Slender_The_8_Pages.twld.bak/file");
            Process.Start("http://www.mediafire.com/file/y98ss4cb2vgk4vj/Slender_The_8_Pages.wld/file");
        }
    }
    public class SlenderMapDownloadDiscord : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "slenderMapDiscord";

        public override string Usage
            => "/slenderMapDiscord";

        public override string Description
            => "download the official terraria slender map, made by Ryan (RighteousRyan) from discord. Download all files.";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            Main.NewText("Opened discord.", Color.MediumPurple);
            Process.Start("https://discord.gg/pT2BzSG");
        }
    }
    public class ModifyPagesCount_Debug : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "pages";

        public override string Usage
            => "pages <count>";

        public override string Description
            => "set page count to a number (DEBUG)";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            Player player = Main.player[Main.myPlayer];
            if (!ModContent.GetInstance<SpookyConfigServer>().debugMode)
            {
                Main.NewText($"Not enough permissions.", Color.Red);
                return;
            }
            else
            {
                if (args.Length == 0)
                {
                    Main.NewText($"Expected Argument: <count>", Color.Red);
                    return;
                }
                var pageCount = args[0];
                if (int.TryParse(args[0], out int type1))
                {
                    if (type1 > 0)
                    {
                        if (int.Parse(args[0]) < 0 || int.Parse(args[0]) > 8)
                        {
                            Main.NewText("<count> was an invalid operation, please provide a valid integer.", Color.OrangeRed);
                            return;
                        }
                    }
                }

                if (!int.TryParse(args[0], out int type2))
                {
                    if (type2 == 0)
                    {
                        Main.NewText($"{pageCount} is not a valid argument. (Did you provide an integer?)", Color.OrangeRed);
                        return;
                    }
                }
                SpookyPlayer.pages = type2;
                CombatText.NewText(player.getRect(), Color.White, $"Pages was set to {type2}.");
            }
        }
    }
    public class PlacePage : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "placePage";

        public override string Usage
            => "placePage <type>";

        public override string Description
            => "Place a page [Left, Right, Invis] (DEBUG)";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            Player player = Main.player[Main.myPlayer];
            if (!ModContent.GetInstance<SpookyConfigServer>().debugMode)
            {
                Main.NewText($"Not enough permissions.", Color.Red);
                return;
            }
            else
            {
                if (args.Length == 0)
                {
                    Main.NewText($"Expected Argument: <type>", Color.Red);
                    return;
                }
                var pageCount = args[0];
                if (int.TryParse(args[0], out int type1))
                {
                    if (type1 > 0)
                    {
                        if (int.Parse(args[0]) < 1 || int.Parse(args[0]) > 4)
                        {
                            Main.NewText("<type> was an invalid operation, please provide a valid integer.", Color.OrangeRed);
                            return;
                        }
                    }
                }
                if (!int.TryParse(args[0], out int type2))
                {
                    if (type2 == 0)
                    {
                        Main.NewText($"{pageCount} is not a valid argument. (Did you provide an integer?)", Color.OrangeRed);
                        return;
                    }
                }
                if (type2 == 4)
                {
                    WorldGen.PlaceTile((int)Main.MouseWorld.X / 16, (int)Main.MouseWorld.Y / 16, ModContent.TileType<PageTileWall>());
                    CombatText.NewText(player.getRect(), Color.White, $"Placed PageTileWall");
                }
                if (type2 == 3)
                {
                    WorldGen.PlaceTile((int)Main.MouseWorld.X / 16, (int)Main.MouseWorld.Y / 16, ModContent.TileType<InvisTile>());
                    CombatText.NewText(player.getRect(), Color.White, $"Placed InvisTile!");
                }
                if (type2 == 2)
                {
                    WorldGen.PlaceTile((int)Main.MouseWorld.X / 16, (int)Main.MouseWorld.Y / 16, ModContent.TileType<PageTileRight>());
                    CombatText.NewText(player.getRect(), Color.White, $"Placed PageTileRight!");
                }
                if (type2 == 1)
                {
                    WorldGen.PlaceTile((int)Main.MouseWorld.X / 16, (int)Main.MouseWorld.Y / 16, ModContent.TileType<PageTileLeft>());
                    CombatText.NewText(player.getRect(), Color.White, $"Placed PageTileLeft!");
                }
            }
        }
    }
    public class SpookyTerraria : Mod
    {
        public override void ModifyTransformMatrix(ref SpriteViewMatrix Transform)
        {
            // 43 + 42 = 85 (width of bathrooms)
            // 65 + (57 + 74) = 196 (width of tunnel)
            Player player = Main.player[Main.myPlayer];
            // Main.tile[2091, 367] /* TopLeft of Bathrooms */
            // Main.tile[640,298] /* TopLeft of Tunnel */
            if (Main.worldName == SpookyTerrariaUtils.slenderWorldName)
            {
                Rectangle insideTunnel = new Rectangle(640 * SpookyTerrariaUtils.tileScaling, 298 * SpookyTerrariaUtils.tileScaling, 138 * SpookyTerrariaUtils.tileScaling, 8 * SpookyTerrariaUtils.tileScaling);
                if (player.Hitbox.Intersects(insideTunnel))
                {
                    Transform.Zoom = new Vector2(updateGameZoomTargetValue + 7f);
                }
                Rectangle insideBathrooms = new Rectangle(2091 * SpookyTerrariaUtils.tileScaling, 367 * SpookyTerrariaUtils.tileScaling, 85 * SpookyTerrariaUtils.tileScaling, 26 * SpookyTerrariaUtils.tileScaling);
                if (player.Hitbox.Intersects(insideBathrooms))
                {
                    Transform.Zoom = new Vector2(updateGameZoomTargetValue + 8.75f);
                }
            }
        }
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
        public void MenuMusicSet_Slender()
        {
            customTitleMusicSlot = GetSoundSlot(SoundType.Music, "Sounds/Music/MainMenu/Slender_MainMenu");
            IL.Terraria.Main.UpdateAudio += new ILContext.Manipulator(ChangeMenuMusic);
        }
        /*private void MenuMusicSet_Spooky()
        {
            customTitleMusicSlot = GetSoundSlot(SoundType.Music, "Sounds/Music/MainMenu/Spooky");
            IL.Terraria.Main.UpdateAudio += new ILContext.Manipulator(ChangeMenuMusic);
        }*/
        /*private void MenuMusicSet_SpookyRemix()
        {
            customTitleMusicSlot = GetSoundSlot(SoundType.Music, "Sounds/Music/MainMenu/SpookRemix");
            IL.Terraria.Main.UpdateAudio += new ILContext.Manipulator(ChangeMenuMusic);
        }*/
        public bool beatGame;
        public static ModHotKey Sprint;
        public override void PostSetupContent()
        {
            if (ModLoader.GetMod("TerrariaOverhaul") == null)
            {
                MenuMusicSet_Slender();
            }
            SpookyConfigClient recievedInstance = ModContent.GetInstance<SpookyConfigClient>();
            if (recievedInstance.betterUI)
            {
                SpookyTerrariaUtils.ModifyUITextures();
            }
        }
        public override void Load()
        {
            Main.soundMenuTick = GetSound("Sounds/Custom/Other/Nothingness");
            Main.soundMenuOpen = GetSound("Sounds/Custom/Other/Nothingness");
            Main.soundMenuClose = GetSound("Sounds/Custom/Other/Nothingness");
            Mod spooky = ModLoader.GetMod("SpookyTerraria");
            Main.versionNumber = $"Terraria {Main.versionNumber}\n{spooky.Name} v{spooky.Version}";

            Sprint = RegisterHotKey("Sprint", "LeftShift");

            IL.Terraria.Main.DrawMenu += NewDrawMenu;
            On.Terraria.Main.DrawInterface_35_YouDied += Main_DrawInterface_35_YouDied;
            // Shader initialization

            if (!Main.dedServ)
            {
                // Shaders
                Ref<Effect> Darkness = new Ref<Effect>(GetEffect("Effects/Darkness"));
                Filters.Scene["Darkness"] = new Filter(new ScreenShaderData(Darkness, "Darkness"), EffectPriority.VeryHigh);
                // Skies
                Filters.Scene["SpookyTerraria:BlackSky"] = new Filter(new ScreenShaderData("FilterTower").UseColor(0.0f, 0.0f, 0.0f).UseOpacity(0f), EffectPriority.Medium);
                SkyManager.Instance["SpookyTerraria:BlackSky"] = new BlackSky();
            }
        }

        private void NewDrawMenu(ILContext il)
        {
            ILCursor c = new ILCursor(il).Goto(0, 0, false);
            ILCursor ilcursor = c;
            Func<Instruction, bool>[] array = new Func<Instruction, bool>[1];
            array[0] = ((Instruction i) => ILPatternMatchingExt.MatchLdsfld(i, typeof(Main).GetField("dayTime")));
            bool flag = !ilcursor.TryGotoNext(array);
            if (flag)
            {
                Logger.Info("Failed to change main menu.");
            }
            else
            {
                c.Emit(OpCodes.Call, typeof(SpookyTerraria).GetMethod("DrawMenuNew", BindingFlags.NonPublic | BindingFlags.Static));
            }
        }
        private static void DrawMenuNew()
        {
            Texture2D frame = ModContent.GetTexture("SpookyTerraria/Assets/Slender8Pages");
            Rectangle originalFrameRect = Utils.Frame(frame, 1, 1, 0, 0);
            Rectangle targetRect = originalFrameRect;
            bool flag3 = Main.screenWidth > Main.screenHeight;
            if (flag3)
            {
                targetRect.Height = (int)((float)targetRect.Height * Main.screenWidth / (float)targetRect.Width);
                targetRect.Width = Main.screenWidth;
            }
            else
            {
                targetRect.Width = (int)(targetRect.Width * (Main.screenHeight / targetRect.Height));
                targetRect.Height = Main.screenHeight;
            }
        }
        private void Main_DrawInterface_35_YouDied(On.Terraria.Main.orig_DrawInterface_35_YouDied orig)
        {
        }
        /*internal static void HookMenuSplash(ILContext il)
        {
            var c = new ILCursor(il).Goto(0);
            if (!c.TryGotoNext(i => i.MatchLdsfld(typeof(Main).GetField("dayTime"))))
                return;
            c.Emit(OpCodes.Call, typeof(SpookyTerraria).GetMethod("Draw_GetWorldFileText"));
        }
        public static void Draw_GetWorldFileText()
        {
            Vector2 origin2;
            origin2.X = 0.5f;
            origin2.Y = 0.5f;
            Rectangle IClickable = new Rectangle(Main.screenHeight - 25, Main.screenWidth - (Main.screenWidth + 100), 1000, 1000);
            bool isHovering = IClickable.Contains(Main.MouseScreen.ToPoint());
            Main.spriteBatch.DrawString(Main.fontMouseText, "fdsafdiuf duf gadsu fads bfdsf d bfhs bfdgfb yafdsf fbgasduf uf bdasydfbsuf", new Vector2(Main.screenWidth - origin2.X - 390f, origin2.Y + 40f), isHovering ? Color.Yellow : Color.White);
            string toFile = $"C://Users//" + Path.Combine($"{Environment.UserName}") + "//Documents//My Games//Terraria//ModLoader//Worlds//Slender_The_8_Pages.twld";
            if (isHovering)
            {
                if (Main.mouseRight && Main.mouseRightRelease)
                {
                    if (!File.Exists(toFile))
                    {
                        Main.WorldPath = "C://Documents//My Games//Terraria//ModLoader//Mod Sources//SpookyTerraria//SlenderWorld//Slender_The_8_Pages.twld";
                    }
                }
            }
            // Creating file for the slendy world
            // Me: Tries to sound smart
            // Her: Poop

            // IL == bad
        }*/
        public override void Unload()
        {
            IL.Terraria.Main.UpdateAudio -= new ILContext.Manipulator(ChangeMenuMusic);
            customTitleMusicSlot = 6;
            ManualResetEvent evt = titleMusicStopped;
            if (evt != null)
            {
                evt.Set();
            }
            titleMusicStopped = null;
            // Wtf...
            Main.versionNumber = $"v1.3.5.3";
            SpookyTerrariaUtils.ReturnTexturesToDefaults();

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
        public static bool PlayerIsInNoBiome(Player player)
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
                && !player.ZoneOverworldHeight;
        }
        public static bool PlayerUnderground(Player player)
        {
            return !player.ZoneOverworldHeight
                && !player.ZoneDirtLayerHeight
                && player.ZoneRockLayerHeight;
        }
        public override void PreSaveAndQuit()
        {
            playMusicTimer_UseOnce = 0;
            SoundEngine.StopAllAmbientSounds();
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
            if (ModLoader.GetMod("TerrariaOverhaul") == null && ModLoader.GetMod("CalamityModMusic") == null && ModLoader.GetMod("MusicFromOnePointFour") == null && ModLoader.GetMod("MusicFromOnePointFour") == null)
            {
                MenuMusicSet_Slender(); // Mirsario, you suck ass. Your mod takes all priority. So do you, Fabsol.
            }
            else
            {
                return;
            }
            base.PreSaveAndQuit();
        }
        public override void Close()
        {
            int soundSlot2 = GetSoundSlot(SoundType.Music, "Sounds/Music/MainMenu/Slender_MainMenu");
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
        public float updateGameZoomTargetValue;
        /// <summary>
        /// Determining play values for slender music.
        /// </summary>
        public int playMusicTimer_UseOnce;
        public override void PostUpdateEverything()
        {
            playMusicTimer_UseOnce++;
            // Main.NewText($"X: {(int)Main.MouseWorld.X / 16} | Y: {(int)Main.MouseWorld.Y / 16}");
            updateGameZoomTargetValue = Main.GameZoomTarget;
            Player player = Main.player[Main.myPlayer];
            if (Main.hasFocus)
            {
                player.GetModPlayer<SpookyPlayer>().deathTextTimer--;
                if (player.GetModPlayer<SpookyPlayer>().deathTextTimer < 0)
                {
                    player.GetModPlayer<SpookyPlayer>().deathTextTimer = 0;
                }
            }
            if (player.respawnTimer == 25)
            {
                if (Main.worldName == SpookyTerrariaUtils.slenderWorldName)
                {
                    SpookyPlayer.pages = 0;
                    SpookyTerrariaUtils.RemoveAllPossiblePagePositions();
                    Main.SaveSettings();

                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        WorldFile.CacheSaveTime();
                    }
                    Main.invasionProgress = 0;
                    Main.invasionProgressDisplayLeft = 0;
                    Main.invasionProgressAlpha = 0f;
                    Main.menuMode = 10;
                    Main.StopTrackedSounds();
                    CaptureInterface.ResetFocus();
                    Main.ActivePlayerFileData.StopPlayTimer();

                    Player.SavePlayer(Main.ActivePlayerFileData, false);

                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        WorldFile.saveWorld();
                    }
                    else
                    {
                        Netplay.disconnect = true;
                        Main.netMode = NetmodeID.SinglePlayer;
                    }

                    Main.fastForwardTime = false;
                    Main.UpdateSundial();
                    Main.menuMode = 0;
                }
            }
            /*
            if (Main.worldName == SpookyTerrariaUtils.slenderWorldName)
            {
                Rectangle insideBathrooms = new Rectangle(2091 * SpookyTerrariaUtils.tileScaling, 367 * SpookyTerrariaUtils.tileScaling, 85 * SpookyTerrariaUtils.tileScaling, 26 * SpookyTerrariaUtils.tileScaling);
                if (player.Hitbox.Intersects(insideBathrooms))
                {
                    Main.NewText(updateGameZoomTargetValue + ", " + Main.GameZoomTarget);
                    updateGameZoomTargetValue += 0.05f;
                    if (updateGameZoomTargetValue > 8.75f)
                    {
                        updateGameZoomTargetValue = 8.75f;
                    }
                }
            }
            */
            // Fix for lerping
            Main.soundInstanceMenuTick.Volume = 0f;
            Main.soundInstanceMenuOpen.Volume = 0f;
            Main.soundInstanceMenuClose.Volume = 0f;
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
                if (!stopTitleMusic)
                {
                    music = 6;
                }
                else
                {
                    stopTitleMusic = true;
                }
                customTitleMusicSlot = 6;
                Music music2 = GetMusic("Sounds/Music/MainMenu/Slender_MainMenu");
                if (music2.IsPlaying)
                {
                    music2.Stop(AudioStopOptions.Immediate);
                }
                titleMusicStopped?.Set();
                stopTitleMusic = false;
            }
            Player player = Main.player[Main.myPlayer];
            bool pageReqMetTier1 = SpookyPlayer.pages >= 1 && SpookyPlayer.pages < 3;
            bool pageReqMetTier2 = SpookyPlayer.pages >= 3 && SpookyPlayer.pages < 5;
            bool pageReqMetTier3 = SpookyPlayer.pages >= 5 && SpookyPlayer.pages < 7;
            bool pageReqMetTier4 = SpookyPlayer.pages >= 7 && SpookyPlayer.pages < 8;

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