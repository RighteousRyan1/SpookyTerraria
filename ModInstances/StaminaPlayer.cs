using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpookyTerraria.Utilities;
using System;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpookyTerraria.ModIntances
{
	public class StaminaPlayer : ModPlayer
    {
        public byte Stamina;
        public int staminaDecrementTimer;
        public int staminaIncrementTimer;

        bool isSwimming;
        public const int maxArmor = 9; // Max armor slots (up to 9, the max)
        public void HandleSprinting()
        {
            for (int i = 0; i < maxArmor; i++)
            {
                Item accItem = player.armor[i];
                bool accRightItem = accItem.type == ItemID.DivingGear || accItem.type == ItemID.JellyfishDivingGear || accItem.type == ItemID.ArcticDivingGear;
                if (accRightItem)
                {
                    if (player.wet && player.velocity.Y < 0)
                    {
                        staminaDecrementTimer = 0;
                        staminaIncrementTimer++;
                        if (staminaIncrementTimer >= 12)
                        {
                            Stamina--;
                            staminaIncrementTimer = 0;
                        }
                    }
                }
            }

            bool isSprinting = SpookyTerraria.Sprint.Current && Stamina != 0 && player.velocity.X != 0 && player.velocity.Y == 0;
            bool currentlyJumping = player.velocity.Y < 0 && !player.mount.Active;

            if (isSprinting)
            {
                SpookyTerrariaUtils.NegateStaminaValue(Main.player[Main.myPlayer], 4);
            }
            if (currentlyJumping && !player.wet)
            {
                SpookyTerrariaUtils.NegateStaminaValue(Main.player[Main.myPlayer], 1);
            }
            if (currentlyJumping && player.wet)
            {
                SpookyTerrariaUtils.NegateStaminaValue(Main.player[Main.myPlayer], 5);
            }
            // Main.NewText(currentlyJumping + " " + staminaIncrementTimer + " " + isSprinting);
            if (!isSprinting && !currentlyJumping && !player.wet)
            {
                staminaIncrementTimer = 0;
                staminaDecrementTimer++;
                if (staminaDecrementTimer >= 6)
                {
                    Stamina++;
                    staminaDecrementTimer = 0;
                }
            }
            if (!isSprinting && !currentlyJumping && player.wet && player.velocity.Y >= 0)
            {
                staminaIncrementTimer = 0;
                staminaDecrementTimer++;
                if (staminaDecrementTimer >= 12)
                {
                    Stamina++;
                    staminaDecrementTimer = 0;
                }
            }
            if (!isSprinting && !currentlyJumping && player.wet && player.mount.Active)
            {
                staminaIncrementTimer = 0;
                staminaDecrementTimer++;
                if (staminaDecrementTimer >= 12)
                {
                    Stamina++;
                    staminaDecrementTimer = 0;
                }
            }
            if (Stamina < 0)
            {
                Stamina = 0;
            }
            if (Stamina > 100)
            {
                Stamina = 100;
            }
        }
        public override void PostUpdate()
        {
            HandleSprinting(); // TODO: Fix stamina bug
        }
    }
}
