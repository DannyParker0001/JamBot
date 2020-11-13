using Discord.WebSocket;
using JamBot.Commands.DataBase;
using JamBot.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JamBot.Commands.JamBotUtil
{
    class ShutDown : Command
    {
        public override string commandname { get { return "shutdown"; } }
        public override Permissions permissionsNeeded
        {
            get { return Permissions.Jam; }
        }

        public override async Task Execute(string[] args, SocketMessage message)
        {
            await message.Channel.SendMessageAsync("", false, EmbedUtil.GetEmbed("Saving database then yeeting myself"));
            SaveDataBase x = new SaveDataBase();
            await x.Execute(args, message);
            System.Environment.Exit(0);
        }
    }
}
