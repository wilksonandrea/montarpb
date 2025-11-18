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
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ClientPacket;
using Server.Game.Network.ServerPacket;

namespace Server.Game
{
	// Token: 0x02000003 RID: 3
	public class GameClient : IDisposable
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002515 File Offset: 0x00000715
		public GameClient(int int_0, Socket socket_0)
		{
			this.ServerId = int_0;
			this.Client = socket_0;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000064C4 File Offset: 0x000046C4
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

		// Token: 0x0600000E RID: 14 RVA: 0x00006500 File Offset: 0x00004700
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
					this.PlayerId = 0L;
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

		// Token: 0x0600000F RID: 15 RVA: 0x00006580 File Offset: 0x00004780
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
				this.Close(0, true, false);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002553 File Offset: 0x00000753
		private void method_0()
		{
			Thread.Sleep(10000);
			if (this.Client != null && this.FirstPacketId == 0)
			{
				CLogger.Print("Connection destroyed due to no responses. IPAddress: " + this.GetIPAddress(), LoggerType.Hack, null);
				this.Close(0, true, false);
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00006614 File Offset: 0x00004814
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

		// Token: 0x06000012 RID: 18 RVA: 0x00006678 File Offset: 0x00004878
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

		// Token: 0x06000013 RID: 19 RVA: 0x0000258F File Offset: 0x0000078F
		private void method_1()
		{
			this.SendPacket(new PROTOCOL_BASE_CONNECT_ACK(this));
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000066CC File Offset: 0x000048CC
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
				this.Close(0, true, false);
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000067BC File Offset: 0x000049BC
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
				this.Close(0, true, false);
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000067F8 File Offset: 0x000049F8
		public void SendPacket(GameServerPacket Packet)
		{
			try
			{
				try
				{
					byte[] bytes = Packet.GetBytes("GameClient.SendPacket");
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
				this.Close(0, true, false);
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00006864 File Offset: 0x00004A64
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
				this.Close(0, true, false);
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000068B0 File Offset: 0x00004AB0
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
				this.Close(0, true, false);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00006924 File Offset: 0x00004B24
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
						this.Close(0, true, false);
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
								this.Close(0, true, false);
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
							this.Close(0, true, false);
						}
					}
				}
			}
			catch
			{
				this.Close(0, true, false);
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00006A98 File Offset: 0x00004C98
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
								this.Close(0, true, false);
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
							this.Close(0, true, false);
						}
					}
				}
			}
			catch
			{
				this.Close(0, true, false);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00006BA0 File Offset: 0x00004DA0
		public void Close(int TimeMS, bool DestroyConnection, bool Kicked = false)
		{
			if (this.bool_1)
			{
				return;
			}
			try
			{
				this.bool_1 = true;
				GameXender.Client.RemoveSession(this);
				Account player = this.Player;
				if (DestroyConnection)
				{
					if (this.PlayerId > 0L && player != null)
					{
						player.SetOnlineStatus(false);
						RoomModel room = player.Room;
						if (room != null)
						{
							room.RemovePlayer(player, false, (Kicked > false) ? 1 : 0);
						}
						MatchModel match = player.Match;
						if (match != null)
						{
							match.RemovePlayer(player);
						}
						ChannelModel channel = player.GetChannel();
						if (channel != null)
						{
							channel.RemovePlayer(player);
						}
						player.Status.ResetData(this.PlayerId);
						AllUtils.SyncPlayerToFriends(player, false);
						AllUtils.SyncPlayerToClanMembers(player);
						player.SimpleClear();
						player.UpdateCacheInfo();
						this.Player = null;
					}
					this.PlayerId = 0L;
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
				CLogger.Print("GameClient.Close: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00006CE0 File Offset: 0x00004EE0
		private void method_5(ushort ushort_2)
		{
			if (this.FirstPacketId != 0)
			{
				return;
			}
			this.FirstPacketId = (int)ushort_2;
			if (ushort_2 != 2330 && ushort_2 != 2309)
			{
				CLogger.Print(string.Format("Connection destroyed due to unknown first packet. Opcode: {0}; IPAddress: {1}", ushort_2, this.GetIPAddress()), LoggerType.Hack, null);
				this.Close(0, true, false);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00006D34 File Offset: 0x00004F34
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

		// Token: 0x0600001E RID: 30 RVA: 0x0000259D File Offset: 0x0000079D
		private ushort method_6()
		{
			this.ushort_0 = (ushort)(((int)this.ushort_0 * 214013 + 2531011 >> 16) & 32767);
			return this.ushort_0;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00006DC4 File Offset: 0x00004FC4
		private void method_7(ushort ushort_2, byte[] byte_0, string string_0)
		{
			GameClient.Class1 @class = new GameClient.Class1();
			@class.gameClient_0 = this;
			try
			{
				@class.gameClientPacket_0 = null;
				if (ushort_2 <= 2508)
				{
					if (ushort_2 <= 1811)
					{
						if (ushort_2 <= 892)
						{
							if (ushort_2 <= 830)
							{
								if (ushort_2 <= 822)
								{
									if (ushort_2 == 769)
									{
										@class.gameClientPacket_0 = new PROTOCOL_CS_CLIENT_ENTER_REQ();
										goto IL_126A;
									}
									if (ushort_2 == 771)
									{
										@class.gameClientPacket_0 = new PROTOCOL_CS_CLIENT_LEAVE_REQ();
										goto IL_126A;
									}
									switch (ushort_2)
									{
									case 800:
										@class.gameClientPacket_0 = new PROTOCOL_CS_DETAIL_INFO_REQ();
										goto IL_126A;
									case 802:
										@class.gameClientPacket_0 = new PROTOCOL_CS_MEMBER_CONTEXT_REQ();
										goto IL_126A;
									case 804:
										@class.gameClientPacket_0 = new PROTOCOL_CS_MEMBER_LIST_REQ();
										goto IL_126A;
									case 806:
										@class.gameClientPacket_0 = new PROTOCOL_CS_CREATE_CLAN_REQ();
										goto IL_126A;
									case 808:
										@class.gameClientPacket_0 = new PROTOCOL_CS_CLOSE_CLAN_REQ();
										goto IL_126A;
									case 810:
										@class.gameClientPacket_0 = new PROTOCOL_CS_CHECK_JOIN_AUTHORITY_ERQ();
										goto IL_126A;
									case 812:
										@class.gameClientPacket_0 = new PROTOCOL_CS_JOIN_REQUEST_REQ();
										goto IL_126A;
									case 814:
										@class.gameClientPacket_0 = new PROTOCOL_CS_CANCEL_REQUEST_REQ();
										goto IL_126A;
									case 816:
										@class.gameClientPacket_0 = new PROTOCOL_CS_REQUEST_CONTEXT_REQ();
										goto IL_126A;
									case 818:
										@class.gameClientPacket_0 = new PROTOCOL_CS_REQUEST_LIST_REQ();
										goto IL_126A;
									case 820:
										@class.gameClientPacket_0 = new PROTOCOL_CS_REQUEST_INFO_REQ();
										goto IL_126A;
									case 822:
										@class.gameClientPacket_0 = new PROTOCOL_CS_ACCEPT_REQUEST_REQ();
										goto IL_126A;
									}
								}
								else
								{
									if (ushort_2 == 825)
									{
										@class.gameClientPacket_0 = new PROTOCOL_CS_DENIAL_REQUEST_REQ();
										goto IL_126A;
									}
									if (ushort_2 == 828)
									{
										@class.gameClientPacket_0 = new PROTOCOL_CS_SECESSION_CLAN_REQ();
										goto IL_126A;
									}
									if (ushort_2 == 830)
									{
										@class.gameClientPacket_0 = new PROTOCOL_CS_DEPORTATION_REQ();
										goto IL_126A;
									}
								}
							}
							else if (ushort_2 <= 839)
							{
								if (ushort_2 == 833)
								{
									@class.gameClientPacket_0 = new PROTOCOL_CS_COMMISSION_MASTER_REQ();
									goto IL_126A;
								}
								if (ushort_2 == 836)
								{
									@class.gameClientPacket_0 = new PROTOCOL_CS_COMMISSION_STAFF_REQ();
									goto IL_126A;
								}
								if (ushort_2 == 839)
								{
									@class.gameClientPacket_0 = new PROTOCOL_CS_COMMISSION_REGULAR_REQ();
									goto IL_126A;
								}
							}
							else if (ushort_2 <= 868)
							{
								switch (ushort_2)
								{
								case 854:
									@class.gameClientPacket_0 = new PROTOCOL_CS_CHATTING_REQ();
									goto IL_126A;
								case 855:
								case 857:
								case 859:
									break;
								case 856:
									@class.gameClientPacket_0 = new PROTOCOL_CS_CHECK_MARK_REQ();
									goto IL_126A;
								case 858:
									@class.gameClientPacket_0 = new PROTOCOL_CS_REPLACE_NOTICE_REQ();
									goto IL_126A;
								case 860:
									@class.gameClientPacket_0 = new PROTOCOL_CS_REPLACE_INTRO_REQ();
									goto IL_126A;
								default:
									if (ushort_2 == 868)
									{
										@class.gameClientPacket_0 = new PROTOCOL_CS_REPLACE_MANAGEMENT_REQ();
										goto IL_126A;
									}
									break;
								}
							}
							else
							{
								if (ushort_2 == 877)
								{
									@class.gameClientPacket_0 = new PROTOCOL_CS_ROOM_INVITED_REQ();
									goto IL_126A;
								}
								switch (ushort_2)
								{
								case 886:
									@class.gameClientPacket_0 = new PROTOCOL_CS_PAGE_CHATTING_REQ();
									goto IL_126A;
								case 888:
									@class.gameClientPacket_0 = new PROTOCOL_CS_INVITE_REQ();
									goto IL_126A;
								case 890:
									@class.gameClientPacket_0 = new PROTOCOL_CS_INVITE_ACCEPT_REQ();
									goto IL_126A;
								case 892:
									@class.gameClientPacket_0 = new PROTOCOL_CS_NOTE_REQ();
									goto IL_126A;
								}
							}
						}
						else if (ushort_2 <= 1049)
						{
							if (ushort_2 <= 997)
							{
								if (ushort_2 == 914)
								{
									@class.gameClientPacket_0 = new PROTOCOL_CS_CREATE_CLAN_CONDITION_REQ();
									goto IL_126A;
								}
								if (ushort_2 == 916)
								{
									@class.gameClientPacket_0 = new PROTOCOL_CS_CHECK_DUPLICATE_REQ();
									goto IL_126A;
								}
								if (ushort_2 == 997)
								{
									@class.gameClientPacket_0 = new PROTOCOL_CS_CLAN_LIST_FILTER_REQ();
									goto IL_126A;
								}
							}
							else
							{
								if (ushort_2 == 999)
								{
									@class.gameClientPacket_0 = new PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_REQ();
									goto IL_126A;
								}
								switch (ushort_2)
								{
								case 1025:
									@class.gameClientPacket_0 = new PROTOCOL_SHOP_ENTER_REQ();
									goto IL_126A;
								case 1026:
								case 1028:
									break;
								case 1027:
									@class.gameClientPacket_0 = new PROTOCOL_SHOP_LEAVE_REQ();
									goto IL_126A;
								case 1029:
									@class.gameClientPacket_0 = new PROTOCOL_SHOP_GET_SAILLIST_REQ();
									goto IL_126A;
								default:
									switch (ushort_2)
									{
									case 1041:
										@class.gameClientPacket_0 = new PROTOCOL_AUTH_SHOP_GET_GIFTLIST_REQ();
										goto IL_126A;
									case 1043:
										@class.gameClientPacket_0 = new PROTOCOL_AUTH_SHOP_GOODS_BUY_REQ();
										goto IL_126A;
									case 1045:
										@class.gameClientPacket_0 = new PROTOCOL_AUTH_SHOP_GOODS_GIFT_REQ();
										goto IL_126A;
									case 1047:
										@class.gameClientPacket_0 = new PROTOCOL_AUTH_SHOP_ITEM_AUTH_REQ();
										goto IL_126A;
									case 1049:
										@class.gameClientPacket_0 = new PROTOCOL_INVENTORY_USE_ITEM_REQ();
										goto IL_126A;
									}
									break;
								}
							}
						}
						else if (ushort_2 <= 1075)
						{
							switch (ushort_2)
							{
							case 1053:
								@class.gameClientPacket_0 = new PROTOCOL_AUTH_SHOP_AUTH_GIFT_REQ();
								goto IL_126A;
							case 1054:
							case 1056:
								break;
							case 1055:
								@class.gameClientPacket_0 = new PROTOCOL_AUTH_SHOP_DELETE_ITEM_REQ();
								goto IL_126A;
							case 1057:
								@class.gameClientPacket_0 = new PROTOCOL_AUTH_GET_POINT_CASH_REQ();
								goto IL_126A;
							default:
								if (ushort_2 == 1061)
								{
									@class.gameClientPacket_0 = new PROTOCOL_AUTH_USE_ITEM_CHECK_NICK_REQ();
									goto IL_126A;
								}
								if (ushort_2 == 1075)
								{
									@class.gameClientPacket_0 = new PROTOCOL_AUTH_SHOP_EXTEND_REQ();
									goto IL_126A;
								}
								break;
							}
						}
						else if (ushort_2 <= 1084)
						{
							if (ushort_2 == 1076)
							{
								@class.gameClientPacket_0 = new PROTOCOL_SHOP_REPAIR_REQ();
								goto IL_126A;
							}
							if (ushort_2 == 1084)
							{
								@class.gameClientPacket_0 = new PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_REQ();
								goto IL_126A;
							}
						}
						else
						{
							if (ushort_2 == 1087)
							{
								@class.gameClientPacket_0 = new PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ();
								goto IL_126A;
							}
							if (ushort_2 == 1811)
							{
								@class.gameClientPacket_0 = new PROTOCOL_AUTH_FRIEND_INVITED_REQ();
								goto IL_126A;
							}
						}
					}
					else if (ushort_2 <= 2364)
					{
						if (ushort_2 <= 2307)
						{
							if (ushort_2 <= 1828)
							{
								switch (ushort_2)
								{
								case 1816:
									@class.gameClientPacket_0 = new PROTOCOL_AUTH_FRIEND_ACCEPT_REQ();
									goto IL_126A;
								case 1817:
								case 1819:
									break;
								case 1818:
									@class.gameClientPacket_0 = new PROTOCOL_AUTH_FRIEND_INSERT_REQ();
									goto IL_126A;
								case 1820:
									@class.gameClientPacket_0 = new PROTOCOL_AUTH_FRIEND_DELETE_REQ();
									goto IL_126A;
								default:
									if (ushort_2 == 1826)
									{
										@class.gameClientPacket_0 = new PROTOCOL_AUTH_SEND_WHISPER_NOPID_REQ();
										goto IL_126A;
									}
									if (ushort_2 == 1828)
									{
										@class.gameClientPacket_0 = new PROTOCOL_AUTH_SEND_WHISPER_REQ();
										goto IL_126A;
									}
									break;
								}
							}
							else
							{
								if (ushort_2 == 1921)
								{
									@class.gameClientPacket_0 = new PROTOCOL_MESSENGER_NOTE_SEND_REQ();
									goto IL_126A;
								}
								switch (ushort_2)
								{
								case 1926:
									@class.gameClientPacket_0 = new PROTOCOL_MESSENGER_NOTE_CHECK_READED_REQ();
									goto IL_126A;
								case 1927:
								case 1929:
									break;
								case 1928:
									@class.gameClientPacket_0 = new PROTOCOL_MESSENGER_NOTE_DELETE_REQ();
									goto IL_126A;
								case 1930:
									@class.gameClientPacket_0 = new PROTOCOL_MESSENGER_NOTE_RECEIVE_REQ();
									goto IL_126A;
								default:
									if (ushort_2 == 2307)
									{
										@class.gameClientPacket_0 = new PROTOCOL_BASE_LOGOUT_REQ();
										goto IL_126A;
									}
									break;
								}
							}
						}
						else if (ushort_2 <= 2322)
						{
							if (ushort_2 == 2309)
							{
								@class.gameClientPacket_0 = new PROTOCOL_BASE_KEEP_ALIVE_REQ();
								goto IL_126A;
							}
							if (ushort_2 == 2312)
							{
								@class.gameClientPacket_0 = new PROTOCOL_BASE_GAMEGUARD_REQ();
								goto IL_126A;
							}
							if (ushort_2 == 2322)
							{
								@class.gameClientPacket_0 = new PROTOCOL_BASE_OPTION_SAVE_REQ();
								goto IL_126A;
							}
						}
						else if (ushort_2 <= 2350)
						{
							switch (ushort_2)
							{
							case 2326:
								@class.gameClientPacket_0 = new PROTOCOL_BASE_CREATE_NICK_REQ();
								goto IL_126A;
							case 2327:
							case 2329:
							case 2331:
							case 2333:
							case 2335:
							case 2337:
								break;
							case 2328:
								@class.gameClientPacket_0 = new PROTOCOL_BASE_USER_LEAVE_REQ();
								goto IL_126A;
							case 2330:
								@class.gameClientPacket_0 = new PROTOCOL_BASE_USER_ENTER_REQ();
								goto IL_126A;
							case 2332:
								@class.gameClientPacket_0 = new PROTOCOL_BASE_GET_CHANNELLIST_REQ();
								goto IL_126A;
							case 2334:
								@class.gameClientPacket_0 = new PROTOCOL_BASE_SELECT_CHANNEL_REQ();
								goto IL_126A;
							case 2336:
								@class.gameClientPacket_0 = new PROTOCOL_BASE_ATTENDANCE_REQ();
								goto IL_126A;
							case 2338:
								@class.gameClientPacket_0 = new PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_REQ();
								goto IL_126A;
							default:
								if (ushort_2 == 2350)
								{
									@class.gameClientPacket_0 = new PROTOCOL_BASE_GET_RECORD_INFO_DB_REQ();
									goto IL_126A;
								}
								break;
							}
						}
						else
						{
							if (ushort_2 == 2360)
							{
								@class.gameClientPacket_0 = new PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_REQ();
								goto IL_126A;
							}
							if (ushort_2 == 2364)
							{
								@class.gameClientPacket_0 = new PROTOCOL_BASE_QUEST_BUY_CARD_SET_REQ();
								goto IL_126A;
							}
						}
					}
					else if (ushort_2 <= 2414)
					{
						if (ushort_2 <= 2384)
						{
							if (ushort_2 == 2366)
							{
								@class.gameClientPacket_0 = new PROTOCOL_BASE_QUEST_DELETE_CARD_SET_REQ();
								goto IL_126A;
							}
							switch (ushort_2)
							{
							case 2376:
								@class.gameClientPacket_0 = new PROTOCOL_BASE_USER_TITLE_CHANGE_REQ();
								goto IL_126A;
							case 2377:
							case 2379:
								break;
							case 2378:
								@class.gameClientPacket_0 = new PROTOCOL_BASE_USER_TITLE_EQUIP_REQ();
								goto IL_126A;
							case 2380:
								@class.gameClientPacket_0 = new PROTOCOL_BASE_USER_TITLE_RELEASE_REQ();
								goto IL_126A;
							default:
								if (ushort_2 == 2384)
								{
									@class.gameClientPacket_0 = new PROTOCOL_BASE_CHATTING_REQ();
									goto IL_126A;
								}
								break;
							}
						}
						else if (ushort_2 <= 2399)
						{
							if (ushort_2 == 2392)
							{
								goto IL_126A;
							}
							if (ushort_2 == 2399)
							{
								@class.gameClientPacket_0 = new PROTOCOL_BASE_GAME_SERVER_STATE_REQ();
								goto IL_126A;
							}
						}
						else
						{
							if (ushort_2 == 2401)
							{
								@class.gameClientPacket_0 = new PROTOCOL_BASE_ENTER_PASS_REQ();
								goto IL_126A;
							}
							if (ushort_2 == 2414)
							{
								@class.gameClientPacket_0 = new PROTOCOL_BASE_DAILY_RECORD_REQ();
								goto IL_126A;
							}
						}
					}
					else if (ushort_2 <= 2465)
					{
						switch (ushort_2)
						{
						case 2422:
							@class.gameClientPacket_0 = new PROTOCOL_BASE_GET_USER_DETAIL_INFO_REQ();
							goto IL_126A;
						case 2423:
							break;
						case 2424:
							@class.gameClientPacket_0 = new PROTOCOL_BASE_GET_ROOM_USER_DETAIL_INFO_REQ();
							goto IL_126A;
						case 2425:
							@class.gameClientPacket_0 = new PROTOCOL_BASE_GET_USER_BASIC_INFO_REQ();
							goto IL_126A;
						case 2426:
							@class.gameClientPacket_0 = new PROTOCOL_AUTH_FIND_USER_REQ();
							goto IL_126A;
						default:
							if (ushort_2 == 2447)
							{
								@class.gameClientPacket_0 = new PROTOCOL_BASE_GET_USER_SUBTASK_REQ();
								goto IL_126A;
							}
							if (ushort_2 == 2465)
							{
								@class.gameClientPacket_0 = new PROTOCOL_BASE_URL_LIST_REQ();
								goto IL_126A;
							}
							break;
						}
					}
					else if (ushort_2 <= 2491)
					{
						if (ushort_2 == 2489)
						{
							@class.gameClientPacket_0 = new PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_REQ();
							goto IL_126A;
						}
						if (ushort_2 == 2491)
						{
							@class.gameClientPacket_0 = new PROTOCOL_LOBBY_NEW_MYINFO_REQ();
							goto IL_126A;
						}
					}
					else
					{
						if (ushort_2 == 2498)
						{
							@class.gameClientPacket_0 = new PROTOCOL_BASE_RANDOMBOX_LIST_REQ();
							goto IL_126A;
						}
						if (ushort_2 == 2508)
						{
							@class.gameClientPacket_0 = new PROTOCOL_BASE_TICKET_UPDATE_REQ();
							goto IL_126A;
						}
					}
				}
				else if (ushort_2 <= 3852)
				{
					if (ushort_2 <= 3596)
					{
						if (ushort_2 <= 2583)
						{
							if (ushort_2 <= 2519)
							{
								if (ushort_2 == 2510)
								{
									@class.gameClientPacket_0 = new PROTOCOL_BASE_EVENT_PORTAL_REQ();
									goto IL_126A;
								}
								if (ushort_2 == 2518 || ushort_2 == 2519)
								{
									goto IL_126A;
								}
							}
							else
							{
								if (ushort_2 == 2561)
								{
									@class.gameClientPacket_0 = new PROTOCOL_LOBBY_LEAVE_REQ();
									goto IL_126A;
								}
								if (ushort_2 == 2567)
								{
									@class.gameClientPacket_0 = new PROTOCOL_LOBBY_GET_ROOMINFOADD_REQ();
									goto IL_126A;
								}
								if (ushort_2 == 2583)
								{
									@class.gameClientPacket_0 = new PROTOCOL_LOBBY_ENTER_REQ();
									goto IL_126A;
								}
							}
						}
						else if (ushort_2 <= 3329)
						{
							if (ushort_2 == 2587)
							{
								@class.gameClientPacket_0 = new PROTOCOL_LOBBY_GET_ROOMLIST_REQ();
								goto IL_126A;
							}
							if (ushort_2 == 3083)
							{
								@class.gameClientPacket_0 = new PROTOCOL_LOBBY_GET_ROOMINFOADD_REQ();
								goto IL_126A;
							}
							if (ushort_2 == 3329)
							{
								@class.gameClientPacket_0 = new PROTOCOL_INVENTORY_ENTER_REQ();
								goto IL_126A;
							}
						}
						else if (ushort_2 <= 3585)
						{
							if (ushort_2 == 3331)
							{
								@class.gameClientPacket_0 = new PROTOCOL_INVENTORY_LEAVE_REQ();
								goto IL_126A;
							}
							if (ushort_2 == 3585)
							{
								@class.gameClientPacket_0 = new PROTOCOL_ROOM_JOIN_REQ();
								goto IL_126A;
							}
						}
						else
						{
							if (ushort_2 == 3592)
							{
								@class.gameClientPacket_0 = new PROTOCOL_ROOM_CREATE_REQ();
								goto IL_126A;
							}
							if (ushort_2 == 3596)
							{
								@class.gameClientPacket_0 = new PROTOCOL_ROOM_GET_PLAYERINFO_REQ();
								goto IL_126A;
							}
						}
					}
					else if (ushort_2 <= 3643)
					{
						if (ushort_2 <= 3619)
						{
							if (ushort_2 == 3602)
							{
								@class.gameClientPacket_0 = new PROTOCOL_ROOM_CHANGE_PASSWD_REQ();
								goto IL_126A;
							}
							if (ushort_2 == 3604)
							{
								@class.gameClientPacket_0 = new PROTOCOL_ROOM_CHANGE_SLOT_REQ();
								goto IL_126A;
							}
							switch (ushort_2)
							{
							case 3609:
								@class.gameClientPacket_0 = new PROTOCOL_ROOM_PERSONAL_TEAM_CHANGE_REQ();
								goto IL_126A;
							case 3611:
								@class.gameClientPacket_0 = new PROTOCOL_ROOM_REQUEST_MAIN_REQ();
								goto IL_126A;
							case 3613:
								@class.gameClientPacket_0 = new Class3();
								goto IL_126A;
							case 3615:
								@class.gameClientPacket_0 = new PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_REQ();
								goto IL_126A;
							case 3617:
								@class.gameClientPacket_0 = new Class2();
								goto IL_126A;
							case 3619:
								@class.gameClientPacket_0 = new PROTOCOL_ROOM_TOTAL_TEAM_CHANGE_REQ();
								goto IL_126A;
							}
						}
						else if (ushort_2 <= 3635)
						{
							if (ushort_2 == 3631)
							{
								@class.gameClientPacket_0 = new PROTOCOL_ROOM_GET_LOBBY_USER_LIST_REQ();
								goto IL_126A;
							}
							if (ushort_2 == 3635)
							{
								@class.gameClientPacket_0 = new PROTOCOL_BASE_UNKNOWN_3635_REQ();
								goto IL_126A;
							}
						}
						else
						{
							if (ushort_2 == 3639)
							{
								@class.gameClientPacket_0 = new PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ();
								goto IL_126A;
							}
							if (ushort_2 == 3643)
							{
								@class.gameClientPacket_0 = new PROTOCOL_GM_KICK_COMMAND_REQ();
								goto IL_126A;
							}
						}
					}
					else if (ushort_2 <= 3665)
					{
						if (ushort_2 == 3647)
						{
							@class.gameClientPacket_0 = new PROTOCOL_GM_EXIT_COMMAND_REQ();
							goto IL_126A;
						}
						if (ushort_2 == 3657)
						{
							@class.gameClientPacket_0 = new PROTOCOL_ROOM_LOADING_START_REQ();
							goto IL_126A;
						}
						if (ushort_2 == 3665)
						{
							@class.gameClientPacket_0 = new PROTOCOL_ROOM_GET_USER_EQUIPMENT_REQ();
							goto IL_126A;
						}
					}
					else if (ushort_2 <= 3686)
					{
						switch (ushort_2)
						{
						case 3671:
							@class.gameClientPacket_0 = new PROTOCOL_ROOM_INFO_ENTER_REQ();
							goto IL_126A;
						case 3672:
						case 3674:
						case 3676:
						case 3678:
						case 3681:
							break;
						case 3673:
							@class.gameClientPacket_0 = new PROTOCOL_ROOM_INFO_LEAVE_REQ();
							goto IL_126A;
						case 3675:
							@class.gameClientPacket_0 = new PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_REQ();
							goto IL_126A;
						case 3677:
							@class.gameClientPacket_0 = new PROTOCOL_ROOM_CHANGE_COSTUME_REQ();
							goto IL_126A;
						case 3679:
							@class.gameClientPacket_0 = new PROTOCOL_ROOM_SELECT_SLOT_CHANGE_REQ();
							goto IL_126A;
						case 3680:
							@class.gameClientPacket_0 = new PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_REQ();
							goto IL_126A;
						case 3682:
							@class.gameClientPacket_0 = new PROTOCOL_ROOM_SELECT_SLOT_CHANGE_REQ();
							goto IL_126A;
						default:
							if (ushort_2 == 3686)
							{
								@class.gameClientPacket_0 = new PROTOCOL_ROOM_CHANGE_OBSERVER_SLOT_REQ();
								goto IL_126A;
							}
							break;
						}
					}
					else
					{
						if (ushort_2 == 3850)
						{
							@class.gameClientPacket_0 = new PROTOCOL_COMMUNITY_USER_REPORT_REQ();
							goto IL_126A;
						}
						if (ushort_2 == 3852)
						{
							@class.gameClientPacket_0 = new PROTOCOL_COMMUNITY_USER_REPORT_CONDITION_CHECK_REQ();
							goto IL_126A;
						}
					}
				}
				else if (ushort_2 <= 5188)
				{
					if (ushort_2 <= 5158)
					{
						if (ushort_2 <= 5145)
						{
							if (ushort_2 == 5123)
							{
								@class.gameClientPacket_0 = new PROTOCOL_BATTLE_READYBATTLE_REQ();
								goto IL_126A;
							}
							switch (ushort_2)
							{
							case 5129:
								@class.gameClientPacket_0 = new PROTOCOL_BATTLE_PRESTARTBATTLE_REQ();
								goto IL_126A;
							case 5130:
							case 5132:
							case 5134:
							case 5136:
								break;
							case 5131:
								@class.gameClientPacket_0 = new PROTOCOL_BATTLE_STARTBATTLE_REQ();
								goto IL_126A;
							case 5133:
								@class.gameClientPacket_0 = new PROTOCOL_BATTLE_GIVEUPBATTLE_REQ();
								goto IL_126A;
							case 5135:
								@class.gameClientPacket_0 = new PROTOCOL_BATTLE_DEATH_REQ();
								goto IL_126A;
							case 5137:
								@class.gameClientPacket_0 = new PROTOCOL_BATTLE_RESPAWN_REQ();
								goto IL_126A;
							default:
								if (ushort_2 == 5145)
								{
									goto IL_126A;
								}
								break;
							}
						}
						else
						{
							if (ushort_2 == 5146)
							{
								@class.gameClientPacket_0 = new PROTOCOL_BATTLE_SENDPING_REQ();
								goto IL_126A;
							}
							if (ushort_2 == 5156)
							{
								@class.gameClientPacket_0 = new PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_REQ();
								goto IL_126A;
							}
							if (ushort_2 == 5158)
							{
								@class.gameClientPacket_0 = new PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_REQ();
								goto IL_126A;
							}
						}
					}
					else if (ushort_2 <= 5172)
					{
						if (ushort_2 == 5166)
						{
							@class.gameClientPacket_0 = new PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_REQ();
							goto IL_126A;
						}
						if (ushort_2 == 5168)
						{
							@class.gameClientPacket_0 = new PROTOCOL_BATTLE_TIMERSYNC_REQ();
							goto IL_126A;
						}
						if (ushort_2 == 5172)
						{
							@class.gameClientPacket_0 = new PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_REQ();
							goto IL_126A;
						}
					}
					else if (ushort_2 <= 5180)
					{
						if (ushort_2 == 5174)
						{
							@class.gameClientPacket_0 = new PROTOCOL_BATTLE_RESPAWN_FOR_AI_REQ();
							goto IL_126A;
						}
						if (ushort_2 == 5180)
						{
							@class.gameClientPacket_0 = new PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_REQ();
							goto IL_126A;
						}
					}
					else
					{
						if (ushort_2 == 5182)
						{
							@class.gameClientPacket_0 = new PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_REQ();
							goto IL_126A;
						}
						if (ushort_2 == 5188)
						{
							@class.gameClientPacket_0 = new PROTOCOL_BATTLE_MISSION_TUTORIAL_ROUND_END_REQ();
							goto IL_126A;
						}
					}
				}
				else if (ushort_2 <= 6151)
				{
					if (ushort_2 <= 5292)
					{
						if (ushort_2 == 5262)
						{
							@class.gameClientPacket_0 = new PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_REQ();
							goto IL_126A;
						}
						if (ushort_2 == 5276)
						{
							@class.gameClientPacket_0 = new PROTOCOL_BATTLE_USER_SOPETYPE_REQ();
							goto IL_126A;
						}
						if (ushort_2 == 5292)
						{
							@class.gameClientPacket_0 = new PROTOCOL_BASE_UNKNOWN_PACKET_REQ();
							goto IL_126A;
						}
					}
					else if (ushort_2 <= 6145)
					{
						if (ushort_2 == 5377)
						{
							@class.gameClientPacket_0 = new PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ();
							goto IL_126A;
						}
						if (ushort_2 == 6145)
						{
							@class.gameClientPacket_0 = new PROTOCOL_CHAR_CREATE_CHARA_REQ();
							goto IL_126A;
						}
					}
					else
					{
						if (ushort_2 == 6149)
						{
							@class.gameClientPacket_0 = new PROTOCOL_CHAR_CHANGE_EQUIP_REQ();
							goto IL_126A;
						}
						if (ushort_2 == 6151)
						{
							@class.gameClientPacket_0 = new PROTOCOL_CHAR_DELETE_CHARA_REQ();
							goto IL_126A;
						}
					}
				}
				else if (ushort_2 <= 7429)
				{
					switch (ushort_2)
					{
					case 6657:
						@class.gameClientPacket_0 = new PROTOCOL_GMCHAT_START_CHAT_REQ();
						goto IL_126A;
					case 6658:
					case 6660:
					case 6662:
					case 6664:
						break;
					case 6659:
						@class.gameClientPacket_0 = new PROTOCOL_GMCHAT_SEND_CHAT_REQ();
						goto IL_126A;
					case 6661:
						@class.gameClientPacket_0 = new PROTOCOL_GMCHAT_END_CHAT_REQ();
						goto IL_126A;
					case 6663:
						@class.gameClientPacket_0 = new PROTOCOL_GMCHAT_APPLY_PENALTY_REQ();
						goto IL_126A;
					case 6665:
						@class.gameClientPacket_0 = new PROTOCOL_GMCHAT_NOTI_USER_PENALTY_REQ();
						goto IL_126A;
					default:
						if (ushort_2 == 6965)
						{
							@class.gameClientPacket_0 = new PROTOCOL_CLAN_WAR_RESULT_REQ();
							goto IL_126A;
						}
						if (ushort_2 == 7429)
						{
							@class.gameClientPacket_0 = new PROTOCOL_BATTLEBOX_AUTH_REQ();
							goto IL_126A;
						}
						break;
					}
				}
				else if (ushort_2 <= 7699)
				{
					if (ushort_2 == 7681)
					{
						@class.gameClientPacket_0 = new PROTOCOL_MATCH_SERVER_IDX_REQ();
						goto IL_126A;
					}
					if (ushort_2 == 7699)
					{
						@class.gameClientPacket_0 = new PROTOCOL_MATCH_CLAN_SEASON_REQ();
						goto IL_126A;
					}
				}
				else
				{
					if (ushort_2 == 8449)
					{
						@class.gameClientPacket_0 = new PROTOCOL_SEASON_CHALLENGE_INFO_REQ();
						goto IL_126A;
					}
					if (ushort_2 == 8453)
					{
						@class.gameClientPacket_0 = new PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_REQ();
						goto IL_126A;
					}
				}
				CLogger.Print(Bitwise.ToHexData(string.Format("Opcode Not Found: [{0}] | {1}", ushort_2, string_0), byte_0), LoggerType.Opcode, null);
				IL_126A:
				if (@class.gameClientPacket_0 != null)
				{
					using (@class.gameClientPacket_0)
					{
						if (ConfigLoader.DebugMode)
						{
							CLogger.Print(string.Format("{0}; Address: {1}; Opcode: [{2}]", @class.gameClientPacket_0.GetType().Name, this.Client.RemoteEndPoint, ushort_2), LoggerType.Debug, null);
						}
						@class.gameClientPacket_0.Makeme(this, byte_0);
						ThreadPool.QueueUserWorkItem(new WaitCallback(@class.method_0));
						@class.gameClientPacket_0.Dispose();
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000025C7 File Offset: 0x000007C7
		[CompilerGenerated]
		private void method_8(object object_0)
		{
			this.method_1();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000025CF File Offset: 0x000007CF
		[CompilerGenerated]
		private void method_9(object object_0)
		{
			this.method_3();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000025D7 File Offset: 0x000007D7
		[CompilerGenerated]
		private void method_10(object object_0)
		{
			this.method_0();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00008100 File Offset: 0x00006300
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
				this.Close(0, true, false);
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000025CF File Offset: 0x000007CF
		[CompilerGenerated]
		private void method_12(object object_0)
		{
			this.method_3();
		}

		// Token: 0x04000003 RID: 3
		public int ServerId;

		// Token: 0x04000004 RID: 4
		public long PlayerId;

		// Token: 0x04000005 RID: 5
		public Socket Client;

		// Token: 0x04000006 RID: 6
		public Account Player;

		// Token: 0x04000007 RID: 7
		public DateTime SessionDate;

		// Token: 0x04000008 RID: 8
		public int SessionId;

		// Token: 0x04000009 RID: 9
		public ushort SessionSeed;

		// Token: 0x0400000A RID: 10
		private ushort ushort_0;

		// Token: 0x0400000B RID: 11
		public int SessionShift;

		// Token: 0x0400000C RID: 12
		public int FirstPacketId;

		// Token: 0x0400000D RID: 13
		private bool bool_0;

		// Token: 0x0400000E RID: 14
		private bool bool_1;

		// Token: 0x0400000F RID: 15
		private readonly SafeHandle safeHandle_0 = new SafeFileHandle(IntPtr.Zero, true);

		// Token: 0x04000010 RID: 16
		private readonly ushort[] ushort_1 = new ushort[] { 1030, 1099, 2499 };

		// Token: 0x02000004 RID: 4
		[CompilerGenerated]
		private sealed class Class1
		{
			// Token: 0x06000025 RID: 37 RVA: 0x000025DF File Offset: 0x000007DF
			public Class1()
			{
			}

			// Token: 0x06000026 RID: 38 RVA: 0x00008154 File Offset: 0x00006354
			internal void method_0(object object_0)
			{
				try
				{
					this.gameClientPacket_0.Run();
				}
				catch (Exception ex)
				{
					CLogger.Print(ex.Message, LoggerType.Error, ex);
					this.gameClient_0.Close(50, true, false);
				}
			}

			// Token: 0x04000011 RID: 17
			public GameClient gameClient_0;

			// Token: 0x04000012 RID: 18
			public GameClientPacket gameClientPacket_0;
		}
	}
}
