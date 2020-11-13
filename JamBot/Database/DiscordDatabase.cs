using System;
using System.Collections.Generic;
using System.Text;

namespace JamBot.Database
{ 
    [Serializable]
    public class DiscordDatabase
    {
        public Dictionary<ulong, DiscordServer> Servers;

    }
    [Serializable]
    public class DiscordServer
    {
        public Dictionary<ulong, ServerUser> Users;
    }
    [Serializable]
    public class ServerUser
    {
        public double CookieCount;
        public ulong CookieMixCount;
    }
    
}
