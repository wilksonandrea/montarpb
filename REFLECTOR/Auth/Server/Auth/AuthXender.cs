namespace Server.Auth
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.XML;
    using Server.Auth.Data.Sync;
    using System;
    using System.Collections.Concurrent;
    using System.Runtime.CompilerServices;

    public class AuthXender
    {
        public static bool GetPlugin(string Host, int Port)
        {
            try
            {
                SocketSessions = new ConcurrentDictionary<int, AuthClient>();
                SocketConnections = new ConcurrentDictionary<string, int>();
                Sync = new AuthSync(SynchronizeXML.GetServer(Port).Connection);
                Client = new AuthManager(0, Host, ConfigLoader.DEFAULT_PORT[0]);
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

        public static AuthSync Sync { get; set; }

        public static AuthManager Client { get; set; }

        public static ConcurrentDictionary<int, AuthClient> SocketSessions { get; set; }

        public static ConcurrentDictionary<string, int> SocketConnections { get; set; }
    }
}

