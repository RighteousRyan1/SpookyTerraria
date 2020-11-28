using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Linq;

namespace SpookyTerraria.ModIntances
{
    public class SpookyGlobalItem : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ItemID.CarriageLantern)
            {
                tooltips.Add(new TooltipLine(mod, "HoldableLantern", "HoldableLantern")
                {
                    text = "Can be used as a holdable lantern for light\nDoes not require any fuel of any sort"
                });
            }
            TooltipLine damageLine = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.mod == "Terraria");
            if (damageLine != null)
            {
                damageLine.overrideColor = Color.LawnGreen;
            }
            TooltipLine critLine = tooltips.FirstOrDefault(x => x.Name == "CritChance" && x.mod == "Terraria");
            if (critLine != null)
            {
                critLine.overrideColor = Color.Red;
            }
        }
        public override void SetDefaults(Item item)
        {
            item.useTurn = false;
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
                Lighting.AddLight(player.itemLocation, Color.Goldenrod.ToVector3() * Main.essScale);
            }
        }
        public override void PostUpdate(Item item)
        {
            if (item.type == ItemID.CarriageLantern)
            { 
                Lighting.AddLight(item.Center, Color.Goldenrod.ToVector3() * Main.essScale);
            }
        }
        public override bool HoldItemFrame(Item item, Player player)
        {
            if (item.type == ItemID.CarriageLantern)
            {
                player.bodyFrame.Y = player.bodyFrame.Height * 2;
                return true;
            }
            return base.HoldItemFrame(item, player);
        }
        public override void HoldStyle(Item item, Player player)
        {
            player.itemLocation = player.itemLocation.Floor();
            if (item.type == ItemID.CarriageLantern)
            {
                float initialRotationLeft = -0.79f;
                if (player.direction == -1)
                {
                    Vector2 origin = new Vector2(0, 18);
                    player.itemRotation += initialRotationLeft;
                    player.itemLocation.Y = player.Center.Y - origin.RotatedBy(player.itemRotation).Y * player.direction;
                    player.itemLocation.X = player.Center.X + origin.RotatedBy(player.itemRotation).X * player.direction;
                }
                else if (player.direction == 1)
                {
                    player.itemRotation += MathHelper.PiOver4;
                    Vector2 origin = new Vector2(0, -18);
                    // player.itemRotation = MathHelper.PiOver4 + player.velocity.X - (new Vector2(10, 0).RotatedBy(player.itemRotation).X);
                    player.itemLocation.Y = player.Center.Y - origin.RotatedBy(player.itemRotation).Y * player.direction;
                    player.itemLocation.X = player.Center.X - origin.RotatedBy(player.itemRotation).X * player.direction;
                }
            }
        }
    }
}