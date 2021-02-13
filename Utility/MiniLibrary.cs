using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;

namespace SpookyTerraria.Utilities
{
    public class SlenderMain
    {
        public static short updateLogMode;
    }
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
    public class SlenderMenuModeID
    {
        public const short SlenderExtras = 505;
        public const short SlenderChangeLogs = 1010;

        public struct UIStates
        {
            public static UIState SlenderIngameInterface { get; set; }
        }
    }
    public class UIHelper
    {
        /// <summary>
        /// Allows the easiest any most simple creation of a menu button ever! Font being the <paramref name="font"/> you want to use,
        /// <paramref name="position"/> being the position of the text (centered)
        /// <para>
        /// <paramref name="text"/> being what texty you want to show, <paramref name="whatToDo"/> being all actions you want to happen when the button is clicked
        /// </para>
        /// <para>
        /// And <paramref name="scale"/> being the scalar you want to use. Please used a once instanced float for <paramref name="scale"/>.
        /// </para>
        /// </summary>
        /// <param name="font"></param>
        /// <param name="position"></param>
        /// <param name="text"></param>
        /// <param name="whatToDo"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public Rectangle CreateSimpleUIButton(DynamicSpriteFont font, Vector2 position, string text, Action whatToDo, ref float scale)
        {
            // Mod mod = ModContent.GetInstance<SpookyTerraria>();
            scale = MathHelper.Clamp(scale, 0.7f, 0.9f);

            var bounds = font.MeasureString(text);
            var rectHoverable = new Rectangle((int)position.X - (int)(bounds.X / 2 * scale), (int)position.Y - (int)bounds.Y / 2 + 5, (int)(bounds.X * scale), (int)bounds.Y - 30);

            bool hovering = rectHoverable.Contains(Main.MouseScreen.ToPoint());
            ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, font, text, position, hovering ? Color.Yellow : Color.Gray, 0f, bounds / 2, new Vector2(scale));
            // Main.spriteBatch.Draw(mod.GetTexture("Assets/Debug/WhitePixel"), rectHoverable, Color.White * 0.1f);
            if (Main.mouseLeft && Main.mouseLeftRelease && hovering)
            {
                whatToDo();
            }
            scale += hovering ? 0.015f : -0.015f;
            return rectHoverable;
        }
        public Rectangle CreateUIButton(SpriteBatch sb, Texture2D texture, Vector2 position, Action whatToDo, Color color, float scale, SpriteEffects effects, Rectangle? sourceRectangle = null)
        {
            Mod mod = ModContent.GetInstance<SpookyTerraria>();
            var bounds = texture.Size();
            var rectHoverable = new Rectangle((int)position.X - (int)bounds.X / 2, (int)position.Y - (int)bounds.Y / 2, (int)bounds.X, (int)bounds.Y);

            bool hovering = rectHoverable.Contains(Main.MouseScreen.ToPoint());
            sb.Draw(texture, position, sourceRectangle, color, 0f, bounds / 2, scale, effects, 1f);            
            if (Main.mouseLeft && Main.mouseLeftRelease && hovering)
            {
                whatToDo();
            }
            return rectHoverable;
        }
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
    public class EaseOfAccess
    {
        /// <summary>
        /// Detects if <code>iniRect</code> and <code>collidingRect</code> are intersecting.
        /// </summary>
        /// <param name="iniRect"></param>
        /// <param name="collidingRect"></param>
        /// <returns></returns>
        public static bool Intersecting(Rectangle iniRect, Rectangle collidingRect)
        {
            return iniRect.Intersects(collidingRect);
        }
        /// <summary>
        /// checks <code>hoveringOver.Contains(Main.MouseScreen.ToPoint())</code> which results in checking if the mouse position is on top of said rectangle.
        /// </summary>
        /// <param name="hoveringOver"></param>
        /// <returns></returns>
        public static bool IsMouseOver(Rectangle hoveringOver)
        {
            return hoveringOver.Contains(Main.MouseScreen.ToPoint());
        }
    }
}