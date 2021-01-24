using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using SpookyTerraria.ModIntances;
using SpookyTerraria.NPCs;
using SpookyTerraria.Tiles;
using SpookyTerraria.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Terraria.IO;
using Terraria.Graphics.Capture;
using System.Reflection;
using Terraria.GameContent.UI.States;
using static Terraria.GameContent.UI.States.UIVirtualKeyboard;
using Terraria.UI.Chat;
using Microsoft.Xna.Framework.Input;

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
        public SpookyTerraria() { ModContent.GetInstance<SpookyTerraria>(); }
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

        public static SoundEffect tick;
        public static SoundEffect open;
        public static SoundEffect close;
        public static Texture2D storedLogo;
        public static Texture2D storedLogo2;
        public override void Load()
        {
            storedLogo = Main.logoTexture;
            storedLogo2 = Main.logo2Texture;
            Main.logoTexture = GetTexture("Assets/Invisible");
            Main.logo2Texture = GetTexture("Assets/Invisible");

            msgOfTheDay = ChooseRandomMessage(rand);
            rand = Main.rand.Next(0, 20);
            tick = Main.soundMenuTick;
            open = Main.soundMenuOpen;
            close = Main.soundMenuClose;

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
            // MaxMenuItems = 16
            On.Terraria.Main.DrawInterface_30_Hotbar += Main_DrawInterface_30_Hotbar;
            On.Terraria.Main.DrawPlayers += Main_DrawPlayers;
            On.Terraria.Main.DrawBG += Main_DrawBG;
            On.Terraria.Main.DrawMenu += Main_DrawMenu;
            On.Terraria.Main.DrawInterface_35_YouDied += Main_DrawInterface_35_YouDied;

            // IL.Terraria.Main.DrawMenu += MoveOptionUI;
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
            fart = GetSound("Sounds/Custom/Other/wtf");
            fartInstance = fart.CreateInstance();
            /*ContentInstance.Register(new AmbienceHelper());
            ModContent.GetInstance<AmbienceHelper>().InitializeSoundInstances();*/
        }

        private void Main_DrawInterface_30_Hotbar(On.Terraria.Main.orig_DrawInterface_30_Hotbar orig, Main self)
        {
            orig(self);
            // maybe later
        }

        public override void Unload()
        {
            Main.logoTexture = storedLogo;
            Main.logo2Texture = storedLogo2;
            Main.soundMenuTick = tick;
            Main.soundMenuClose = close;
            Main.soundMenuOpen = open;
            Main.logoTexture = storedLogo;
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

            // Main.soundMenuTick = Main.instance.OurLoad<SoundEffect>("Main/MenuTick");
            /*Main.soundMenuOpen = Main.soundMenuOpen;
            Main.soundMenuClose = Main.soundMenuClose;*/

            Sprint = null;
        }
        public SoundEffect fart;
        public SoundEffectInstance fartInstance;
        public override void PostUpdateEverything()
        {
            // ModContent.GetInstance<AmbienceHelper>().HandleAmbiences();
            Main.raining = false;
            Main.rainTime = 0;
            Main.maxRain = 0;
            musicType = ModContent.GetInstance<SpookyConfigClient>().musicType;
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
        public Rectangle staticFrame = new Rectangle(0, 0, 1271, 650);
        public int timerToLightStatic;
        public int timerToMediumStatic;
        public int timerToSevereStatic;
        public float fadeScale;
        private void Main_DrawPlayers(On.Terraria.Main.orig_DrawPlayers orig, Main self)
        {
            orig(self);
            Mod mod = this;
            Player player = Main.player[Main.myPlayer];
            if (player.dead)
            {
                Main.hideUI = true;
                fadeScale = 0f;
            }
            if (!Main.hasFocus)
            {
                timerToLightStatic = 0;
                timerToMediumStatic = 0;
                timerToSevereStatic = 0;
            }
            for (int ll = 0; ll < Main.maxNPCs; ll++)
            {
                NPC npc = Main.npc[ll];
                float distance = npc.Distance(Main.player[Main.myPlayer].Center);
                if (npc.active && distance <= 1000f)
                {
                    if (npc.type == ModContent.NPCType<Slenderman>())
                    {
                        bool noLight = Lighting.GetSubLight(npc.Top).X < 0.05f;
                        if (Main.GameUpdateCount % 3 == 0)
                        {
                            staticFrame.Y += 650;
                        }
                        if (staticFrame.Y >= 1950)
                        {
                            staticFrame.Y = 0;
                        }
                        bool facingTowardsSlendermanLeft = npc.Center.X < player.Center.X && player.direction == -1;
                        bool facingTowardsSlendermanRight = npc.Center.X > player.Center.X && player.direction == 1;
                        Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend); // sourceRectangle: new Rectangle((int)Main.screenPosition.X, (int)Main.screenPosition.Y
                        Main.spriteBatch.Draw(mod.GetTexture("Assets/Static"), new Vector2(Main.screenWidth / 2, Main.screenHeight / 2), sourceRectangle: staticFrame, Color.White * fadeScale, 0f, new Vector2(Main.screenWidth / 2, Main.screenHeight / 2), 5f, SpriteEffects.None, 1f);
                        Main.spriteBatch.End();
                        if (facingTowardsSlendermanLeft || facingTowardsSlendermanRight && !noLight)
                        {
                            if (fadeScale > 0f && fadeScale < 0.4f)
                            {
                                timerToMediumStatic = 0;
                                timerToSevereStatic = 0;
                                timerToLightStatic++;
                                if (Main.hasFocus)
                                {
                                    AmbienceHandler.StopAmbientSound("Sounds/Custom/Slender/StaticMedium");
                                    AmbienceHandler.StopAmbientSound("Sounds/Custom/Slender/StaticSevere");
                                }
                                if (timerToLightStatic == 2)
                                {
                                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Slender/StaticLight"));
                                }
                                if (timerToLightStatic >= 480)
                                {
                                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Slender/StaticLight"));
                                    timerToLightStatic = 0;
                                }
                            }
                            if (fadeScale >= 0.4 && fadeScale < 0.8f)
                            {
                                timerToLightStatic = 0;
                                timerToSevereStatic = 0;
                                timerToMediumStatic++;
                                if (Main.hasFocus)
                                {
                                    AmbienceHandler.StopAmbientSound("Sounds/Custom/Slender/StaticLight");
                                    AmbienceHandler.StopAmbientSound("Sounds/Custom/Slender/StaticSevere");
                                }
                                if (timerToMediumStatic == 2)
                                {
                                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Slender/StaticMedium"));
                                }
                                if (timerToMediumStatic >= 480)
                                {
                                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Slender/StaticMedium"));
                                    timerToMediumStatic = 0;
                                }
                            }
                            if (fadeScale >= 0.8f)
                            {
                                timerToMediumStatic = 0;
                                timerToLightStatic = 0;
                                timerToSevereStatic++;
                                if (Main.hasFocus)
                                {
                                    AmbienceHandler.StopAmbientSound("Sounds/Custom/Slender/StaticLight");
                                    AmbienceHandler.StopAmbientSound("Sounds/Custom/Slender/StaticMedium");
                                }
                                if (timerToSevereStatic == 2)
                                {
                                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Slender/StaticSevere"));
                                }
                                if (timerToSevereStatic >= 132)
                                {
                                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Slender/StaticSevere"));
                                    timerToSevereStatic = 0;
                                }
                            }
                            // MAJOR TODO: MAKE STATIC
                            if (!Collision.SolidCollision(npc.position, npc.width, 20) && distance <= 1000 && Collision.CanHitLine(player.Top, player.width, 10, npc.Top, npc.width, 20))
                            {
                                if (!noLight)
                                {
                                    if (Main.GameUpdateCount % 3 == 0)
                                    {
                                        fadeScale += 0.005f;
                                    }
                                }
                                else
                                {
                                    if (Main.GameUpdateCount % 10 == 0)
                                    {
                                        fadeScale += 0.005f;
                                    }
                                }
                            }
                        }
                        else if (!facingTowardsSlendermanLeft && !facingTowardsSlendermanRight)
                        {
                            timerToLightStatic = 0;
                            timerToMediumStatic = 0;
                            timerToSevereStatic = 0;
                            if (!noLight)
                            {
                                if (Main.GameUpdateCount % 3 == 0)
                                {
                                    fadeScale -= 0.005f;
                                }
                            }
                            else
                            {
                                if (Main.GameUpdateCount % 10 == 0)
                                {
                                    fadeScale -= 0.005f;
                                }
                            }
                        }
                        else if (distance > 1000f)
                        {
                            if (Main.GameUpdateCount % 3 == 0)
                            {
                                fadeScale -= 0.005f;
                            }
                        }
                        if (fadeScale > 1f)
                        {
                            fadeScale = 1f;
                        }
                        if (fadeScale < 0f)
                        {
                            fadeScale = 0f;
                        }
                        if (fadeScale >= 1f)
                        {
                            player.KillMe(Terraria.DataStructures.PlayerDeathReason.ByCustomReason($"{player.name} stared at death for too long."), player.statLife + 50, 0, false);
                        }
                        if (fadeScale == 0f)
                        {
                            if (Main.hasFocus)
                            {
                                AmbienceHandler.StopAmbientSound("Sounds/Custom/Slender/StaticMedium");
                                AmbienceHandler.StopAmbientSound("Sounds/Custom/Slender/StaticLight");
                                AmbienceHandler.StopAmbientSound("Sounds/Custom/Slender/StaticSevere");
                            }
                        }
                    }
                    if (npc.type == ModContent.NPCType<Slenderman>() && !npc.active)
                    {
                        AmbienceHandler.StopAmbientSound("Sounds/Custom/Slender/StaticMedium");
                        AmbienceHandler.StopAmbientSound("Sounds/Custom/Slender/StaticLight");
                        timerToLightStatic = 0;
                        timerToMediumStatic = 0;
                        timerToSevereStatic = 0;
                        fadeScale -= 0.005f;
                    }
                }
                // Main.NewText($"L: {timerToLightStatic} | M: {timerToMediumStatic} | S: {timerToSevereStatic}");
            }
        }
        private string Lang_GetRandomGameTitle(On.Terraria.Lang.orig_GetRandomGameTitle orig)
        {
            return $"Spooky Terraria: {msgOfTheDay}";
        }

        private void Hooks_On_AddMenuButtons(Hooks.Orig_AddMenuButtons orig, Main main, int selectedMenu, string[] buttonNames, float[] buttonScales, ref int offY, ref int spacing, ref int buttonIndex, ref int numButtons)
        {
            orig(main, selectedMenu, buttonNames, buttonScales, ref offY, ref spacing, ref buttonIndex, ref numButtons);
            offY = 525;
            spacing = 35;
            MenuHelper.AddButton("Slender Extras",
            delegate
            {
                Main.menuMode = SlenderMenuModeID.SlenderExtras;
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

        private static Vector2 drawPos;
        private static float rotationTimer_BasedOnSineWave;
        private static float scaleTimer_BasedOnSineWave;
        private void Main_DrawBG(On.Terraria.Main.orig_DrawBG orig, Main self)
        {
            scaleTimer_BasedOnSineWave += 0.1f;
            rotationTimer_BasedOnSineWave += 0.01f;

            Vector2 screenBounds = new Vector2(Main.screenWidth, Main.screenHeight);
            float sinValueRot = (float)Math.Sin(rotationTimer_BasedOnSineWave);
            float sinValueScale = (float)Math.Sin(scaleTimer_BasedOnSineWave / 2);
            bool inSettingsOrMPUI = Main.menuMode > 0 && Main.menuMode != 888 && Main.menuMode != 1010;
            if (Main.gameMenu)
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                /*if (displayCooldown <= 0 && timeToDisplay > 0)
                {
                    directionRand = Main.rand.Next(1, 9);
                    fadeScale += 0.01f;
                    switch (directionRand)
                    {
                        case 1:
                            direction = SketchTravelDirections.Up;
                            break;
                        case 2:
                            direction = SketchTravelDirections.Down;
                            break;
                        case 3:
                            direction = SketchTravelDirections.Left;
                            break;
                        case 4:
                            direction = SketchTravelDirections.Right;
                            break;
                        case 5:
                            direction = SketchTravelDirections.UpRight;
                            break;
                        case 6:
                            direction = SketchTravelDirections.UpLeft;
                            break;
                        case 7:
                            direction = SketchTravelDirections.DownLeft;
                            break;
                        case 8:
                            direction = SketchTravelDirections.DownRight;
                            break;
                        default:
                            Logger.Warn($"directionRand returned {directionRand}! (Invalid)");
                            break;
                    }
                }
                else
                {
                    fadeScale -= 0.01f;
                }
                if (direction == SketchTravelDirections.Up)
                {
                    initialDrawPosition.Y -= 0.05f;
                }
                if (direction == SketchTravelDirections.Down)
                {
                    initialDrawPosition.Y += 0.05f;
                }
                if (direction == SketchTravelDirections.Left)
                {
                    initialDrawPosition.X -= 0.05f;
                }
                if (direction == SketchTravelDirections.Right)
                {
                    initialDrawPosition.X += 0.05f;
                }
                if (direction == SketchTravelDirections.UpRight)
                {
                    initialDrawPosition.X += 0.05f;
                    initialDrawPosition.Y -= 0.05f;
                }
                if (direction == SketchTravelDirections.UpLeft)
                {
                    initialDrawPosition.X -= 0.05f;
                    initialDrawPosition.Y -= 0.05f;
                }
                if (direction == SketchTravelDirections.DownRight)
                {
                    initialDrawPosition.X += 0.05f;
                    initialDrawPosition.Y += 0.05f;
                }
                if (direction == SketchTravelDirections.DownLeft)
                {
                    initialDrawPosition.X -= 0.05f;
                    initialDrawPosition.Y += 0.05f;
                }
                SpriteEffects none = SpriteEffects.None;
                _fadeScale = Main.rand.NextFloat();
                if (fadeScale == 0f)
                {
                    actualRand = Main.rand.Next(PageID.Follows, PageID.NoX12 + 1);
                    try
                    {
                        int randW = Main.rand.Next(-250, Main.screenWidth + 250);
                        int randH = Main.rand.Next(-250, Main.screenHeight + 250);
                        initialDrawPosition = new Vector2(randW, randH);
                        timeToDisplay = TimeCreator.AddSeconds(2);
                    }
                    catch (Exception thrown)
                    {
                        actualRand = _validSketches[Main.rand.Next(_validSketches[0], _validSketches.Length)];
                        Logger.Warn($"Exception thrown, but draw was reverted to sketch{actualRand}, Exception: {thrown.Message}\nStackTrace: {thrown.StackTrace}");
                    }
                }
                else
                {
                    Main.spriteBatch.Draw(GetTexture($"Assets/MenuUI/sketch{actualRand}"), initialDrawPosition, null, Color.White * fadeScale, 0f, GetTexture($"Assets/MenuUI/sketch{actualRand}").Size() / 2, 1f, none, 1f);
                }
                Main.spriteBatch.DrawString(Main.fontCombatText[1], $"actualRand: {actualRand}\ninitialDrawPosition: {initialDrawPosition}\nDirection: {direction}\n_fadeScale: {_fadeScale}", new Vector2(50, 50), Color.White);*/
                Main.spriteBatch.Draw(GetTexture("Assets/BlackPixel"), Vector2.Zero, null, Color.White, 0f, Vector2.Zero, new Vector2(Main.screenWidth, Main.screenHeight), SpriteEffects.None, 1f);
                Main.spriteBatch.Draw(GetTexture("Assets/THE_CHAD"), new Vector2(150, Main.screenHeight - 30), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                if (Main.menuMode == SlenderMenuModeID.SlenderChangeLogs)
                {
                    drawPos = new Vector2(screenBounds.X - 300, screenBounds.Y / 2);
                    Main.spriteBatch.Draw(GetTexture("Assets/Slender"), drawPos, null, Color.White, sinValueRot / 50, new Vector2(GetTexture("Assets/Slender").Width / 2, GetTexture("Assets/Slender").Height / 2), 1f + (sinValueScale / 80), SpriteEffects.None, 1f);
                }
                if (Main.menuMode != SlenderMenuModeID.SlenderChangeLogs)
                {
                    Main.spriteBatch.Draw(GetTexture("Assets/Slender"), inSettingsOrMPUI ? new Vector2(Main.screenWidth / 2, 725) : new Vector2(Main.screenWidth / 2, 220), null, Color.White, sinValueRot / 50, new Vector2(GetTexture("Assets/Slender").Width / 2, GetTexture("Assets/Slender").Height / 2), 1f + (sinValueScale / 80), SpriteEffects.None, 1f);
                }
            }
            else
            {
                orig(self);
            }
        }
        // Vars for drawing scribble on the main menu
        internal enum SketchTravelDirections
        {
            Up,
            Down,
            Left,
            Right,
            UpRight,
            UpLeft,
            DownLeft,
            DownRight
        }
        /*internal int actualRand = 0;
        internal SketchTravelDirections direction;
        internal static int directionRand;
        internal static int timeToDisplay;
        internal static int displayCooldown;
        public static Vector2 initialDrawPosition;
        private float _fadeScale = 0f;
        private short[] _validSketches = new short[] { PageID.Follows, PageID.CantRun, PageID.Trees, PageID.LeaveMeAlone, PageID.AlwaysWatchesNoEyes, PageID.HelpMe, PageID.DontLook, PageID.NoX12 };
        */
        public static string msgOfTheDay;
        internal static float buttonScaleReturn;
        internal static float buttonScaleDownloadMap;
        internal static float buttonScaleDownloadSlender;
        internal static float buttonScaleMythos;
        internal static float buttonScaleChangeLogs;

        private static string changeLog;

        private static int compareLogs;
        private static int currentLogsMenu;

        public static int[] possibleLogPages = new int[]
        {
            0,
            1,
            2,
        };
        private void Main_DrawMenu(On.Terraria.Main.orig_DrawMenu orig, Main self, GameTime gameTime)
        {
            AmbienceHandler.StopAllAmbientSounds();

            int yOff = 50;

            buttonScaleReturn = Utils.Clamp(buttonScaleReturn, 0.7f, 1f);
            buttonScaleDownloadMap = Utils.Clamp(buttonScaleDownloadMap, 0.7f, 1f);
            buttonScaleDownloadSlender = Utils.Clamp(buttonScaleDownloadSlender, 0.7f, 1f);
            buttonScaleMythos = Utils.Clamp(buttonScaleMythos, 0.7f, 1f);
            buttonScaleChangeLogs = Utils.Clamp(buttonScaleChangeLogs, 0.7f, 1f);

            string @return = "Back";
            string downloadMap = "Download Slender Terraria Map";
            string downloadSlender = "Download Slender: The 8 Pages";
            string visitMythos = "Slender Mythos";
            string changeLogs = "Spooky Terraria Changelogs";

            Vector2 midScreen = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
            Vector2 midReturn = Main.fontDeathText.MeasureString(@return) / 2;
            Vector2 midMythos = Main.fontDeathText.MeasureString(visitMythos) / 2;
            Vector2 midGameDL = Main.fontDeathText.MeasureString(downloadSlender) / 2;
            Vector2 midMapDL = Main.fontDeathText.MeasureString(downloadMap) / 2;
            Vector2 midChangeLogs = Main.fontDeathText.MeasureString(changeLogs) / 2;

            Rectangle returnButton = new Rectangle((int)midScreen.X - 65, (int)midScreen.Y - 40, 120, 50);
            Rectangle changeLogsButton = new Rectangle((int)midScreen.X - 310, (int)midScreen.Y - 90, 600, 50);
            Rectangle slenderMapButton = new Rectangle((int)midScreen.X - 350, (int)midScreen.Y - 240, 690, 50);
            Rectangle slenderGameButton = new Rectangle((int)midScreen.X - 350, (int)midScreen.Y - 190, 690, 50);
            Rectangle mythosButton = new Rectangle((int)midScreen.X - 150, (int)midScreen.Y - 140, 310, 50);

            bool containReturn = returnButton.Contains(Main.MouseScreen.ToPoint());
            bool containMapDL = slenderMapButton.Contains(Main.MouseScreen.ToPoint());
            bool containGameDL = slenderGameButton.Contains(Main.MouseScreen.ToPoint());
            bool containMythosPage = mythosButton.Contains(Main.MouseScreen.ToPoint());
            bool containChangeLogs = changeLogsButton.Contains(Main.MouseScreen.ToPoint());

            buttonScaleReturn += containReturn ? 0.03f : -0.03f;
            buttonScaleDownloadSlender += containGameDL ? 0.03f : -0.03f;
            buttonScaleDownloadMap += containMapDL ? 0.03f : -0.03f;
            buttonScaleMythos += containMythosPage ? 0.03f : -0.03f;
            buttonScaleChangeLogs += containChangeLogs ? 0.03f : -0.03f;

            if (Main.menuMode == SlenderMenuModeID.SlenderExtras)
            {
                // Main.spriteBatch.Draw(GetTexture("Assets/Debug/WhitePixel"), new Vector2(returnButton.X, returnButton.Y), null, Color.White, 0f, Vector2.Zero, new Vector2(returnButton.Width, returnButton.Height), SpriteEffects.None, 1f);
                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontDeathText, downloadMap, midScreen - new Vector2(0, yOff * 4), containMapDL ? Color.Yellow : Color.Gray, 0f, midMapDL, new Vector2(buttonScaleDownloadMap, buttonScaleDownloadMap));
                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontDeathText, downloadSlender, midScreen - new Vector2(0, yOff * 3), containGameDL ? Color.Yellow : Color.Gray, 0f, midGameDL, new Vector2(buttonScaleDownloadSlender, buttonScaleDownloadSlender));
                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontDeathText, visitMythos, midScreen - new Vector2(0, yOff * 2), containMythosPage ? Color.Yellow : Color.Gray, 0f, midMythos, new Vector2(buttonScaleMythos, buttonScaleMythos));
                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontDeathText, @return, midScreen - new Vector2(0, 0), containReturn ? Color.Yellow : Color.Gray, 0f, midReturn, new Vector2(buttonScaleReturn, buttonScaleReturn));
                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontDeathText, changeLogs, midScreen - new Vector2(0, yOff), containChangeLogs ? Color.Yellow : Color.Gray, 0f, midChangeLogs, new Vector2(buttonScaleChangeLogs, buttonScaleChangeLogs));

                if (containReturn)
                {
                    if (Main.mouseLeft && Main.mouseLeftRelease)
                    {
                        Main.menuMode = 0;
                    }
                }
                if (containMapDL)
                {
                    if (Main.mouseLeft && Main.mouseLeftRelease)
                    {
                        Process.Start("https://cdn.discordapp.com/attachments/743141999846096959/796195488143245333/Slender_The_8_Pages.wld");
                    }
                }
                if (containGameDL)
                {
                    if (Main.mouseLeft && Main.mouseLeftRelease)
                    {
                        Process.Start("https://cdn.discordapp.com/attachments/427893900594774026/796565609852698644/Slender_v0.9.7.rar");
                    }
                }
                if (containMythosPage)
                {
                    if (Main.mouseLeft && Main.mouseLeftRelease)
                    {
                        Process.Start("https://theslenderman.fandom.com/wiki/Original_Mythos");
                    }
                }
                if (containChangeLogs)
                {
                    if (Main.mouseLeft && Main.mouseLeftRelease)
                    {
                        Main.menuMode = SlenderMenuModeID.SlenderChangeLogs;
                    }
                }
            }
            if (Main.menuMode == SlenderMenuModeID.SlenderChangeLogs)
            {
                Vector2 screenBounds = new Vector2(Main.screenWidth, Main.screenHeight);

                var keyState = Main.keyState;
                if (keyState.IsKeyDown(Keys.Escape))
                {
                    Main.menuMode = SlenderMenuModeID.SlenderExtras;
                    // Main.PlaySound(GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Action/JumpscareLoud"));
                    // This is a joke...
                }
                string escToReturn = "Press ESC to return";
                string reccommend = "If you wish to reccommend anything to me or let me know of a missing feature, please visit the forums post (homepage link) or join my discord server.";
                if (SlenderMain.updateLogMode == -1)
                {
                    changeLog = "There is no update log before version v[c/FFFF00:0.5.5].\nI apologize for not making one sooner!";
                }
                if (SlenderMain.updateLogMode == 0)
                {
                    Rectangle bugReports = new Rectangle(Main.screenWidth / 2 - 35, 280, 125, 20);
                    bool containBugReports = bugReports.Contains(Main.MouseScreen.ToPoint());
                    if (containBugReports)
                    {
                        if (Main.mouseLeft && Main.mouseLeftRelease)
                        {
                            Process.Start("https://discord.com/channels/701878519923343361/738155588193878046");
                        }
                    }
                    changeLog =
                        "            Change Logs for [c/FFFF00:Spooky Terraria]: v[c/FFFF00:0.5.5]"
                        + "\n - Added this very log!\n - Fixed some NPC chat issues where some chats\nwould not display."
                        + "\n - Potential fix to slender not spawning on some occasions.\n - Potential fix for multiple slendermen spawning.\nIf this issue persists, please visit my [c/5440cd:Discord] server and visit [c/5440cd:#bug-reports] to let me know of the issue."
                        + "\n - Added some new sounds."
						+ "\n - Added a lot of things to the Spooky Terraria Forums Page."
						+ "\n - A few UI revamps.";
                }
                if (SlenderMain.updateLogMode == 1)
                {
                    changeLog = "This update does not exist yet...\nPlease check regularly on the Mod Browser for more updates!";
                }
                float scale = 0.45f;

                Vector2 midEscText = Main.fontDeathText.MeasureString(escToReturn) / 2;
                Vector2 recMid = Main.fontDeathText.MeasureString(reccommend) / 2;
                Vector2 log055Dimensions = Main.fontDeathText.MeasureString(changeLog) * scale;

                Texture2D buttonPlay = Main.instance.OurLoad<Texture2D>("Images/UI/ButtonPlay");

                Vector2 topMid = new Vector2(screenBounds.X / 2, 50);
                Vector2 botMid = new Vector2(screenBounds.X / 2, screenBounds.Y - 10);

                Vector2 off = new Vector2(50, log055Dimensions.Y * 0.55f);

                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontDeathText, escToReturn, botMid - new Vector2(0, 15), Color.Gray, 0f, midEscText, new Vector2(0.3f, 0.3f));
                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontDeathText, reccommend, botMid, Color.Gray, 0f, recMid, new Vector2(0.3f, 0.3f));
                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontDeathText, changeLog, new Vector2(screenBounds.X / 2, 50), Color.White, 0f, new Vector2(log055Dimensions.X / 2, 0), new Vector2(scale, scale));

                Main.spriteBatch.Draw(buttonPlay, topMid + new Vector2(50, -15), null, Color.White, 0f, buttonPlay.Size() / 2, 1f, SpriteEffects.None, 1f);
                Main.spriteBatch.Draw(buttonPlay, topMid - new Vector2(50, 15), null, Color.White, 0f, buttonPlay.Size() / 2, 1f, SpriteEffects.FlipHorizontally, 1f);

                Rectangle pageRight = new Rectangle((int)topMid.X + 40, 23, buttonPlay.Width, buttonPlay.Height);
                Rectangle pageLeft = new Rectangle((int)topMid.X - 60, 23, buttonPlay.Width, buttonPlay.Height);

                bool containR = pageRight.Contains(Main.MouseScreen.ToPoint());
                bool containL = pageLeft.Contains(Main.MouseScreen.ToPoint());

                // Rectangle dimLogs = new Rectangle((int)topMid.X, (int)topMid.Y, (int)log055Dimensions.X, (int)log055Dimensions.Y);

                /*Main.spriteBatch.Draw(GetTexture("Assets/Debug/WhitePixel"), pageLeft, Color.White);
                Main.spriteBatch.Draw(GetTexture("Assets/Debug/WhitePixel"), pageRight, Color.Yellow);*/
                // Main.spriteBatch.Draw(GetTexture("Assets/Debug/WhitePixel"), bugReports, Color.White);

                /*compareLogs++;
                if (compareLogs == 2)
                {
                    currentLogsMenu = SlenderMain.updateLogMode;
                }
                if (compareLogs >= 3)
                {
                    compareLogs = 0;
                }
                if (currentLogsMenu != SlenderMain.updateLogMode)
                {
                    Main.PlaySound(GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Action/PagePickup"));
                }*/
                // ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontDeathText, $"compareLogs: {compareLogs}\nMode: {SlenderMain.updateLogMode}", Main.MouseScreen + new Vector2(25, 25), Color.Gray, 0f, Vector2.Zero, new Vector2(0.3f, 0.3f));
                if (containR)
                {
                    Main.spriteBatch.Draw(buttonPlay, topMid + new Vector2(50, -15), null, Color.White, 0f, buttonPlay.Size() / 2, 1.25f, SpriteEffects.None, 1f);

                    if (Main.mouseLeft && Main.mouseLeftRelease)
                    {
                        SlenderMain.updateLogMode++;
                        Main.PlaySound(GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Action/PagePickup"));
                    }
                }
                if (containL)
                {
                    Main.spriteBatch.Draw(buttonPlay, topMid - new Vector2(50, 15), null, Color.White, 0f, buttonPlay.Size() / 2, 1.25f, SpriteEffects.FlipHorizontally, 1f);

                    if (Main.mouseLeft && Main.mouseLeftRelease)
                    {
                        SlenderMain.updateLogMode--;
                        Main.PlaySound(GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Action/PagePickup"));
                    }
                }
            }
            SlenderMain.updateLogMode = Utils.Clamp<short>(SlenderMain.updateLogMode, -1, 1);
            // Completely different part of draw below, above is menu modes, under is socials

            SpriteEffects none = SpriteEffects.None;

            int y = 40;
            int y2 = 80;

            float txtScale = 0.25f;

            string myMedia = $"My Socials:";
            string display4Twitch = "<- My Twitch Channel!";
            string display4YT = "<- My YouTube Channel!";
            string display4Discord = "<- My Discord Server!";

            Texture2D Twitch = GetTexture("Assets/OtherAssets/TwitchLogo");
            Texture2D YouTube = GetTexture("Assets/OtherAssets/YouTubeLogo");
            Texture2D Discord = GetTexture("Assets/OtherAssets/DiscordLogo");

            Vector2 drawPosition = new Vector2(20, 40);

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
                    Main.spriteBatch.DrawString(Main.fontDeathText, msgOfTheDay, new Vector2(Main.screenWidth / 2, Main.screenHeight - 25), Color.White, 0f, Main.fontDeathText.MeasureString(msgOfTheDay) / 2, 1f, none, 1f);
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
                Logger.Warn($"{0} was not able to draw correctly!\nException caught! Message: {e.Message} StackTrace: {e.StackTrace}");
            }
            // scribbleX
            orig(self, gameTime);
        }
        private void Main_DrawInterface_35_YouDied(On.Terraria.Main.orig_DrawInterface_35_YouDied orig)
        {
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
            // Directory.CreateDirectory LUL
            msgOfTheDay = ChooseRandomMessage(rand);
            Main.chTitle = true;
            playMusicTimer_UseOnce = 0;
            // SoundEngine.StopAllAmbientSounds();
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
        public static string[] MusicTypes = new string[] { "The 8 Pages", "Slender's Shadow", "7th Street" };
        public static string musicType;
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

            if (musicType == MusicTypes[SlenderMusicID.TheEightPages])
            {
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
            }
            else if (musicType == MusicTypes[SlenderMusicID.SlendersShadow])
            {
                if (pageReqMetTier1)
                {
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/Slender/ShadowStage1");
                    priority = MusicPriority.BossHigh;
                }
                if (pageReqMetTier2)
                {
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/Slender/ShadowStage2");
                    priority = MusicPriority.BossHigh;
                }
                if (pageReqMetTier3)
                {
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/Slender/ShadowStage3");
                    priority = MusicPriority.BossHigh;
                }
                if (pageReqMetTier4)
                {
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/Slender/ShadowStage4");
                    priority = MusicPriority.BossHigh;
                }
            }
            else if (musicType == MusicTypes[SlenderMusicID.SeventhStreet])
            {
                if (SpookyPlayer.pages > 0)
                {
                    music = GetSoundSlot(SoundType.Music, $"Sounds/Music/Slender/7thStreet/7thStreetStage{SpookyPlayer.pages}");
                    priority = MusicPriority.BossHigh;
                }
            }
            if ((PlayerIsInForest(player) || player.ZoneSnow || player.ZoneBeach || player.ZoneCorrupt || player.ZoneCrimson || PlayerUnderground(player) || player.ZoneDirtLayerHeight || player.ZoneUndergroundDesert || player.ZoneUnderworldHeight || player.ZoneSkyHeight || player.ZoneCorrupt || player.ZoneCrimson || player.ZoneHoly || player.ZoneJungle || (player.ZoneDesert && !player.ZoneBeach) || player.ZoneDungeon || player.ZoneGlowshroom || player.ZoneMeteor || player.ZoneRain || player.ZoneOldOneArmy || player.ZoneSandstorm) && !pageReqMetTier1 && !pageReqMetTier2 && !pageReqMetTier3 && !pageReqMetTier4)
            {
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Nothingness");
                priority = MusicPriority.BossMedium;
            }
        }
    }
}