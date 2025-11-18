using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Auth.Data.Sync.Client;

namespace Server.Auth.Data.Sync
{
	// Token: 0x0200004B RID: 75
	public class AuthSync
	{
		// Token: 0x0600011B RID: 283 RVA: 0x0000AB78 File Offset: 0x00008D78
		public AuthSync(IPEndPoint ipendPoint_0)
		{
			this.ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			this.ClientSocket.Bind(ipendPoint_0);
			this.ClientSocket.IOControl(-1744830452, new byte[] { Convert.ToByte(false) }, null);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000ABC8 File Offset: 0x00008DC8
		public bool Start()
		{
			bool flag;
			try
			{
				IPEndPoint ipendPoint = this.ClientSocket.LocalEndPoint as IPEndPoint;
				CLogger.Print(string.Format("Auth Sync Address {0}:{1}", ipendPoint.Address, ipendPoint.Port), LoggerType.Info, null);
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

		// Token: 0x0600011D RID: 285 RVA: 0x0000AC44 File Offset: 0x00008E44
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
				CLogger.Print("AuthSync socket disposed during StartReceive.", LoggerType.Warning, null);
				this.Close();
			}
			catch (Exception ex)
			{
				CLogger.Print("Error in StartReceive: " + ex.Message, LoggerType.Error, ex);
				this.Close();
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000AD04 File Offset: 0x00008F04
		private void method_1(IAsyncResult iasyncResult_0)
		{
			AuthSync.Class3 @class = new AuthSync.Class3();
			@class.authSync_0 = this;
			if (!this.bool_0 && AuthXender.Client != null && !AuthXender.Client.ServerIsClosed)
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
					CLogger.Print("AuthSync socket disposed during ReceiveCallback.", LoggerType.Warning, null);
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

		// Token: 0x0600011F RID: 287 RVA: 0x0000ADF8 File Offset: 0x00008FF8
		private void method_2(byte[] byte_0)
		{
			try
			{
				SyncClientPacket syncClientPacket = new SyncClientPacket(byte_0);
				short num = syncClientPacket.ReadH();
				switch (num)
				{
				case 11:
					FriendInfo.Load(syncClientPacket);
					goto IL_124;
				case 12:
				case 14:
				case 18:
				case 21:
				case 25:
				case 26:
				case 27:
				case 28:
				case 29:
				case 30:
					break;
				case 13:
					AccountInfo.Load(syncClientPacket);
					goto IL_124;
				case 15:
					ServerCache.Load(syncClientPacket);
					goto IL_124;
				case 16:
					ClanSync.Load(syncClientPacket);
					goto IL_124;
				case 17:
					FriendSync.Load(syncClientPacket);
					goto IL_124;
				case 19:
					PlayerSync.Load(syncClientPacket);
					goto IL_124;
				case 20:
					ServerWarning.LoadGMWarning(syncClientPacket);
					goto IL_124;
				case 22:
					ServerWarning.LoadShopRestart(syncClientPacket);
					goto IL_124;
				case 23:
					ServerWarning.LoadServerUpdate(syncClientPacket);
					goto IL_124;
				case 24:
					ServerWarning.LoadShutdown(syncClientPacket);
					goto IL_124;
				case 31:
					EventInfo.LoadEventInfo(syncClientPacket);
					goto IL_124;
				case 32:
					ReloadConfig.Load(syncClientPacket);
					goto IL_124;
				case 33:
					ChannelCache.Load(syncClientPacket);
					goto IL_124;
				case 34:
					ReloadPermn.Load(syncClientPacket);
					goto IL_124;
				default:
					if (num == 7171)
					{
						ServerMessage.Load(syncClientPacket);
						goto IL_124;
					}
					break;
				}
				CLogger.Print(Bitwise.ToHexData(string.Format("Auth - Opcode Not Found: [{0}]", num), syncClientPacket.ToArray()), LoggerType.Opcode, null);
				IL_124:;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000AF58 File Offset: 0x00009158
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
				CLogger.Print(string.Format("AuthSync socket disposed during SendPacket to {0}.", Address), LoggerType.Warning, null);
			}
			catch (Exception ex)
			{
				CLogger.Print(string.Format("Error sending UDP packet to {0}: {1}", Address, ex.Message), LoggerType.Error, ex);
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000AFE4 File Offset: 0x000091E4
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
				CLogger.Print("AuthSync socket disposed during SendCallback.", LoggerType.Warning, null);
			}
			catch (Exception ex)
			{
				CLogger.Print("Error in SendCallback: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000B058 File Offset: 0x00009258
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
				CLogger.Print("Error closing AuthSync: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00002B11 File Offset: 0x00000D11
		[CompilerGenerated]
		private void method_4(object object_0)
		{
			this.method_0();
		}

		// Token: 0x04000097 RID: 151
		protected Socket ClientSocket;

		// Token: 0x04000098 RID: 152
		private bool bool_0;

		// Token: 0x0200004C RID: 76
		[CompilerGenerated]
		private sealed class Class3
		{
			// Token: 0x06000124 RID: 292 RVA: 0x00002409 File Offset: 0x00000609
			public Class3()
			{
			}

			// Token: 0x06000125 RID: 293 RVA: 0x0000B0C8 File Offset: 0x000092C8
			internal void method_0(object object_0)
			{
				try
				{
					this.authSync_0.method_2(this.byte_0);
				}
				catch (Exception ex)
				{
					CLogger.Print("Error processing AuthSync packet in thread pool: " + ex.Message, LoggerType.Error, ex);
				}
			}

			// Token: 0x04000099 RID: 153
			public AuthSync authSync_0;

			// Token: 0x0400009A RID: 154
			public byte[] byte_0;
		}
	}
}
