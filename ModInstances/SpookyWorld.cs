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

namespace SpookyTerraria.ModIntances
{
    public class HasBuffClass : GlobalBuff
    {
        public override void DrawCustomBuffTip(string buffTip, SpriteBatch spriteBatch, int originX, int originY)
        {
            SpookyPlayer p = new SpookyPlayer();
            p.hoveringBuff = true;
        }
        public override void Update(int type, Player player, ref int buffIndex)
        {
            SpookyPlayer p = new SpookyPlayer();
            p.hasBuff = true;
        }
    }
    // Rename this lmao
    public class CountStalkersWorld : ModWorld
	{
        public override void PostDrawTiles()
        {
            SpookyPlayer p = new SpookyPlayer();
            Player player = Main.player[Main.myPlayer];
            int count = player.CountItem(ModContent.ItemType<Paper>(), 8);
            if (!Main.playerInventory && player.CountBuffs() == 0)
            {
                Utilities.SpookyTerrariaUtils.DrawPageUI(count, 35f, 100f);
            }
            if (!Main.playerInventory && player.CountBuffs() <= 11 && player.CountBuffs() > 0)
            {
                Utilities.SpookyTerrariaUtils.DrawPageUI(count, 35f, 150f);
            }
            if (!Main.playerInventory && player.CountBuffs() > 11)
            {
                Utilities.SpookyTerrariaUtils.DrawPageUI(count, 35f, 210f);
            }
            if (Main.playerInventory)
            {
                Utilities.SpookyTerrariaUtils.DrawPageUI(count, 35f, 325f);
            }
        }
        public override void PostUpdate()
        {
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
			int shiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));

			if (shiniesIndex != -1)
			{
				tasks.Insert(shiniesIndex + 1, new PassLegacy("SpookyTerraria PageGen", BlockItemsOres));
			}
		}
		private void BlockItemsOres(GenerationProgress progress)
		{
			progress.Message = "Pages... Find them!";

			for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * .0002); k++)
			{
				int x = WorldGen.genRand.Next(0, Main.maxTilesX);
				int y = WorldGen.genRand.Next((int)WorldGen.rockLayerHigh, Main.maxTilesY);

				WorldGen.TileRunner(x, y, WorldGen.genRand.Next(5, 9), WorldGen.genRand.Next(4, 7), ModContent.TileType<PageTile>()); 
			}
		}
	}
}
