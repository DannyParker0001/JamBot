using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JamBot.Database
{
   public class DBUtil
    {
        public static ServerUser UserFromMessage(SocketMessage message)
        {

            SocketGuildChannel channel = (SocketGuildChannel)message.Channel;
            return Program.db.Servers[channel.Guild.Id].Users[message.Author.Id];        }

        public static ServerUser UserFromIDandMessage(SocketMessage message, ulong userid)
        {

            SocketGuildChannel channel = (SocketGuildChannel)message.Channel;
            ServerUser userdata = null;
            
            Program.db.Servers[channel.Guild.Id].Users.TryGetValue(userid, out userdata);
            return userdata;
            
        }
    }
}
