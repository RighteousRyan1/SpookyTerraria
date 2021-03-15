using DiscordRPC;
using DiscordRPC.Message;
using DiscordRPC.Logging;
using DiscordRPC.IO;
using DiscordRPC.Helper;
using DiscordRPC.Exceptions;
using DiscordRPC.Events;

using Terraria;
using Terraria.ModLoader;

using System.Linq;
using System;

using SpookyTerraria.Utilities;

namespace SpookyTerraria.IO
{
    // Huge thanks to Agrair on GitHub for code and documentation/comments. 
    public class DiscordRPCInfo
    {
        // Mod mod = ModContent.GetInstance<SpookyTerraria>();

        public User discordUser;

        public string userName;
        public ulong ID
        {
            get;
            internal set;
        }

        private static RichPresence _rpc;

        public static DiscordRpcClient rpcClient;

        public static void Load()
        {
            _rpc = new RichPresence()
            {
                Details = "Collecting Pages",
            };
            rpcClient = new DiscordRpcClient("689253540505714732");
            _rpc.Timestamps = new Timestamps()
            {
                Start = DateTime.UtcNow,
            };
            rpcClient?.SetPresence(_rpc);
            rpcClient.Initialize();
        }
        public static Player RPCPlayer;
        public static void Update()
        {
            rpcClient?.Invoke();

            if (Main.menuMode == MenuModeID.HowToPlay)
            {
                _rpc.Details = $"Viewing how to Play";
            }
            if (Main.menuMode == MenuModeID.SlenderChangeLogs)
            {
                _rpc.Details = $"Looking at the Change Logs";
            }
            if (!Main.gameMenu)
            {
                if (RPCPlayer != null)
                {
                    if (!RPCPlayer.dead)
                    {
                        _rpc.Assets = new Assets()
                        {
                            LargeImageKey = "spookyterraria",
                            SmallImageKey = "paper",
                            SmallImageText = $"{SpookyPlayer.Pages} of 8 Pages",
                            LargeImageText = $"Player Name: {RPCPlayer.name}"
                        };
                        _rpc.Details = "In World - Collecting Pages";
                    }
                    else
                    {
                        _rpc.Assets = new Assets()
                        {
                            LargeImageKey = "spookyterraria",
                            SmallImageKey = "paper",
                            SmallImageText = $"Player is dead: Had {SpookyPlayer.Pages} out of 8 pages",
                            LargeImageText = $"{RPCPlayer.name}"
                        };
                        _rpc.Details = "Player Dead, Game Over";
                    }
                }
                else
                {
                    _rpc.Assets = new Assets()
                    {
                        LargeImageKey = "spookyterraria",
                        SmallImageKey = "paper",
                        SmallImageText = $"null (please report this)",
                        LargeImageText = $"null (please report this)"
                    };
                }
            }
            else if (Main.menuMode != MenuModeID.HowToPlay
                && Main.menuMode != MenuModeID.SlenderChangeLogs
                && Main.menuMode != MenuModeID.SlenderModesMenu)
            {
                _rpc.Assets = new Assets()
                {
                    LargeImageKey = "spookyterraria",
                    LargeImageText = $"Username: {Steamworks.SteamFriends.GetPersonaName()}"
                };
                _rpc.Details = $"Browsing the Menus";
                _rpc.State = "Playing the Spooky Terraria Mod";
            }
            if (!rpcClient.IsDisposed)
            {
                rpcClient?.SetPresence(_rpc);
            }
        }
        public static void Terminate()
        {
            rpcClient?.UpdateEndTime(DateTime.UtcNow);
            rpcClient?.Dispose();
        }
    }
}
