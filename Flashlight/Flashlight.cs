using Microsoft.Xna.Framework;
using MusicFromOnePointFour;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpookyTerraria.Flashlight
{
    public static class CustomItemHoldStyleID
    {
        public static int LookAtCursor = 16;
    }
    internal abstract class PacketHandler
    {
        public byte HandlerType { get; set; }

        public abstract void HandlePacket(BinaryReader reader, int fromWho);

        protected PacketHandler(byte handlerType)
        {
            HandlerType = handlerType;
        }

        protected ModPacket GetPacket(byte packetType, int fromWho)
        {
            var p = ModContent.GetInstance<SpookyTerraria>().GetPacket();
            p.Write(HandlerType);
            p.Write(packetType);
            if (Main.netMode == NetmodeID.Server)
            {
                p.Write((byte)fromWho);
            }
            return p;
        }
    }
    internal class ModNetHandler
    {
        public static byte currentPacketHandler = 0;

        public static List<PacketHandler> PacketHandlers;
        // [LoadManager(LoadManagerAttribute.ManageType.Load)]
        public static void Load()
        {
            PacketHandlers = new List<PacketHandler>();
            InitializePacketHandlers();
        }
        // [LoadManager(LoadManagerAttribute.ManageType.Unload)]
        public static void Unload()
        {
            PacketHandlers = null;
        }
        public static void HandlePacket(BinaryReader r, int fromWho)
        {
            byte packetHandlerType = r.ReadByte();
            bool success = false;
            foreach (PacketHandler packetHandler in PacketHandlers)
            {
                if (packetHandlerType == packetHandler.HandlerType)
                {
                    packetHandler.HandlePacket(r, fromWho);
                    success = true;
                    break;
                }
            }
            if (!success)
            {
                Main.NewText($"Exception of type SpookyTerraria.Flashlight.Flashlight was thrown: {packetHandlerType} could not be identified.", Color.OrangeRed);
            }
        }
        /// <summary>
        ///  Gets the specified packet handler from the PacketHandler list. Returns null if no such handler could be found.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetPacketHandler<T>() where T : PacketHandler
        {
            foreach (PacketHandler packetHandler in PacketHandlers)
            {
                if (packetHandler.GetType().Equals(typeof(T)))
                {
                    return (T)packetHandler;
                }
            }
            return null;
        }
        public static void RegisterPacketHandler<T>() where T : PacketHandler
        {
            RegisterPacketHandler(typeof(T));
        }
        public static void RegisterPacketHandler(Type type)
        {
            if (type.IsSubclassOf(typeof(PacketHandler)))
            {
                PacketHandlers.Add((PacketHandler)Activator.CreateInstance(type, currentPacketHandler));
                currentPacketHandler++;
            }
        }
        public static void InitializePacketHandlers()
        {
            Type[] totalTypes = Assembly.GetExecutingAssembly().GetTypes();

            // Order packet handlers by name in order to prevent any kind of desync between clients that load the types in a different order
            IEnumerable<Type> packetHandlers = totalTypes.Where((type) => type.BaseType == typeof(PacketHandler)).OrderBy((type) => type.Name);

            foreach (Type packetHandler in packetHandlers)
            {
                RegisterPacketHandler(packetHandler);
            }
        }
    }
    public class Flashlight : ModItem
    {
        public bool isActive = true;
        // TODO: Fix HoldStyle
        public float itemRotation = 0;
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Use this to light up where you point around you in the direction of your mouse\nCan be used as a makeshift weapon");
        }
        public override void SetDefaults()
        {
            item.melee = true;
            item.holdStyle = CustomItemHoldStyleID.LookAtCursor;
            item.rare = ItemRarityID.LightRed;
            item.damage = 6;
            item.useStyle = ItemUseStyleID.Stabbing;
            item.useAnimation = 10;
            item.useTime = 10;
            item.knockBack = 2.5f;
            item.useTurn = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 3);
            recipe.AddIngredient(ItemID.Glass, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool HoldItemFrame(Player player)
        {
            if (Main.myPlayer != player.whoAmI)
            {
                return true;
            }
            Vector2 playerToCursor = (Main.MouseWorld - player.Center);
            playerToCursor.Normalize();
            player.bodyFrame.Y = Math.Max(BodyFrameCalculator.GetBodyFrameFromRotation(playerToCursor.ToRotation()), 3) * player.bodyFrame.Height;
            if (Main.MouseWorld.X > player.Center.X)
            {
                player.ChangeDir(1);
            }
            else
            {
                player.ChangeDir(-1);
            }
            return true;
        }
        public override void HoldStyle(Player player)
        {
            if (!Main.gameMenu)
            {
                if (Main.myPlayer == player.whoAmI) // This client
                {
                    if (Main.MouseWorld.X > player.Center.X)
                    {
                        player.ChangeDir(1);
                    }
                    else
                    {
                        player.ChangeDir(-1);
                    }
                }
                player.itemLocation = player.MountedCenter + new Vector2(6 * player.direction, 4);
                if (Main.myPlayer == player.whoAmI) // This client
                {
                    Vector2 playerToCursor = (Main.MouseWorld - player.Center);
                    playerToCursor.Normalize();
                    player.itemRotation = playerToCursor.ToRotation() + (float)Math.PI / 4.0f;

                    // Mulitplayer support
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        ModNetHandler.GetPacketHandler<FlashlightItemRotationPacketHandler>().SendTarget(-1, Main.myPlayer, player.itemRotation, player.direction);
                    }
                    Vector2 handleOffset = (new Vector2(4, -4)).RotatedBy(player.itemRotation);
                    player.itemLocation -= handleOffset;
                }
                else // For other clients
                {
                    player.itemRotation = itemRotation;
                    Vector2 handleOffset = (new Vector2(4, -4)).RotatedBy(player.itemRotation);
                    player.itemLocation -= handleOffset;
                }
                if (player.direction == -1)
                {
                    player.itemRotation += (float)Math.PI / 2.0f;
                }
            }
        }
        public override void HoldItem(Player player)
        {
            Light(player);
        }
        public void Light(Player player, bool notHoldingItem = false)
        {
                int lightRange = 50;
                float lightPercent = 1;
                for (int x = 0; x < lightRange; x++)
                {
                    //Vector2 playerToCursor = (Main.MouseWorld - player.Center);
                    //playerToCursor.Normalize();
                    float lightItemRotation; // For this player, directly calculate the mouse
                    if (notHoldingItem)
                    {
                        if (player.whoAmI == Main.myPlayer)
                        {
                            Vector2 playerToCursor2 = (Main.MouseWorld - player.Center);
                            playerToCursor2.Normalize();
                            lightItemRotation = playerToCursor2.ToRotation() + (float)Math.PI / 4.0f;
                        }
                        else // For other players
                        {
                            lightItemRotation = itemRotation;
                        }
                    }
                    else // If holding item and itemRotation is already calculated and synced
                    {
                        lightItemRotation = player.itemRotation;
                    }
                    Vector2 playerToCursor;
                    if (notHoldingItem)
                    {
                        playerToCursor = (lightItemRotation - (float)Math.PI / 4.0f).ToRotationVector2();
                    }
                    else
                    {
                        playerToCursor = (lightItemRotation - (player.direction == -1 ? (float)Math.PI / 2.0f : 0) - (float)Math.PI / 4.0f).ToRotationVector2();
                    }
                    Vector2 position = player.Center + playerToCursor * 16 * x;
                    if (Collision.SolidCollision(position, 10, 10))
                    {
                        lightPercent *= 0.8f;
                    }
                    Lighting.AddLight(position, new Vector3(1, 1, 1) / (x * 0.1f + 1) * 0.9f * lightPercent);
                }
        }
    }
    internal class FlashlightItemRotationPacketHandler : PacketHandler
    {
        public const byte SyncTarget = 1;

        public FlashlightItemRotationPacketHandler(byte handlerType) : base(handlerType)
        {
        }
        public override void HandlePacket(BinaryReader reader, int fromWho)
        {
            switch (reader.ReadByte())
            {
                case SyncTarget:
                    ReceiveTarget(reader, fromWho);
                    break;
            }
        }

        public void SendTarget(int toWho, int fromWho, float itemRotation, int playerDirection)
        {
            ModPacket packet = GetPacket(SyncTarget, fromWho);
            packet.Write(itemRotation);
            packet.Write(playerDirection);
            packet.Send(toWho, fromWho);
        }

        public void ReceiveTarget(BinaryReader reader, int fromWho)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                fromWho = reader.ReadByte(); // fromWho
            }
            float itemRotation = reader.ReadSingle();
            int playerDirection = reader.ReadInt32();
            if (Main.netMode == NetmodeID.Server)
            {
                SendTarget(-1, fromWho, itemRotation, playerDirection);
            }
            else
            {
                if (Main.player[fromWho].HeldItem.type == ModContent.ItemType<Flashlight>())
                {
                    ((Flashlight)Main.player[fromWho].HeldItem.modItem).itemRotation = itemRotation;
                }
                Main.player[fromWho].ChangeDir(playerDirection);
            }
        }
    }
}