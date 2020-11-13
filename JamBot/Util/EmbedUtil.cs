using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JamBot.Util
{
    class EmbedUtil
    {
        public static Random rand = new Random();
        public static byte[] bytestore = new byte[3];
        public static Embed GetEmbed(string message)
        {
            EmbedBuilder builder = new EmbedBuilder();
            rand.NextBytes(bytestore);
            builder.WithColor(bytestore[0], bytestore[1], bytestore[2]);
            //builder.WithTitle("JamBot");
            builder.WithDescription(message);
            return builder.Build();
        }
    }
}
