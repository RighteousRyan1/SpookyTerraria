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
using Terraria.Utilities;
using System.Runtime.Caching;
using Microsoft.Xna.Framework.Audio;
using SpookyTerraria.Tiles;
using Microsoft.Xna.Framework.Input;

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
        public static Vector2 ToVector2(this Tile tile, int x, int y)
        {
            return new Vector2(x / 16, y / 16);
        }
        public static float GetRandomPoint(this Rectangle @this, bool forX, bool forY)
        {
            if (forX)
            {
                return Main.rand.NextFloat(0, @this.Height);
            }
            else if (forX)
            {
                return Main.rand.NextFloat(0, @this.Width);
            }
            if ((!forX && !forY) || (forX && forY))
            {
                throw new FormatException("Both forX and forY were set. Please set one of them to true.");
            }
            return 0f;
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
        public static KeyboardState newKeyboardState;
        public static KeyboardState oldKeyboardState;

        public static void HandleKeyboardInputs()
        {
            newKeyboardState = Keyboard.GetState();
        }
        public static bool KeyJustPressed(Keys pressed)
        {
            return newKeyboardState.IsKeyDown(pressed) && oldKeyboardState.IsKeyUp(pressed);
        }
        public static string ChooseRandomDeathText(int choice)
        {
            switch (choice)
            {
                case 0:
                    return "You were slaughtered...";
                case 1:
                    return "You have met your end...";
                case 2:
                    return "Your book has closed...";
                case 3:
                    return "An unfortunate end...";
                case 4:
                    return "A life that came to a close...";
                case 5:
                    return "You were slain...";
                case 6:
                    return "You are dead...";
                case 7:
                    return "Game over...";
                case 8:
                    return "You have died...";
                case 9:
                    return "Not again!";
                case 10:
                    return "Life is only temporary...";
                default:
                    return "Death is eternal.";
            }
        }
        public static Color ColorSwitcher(Color firstColor, Color secondColor, float transitionTime)
        {
            return Color.Lerp(firstColor, secondColor, (float)(Math.Sin(Main.GameUpdateCount / transitionTime) + 1f) / 2f);
        }
        public const int tileScaling = 16;
        public const int BGStyleID_DefaultStyle = -1;
        public enum BGStyleID
        {
            DefaultForest,
            Corruption,
            Desert, 
            Jungle,
            Beach,
            EvilDesert,
            HallowTrees,
            SnowTrees,
        }
        public const string slenderWorldName = "Slender: The 8 Pages";
        /// <summary>
        /// Removes all harcoded spawns
        /// </summary>
        public static void RemoveAllPossiblePagePositions()
        {
            float givenFloat = Main.rand.NextFloat();

            Tile treeRandPosTile1 = Framing.GetTileSafely(1161, 344); // Main.tile[1161,344]
            Tile treeRandPosTile2 = Framing.GetTileSafely(1137, 345); // Main.tile[1137,345]
            treeRandPosTile1.active(false);
            treeRandPosTile2.active(false);
            Tile xWallsPos1 = Framing.GetTileSafely(929, 323); // Main.tile[929,323]
            Tile xWallsPos2 = Framing.GetTileSafely(898, 317); // Main.tile[898,317]
            xWallsPos1.active(false);
            xWallsPos2.active(false);
            Tile tunnsPos1 = Framing.GetTileSafely(658, 302); // Main.tile[658,302]
            Tile tunnsPos2 = Framing.GetTileSafely(754, 302); // Main.tile[754,302]
            tunnsPos1.active(false);
            tunnsPos2.active(false);
            Tile tankersPos1 = Framing.GetTileSafely(179, 354); // Main.tile[179,354]
            Tile tankersPos2 = Framing.GetTileSafely(157, 353); // Main.tile[157,353]
            tankersPos1.active(false);
            tankersPos2.active(false);
            Tile siloPos1 = Framing.GetTileSafely(1507, 301); // Main.tile[1507, 301]
            Tile siloPos2 = Framing.GetTileSafely(1491, 300); // Main.tile[1491,300]
            siloPos1.active(false);
            siloPos2.active(false);
            Tile underGroundPos = Framing.GetTileSafely(1770, 298); // Main.tile[1770,298]
            Tile truckPos = Framing.GetTileSafely(1761, 273); // Main.tile[1761,273]
            underGroundPos.active(false);
            truckPos.active(false);
            Tile bRoomsPos1 = Framing.GetTileSafely(2133, 389); // Main.tile[2133,389]
            Tile bRoomsPos2 = Framing.GetTileSafely(2133, 379); // Main.tile[2133,379]
            Tile bRoomsPos3 = Framing.GetTileSafely(2133, 369); // Main.tile[2133,369]
            bRoomsPos1.active(false);
            bRoomsPos2.active(false);
            bRoomsPos3.active(false);
            Tile rocksPos = Framing.GetTileSafely(296, 340); // Main.tile[296,340]
            rocksPos.active(false);
        }
        /// <summary>
        /// Extremely hardcoded page generation spots, but still cool.
        /// </summary>
        public static void GenerateRandomPagePositions()
        {
            float givenFloat = Main.rand.NextFloat();
            if (SpookyPlayer.Pages == 0)
            {
                Tile treeRandPosTile1 = Framing.GetTileSafely(1161, 344); // Main.tile[1161,344]
                Tile treeRandPosTile2 = Framing.GetTileSafely(1137, 345); // Main.tile[1137,345]
                if (!treeRandPosTile1.active() && !treeRandPosTile2.active())
                {
                    if (Main.rand.NextFloat() <= 0.5f)
                    {
                        WorldGen.PlaceTile(1161, 344, ModContent.TileType<PageTileLeft>());
                    }
                    else
                    {
                        WorldGen.PlaceTile(1137, 345, ModContent.TileType<PageTileRight>());
                    }
                }
                Tile xWallsPos1 = Framing.GetTileSafely(929, 323); // Main.tile[929,323]
                Tile xWallsPos2 = Framing.GetTileSafely(898, 317); // Main.tile[898,317]
                if (!xWallsPos1.active() && !xWallsPos2.active())
                {
                    if (Main.rand.NextFloat() <= 0.5f)
                    {
                        WorldGen.PlaceTile(929, 323, ModContent.TileType<PageTileLeft>());
                    }
                    else
                    {
                        WorldGen.PlaceTile(898, 317, ModContent.TileType<PageTileRight>());
                    }
                    // Good
                }
                Tile tunnsPos1 = Framing.GetTileSafely(658, 302); // Main.tile[658,302]
                Tile tunnsPos2 = Framing.GetTileSafely(754, 302); // Main.tile[754,302]
                if (!tunnsPos1.active() && !tunnsPos2.active())
                {
                    if (Main.rand.NextFloat() <= 0.5f)
                    {
                        WorldGen.PlaceTile(658, 302, ModContent.TileType<PageTileWall>());
                    }
                    else
                    {
                        WorldGen.PlaceTile(754, 302, ModContent.TileType<PageTileWall>());
                    }
                    // Good
                }
                Tile tankersPos1 = Framing.GetTileSafely(179, 354); // Main.tile[179,354]
                Tile tankersPos2 = Framing.GetTileSafely(157, 353); // Main.tile[157,353]
                if (!tankersPos1.active() && !tankersPos2.active())
                {
                    if (Main.rand.NextFloat() <= 0.5f)
                    {
                        WorldGen.PlaceTile(179, 354, ModContent.TileType<PageTileRight>());
                    }
                    else
                    {
                        WorldGen.PlaceTile(157, 353, ModContent.TileType<PageTileRight>());
                    }
                    // Good
                }
                Tile siloPos1 = Framing.GetTileSafely(1507, 301); // Main.tile[1507, 301]
                Tile siloPos2 = Framing.GetTileSafely(1491, 300); // Main.tile[1491,300]
                if (!siloPos1.active() && !siloPos2.active())
                {
                    if (Main.rand.NextFloat() <= 0.5f)
                    {
                        WorldGen.PlaceTile(1507, 301, ModContent.TileType<PageTileLeft>());
                    }
                    else
                    {
                        WorldGen.PlaceTile(1491, 300, ModContent.TileType<PageTileRight>());
                    }
                    // Good
                }
                Tile underGroundPos = Framing.GetTileSafely(1770, 298); // Main.tile[1770,298]
                Tile truckPos = Framing.GetTileSafely(1761, 273); // Main.tile[1761,273]
                if (!underGroundPos.active() && !truckPos.active())
                {
                    if (Main.rand.NextFloat() <= 0.5f)
                    {
                        WorldGen.PlaceTile(1770, 298, ModContent.TileType<PageTileWall>());
                    }
                    else
                    {
                        WorldGen.PlaceTile(1761, 273, ModContent.TileType<PageTileWall>());
                    }
                    // Good
                }
                Tile bRoomsPos1 = Framing.GetTileSafely(2133, 389); // Main.tile[2133,389]
                Tile bRoomsPos2 = Framing.GetTileSafely(2133, 379); // Main.tile[2133,379]
                Tile bRoomsPos3 = Framing.GetTileSafely(2133, 369); // Main.tile[2133,369]
                if (!bRoomsPos1.active() && !bRoomsPos2.active() && !bRoomsPos3.active())
                {
                    if (givenFloat < 0.33f)
                    {
                        WorldGen.PlaceTile(2133, 389, ModContent.TileType<PageTileWall>());
                    }
                    else if (givenFloat >= 0.33f && givenFloat < 0.66f)
                    {
                        WorldGen.PlaceTile(2133, 379, ModContent.TileType<PageTileWall>());
                    }
                    else if (givenFloat >= 0.66f && givenFloat <= 1f)
                    {
                        WorldGen.PlaceTile(2133, 369, ModContent.TileType<PageTileWall>());
                    }
                }
                Tile rocksPos = Framing.GetTileSafely(296, 340); // Main.tile[296,340]
                if (!rocksPos.active())
                {
                    WorldGen.PlaceTile(296, 340, ModContent.TileType<PageTileWall>());
                }
            }
        }
        /// <summary>
        /// No real use; Don't use unless fucking around
        /// </summary>
        /// <param name="toAdd"></param>
        /// <param name="toSubtract"></param>
        public static void GenerateStar(int toAdd, int toSubtract)
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
            Mod mod = ModContent.GetInstance<SpookyTerraria>();
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
        public static string blackSquare = "Assets/BlackSquare";
        /// <summary>
        /// Changes all vanilla textures to different ones
        /// </summary>
        public static void ModifyUITextures()
        {
            Mod mod = ModContent.GetInstance<SpookyTerraria>();

            // TODO: Friendly Reminder; Finish the shits

            Main.heartTexture = mod.GetTexture("Assets/UI/NewHeart_Red");
            Main.heart2Texture = mod.GetTexture("Assets/UI/NewHeart_Yellow");
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
        public static void DrawPageUI(int numPages, float posX, float posY)
        {
            Player player = Main.player[Main.myPlayer];
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            Mod mod = ModContent.GetInstance<SpookyTerraria>();
            Color alphaBlendColor = Color.White * blend;

            int count = SpookyPlayer.Pages;
            string pageUIDisplayString = $"{count} out of 8 pages";
            Rectangle pageUIPos = new Rectangle((int)posX, (int)posY, 176, 36);

            if (pageUIPos.Contains(Main.MouseScreen.ToPoint()))
            {
                blend += 0.05f;
                if (blend > 1f)
                {
                    blend = 1f;
                }
                Main.spriteBatch.DrawString(Main.fontMouseText, pageUIDisplayString, Main.MouseScreen + new Vector2(25, 25), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);
            }

            int unfoundPages = MathUtils.FindRemainder(SpookyPlayer.Pages, 8);

            if (!pageUIPos.Contains(Main.MouseScreen.ToPoint()))
            {
                blend -= 0.05f;
                if (blend < 0.2f)
                {
                    blend = 0.2f;
                }
            }
            if (player.dead)
            {
                if (numPages == 0)
                {
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 20, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 40, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 60, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 80, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 100, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 120, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 140, posY), Color.White);
                }
                if (numPages == 1)
                {
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 20, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 40, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 60, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 80, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 100, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 120, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 140, posY), Color.White);
                }
                if (numPages == 2)
                {
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 20, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 40, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 60, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 80, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 100, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 120, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 140, posY), Color.White);
                }
                if (numPages == 3)
                {
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 20, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 40, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 60, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 80, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 100, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 120, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 140, posY), Color.White);
                }
                if (numPages == 4)
                {
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 20, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 40, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 60, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 80, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 100, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 120, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 140, posY), Color.White);
                }
                if (numPages == 5)
                {
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 20, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 40, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 60, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 80, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 100, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 120, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 140, posY), Color.White);
                }
                if (numPages == 6)
                {
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 20, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 40, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 60, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 80, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 100, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 120, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 140, posY), Color.White);
                }
                if (numPages == 7)
                {
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 20, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 40, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 60, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 80, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 100, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 120, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 140, posY), Color.White);
                }
                if (numPages == 8)
                {
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 20, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 40, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 60, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 80, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 100, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 120, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 140, posY), Color.White);
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 140, posY), Color.White);
                }
            }
            else if (!player.dead)
            {
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
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Missing"), new Vector2(posX + 140, posY), alphaBlendColor);
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
                    Main.spriteBatch.Draw(ModContent.GetTexture("SpookyTerraria/Assets/UniqueUI/PageUI_Collected"), new Vector2(posX + 140, posY), alphaBlendColor);
                }
            }
            Main.spriteBatch.End();
        }
        public const int maxDisplayTimes = 2;

        public static void DrawPageInterface(int pageIndex)
        {
            Mod mod = ModContent.GetInstance<SpookyTerraria>();

            Player player = Main.player[Main.myPlayer];

            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            Vector2 middleOfScreen = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
            Main.spriteBatch.Draw(mod.GetTexture($"Assets/UniqueUI/PageInterface_{pageIndex}"), middleOfScreen - new Vector2(250, 500), Color.White * (player.GetModPlayer<SpookyPlayer>().pageDisplayTimer / 100f));
            Main.spriteBatch.End();
        }
        public static void DrawPageInterface_PostCallEnd(int pageIndex)
        {
            Mod mod = ModContent.GetInstance<SpookyTerraria>();

            Player player = Main.player[Main.myPlayer];

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            Vector2 middleOfScreen = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
            Main.spriteBatch.Draw(mod.GetTexture($"Assets/UniqueUI/PageInterface_{pageIndex}"), middleOfScreen - new Vector2(250, 250), Color.White * (player.GetModPlayer<SpookyPlayer>().pageDisplayTimer / 100f));
            Main.spriteBatch.End();
        }
    }
    public class AmbienceHandler
    {
        public static void ReplayAt2Container()
        {
            Mod mod = ModContent.GetInstance<SpookyTerraria>();
            Player player = Main.player[Main.myPlayer];

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
            if (dayAmbienceTimer == 2 && Main.dayTime && PlayerIsInForest(player))
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/Biome/ForestAmbience_Day"));
            }
            if (jungleAmbTimer == 2 && player.ZoneJungle && (player.ZoneDirtLayerHeight || player.ZoneOverworldHeight))
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/Biome/JungleAmbience"));
            }
        }
        /// <summary>
        /// Pretty hardcoded as of now, but plays all needed ambience
        /// </summary>
        public static void Play()
        {
            ReplayAt2Container();

            Mod mod = ModContent.GetInstance<SpookyTerraria>();
            Player player = Main.player[Main.myPlayer];

            if (caveRumbleTimer == 1680 && player.ZoneRockLayerHeight)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/Biome/CaveRumble"));
                caveRumbleTimer = 0;
            }
            string unwantedWorldName = "Slender: The 8 Pages";
            if (Main.worldName != unwantedWorldName || Main.worldName != unwantedWorldName.ToUpper() || Main.worldName != unwantedWorldName.ToLower())
            {
                if (player.ZoneBeach && oceanWavesTimer == 9060 && !player.ZoneDirtLayerHeight && !player.ZoneRockLayerHeight)
                {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/Biome/OceanAmbience"));
                    oceanWavesTimer = 0;
                }
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
            if (jungleAmbTimer == 9780 && player.ZoneJungle && player.ZoneOverworldHeight)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/Biome/JungleAmbience"));
                jungleAmbTimer = 0;
            }
            if (dayAmbienceTimer == 4620 && PlayerIsInForest(player) && Main.dayTime && SpookyPlayer.Pages >= 8)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Ambient/Biome/ForestAmbience_Day"));
                dayAmbienceTimer = 0;
            }
        }
        /// <summary>
        /// Stop the SoundEffectInstance of <paramref name="path"/>.
        /// </summary>
        /// <param name="path"></param>
        public static void StopAmbientSound(string path)
        {
            Player p = Main.player[Main.myPlayer];
            Mod mod = ModContent.GetInstance<SpookyTerraria>();
            SoundEffectInstance pathInstance = Main.PlaySound(SoundLoader.customSoundType, (int)p.position.X, (int)p.position.Y, mod.GetSoundSlot(SoundType.Custom, $"{path}"));
            pathInstance?.Stop();
        }
        /// <summary>
        /// Modify the <paramref name="pan"/>, <paramref name="pitch"/>, and <paramref name="volume"/> of <paramref name="path"/>.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="volume"></param>
        /// <param name="pitch"></param>
        /// <param name="pan"></param>
        public static void ModifyAmbientAttribute(string path, float volume = 1f, float pitch = 1f, float pan = 1f)
        {
            Player p = Main.player[Main.myPlayer];
            Mod mod = ModContent.GetInstance<SpookyTerraria>();
            SoundEffectInstance pathInstance = Main.PlaySound(SoundLoader.customSoundType, (int)p.position.X, (int)p.position.Y, mod.GetSoundSlot(SoundType.Custom, $"{path}"));
            if (pathInstance != null)
            {
                pathInstance.Pan = pan;
                pathInstance.Pitch = pitch;
                pathInstance.Volume = volume;
            }
        }
        /// <summary>
        /// Use instance of SoundEffectInstance instead of indirectly. Automatically clamps the SoundEffectInstance's Pitch and Volume values between 0 and 1.
        /// </summary>
        /// <param name="path">The path to the sound effect.</param>
        public static SoundEffectInstance GetSoundEffect(string path)
        {
            Player p = Main.player[Main.myPlayer];
            Mod mod = ModContent.GetInstance<SpookyTerraria>();
            SoundEffectInstance instance = Main.PlaySound(SoundLoader.customSoundType, (int)p.position.X, (int)p.position.Y, mod.GetSoundSlot(SoundType.Custom, $"{path}"));

            if (instance.Volume > 1f)
            {
                instance.Volume = 1f;
            }
            if (instance.Volume < 0f)
            {
                instance.Volume = 0f;
            }

            if (instance.Pitch > 1f)
            {
                instance.Pitch = 1f;
            }
            if (instance.Pitch < 0f)
            {
                instance.Pitch = 0f;
            }
            return instance;
        }
        /// <summary>
        /// Stops ALL active ambient sounds.
        /// </summary>
        public static void StopAllAmbientSounds()
        {
            Player p = Main.player[Main.myPlayer];
            Mod mod = ModContent.GetInstance<SpookyTerraria>();
            SoundEffectInstance a = Main.PlaySound(SoundLoader.customSoundType, (int)p.position.X, (int)p.position.Y, mod.GetSoundSlot(SoundType.Custom, $"Sounds/Custom/Ambient/Biome/ForestAmbience"));
            SoundEffectInstance b = Main.PlaySound(SoundLoader.customSoundType, (int)p.position.X, (int)p.position.Y, mod.GetSoundSlot(SoundType.Custom, $"Sounds/Custom/Ambient/Biome/OceanAmbience"));
            SoundEffectInstance c = Main.PlaySound(SoundLoader.customSoundType, (int)p.position.X, (int)p.position.Y, mod.GetSoundSlot(SoundType.Custom, $"Sounds/Custom/Ambient/Biome/SnowAmbience"));
            SoundEffectInstance d = Main.PlaySound(SoundLoader.customSoundType, (int)p.position.X, (int)p.position.Y, mod.GetSoundSlot(SoundType.Custom, $"Sounds/Custom/Ambient/Biome/CaveRumble"));
            SoundEffectInstance e = Main.PlaySound(SoundLoader.customSoundType, (int)p.position.X, (int)p.position.Y, mod.GetSoundSlot(SoundType.Custom, $"Sounds/Custom/Ambient/Breezes"));
            SoundEffectInstance f = Main.PlaySound(SoundLoader.customSoundType, (int)p.position.X, (int)p.position.Y, mod.GetSoundSlot(SoundType.Custom, $"Sounds/Custom/Ambient/Biome/ForestAmbience_Day"));
            SoundEffectInstance g = Main.PlaySound(SoundLoader.customSoundType, (int)p.position.X, (int)p.position.Y, mod.GetSoundSlot(SoundType.Custom, $"Sounds/Custom/Ambient/Biome/JungleAmbience"));
            a?.Stop();
            b?.Stop();
            c?.Stop();
            d?.Stop();
            e?.Stop();
            f?.Stop();
            g?.Stop();
        }
        /// <summary>
        /// Stops all directory 
        /// </summary>
        /// <param name="paths"></param>
        public static void StopSoundList(string[] paths)
        {
            Player p = Main.player[Main.myPlayer];
            Mod mod = ModContent.GetInstance<SpookyTerraria>();
            foreach (string path in paths)
            {
                SoundEffectInstance instances = Main.PlaySound(SoundLoader.customSoundType, (int)p.position.X, (int)p.position.Y, mod.GetSoundSlot(SoundType.Custom, $"{path}"));
                instances?.Stop();
            }
        }

        public const string cricketsSoundDir = "Sounds/Custom/Ambient/Biome/ForestAmbience";
        public const string blizzardSoundDir = "Sounds/Custom/Ambient/Biome/SnowAmbience";
        public const string wavesSoundDir = "Sounds/Custom/Ambient/Biome/OceanAmbience";
        public const string rumblesSoundDir = "Sounds/Custom/Ambient/Biome/CaveRumble";
        public const string breezeSoundDir = "Sounds/Custom/Ambient/Breezes";
        public const string dayAmbienceDir = "Sounds/Custom/Ambient/Biome/ForestAmbience_Day";
        public const string jungleAmbienceDir = "Sounds/Custom/Ambient/Biome/JungleAmbience";

        /// <summary>
        /// Handle sound instancing, such as stopping sounds, etc.
        /// </summary>
        public static void HandleSoundInstancing()
        {
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
                // GetSoundEffect(breezeSoundDir).Volume--;
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
                ModifyAmbientAttribute(breezeSoundDir, 0.4f, 0.2f, 0.98f);
                oceanWavesTimer = 0;
                caveRumbleTimer = 0;
                cricketsTimer = 0;
                jungleAmbTimer = 0;
                dayAmbienceTimer = 0;
                // checked
            }
            if (PlayerIsInForest(player) && !Main.dayTime && !player.ZoneRockLayerHeight) // Forest
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
            if (PlayerIsInForest(player) && Main.dayTime && !player.ZoneRockLayerHeight) // Forest
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
            // Main.NewText($"CaveRumble: {caveRumbleTimer}, OceanWaves: {oceanWavesTimer}, Crickets: {cricketsTimer}, Blizzard: {blizzTimer}, DayAmb: {dayAmbienceTimer}, JungleAmb: {jungleAmbTimer}");
        }
    }
}