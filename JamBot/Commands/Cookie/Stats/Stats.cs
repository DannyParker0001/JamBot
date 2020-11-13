using Discord.WebSocket;
using JamBot.Database;
using JamBot.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JamBot.Commands.Cookie.Stats
{
    class Stats : Command
    {
        public override string commandname { get { return "stats"; } }

        public override Permissions permissionsNeeded
        {
            get { return Permissions.None; }
        }

        public override async Task Execute(string[] args, SocketMessage message)
        {

            ulong userid = 0;
            ServerUser user = null;
            if (args.Length == 1)
            {
                userid = message.Author.Id;
                user = DBUtil.UserFromMessage(message);
            }
            else
            {
                if (args[1][0] == '<')
                {
                    userid = ulong.Parse(args[1].Substring(3, args[1].Length - 4));
                }
                else
                {
                    userid = ulong.Parse(args[1]);
                }
                user = DBUtil.UserFromIDandMessage(message, userid);
                if (user == null)
                {
                    await message.Channel.SendMessageAsync("That user doesnt have a cookie jar");
                }
            }
            await message.Channel.SendMessageAsync("", false, EmbedUtil.GetEmbed("<@!" + userid + "> Has " + user.CookieCount + " Cookies in their jar"));
        }
    }
}
