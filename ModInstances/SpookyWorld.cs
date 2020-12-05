using Terraria.ModLoader;
using Terraria;
using SpookyTerraria.NPCs;
using System.Collections.Generic;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;
using Terraria.ID;
using SpookyTerraria.Tiles;
using Microsoft.Xna.Framework;
using SpookyTerraria.OtherItems;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader.IO;
using SpookyTerraria.Utilities;
using static SpookyTerraria.SoundPlayer;
using ReLogic.Graphics;
using System;

namespace SpookyTerraria.ModIntances
{
    public class HasBuffClass : GlobalBuff
    {
        public override void DrawCustomBuffTip(string buffTip, SpriteBatch spriteBatch, int originX, int originY)
        {
            Main.player[Main.myPlayer].GetModPlayer<SpookyPlayer>().hoveringBuff = true;
        }
    }
    // Rename this lmao
    public class CountStalkersWorld : ModWorld
	{
        public override void Initialize()
        {
            SpookyPlayer.pages = 0;
        }
        public override void Load(TagCompound tag)
        {
            SpookyPlayer.pages = tag.GetInt("pages");
        }
        public override TagCompound Save()
        {
            return new TagCompound
            {
                {
                    "pages", SpookyPlayer.pages
                }
            };
        }
        public int choice;
        public int deathTextChoice;
        public float scale;
        public float fadeScale;
        public Rectangle staticFrame = new Rectangle(0, 0, 1271, 650);
        public int timerToLightStatic;
        public int timerToMediumStatic;
        public int timerToSevereStatic;
        public override void PostDrawTiles()
        {
            Player player = Main.player[Main.myPlayer];
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            Main.spriteBatch.Draw(mod.GetTexture("Assets/WhiteSquare"), player.position, null, Color.White * 0.25f, 0f, new Vector2(Main.screenWidth / 2, Main.screenHeight / 2), new Vector2(player.Hitbox.Width, player.Hitbox.Height), SpriteEffects.None, 1f);
            Main.spriteBatch.End();
            if (player.dead)
            {
                Main.hideUI = true;
                fadeScale = 0f;
            }
            if (!Main.hasFocus)
            {
                timerToLightStatic = 0;
                timerToMediumStatic = 0;
                timerToSevereStatic = 0;
            }
            for (int ll = 0; ll < Main.maxNPCs; ll++)
            {
                NPC npc = Main.npc[ll];
                float distance = npc.Distance(Main.player[Main.myPlayer].Center);
                if (npc.active && distance <= 1000f)
                {
                    if (npc.type == ModContent.NPCType<Slenderman>())
                    {
                        if (Main.GameUpdateCount % 3 == 0)
                        {
                            staticFrame.Y += 650;
                        }
                        if (staticFrame.Y >= 1950)
                        {
                            staticFrame.Y = 0;
                        }
                        bool facingTowardsSlendermanLeft = npc.Center.X < player.Center.X && player.direction == -1;
                        bool facingTowardsSlendermanRight = npc.Center.X > player.Center.X && player.direction == 1;
                        Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend); // sourceRectangle: new Rectangle((int)Main.screenPosition.X, (int)Main.screenPosition.Y
                        Main.spriteBatch.Draw(mod.GetTexture("Assets/Static"), new Vector2(Main.screenWidth / 2, Main.screenHeight / 2), sourceRectangle: staticFrame, Color.White * fadeScale, 0f, new Vector2(Main.screenWidth / 2, Main.screenHeight / 2), 5f, SpriteEffects.None, 1f);
                        Main.spriteBatch.End();
                        if (facingTowardsSlendermanLeft || facingTowardsSlendermanRight)
                        {
                            if (fadeScale > 0f && fadeScale < 0.4f)
                            {
                                timerToMediumStatic = 0;
                                timerToSevereStatic = 0;
                                timerToLightStatic++;
                                if (Main.hasFocus)
                                {
                                    SoundEngine.StopAmbientSound("Sounds/Custom/Slender/StaticMedium");
                                    SoundEngine.StopAmbientSound("Sounds/Custom/Slender/StaticSevere");
                                }
                                if (timerToLightStatic == 2)
                                {
                                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Slender/StaticLight"));
                                }
                                if (timerToLightStatic >= 480)
                                {
                                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Slender/StaticLight"));
                                    timerToLightStatic = 0;
                                }
                            }
                            if (fadeScale >= 0.4 && fadeScale < 0.8f)
                            {
                                timerToLightStatic = 0;
                                timerToSevereStatic = 0;
                                timerToMediumStatic++;
                                if (Main.hasFocus)
                                {
                                    SoundEngine.StopAmbientSound("Sounds/Custom/Slender/StaticLight");
                                    SoundEngine.StopAmbientSound("Sounds/Custom/Slender/StaticSevere");
                                }
                                if (timerToMediumStatic == 2)
                                {
                                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Slender/StaticMedium"));
                                }
                                if (timerToMediumStatic >= 480)
                                {
                                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Slender/StaticMedium"));
                                    timerToMediumStatic = 0;
                                }
                            }
                            if (fadeScale >= 0.8f)
                            {
                                timerToMediumStatic = 0;
                                timerToLightStatic = 0;
                                timerToSevereStatic++;
                                if (Main.hasFocus)
                                {
                                    SoundEngine.StopAmbientSound("Sounds/Custom/Slender/StaticLight");
                                    SoundEngine.StopAmbientSound("Sounds/Custom/Slender/StaticMedium");
                                }
                                if (timerToSevereStatic == 2)
                                {
                                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Slender/StaticSevere"));
                                }
                                if (timerToSevereStatic >= 132)
                                {
                                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Slender/StaticSevere"));
                                    timerToSevereStatic = 0;
                                }
                            }
                            // Main.NewText($"L: {timerToLightStatic} | M: {timerToMediumStatic} | S: {timerToSevereStatic}");
                            // MAJOR TODO: MAKE STATIC
                            if (!Collision.SolidCollision(npc.position, npc.width, 20))
                            {
                                if (Main.GameUpdateCount % 3 == 0)
                                {
                                    fadeScale += 0.005f;
                                }
                            }
                        }
                        else if (!facingTowardsSlendermanLeft && !facingTowardsSlendermanRight)
                        {
                            timerToLightStatic = 0;
                            timerToMediumStatic = 0;
                            timerToSevereStatic = 0;
                            if (Main.GameUpdateCount % 3 == 0)
                            {
                                fadeScale -= 0.005f;
                            }
                        }
                        else if (distance > 1000f)
                        {
                            if (Main.GameUpdateCount % 3 == 0)
                            {
                                fadeScale -= 0.005f;
                            }
                        }
                        if (fadeScale > 1f)
                        {
                            fadeScale = 1f;
                        }
                        if (fadeScale < 0f)
                        {
                            fadeScale = 0f;
                        }
                        if (fadeScale >= 1f)
                        {
                            // player.KillMe(Terraria.DataStructures.PlayerDeathReason.ByCustomReason($"{player.name} stared at death for too long."), player.statLife + 50, 0, false);
                        }
                    }
                    if (npc.type == ModContent.NPCType<Slenderman>() && !npc.active)
                    {
                        timerToLightStatic = 0;
                        timerToMediumStatic = 0;
                        timerToSevereStatic = 0;
                        fadeScale -= 0.005f;
                    }
                }
                if (npc.type == ModContent.NPCType<Stalker>() && !npc.active)
                {
                    SoundEngine.StopAmbientSound("Sounds/Custom/Slender/StaticMedium");
                    SoundEngine.StopAmbientSound("Sounds/Custom/Slender/StaticLight");
                }
            }

