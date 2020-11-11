using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace SpookyTerraria.ModIntances
{
    public class SpookyGlobalItem : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ItemID.CarriageLantern)
            {
                tooltips.Add(new TooltipLine(mod, "Fuck'd", "nrd")
                {
                    text = "Can be used as a holdable lantern for light\nDoes not require any fuel of any sort"
                });
            }
        }
        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.CarriageLantern)
            {
                item.holdStyle = ItemHoldStyleID.HoldingUp; // TODO: Fix moonwalking bug
            }
        }
        public override void HoldItem(Item item, Player player)
        {
            if (item.type == ItemID.CarriageLantern)
            {
                Main.placementPreview = false;
                Vector2 playerToCursor = (Main.MouseWorld - player.Center);
                playerToCursor.Normalize();
                player.ChangeDir(Main.MouseWorld.X > player.Center.X ? 1 : -1);
                Lighting.AddLight(player.itemLocation, Color.Goldenrod.ToVector3() * (float)Main.essScale);
            }
        }
        public override void PostUpdate(Item item)
        {
            if (item.type == ItemID.CarriageLantern)
            { 
                Lighting.AddLight(item.Center, Color.Goldenrod.ToVector3() * (float)Main.essScale);
            }
        }
        public override bool HoldItemFrame(Item item, Player player)
        {
            if (item.type == ItemID.CarriageLantern)
            {
                if (Main.MouseWorld.X > player.Center.X)
                {
                    player.ChangeDir(1);
                }
                else
                {
                    player.ChangeDir(-1);
                }
                player.bodyFrame.Y = player.bodyFrame.Height * 2;
                return true;
            }
            return base.HoldItemFrame(item, player);
        }
        public override void HoldStyle(Item item, Player player)
        {
            if (item.type == ItemID.CarriageLantern)
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
                float initialRotationLeft = -0.79f;
                if (player.direction == -1)
                {
                    Vector2 origin = new Vector2(0, 18);
                    player.itemRotation += initialRotationLeft;
                    player.itemLocation.Y = player.Center.Y - origin.RotatedBy(player.itemRotation).Y * player.direction;
                    player.itemLocation.X = player.Center.X + (origin.RotatedBy(player.itemRotation).X) * player.direction;
                }
                else if (player.direction == 1)
                {
                    player.itemRotation += MathHelper.PiOver4;
                    Vector2 origin = new Vector2(0, -18);
                    // player.itemRotation = MathHelper.PiOver4 + player.velocity.X - (new Vector2(10, 0).RotatedBy(player.itemRotation).X);
                    player.itemLocation.Y = player.Center.Y - origin.RotatedBy(player.itemRotation).Y * player.direction;
                    player.itemLocation.X = player.Center.X - (origin.RotatedBy(player.itemRotation).X) * player.direction;
                }
            }
        }
    }
}