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
using Terraria.Localization;

namespace SpookyTerraria
{
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
                CombatText.NewText(player.getRect(), Color.White, $"Page count was set to {type2}.");
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
        int rand;
        public override void Load()
        {
            rand = Main.rand.Next(0, 20);
            msgOfTheDay = ChooseRandomMessage(rand);
            Main.soundMenuTick = GetSound("Sounds/Custom/Other/Nothingness");
            Main.soundMenuOpen = GetSound("Sounds/Custom/Other/Nothingness");
            Main.soundMenuClose = GetSound("Sounds/Custom/Other/Nothingness");
            Mod spooky = ModLoader.GetMod("SpookyTerraria");
            Main.versionNumber = $"Terraria {Main.versionNumber}\n{spooky.Name} v{spooky.Version}";

            Sprint = RegisterHotKey("Sprint", "LeftShift");

            Hooks.On_AddMenuButtons += Hooks_On_AddMenuButtons;
            if (!Main.dedServ)
            {
                On.Terraria.Lang.GetRandomGameTitle += Lang_GetRandomGameTitle;
                Main.chTitle = true;
            }
            
            On.Terraria.Main.DrawBG += Main_DrawBG;
            On.Terraria.Main.DrawMenu += Main_DrawMenu;
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
        private string Lang_GetRandomGameTitle(On.Terraria.Lang.orig_GetRandomGameTitle orig)
        {
            return $"Spooky Terraria: {msgOfTheDay}";
        }

        private void Hooks_On_AddMenuButtons(Hooks.Orig_AddMenuButtons orig, Main main, int selectedMenu, string[] buttonNames, float[] buttonScales, ref int offY, ref int spacing, ref int buttonIndex, ref int numButtons)
        {
            orig(main, selectedMenu, buttonNames, buttonScales, ref offY, ref spacing, ref buttonIndex, ref numButtons);
            MenuHelper.AddButton("Download Slender Map",
            delegate
            {
                Process.Start("http://www.mediafire.com/file/y98ss4cb2vgk4vj/Slender_The_8_Pages.wld/file");
            },
            selectedMenu, buttonNames, ref buttonIndex, ref numButtons);
        }

        public virtual string ChooseRandomMessage(int type)
        {
            var steamID = Steamworks.SteamUser.GetSteamID();
            switch (type)
            {
                default:
                    return "Beware the man himself!";
                case 1:
                    return "Are you spooked yet?";
                case 2:
                    return $"Enjoy the mod, {Steamworks.SteamFriends.GetFriendPersonaName(steamID)}.";
                case 3:
                    return "What awaits will not be pleasant.";
                case 4:
                    return "Have a nice ride!";
                case 5:
                    return "Is the main menu spooking you?";
                case 6:
                    return "Too spooky 5 u";
                case 7:
                    return "I await your return to this screen.";
                case 8:
                    return "I know where you live...";
                case 9:
                    return "Everything is nothing, I think.";
                case 10:
                    return "This is not a copied game!";
                case 11:
                    return "Press 'Single Player' to get started. I am waiting.";
                case 12:
                    return "This is your final breath.";
                case 13:
                    return "He stalks you, but which one of them am I talking about?";
                case 14:
                    return "Nothing is worse than death by tentacles.";
                case 15:
                    return "Only the most diligent will win.";
                case 16:
                    return "Kill.";
                case 17:
                    return "Hell.";
                case 18:
                    return "Fear is the only emotion you should know.";
                case 19:
                    return "No pain, no gain.";
            }
        }
        internal static string ChooseRandomSubString(int type)
        {
            switch (type)
            {
                case 1:
                    return "ERRRRR";
                case 2:
                    return "SUFFERING";
                case 3:
                    return "IMMINENT DEATH";
                case 4:
                    return "DEATH";
                default:
                    return "PAIN";
            }
        }

        private static float rotationTimer_BasedOnSineWave;
        private static float scaleTimer_BasedOnSineWave;
        private void Main_DrawBG(On.Terraria.Main.orig_DrawBG orig, Main self)
        {
            scaleTimer_BasedOnSineWave += 0.1f;
            rotationTimer_BasedOnSineWave += 0.01f;
            Mod mod = this;
            Texture2D slendeyBG = mod.GetTexture("Assets/Slender8Pages");
            Texture2D blackPixel = mod.GetTexture("Assets/BlackPixel");
            Texture2D slender = mod.GetTexture("Assets/Slender");

            float sinValueRot = (float)Math.Sin(rotationTimer_BasedOnSineWave);
            float sinValueScale = (float)Math.Sin(scaleTimer_BasedOnSineWave / 2);
            if (Main.gameMenu)
            {
                Main.spriteBatch.Draw(blackPixel, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, new Vector2(Main.screenWidth, Main.screenHeight), SpriteEffects.None, 1f);
                Main.spriteBatch.Draw(slender, new Vector2(Main.screenWidth / 2, Main.screenHeight / 2), null, Color.White, sinValueRot / 50, new Vector2(slender.Width / 2, slender.Height / 2),  2f + (sinValueScale / 80), SpriteEffects.None, 1f);
            }
            else
            {
                orig(self);
            }
        }
        public static string msgOfTheDay;
        private void Main_DrawMenu(On.Terraria.Main.orig_DrawMenu orig, Main self, GameTime gameTime)
        {
            Mod mod = this;

            SpriteEffects none = SpriteEffects.None;

            int y = 40;
            int y2 = 80;

            float txtScale = 0.25f;

            string myMedia = "My Media:";
            string display4Twitch = "<- My Twitch Channel!";
            string display4YT = "<- My YouTube Channel!";
            string display4Discord = "<- My Discord Server!";

            Texture2D Twitch = mod.GetTexture("Assets/OtherAssets/TwitchLogo");
            Texture2D YouTube = mod.GetTexture("Assets/OtherAssets/YouTubeLogo");
            Texture2D Discord = mod.GetTexture("Assets/OtherAssets/DiscordLogo");

            Vector2 drawPosition = new Vector2(20, 40);
            Vector2 TwitchHalf = Main.fontDeathText.MeasureString(display4Twitch) / 2;
            Vector2 YTHalf = Main.fontDeathText.MeasureString(display4YT) / 2;
            Vector2 DiscordHalf = Main.fontDeathText.MeasureString(display4Discord) / 2;

            Rectangle TwitchRect = new Rectangle((int)drawPosition.X, (int)drawPosition.Y, 30, 30);
            Rectangle YTRect = new Rectangle((int)drawPosition.X, (int)drawPosition.Y + y, 40, 25);
            Rectangle DiscordRect = new Rectangle((int)drawPosition.X, (int)drawPosition.Y + y2, 30, 35);

            Point mouse = Main.MouseScreen.ToPoint();

            bool mouseContainedTwitch = TwitchRect.Contains(mouse);
            bool mouseContainedYT = YTRect.Contains(mouse);
            bool mouseContainedDiscord = DiscordRect.Contains(mouse);

            // Strings to pics, proper drawpos, etc
            try
            {
                Main.spriteBatch.Draw(Twitch, drawPosition, null, mouseContainedTwitch ? Color.Yellow : Color.White, 0f, Vector2.Zero, 0.1f, none, 1f);
                Main.spriteBatch.Draw(YouTube, drawPosition + new Vector2(0, y), null, mouseContainedYT ? Color.Yellow : Color.White, 0f, Vector2.Zero, 0.15f, none, 1f);
                Main.spriteBatch.Draw(Discord, drawPosition + new Vector2(0, y2), null, mouseContainedDiscord ? Color.Yellow : Color.White, 0f, Vector2.Zero, 0.075f, none, 1f);

                Main.spriteBatch.DrawString(Main.fontDeathText, myMedia, drawPosition - new Vector2(0, 20), Color.White, 0f, Vector2.Zero, txtScale, none, 1f);
                if (Main.menuMode == 0)
                {
                    Main.spriteBatch.DrawString(Main.fontDeathText, msgOfTheDay, new Vector2(Main.screenWidth / 2, Main.screenHeight - 20), Color.White, 0f, Main.fontDeathText.MeasureString(msgOfTheDay) / 2, 1f, none, 1f);
                }

                if (mouseContainedTwitch)
                {
                    Main.spriteBatch.DrawString(Main.fontDeathText, display4Twitch, new Vector2(60, 45), Color.White, 0f, Vector2.Zero, txtScale, none, 1f);
                    if (Main.mouseLeft && Main.mouseLeftRelease)
                    {
                        Process.Start("https://twitch.tv/righteousryan_");
                    }
                }
                if (mouseContainedYT)
                {
                    Main.spriteBatch.DrawString(Main.fontDeathText, display4YT, new Vector2(55, 85), Color.White, 0f, Vector2.Zero, txtScale, none, 1f);
                    if (Main.mouseLeft && Main.mouseLeftRelease)
                    {
                        Process.Start("https://www.youtube.com/channel/UCnz-trf-dF7j8mDVOuoy4Ig");
                    }
                }
                if (mouseContainedDiscord)
                {
                    Main.spriteBatch.DrawString(Main.fontDeathText, display4Discord, new Vector2(50, 130), Color.White, 0f, Vector2.Zero, txtScale, none, 1f);
                    if (Main.mouseLeft && Main.mouseLeftRelease)
                    {
                        Process.Start("https://discord.gg/pT2BzSG");
                    }
                }
            }
            catch(Exception e)
            {
                mod.Logger.Warn($"{0} was not able to draw correctly!\nException caught! {e.Message} {e.StackTrace}");
            }
            orig(self, gameTime);
        }
        private void Main_DrawInterface_35_YouDied(On.Terraria.Main.orig_DrawInterface_35_YouDied orig)
        {
        }
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