using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.XML;
using Server.Auth.Data.Sync;
using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace Server.Auth
{
	public class AuthXender
	{
		public static AuthManager Client
		{
			get;
			set;
		}

		public static ConcurrentDictionary<string, int> SocketConnections
		{
			get;
			set;
		}

		public static ConcurrentDictionary<int, AuthClient> SocketSessions
		{
			get;
			set;
		}

		public static AuthSync Sync
		{
			get;
			set;
		}

		public AuthXender()
		{
		}

		public static bool GetPlugin(string Host, int Port)
		{
			bool flag;
			try
			{
				AuthXender.SocketSessions = new ConcurrentDictionary<int, AuthClient>();
				AuthXender.SocketConnections = new ConcurrentDictionary<string, int>();
				AuthXender.Sync = new AuthSync(SynchronizeXML.GetServer(Port).Connection);
				AuthXender.Client = new AuthManager(0, Host, ConfigLoader.DEFAULT_PORT[0]);
				AuthXender.Sync.Start();
				AuthXender.Client.Start();
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