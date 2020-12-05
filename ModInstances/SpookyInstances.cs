using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace SpookyTerraria
{
    public class SpookyConfigClient : ModConfig
    {
        [Label("Spooky Terraria Settings (Client Side)")]
        public override ConfigScope Mode => ConfigScope.ClientSide;
        [Header("Ambience Settings")]
        [Label("Toggle Winds")]
        [DefaultValue(true)]
        [Tooltip("Winds no longer blow...")]
        public bool toggleWind;

        [Label("Toggle Owl Hoots")]
        [DefaultValue(true)]
        [Tooltip("Owls no longer hoot.")]
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
        [DefaultValue(false)]
        [Tooltip("Removes all spawns besides Spooky Terraria NPCs. For when you want to feel isolated.")]
        public bool excludeAllNPCsBesideCreepyTerrariaOnesFromSpawnPool;

        [Header("Debug Mode")]
        [Label("Debug Mode")]
        [DefaultValue(false)]
        [Tooltip("If on, enables debug mode. Please do not use this to abuse your playthroughs.")]
        public bool debugMode;
    }
}