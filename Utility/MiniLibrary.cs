using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Graphics;
using Steamworks;
using System;
using System.Collections.Generic;
using System.IO;
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
    public class MenuModeID
    {
        public const int MainMenu = 0;
        /// <summary>
        /// The Extras (the menu to all the other menu modes)
        /// </summary>
        public const short SlenderExtras = 505;
        /// <summary>
        /// The Spooky Terraria changelogs
        /// </summary>
        public const short SlenderChangeLogs = 1010;
        /// <summary>
        /// The modes for the game (20 dollars, glowsticks)
        /// </summary>
        public const short SlenderModesMenu = 1515;
        /// <summary>
        /// The How to Play Menu
        /// </summary>
        public const short HowToPlay = 1551;
        public struct UIStates
        {
            public static UIState SlenderIngameInterface { get; set; }
        }
    }
    public class UIHelper
    {
        public static Vector2 SingleToVec2(float value)
        {
            return new Vector2(value, value);
        }
        public static MouseState MSNew
        {
            get;
            internal set;
        }
        public static MouseState MSOld
        {
            get;
            internal set;
        }
        public static void ClickHandling()
        {
            MSNew = Mouse.GetState();
        }
        public static bool ClickEnded(bool leftClick)
        {
            if (leftClick)
            {
                return MSOld.LeftButton == ButtonState.Pressed && MSNew.LeftButton == ButtonState.Released;
            }
            else
            {
                return MSOld.RightButton == ButtonState.Pressed && MSNew.RightButton == ButtonState.Released;
            }
        }
        /// <summary>
        /// Allows the easiest any most simple creation of a menu button ever! Font being the <paramref name="font"/> you want to use,
        /// <paramref name="position"/> being the position of the text (centered)
        /// <para>
        /// <paramref name="text"/> being what texty you want to show, <paramref name="whatToDo"/> being all actions you want to happen when the button is clicked
        /// </para>
        /// <para>
        /// And <paramref name="useScale"/> being the scalar you want to use. Please used a once instanced float for <paramref name="useScale"/>.
        /// </para>
        /// </summary>
        /// <returns>A rectangle of the button (hovering, etc)</returns>
        public Rectangle CreateSimpleUIButton(DynamicSpriteFont font,
            Vector2 position,
            string text,
            Action whatToDo,
            ref float useScale, 
            Color colorWhenHovered = default, 
            float scaleMultiplier = 0.015f, 
            Action hoveringToDo = null,
            Color nonHoverColor = default,
            float alpha = 1f,
            bool waitUntilClickEnd = false)
        {
            // Mod mod = ModContent.GetInstance<SpookyTerraria>();
            if (colorWhenHovered == default)
            {
                colorWhenHovered = Color.Yellow;
            }
            if (nonHoverColor == default)
            {
                nonHoverColor = new Color(86, 86, 86, 50);
            }
            useScale = MathHelper.Clamp(useScale, 0.65f, 0.85f);

            var bounds = font.MeasureString(text);
            var rectHoverable = new Rectangle((int)position.X - (int)(bounds.X / 2 * useScale), (int)(position.Y - bounds.Y / 2 + 10), (int)(bounds.X * useScale), (int)bounds.Y - 30);

            bool hovering = rectHoverable.Contains(Main.MouseScreen.ToPoint());
            ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, font, text, position, hovering ? colorWhenHovered * alpha : nonHoverColor * alpha, 0f, bounds / 2, new Vector2(useScale));
            // Main.spriteBatch.Draw(mod.GetTexture("Assets/Debug/WhitePixel"), rectHoverable, Color.White * 0.1f);
            if (!waitUntilClickEnd)
            {
                if (Main.mouseLeft && Main.mouseLeftRelease && hovering && whatToDo != null)
                {
                    whatToDo();
                }
            }
            else
            {
                if (ClickEnded(true) && hovering && whatToDo != null)
                {
                    whatToDo();
                }
            }
            if (hoveringToDo != null)
            {
                if (hovering)
                {
                    hoveringToDo();
                }
            }
            useScale += hovering ? scaleMultiplier : -scaleMultiplier;
            return rectHoverable;
        }
        public Rectangle CreateUIButton(SpriteBatch sb, 
            Texture2D texture, 
            Vector2 position, 
            Action whatToDo, 
            Color color, 
            float scale, 
            SpriteEffects effects, 
            Rectangle? sourceRectangle = null)
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
        public static Rectangle ScreenBounds
        {
            get
            {
                return new Rectangle(Main.instance.Window.ClientBounds.X, Main.instance.Window.ClientBounds.Y, Main.screenWidth, Main.screenHeight);
            }
        }
        public static Vector2 Screen
        {
            get
            {
                return new Vector2(Main.screenWidth, Main.screenHeight);
            }
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