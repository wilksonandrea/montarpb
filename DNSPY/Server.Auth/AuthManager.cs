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
using Server.Auth.Data.Models;
using Server.Auth.Network;

namespace Server.Auth
{
	// Token: 0x02000005 RID: 5
	public class AuthManager
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002411 File Offset: 0x00000611
		public AuthManager(int int_1, string string_1, int int_2)
		{
			this.string_0 = string_1;
			this.int_0 = int_2;
			this.ServerId = int_1;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003990 File Offset: 0x00001B90
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
				CLogger.Print(string.Format("Auth Serv Address {0}:{1}", this.string_0, this.int_0), LoggerType.Info, null);
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

		// Token: 0x06000026 RID: 38 RVA: 0x00003A80 File Offset: 0x00001C80
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

		// Token: 0x06000027 RID: 39 RVA: 0x00003AD0 File Offset: 0x00001CD0
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
			this.method_2(socket2);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003B3C File Offset: 0x00001D3C
		private void method_2(Socket socket_0)
		{
			try
			{
				Thread.Sleep(5);
				this.method_0();
				if (socket_0 != null)
				{
					this.AddSession(new AuthClient(this.ServerId, socket_0));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003B8C File Offset: 0x00001D8C
		public void AddSession(AuthClient Client)
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
						if (!AuthXender.SocketSessions.ContainsKey(i) && AuthXender.SocketSessions.TryAdd(i, Client))
						{
							Client.SessionDate = dateTime;
							Client.SessionId = i;
							Client.SessionSeed = (ushort)new Random(dateTime.Millisecond).Next(i, 32767);
							Client.StartSession();
							return;
						}
					}
					CLogger.Print(string.Format("Unable to add session list. IPAddress: {0}; Date: {1}", Client.GetIPAddress(), dateTime), LoggerType.Warning, null);
					Client.Close(500, true);
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003C60 File Offset: 0x00001E60
		public bool RemoveSession(AuthClient Client)
		{
			try
			{
				if (Client == null || Client.SessionId == 0)
				{
					return false;
				}
				AuthClient authClient;
				if (AuthXender.SocketSessions.ContainsKey(Client.SessionId) && AuthXender.SocketSessions.TryRemove(Client.SessionId, out authClient))
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

		// Token: 0x0600002B RID: 43 RVA: 0x00003CD0 File Offset: 0x00001ED0
		public int SendPacketToAllClients(AuthServerPacket Packet)
		{
			int num = 0;
			if (AuthXender.SocketSessions.Count == 0)
			{
				return num;
			}
			byte[] completeBytes = Packet.GetCompleteBytes("AuthManager.SendPacketToAllClients");
			foreach (AuthClient authClient in AuthXender.SocketSessions.Values)
			{
				Account player = authClient.Player;
				if (player != null && player.IsOnline)
				{
					player.SendCompletePacket(completeBytes, Packet.GetType().Name);
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003D60 File Offset: 0x00001F60
		public Account SearchActiveClient(long PlayerId)
		{
			if (AuthXender.SocketSessions.Count == 0)
			{
				return null;
			}
			foreach (AuthClient authClient in AuthXender.SocketSessions.Values)
			{
				Account player = authClient.Player;
				if (player != null && player.PlayerId == PlayerId)
				{
					return player;
				}
			}
			return null;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003DD4 File Offset: 0x00001FD4
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

		// Token: 0x04000012 RID: 18
		private readonly string string_0;

		// Token: 0x04000013 RID: 19
		private readonly int int_0;

		// Token: 0x04000014 RID: 20
		public readonly int ServerId;

		// Token: 0x04000015 RID: 21
		public ServerConfig Config;

		// Token: 0x04000016 RID: 22
		public Socket MainSocket;

		// Token: 0x04000017 RID: 23
		public bool ServerIsClosed;
	}
}
