using Microsoft.Xna.Framework;
using SpookyTerraria.ModIntances;
using SpookyTerraria.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpookyTerraria.Fists
{
	public class Fists : ModItem
	{
        public class CustomUseStyle
        {
            public const int Punching = 15;
        }
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("Left click for fighting, right click for mining\n'The bare necessities'");
		}
        public override void SetDefaults()
        {
            item.damage = 6;
            item.melee = true;
            item.width = 5;
            item.height = 5;
            item.useTime = 20;
            item.useAnimation = 20;
            item.knockBack = 3;
            item.rare = ItemRarityID.Blue;
            item.autoReuse = true;
            item.noUseGraphic = true;
            item.useStyle = CustomUseStyle.Punching;
            item.useTurn = true;
            item.pick = 8;
            item.axe = 2;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.melee = true;
                item.width = 5;
                item.height = 5;
                item.useTime = 20;
                item.useAnimation = 20;
                item.knockBack = 3;
                item.rare = ItemRarityID.Blue;
                item.autoReuse = true;
                item.noUseGraphic = true;
                item.useStyle = CustomUseStyle.Punching;
                item.useTurn = true;
                item.pick = 8;
                item.axe = 2;
            }
            else if (player.altFunctionUse != 2)
            {
                item.damage = 6;
                item.melee = true;
                item.width = 5;
                item.height = 5;
                item.useTime = 20;
                item.useAnimation = 20;
                item.knockBack = 3;
                item.rare = ItemRarityID.Blue;
                item.autoReuse = true;
                item.noUseGraphic = true;
                item.useStyle = CustomUseStyle.Punching;
                item.useTurn = true;
                item.pick = 0;
                item.axe = 0;
            }
            return true;
        }
        public override void HoldItem(Player player)
        {
            int indexInRange = Main.rand.Next(1, 7);
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, $"Sounds/Custom/Fists/Whiff{indexInRange}");
        }
        public override bool UseItemFrame(Player player)
        {
            player.itemLocation = player.Center;
            player.GetModPlayer<BeatGamePlayer>().punchPhase1 = player.itemAnimation < player.itemAnimationMax * 1f && player.itemAnimation > player.itemAnimationMax * 0.8f;
            player.GetModPlayer<BeatGamePlayer>().punchPhase2 = player.itemAnimation < player.itemAnimationMax * 0.8f && player.itemAnimation > player.itemAnimationMax * 0.6f;
            player.GetModPlayer<BeatGamePlayer>().punchPhase3 = player.itemAnimation < player.itemAnimationMax * 0.6f && player.itemAnimation > player.itemAnimationMax * 0.4f;
            player.GetModPlayer<BeatGamePlayer>().punchPhase4 = player.itemAnimation < player.itemAnimationMax * 0.4f && player.itemAnimation > player.itemAnimationMax * 0f;

            Vector2 playerToCursor = (Main.MouseWorld - player.Center);
            playerToCursor.Normalize();

            if (player.GetModPlayer<BeatGamePlayer>().punchPhase1)
            {
                player.bodyFrame.Y = player.bodyFrame.Height * (int)SpookyTerrariaUtils.BodyFrames.walking5;
            }
            if (player.GetModPlayer<BeatGamePlayer>().punchPhase2)
            {
                player.bodyFrame.Y = player.bodyFrame.Height * (int)SpookyTerrariaUtils.BodyFrames.walking6;
            }
            if (player.GetModPlayer<BeatGamePlayer>().punchPhase3)
            {
                player.bodyFrame.Y = player.bodyFrame.Height * (int)SpookyTerrariaUtils.BodyFrames.walking1;
            }
            if (player.GetModPlayer<BeatGamePlayer>().punchPhase4)
            {
                if (BodyFrameCalculator.GetBodyFrameFromRotation(playerToCursor.ToRotation()) > 0.7853982f && BodyFrameCalculator.GetBodyFrameFromRotation(playerToCursor.ToRotation()) < 2.3561945f)
                {
                    player.GetModPlayer<BeatGamePlayer>().punchingDown = false;
                    player.GetModPlayer<BeatGamePlayer>().punchingNeutral = false;
                    player.GetModPlayer<BeatGamePlayer>().punchingUp = true;
                    player.bodyFrame.Y = player.bodyFrame.Height * (int)SpookyTerrariaUtils.BodyFrames.armDiagonalUp;
                }
                else
                {
                    if (Math.Max(BodyFrameCalculator.GetBodyFrameFromRotation(playerToCursor.ToRotation()), 3) == 3)
                    {
                        player.GetModPlayer<BeatGamePlayer>().punchingDown = false;
                        player.GetModPlayer<BeatGamePlayer>().punchingNeutral = true;
                        player.GetModPlayer<BeatGamePlayer>().punchingUp = false;
                    }
                    else if (Math.Max(BodyFrameCalculator.GetBodyFrameFromRotation(playerToCursor.ToRotation()), 3) == 4)
                    {
                        player.GetModPlayer<BeatGamePlayer>().punchingUp = false;
                        player.GetModPlayer<BeatGamePlayer>().punchingNeutral = false;
                        player.GetModPlayer<BeatGamePlayer>().punchingDown = true;
                    }
                    player.bodyFrame.Y = Math.Max(BodyFrameCalculator.GetBodyFrameFromRotation(playerToCursor.ToRotation()), 3) * player.bodyFrame.Height;
                }
            }
            // Main.NewText($"Up: {player.GetModPlayer<BeatGamePlayer>().punchingUp}, Neutral: {player.GetModPlayer<BeatGamePlayer>().punchingNeutral}, Down: {player.GetModPlayer<BeatGamePlayer>().punchingDown}");
            return true;
        }
        public override void UseStyle(Player player)
        {
            base.UseStyle(player);
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Get the vanilla damage tooltip
            TooltipLine tt = tooltips.FirstOrDefault(x => x.Name == "AxePower" && x.mod == "Terraria");
            if (tt != null)
            {
                if (item.axe == 2)
                {
                    tt.text = "8% axe power";
                }
                if (item.axe != 2)
                {
                    tt.text = $"{item.axe * 5}% axe power";
                }
            }
        }
        public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            bool up = player.GetModPlayer<BeatGamePlayer>().punchingUp;
            bool neutral = player.GetModPlayer<BeatGamePlayer>().punchingNeutral;
            bool down = player.GetModPlayer<BeatGamePlayer>().punchingDown;
            if (player.GetModPlayer<BeatGamePlayer>().punchPhase1 || player.GetModPlayer<BeatGamePlayer>().punchPhase2 || player.GetModPlayer<BeatGamePlayer>().punchPhase3)
            {
                noHitbox = true;
                hitbox.Width = 0;
                hitbox.Height = 0;
                hitbox.Y += 999999;
                hitbox.X += 999999;
            }
            else if (neutral)
            {
                hitbox.Width = 8;
                hitbox.Height = 8;
                noHitbox = false;
                hitbox.Y += 27;
                hitbox.X += player.direction == 1 ? 10 : 12;
            }
            else if (up)
            {
                hitbox.Width = 8;
                hitbox.Height = 8;
                noHitbox = false;
                hitbox.Y += 10;
                hitbox.X += player.direction == 1 ? 10 : 12;
            }
            else if (down)
            {
                hitbox.Width = 8;
                hitbox.Height = 8;
                noHitbox = false;
                hitbox.Y += 45;
                hitbox.X += player.direction == 1 ? 10 : 12;
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}