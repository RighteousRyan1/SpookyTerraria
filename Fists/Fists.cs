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
        public bool withinBoundsOfAttack;
        public class CustomUseStyle
        {
            public const int Punching = 15;
        }
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("Left click for light attacks\nRight click for powerful, slow attacks\n'The bare necessities'");
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
        }
        public override bool AltFunctionUse(Player player)
        {
            return player.GetModPlayer<StaminaPlayer>().Stamina >= 25;
        }
        public override bool UseItem(Player player)
        {
            return true;
        }
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            if (player.altFunctionUse == 2)
            {
                mult = 2.5f;
            }
            else if (player.altFunctionUse != 2)
            {
                mult = 1f;
            }
        }
        public override float UseTimeMultiplier(Player player)
        {
            return player.altFunctionUse == 2 ? 0.5f : 1f;
        }
        public override float MeleeSpeedMultiplier(Player player)
        {
            return player.altFunctionUse == 2 ? 0.5f : 1f;
        }
        public override bool CanUseItem(Player player)
        {
            return player.GetModPlayer<StaminaPlayer>().Stamina >= 10;
        }
        public override void GetWeaponKnockback(Player player, ref float knockback)
        {
            if (player.GetModPlayer<SpookyPlayer>().punchingCharged)
            {
                knockback = 10f;
            }
            else if (player.GetModPlayer<SpookyPlayer>().punchingLight)
            {
                knockback = 1f;
            }
        }
        public override void GetWeaponCrit(Player player, ref int crit)
        {
            if (player.GetModPlayer<SpookyPlayer>().punchingCharged)
            {
                crit *= (int)(item.crit * 2.5f);
            }
            else if (player.GetModPlayer<SpookyPlayer>().punchingLight)
            {
                crit = (int)(item.crit * 1f);
            }
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
        }
        public override void HoldItem(Player player)
        {
        }
        public override bool UseItemFrame(Player player)
        {
            player.itemLocation = player.Center;
            player.GetModPlayer<SpookyPlayer>().punchPhase1 = player.itemAnimation < player.itemAnimationMax * 1f && player.itemAnimation > player.itemAnimationMax * 0.8f;
            player.GetModPlayer<SpookyPlayer>().punchPhase2 = player.itemAnimation < player.itemAnimationMax * 0.8f && player.itemAnimation > player.itemAnimationMax * 0.6f;
            player.GetModPlayer<SpookyPlayer>().punchPhase3 = player.itemAnimation < player.itemAnimationMax * 0.6f && player.itemAnimation > player.itemAnimationMax * 0.4f;
            player.GetModPlayer<SpookyPlayer>().punchPhase4 = player.itemAnimation < player.itemAnimationMax * 0.4f && player.itemAnimation > player.itemAnimationMax * 0f;

            Vector2 playerToCursor = (Main.MouseWorld - player.Center);
            playerToCursor.Normalize();

            if (player.GetModPlayer<SpookyPlayer>().punchPhase1)
            {
                player.bodyFrame.Y = player.bodyFrame.Height * (int)SpookyTerrariaUtils.BodyFrames.walking5;
            }
            if (player.GetModPlayer<SpookyPlayer>().punchPhase2)
            {
                player.bodyFrame.Y = player.bodyFrame.Height * (int)SpookyTerrariaUtils.BodyFrames.walking6;
            }
            if (player.GetModPlayer<SpookyPlayer>().punchPhase3)
            {
                player.bodyFrame.Y = player.bodyFrame.Height * (int)SpookyTerrariaUtils.BodyFrames.walking1;
            }
            if (player.GetModPlayer<SpookyPlayer>().punchPhase4)
            {
                if (BodyFrameCalculator.GetBodyFrameFromRotation(playerToCursor.ToRotation()) > 0.7853982f && BodyFrameCalculator.GetBodyFrameFromRotation(playerToCursor.ToRotation()) < 2.3561945f)
                {
                    player.GetModPlayer<SpookyPlayer>().punchingDown = false;
                    player.GetModPlayer<SpookyPlayer>().punchingNeutral = false;
                    player.GetModPlayer<SpookyPlayer>().punchingUp = true;
                    player.bodyFrame.Y = player.bodyFrame.Height * (int)SpookyTerrariaUtils.BodyFrames.armDiagonalUp;
                }
                else
                {
                    if (Math.Max(BodyFrameCalculator.GetBodyFrameFromRotation(playerToCursor.ToRotation()), 3) == 3)
                    {
                        player.GetModPlayer<SpookyPlayer>().punchingDown = false;
                        player.GetModPlayer<SpookyPlayer>().punchingNeutral = true;
                        player.GetModPlayer<SpookyPlayer>().punchingUp = false;
                    }
                    else if (Math.Max(BodyFrameCalculator.GetBodyFrameFromRotation(playerToCursor.ToRotation()), 3) == 4)
                    {
                        player.GetModPlayer<SpookyPlayer>().punchingUp = false;
                        player.GetModPlayer<SpookyPlayer>().punchingNeutral = false;
                        player.GetModPlayer<SpookyPlayer>().punchingDown = true;
                    }
                    player.bodyFrame.Y = Math.Max(BodyFrameCalculator.GetBodyFrameFromRotation(playerToCursor.ToRotation()), 3) * player.bodyFrame.Height;
                }
            }
            // Main.NewText($"Up: {player.GetModPlayer<BeatGamePlayer>().punchingUp}, Neutral: {player.GetModPlayer<BeatGamePlayer>().punchingNeutral}, Down: {player.GetModPlayer<BeatGamePlayer>().punchingDown}");
            return true;
        }
        public override void UseStyle(Player player)
        {
            if (player.itemAnimation > player.itemAnimationMax * 0.4 && player.itemAnimation < player.itemAnimationMax * 0.3)
            {
                withinBoundsOfAttack = true;
            }
            if (player.altFunctionUse == 2)
            {
                player.GetModPlayer<SpookyPlayer>().punchingCharged = true;
                player.GetModPlayer<SpookyPlayer>().punchingLight = false;
            }
            else if (player.altFunctionUse == 0)
            {
                player.GetModPlayer<SpookyPlayer>().punchingCharged = false;
                player.GetModPlayer<SpookyPlayer>().punchingLight = true;
            }
            int indexInRange = Main.rand.Next(1, 7);
            int indexInRange2 = Main.rand.Next(1, 4);
            if (player.itemAnimation == (int)(player.itemAnimationMax * 0.4f))
            {
                if (player.altFunctionUse == 2)
                {
                    if (player.GetModPlayer<StaminaPlayer>().Stamina >= 25)
                    {
                        player.GetModPlayer<StaminaPlayer>().Stamina -= 25;
                    }
                    if (player.GetModPlayer<StaminaPlayer>().Stamina < 25)
                    {
                        player.GetModPlayer<StaminaPlayer>().Stamina = 0;
                    }
                    if (player.Distance(Main.MouseWorld) <= 50f)
                    {
                        player.PickTile((int)Main.MouseWorld.X / 16, (int)Main.MouseWorld.Y / 16, 25);
                    }
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, $"Sounds/Custom/Fists/WhiffSlow{indexInRange2}"));
                }
                if (player.altFunctionUse == 0)
                {
                    if (player.GetModPlayer<StaminaPlayer>().Stamina >= 10)
                    {
                        player.GetModPlayer<StaminaPlayer>().Stamina -= 10;
                    }
                    if (player.GetModPlayer<StaminaPlayer>().Stamina < 10)
                    {
                        player.GetModPlayer<StaminaPlayer>().Stamina = 0;
                    }
                    if (player.Distance(Main.MouseWorld) <= 50f)
                    {
                        player.PickTile((int)Main.MouseWorld.X / 16, (int)Main.MouseWorld.Y / 16, 8);
                    }
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, $"Sounds/Custom/Fists/Whiff{indexInRange}"));
                }
            }
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
            if (withinBoundsOfAttack)
            {
                Main.NewText('a');
            }
            bool chargedAttack = player.GetModPlayer<SpookyPlayer>().punchingCharged;
            bool lightAttack = player.GetModPlayer<SpookyPlayer>().punchingLight;

            bool up = player.GetModPlayer<SpookyPlayer>().punchingUp;
            bool neutral = player.GetModPlayer<SpookyPlayer>().punchingNeutral;
            bool down = player.GetModPlayer<SpookyPlayer>().punchingDown;
            if (player.GetModPlayer<SpookyPlayer>().punchPhase1 || player.GetModPlayer<SpookyPlayer>().punchPhase2 || player.GetModPlayer<SpookyPlayer>().punchPhase3)
            {
                hitbox.Width = player.Hitbox.Width;
                hitbox.Height = player.Hitbox.Height;
                hitbox.Y += player.Hitbox.Y;
                hitbox.X = player.Hitbox.X;
            }
            else if (neutral && lightAttack)
            {
                hitbox.Width = 16;
                hitbox.Height = 16;
                noHitbox = false;
                hitbox.Y += 23;
                hitbox.X += player.direction == 1 ? 10 : 6;
            }
            else if (up && lightAttack)
            {
                hitbox.Width = 16;
                hitbox.Height = 16;
                noHitbox = false;
                hitbox.Y += 8;
                hitbox.X += player.direction == 1 ? 6 : 10;
            }
            else if (down && lightAttack)
            {
                hitbox.Width = 16;
                hitbox.Height = 16;
                noHitbox = false;
                hitbox.Y += 38;
                hitbox.X += player.direction == 1 ? 6 : 8;
            }
            else if (neutral && chargedAttack)
            {
                hitbox.Width = 24;
                hitbox.Height = 24;
                noHitbox = false;
                hitbox.Y += 23;
                hitbox.X += player.direction == 1 ? 10 : -3;
            }
            else if (up && chargedAttack)
            {
                hitbox.Width = 24;
                hitbox.Height = 24;
                noHitbox = false;
                hitbox.Y += 0;
                hitbox.X += player.direction == 1 ? 6 : 6;
            }
            else if (down && chargedAttack)
            {
                hitbox.Width = 24;
                hitbox.Height = 24;
                noHitbox = false;
                hitbox.Y += 38;
                hitbox.X += player.direction == 1 ? 6 : 2;
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