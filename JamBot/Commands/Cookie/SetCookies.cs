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
    class SetCookies : Command
    {
        public override string commandname
        {
            get { return "setcookies"; }
        }
        public override Permissions permissionsNeeded
        {
            get { return Permissions.JamBotAdmins; }
        }

        public override async Task Execute(string[] args, SocketMessage message)
        {
            ServerUser user = DBUtil.UserFromMessage(message);
            user.CookieCount = double.Parse(args[1]);
            await message.Channel.SendMessageAsync("", false, EmbedUtil.GetEmbed("You have set your cookies to " + user.CookieCount));
        }
    }
}
