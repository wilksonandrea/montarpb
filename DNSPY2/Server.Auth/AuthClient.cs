using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Win32.SafeHandles;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Utility;
using Server.Auth.Data.Models;
using Server.Auth.Data.Sync.Server;
using Server.Auth.Network;
using Server.Auth.Network.ClientPacket;
using Server.Auth.Network.ServerPacket;

namespace Server.Auth
{
	// Token: 0x02000002 RID: 2
	public class AuthClient : IDisposable
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002340 File Offset: 0x00000540
		public AuthClient(int int_0, Socket socket_0)
		{
			this.ServerId = int_0;
			this.Client = socket_0;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002CA4 File Offset: 0x00000EA4
		public void Dispose()
		{
			try
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002CE0 File Offset: 0x00000EE0
		protected virtual void Dispose(bool disposing)
		{
			try
			{
				if (!this.bool_0)
				{
					this.Player = null;
					if (this.Client != null)
					{
						this.Client.Dispose();
						this.Client = null;
					}
					if (disposing)
					{
						this.safeHandle_0.Dispose();
					}
					this.bool_0 = true;
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002D50 File Offset: 0x00000F50
		public void StartSession()
		{
			try
			{
				this.ushort_0 = this.SessionSeed;
				this.SessionShift = (this.SessionId + Bitwise.CRYPTO[0]) % 7 + 1;
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.method_8));
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.method_9));
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.method_10));
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				this.Close(0, true);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000237E File Offset: 0x0000057E
		private void method_0()
		{
			Thread.Sleep(10000);
			if (this.Client != null && this.FirstPacketId == 0)
			{
				CLogger.Print("Connection destroyed due to no responses. IPAddress: " + this.GetIPAddress(), LoggerType.Hack, null);
				this.Close(0, true);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002DE4 File Offset: 0x00000FE4
		public void HeartBeatCounter()
		{
			AuthClient.Class0 @class = new AuthClient.Class0();
			@class.authClient_0 = this;
			@class.timerState_0 = new TimerState();
			@class.timerState_0.StartTimer(TimeSpan.FromMinutes(20.0), new TimerCallback(@class.method_0));
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002E30 File Offset: 0x00001030
		public string GetIPAddress()
		{
			string text;
			try
			{
				if (this.Client != null && this.Client.RemoteEndPoint != null)
				{
					text = (this.Client.RemoteEndPoint as IPEndPoint).Address.ToString();
				}
				else
				{
					text = "";
				}
			}
			catch
			{
				text = "";
			}
			return text;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002E94 File Offset: 0x00001094
		public IPAddress GetAddress()
		{
			IPAddress ipaddress;
			try
			{
				if (this.Client != null && this.Client.RemoteEndPoint != null)
				{
					ipaddress = (this.Client.RemoteEndPoint as IPEndPoint).Address;
				}
				else
				{
					ipaddress = null;
				}
			}
			catch
			{
				ipaddress = null;
			}
			return ipaddress;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000023B9 File Offset: 0x000005B9
		private void method_1()
		{
			this.SendPacket(new PROTOCOL_BASE_CONNECT_ACK(this));
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002EE8 File Offset: 0x000010E8
		public void SendCompletePacket(byte[] OriginalData, string PacketName)
		{
			try
			{
				byte[] bytes = BitConverter.GetBytes((ushort)(OriginalData.Length + 2));
				byte[] array = new byte[OriginalData.Length + bytes.Length];
				Array.Copy(bytes, 0, array, 0, bytes.Length);
				Array.Copy(OriginalData, 0, array, 2, OriginalData.Length);
				byte[] array2 = new byte[array.Length + 5];
				Array.Copy(array, 0, array2, 0, array.Length);
				if (ConfigLoader.DebugMode)
				{
					ushort num = BitConverter.ToUInt16(array2, 2);
					Bitwise.ToByteString(array2);
					CLogger.Print(string.Format("{0}; Address: {1}; Opcode: [{2}]", PacketName, this.Client.RemoteEndPoint, num), LoggerType.Debug, null);
				}
				Bitwise.ProcessPacket(array2, 2, 5, this.ushort_1);
				if (this.Client != null && array2.Length != 0)
				{
					this.Client.BeginSend(array2, 0, array2.Length, SocketFlags.None, new AsyncCallback(this.method_2), this.Client);
				}
			}
			catch
			{
				this.Close(0, true);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002FD4 File Offset: 0x000011D4
		public void SendPacket(byte[] OriginalData, string PacketName)
		{
			try
			{
				if (OriginalData.Length >= 2)
				{
					this.SendCompletePacket(OriginalData, PacketName);
				}
			}
			catch
			{
				this.Close(0, true);
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00003010 File Offset: 0x00001210
		public void SendPacket(AuthServerPacket Packet)
		{
			try
			{
				try
				{
					byte[] bytes = Packet.GetBytes("AuthClient.SendPacket");
					this.SendPacket(bytes, Packet.GetType().Name);
					Packet.Dispose();
				}
				finally
				{
					if (Packet != null)
					{
						((IDisposable)Packet).Dispose();
					}
				}
			}
			catch
			{
				this.Close(0, true);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00003078 File Offset: 0x00001278
		private void method_2(IAsyncResult iasyncResult_0)
		{
			try
			{
				Socket socket = iasyncResult_0.AsyncState as Socket;
				if (socket != null && socket.Connected)
				{
					socket.EndSend(iasyncResult_0);
				}
			}
			catch
			{
				this.Close(0, true);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000030C4 File Offset: 0x000012C4
		private void method_3()
		{
			try
			{
				StateObject stateObject = new StateObject
				{
					WorkSocket = this.Client,
					RemoteEP = new IPEndPoint(IPAddress.Any, 0)
				};
				this.Client.BeginReceive(stateObject.TcpBuffer, 0, 8912, SocketFlags.None, new AsyncCallback(this.method_4), stateObject);
			}
			catch
			{
				this.Close(0, true);
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00003138 File Offset: 0x00001338
		public void Close(int TimeMS, bool DestroyConnection)
		{
			if (this.bool_1)
			{
				return;
			}
			try
			{
				this.bool_1 = true;
				AuthXender.Client.RemoveSession(this);
				Account player = this.Player;
				if (DestroyConnection)
				{
					if (player != null)
					{
						player.SetOnlineStatus(false);
						if (player.Status.ServerId == 0)
						{
							SendRefresh.RefreshAccount(player, false);
						}
						player.Status.ResetData(player.PlayerId);
						player.SimpleClear();
						player.UpdateCacheInfo();
						this.Player = null;
					}
					if (this.Client != null)
					{
						this.Client.Close(TimeMS);
					}
					Thread.Sleep(TimeMS);
					this.Dispose();
				}
				else if (player != null)
				{
					player.SimpleClear();
					player.UpdateCacheInfo();
					this.Player = null;
				}
				UpdateServer.RefreshSChannel(this.ServerId);
			}
			catch (Exception ex)
			{
				CLogger.Print("AuthClient.Close: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00003220 File Offset: 0x00001420
		private void method_4(IAsyncResult iasyncResult_0)
		{
			StateObject stateObject = iasyncResult_0.AsyncState as StateObject;
			try
			{
				int num = stateObject.WorkSocket.EndReceive(iasyncResult_0);
				if (num > 0)
				{
					if (num > 8912)
					{
						CLogger.Print("Received data exceeds buffer size. IP: " + this.GetIPAddress(), LoggerType.Error, null);
						this.Close(0, true);
					}
					else
					{
						byte[] array = new byte[num];
						Array.Copy(stateObject.TcpBuffer, 0, array, 0, num);
						int num2 = (int)(BitConverter.ToUInt16(array, 0) & 32767);
						if (num2 > 0 && num2 <= num - 2)
						{
							byte[] array2 = new byte[num2];
							Array.Copy(array, 2, array2, 0, array2.Length);
							byte[] array3 = Bitwise.Decrypt(array2, this.SessionShift);
							ushort num3 = BitConverter.ToUInt16(array3, 0);
							ushort num4 = BitConverter.ToUInt16(array3, 2);
							this.method_5(num3);
							if (!this.CheckSeed(num4, true))
							{
								this.Close(0, true);
							}
							else if (!this.bool_1)
							{
								this.method_7(num3, array3, "REQ");
								this.CheckOut(array, num2);
								ThreadPool.QueueUserWorkItem(new WaitCallback(this.method_11));
							}
						}
						else
						{
							CLogger.Print(string.Format("Invalid PacketLength. IP: {0}; Length: {1}; RawBytes: {2}", this.GetIPAddress(), num2, num), LoggerType.Hack, null);
							this.Close(0, true);
						}
					}
				}
			}
			catch
			{
				this.Close(0, true);
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00003390 File Offset: 0x00001590
		public void CheckOut(byte[] BufferTotal, int FirstLength)
		{
			int num = BufferTotal.Length;
			try
			{
				if (num > FirstLength + 3)
				{
					byte[] array = new byte[num - FirstLength - 3];
					Array.Copy(BufferTotal, FirstLength + 3, array, 0, array.Length);
					if (array.Length != 0)
					{
						int num2 = (int)(BitConverter.ToUInt16(array, 0) & 32767);
						if (num2 > 0 && num2 <= array.Length - 2)
						{
							byte[] array2 = new byte[num2];
							Array.Copy(array, 2, array2, 0, array2.Length);
							byte[] array3 = Bitwise.Decrypt(array2, this.SessionShift);
							ushort num3 = BitConverter.ToUInt16(array3, 0);
							ushort num4 = BitConverter.ToUInt16(array3, 2);
							if (!this.CheckSeed(num4, false))
							{
								this.Close(0, true);
							}
							else
							{
								this.method_7(num3, array3, "REQ");
								this.CheckOut(array, num2);
							}
						}
						else
						{
							CLogger.Print(string.Format("Invalid PacketLength in CheckOut. IP: {0}; Length: {1}; RawBytes: {2}", this.GetIPAddress(), num2, array.Length), LoggerType.Hack, null);
							this.Close(0, true);
						}
					}
				}
			}
			catch
			{
				this.Close(0, true);
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00003498 File Offset: 0x00001698
		private void method_5(ushort ushort_2)
		{
			if (this.FirstPacketId != 0)
			{
				return;
			}
			this.FirstPacketId = (int)ushort_2;
			if (ushort_2 != 1281 && ushort_2 != 2309)
			{
				CLogger.Print(string.Format("Connection destroyed due to unknown first packet. Opcode: {0}; IPAddress: {1}", ushort_2, this.GetIPAddress()), LoggerType.Hack, null);
				this.Close(0, true);
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000034EC File Offset: 0x000016EC
		public bool CheckSeed(ushort PacketSeed, bool IsTheFirstPacket)
		{
			if (PacketSeed == this.method_6())
			{
				return true;
			}
			CLogger.Print(string.Format("Connection blocked. IP: {0}; Date: {1}; SessionId: {2}; PacketSeed: {3} / NextSessionSeed: {4}; PrimarySeed: {5}", new object[]
			{
				this.GetIPAddress(),
				DateTimeUtil.Now(),
				this.SessionId,
				PacketSeed,
				this.ushort_0,
				this.SessionSeed
			}), LoggerType.Hack, null);
			if (IsTheFirstPacket)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.method_12));
			}
			return false;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000023C7 File Offset: 0x000005C7
		private ushort method_6()
		{
			this.ushort_0 = (ushort)(((int)this.ushort_0 * 214013 + 2531011 >> 16) & 32767);
			return this.ushort_0;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000357C File Offset: 0x0000177C
		private void method_7(ushort ushort_2, byte[] byte_0, string string_0)
		{
			AuthClient.Class1 @class = new AuthClient.Class1();
			@class.authClient_0 = this;
			try
			{
				@class.authClientPacket_0 = null;
				if (ushort_2 <= 2332)
				{
					if (ushort_2 <= 2311)
					{
						if (ushort_2 == 1057)
						{
							@class.authClientPacket_0 = new PROTOCOL_AUTH_GET_POINT_CASH_REQ();
							goto IL_22F;
						}
						if (ushort_2 == 1281)
						{
							@class.authClientPacket_0 = new PROTOCOL_BASE_LOGIN_REQ();
							goto IL_22F;
						}
						switch (ushort_2)
						{
						case 2307:
							@class.authClientPacket_0 = new PROTOCOL_BASE_LOGOUT_REQ();
							goto IL_22F;
						case 2309:
							@class.authClientPacket_0 = new PROTOCOL_BASE_KEEP_ALIVE_REQ();
							goto IL_22F;
						case 2311:
							@class.authClientPacket_0 = new PROTOCOL_BASE_GAMEGUARD_REQ();
							goto IL_22F;
						}
					}
					else
					{
						switch (ushort_2)
						{
						case 2314:
							@class.authClientPacket_0 = new PROTOCOL_BASE_GET_SYSTEM_INFO_REQ();
							goto IL_22F;
						case 2315:
						case 2317:
						case 2319:
						case 2321:
							break;
						case 2316:
							@class.authClientPacket_0 = new PROTOCOL_BASE_GET_USER_INFO_REQ();
							goto IL_22F;
						case 2318:
							@class.authClientPacket_0 = new PROTOCOL_BASE_GET_INVEN_INFO_REQ();
							goto IL_22F;
						case 2320:
							@class.authClientPacket_0 = new PROTOCOL_BASE_GET_OPTION_REQ();
							goto IL_22F;
						case 2322:
							@class.authClientPacket_0 = new PROTOCOL_BASE_OPTION_SAVE_REQ();
							goto IL_22F;
						default:
							if (ushort_2 == 2328)
							{
								@class.authClientPacket_0 = new PROTOCOL_BASE_USER_LEAVE_REQ();
								goto IL_22F;
							}
							if (ushort_2 == 2332)
							{
								@class.authClientPacket_0 = new PROTOCOL_BASE_GET_CHANNELLIST_REQ();
								goto IL_22F;
							}
							break;
						}
					}
				}
				else if (ushort_2 <= 2459)
				{
					if (ushort_2 == 2399)
					{
						@class.authClientPacket_0 = new PROTOCOL_BASE_GAME_SERVER_STATE_REQ();
						goto IL_22F;
					}
					if (ushort_2 == 2414)
					{
						@class.authClientPacket_0 = new PROTOCOL_BASE_DAILY_RECORD_REQ();
						goto IL_22F;
					}
					if (ushort_2 == 2459)
					{
						@class.authClientPacket_0 = new PROTOCOL_BASE_GET_MAP_INFO_REQ();
						goto IL_22F;
					}
				}
				else if (ushort_2 <= 2516)
				{
					if (ushort_2 == 2489)
					{
						@class.authClientPacket_0 = new PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_REQ();
						goto IL_22F;
					}
					if (ushort_2 == 2516)
					{
						@class.authClientPacket_0 = new PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_REQ();
						goto IL_22F;
					}
				}
				else
				{
					if (ushort_2 == 7681)
					{
						@class.authClientPacket_0 = new PROTOCOL_MATCH_SERVER_IDX_REQ();
						goto IL_22F;
					}
					if (ushort_2 == 7699)
					{
						@class.authClientPacket_0 = new PROTOCOL_MATCH_CLAN_SEASON_REQ();
						goto IL_22F;
					}
				}
				CLogger.Print(Bitwise.ToHexData(string.Format("Opcode Not Found: [{0}] | {1}", ushort_2, string_0), byte_0), LoggerType.Opcode, null);
				IL_22F:
				if (@class.authClientPacket_0 != null)
				{
					using (@class.authClientPacket_0)
					{
						if (ConfigLoader.DebugMode)
						{
							CLogger.Print(string.Format("{0}; Address: {1}; Opcode: [{2}]", @class.authClientPacket_0.GetType().Name, this.Client.RemoteEndPoint, ushort_2), LoggerType.Debug, null);
						}
						@class.authClientPacket_0.Makeme(this, byte_0);
						ThreadPool.QueueUserWorkItem(new WaitCallback(@class.method_0));
						@class.authClientPacket_0.Dispose();
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023F1 File Offset: 0x000005F1
		[CompilerGenerated]
		private void method_8(object object_0)
		{
			this.method_1();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000023F9 File Offset: 0x000005F9
		[CompilerGenerated]
		private void method_9(object object_0)
		{
			this.method_3();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002401 File Offset: 0x00000601
		[CompilerGenerated]
		private void method_10(object object_0)
		{
			this.method_0();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000387C File Offset: 0x00001A7C
		[CompilerGenerated]
		private void method_11(object object_0)
		{
			try
			{
				this.method_3();
			}
			catch (Exception ex)
			{
				CLogger.Print("Error processing packet in thread pool for IP: " + this.GetIPAddress() + ": " + ex.Message, LoggerType.Error, ex);
				this.Close(0, true);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023F9 File Offset: 0x000005F9
		[CompilerGenerated]
		private void method_12(object object_0)
		{
			this.method_3();
		}

		// Token: 0x04000001 RID: 1
		public int ServerId;

		// Token: 0x04000002 RID: 2
		public Socket Client;

		// Token: 0x04000003 RID: 3
		public Account Player;

		// Token: 0x04000004 RID: 4
		public DateTime SessionDate;

		// Token: 0x04000005 RID: 5
		public int SessionId;

		// Token: 0x04000006 RID: 6
		public ushort SessionSeed;

		// Token: 0x04000007 RID: 7
		private ushort ushort_0;

		// Token: 0x04000008 RID: 8
		public int SessionShift;

		// Token: 0x04000009 RID: 9
		public int FirstPacketId;

		// Token: 0x0400000A RID: 10
		private bool bool_0;

		// Token: 0x0400000B RID: 11
		private bool bool_1;

		// Token: 0x0400000C RID: 12
		private readonly SafeHandle safeHandle_0 = new SafeFileHandle(IntPtr.Zero, true);

		// Token: 0x0400000D RID: 13
		private readonly ushort[] ushort_1 = new ushort[] { 1030, 1099, 2499 };

		// Token: 0x02000003 RID: 3
		[CompilerGenerated]
		private sealed class Class0
		{
			// Token: 0x06000020 RID: 32 RVA: 0x00002409 File Offset: 0x00000609
			public Class0()
			{
			}

			// Token: 0x06000021 RID: 33 RVA: 0x000038D0 File Offset: 0x00001AD0
			internal void method_0(object object_0)
			{
				if (!this.authClient_0.bool_1)
				{
					CLogger.Print("Connection destroyed due to no response for 20 minutes (HeartBeat). IPAddress: " + this.authClient_0.GetIPAddress(), LoggerType.Hack, null);
					this.authClient_0.Close(0, true);
				}
				lock (object_0)
				{
					this.timerState_0.StopJob();
				}
			}

			// Token: 0x0400000E RID: 14
			public AuthClient authClient_0;

			// Token: 0x0400000F RID: 15
			public TimerState timerState_0;
		}

		// Token: 0x02000004 RID: 4
		[CompilerGenerated]
		private sealed class Class1
		{
			// Token: 0x06000022 RID: 34 RVA: 0x00002409 File Offset: 0x00000609
			public Class1()
			{
			}

			// Token: 0x06000023 RID: 35 RVA: 0x00003948 File Offset: 0x00001B48
			internal void method_0(object object_0)
			{
				try
				{
					this.authClientPacket_0.Run();
				}
				catch (Exception ex)
				{
					CLogger.Print(ex.Message, LoggerType.Error, ex);
					this.authClient_0.Close(50, true);
				}
			}

			// Token: 0x04000010 RID: 16
			public AuthClient authClient_0;

			// Token: 0x04000011 RID: 17
			public AuthClientPacket authClientPacket_0;
		}
	}
}
