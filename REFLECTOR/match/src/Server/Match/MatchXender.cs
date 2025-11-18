namespace Server.Match
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.XML;
    using Server.Match.Data.Sync;
    using System;
    using System.Collections.Concurrent;
    using System.Runtime.CompilerServices;

    public class MatchXender
    {
        public static bool GetPlugin(string Host, int Port)
        {
            try
            {
                SpamConnections = new ConcurrentDictionary<string, int>();
                UdpClients = new ConcurrentDictionary<IPEndPoint, Socket>();
                Sync = new MatchSync(SynchronizeXML.GetServer(Port).Connection);
                Client = new MatchManager(Host, Port);
                Sync.Start();
                Client.Start();
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static MatchSync Sync { get; set; }

        public static MatchManager Client { get; set; }

        public static ConcurrentDictionary<string, int> SpamConnections { get; set; }

        public static ConcurrentDictionary<IPEndPoint, Socket> UdpClients { get; set; }
    }
}

