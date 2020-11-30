using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpookyTerraria.ModIntances;
using SpookyTerraria.NPCs;
using SpookyTerraria.OtherItems;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using static SpookyTerraria.SoundPlayer;
using ReLogic.Graphics;

namespace SpookyTerraria.Utilities
{
    public class Lists_SpookyNPCs
    {
        /// <summary>
        /// List of all 'Spooky' NPCs
        /// </summary>
        public static List<int> SpookyNPCs = new List<int>
        {
            ModContent.NPCType<Stalker>()
        };
    }
    public static class EntityHelper
    {
        /// <summary>
        /// Completely zeroes the velocity of the projectile.
        /// </summary>
        /// <param name="projectile"></param>
        public static void HaltVelocity(this Entity entity)
        {
            entity.velocity.X = 0;
            entity.velocity.Y = 0;
        }
        public static bool CheckGround(this Entity entity, bool checkOnGround = true)
        {
            if (checkOnGround)
            {
                if (entity.velocity.Y == 0)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool CheckGround(this Player player, bool checkOnGround = true)
        {
            if (checkOnGround)
            {
                if (player.velocity.Y == 0 && !player.mount.Active)
                {
                    return true;
                }
            }
            return false;
        }
    }
    public class SpookyTerrariaUtils
    {
        /// <summary>
        /// No real use; Don't use unless fucking around
        /// </summary>
        /// <param name="toAdd"></param>
        /// <param name="toSubtract"></param>
        public static void Square(int toAdd, int toSubtract)
        {

            WorldGen.KillTile((int)Main.MouseWorld.X / 16, (int)Main.MouseWorld.Y / 16);

            WorldGen.KillTile(((int)Main.MouseWorld.X / 16) + toAdd, ((int)Main.MouseWorld.Y / 16) + toAdd);

            WorldGen.KillTile(((int)Main.MouseWorld.X / 16), ((int)Main.MouseWorld.Y / 16) + toAdd);

            WorldGen.KillTile(((int)Main.MouseWorld.X / 16) + toAdd, ((int)Main.MouseWorld.Y / 16));

            WorldGen.KillTile(((int)Main.MouseWorld.X / 16) - toSubtract, ((int)Main.MouseWorld.Y / 16) - toSubtract);

            WorldGen.KillTile(((int)Main.MouseWorld.X / 16) - toSubtract, ((int)Main.MouseWorld.Y / 16));

            WorldGen.KillTile(((int)Main.MouseWorld.X / 16), ((int)Main.MouseWorld.Y / 16) - toSubtract);

            WorldGen.KillTile(((int)Main.MouseWorld.X / 16) - toSubtract, ((int)Main.MouseWorld.Y / 16) + toAdd);

            WorldGen.KillTile(((int)Main.MouseWorld.X / 16) + toAdd, ((int)Main.MouseWorld.Y / 16) - toSubtract);



            WorldGen.KillWall((int)Main.MouseWorld.X / 16, (int)Main.MouseWorld.Y / 16);

            WorldGen.KillWall(((int)Main.MouseWorld.X / 16) + toAdd, ((int)Main.MouseWorld.Y / 16) + toAdd);

            WorldGen.KillWall(((int)Main.MouseWorld.X / 16), ((int)Main.MouseWorld.Y / 16) + toAdd);

            WorldGen.KillWall(((int)Main.MouseWorld.X / 16) + toAdd, ((int)Main.MouseWorld.Y / 16));

            WorldGen.KillWall(((int)Main.MouseWorld.X / 16) - toSubtract, ((int)Main.MouseWorld.Y / 16) - toSubtract);

            WorldGen.KillWall(((int)Main.MouseWorld.X / 16) - toSubtract, ((int)Main.MouseWorld.Y / 16));

            WorldGen.KillWall(((int)Main.MouseWorld.X / 16), ((int)Main.MouseWorld.Y / 16) - toSubtract);

            WorldGen.KillWall(((int)Main.MouseWorld.X / 16) - toSubtract, ((int)Main.MouseWorld.Y / 16) + toAdd);

            WorldGen.KillWall(((int)Main.MouseWorld.X / 16) + toAdd, ((int)Main.MouseWorld.Y / 16) - toSubtract);
        }
        /// <summary>
        /// Cap a value at a max or a min.
        /// </summary>
        /// <param name="valueToBeCapped"></param>
        /// <param name="capValue"></param>
        /// <param name="checkLessThan"></param>
        public static void CapValue(int valueToBeCapped, int capValue, bool checkLessThan = false)
        {
            if (checkLessThan)
            {
                if (valueToBeCapped > capValue)
                {
                    valueToBeCapped = capValue;
                }
            }
            else if (!checkLessThan)
            {
                if (valueToBeCapped < capValue)
                {
                    valueToBeCapped = -capValue;
                }
            }
        }
        public static void CapValue(float valueToBeCapped, float capValue, bool checkLessThan = false)
        {
            if (checkLessThan)
            {
                if (valueToBeCapped > capValue)
                {
                    valueToBeCapped = capValue;
                }
            }
            else if (!checkLessThan)
            {
                if (valueToBeCapped < capValue)
                {
                    valueToBeCapped = -capValue;
                }
            }
        }
        public static void CapValue(double valueToBeCapped, double capValue, bool checkLessThan = false)
        {
            if (checkLessThan)
            {
                if (valueToBeCapped > capValue)
                {
                    valueToBeCapped = capValue;
                }
            }
            else if (!checkLessThan)
            {
                if (valueToBeCapped < capValue)
                {
                    valueToBeCapped = -capValue;
                }
            }
        }
        public static void CapValue(decimal valueToBeCapped, decimal capValue, bool checkLessThan = false)
        {
            if (checkLessThan)
            {
                if (valueToBeCapped > capValue)
                {
                    valueToBeCapped = capValue;
                }
            }
            else if (!checkLessThan)
            {
                if (valueToBeCapped < capValue)
                {
                    valueToBeCapped = -capValue;
                }
            }
        }
        public static void CapValue(long valueToBeCapped, long capValue, bool checkLessThan = false)
        {
            if (checkLessThan)
            {
                if (valueToBeCapped > capValue)
                {
                    valueToBeCapped = capValue;
                }
            }
            else if (!checkLessThan)
            {
                if (valueToBeCapped < capValue)
                {
                    valueToBeCapped = -capValue;
                }
            }
        }
        /// <summary>
        /// Hardcoded value capping
        /// </summary>
        public static void DoCapping()
        {
            Player player = Main.player[Main.myPlayer];
        }
        public enum BodyFrames
        {
            stand,
            armUp,
            armDiagonalUp,
            armHoldOut,
            armDiagonalDown,
            jumping,
            walking1,
            walking2,
            walking3,
            walking4,
            walking5,
            walking6,
            walking7,
            walking8,
            walking9,
            walking10,
            walking11,
            walking12,
            walking13,
        }
        /// <summary>
        /// Set body frame, read documentation
        /// </summary>
        /// <param name="type"></param>
        public static void SetBodyFrame(int type)
        {
            Player player = Main.player[Main.myPlayer];
            switch (type)
            {
                case 0:
                    player.bodyFrame.Y = player.bodyFrame.Height * 0;
                    break;
                case 1:
                    player.bodyFrame.Y = player.bodyFrame.Height * 1;
                    break;
                case 2:
                    player.bodyFrame.Y = player.bodyFrame.Height * 2;
                    break;
                case 3:
                    player.bodyFrame.Y = player.bodyFrame.Height * 3;
                    break;
                case 4:
                    player.bodyFrame.Y = player.bodyFrame.Height * 4;
                    break;
                case 5:
                    player.bodyFrame.Y = player.bodyFrame.Height * 5;
                    break;
                case 6:
                    player.bodyFrame.Y = player.bodyFrame.Height * 6;
                    break;
                case 7:
                    player.bodyFrame.Y = player.bodyFrame.Height * 7;
                    break;
                case 8:
                    player.bodyFrame.Y = player.bodyFrame.Height * 8;
                    break;
                case 9:
                    player.bodyFrame.Y = player.bodyFrame.Height * 9;
                    break;
                case 10:
                    player.bodyFrame.Y = player.bodyFrame.Height * 10;
                    break;
                case 11:
                    player.bodyFrame.Y = player.bodyFrame.Height * 11;
                    break;
                case 12:
                    player.bodyFrame.Y = player.bodyFrame.Height * 12;
                    break;
                case 13:
                    player.bodyFrame.Y = player.bodyFrame.Height * 13;
                    break;
                case 14:
                    player.bodyFrame.Y = player.bodyFrame.Height * 14;
                    break;
                case 15:
                    player.bodyFrame.Y = player.bodyFrame.Height * 15;
                    break;
                case 16:
                    player.bodyFrame.Y = player.bodyFrame.Height * 16;
                    break;
                case 17:
                    player.bodyFrame.Y = player.bodyFrame.Height * 17;
                    break;
                case 18:
                    player.bodyFrame.Y = player.bodyFrame.Height * 18;
                    break;
                case 19:
                    player.bodyFrame.Y = player.bodyFrame.Height * 18;
                    break;
            }
        }
        /// <summary>
        /// To be worked on...
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public struct GiveInstance<T> where T : class
        {
            // Work later nerd
        }
        /// <summary>
        /// Play step sound. Modify the directory if need be
        /// </summary>
        /// <param name="pathGrass1"></param>
        /// <param name="pathGrass2"></param>
        /// <param name="pathStone1"></param>
        /// <param name="pathStone2"></param>
        /// <param name="pathWood1"></param>
        /// <param name="pathWood2"></param>
        public virtual void PlayStepSound(string pathGrass1 = "Sounds/Custom/Action/GrassStep1", string pathGrass2 = "Sounds/Custom/Action/GrassStep2",
            string pathStone1 = "Sounds/Custom/Action/StoneStep1", string pathStone2 = "Sounds/Custom/Action/StoneStep2", string pathWood1 = "Sounds/Custom/Action/WoodStep1",
            string pathWood2 = "Sounds/Custom/Action/WoodStep1")
        {
            Player player = Main.player[Main.myPlayer];
            Mod mod = ModLoader.GetMod("SpookyTerraria");
            if (!player.wet)
            {
                if (player.legFrame.Y == player.legFrame.Height * 9 && player.GetModPlayer<SpookyPlayer>().isOnGrassyTile) // Check for step timer
                {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, $"{pathGrass1}"), player.Bottom); // Play sound
                }
                if (player.legFrame.Y == player.legFrame.Height * 16 && player.GetModPlayer<SpookyPlayer>().isOnGrassyTile)
                {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, $"{pathGrass2}"), player.Bottom); // Play sound
                }
                if (player.legFrame.Y == player.legFrame.Height * 9 && player.GetModPlayer<SpookyPlayer>().isOnStoneTile) // Check for step timer
                {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, $"{pathStone1}"), player.Bottom); // Play sound
                }
                if (player.legFrame.Y == player.legFrame.Height * 16 && player.GetModPlayer<SpookyPlayer>().isOnStoneTile)
                {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, $"{pathStone2}"), player.Bottom); // Play sound
                }
                if (player.legFrame.Y == player.legFrame.Height * 9 && player.GetModPlayer<SpookyPlayer>().isOnWoodTile) // Check for step timer
                {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, $"{pathWood1}"), player.Bottom); // Play sound
                }
                if (player.legFrame.Y == player.legFrame.Height * 16 && player.GetModPlayer<SpookyPlayer>().isOnWoodTile)
                {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, $"{pathWood2}"), player.Bottom); // Play sound
                }
            }
        }
        /// <summary>
        /// Decreases stamina. the higher <paramref name="speedOfDecrease"/> is, the slower the decrease of stamina is.
        /// </summary>
        /// <param name="player">The player instance</param>
        /// <param name="speedOfDecrease"></param>
        public static void NegateStaminaValue(Player player, int speedOfDecrease)
        {
            player.GetModPlayer<StaminaPlayer>().staminaDecrementTimer = 0;
            player.GetModPlayer<StaminaPlayer>().staminaIncrementTimer++;
            if (player.GetModPlayer<StaminaPlayer>().staminaIncrementTimer >= speedOfDecrease)
            {
                player.GetModPlayer<StaminaPlayer>().Stamina--;
                player.GetModPlayer<StaminaPlayer>().staminaIncrementTimer = 0;
            }
        }
        /// <summary>
        /// Method for easy stamina negation
        /// </summary>
        /// <param name="player">The player instance</param>
        /// <param name="speedOfDecrease"></param>
        /// <param name="cond"></param>
        public static void NegateStaminaValue(Player player, int speedOfDecrease, bool cond)
        {
            if (cond)
            {
                player.GetModPlayer<StaminaPlayer>().staminaDecrementTimer = 0;
                player.GetModPlayer<StaminaPlayer>().staminaIncrementTimer++;
                if (player.GetModPlayer<StaminaPlayer>().staminaIncrementTimer >= speedOfDecrease)
                {
                    player.GetModPlayer<StaminaPlayer>().Stamina--;
                    player.GetModPlayer<StaminaPlayer>().staminaIncrementTimer = 0;
                }
            }
        }
        /// <summary>
        /// Completely resets all modified UI textures back to their original states. Only call during Close() or Unload()
        /// </summary>
        public static void ReturnTexturesToDefaults()
        {
            Main.heartTexture = Main.instance.OurLoad<Texture2D>("Images/" + "Heart");
            Main.heart2Texture = Main.instance.OurLoad<Texture2D>("Images/" + "Heart2");
            Main.manaTexture = Main.instance.OurLoad<Texture2D>("Images/" + "Mana");
            Main.inventoryBack2Texture = Main.instance.OurLoad<Texture2D>("Images/" + "Inventory_Back2");
            Main.inventoryBack3Texture = Main.instance.OurLoad<Texture2D>("Images/" + "Inventory_Back3");
            Main.inventoryBack4Texture = Main.instance.OurLoad<Texture2D>("Images/" + "Inventory_Back4");
            Main.inventoryBack5Texture = Main.instance.OurLoad<Texture2D>("Images/" + "Inventory_Back5");
            Main.inventoryBack6Texture = Main.instance.OurLoad<Texture2D>("Images/" + "Inventory_Back6");
            Main.inventoryBack7Texture = Main.instance.OurLoad<Texture2D>("Images/" + "Inventory_Back7");
            Main.inventoryBack8Texture = Main.instance.OurLoad<Texture2D>("Images/" + "Inventory_Back8");
            Main.inventoryBack9Texture = Main.instance.OurLoad<Texture2D>("Images/" + "Inventory_Back9");
            Main.inventoryBack10Texture = Main.instance.OurLoad<Texture2D>("Images/" + "Inventory_Back10");
            Main.inventoryBack11Texture = Main.instance.OurLoad<Texture2D>("Images/" + "Inventory_Back11");
            Main.inventoryBack12Texture = Main.instance.OurLoad<Texture2D>("Images/" + "Inventory_Back12");
            Main.inventoryBack13Texture = Main.instance.OurLoad<Texture2D>("Images/" + "Inventory_Back13");
            Main.inventoryBack14Texture = Main.instance.OurLoad<Texture2D>("Images/" + "Inventory_Back14");
            Main.inventoryBack15Texture = Main.instance.OurLoad<Texture2D>("Images/" + "Inventory_Back15");
            Main.inventoryBack16Texture = Main.instance.OurLoad<Texture2D>("Images/" + "Inventory_Back16");
            // Main.inventoryTickOffTexture = mod.GetTexture("Assets/Box");
            // Main.inventoryTickOffTexture = mod.GetTexture("Assets/Box");
            Main.inventoryBackTexture = Main.instance.OurLoad<Texture2D>("Images/" + "Inventory_Back");

            // Main.craft... texture backLoad
            Main.chatBackTexture = Main.instance.OurLoad<Texture2D>("Images/" + "Chat_Back");
        }
        /// <summary>
        /// The directory to the 'Unfinished' UI Element
        /// </summary>
        public static string unfinishedAssetPath = "Assets/Unfinished";
        /// <summary>
        /// Changes all vanilla textures to different ones
        /// </summary>
        public static void ModifyUITextures()
        {
            Mod mod = ModLoader.GetMod("SpookyTerraria");

            // TODO: Friendly Reminder; Finish the shits

            Main.heartTexture = mod.GetTexture("Assets/UI/Heart_Purple");
            Main.heart2Texture = mod.GetTexture("Assets/UI/Heart_Blue");
            Main.manaTexture = mod.GetTexture("Assets/UI/ManaStar_Green");

            Main.inventoryBack2Texture = mod.GetTexture("Assets/UI/Box"); // Good
            Main.inventoryBack3Texture = mod.GetTexture("Assets/UI/ArmorSlotUI");

            // Main.inventoryBack4Texture = mod.GetTexture("Assets/Box");
            Main.inventoryBack5Texture = mod.GetTexture("Assets/UI/ChestUI");

            Main.inventoryBack6Texture = mod.GetTexture("Assets/UI/ShopUI");
            Main.inventoryBack7Texture = mod.GetTexture("Assets/UI/TrashSlot");
            Main.inventoryBack8Texture = mod.GetTexture("Assets/UI/VanitySlotUI");
            Main.inventoryBack9Texture = mod.GetTexture("Assets/UI/BoxBrighter");
            Main.inventoryBack10Texture = mod.GetTexture("Assets/UI/BoxFavorite");
            // Main.inventoryBack11Texture = mod.GetTexture(unfinishedAssetPath);
            Main.inventoryBack12Texture = mod.GetTexture("Assets/UI/DyeSlotUI");

            // Main.inventoryBack13Texture = mod.GetTexture("Assets/UI/Box");
            Main.inventoryBack14Texture = mod.GetTexture("Assets/UI/BoxSel");

            Main.inventoryBack15Texture = mod.GetTexture("Assets/UI/BoxSel");

            // Main.inventoryBack16Texture = mod.GetTexture("Assets/UI/Box");
            // Main.inventoryTickOffTexture = mod.GetTexture("Assets/UI/Box");
            // Main.inventoryTickOffTexture = mod.GetTexture("Assets/UI/Box");

            Main.inventoryBackTexture = mod.GetTexture("Assets/UI/Box");

            // Main.craft... texture
            Main.chatBackTexture = mod.GetTexture("Assets/UI/ChatBox");
        }
        /// <summary>
        /// Caps run speeds at <paramref name="maxRunSpeed"/>'s value
        /// </summary>
        /// <param name="maxRunSpeed"></param>
        public static void HandleMaxRunSpeeds(int maxRunSpeed)
        {
            Player player = Main.LocalPlayer;
            if (player.velocity.X > maxRunSpeed && player.velocity.Y == 0 && !player.mount.Active && !player.wet)
            {
                player.velocity.X = maxRunSpeed;
            }
            if (player.velocity.X < -maxRunSpeed && player.velocity.Y == 0 && !player.mount.Active && !player.wet)
            {
                player.velocity.X = -maxRunSpeed;
            }
        }
        public static float blend;
        /// <summary>
        /// Draw UI for pages. Count is the number of pages to be displayed, with the remainder to 8 being leftover as "missing"
        /// </summary>
        /// <param name="numPages"></param>
        /// <param name="position"></param>
        public static void DrawPageUI(int numPages, float posX, float posY, bool condition = true)
        {
            Player player = Main.player[Main.myPlayer];
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            Mod mod = ModLoader.GetMod("SpookyTerraria");
            if (condition)
            {
                Color alphaBlendColor = Color.White * blend;

                int count = player.CountItem(ModContent.ItemType<Paper>(), 8);
                string pageUIDisplayString = $"{count} out of 8 pages";
                Rectangle pageUIPos = new Rectangle((int)posX, (int)posY, 175, 36);

                if (pageUIPos.Contains(Main.MouseScreen.ToPoint()))
                {
                    blend += 0.05f;
                    if (blend > 1f)
                    {
                        blend = 1f;
                    }
                    // For drawing a border, i guess
                    // Main.spriteBatch.DrawString(Main.fontMouseText, pageUIDisplayString, Main.MouseScreen + new Vector2(22, 23), Color.Black, 0f, new Vector2(0, 0), 1.1f, SpriteEffects.None, 0f);
                    Main.spriteBatch.DrawString(Main.fontMouseText, pageUIDisplayString, Main.MouseScreen + new Vector2(25, 25), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);
                }
                if (!pageUIPos.Contains(Main.MouseScreen.ToPoint()))
                {
                    blend -= 0.05f;
                    if (blend < 0.2f)
                    {
                        blend = 0.2f;
                    }
                }
                if (numPages == 0)
                {
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 20, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 40, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 60, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 80, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 100, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 120, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 140, posY), alphaBlendColor);
                }
                if (numPages == 1)
                {
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 20, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 40, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 60, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 80, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 100, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 120, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 140, posY), alphaBlendColor);
                }
                if (numPages == 2)
                {
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 20, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 40, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 60, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 80, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 100, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 120, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 140, posY), alphaBlendColor);
                }
                if (numPages == 3)
                {
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 20, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 40, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 60, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 80, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 100, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 120, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 140, posY), alphaBlendColor);
                }
                if (numPages == 4)
                {
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 20, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 40, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 60, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 80, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 100, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 120, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 140, posY), alphaBlendColor);
                }
                if (numPages == 5)
                {
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 20, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 40, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 60, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 80, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 100, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 120, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 140, posY), alphaBlendColor);
                }
                if (numPages == 6)
                {
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 20, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 40, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 60, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 80, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 100, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 120, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 140, posY), Color.White);
                }
                if (numPages == 7)
                {
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 20, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 40, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 60, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 80, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 100, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 120, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 140, posY), alphaBlendColor);
                }
                if (numPages == 8)
                {
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 20, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 40, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 60, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 80, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 100, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 120, posY), alphaBlendColor);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 140, posY), alphaBlendColor);
                }
                if (numPages > 8 || numPages < 0)
                {
                    throw new Exception($"value numPages ({numPages}) was not within the bounds of expected values!");
                }
                Main.spriteBatch.End();
            }
        }
    }
    public class SoundEngine
    {
        /// <summary>
        /// Pretty hardcoded as of now, but plays all needed ambience
        /// </summary>
        public static void Play()
        {
            Mod mod = ModLoader.GetMod("SpookyTerraria");
            Player player = Main.player[Main.myPlayer];
            /*SoundEffectInstance breezeSounds;
            breezeSounds = Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, $"Sounds/Custom/Ambience/Breezes"));
            SoundEffectInstance crickets;
            crickets = Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, $"Sounds/Custom/Ambience/Biome/ForestAmbience"));
            SoundEffectInstance waves;
            waves = Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, $"Sounds/Custom/Ambience/Biome/OceanAmbience"));
            SoundEffectInstance blizz;
            blizz = Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, $"Sounds/Custom/Ambience/Biome/SnowAmbience"));
            SoundEffectInstance cavesSound;
            cavesSound = Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, $"Sounds/Custom/Ambience/Biome/CaveRumble"));*/
            // Use SoundEffectInstance Eventually

            caveRumbleTimer++;
            cricketsTimer++;
            blizzTimer++;
            oceanWavesTimer++;

            // Main.NewText($"CaveRumble: {caveRumbleTimer}, OceanWaves: {oceanWavesTimer}, Crickets: {cricketsTimer}, Blizzard: {blizzTimer}");
            /*if (Main.gameMenu)
            {
                cavesSound.Stop();
                crickets.Stop();
                breezeSounds.Stop();
                blizz.Stop();
                waves.Stop();
            }*/

            if (player.ZoneRockLayerHeight) // Rock Layer
            {
                /*waves.Stop();
                crickets.Stop();
                breezeSounds.Stop();
                blizz.Stop();*/
                oceanWavesTimer = 0;
                cricketsTimer = 0;
                blizzTimer = 0;
                // Checked
            }
            if (player.ZoneBeach && !player.ZoneDirtLayerHeight && !player.ZoneRockLayerHeight) // Beach
            {
                /*crickets.Stop();
                breezeSounds.Stop();
                blizz.Stop();
                cavesSound.Stop();*/
                cricketsTimer = 0;
                caveRumbleTimer = 0;
                blizzTimer = 0;
                // Checked
            }
            if (player.ZoneDirtLayerHeight)
            {
                oceanWavesTimer = 0;
                caveRumbleTimer = 0;
            }
            if (player.ZoneSnow && !player.ZoneDirtLayerHeight && !player.ZoneRockLayerHeight) // Snow
            {
                /*waves.Stop();
                crickets.Stop();
                breezeSounds.Stop();
                cavesSound.Stop();*/
                oceanWavesTimer = 0;
                caveRumbleTimer = 0;
                cricketsTimer = 0;
                // checked
            }
            if (PlayerIsInForest(player)) // Forest
            {
                oceanWavesTimer = 0;
                caveRumbleTimer = 0;
                blizzTimer = 0;
                /// checked
            }
            if (caveRumbleTimer == 1680 && player.ZoneRockLayerHeight)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/Biome/CaveRumble"));
                caveRumbleTimer = 0;
            }
            if (caveRumbleTimer == 2 && (player.ZoneRockLayerHeight))
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/Biome/CaveRumble"));
            }
            if (oceanWavesTimer == 2 && !player.ZoneDirtLayerHeight && !player.ZoneRockLayerHeight)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/Biome/OceanAmbience"));
            }
            if (blizzTimer == 2 && !player.ZoneDirtLayerHeight && !player.ZoneRockLayerHeight)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/Biome/SnowAmbience"));
            }
            if (cricketsTimer == 2 && !player.ZoneDirtLayerHeight && !player.ZoneRockLayerHeight)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/Biome/ForestAmbience"));
            }
            if (player.ZoneBeach && oceanWavesTimer == 9060 && !player.ZoneDirtLayerHeight && !player.ZoneRockLayerHeight)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/Biome/OceanAmbience"));
                oceanWavesTimer = 0;
            }
            if (player.ZoneSnow && !player.ZoneDirtLayerHeight && !player.ZoneRockLayerHeight && blizzTimer == 6060)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/Biome/SnowAmbience"));
                blizzTimer = 0;
            }
            if (PlayerIsInForest(player) && cricketsTimer == 2700)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/Biome/ForestAmbience"));
                cricketsTimer = 0;
            }
        }
    }
}