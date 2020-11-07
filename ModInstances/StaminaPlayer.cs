using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace SpookyTerraria.ModIntances
{
	public class StaminaPlayer : ModPlayer
    {
        public byte Stamina;
        public int staminaDecrementTimer;
        public int staminaIncrementTimer;
        public void HandleSprinting()
        {
            bool isSprinting = SpookyTerraria.Sprint.Current && Stamina != 0 && player.velocity.X != 0 && player.velocity.Y == 0;
            bool currentlyJumping = player.velocity.Y < 0 && !player.mount.Active;
            if (isSprinting)
            {
                staminaDecrementTimer = 0;
                staminaIncrementTimer++;
                if (staminaIncrementTimer >= 4)
                {
                    Stamina--;
                    staminaIncrementTimer = 0;
                }
            }
            if (currentlyJumping)
            {
                staminaDecrementTimer = 0;
                staminaIncrementTimer++;
                if (staminaIncrementTimer >= 1)
                {
                    Stamina--;
                    staminaIncrementTimer = 0;
                }
            }
            // Main.NewText(currentlyJumping + " " + staminaIncrementTimer + " " + isSprinting);
            if (!isSprinting && !currentlyJumping)
            {
                staminaIncrementTimer = 0;
                staminaDecrementTimer++;
                if (staminaDecrementTimer >= 6)
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
            HandleSprinting();
        }
    }
}
