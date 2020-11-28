using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpookyTerraria.NPCs;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using static SpookyTerraria.SoundPlayer;

namespace SpookyTerraria.ModIntances
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
    public class SpookyTerrariaUtils
    {
        // Ignore this
        //public static void HaltVelocity(this Projectile projectile)
        //{

        //}
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

            Main.heartTexture = mod.GetTexture("Assets/Heart_Purple");
            Main.heart2Texture = mod.GetTexture("Assets/Heart_Blue");
            Main.manaTexture = mod.GetTexture("Assets/ManaStar_Green");

            Main.inventoryBack2Texture = mod.GetTexture("Assets/Box"); // Good
            Main.inventoryBack3Texture = mod.GetTexture("Assets/ArmorSlotUI");

            // Main.inventoryBack4Texture = mod.GetTexture("Assets/Box");
            // Main.inventoryBack5Texture = mod.GetTexture("Assets/Box");

            Main.inventoryBack6Texture = mod.GetTexture("Assets/ShopUI");
            Main.inventoryBack7Texture = mod.GetTexture("Assets/TrashSlot");
            Main.inventoryBack8Texture = mod.GetTexture("Assets/VanitySlotUI");
            Main.inventoryBack9Texture = mod.GetTexture("Assets/BoxBrighter");
            Main.inventoryBack10Texture = mod.GetTexture("Assets/BoxFavorite");
            // Main.inventoryBack11Texture = mod.GetTexture(unfinishedAssetPath);
            Main.inventoryBack12Texture = mod.GetTexture("Assets/DyeSlotUI");

            // Main.inventoryBack13Texture = mod.GetTexture("Assets/Box");
            Main.inventoryBack14Texture = mod.GetTexture("Assets/BoxSel");

            Main.inventoryBack15Texture = mod.GetTexture("Assets/BoxSel");

            // Main.inventoryBack16Texture = mod.GetTexture("Assets/Box");
            // Main.inventoryTickOffTexture = mod.GetTexture("Assets/Box");
            // Main.inventoryTickOffTexture = mod.GetTexture("Assets/Box");

            Main.inventoryBackTexture = mod.GetTexture("Assets/Box");

            // Main.craft... texture
            Main.chatBackTexture = mod.GetTexture("Assets/ChatBox");
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