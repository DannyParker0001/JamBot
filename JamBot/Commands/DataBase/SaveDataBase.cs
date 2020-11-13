using Discord.WebSocket;
using JamBot.Database;
using JamBot.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace JamBot.Commands.DataBase
{
    class SaveDataBase : Command
    {
        public override string commandname { get { return "savedatabase"; } }
        public override Permissions permissionsNeeded
        {
            get { return Permissions.JamBotAdmins; }
        }

        public override async Task Execute(string[] args, SocketMessage message)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(@"test.bin", FileMode.Create, FileAccess.Write);

#pragma warning disable SYSLIB0011 // Type or member is obsolete
            formatter.Serialize(stream, Program.db);
#pragma warning restore SYSLIB0011 // Type or member is obsolete
            stream.Close();
            stopwatch.Stop();
            await message.Channel.SendMessageAsync("", false, EmbedUtil.GetEmbed("Saved!, Took " + stopwatch.ElapsedMilliseconds + "ms"));
        }
    }
}
