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

        /*
        [Label("Toggle Note Collecting")]
        [DefaultValue(false)]
        [Tooltip("The objective of the game normally is to collect 300 notes from the form of blocks. If this is on, then your objective is to beat the Moon Lord")]
        public bool normalProgression;
        */

        [Label("Modify Spawn Pool")]
        [DefaultValue(false)]
        [Tooltip("Removes all spawns besides Spooky Terraria NPCs. For when you want to feel isolated.")]
        public bool excludeAllNPCsBesideCreepyTerrariaOnesFromSpawnPool;
    }
}