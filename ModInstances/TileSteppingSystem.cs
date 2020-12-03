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
        public override bool Drop(int i, int j, int type)
        {
            return base.Drop(i, j, type);
        }
        public override bool KillSound(int i, int j, int type)
        {
            return base.KillSound(i, j, type);
        }
        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
        }
        public override void SetDefaults()
        {
            if (Main.player[Main.myPlayer].HeldItem.type == ItemID.CarriageLantern)
            {
                Main.placementPreview = false;
            }
        }
        public override void FloorVisuals(int type, Player player)
        {
            if (type == TileID.Grass || type == TileID.Dirt || type == TileID.BlueMoss || type == TileID.BrownMoss
                 || type == TileID.GreenMoss
                  || type == TileID.LavaMoss
                   || type == TileID.LongMoss
                    || type == TileID.PurpleMoss
                     || type == TileID.RedMoss
                      || type == TileID.Silt
                       || type == TileID.Slush
                        || type == TileID.JungleGrass
                         || type == TileID.Mud
                          || type == TileID.CorruptGrass
                           || type == TileID.FleshGrass
                            || type == TileID.HallowedGrass
                             || type == TileID.LivingMahoganyLeaves
                              || type == TileID.LeafBlock
                               || type == TileID.MushroomGrass
                               || type == TileID.ClayBlock
                               || type == TileID.Ash
                               || type == TileID.Sand
                               || type == TileID.Ebonsand
                               || type == TileID.Pearlsand
                               || type == TileID.Crimsand
                               || type == TileID.Cloud
                               || type == TileID.Asphalt
                               || type == TileID.RainCloud
                               || type == TileID.HoneyBlock
                               || type == TileID.CrispyHoneyBlock
                               || type == TileID.PumpkinBlock
                               || type == TileID.SnowBlock)
            {
                player.GetModPlayer<SpookyPlayer>().isOnGrassyTile = true;
            }
            else
            {
                player.GetModPlayer<SpookyPlayer>().isOnGrassyTile = false;
            }
            // TODO: Include ore bricks later, as well as the ore themselves
            if (type == TileID.Stone || type == TileID.StoneSlab || type == TileID.ActiveStoneBlock || type == TileID.GrayBrick
                 || type == TileID.IceBlock
                  || type == TileID.HallowedIce
                   || type == TileID.CorruptIce
                    || type == TileID.FleshIce
                     || type == TileID.PinkDungeonBrick
                      || type == TileID.GreenDungeonBrick
                       || type == TileID.BlueDungeonBrick
                        || type == TileID.Granite
                         || type == TileID.Marble
                          || type == TileID.MarbleBlock
                           || type == TileID.GraniteBlock
                            || type == TileID.Diamond
                             || type == TileID.DiamondGemspark
                              || type == TileID.DiamondGemsparkOff
                               || type == TileID.Ruby
                               || type == TileID.RubyGemspark
                               || type == TileID.RubyGemsparkOff
                               || type == TileID.Topaz
                               || type == TileID.TopazGemspark
                               || type == TileID.TopazGemsparkOff
                               || type == TileID.Sapphire
                               || type == TileID.SapphireGemspark
                               || type == TileID.SapphireGemsparkOff
                               || type == TileID.Amethyst
                               || type == TileID.AmethystGemspark
                               || type == TileID.AmethystGemsparkOff
                               || type == TileID.Emerald
                               || type == TileID.EmeraldGemspark
                               || type == TileID.EmeraldGemsparkOff
                               || type == TileID.AmberGemspark
                               || type == TileID.AmberGemsparkOff
                               || type == TileID.LihzahrdBrick
                               || type == TileID.Ebonstone
                               || type == TileID.EbonstoneBrick
                               || type == TileID.FleshBlock
                               || type == TileID.Crimstone
                               || type == TileID.CrimsonSandstone
                               || type == TileID.CorruptHardenedSand
                               || type == TileID.CorruptSandstone
                               || type == TileID.CrimsonHardenedSand
                               || type == TileID.HardenedSand
                               || type == TileID.Sandstone
                               || type == TileID.HallowSandstone
                               || type == TileID.SandstoneBrick
                               || type == TileID.SandStoneSlab
                               || type == TileID.HallowHardenedSand
                               || type == TileID.Sunplate
                               || type == TileID.Obsidian
                               || type == TileID.Pearlstone
                               || type == TileID.PearlstoneBrick
                               || type == TileID.Mudstone
                               || type == TileID.IridescentBrick
                               || type == TileID.CobaltBrick
                               || type == TileID.MythrilBrick
                               || type == TileID.MythrilAnvil
                               || type == TileID.Adamantite
                               || type == TileID.Mythril
                               || type == TileID.Cobalt
                               || type == TileID.Titanium
                               || type == TileID.Titanstone
                               || type == TileID.Palladium
                               || type == TileID.MetalBars
                               || type == TileID.LunarOre
                               || type == TileID.ObsidianBrick
                               || type == TileID.SnowBrick
                               || type == TileID.GreenCandyCaneBlock
                               || type == TileID.CandyCaneBlock
                               || type == TileID.GrayStucco
                               || type == TileID.GreenStucco
                               || type == TileID.RedStucco
                               || type == TileID.YellowStucco
                               || type == TileID.Copper
                               || type == TileID.CopperBrick
                               || type == TileID.Tin
                               || type == TileID.TinBrick
                               || type == TileID.Silver
                               || type == TileID.SilverBrick
                               || type == TileID.Tungsten
                               || type == TileID.TungstenBrick
                               || type == TileID.Iron
                               || type == TileID.Lead
                               || type == TileID.Gold
                               || type == TileID.GoldBrick
                               || type == TileID.Platinum
                               || type == TileID.PlatinumBrick
                               || type == TileID.Hellstone
                               || type == TileID.HellstoneBrick)

            {
                player.GetModPlayer<SpookyPlayer>().isOnStoneTile = true;
            }
            else
            {
                player.GetModPlayer<SpookyPlayer>().isOnStoneTile = false;
            }
            if (!player.GetModPlayer<SpookyPlayer>().isOnStoneTile && !player.GetModPlayer<SpookyPlayer>().isOnGrassyTile)
            {
                player.GetModPlayer<SpookyPlayer>().isOnWoodTile = true;
            }
            else
            {
                player.GetModPlayer<SpookyPlayer>().isOnWoodTile = false;
            }
        }
    }
}