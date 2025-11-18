using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.JSON;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Network;

namespace Server.Game
{
	// Token: 0x02000005 RID: 5
	public class GameManager
	{
		// Token: 0x06000027 RID: 39 RVA: 0x000025E7 File Offset: 0x000007E7
		public GameManager(int int_1, string string_1, int int_2)
		{
			this.string_0 = string_1;
			this.int_0 = int_2;
			this.ServerId = int_1;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000081A0 File Offset: 0x000063A0
		public bool Start()
		{
			bool flag;
			try
			{
				this.Config = ServerConfigJSON.GetConfig(ConfigLoader.ConfigId);
				this.MainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				IPEndPoint ipendPoint = new IPEndPoint(IPAddress.Parse(this.string_0), this.int_0);
				this.MainSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
				this.MainSocket.SetIPProtectionLevel(IPProtectionLevel.EdgeRestricted);
				this.MainSocket.DontFragment = false;
				this.MainSocket.NoDelay = true;
				this.MainSocket.Bind(ipendPoint);
				this.MainSocket.Listen(ConfigLoader.BackLog);
				CLogger.Print(string.Format("Game Serv Address {0}:{1}", this.string_0, this.int_0), LoggerType.Info, null);
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.method_3));
				flag = true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00008290 File Offset: 0x00006490
		private void method_0()
		{
			try
			{
				this.MainSocket.BeginAccept(new AsyncCallback(this.method_1), this.MainSocket);
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000082E0 File Offset: 0x000064E0
		private void method_1(IAsyncResult iasyncResult_0)
		{
			if (this.ServerIsClosed)
			{
				return;
			}
			Socket socket = iasyncResult_0.AsyncState as Socket;
			Socket socket2 = null;
			try
			{
				socket2 = socket.EndAccept(iasyncResult_0);
			}
			catch (Exception ex)
			{
				CLogger.Print(string.Format("Accept Callback Date: {0}; Exception: {1}", DateTimeUtil.Now(), ex.Message), LoggerType.Error, null);
			}
			this.method_2(socket2, socket);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000834C File Offset: 0x0000654C
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
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000839C File Offset: 0x0000659C
		public void AddSession(GameClient Client)
		{
			try
			{
				if (Client == null)
				{
					CLogger.Print("Destroyed after failed to add to list.", LoggerType.Warning, null);
				}
				else
				{
					DateTime dateTime = DateTimeUtil.Now();
					for (int i = 1; i < 100000; i++)
					{
						if (!GameXender.SocketSessions.ContainsKey(i) && GameXender.SocketSessions.TryAdd(i, Client))
						{
							Client.SessionDate = dateTime;
							Client.SessionId = i;
							Client.SessionSeed = (ushort)new Random(dateTime.Millisecond).Next(i, 32767);
							Client.StartSession();
							return;
						}
					}
					CLogger.Print(string.Format("Unable to add session list. IPAddress: {0}; Date: {1}", Client.GetIPAddress(), dateTime), LoggerType.Warning, null);
					Client.Close(500, true, false);
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00008470 File Offset: 0x00006670
		public bool RemoveSession(GameClient Client)
		{
			try
			{
				if (Client == null || Client.SessionId == 0)
				{
					return false;
				}
				GameClient gameClient;
				if (GameXender.SocketSessions.ContainsKey(Client.SessionId) && GameXender.SocketSessions.TryRemove(Client.SessionId, out gameClient))
				{
					return true;
				}
				Client = null;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
			return false;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000084E0 File Offset: 0x000066E0
		public int SendPacketToAllClients(GameServerPacket Packet)
		{
			int num = 0;
			if (GameXender.SocketSessions.Count == 0)
			{
				return num;
			}
			byte[] completeBytes = Packet.GetCompleteBytes("GameManager.SendPacketToAllClients");
			foreach (GameClient gameClient in GameXender.SocketSessions.Values)
			{
				Account player = gameClient.Player;
				if (player != null && player.IsOnline)
				{
					player.SendCompletePacket(completeBytes, Packet.GetType().Name);
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00008570 File Offset: 0x00006770
		public Account SearchActiveClient(long PlayerId)
		{
			if (GameXender.SocketSessions.Count == 0)
			{
				return null;
			}
			foreach (GameClient gameClient in GameXender.SocketSessions.Values)
			{
				Account player = gameClient.Player;
				if (player != null && player.PlayerId == PlayerId)
				{
					return player;
				}
			}
			return null;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000085E4 File Offset: 0x000067E4
		public Account SearchActiveClient(uint SessionId)
		{
			if (GameXender.SocketSessions.Count == 0)
			{
				return null;
			}
			foreach (GameClient gameClient in GameXender.SocketSessions.Values)
			{
				if (gameClient.Player != null && (long)gameClient.SessionId == (long)((ulong)SessionId))
				{
					return gameClient.Player;
				}
			}
			return null;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00008660 File Offset: 0x00006860
		public int KickActiveClient(double Hours)
		{
			int num = 0;
			DateTime dateTime = DateTimeUtil.Now();
			foreach (GameClient gameClient in GameXender.SocketSessions.Values)
			{
				Account player = gameClient.Player;
				if (player != null && player.Room == null && player.ChannelId > -1 && !player.IsGM() && (dateTime - player.LastLobbyEnter).TotalHours >= Hours)
				{
					num++;
					player.Close(5000, false);
				}
			}
			return num;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00008700 File Offset: 0x00006900
		public int KickCountActiveClient(double Hours)
		{
			int num = 0;
			DateTime dateTime = DateTimeUtil.Now();
			foreach (GameClient gameClient in GameXender.SocketSessions.Values)
			{
				Account player = gameClient.Player;
				if (player != null && player.Room == null && player.ChannelId > -1 && !player.IsGM() && (dateTime - player.LastLobbyEnter).TotalHours >= Hours)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00008794 File Offset: 0x00006994
		[CompilerGenerated]
		private void method_3(object object_0)
		{
			try
			{
				this.method_0();
			}
			catch (Exception ex)
			{
				CLogger.Print("Error processing TCP packet from " + this.string_0 + ": " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x04000013 RID: 19
		private readonly string string_0;

		// Token: 0x04000014 RID: 20
		private readonly int int_0;

		// Token: 0x04000015 RID: 21
		public readonly int ServerId;

		// Token: 0x04000016 RID: 22
		public ServerConfig Config;

		// Token: 0x04000017 RID: 23
		public Socket MainSocket;

		// Token: 0x04000018 RID: 24
		public bool ServerIsClosed;
	}
}
