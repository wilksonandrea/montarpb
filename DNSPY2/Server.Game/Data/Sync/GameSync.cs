using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Sync.Client;
using Server.Game.Network;

namespace Server.Game.Data.Sync
{
	// Token: 0x020001E3 RID: 483
	public class GameSync
	{
		// Token: 0x060005A9 RID: 1449 RVA: 0x0002E108 File Offset: 0x0002C308
		public GameSync(IPEndPoint ipendPoint_0)
		{
			this.ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			this.ClientSocket.Bind(ipendPoint_0);
			this.ClientSocket.IOControl(-1744830452, new byte[] { Convert.ToByte(false) }, null);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0002E158 File Offset: 0x0002C358
		public bool Start()
		{
			bool flag;
			try
			{
				IPEndPoint ipendPoint = this.ClientSocket.LocalEndPoint as IPEndPoint;
				CLogger.Print(string.Format("Game Sync Address {0}:{1}", ipendPoint.Address, ipendPoint.Port), LoggerType.Info, null);
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

		// Token: 0x060005AB RID: 1451 RVA: 0x0002E1D4 File Offset: 0x0002C3D4
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
				CLogger.Print("GameSync socket disposed during StartReceive.", LoggerType.Warning, null);
				this.Close();
			}
			catch (Exception ex)
			{
				CLogger.Print("Error in StartReceive: " + ex.Message, LoggerType.Error, ex);
				this.Close();
			}
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0002E294 File Offset: 0x0002C494
		private void method_1(IAsyncResult iasyncResult_0)
		{
			GameSync.Class10 @class = new GameSync.Class10();
			@class.gameSync_0 = this;
			if (!this.bool_0 && GameXender.Client != null && !GameXender.Client.ServerIsClosed)
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
					CLogger.Print("GameSync socket disposed during ReceiveCallback.", LoggerType.Warning, null);
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

		// Token: 0x060005AD RID: 1453 RVA: 0x0002E388 File Offset: 0x0002C588
		private void method_2(byte[] byte_0)
		{
			try
			{
				SyncClientPacket syncClientPacket = new SyncClientPacket(byte_0);
				short num = syncClientPacket.ReadH();
				switch (num)
				{
				case 1:
					RoomPassPortal.Load(syncClientPacket);
					goto IL_193;
				case 2:
					RoomBombC4.Load(syncClientPacket);
					goto IL_193;
				case 3:
					RoomDeath.Load(syncClientPacket);
					goto IL_193;
				case 4:
					RoomHitMarker.Load(syncClientPacket);
					goto IL_193;
				case 5:
					RoomSadeSync.Load(syncClientPacket);
					goto IL_193;
				case 6:
					RoomPing.Load(syncClientPacket);
					goto IL_193;
				case 7:
				case 8:
				case 9:
				case 12:
				case 14:
				case 25:
				case 26:
				case 27:
				case 28:
				case 29:
				case 30:
					break;
				case 10:
					AuthLogin.Load(syncClientPacket);
					goto IL_193;
				case 11:
					FriendInfo.Load(syncClientPacket);
					goto IL_193;
				case 13:
					AccountInfo.Load(syncClientPacket);
					goto IL_193;
				case 15:
					ServerCache.Load(syncClientPacket);
					goto IL_193;
				case 16:
					ClanSync.Load(syncClientPacket);
					goto IL_193;
				case 17:
					FriendSync.Load(syncClientPacket);
					goto IL_193;
				case 18:
					InventorySync.Load(syncClientPacket);
					goto IL_193;
				case 19:
					PlayerSync.Load(syncClientPacket);
					goto IL_193;
				case 20:
					ServerWarning.LoadGMWarning(syncClientPacket);
					goto IL_193;
				case 21:
					ClanServersSync.Load(syncClientPacket);
					goto IL_193;
				case 22:
					ServerWarning.LoadShopRestart(syncClientPacket);
					goto IL_193;
				case 23:
					ServerWarning.LoadServerUpdate(syncClientPacket);
					goto IL_193;
				case 24:
					ServerWarning.LoadShutdown(syncClientPacket);
					goto IL_193;
				case 31:
					EventInfo.LoadEventInfo(syncClientPacket);
					goto IL_193;
				case 32:
					ReloadConfig.Load(syncClientPacket);
					goto IL_193;
				default:
					if (num == 7171)
					{
						ServerMessage.Load(syncClientPacket);
						goto IL_193;
					}
					break;
				}
				CLogger.Print(Bitwise.ToHexData(string.Format("Game - Opcode Not Found: [{0}]", num), syncClientPacket.ToArray()), LoggerType.Opcode, null);
				IL_193:;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00005B55 File Offset: 0x00003D55
		public SChannelModel GetServer(AccountStatus status)
		{
			return this.GetServer((int)status.ServerId);
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00005B63 File Offset: 0x00003D63
		public SChannelModel GetServer(int serverId)
		{
			if (serverId != 255)
			{
				if (serverId != GameXender.Client.ServerId)
				{
					return SChannelXML.GetServer(serverId);
				}
			}
			return null;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0002E558 File Offset: 0x0002C758
		public void SendBytes(long PlayerId, GameServerPacket Packet, int ServerId)
		{
			try
			{
				if (Packet != null)
				{
					SChannelModel server = this.GetServer(ServerId);
					if (server != null)
					{
						string name = Packet.GetType().Name;
						byte[] bytes = Packet.GetBytes("GameSync.SendBytes");
						IPEndPoint connection = SynchronizeXML.GetServer((int)server.Port).Connection;
						using (SyncServerPacket syncServerPacket = new SyncServerPacket())
						{
							syncServerPacket.WriteH(13);
							syncServerPacket.WriteQ(PlayerId);
							syncServerPacket.WriteC(0);
							syncServerPacket.WriteC((byte)(name.Length + 1));
							syncServerPacket.WriteS(name, name.Length + 1);
							syncServerPacket.WriteH((ushort)bytes.Length);
							syncServerPacket.WriteB(bytes);
							this.SendPacket(syncServerPacket.ToArray(), connection);
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0002E648 File Offset: 0x0002C848
		public void SendBytes(long PlayerId, string PacketName, byte[] Data, int ServerId)
		{
			try
			{
				if (Data.Length != 0)
				{
					SChannelModel server = this.GetServer(ServerId);
					if (server != null)
					{
						IPEndPoint connection = SynchronizeXML.GetServer((int)server.Port).Connection;
						using (SyncServerPacket syncServerPacket = new SyncServerPacket())
						{
							syncServerPacket.WriteH(13);
							syncServerPacket.WriteQ(PlayerId);
							syncServerPacket.WriteC(0);
							syncServerPacket.WriteC((byte)(PacketName.Length + 1));
							syncServerPacket.WriteS(PacketName, PacketName.Length + 1);
							syncServerPacket.WriteH((ushort)Data.Length);
							syncServerPacket.WriteB(Data);
							this.SendPacket(syncServerPacket.ToArray(), connection);
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0002E714 File Offset: 0x0002C914
		public void SendCompleteBytes(long PlayerId, string PacketName, byte[] Data, int ServerId)
		{
			try
			{
				if (Data.Length != 0)
				{
					SChannelModel server = this.GetServer(ServerId);
					if (server != null)
					{
						IPEndPoint connection = SynchronizeXML.GetServer((int)server.Port).Connection;
						using (SyncServerPacket syncServerPacket = new SyncServerPacket())
						{
							syncServerPacket.WriteH(13);
							syncServerPacket.WriteQ(PlayerId);
							syncServerPacket.WriteC(1);
							syncServerPacket.WriteC((byte)(PacketName.Length + 1));
							syncServerPacket.WriteS(PacketName, PacketName.Length + 1);
							syncServerPacket.WriteH((ushort)Data.Length);
							syncServerPacket.WriteB(Data);
							this.SendPacket(syncServerPacket.ToArray(), connection);
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0002E7E0 File Offset: 0x0002C9E0
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
				CLogger.Print(string.Format("GameSync socket disposed during SendPacket to {0}.", Address), LoggerType.Warning, null);
			}
			catch (Exception ex)
			{
				CLogger.Print(string.Format("Error sending UDP packet to {0}: {1}", Address, ex.Message), LoggerType.Error, ex);
			}
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0002E86C File Offset: 0x0002CA6C
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
				CLogger.Print("GameSync socket disposed during SendCallback.", LoggerType.Warning, null);
			}
			catch (Exception ex)
			{
				CLogger.Print("Error in SendCallback: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0002E8E0 File Offset: 0x0002CAE0
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
				CLogger.Print("Error closing GameSync: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00005B84 File Offset: 0x00003D84
		[CompilerGenerated]
		private void method_4(object object_0)
		{
			this.method_0();
		}

		// Token: 0x04000397 RID: 919
		protected Socket ClientSocket;

		// Token: 0x04000398 RID: 920
		private bool bool_0;

		// Token: 0x020001E4 RID: 484
		[CompilerGenerated]
		private sealed class Class10
		{
			// Token: 0x060005B7 RID: 1463 RVA: 0x000025DF File Offset: 0x000007DF
			public Class10()
			{
			}

			// Token: 0x060005B8 RID: 1464 RVA: 0x0002E950 File Offset: 0x0002CB50
			internal void method_0(object object_0)
			{
				try
				{
					this.gameSync_0.method_2(this.byte_0);
				}
				catch (Exception ex)
				{
					CLogger.Print("Error processing AuthSync packet in thread pool: " + ex.Message, LoggerType.Error, ex);
				}
			}

			// Token: 0x04000399 RID: 921
			public GameSync gameSync_0;

			// Token: 0x0400039A RID: 922
			public byte[] byte_0;
		}
	}
}
