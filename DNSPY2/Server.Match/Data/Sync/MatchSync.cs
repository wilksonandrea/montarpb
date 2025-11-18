using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Match.Data.Sync.Client;

namespace Server.Match.Data.Sync
{
	// Token: 0x0200002A RID: 42
	public class MatchSync
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x000097C4 File Offset: 0x000079C4
		public MatchSync(IPEndPoint ipendPoint_0)
		{
			this.ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			this.ClientSocket.Bind(ipendPoint_0);
			this.ClientSocket.IOControl(-1744830452, new byte[] { Convert.ToByte(false) }, null);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00009814 File Offset: 0x00007A14
		public bool Start()
		{
			bool flag;
			try
			{
				IPEndPoint ipendPoint = this.ClientSocket.LocalEndPoint as IPEndPoint;
				CLogger.Print(string.Format("Match Sync Address {0}:{1}", ipendPoint.Address, ipendPoint.Port), LoggerType.Info, null);
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.method_4));
				flag = true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00009890 File Offset: 0x00007A90
		private void method_0()
		{
			if (this.bool_0)
			{
				return;
			}
			try
			{
				StateObject stateObject = new StateObject
				{
					WorkSocket = this.ClientSocket,
					RemoteEP = new IPEndPoint(IPAddress.Any, 8000)
				};
				this.ClientSocket.BeginReceiveFrom(stateObject.UdpBuffer, 0, 8096, SocketFlags.None, ref stateObject.RemoteEP, new AsyncCallback(this.method_1), stateObject);
			}
			catch (ObjectDisposedException)
			{
				CLogger.Print("MatchSync socket disposed during StartReceive.", LoggerType.Warning, null);
				this.Close();
			}
			catch (Exception ex)
			{
				CLogger.Print("Error in StartReceive: " + ex.Message, LoggerType.Error, ex);
				this.Close();
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00009950 File Offset: 0x00007B50
		private void method_1(IAsyncResult iasyncResult_0)
		{
			MatchSync.Class0 @class = new MatchSync.Class0();
			@class.matchSync_0 = this;
			if (!this.bool_0 && MatchXender.Client != null && !MatchXender.Client.ServerIsClosed)
			{
				StateObject stateObject = iasyncResult_0.AsyncState as StateObject;
				try
				{
					int num = this.ClientSocket.EndReceiveFrom(iasyncResult_0, ref stateObject.RemoteEP);
					if (num > 0)
					{
						@class.byte_0 = new byte[num];
						Array.Copy(stateObject.UdpBuffer, 0, @class.byte_0, 0, num);
						ThreadPool.QueueUserWorkItem(new WaitCallback(@class.method_0));
					}
				}
				catch (ObjectDisposedException)
				{
					CLogger.Print("MatchSync socket disposed during ReceiveCallback.", LoggerType.Warning, null);
					this.Close();
				}
				catch (Exception ex)
				{
					CLogger.Print("Error in ReceiveCallback: " + ex.Message, LoggerType.Error, ex);
				}
				finally
				{
					this.method_0();
				}
				return;
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00009A44 File Offset: 0x00007C44
		private void method_2(byte[] byte_0)
		{
			try
			{
				SyncClientPacket syncClientPacket = new SyncClientPacket(byte_0);
				short num = syncClientPacket.ReadH();
				switch (num)
				{
				case 1:
					RespawnSync.Load(syncClientPacket);
					break;
				case 2:
					RemovePlayerSync.Load(syncClientPacket);
					break;
				case 3:
					MatchRoundSync.Load(syncClientPacket);
					break;
				default:
					CLogger.Print(Bitwise.ToHexData(string.Format("Match - Opcode Not Found: [{0}]", num), syncClientPacket.ToArray()), LoggerType.Opcode, null);
					break;
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00009AD0 File Offset: 0x00007CD0
		public void SendPacket(byte[] Data, IPEndPoint Address)
		{
			if (this.bool_0)
			{
				return;
			}
			try
			{
				this.ClientSocket.BeginSendTo(Data, 0, Data.Length, SocketFlags.None, Address, new AsyncCallback(this.method_3), this.ClientSocket);
			}
			catch (ObjectDisposedException)
			{
				CLogger.Print(string.Format("MatchSync socket disposed during SendPacket to {0}.", Address), LoggerType.Warning, null);
			}
			catch (Exception ex)
			{
				CLogger.Print(string.Format("Error sending UDP packet to {0}: {1}", Address, ex.Message), LoggerType.Error, ex);
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00009B5C File Offset: 0x00007D5C
		private void method_3(IAsyncResult iasyncResult_0)
		{
			try
			{
				Socket socket = iasyncResult_0.AsyncState as Socket;
				if (socket != null && socket.Connected)
				{
					socket.EndSend(iasyncResult_0);
				}
			}
			catch (ObjectDisposedException)
			{
				CLogger.Print("MatchSync socket disposed during SendCallback.", LoggerType.Warning, null);
			}
			catch (Exception ex)
			{
				CLogger.Print("Error in SendCallback: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00009BD0 File Offset: 0x00007DD0
		public void Close()
		{
			if (this.bool_0)
			{
				return;
			}
			this.bool_0 = true;
			try
			{
				if (this.ClientSocket != null)
				{
					this.ClientSocket.Close();
					this.ClientSocket.Dispose();
					this.ClientSocket = null;
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("Error closing MatchSync: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00002368 File Offset: 0x00000568
		[CompilerGenerated]
		private void method_4(object object_0)
		{
			this.method_0();
		}

		// Token: 0x0400000E RID: 14
		protected Socket ClientSocket;

		// Token: 0x0400000F RID: 15
		private bool bool_0;

		// Token: 0x0200002B RID: 43
		[CompilerGenerated]
		private sealed class Class0
		{
			// Token: 0x060000BA RID: 186 RVA: 0x000020A2 File Offset: 0x000002A2
			public Class0()
			{
			}

			// Token: 0x060000BB RID: 187 RVA: 0x00009C40 File Offset: 0x00007E40
			internal void method_0(object object_0)
			{
				try
				{
					this.matchSync_0.method_2(this.byte_0);
				}
				catch (Exception ex)
				{
					CLogger.Print("Error processing AuthSync packet in thread pool: " + ex.Message, LoggerType.Error, ex);
				}
			}

			// Token: 0x04000010 RID: 16
			public MatchSync matchSync_0;

			// Token: 0x04000011 RID: 17
			public byte[] byte_0;
		}
	}
}
