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
using System.Windows.Forms;
using System.IO;
using Steamworks;
using SpookyTerraria.IO;

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
                SpookyPlayer.Pages = type2;
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
        public static bool Gimme20Dollars;
        public static bool Glowsticks;





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
        public static int volRaisedNotifTimer;
        public override void PostSetupContent()
        {
            if (Main.soundVolume == 0f)
            {
                volRaisedNotifTimer = 90;
                Main.soundVolume = 0.5f;
            }
            if (Main.musicVolume == 0f)
            {
                volRaisedNotifTimer = 90;
                Main.musicVolume = 0.25f;
            }
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

        public static bool BadOSVersion
        {
            get
            {
                return Environment.OSVersion.ToString() == Environment.OSVersion.Version.ToString();
            }
        }
        public override void Load()
        {
            Logger.Info(Environment.OSVersion.ToString());
            ContentInstance.Register(new UIHelper());
            ContentInstance.Register(new DiscordRPCInfo());
            DiscordRPCInfo.Load();
            blackPixel = GetTexture("Assets/BlackPixel");
            slenderLogo = GetTexture("Assets/Slender");
            chad = GetTexture("Assets/THE_CHAD");
            storedLogo = Main.logoTexture;
            storedLogo2 = Main.logo2Texture;
            Main.logoTexture = GetTexture("Assets/Invisible");
            Main.logo2Texture = GetTexture("Assets/Invisible");

            MenuHelper.msgOfTheDay = ChooseRandomMessage(rand);
            rand = Main.rand.Next(0, 20);
            tick = Main.soundMenuTick;
            open = Main.soundMenuOpen;
            close = Main.soundMenuClose;

            Main.soundMenuTick = GetSound("Sounds/Custom/Other/Nothingness");
            Main.soundMenuOpen = GetSound("Sounds/Custom/Other/Nothingness");
            Main.soundMenuClose = GetSound("Sounds/Custom/Other/Nothingness");
            Main.versionNumber = $"Terraria {Main.versionNumber}\n{Name} v{Version}";

            Sprint = RegisterHotKey("Sprint", "LeftShift");

            Hooks.On_AddMenuButtons += Hooks_On_AddMenuButtons;
            Hooks.On_ModLoaderMenus += Hooks_On_ModLoaderMenus;
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
            Main.OnTick += Main_OnTick;
            On.Terraria.Main.DrawInterface_35_YouDied += nothing => { };

            On.Terraria.IngameOptions.DrawLeftSide += IngameOptions_DrawLeftSide;
            On.Terraria.IngameOptions.Draw += IngameOptions_Draw;

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
            /*ContentInstance.Register(new AmbienceHelper());
            ModContent.GetInstance<AmbienceHelper>().InitializeSoundInstances();*/
        }

        private void Main_OnTick()
        {
            if (DiscordRPCInfo.rpcClient != null && !DiscordRPCInfo.rpcClient.IsDisposed) DiscordRPCInfo.Update();
        }

        private void IngameOptions_Draw(On.Terraria.IngameOptions.orig_Draw orig, Main mainInstance, SpriteBatch sb)
        {
            /*int num17 = 20;
            Vector2 value4 = new Vector2(Main.screenWidth, Main.screenHeight);
            // Vector2 value2 = new Vector2(670f, 480f);
            Vector2 value2 = new Vector2(670f, 250f);
            Vector2 value3 = value4 / 2f - value2 / 2f;
            int num21 = 1;
            int num22 = 5 + num21 + 2 + 1;
            var vector = new Vector2(value3.X + value2.X / 4f, value3.Y + (num17 * 5 / 2));
            Vector2 vector2 = new Vector2(0f, value2.Y - (num17 * 5)) / (num22 + 1);
            int index = 8;
            bool click = Main.mouseLeft && Main.mouseLeftRelease;*/

            orig(mainInstance, sb);
            /*
            if (IngameOptions.DrawLeftSide(sb, "Slender Options", index, vector, vector2, IngameOptions.leftScale))
            {
                IngameOptions.leftHover = index;
                if (click)
                {
                    IngameFancyUI.CoverNextFrame();

                    Main.playerInventory = false;
                    Main.editChest = false;
                    Main.npcChatText = "";
                    Main.inFancyUI = true;


                    Main.InGameUI.SetState(SlenderMenuModeID.UIStates.SlenderIngameInterface);
                }
            }
            if (Main.InGameUI.CurrentState == SlenderMenuModeID.UIStates.SlenderIngameInterface)
            {

            }*/
        }

        private bool IngameOptions_DrawLeftSide(On.Terraria.IngameOptions.orig_DrawLeftSide orig, SpriteBatch sb, string txt, int i, Vector2 anchor, Vector2 offset, float[] scales, float minscale, float maxscale, float scalespeed)
        {
            /*
            if (i > 4 && i < 8)
            {
                return orig(sb, txt, i, anchor, offset + new Vector2(0, 5), scales, minscale, maxscale, scalespeed);
            }*/
            /*else*/ return orig(sb, txt, i, anchor, offset, scales, minscale, maxscale, scalespeed);
        }

        private void Hooks_On_ModLoaderMenus(Hooks.Orig_ModLoaderMenus orig, Main main, int selectedMenu, string[] buttonNames, float[] buttonScales, int[] buttonVerticalSpacing, ref int offY, ref int spacing, ref int numButtons, ref bool backButtonDown)
        {
            orig(main, selectedMenu, buttonNames, buttonScales, buttonVerticalSpacing, ref offY, ref spacing, ref numButtons, ref backButtonDown);
            for (int i = 0; i < buttonVerticalSpacing.Length; i++)
            {
                
            }
            buttonNames[6] += " (PLEASE DO NOT DO)"; // idk man
        }   
        private void Main_DrawInterface_30_Hotbar(On.Terraria.Main.orig_DrawInterface_30_Hotbar orig, Main self)
        {
            orig(self);
        }

        public override void Unload()
        {
            DiscordRPCInfo.Terminate();
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
            /*for (int texIteration = 0; texIteration < _texturesDisposable.Length - 1; texIteration++)
            {
                if (_texturesDisposable[texIteration] != null)
                {
                    lock (_texturesDisposable[texIteration])
                    {
                        _texturesDisposable[texIteration] = null;
                    }
                }
            }*/
            Sprint = null;
        }
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
                    SpookyPlayer.Pages = 0;
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
                    Main.menuMode = MenuModeID.MainMenu;
                }
            }
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
                        Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
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
            return $"Spooky Terraria: {MenuHelper.msgOfTheDay}";
        }

        private void Hooks_On_AddMenuButtons(Hooks.Orig_AddMenuButtons orig, Main main, int selectedMenu, string[] buttonNames, float[] buttonScales, ref int offY, ref int spacing, ref int buttonIndex, ref int numButtons)
        {
            orig(main, selectedMenu, buttonNames, buttonScales, ref offY, ref spacing, ref buttonIndex, ref numButtons);
            // spacing = (int)(Math.Sin(scaleTimer_BasedOnSineWave) * 15);
            // offY = (int)(Math.Sin(scaleTimer_BasedOnSineWave) * 100) + 545;
            offY = Main.screenHeight > 800 ? 500 : 250;
            spacing = 35;

            MenuHelper.AddButton("Slender Extras",
            delegate
            {
                Main.menuMode = MenuModeID.SlenderExtras;
            },
            selectedMenu, buttonNames, ref buttonIndex, ref numButtons);
        }

        public virtual string ChooseRandomMessage(int type)
        {
            var steamID = SteamUser.GetSteamID();
            switch (type)
            {
                default:
                    return "Beware the man himself!";
                case 1:
                    return "Are you spooked yet?";
                case 2:
                    return $"Enjoy the mod, {SteamFriends.GetFriendPersonaName(steamID)}.";
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
                    return "Does victory await?";
                case 17:
                    return "Will you win? Only time will tell.";
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
                Main.spriteBatch.SafeDraw(blackPixel, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, new Vector2(Main.screenWidth, Main.screenHeight), SpriteEffects.None);
                Main.spriteBatch.SafeDraw(chad, new Vector2(150, Main.screenHeight - 30), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None);
                if (Main.menuMode == 0 && Main.screenHeight > 800)
                {
                    Main.spriteBatch.SafeDraw(slenderLogo, new Vector2(Main.screenWidth / 2, 220), null, Color.White, sinValueRot / 50, new Vector2(slenderLogo.Width / 2, slenderLogo.Height / 2), 1f + (sinValueScale / 80), SpriteEffects.None);
                }
            }
            else
            {
                orig(self);
            }
        }

        public static Texture2D blackPixel;
        public static Texture2D chad;
        public static Texture2D slenderLogo;

        private static Texture2D[] _texturesDisposable = new Texture2D[]
        {
            blackPixel,
            chad,
            slenderLogo
        };

        public UIHelper UIHelper
        {
            get
            {
                return ModContent.GetInstance<UIHelper>();
            }
        }
        public DiscordRPCInfo DiscordInfo
        {
            get
            {
                return ModContent.GetInstance<DiscordRPCInfo>();
            }
        }
        public static int fileAddedNoticeTimer;
        private void Main_DrawMenu(On.Terraria.Main.orig_DrawMenu orig, Main self, GameTime gameTime)
        {
            ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontDeathText, $"{DiscordInfo.userName}", new Vector2(Main.screenWidth / 2, Main.screenHeight - 8), Color.LightGray, 0f, Main.fontDeathText.MeasureString($"Added a world to your worlds!") / 2, new Vector2(0.275f, 0.275f));
            UIHelper.ClickHandling();
            if (Main.menuMode == MenuModeID.HowToPlay)
            {
                MenuHelper.DrawHTP();
            }
            fileAddedNoticeTimer--;
            if (fileAddedNoticeTimer >= 0)
            {
                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontDeathText, $"Added a world to your worlds!", new Vector2(Main.screenWidth / 2, Main.screenHeight - 8), Color.LightGray, 0f, Main.fontDeathText.MeasureString($"Added a world to your worlds!") / 2, new Vector2(0.275f, 0.275f));
            }
            // UIHelper.CreateSimpleUIButton(Main.fontDeathText, new Vector2(300, 100), $"{Environment.OSVersion.Platform} {Environment.OSVersion.Version}", null, ref scaleTimer_BasedOnSineWave);
            if (Main.menuMode == 0) ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontDeathText, $"Authenticated as: {SteamUser.GetSteamID().GetAccountID()} ({SteamFriends.GetPersonaName()})", new Vector2(6,6), Color.LightGray, 0f, Vector2.Zero, new Vector2(0.275f, 0.275f));
            MenuHelper.DrawUserStatistics();
            AmbienceHandler.StopAllAmbientSounds();
            MenuHelper.DrawSlenderMenuUI();
            MenuHelper.DrawChangeLogs();
            
            if (volRaisedNotifTimer >= 0)
            {
                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontDeathText, "Volume Adjusted Automatically.", new Vector2(Main.screenWidth / 2, Main.screenHeight - 10), Color.Gray * volRaisedNotifTimer, 0f, Main.fontDeathText.MeasureString("Volume Adjusted Automatically.") / 2, new Vector2(0.3f, 0.3f));
            }
            if (Main.menuMode == MenuModeID.SlenderExtras)
            {
                MenuHelper.DrawSocials();
            }
            volRaisedNotifTimer--;
            SpookyTerrariaUtils.HandleKeyboardInputs();
            SpookyTerrariaUtils.oldKeyboardState = SpookyTerrariaUtils.newKeyboardState;
            UIHelper.MSOld = UIHelper.MSNew;
            orig(self, gameTime);
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
                if (ModContent.GetInstance<SpookyTerraria>() != null)
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
            MenuHelper.msgOfTheDay = ChooseRandomMessage(rand);
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
            for (int texIteration = 0; texIteration < _texturesDisposable.Length - 1; texIteration++)
            {
                if (_texturesDisposable[texIteration] != null)
                {
                    lock (_texturesDisposable[texIteration])
                    {
                        _texturesDisposable[texIteration] = null;
                    }
                }
            }
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
            bool pageReqMetTier1 = SpookyPlayer.Pages >= 1 && SpookyPlayer.Pages < 3;
            bool pageReqMetTier2 = SpookyPlayer.Pages >= 3 && SpookyPlayer.Pages < 5;
            bool pageReqMetTier3 = SpookyPlayer.Pages >= 5 && SpookyPlayer.Pages < 7;
            bool pageReqMetTier4 = SpookyPlayer.Pages >= 7 && SpookyPlayer.Pages < 8;

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
                if (SpookyPlayer.Pages > 0)
                {
                    music = GetSoundSlot(SoundType.Music, $"Sounds/Music/Slender/7thStreet/7thStreetStage{SpookyPlayer.Pages}");
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