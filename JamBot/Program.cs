using Discord;
using Discord.WebSocket;
using JamBot.Commands;
using JamBot.Database;
using JamBot.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
namespace JamBot
{

    public class Program
    {
        private DiscordSocketClient _client;
        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();
        
        public static Dictionary<string, Command> commands;
        public const string prefix = "|";
        public static DiscordDatabase db;
        public async Task MainAsync()
        {

            //idk 
            commands = new Dictionary<string, Command>();
            var eee = ReflectiveEnumerator.GetEnumerableOfType<Command>();
            foreach(var command in eee)
            {
                commands.Add(command.commandname, command);
            }
            if (File.Exists(@"test.bin"))
            {
                Stream stream = new FileStream(@"test.bin", FileMode.Open, FileAccess.Read);
                IFormatter formatter = new BinaryFormatter();
#pragma warning disable SYSLIB0011 // Type or member is obsolete
                db = (DiscordDatabase)formatter.Deserialize(stream);//get fucked ms

#pragma warning restore SYSLIB0011 // Type or member is obsolete
            }
            else
            {
                db = new DiscordDatabase();
                db.Servers = new Dictionary<ulong, DiscordServer>();
               
            }
            _client = new DiscordSocketClient();
            _client.Log += Log;
            _client.MessageReceived += MessageReceivedAsync;
            String tok = File.ReadAllText("Token.txt");
            await _client.LoginAsync(TokenType.Bot, tok);
            await _client.StartAsync();
          
            // Block this task until the program is closed.
            await Task.Delay(-1);
        }
        public void LoadCommands()
        {
        }
       
        private async Task MessageReceivedAsync(SocketMessage message)
        {
            SocketGuildChannel channel = (SocketGuildChannel) message.Channel;
            if (!db.Servers.ContainsKey(channel.Guild.Id))
            {
                DiscordServer x = new DiscordServer();
                x.Users = new Dictionary<ulong, ServerUser>();

                db.Servers.Add(channel.Guild.Id, x);
            }
            DiscordServer server = db.Servers[channel.Guild.Id];
            if (!server.Users.ContainsKey(message.Author.Id))
            {
                ServerUser x = new ServerUser();
                x.CookieCount = 0;
                server.Users.Add(message.Author.Id, x);
            }
            if (message.Content.StartsWith(prefix))
            {
                string[] args = message.Content.Split(' ');
                args[0] = args[0].Substring(prefix.Length, args[0].Length-prefix.Length).ToLower();
                if(commands.ContainsKey(args[0]))
                {
                    Command command = commands[args[0]];
                    Permissions permissions= command.permissionsNeeded;
                    bool haspermissions = false;
                    switch (permissions)
                    {
                        case Permissions.Jam:
                            if(message.Author.Id == PermissionsDB.Jamid)
                            {
                                haspermissions = true;
                            }
                            break;
                        case Permissions.JamBotAdmins:
                            if (PermissionsDB.JamBotAdmins.Contains(message.Author.Id))
                            {
                                haspermissions = true;
                            }
                            break;
                        case Permissions.ServerAdmin:
                            SocketGuildUser user =(SocketGuildUser) message.Author;
                            if (user.GuildPermissions.Administrator)
                            {
                                haspermissions = true;
                            }
                            break;
                        case Permissions.None:
                            haspermissions = true;
                            break;
                        default:
                            haspermissions = false;
                            break;
                    }
                    if (haspermissions)
                    {
                        await commands[args[0]].Execute(args, message);
                    }
                    else
                    {
                        await message.Channel.SendMessageAsync("", false, EmbedUtil.GetEmbed("You lack permissions to use this command"));
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                //cookie mix 
                DBUtil.UserFromMessage(message).CookieMixCount++;
            }










            //if (message.Content == "!ping")
            //{
            //    await message.Channel.SendMessageAsync("Pong!");
            //}
            //if (message.Content == "!savedatabase")
            //{

            //    DiscordDatabase db = new DiscordDatabase();
            //    for (ulong i = 0; i < 1000; i++)
            //    {
            //        db.Servers = new Dictionary<ulong, DiscordServer>();
            //        db.Servers.Add(i, new DiscordServer());
            //        db.Servers[i].Users = new Dictionary<ulong, ServerUser>();
            //        for (ulong j = 0; j < 1000; j++)
            //        {

            //            db.Servers[i].Users.Add(j, new ServerUser());
            //            ServerUser su = new ServerUser();
            //            su.CookieCount = 1000;
            //            db.Servers[i].Users[j] = su;
            //        }
            //    }


            //    Stopwatch stopwatch = new Stopwatch();
            //    stopwatch.Start();
            //    IFormatter formatter = new BinaryFormatter();
            //    Stream stream = new FileStream(@"test.bin", FileMode.Create, FileAccess.Write);

            //    formatter.Serialize(stream, db);
            //    stream.Close();
            //    stopwatch.Stop();
            //    await message.Channel.SendMessageAsync("Saved!, Took " + stopwatch.ElapsedMilliseconds + "ms");
            //}
            //if (message.Content == "!loaddatabase")
            //{

            //    Stopwatch stopwatch = new Stopwatch();
            //    stopwatch.Start();
            //    Stream stream = new FileStream(@"test.bin", FileMode.Open, FileAccess.Read);
            //    IFormatter formatter = new BinaryFormatter();
            //    DiscordDatabase d = (DiscordDatabase)formatter.Deserialize(stream);
            //    stream.Close();
            //    stopwatch.Stop();
            //    await message.Channel.SendMessageAsync("Loaded!, Took " + stopwatch.ElapsedMilliseconds + "ms");
            //}
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
