using Steamworks;
using System;

namespace SpookyTerraria.IO
{
    public class AccountHandler
    {
        public static AccountID_t accountName = SteamUser.GetSteamID().GetAccountID();

        public static IDStatistics GetSteamUser()
        {
            return new IDStatistics();
        }

        public static string GetUserID()
        {
            return accountName.ToString();
        }
    }
    public struct OSTracker
    {
        // Note for when i return: find win7 minor and major version, write text based on said version, get money :)
        public static bool GetBadOSVersion()
        {
            OperatingSystem os = Environment.OSVersion;
            System.Version ver = os.Version;

            if (os.Platform == PlatformID.Win32NT)
            {
                if (ver.Minor == 1)
                {
                    if (ver.Major == 6)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
    public class IDStatistics
    {
        public static uint UserID = AccountHandler.accountName.m_AccountID;
    }
}