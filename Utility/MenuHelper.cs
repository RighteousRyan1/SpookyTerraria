using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.RuntimeDetour.HookGen;
using System;
using System.Diagnostics;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI.Chat;
using ReLogic.Graphics;
using SpookyTerraria.IO;
using System.IO;
using Microsoft.Xna.Framework.Input;
using DiscordRPC;

namespace SpookyTerraria.Utilities
{
    // Thank you, Steviegt6
    public static class Hooks
    {
		public delegate void Orig_AddMenuButtons(Main main, int selectedMenu, string[] buttonNames, float[] buttonScales, ref int offY, ref int spacing, ref int buttonIndex, ref int numButtons);

		public delegate void Hook_AddMenuButtons(Orig_AddMenuButtons orig, Main main, int selectedMenu, string[] buttonNames, float[] buttonScales, ref int offY, ref int spacing, ref int buttonIndex, ref int numButtons);

		public static event Hook_AddMenuButtons On_AddMenuButtons
		{
			add
			{
				HookEndpointManager.Add<Hook_AddMenuButtons>(typeof(Mod).Assembly.GetType("Terraria.ModLoader.UI.Interface").GetMethod("AddMenuButtons", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic), value);
			}
			remove
			{
				HookEndpointManager.Remove<Hook_AddMenuButtons>(typeof(Mod).Assembly.GetType("Terraria.ModLoader.UI.Interface").GetMethod("AddMenuButtons", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic), value);
			}
		}
		public delegate void Orig_ModLoaderMenus(Main main, int selectedMenu, string[] buttonNames, float[] buttonScales, int[] buttonVerticalSpacing, ref int offY, ref int spacing, ref int numButtons, ref bool backButtonDown);

		public delegate void Hook_ModLoaderMenus(Orig_ModLoaderMenus orig, Main main, int selectedMenu, string[] buttonNames, float[] buttonScales, int[] buttonVerticalSpacing, ref int offY, ref int spacing, ref int numButtons, ref bool backButtonDown);

		public static event Hook_ModLoaderMenus On_ModLoaderMenus
		{
			add
			{
				HookEndpointManager.Add<Hook_ModLoaderMenus>(typeof(Mod).Assembly.GetType("Terraria.ModLoader.UI.Interface").GetMethod("ModLoaderMenus", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic), value);
			}
			remove
			{
				HookEndpointManager.Remove<Hook_ModLoaderMenus>(typeof(Mod).Assembly.GetType("Terraria.ModLoader.UI.Interface").GetMethod("ModLoaderMenus", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic), value);
			}
		}
	}


