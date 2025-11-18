using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.JSON;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Server.Game
{
	public class GameManager
	{
		private readonly string string_0;

		private readonly int int_0;

		public readonly int ServerId;

		public ServerConfig Config;

		public Socket MainSocket;

		public bool ServerIsClosed;

		public GameManager(int int_1, string string_1, int int_2)
		{
			this.string_0 = string_1;
			this.int_0 = int_2;
			this.ServerId = int_1;
		}

		public void AddSession(GameClient Client)
		{
			try
			{
				if (Client != null)
				{
					DateTime dateTime = DateTimeUtil.Now();
					int ınt32 = 1;
					while (ınt32 < 100000)
					{
						if (GameXender.SocketSessions.ContainsKey(ınt32) || !GameXender.SocketSessions.TryAdd(ınt32, Client))
						{
							ınt32++;
						}
						else
						{
							Client.SessionDate = dateTime;
							Client.SessionId = ınt32;
							Client.SessionSeed = (ushort)(new Random(dateTime.Millisecond)).Next(ınt32, 32767);
							Client.StartSession();
							return;
						}
					}
					CLogger.Print(string.Format("Unable to add session list. IPAddress: {0}; Date: {1}", Client.GetIPAddress(), dateTime), LoggerType.Warning, null);
					Client.Close(500, true, false);
				}
				else
				{
					CLogger.Print("Destroyed after failed to add to list.", LoggerType.Warning, null);
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}

		public int KickActiveClient(double Hours)
		{
			int ınt32 = 0;
			DateTime dateTime = DateTimeUtil.Now();
			foreach (GameClient value in GameXender.SocketSessions.Values)
			{
				Account player = value.Player;
				if (player == null || player.Room != null || player.ChannelId <= -1 || player.IsGM() || (dateTime - player.LastLobbyEnter).TotalHours < Hours)
				{
					continue;
				}
				ınt32++;
				player.Close(5000, false);
			}
			return ınt32;
		}

		public int KickCountActiveClient(double Hours)
		{
			int ınt32 = 0;
			DateTime dateTime = DateTimeUtil.Now();
			foreach (GameClient value in GameXender.SocketSessions.Values)
			{
				Account player = value.Player;
				if (player == null || player.Room != null || player.ChannelId <= -1 || player.IsGM() || (dateTime - player.LastLobbyEnter).TotalHours < Hours)
				{
					continue;
				}
				ınt32++;
			}
			return ınt32;
		}

		private void method_0()
		{
			try
			{
				this.MainSocket.BeginAccept(new AsyncCallback(this.method_1), this.MainSocket);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}

		private void method_1(IAsyncResult iasyncResult_0)
		{
			if (this.ServerIsClosed)
			{
				return;
			}
			Socket asyncState = iasyncResult_0.AsyncState as Socket;
			Socket socket = null;
			try
			{
				socket = asyncState.EndAccept(iasyncResult_0);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Format("Accept Callback Date: {0}; Exception: {1}", DateTimeUtil.Now(), exception.Message), LoggerType.Error, null);
			}
			this.method_2(socket, asyncState);
		}

		private void method_2(Socket socket_0, Socket socket_1)
		{
			try
			{
				Thread.Sleep(5);
				this.method_0();
				if (socket_0 != null)
				{
					this.AddSession(new GameClient(this.ServerId, socket_0));
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}

		public bool RemoveSession(GameClient Client)
		{
			GameClient gameClient;
			bool flag;
			try
			{
				if (Client == null || Client.SessionId == 0)
				{
					flag = false;
				}
				else if (!GameXender.SocketSessions.ContainsKey(Client.SessionId) || !GameXender.SocketSessions.TryRemove(Client.SessionId, out gameClient))
				{
					Client = null;
					return false;
				}
				else
				{
					flag = true;
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				return false;
			}
			return flag;
		}

		public Account SearchActiveClient(long PlayerId)
		{
			Account account;
			if (GameXender.SocketSessions.Count == 0)
			{
				return null;
			}
			using (IEnumerator<GameClient> enumerator = GameXender.SocketSessions.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Account player = enumerator.Current.Player;
					if (player == null || player.PlayerId != PlayerId)
					{
						continue;
					}
					account = player;
					return account;
				}
				return null;
			}
			return account;
		}

		public Account SearchActiveClient(uint SessionId)
		{
			Account player;
			if (GameXender.SocketSessions.Count == 0)
			{
				return null;
			}
			using (IEnumerator<GameClient> enumerator = GameXender.SocketSessions.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					GameClient current = enumerator.Current;
					if (current.Player == null || (long)current.SessionId != (ulong)SessionId)
					{
						continue;
					}
					player = current.Player;
					return player;
				}
				return null;
			}
			return player;
		}

		public int SendPacketToAllClients(GameServerPacket Packet)
		{
			int ınt32 = 0;
			if (GameXender.SocketSessions.Count == 0)
			{
				return ınt32;
			}
			byte[] completeBytes = Packet.GetCompleteBytes("GameManager.SendPacketToAllClients");
			foreach (GameClient value in GameXender.SocketSessions.Values)
			{
				Account player = value.Player;
				if (player == null || !player.IsOnline)
				{
					continue;
				}
				player.SendCompletePacket(completeBytes, Packet.GetType().Name);
				ınt32++;
			}
			return ınt32;
		}

		public bool Start()
		{
			bool flag;
			try
			{
				this.Config = ServerConfigJSON.GetConfig(ConfigLoader.ConfigId);
				this.MainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				IPEndPoint pEndPoint = new IPEndPoint(IPAddress.Parse(this.string_0), this.int_0);
				this.MainSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
				this.MainSocket.SetIPProtectionLevel(IPProtectionLevel.EdgeRestricted);
				this.MainSocket.DontFragment = false;
				this.MainSocket.NoDelay = true;
				this.MainSocket.Bind(pEndPoint);
				this.MainSocket.Listen(ConfigLoader.BackLog);
				CLogger.Print(string.Format("Game Serv Address {0}:{1}", this.string_0, this.int_0), LoggerType.Info, null);
				ThreadPool.QueueUserWorkItem((object object_0) => {
					try
					{
						this.method_0();
					}
					catch (Exception exception1)
					{
						Exception exception = exception1;
						CLogger.Print(string.Concat("Error processing TCP packet from ", this.string_0, ": ", exception.Message), LoggerType.Error, exception);
					}
				});
				flag = true;
			}
			catch (Exception exception3)
			{
				Exception exception2 = exception3;
				CLogger.Print(exception2.Message, LoggerType.Error, exception2);
				flag = false;
			}
			return flag;
		}
	}
}