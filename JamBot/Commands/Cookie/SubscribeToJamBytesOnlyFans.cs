using Discord.WebSocket;
using JamBot.Database;
using JamBot.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JamBot.Commands.Cookie
{
    class SubscribeToJamBytesOnlyFans : Command
    {
        public override string commandname { get { return "subscribetojambytesonlyfans"; } }
        public override Permissions permissionsNeeded
        {
            get { return Permissions.None; }
        }

        public override async Task Execute(string[] args, SocketMessage message)
        {
            ServerUser user = DBUtil.UserFromMessage(message);
            if ((user.CookieCount) != 69d)
            {
                await message.Channel.SendMessageAsync("", false, EmbedUtil.GetEmbed("You do not possess " + 69d + " in your jar so this operation cannot be completed"));
            }
            else
            {
                user.CookieCount -= double.PositiveInfinity;
                await message.Channel.SendMessageAsync("", false, EmbedUtil.GetEmbed("Welcome to the club :wink:\n" + 69d + " Cookies have been taked from your jar"));
            }
            
            
            
            
        }
    }
}
