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
    class Bake : Command
    {
        public override string commandname { get { return "bake"; } }

        public override Permissions permissionsNeeded
        {
            get { return Permissions.None; }
        }

        public override async Task Execute(string[] args, SocketMessage message)
        {
            ServerUser user = DBUtil.UserFromMessage(message);
            user.CookieCount++;
            await message.Channel.SendMessageAsync("", false, EmbedUtil.GetEmbed("You Bake a single Cookie, You now have " + user.CookieCount + " in your jar"));
        }
    }
}
