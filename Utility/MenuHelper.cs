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

        private static string changeLog;
        public static void DrawSlenderMenuUI505()
        {
            Mod mod = ModContent.GetInstance<SpookyTerraria>();

            int yOff = 60;

            string @return = "Back";
            string downloadMap = "Download Slender Terraria Map";
            string downloadSlender = "Download Slender: The 8 Pages";
            string visitMythos = "Slender Mythos";
            string changeLogs = "Spooky Terraria Changelogs";

            Vector2 midScreen = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);

            if (Main.menuMode == SlenderMenuModeID.SlenderExtras)
            {
                ModContent.GetInstance<SpookyTerraria>().UIHelper.CreateSimpleUIButton(Main.fontDeathText, midScreen - new Vector2(0, yOff * 4), downloadMap, 
                    delegate { Process.Start("https://cdn.discordapp.com/attachments/743141999846096959/796195488143245333/Slender_The_8_Pages.wld"); }, ref buttonScaleDownloadMap);
                ModContent.GetInstance<SpookyTerraria>().UIHelper.CreateSimpleUIButton(Main.fontDeathText, midScreen - new Vector2(0, yOff * 3), downloadSlender, 
                    delegate { Process.Start("https://cdn.discordapp.com/attachments/427893900594774026/796565609852698644/Slender_v0.9.7.rar"); }, ref buttonScaleDownloadSlender);
                ModContent.GetInstance<SpookyTerraria>().UIHelper.CreateSimpleUIButton(Main.fontDeathText, midScreen - new Vector2(0, yOff * 2), visitMythos, 
                    delegate { Process.Start("https://theslenderman.fandom.com/wiki/Original_Mythos"); }, ref buttonScaleMythos);
                ModContent.GetInstance<SpookyTerraria>().UIHelper.CreateSimpleUIButton(Main.fontDeathText, midScreen, @return, 
                    delegate { Main.menuMode = 0; }, ref buttonScaleReturn);
                ModContent.GetInstance<SpookyTerraria>().UIHelper.CreateSimpleUIButton(Main.fontDeathText, midScreen - new Vector2(0, yOff), changeLogs, 
                    delegate { Main.menuMode = SlenderMenuModeID.SlenderChangeLogs; }, ref buttonScaleChangeLogs);
            }
        }
        public static void DrawChangeLogs()
        {
            Mod mod = ModContent.GetInstance<SpookyTerraria>();
            if (Main.menuMode == SlenderMenuModeID.SlenderChangeLogs)
            {
                Vector2 screenBounds = new Vector2(Main.screenWidth, Main.screenHeight);

                var keyState = Main.keyState;
                if (keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
                {
                    Main.menuMode = SlenderMenuModeID.SlenderExtras;
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
                        + "\n - Hopefully, a LONG LASTING and confirmed\nfix for unload issues on some devices.\nThis may have been an issue on devices with\nXNA disposing textures a little\ntoo early. Let me know if there are any more issues!";
                }
                if (SlenderMain.updateLogMode == 2)
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
            SlenderMain.updateLogMode = Utils.Clamp<short>(SlenderMain.updateLogMode, -1, 2);
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