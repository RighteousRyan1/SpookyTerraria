using System.Linq;
using Terraria.Graphics.Effects;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SpookyTerraria.OtherItems;

namespace SpookyTerraria.ModIntances
{
    public class CurrentTile : GlobalTile
    {
        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
        }

        public override void SetDefaults()
        {
            if (Main.player[Main.myPlayer].HeldItem.type == ItemID.CarriageLantern)
                Main.placementPreview = false;
        }

        public override void FloorVisuals(int type, Player player)
        {
            var grassTiles = new[]
            {
                TileID.Grass,
                TileID.Dirt,
                TileID.BlueMoss,
                TileID.BrownMoss,
                TileID.GreenMoss,
                TileID.LavaMoss,
                TileID.LongMoss,
                TileID.PurpleMoss,
                TileID.RedMoss,
                TileID.Silt,
                TileID.Slush,
                TileID.JungleGrass,
                TileID.Mud,
                TileID.CorruptGrass,
                TileID.FleshGrass,
                TileID.HallowedGrass,
                TileID.LivingMahoganyLeaves,
                TileID.LeafBlock,
                TileID.MushroomGrass,
                TileID.ClayBlock,
                TileID.Ash,
                TileID.Sand,
                TileID.Ebonsand,
                TileID.Pearlsand,
                TileID.Crimsand,
                TileID.Cloud,
                TileID.Asphalt,
                TileID.RainCloud,
                TileID.HoneyBlock,
                TileID.CrispyHoneyBlock,
                TileID.PumpkinBlock,
                TileID.SnowBlock,
            };


            if (grassTiles.Any(x => x == type))
                player.GetModPlayer<SpookyPlayer>().isOnGrassyTile = true;
            else
                player.GetModPlayer<SpookyPlayer>().isOnGrassyTile = false;

            var stoneBlocks = new[]
            {
                TileID.Stone,
                TileID.StoneSlab,
                TileID.ActiveStoneBlock,
                TileID.GrayBrick,
                TileID.IceBlock,
                TileID.HallowedIce,
                TileID.CorruptIce,
                TileID.FleshIce,
                TileID.PinkDungeonBrick,
                TileID.GreenDungeonBrick,
                TileID.BlueDungeonBrick,
                TileID.Granite,
                TileID.Marble,
                TileID.MarbleBlock,
                TileID.GraniteBlock,
                TileID.Diamond,
                TileID.DiamondGemspark,
                TileID.DiamondGemsparkOff,
                TileID.Ruby,
                TileID.RubyGemspark,
                TileID.RubyGemsparkOff,
                TileID.Topaz,
                TileID.TopazGemspark,
                TileID.TopazGemsparkOff,
                TileID.Sapphire,
                TileID.SapphireGemspark,
                TileID.SapphireGemsparkOff,
                TileID.Amethyst,
                TileID.AmethystGemspark,
                TileID.AmethystGemsparkOff,
                TileID.Emerald,
                TileID.EmeraldGemspark,
                TileID.EmeraldGemsparkOff,
                TileID.AmberGemspark,
                TileID.AmberGemsparkOff,
                TileID.LihzahrdBrick,
                TileID.Ebonstone,
                TileID.EbonstoneBrick,
                TileID.FleshBlock,
                TileID.Crimstone,
                TileID.CrimsonSandstone,
                TileID.CorruptHardenedSand,
                TileID.CorruptSandstone,
                TileID.CrimsonHardenedSand,
                TileID.HardenedSand,
                TileID.Sandstone,
                TileID.HallowSandstone,
                TileID.SandstoneBrick,
                TileID.SandStoneSlab,
                TileID.HallowHardenedSand,
                TileID.Sunplate,
                TileID.Obsidian,
                TileID.Pearlstone,
                TileID.PearlstoneBrick,
                TileID.Mudstone,
                TileID.IridescentBrick,
                TileID.CobaltBrick,
                TileID.MythrilBrick,
                TileID.MythrilAnvil,
                TileID.Adamantite,
                TileID.Mythril,
                TileID.Cobalt,
                TileID.Titanium,
                TileID.Titanstone,
                TileID.Palladium,
                TileID.MetalBars,
                TileID.LunarOre,
                TileID.ObsidianBrick,
                TileID.SnowBrick,
                TileID.GreenCandyCaneBlock,
                TileID.CandyCaneBlock,
                TileID.GrayStucco,
                TileID.GreenStucco,
                TileID.RedStucco,
                TileID.YellowStucco,
                TileID.Copper,
                TileID.CopperBrick,
                TileID.Tin,
                TileID.TinBrick,
                TileID.Silver,
                TileID.SilverBrick,
                TileID.Tungsten,
                TileID.TungstenBrick,
                TileID.Iron,
                TileID.Lead,
                TileID.Gold,
                TileID.GoldBrick,
                TileID.Platinum,
                TileID.PlatinumBrick,
                TileID.Hellstone,
                TileID.HellstoneBrick
            };

            // TODO: Include ore bricks later, as well as the ore themselves
            if (stoneBlocks.Any(x => x == type))
                player.GetModPlayer<SpookyPlayer>().isOnStoneTile = true;
            else
                player.GetModPlayer<SpookyPlayer>().isOnStoneTile = false;

            if (!player.GetModPlayer<SpookyPlayer>().isOnStoneTile && !player.GetModPlayer<SpookyPlayer>().isOnGrassyTile)
                player.GetModPlayer<SpookyPlayer>().isOnWoodTile = true;
            else
                player.GetModPlayer<SpookyPlayer>().isOnWoodTile = false;
        }
    }
}