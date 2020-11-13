using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JamBot.Commands
{
    public abstract class Command
    {
        public abstract string commandname { get; }
        public abstract Task Execute(string[] args, SocketMessage message);
        public abstract Permissions permissionsNeeded { get; }
        
    }
    public enum Permissions
    {
        Jam,
        JamBotAdmins,
        ServerAdmin,
        None
    }
    public class PermissionsDB
    {
        public const ulong Jamid = 308209615911387139;
        public static readonly ulong[] JamBotAdmins = { 308209615911387139 };
    }
}
