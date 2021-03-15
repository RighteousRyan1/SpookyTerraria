using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace SpookyTerraria
{
    public class SpookyConfigClient : ModConfig
    {
        [Label("Spooky Terraria Settings (Client Side)")]
        public override ConfigScope Mode => ConfigScope.ClientSide;
        [Header("Ambience Settings")]
        [Label("Winds/Breezes")]
        [DefaultValue(true)]
        [Tooltip("Toggle Winds blowing.")]
        public bool toggleWind;

        [Label("Owl Hoots")]
        [DefaultValue(true)]
        [Tooltip("Toggle owl hooting.")]
        public bool toggleHoots;

        [Label("Enable Experimental Features")]
        [DefaultValue(false)]
        [Tooltip("Enables experimental features that may or may not work properly. Use at your own risk.")]
        public bool expFeatures;

        [Label("UI Revamp")]
        [DefaultValue(true)]
        [Tooltip("Completely overhauls Terraria UI to look a little better")]
        [ReloadRequired]
        public bool betterUI;

        [Header("Music Changes")]
        [Label("Pages Music Change")]
        [DrawTicks]
        [OptionStrings(new string[] { "The 8 Pages", "Slender's Shadow", "7th Street" })]
        [DefaultValue("The 8 Pages")]
        public string musicType;

        // TODO: 7th Street Music, Config finishing, teleportation noises
    }
    public class SpookyConfigServer : ModConfig
    {
        [Label("Spooky Terraria Settings (Server Side)")]
        public override ConfigScope Mode => ConfigScope.ServerSide;
        [Header("Spookiness Settings")]
        [Label("Light Scalar")]
        [DefaultValue(0.9f)]
        [Range(0f, 1f)]
        public float lightingScale;

        [Label("Modify Spawn Pool")]
        [DefaultValue(true)]
        [Tooltip("Removes all spawns besides Spooky Terraria NPCs. For when you want to feel isolated.")]
        public bool excludeAllNPCsBesideCreepyTerrariaOnesFromSpawnPool;

        [Header("Debug Mode")]
        [Label("Debug Mode")]
        [DefaultValue(false)]
        [Tooltip("If on, enables debug mode. Please do not use this to abuse your playthroughs.")]
        public bool debugMode;
    }
}