	public static class MenuHelper
	{
        /// <summary>
        /// Safely draws a texture without accessing a disposed object or null object.
        /// <para>Reminder to call Begin() and End() for drawing this.</para>
        /// </summary>
        public static void SafeDraw(this SpriteBatch spriteBatch, Texture2D tex, Rectangle rect, Color color)
        {
            if (tex != null && !tex.IsDisposed)
            {
                spriteBatch.Draw(tex, rect, color);
            }
        }
        public static void SafeDraw(this SpriteBatch spriteBatch, Texture2D tex, Vector2 pos, Color color)
        {
            if (tex != null && !tex.IsDisposed)
            {
                spriteBatch.Draw(tex, pos, color);
            }
        }
        public static void SafeDraw(this SpriteBatch spriteBatch, Texture2D tex, Rectangle rect, Rectangle? sourceRect, Color color)
        {
            if (tex != null && !tex.IsDisposed)
            {
                spriteBatch.Draw(tex, rect, sourceRect, color);
            }
        }
        public static void SafeDraw(this SpriteBatch spriteBatch, Texture2D tex, Vector2 pos, Rectangle? sourceRect, Color color)
        {
            if (tex != null && !tex.IsDisposed)
            {
                spriteBatch.Draw(tex, pos, sourceRect, color);
            }
        }
        public static void SafeDraw(this SpriteBatch spriteBatch, Texture2D tex, Rectangle rect, Rectangle? sourceRect, Color color, float rot, Vector2 orig, float scale, SpriteEffects effects)
        {
            if (tex != null && !tex.IsDisposed)
            {
                spriteBatch.Draw(tex, rect, sourceRect, color, rot, orig, effects, 1f);
            }
        }
        public static void SafeDraw(this SpriteBatch spriteBatch, Texture2D tex, Vector2 pos, Rectangle? sourceRect, Color color, float rot, Vector2 orig, float scale, SpriteEffects effects)
        {
            if (tex != null && !tex.IsDisposed)
            {
                spriteBatch.Draw(tex, pos, sourceRect, color, rot, orig, scale, effects, 1f);
            }
        }
        public static void SafeDraw(this SpriteBatch spriteBatch, Texture2D tex, Vector2 pos, Rectangle? sourceRect, Color color, float rot, Vector2 orig, Vector2 scale, SpriteEffects effects)
        {
            if (tex != null && !tex.IsDisposed)
            {
                spriteBatch.Draw(tex, pos, sourceRect, color, rot, orig, scale, effects, 1f);
            }
        }
        public static void DrawUserStatistics()
        {
            AccountHandler.GetUserID().ToString();
        }
		public static void AddButton(string text, Action act, int selectedMenu, string[] buttonNames, ref int buttonIndex, ref int numButtons)
		{
			buttonNames[buttonIndex] = text;

			if (selectedMenu == buttonIndex)
			{
				Main.PlaySound(SoundID.MenuOpen);
				act();
			}

			buttonIndex++;
			numButtons++;
		}
        public static string msgOfTheDay;
        internal static float buttonScaleReturn;
        internal static float buttonScaleDownloadMap;
        internal static float buttonScaleDownloadSlender;
        internal static float buttonScaleMythos;
        internal static float buttonScaleChangeLogs;
        internal static float buttonScaleModes;

        internal static float returnFromModesScale;
        internal static float twentyDollarsScale;
        internal static float glowsticksScale;

        internal static Vector2 shakeOffset;
        internal static int shakeTimer;

