using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Utility;

namespace Server.Match
{
	// Token: 0x02000004 RID: 4
	public class MatchManager
	{
		// Token: 0x06000019 RID: 25 RVA: 0x000020AA File Offset: 0x000002AA
		public MatchManager(string string_1, int int_1)
		{
			this.string_0 = string_1;
			this.int_0 = int_1;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00005DA0 File Offset: 0x00003FA0
		public bool Start()
		{
			bool flag;
			try
			{
				this.MainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
				this.MainSocket.IOControl(-1744830452, new byte[] { Convert.ToByte(false) }, null);
				IPEndPoint ipendPoint = new IPEndPoint(IPAddress.Parse(this.string_0), this.int_0);
				this.MainSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
				this.MainSocket.SetIPProtectionLevel(IPProtectionLevel.EdgeRestricted);
				this.MainSocket.DontFragment = false;
				this.MainSocket.Ttl = 128;
				this.MainSocket.Bind(ipendPoint);
				CLogger.Print(string.Format("Match Serv Address {0}:{1}", ipendPoint.Address, ipendPoint.Port), LoggerType.Info, null);
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.method_5));
				flag = true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00005E9C File Offset: 0x0000409C
		private void method_0()
		{
			try
			{
				StateObject stateObject = new StateObject
				{
					WorkSocket = this.MainSocket,
					RemoteEP = new IPEndPoint(IPAddress.Any, 0)
				};
				this.MainSocket.BeginReceiveFrom(stateObject.UdpBuffer, 0, 8096, SocketFlags.None, ref stateObject.RemoteEP, new AsyncCallback(this.method_1), stateObject);
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00005F1C File Offset: 0x0000411C
		private void method_1(IAsyncResult iasyncResult_0)
		{
			if (!iasyncResult_0.IsCompleted)
			{
				CLogger.Print("IAsyncResult is not completed.", LoggerType.Warning, null);
			}
			StateObject stateObject = iasyncResult_0.AsyncState as StateObject;
			Socket workSocket = stateObject.WorkSocket;
			IPEndPoint ipendPoint = stateObject.RemoteEP as IPEndPoint;
			try
			{
				EndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
				int num = workSocket.EndReceiveFrom(iasyncResult_0, ref endPoint);
				if (num > 0)
				{
					byte[] array = new byte[num];
					Buffer.BlockCopy(stateObject.UdpBuffer, 0, array, 0, num);
					if (array.Length >= 22 && array.Length <= 8096)
					{
						this.BeginReceive(new MatchClient(workSocket, endPoint as IPEndPoint), array);
					}
					else
					{
						CLogger.Print(string.Format("Invalid Buffer Length: {0}; IP: {1}", array.Length, ipendPoint), LoggerType.Hack, null);
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("Error during EndReceiveCallback: " + ex.Message, LoggerType.Error, ex);
				this.method_2(ipendPoint);
				this.method_3(string.Format("{0}", ipendPoint.Address));
			}
			finally
			{
				this.method_0();
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000603C File Offset: 0x0000423C
		public void BeginReceive(MatchClient Client, byte[] Buffer)
		{
			try
			{
				if (Client == null)
				{
					CLogger.Print("Destroyed after failed to add to list.", LoggerType.Warning, null);
				}
				else
				{
					Client.BeginReceive(Buffer, DateTimeUtil.Now());
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00006088 File Offset: 0x00004288
		private bool method_2(IPEndPoint ipendPoint_0)
		{
			try
			{
				if (ipendPoint_0 == null)
				{
					return false;
				}
				Socket socket;
				if (MatchXender.UdpClients.ContainsKey(ipendPoint_0) && MatchXender.UdpClients.TryGetValue(ipendPoint_0, out socket))
				{
					return MatchXender.UdpClients.TryRemove(ipendPoint_0, out socket);
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
			return false;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000060F0 File Offset: 0x000042F0
		private bool method_3(string string_1)
		{
			try
			{
				if (string.IsNullOrEmpty(string_1) || string_1.Equals("0.0.0.0"))
				{
					return false;
				}
				int num;
				if (MatchXender.SpamConnections.ContainsKey(string_1) && MatchXender.SpamConnections.TryGetValue(string_1, out num))
				{
					return MatchXender.SpamConnections.TryRemove(string_1, out num);
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
			return false;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00006168 File Offset: 0x00004368
		public void SendPacket(byte[] Data, IPEndPoint Address)
		{
			try
			{
				this.MainSocket.BeginSendTo(Data, 0, Data.Length, SocketFlags.None, Address, new AsyncCallback(this.method_4), this.MainSocket);
			}
			catch (Exception ex)
			{
				CLogger.Print(string.Format("Failed to send package to {0}: {1}", Address, ex.Message), LoggerType.Error, ex);
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000061C8 File Offset: 0x000043C8
		private void method_4(IAsyncResult iasyncResult_0)
		{
			try
			{
				Socket socket = iasyncResult_0.AsyncState as Socket;
				if (socket != null && socket.Connected)
				{
					socket.EndSend(iasyncResult_0);
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("Error during EndSendCallback: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00006220 File Offset: 0x00004420
		[CompilerGenerated]
		private void method_5(object object_0)
		{
			try
			{
				this.method_0();
			}
			catch (Exception ex)
			{
				CLogger.Print("Error processing UDP packet from " + this.string_0 + ": " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x04000007 RID: 7
		private readonly string string_0;

		// Token: 0x04000008 RID: 8
		private readonly int int_0;

		// Token: 0x04000009 RID: 9
		public Socket MainSocket;

		// Token: 0x0400000A RID: 10
		public bool ServerIsClosed;
	}
}
