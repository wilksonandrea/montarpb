using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.XML;
using Server.Auth.Data.Sync;

namespace Server.Auth;

public class AuthXender
{
	[CompilerGenerated]
	private static AuthSync authSync_0;

	[CompilerGenerated]
	private static AuthManager authManager_0;

	[CompilerGenerated]
	private static ConcurrentDictionary<int, AuthClient> concurrentDictionary_0;

	[CompilerGenerated]
	private static ConcurrentDictionary<string, int> concurrentDictionary_1;

	public static AuthSync Sync
	{
		[CompilerGenerated]
		get
		{
			return authSync_0;
		}
		[CompilerGenerated]
		set
		{
			authSync_0 = value;
		}
	}

	public static AuthManager Client
	{
		[CompilerGenerated]
		get
		{
			return authManager_0;
		}
		[CompilerGenerated]
		set
		{
			authManager_0 = value;
		}
	}

	public static ConcurrentDictionary<int, AuthClient> SocketSessions
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

	public static ConcurrentDictionary<string, int> SocketConnections
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
			SocketSessions = new ConcurrentDictionary<int, AuthClient>();
			SocketConnections = new ConcurrentDictionary<string, int>();
			Sync = new AuthSync(SynchronizeXML.GetServer(Port).Connection);
			Client = new AuthManager(0, Host, ConfigLoader.DEFAULT_PORT[0]);
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
