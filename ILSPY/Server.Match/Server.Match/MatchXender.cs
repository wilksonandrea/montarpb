using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.XML;
using Server.Match.Data.Sync;

namespace Server.Match;

public class MatchXender
{
	[CompilerGenerated]
	private static MatchSync matchSync_0;

	[CompilerGenerated]
	private static MatchManager matchManager_0;

	[CompilerGenerated]
	private static ConcurrentDictionary<string, int> concurrentDictionary_0;

	[CompilerGenerated]
	private static ConcurrentDictionary<IPEndPoint, Socket> concurrentDictionary_1;

	public static MatchSync Sync
	{
		[CompilerGenerated]
		get
		{
			return matchSync_0;
		}
		[CompilerGenerated]
		set
		{
			matchSync_0 = value;
		}
	}

	public static MatchManager Client
	{
		[CompilerGenerated]
		get
		{
			return matchManager_0;
		}
		[CompilerGenerated]
		set
		{
			matchManager_0 = value;
		}
	}

	public static ConcurrentDictionary<string, int> SpamConnections
	{
		[CompilerGenerated]
		get
		{
			return concurrentDictionary_0;
		}
		[CompilerGenerated]
		set
		{
			concurrentDictionary_0 = value;
		}
	}

	public static ConcurrentDictionary<IPEndPoint, Socket> UdpClients
	{
		[CompilerGenerated]
		get
		{
			return concurrentDictionary_1;
		}
		[CompilerGenerated]
		set
		{
			concurrentDictionary_1 = value;
		}
	}

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
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}
}