            if (player.GetModPlayer<SpookyPlayer>().deathTextTimer == 1)
            {
                deathTextChoice = Main.rand.Next(0, 10);
            }
            int countCached = player.GetModPlayer<SpookyPlayer>().cachedPageCount;

            Color lighterRed = new Color(255, 50, 50);
            Color lerpable = SpookyTerrariaUtils.ColorSwitcher(Color.Red, lighterRed, 30f);
            Color moreLerpable = Color.Lerp(Color.Red, Color.Green, player.GetModPlayer<SpookyPlayer>().cachedPageCount * 0.125f);
            // 205, 92, 92, a: 255

            string displayableString = SpookyTerrariaUtils.ChooseRandomDeathText(deathTextChoice);
            string subString = "Returning to Main Menu...";
            string pagesFound = $"Pages Found: {player.GetModPlayer<SpookyPlayer>().cachedPageCount} / 8";

            float x1 = Main.screenWidth / 2;
            float y1 = Main.screenHeight / 2;

            float b = y1 - 75f;

            float x = Main.rand.NextFloat(x1 - 5, x1 + 5);
            float y = Main.rand.NextFloat(y1 - 5, y1 + 5);

            float m = y1 - 100f;

            float j = y1 + 40f;

            Vector2 middle1 = Main.fontDeathText.MeasureString(displayableString) / 2;
            Vector2 middle2 = Main.fontDeathText.MeasureString(subString) / 2;
            Vector2 middle3 = Main.fontDeathText.MeasureString(pagesFound) / 2;
            if (player.dead)
            {
                SpookyTerrariaUtils.DrawPageUI(countCached, x1 - 88, b);
                // Main.hideUI = true;
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                // Text with shake
                Main.spriteBatch.DrawString(Main.fontDeathText, displayableString, new Vector2(x, y), Color.IndianRed * 0.6f, 0f, middle1, 1f, SpriteEffects.None, 0f);
                // Text without shake
                Main.spriteBatch.DrawString(Main.fontDeathText, displayableString, new Vector2(x1, y1), Color.IndianRed, 0f, middle1, 1f, SpriteEffects.None, 0f);
                // Pages Found
                Main.spriteBatch.DrawString(Main.fontDeathText, pagesFound, new Vector2(x1, m), moreLerpable, 0f, middle3, 0.4f, SpriteEffects.None, 0f);
                if (Main.worldName == SpookyTerrariaUtils.slenderWorldName)
                {
                    Main.spriteBatch.DrawString(Main.fontDeathText, subString, new Vector2(x1, j), lerpable, 0f, middle2, 0.5f, SpriteEffects.None, 0f);
                }
                Main.spriteBatch.End();
            }
            if (player.GetModPlayer<SpookyPlayer>().pageDisplayTimer < 99 && player.GetModPlayer<SpookyPlayer>().pageDisplayTimer > 0)
            {
                // player.GetModPlayer<SpookyPlayer>().displayTimes++;
                SpookyTerrariaUtils.DrawPageInterface(choice);
            }
            if (player.GetModPlayer<SpookyPlayer>().pageDisplayTimer == 99)
            {
                choice = Main.rand.Next(1, 9);
            }
            float lighting = Lighting.GetSubLight(Main.MouseWorld).X;
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            if (lighting > 0.05f)
            {
                if (!Main.gameMenu)
                {
                    if (Main.LocalPlayer.GetModPlayer<SpookyPlayer>().hoveringPageLeft || Main.LocalPlayer.GetModPlayer<SpookyPlayer>().hoveringPageRight || Main.LocalPlayer.GetModPlayer<SpookyPlayer>().hoveringPageWall)
                    {
                        string pickUp = $"Right click to pick up";
                        Vector2 g = Main.fontMouseText.MeasureString(pickUp);
                        // Main.spriteBatch.DrawString(Main.fontMouseText, pickUp, Main.MouseScreen + new Vector2(25, 25), Color.White, 0f, new Vector2(0, 0), Main.essScale * 5/*1f*/, SpriteEffects.None, 0f);
                        Main.spriteBatch.DrawString(Main.fontMouseText, pickUp, Main.MouseWorld + new Vector2(15, -35) - Main.screenPosition, Color.White * lighting, 0f, new Vector2(g.X / 2, 5), 1f, SpriteEffects.None, 0f);
                    }
                }
            }
            Main.spriteBatch.End();
            if (Main.hasFocus && Main.worldName == SpookyTerrariaUtils.slenderWorldName)
            {
                SoundEngine.StopAmbientSound(SoundEngine.wavesSoundDir);
                oceanWavesTimer = 0;
            }
            if (!Main.hasFocus)
            {
                SoundEngine.StopAllAmbientSounds();
                SoundEngine.StopAmbientSound(SoundEngine.wavesSoundDir);
                player.GetModPlayer<SpookyPlayer>().hootTimer = 0;
                player.GetModPlayer<SpookyPlayer>().breezeTimer = 0;

                jungleAmbTimer = 0;
                dayAmbienceTimer = 0;

                oceanWavesTimer = 0;
                cricketsTimer = 0;
                blizzTimer = 0;
                caveRumbleTimer = 0;
            }