        private static string changeLog;
        public static void DrawSlenderMenuUI()
        {
            bool isRyan = AccountHandler.accountName.ToString() == "158490005";
            shakeTimer--;
            Mod mod = ModContent.GetInstance<SpookyTerraria>();

            int yOff = 42;
            int offsetMore = 40;

            string @return = "Back";
            string downloadMap = "Download Slender Terraria Map";
            string downloadSlender = "Download Slender: The 8 Pages";
            string visitMythos = "Slender Mythos";
            string changeLogs = "Spooky Terraria Changelogs";
            string HTP = "How to Play";

            Vector2 midScreen = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
            Vector2 topMid = new Vector2(Main.screenWidth / 2, 12);

            if (shakeTimer >= 0)
            {
                shakeOffset = new Vector2(Main.rand.Next(-3, 3), Main.rand.Next(-3, 3));
            }
            else
            {
                shakeOffset = Vector2.Zero;
            }
            // TODO: How to play, finish BadOSVersion
            if (Main.menuMode == MenuModeID.SlenderExtras)
            {
                Rectangle mapDL = ModContent.GetInstance<SpookyTerraria>().UIHelper.CreateSimpleUIButton(Main.fontDeathText, midScreen + new Vector2(0, 150) - new Vector2(0, yOff * 4 + offsetMore), downloadMap, 
                    delegate 
                    {
                        byte[] modFileData = mod.GetFileBytes("SlenderWorld/Slender_The_8_Pages.wld");

                        string pathTo = $"{Path.Combine(Main.WorldPath, "Slender_The_8_Pages.wld")}";
                        if (!File.Exists(Path.Combine(Main.WorldPath, "Slender_The_8_Pages.wld")))
                        {
                            File.WriteAllBytes(pathTo, modFileData);
                            SpookyTerraria.fileAddedNoticeTimer = 90;
                        }
                    }, 
                    ref buttonScaleDownloadMap);

                Rectangle htp = ModContent.GetInstance<SpookyTerraria>().UIHelper.CreateSimpleUIButton(Main.fontDeathText, midScreen + new Vector2(0, 150) - new Vector2(0, yOff * 7), HTP,
                    delegate 
                    { 
                        Main.menuMode = MenuModeID.HowToPlay; 
                    }, 
                    ref HTPButtonScale);

                Rectangle slenderDownload = ModContent.GetInstance<SpookyTerraria>().UIHelper.CreateSimpleUIButton(Main.fontDeathText, midScreen + new Vector2(0, 150) - new Vector2(0, yOff * 3 + offsetMore), downloadSlender, 
                    delegate { Process.Start("https://cdn.discordapp.com/attachments/427893900594774026/796565609852698644/Slender_v0.9.7.rar"); }, ref buttonScaleDownloadSlender);

                Rectangle mythos = ModContent.GetInstance<SpookyTerraria>().UIHelper.CreateSimpleUIButton(Main.fontDeathText, midScreen + new Vector2(0, 150) - new Vector2(0, yOff * 2 + offsetMore), visitMythos, 
                    delegate { Process.Start("https://theslenderman.fandom.com/wiki/Original_Mythos"); }, ref buttonScaleMythos);

                Rectangle returnButton = ModContent.GetInstance<SpookyTerraria>().UIHelper.CreateSimpleUIButton(Main.fontDeathText, midScreen + new Vector2(0, 150) - new Vector2(0, offsetMore), @return, 
                    delegate { Main.menuMode = MenuModeID.MainMenu; }, ref buttonScaleReturn, default, 0.015f, null, default, 1, true);

                Rectangle logs = ModContent.GetInstance<SpookyTerraria>().UIHelper.CreateSimpleUIButton(Main.fontDeathText, midScreen + new Vector2(0, 150) - new Vector2(0, yOff + offsetMore), changeLogs, 
                    delegate { Main.menuMode = MenuModeID.SlenderChangeLogs; }, ref buttonScaleChangeLogs);

                Rectangle modes = ModContent.GetInstance<SpookyTerraria>().UIHelper.CreateSimpleUIButton(Main.fontDeathText, midScreen + new Vector2(0, 150) - new Vector2(0, yOff * 5 + offsetMore) + shakeOffset, !isRyan ? "Slender Modes Menu (Coming Soon)" : "Slender Modes Menu",
                    delegate 
                    { 
                        if (!isRyan)
                        {
                            Main.PlaySound(SoundID.Unlock);
                            shakeTimer = 15;
                        }
                        else
                        {
                            Main.PlaySound(SoundID.CoinPickup);
                            Main.menuMode = MenuModeID.SlenderModesMenu;
                        } 
                    }, 
                    ref buttonScaleModes, !isRyan ? Color.Red : default, 0.015f, null, !isRyan ? Color.DarkRed : default, 1f, true);

                if (mapDL.Contains(Main.MouseScreen.ToPoint()))
                {
                    ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontDeathText, "Download the Official Slender Map for Terraria!", topMid, Color.White, 0f, Main.fontDeathText.MeasureString("Download the Official Slender Map for Terraria!") / 2, new Vector2(0.3f, 0.3f));
                }
                if (slenderDownload.Contains(Main.MouseScreen.ToPoint()))
                {
                    ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontDeathText, "Download the 'Slender: The 8 Pages' Official Game!", topMid, Color.White, 0f, Main.fontDeathText.MeasureString("Download the 'Slender: The 8 Pages' Official Game!") / 2, new Vector2(0.3f, 0.3f));
                }
                if (mythos.Contains(Main.MouseScreen.ToPoint()))
                {
                    ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontDeathText, "Check out the Slender Mythology that the\n       game and mod were based on!", topMid + new Vector2(20, 12), Color.White, 0f, Main.fontDeathText.MeasureString("Check out the Slender Mythology that the\n       game and mod were based on!") / 2, new Vector2(0.3f, 0.3f));
                }
                if (logs.Contains(Main.MouseScreen.ToPoint()))
                {
                    ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontDeathText, "Check out the Change Logs for this Mod!", topMid, Color.White, 0f, Main.fontDeathText.MeasureString("Check out the Change Logs for this Mod!") / 2, new Vector2(0.3f, 0.3f));
                }
                if (htp.Contains(Main.MouseScreen.ToPoint()))
                {
                    ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontDeathText, "See how to play the Mod.", topMid, Color.White, 0f, Main.fontDeathText.MeasureString("See how to play the Mod.") / 2, new Vector2(0.3f, 0.3f));
                }
                if (modes.Contains(Main.MouseScreen.ToPoint()))
                {
                    if (isRyan)
                    {
                        ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontDeathText, "Welcome, tester!", topMid, Color.White, 0f, Main.fontDeathText.MeasureString("Welcome, tester!") / 2, new Vector2(0.3f, 0.3f));
                    }
                    else
                    {
                        ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontDeathText, "WIP: Come back some other time.", topMid, Color.White, 0f, Main.fontDeathText.MeasureString("WIP: Come back some other time.") / 2, new Vector2(0.3f, 0.3f));
                    }
                }
            }
            if (Main.menuMode == MenuModeID.SlenderModesMenu)
            {
                Rectangle backButtonFromModes =
                ModContent.GetInstance<SpookyTerraria>().UIHelper.CreateSimpleUIButton(Main.fontDeathText, EaseOfAccess.Screen / 2 - new Vector2(0, yOff), "Back",
                delegate
                {
                    Main.menuMode = MenuModeID.SlenderExtras;
                },
                ref returnFromModesScale);

                Rectangle twentyDollarsMode =
                ModContent.GetInstance<SpookyTerraria>().UIHelper.CreateSimpleUIButton(Main.fontDeathText, EaseOfAccess.Screen / 2 - new Vector2(0, yOff * 2), SpookyTerraria.Gimme20Dollars ? "20 Dollars Mode: On" : "20 Dollars Mode: Off",
                delegate
                {
                    SpookyTerraria.Gimme20Dollars = !SpookyTerraria.Gimme20Dollars;
                },
                ref twentyDollarsScale);

                Rectangle gSticksMode =
                ModContent.GetInstance<SpookyTerraria>().UIHelper.CreateSimpleUIButton(Main.fontDeathText, EaseOfAccess.Screen / 2 - new Vector2(0, yOff * 3), SpookyTerraria.Glowsticks ? "Glowsticks: On" : "Glowsticks: Off",
                delegate
                {
                    SpookyTerraria.Glowsticks = !SpookyTerraria.Glowsticks;
                },
                ref glowsticksScale);
            }
        }
        public static void DrawChangeLogs()
        {
            Mod mod = ModContent.GetInstance<SpookyTerraria>();
            if (Main.menuMode == MenuModeID.SlenderChangeLogs)
            {
                Vector2 screenBounds = new Vector2(Main.screenWidth, Main.screenHeight);

                var keyState = Main.keyState;
                if (keyState.IsKeyDown(Keys.Escape))
                {
                    Main.menuMode = MenuModeID.SlenderExtras;
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
                    // ItemSlot.Draw // wtf
                    changeLog = "     Change Logs for [c/FFFF00:Spooky Terraria]: v[c/FFFF00:0.5.6]"
                        + "\n - Hopefully, a LONG LASTING and confirmed\nfix for unload issues on some devices.\nThis may have been an issue on devices with\nXNA disposing textures a little\ntoo early. Let me know if there are any more issues!"
                        + "\n - Competely re-overhauled a lot of bad code\n and opened possibility for mod-extensions.\n - Please report the issue to my discord server if this issue persists.\n - My discord server is on its way to 500 members!\nConsider joining and getting us to this amazing milestone!"
                        + "\n - Moved some UI around.";
                }
                if (SlenderMain.updateLogMode == 2)
                {
                    // ItemSlot.Draw // wtf
                    changeLog = "Change Logs for [c/FFFF00:Spooky Terraria]: v[c/FFFF00:0.5.6.1]\n - Added some tooltip messages to the menu.\n - Removed an unintended feature.\n - Added a sneak peek of sorts.";
                }
                if (SlenderMain.updateLogMode == 3)
                {
                    changeLog = "Change Logs for [c/FFFF00:Spooky Terraria]: v[c/FFFF00:0.6]\n - Main Addition: Discord Rich Presence!\nYour friends on Discord can now see you playing this mod and see your stats."
                        + "\nThings such as: Pages Collected, Player name, player status, menu status, and more."
                        + "\n - Repositioned a lot of UI, including removing the logo from a lot of menus."
                        + "\nFixed some code inconsistencies.\n - A FOR SURE fix to mod unloading.\n - Added a How to Play menu.";
                }
                if (SlenderMain.updateLogMode == 4)
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

                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontDeathText, escToReturn, botMid - new Vector2(0, 15), Color.Gray, 0f, midEscText, new Vector2(0.3f, 0.3f));
                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontDeathText, reccommend, botMid, Color.Gray, 0f, recMid, new Vector2(0.3f, 0.3f));
                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontDeathText, changeLog, new Vector2(screenBounds.X / 2, 50), Color.White, 0f, new Vector2(log055Dimensions.X / 2, 0), new Vector2(scale, scale));

                Main.spriteBatch.Draw(buttonPlay, topMid + new Vector2(50, -15), null, Color.White, 0f, buttonPlay.Size() / 2, 1f, SpriteEffects.None, 1f);
                Main.spriteBatch.Draw(buttonPlay, topMid - new Vector2(50, 15), null, Color.White, 0f, buttonPlay.Size() / 2, 1f, SpriteEffects.FlipHorizontally, 1f);

                Rectangle pageRight = new Rectangle((int)topMid.X + 40, 23, buttonPlay.Width, buttonPlay.Height);
                Rectangle pageLeft = new Rectangle((int)topMid.X - 60, 23, buttonPlay.Width, buttonPlay.Height);

                bool containR = pageRight.Contains(Main.MouseScreen.ToPoint());
                bool containL = pageLeft.Contains(Main.MouseScreen.ToPoint());
                if (containR)
                {
                    Main.spriteBatch.Draw(buttonPlay, topMid + new Vector2(50, -15), null, Color.White, 0f, buttonPlay.Size() / 2, 1.25f, SpriteEffects.None, 1f);

                    if (Main.mouseLeft && Main.mouseLeftRelease)
                    {
                        SlenderMain.updateLogMode++;
                        Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Action/PagePickup"));
                    }
                }
                if (containL)
                {
                    Main.spriteBatch.Draw(buttonPlay, topMid - new Vector2(50, 15), null, Color.White, 0f, buttonPlay.Size() / 2, 1.25f, SpriteEffects.FlipHorizontally, 1f);

                    if (Main.mouseLeft && Main.mouseLeftRelease)
                    {
                        SlenderMain.updateLogMode--;
                        Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Action/PagePickup"));
                    }
                }
            }
            // CLAMP FOR LOGS TO BE UPDATED PROPERLY LMAO
            SlenderMain.updateLogMode = Utils.Clamp<short>(SlenderMain.updateLogMode, -1, 4);
        }
        internal static float HTPButtonScale;

        public static int HTPMenuSelected
        {
            get;
            internal set;
        }
        internal static float buttonScale1;
        internal static float buttonScale2;
        internal static float buttonScale3;
        internal static float buttonScale4;

        internal static float defUseScale = 0.85f;

        /// <summary>
        /// Draw the 'How to Play' menu.
        /// </summary>
        /// <param name="condition">Optional - when to draw </param>
        public static void DrawHTP()
        {
            defUseScale = 2f;
            string wte = "You can expect to die quite a few times before winning. The mod takes a while to master asAlso, inside of narrow places, your vision will be reduced in the official map.\nYou are supplied with a flashlight, if you don't use it diligently, you can expect it to run out, or if you don't manage your stamina\nwell enough, you might die to being too slow.";

            string mainIdeas = "In Spooky Terraria, the whole idea is to collect all 8 pages in a world. Of course, you can generate a new world and move on with your day, but it is\nreccommended that you download the map and play it."
                + "You venture the world and you will (eventually!) find all 8 pages if you try hard enough.\nWhile doing this, you have to be weary, there is something chasing you!";

            string basicUnderstanding = "Slenderman has a vendetta against you, and he wants to kill you.\n"
                   + "As you collect pages, he gets faster. Looking at him will static your screen until it is completely opaque and kills you.\nOnce you collect all 8 pages, you win!\nSlenderman can teleport, and in Spooky Terraria, can fly."
                   + "\nBe sure to not get jumpscared.\nSome simple things you need to know are keeping your stamina high and monitoriing your battery usage. \nThe more you hold/use your flashlight, the more battery will be consumed. Along with that, a max of 5 batteries can be held at any one time.\nWith stamina, you are a sitting duck after using up to 75% of it at any given time.";
            if (HTPMenuSelected == 0) HTPMenuSelected = 1;
            int yOff = 42;

            string escToReturn = "Press ESC to return";

            Vector2 screenBounds = new Vector2(Main.screenWidth, Main.screenHeight);
            Vector2 botMid = new Vector2(screenBounds.X / 2, screenBounds.Y - 10);
            Vector2 midEscText = Main.fontDeathText.MeasureString(escToReturn) / 2;

            ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontDeathText, escToReturn, botMid - new Vector2(0, 0), Color.Gray, 0f, midEscText, new Vector2(0.3f, 0.3f));

            Vector2 midScreen = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
            Vector2 topMid = new Vector2(Main.screenWidth / 2, 12);

            Mod mod = ModContent.GetInstance<SpookyTerraria>();

            if (Main.keyState.IsKeyDown(Keys.Escape) && Main.menuMode == MenuModeID.HowToPlay)
            {
                Main.menuMode = MenuModeID.SlenderExtras;
            }

            UIHelper buttonHelper = ModContent.GetInstance<SpookyTerraria>().UIHelper;

            buttonHelper.CreateSimpleUIButton(Main.fontDeathText, midScreen + new Vector2(midScreen.X / 2, yOff), "The Basics",
                delegate
                {
                    HTPMenuSelected = 1;
                },
                ref HTPMenuSelected != 1 ? ref buttonScale1 : ref defUseScale, default, 0.015f, null, HTPMenuSelected == 1 ? Color.Yellow : default);
                buttonHelper.CreateSimpleUIButton(Main.fontDeathText, midScreen + new Vector2(midScreen.X / 2, 0), "What to Expect",
                delegate
                {
                    HTPMenuSelected = 2;
                },
                ref HTPMenuSelected != 2 ? ref buttonScale2 : ref defUseScale, default, 0.015f, null, HTPMenuSelected == 2 ? Color.Yellow : default);
                buttonHelper.CreateSimpleUIButton(Main.fontDeathText, midScreen + new Vector2(midScreen.X / 2, -yOff), "Main Ideas",
                delegate
                {
                    HTPMenuSelected = 3;
                },
                ref HTPMenuSelected != 3 ? ref buttonScale3 : ref defUseScale, default, 0.015f, null, HTPMenuSelected == 3 ? Color.Yellow : default);
            if (Main.screenHeight < 800 || Main.screenWidth < 950)
            {
                buttonHelper.CreateSimpleUIButton(Main.fontDeathText, midScreen + new Vector2(midScreen.X / 2, yOff * 2), "Send Text to File",
                delegate
                {
                    Main.PlaySound(SoundID.DoubleJump);
                    string path = $"C://Users//{Environment.UserName}//Desktop//how_to_play.txt";
                    File.WriteAllText(path,

                        $"(THIS WAS AUTO-GENERATED BY SPOOKY TERRARIA)\n\n<<< MAIN IDEAS >>>\n\n{mainIdeas}\n\n<<< WHAT TO EXPECT >>>\n\n{wte}\n\n<<< BASIC UNDERSTANDING >>>\n\n{basicUnderstanding}"

                        );
                },
                ref buttonScale4, default, 0.015f, null, default, 1f);
            }


            if (HTPMenuSelected == 1)
            {
                var howToPlay_avoidslender = mod.GetTexture("Assets/MenuUI/HowToPlay/avoidslender");
                Main.spriteBatch.SafeDraw(howToPlay_avoidslender, midScreen - new Vector2(midScreen.X / 2, 0), null, Color.White, 0f, howToPlay_avoidslender.Size() / 2, 1f, SpriteEffects.None);
                if (Main.screenHeight < 800 || Main.screenWidth < 950)
                {
                    ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontDeathText, "Screen too small!", midScreen - new Vector2(midScreen.X / 2, 300), Color.White, 0f, Vector2.Zero, UIHelper.SingleToVec2(.35f));
                }
                else
                {
                    ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontDeathText, basicUnderstanding, new Vector2(midScreen.X / 2 - 250, 100), Color.White, 0f, Vector2.Zero, UIHelper.SingleToVec2(.35f));
                }
            }
            if (HTPMenuSelected == 2)
            {
                var howtoplay_death = mod.GetTexture("Assets/MenuUI/HowToPlay/death");
                Main.spriteBatch.SafeDraw(howtoplay_death, midScreen - new Vector2(midScreen.X / 2, 0), null, Color.White, 0f, howtoplay_death.Size() / 2, 1f, SpriteEffects.None);
                if (Main.screenHeight < 800 || Main.screenWidth < 950)
                {
                    ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontDeathText, "Screen too small!", midScreen - new Vector2(midScreen.X / 2, 300), Color.White, 0f, Vector2.Zero, UIHelper.SingleToVec2(.35f));
                }
                else
                {
                    ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontDeathText, wte, new Vector2(midScreen.X / 2 - 250, 100), Color.White, 0f, Vector2.Zero, UIHelper.SingleToVec2(.35f));
                }
            }
            if (HTPMenuSelected == 3)
            {
                var howtoplay_pickuppage = mod.GetTexture("Assets/MenuUI/HowToPlay/pickuppage");
                Main.spriteBatch.SafeDraw(howtoplay_pickuppage, midScreen - new Vector2(midScreen.X / 2, 0), null, Color.White, 0f, howtoplay_pickuppage.Size() / 2, 1f, SpriteEffects.None);
                if (Main.screenHeight < 800 || Main.screenWidth < 950)
                {
                    ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontDeathText, "Screen too small!", midScreen - new Vector2(midScreen.X / 2, 300), Color.White, 0f, Vector2.Zero, UIHelper.SingleToVec2(.35f));
                }
                else
                {
                    ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontDeathText, mainIdeas, new Vector2(midScreen.X / 2 - 250, 100), Color.White, 0f, Vector2.Zero, UIHelper.SingleToVec2(.35f));
                }
            }
        }
        public static void DrawSocials()
        {
            Mod mod = ModContent.GetInstance<SpookyTerraria>();
            SpriteEffects none = SpriteEffects.None;

            int y = 40;
            int y2 = 80;

            float txtScale = 0.25f;

            string myMedia = $"My Socials:";
            string display4Twitch = "<- My Twitch Channel!";
            string display4YT = "<- My YouTube Channel!";
            string display4Discord = "<- My Discord Server!";

            Texture2D Twitch = mod.GetTexture("Assets/OtherAssets/TwitchLogo");
            Texture2D YouTube = mod.GetTexture("Assets/OtherAssets/YouTubeLogo");
            Texture2D Discord = mod.GetTexture("Assets/OtherAssets/DiscordLogo");

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
                    Main.spriteBatch.DrawString(Main.fontDeathText, MenuHelper.msgOfTheDay, new Vector2(Main.screenWidth / 2, Main.screenHeight - 25), Color.White, 0f, Main.fontDeathText.MeasureString(MenuHelper.msgOfTheDay) / 2, 1f, none, 1f);
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
            catch (Exception e)
            {
                mod.Logger.Warn($"{0} was not able to draw correctly!\nException caught! Message: {e.Message} StackTrace: {e.StackTrace}");
            }
        }
	}
}