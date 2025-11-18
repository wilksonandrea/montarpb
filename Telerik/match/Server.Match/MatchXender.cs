using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.XML;
using Server.Match.Data.Sync;
using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;

namespace Server.Match
{
	public class MatchXender
	{
		public static MatchManager Client
		{
			get;
			set;
		}

		public static ConcurrentDictionary<string, int> SpamConnections
		{
			get;
			set;
		}

		public static MatchSync Sync
		{
			get;
			set;
		}

		public static ConcurrentDictionary<IPEndPoint, Socket> UdpClients
		{
			get;
			set;
		}

		public MatchXender()
		{
		}

		public static bool GetPlugin(string Host, int Port)
		{
			bool flag;
			try
			{
				MatchXender.SpamConnections = new ConcurrentDictionary<string, int>();
				MatchXender.UdpClients = new ConcurrentDictionary<IPEndPoint, Socket>();
				MatchXender.Sync = new MatchSync(SynchronizeXML.GetServer(Port).Connection);
				MatchXender.Client = new MatchManager(Host, Port);
				MatchXender.Sync.Start();
				MatchXender.Client.Start();
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}
	}
}