            // TODO: FINISH DRAWN RANDOMIZATION
            int count = SpookyPlayer.pages;
            if (!Main.playerInventory && player.CountBuffs() == 0 && !Main.hideUI && !player.dead)
            {
                SpookyTerrariaUtils.DrawPageUI(count, 35f, 100f);
            }
            if (!Main.playerInventory && player.CountBuffs() <= 11 && player.CountBuffs() > 0 && !Main.hideUI && !player.dead)
            {
                SpookyTerrariaUtils.DrawPageUI(count, 35f, 150f);
            }
            if (!Main.playerInventory && player.CountBuffs() > 11 && !Main.hideUI && !player.dead)
            {
                SpookyTerrariaUtils.DrawPageUI(count, 35f, 210f);
            }
            if (Main.playerInventory && !Main.hideUI && !player.dead)
            {
                SpookyTerrariaUtils.DrawPageUI(count, 35f, 325f);
            }
        }
        public override void PostUpdate()
        {
            ModNPC modNPC = new ModNPC();
            if (modNPC.npc.townNPC)
            {
                modNPC.CanTownNPCSpawn(999, int.MaxValue);
            }
            for (int index = 0; index < Main.maxNPCs; index++)
            {
                NPC npc = Main.npc[index];
                if (npc.type == ModContent.NPCType<Stalker>() && npc.active)
                {
                    Main.player[Main.myPlayer].GetModPlayer<SpookyPlayer>().stalkerConditionMet = true;
                }
            }
        }
		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
		{
			int shiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Planting Trees"));

			if (shiniesIndex != -1)
			{
				tasks.Insert(shiniesIndex + 1, new PassLegacy("Planting Pages", PagesOnTrees));
			}
		}
		private void PagesOnTrees(GenerationProgress progress)
		{
            int numOfPages = 8; // Change this ;)
			progress.Message = "Randomly slapping pages on trees...";

            List<Point> rightSideSpots = new List<Point>();
            List<Point> leftSideSpots = new List<Point>();

            for (int i = 2; i < Main.maxTilesX - 2; i++)
			{
                for (int j = 2; j < Main.maxTilesY - 2; j++)
                {
                    Tile leftTile = Main.tile[i - 1, j];
                    Tile leftMostTile = Main.tile[i - 2, j];
                    Tile rightTile = Main.tile[i + 1, j];
                    Tile rightMostTile = Main.tile[i + 2, j];

                    if (leftTile.type == TileID.Trees && !leftMostTile.active() && leftTile.frameX == 0 && leftTile.frameY < 66)
					{
                        leftSideSpots.Add(new Point(i, j));
					}
                    else if (rightTile.type == TileID.Trees && !rightMostTile.active() && rightTile.frameX == 0 && rightTile.frameY < 66)
					{
                        rightSideSpots.Add(new Point(i, j));
					}
                }
            }
            for (int pages = 0; pages < numOfPages; pages++)
			{
                bool left = WorldGen._genRand.NextBool();
                if (left)
				{
                    int pointIndex = (int)(leftSideSpots.Count * WorldGen._genRand.NextFloat());
                    Point selectedPoint = leftSideSpots[pointIndex];
                    WorldGen.PlaceTile(selectedPoint.X, selectedPoint.Y, ModContent.TileType<PageTileLeft>()); 
                }
                else
				{
                    int pointIndex = (int)(rightSideSpots.Count * WorldGen._genRand.NextFloat());
                    Point selectedPoint = rightSideSpots[pointIndex];
                    WorldGen.PlaceTile(selectedPoint.X, selectedPoint.Y, ModContent.TileType<PageTileRight>());
                }
			} 
		}
	}
}
