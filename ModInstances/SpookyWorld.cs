using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using SpookyTerraria.NPCs;
using SpookyTerraria.Tiles;
using SpookyTerraria.Utilities;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;

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
            SpookyPlayer.Pages = 0;
        }

        public override void Load(TagCompound tag)
        {
            SpookyPlayer.Pages = tag.GetInt("pages");
        }

        public override TagCompound Save()
        {
            return new TagCompound
            {
                {
                    "pages", SpookyPlayer.Pages
                }
            };
        }

        public int choice;
        public int deathTextChoice;
        public float scale;
        public float fadeScale;

        public override void PostDrawTiles()
        {
            var player = Main.player[Main.myPlayer];

            var count = SpookyPlayer.Pages;
            if (!Main.playerInventory && player.CountBuffs() == 0 && !Main.hideUI && !player.dead)
                SpookyTerrariaUtils.DrawPageUI(count, 35f, 100f);

            if (!Main.playerInventory && player.CountBuffs() <= 11 && player.CountBuffs() > 0 && !Main.hideUI && !player.dead)
                SpookyTerrariaUtils.DrawPageUI(count, 35f, 150f);

            if (!Main.playerInventory && player.CountBuffs() > 11 && !Main.hideUI && !player.dead)
                SpookyTerrariaUtils.DrawPageUI(count, 35f, 210f);

            if (Main.playerInventory && !Main.hideUI && !player.dead) SpookyTerrariaUtils.DrawPageUI(count, 35f, 325f);

            if (player.GetModPlayer<SpookyPlayer>().deathTextTimer == 1) deathTextChoice = Main.rand.Next(0, 10);

            var countCached = player.GetModPlayer<SpookyPlayer>().cachedPageCount;

            var lighterRed = new Color(255, 50, 50);
            var lerpable = SpookyTerrariaUtils.ColorSwitcher(Color.Red, lighterRed, 30f);
            var moreLerpable = Color.Lerp(Color.Red, Color.Green, player.GetModPlayer<SpookyPlayer>().cachedPageCount * 0.125f);
            // 205, 92, 92, a: 255

            var displayableString = SpookyTerrariaUtils.ChooseRandomDeathText(deathTextChoice);
            var subString = "Returning to Main Menu...";
            var pagesFound = $"Pages Found: {player.GetModPlayer<SpookyPlayer>().cachedPageCount} / 8";

            float x1 = Main.screenWidth / 2;
            float y1 = Main.screenHeight / 2;

            var b = y1 - 75f;

            var x = Main.rand.NextFloat(x1 - 5, x1 + 5);
            var y = Main.rand.NextFloat(y1 - 5, y1 + 5);

            var m = y1 - 100f;

            var j = y1 + 40f;

            var middle1 = Main.fontDeathText.MeasureString(displayableString) / 2;
            var middle2 = Main.fontDeathText.MeasureString(subString) / 2;
            var middle3 = Main.fontDeathText.MeasureString(pagesFound) / 2;
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
                    Main.spriteBatch.DrawString(Main.fontDeathText, subString, new Vector2(x1, j), lerpable, 0f, middle2, 0.5f, SpriteEffects.None, 0f);

                Main.spriteBatch.End();
            }

            if (player.GetModPlayer<SpookyPlayer>().pageDisplayTimer < 99 && player.GetModPlayer<SpookyPlayer>().pageDisplayTimer > 0)
                // player.GetModPlayer<SpookyPlayer>().displayTimes++;
                SpookyTerrariaUtils.DrawPageInterface(choice);

            if (player.GetModPlayer<SpookyPlayer>().pageDisplayTimer == 99) choice = Main.rand.Next(1, 9);

            var lighting = Lighting.GetSubLight(Main.MouseWorld).X;
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            if (lighting > 0.05f)
                if (!Main.gameMenu)
                    if (Main.LocalPlayer.GetModPlayer<SpookyPlayer>().hoveringPageLeft || Main.LocalPlayer.GetModPlayer<SpookyPlayer>().hoveringPageRight || Main.LocalPlayer.GetModPlayer<SpookyPlayer>().hoveringPageWall)
                    {
                        var pickUp = "Right click to pick up";
                        var g = Main.fontMouseText.MeasureString(pickUp);
                        // Main.spriteBatch.DrawString(Main.fontMouseText, pickUp, Main.MouseScreen + new Vector2(25, 25), Color.White, 0f, new Vector2(0, 0), Main.essScale * 5/*1f*/, SpriteEffects.None, 0f);
                        Main.spriteBatch.DrawString(Main.fontMouseText, pickUp, Main.MouseWorld + new Vector2(15, -35) - Main.screenPosition, Color.White * lighting, 0f, new Vector2(g.X / 2, 5), 1f, SpriteEffects.None, 0f);
                    }

            Main.spriteBatch.End();
        }

        public override void PostUpdate()
        {
            var modNPC = new ModNPC();
            if (modNPC.npc.townNPC) modNPC.CanTownNPCSpawn(999, int.MaxValue);

            for (var index = 0; index < Main.maxNPCs; index++)
            {
                var npc = Main.npc[index];
                if (npc.type == ModContent.NPCType<Stalker>() && npc.active)
                    Main.player[Main.myPlayer].GetModPlayer<SpookyPlayer>().stalkerConditionMet = true;
            }
        }

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            var shiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Planting Trees"));

            if (shiniesIndex != -1) tasks.Insert(shiniesIndex + 1, new PassLegacy("Planting Pages", PagesOnTrees));
        }

        private void PagesOnTrees(GenerationProgress progress)
        {
            var numOfPages = 8; // Change this ;)
            progress.Message = "Randomly slapping pages on trees...";

            var rightSideSpots = new List<Point>();
            var leftSideSpots = new List<Point>();

            for (var i = 2; i < Main.maxTilesX - 2; i++)
            for (var j = 2; j < Main.maxTilesY - 2; j++)
            {
                var leftTile = Main.tile[i - 1, j];
                var leftMostTile = Main.tile[i - 2, j];
                var rightTile = Main.tile[i + 1, j];
                var rightMostTile = Main.tile[i + 2, j];

                if (leftTile.type == TileID.Trees && !leftMostTile.active() && leftTile.frameX == 0 && leftTile.frameY < 66)
                    leftSideSpots.Add(new Point(i, j));
                else if (rightTile.type == TileID.Trees && !rightMostTile.active() && rightTile.frameX == 0 && rightTile.frameY < 66)
                    rightSideSpots.Add(new Point(i, j));
            }

            for (var pages = 0; pages < numOfPages; pages++)
            {
                var left = WorldGen._genRand.NextBool();
                if (left)
                {
                    var pointIndex = (int) (leftSideSpots.Count * WorldGen._genRand.NextFloat());
                    var selectedPoint = leftSideSpots[pointIndex];
                    WorldGen.PlaceTile(selectedPoint.X, selectedPoint.Y, ModContent.TileType<PageTileLeft>());
                }
                else
                {
                    var pointIndex = (int) (rightSideSpots.Count * WorldGen._genRand.NextFloat());
                    var selectedPoint = rightSideSpots[pointIndex];
                    WorldGen.PlaceTile(selectedPoint.X, selectedPoint.Y, ModContent.TileType<PageTileRight>());
                }
            }
        }
    }
}