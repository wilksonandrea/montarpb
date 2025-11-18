using Microsoft.Win32.SafeHandles;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ClientPacket;
using Server.Game.Network.ServerPacket;
using System;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace Server.Game
{
	public class GameClient : IDisposable
	{
		public int ServerId;

		public long PlayerId;

		public Socket Client;

		public Account Player;

		public DateTime SessionDate;

		public int SessionId;

		public ushort SessionSeed;

		private ushort ushort_0;

		public int SessionShift;

		public int FirstPacketId;

		private bool bool_0;

		private bool bool_1;

		private readonly SafeHandle safeHandle_0 = new SafeFileHandle(IntPtr.Zero, true);

		private readonly ushort[] ushort_1 = new ushort[] { 1030, 1099, 2499 };

		public GameClient(int int_0, Socket socket_0)
		{
			this.ServerId = int_0;
			this.Client = socket_0;
		}

		public void CheckOut(byte[] BufferTotal, int FirstLength)
		{
			int length = (int)BufferTotal.Length;
			try
			{
				if (length > FirstLength + 3)
				{
					byte[] numArray = new byte[length - FirstLength - 3];
					Array.Copy(BufferTotal, FirstLength + 3, numArray, 0, (int)numArray.Length);
					if (numArray.Length != 0)
					{
						int uInt16 = BitConverter.ToUInt16(numArray, 0) & 32767;
						if (uInt16 <= 0 || uInt16 > (int)numArray.Length - 2)
						{
							CLogger.Print(string.Format("Invalid PacketLength in CheckOut. IP: {0}; Length: {1}; RawBytes: {2}", this.GetIPAddress(), uInt16, (int)numArray.Length), LoggerType.Hack, null);
							this.Close(0, true, false);
						}
						else
						{
							byte[] numArray1 = new byte[uInt16];
							Array.Copy(numArray, 2, numArray1, 0, (int)numArray1.Length);
							byte[] numArray2 = Bitwise.Decrypt(numArray1, this.SessionShift);
							ushort uInt161 = BitConverter.ToUInt16(numArray2, 0);
							if (this.CheckSeed(BitConverter.ToUInt16(numArray2, 2), false))
							{
								this.method_7(uInt161, numArray2, "REQ");
								this.CheckOut(numArray, uInt16);
							}
							else
							{
								this.Close(0, true, false);
							}
						}
					}
				}
			}
			catch
			{
				this.Close(0, true, false);
			}
		}

		public bool CheckSeed(ushort PacketSeed, bool IsTheFirstPacket)
		{
			if (PacketSeed == this.method_6())
			{
				return true;
			}
			CLogger.Print(string.Format("Connection blocked. IP: {0}; Date: {1}; SessionId: {2}; PacketSeed: {3} / NextSessionSeed: {4}; PrimarySeed: {5}", new object[] { this.GetIPAddress(), DateTimeUtil.Now(), this.SessionId, PacketSeed, this.ushort_0, this.SessionSeed }), LoggerType.Hack, null);
			if (IsTheFirstPacket)
			{
				ThreadPool.QueueUserWorkItem((object object_0) => this.method_3());
			}
			return false;
		}

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
							room.RemovePlayer(player, false, Kicked);
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
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("GameClient.Close: ", exception.Message), LoggerType.Error, exception);
			}
		}

		public void Dispose()
		{
			try
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}

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
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}

		public IPAddress GetAddress()
		{
			IPAddress address;
			try
			{
				if (this.Client == null || this.Client.RemoteEndPoint == null)
				{
					address = null;
				}
				else
				{
					address = (this.Client.RemoteEndPoint as IPEndPoint).Address;
				}
			}
			catch
			{
				address = null;
			}
			return address;
		}

		public string GetIPAddress()
		{
			string str;
			try
			{
				str = (this.Client == null || this.Client.RemoteEndPoint == null ? "" : (this.Client.RemoteEndPoint as IPEndPoint).Address.ToString());
			}
			catch
			{
				str = "";
			}
			return str;
		}

		private void method_0()
		{
			Thread.Sleep(10000);
			if (this.Client != null && this.FirstPacketId == 0)
			{
				CLogger.Print(string.Concat("Connection destroyed due to no responses. IPAddress: ", this.GetIPAddress()), LoggerType.Hack, null);
				this.Close(0, true, false);
			}
		}

		private void method_1()
		{
			this.SendPacket(new PROTOCOL_BASE_CONNECT_ACK(this));
		}

		private void method_2(IAsyncResult iasyncResult_0)
		{
			try
			{
				Socket asyncState = iasyncResult_0.AsyncState as Socket;
				if (asyncState != null && asyncState.Connected)
				{
					asyncState.EndSend(iasyncResult_0);
				}
			}
			catch
			{
				this.Close(0, true, false);
			}
		}

		private void method_3()
		{
			try
			{
				StateObject stateObject = new StateObject()
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

		private void method_4(IAsyncResult iasyncResult_0)
		{
			StateObject asyncState = iasyncResult_0.AsyncState as StateObject;
			try
			{
				int ınt32 = asyncState.WorkSocket.EndReceive(iasyncResult_0);
				if (ınt32 > 0)
				{
					if (ınt32 <= 8912)
					{
						byte[] numArray = new byte[ınt32];
						Array.Copy(asyncState.TcpBuffer, 0, numArray, 0, ınt32);
						int uInt16 = BitConverter.ToUInt16(numArray, 0) & 32767;
						if (uInt16 <= 0 || uInt16 > ınt32 - 2)
						{
							CLogger.Print(string.Format("Invalid PacketLength. IP: {0}; Length: {1}; RawBytes: {2}", this.GetIPAddress(), uInt16, ınt32), LoggerType.Hack, null);
							this.Close(0, true, false);
							return;
						}
						else
						{
							byte[] numArray1 = new byte[uInt16];
							Array.Copy(numArray, 2, numArray1, 0, (int)numArray1.Length);
							byte[] numArray2 = Bitwise.Decrypt(numArray1, this.SessionShift);
							ushort uInt161 = BitConverter.ToUInt16(numArray2, 0);
							ushort uInt162 = BitConverter.ToUInt16(numArray2, 2);
							this.method_5(uInt161);
							if (!this.CheckSeed(uInt162, true))
							{
								this.Close(0, true, false);
								return;
							}
							else if (!this.bool_1)
							{
								this.method_7(uInt161, numArray2, "REQ");
								this.CheckOut(numArray, uInt16);
								ThreadPool.QueueUserWorkItem((object object_0) => {
									try
									{
										this.method_3();
									}
									catch (Exception exception1)
									{
										Exception exception = exception1;
										CLogger.Print(string.Concat("Error processing packet in thread pool for IP: ", this.GetIPAddress(), ": ", exception.Message), LoggerType.Error, exception);
										this.Close(0, true, false);
									}
								});
							}
							else
							{
								return;
							}
						}
					}
					else
					{
						CLogger.Print(string.Concat("Received data exceeds buffer size. IP: ", this.GetIPAddress()), LoggerType.Error, null);
						this.Close(0, true, false);
						return;
					}
				}
			}
			catch
			{
				this.Close(0, true, false);
			}
		}

		private void method_5(ushort ushort_2)
		{
			if (this.FirstPacketId != 0)
			{
				return;
			}
			this.FirstPacketId = ushort_2;
			if (ushort_2 != 2330 && ushort_2 != 2309)
			{
				CLogger.Print(string.Format("Connection destroyed due to unknown first packet. Opcode: {0}; IPAddress: {1}", ushort_2, this.GetIPAddress()), LoggerType.Hack, null);
				this.Close(0, true, false);
			}
		}

		private ushort method_6()
		{
			this.ushort_0 = (ushort)(this.ushort_0 * 214013 + 2531011 >> 16 & 32767);
			return this.ushort_0;
		}

		private void method_7(ushort ushort_2, byte[] byte_0, string string_0)
		{
			try
			{
				GameClientPacket pROTOCOLCSCLIENTENTERREQ = null;
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
										pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CS_CLIENT_ENTER_REQ();
									}
									else if (ushort_2 == 771)
									{
										pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CS_CLIENT_LEAVE_REQ();
									}
									else
									{
										switch (ushort_2)
										{
											case 800:
											{
												pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CS_DETAIL_INFO_REQ();
												break;
											}
											case 802:
											{
												pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CS_MEMBER_CONTEXT_REQ();
												break;
											}
											case 804:
											{
												pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CS_MEMBER_LIST_REQ();
												break;
											}
											case 806:
											{
												pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CS_CREATE_CLAN_REQ();
												break;
											}
											case 808:
											{
												pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CS_CLOSE_CLAN_REQ();
												break;
											}
											case 810:
											{
												pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CS_CHECK_JOIN_AUTHORITY_ERQ();
												break;
											}
											case 812:
											{
												pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CS_JOIN_REQUEST_REQ();
												break;
											}
											case 814:
											{
												pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CS_CANCEL_REQUEST_REQ();
												break;
											}
											case 816:
											{
												pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CS_REQUEST_CONTEXT_REQ();
												break;
											}
											case 818:
											{
												pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CS_REQUEST_LIST_REQ();
												break;
											}
											case 820:
											{
												pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CS_REQUEST_INFO_REQ();
												break;
											}
											case 822:
											{
												pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CS_ACCEPT_REQUEST_REQ();
												break;
											}
											default:
											{
												goto Label0;
											}
										}
									}
								}
								else if (ushort_2 == 825)
								{
									pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CS_DENIAL_REQUEST_REQ();
								}
								else if (ushort_2 == 828)
								{
									pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CS_SECESSION_CLAN_REQ();
								}
								else
								{
									if (ushort_2 != 830)
									{
										goto Label0;
									}
									pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CS_DEPORTATION_REQ();
								}
							}
							else if (ushort_2 <= 839)
							{
								if (ushort_2 == 833)
								{
									pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CS_COMMISSION_MASTER_REQ();
								}
								else if (ushort_2 == 836)
								{
									pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CS_COMMISSION_STAFF_REQ();
								}
								else
								{
									if (ushort_2 != 839)
									{
										goto Label0;
									}
									pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CS_COMMISSION_REGULAR_REQ();
								}
							}
							else if (ushort_2 <= 868)
							{
								switch (ushort_2)
								{
									case 854:
									{
										pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CS_CHATTING_REQ();
										break;
									}
									case 855:
									case 857:
									case 859:
									{
										goto Label0;
									}
									case 856:
									{
										pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CS_CHECK_MARK_REQ();
										break;
									}
									case 858:
									{
										pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CS_REPLACE_NOTICE_REQ();
										break;
									}
									case 860:
									{
										pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CS_REPLACE_INTRO_REQ();
										break;
									}
									default:
									{
										if (ushort_2 == 868)
										{
											pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CS_REPLACE_MANAGEMENT_REQ();
											break;
										}
										else
										{
											goto Label0;
										}
									}
								}
							}
							else if (ushort_2 == 877)
							{
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CS_ROOM_INVITED_REQ();
							}
							else
							{
								switch (ushort_2)
								{
									case 886:
									{
										pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CS_PAGE_CHATTING_REQ();
										break;
									}
									case 888:
									{
										pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CS_INVITE_REQ();
										break;
									}
									case 890:
									{
										pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CS_INVITE_ACCEPT_REQ();
										break;
									}
									case 892:
									{
										pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CS_NOTE_REQ();
										break;
									}
									default:
									{
										goto Label0;
									}
								}
							}
						}
						else if (ushort_2 <= 1049)
						{
							if (ushort_2 > 997)
							{
								if (ushort_2 == 999)
								{
									pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_REQ();
								}
								else
								{
									switch (ushort_2)
									{
										case 1025:
										{
											pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_SHOP_ENTER_REQ();
											break;
										}
										case 1026:
										case 1028:
										{
											goto Label0;
										}
										case 1027:
										{
											pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_SHOP_LEAVE_REQ();
											break;
										}
										case 1029:
										{
											pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_SHOP_GET_SAILLIST_REQ();
											break;
										}
										default:
										{
											switch (ushort_2)
											{
												case 1041:
												{
													pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_AUTH_SHOP_GET_GIFTLIST_REQ();
													break;
												}
												case 1043:
												{
													pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_AUTH_SHOP_GOODS_BUY_REQ();
													break;
												}
												case 1045:
												{
													pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_AUTH_SHOP_GOODS_GIFT_REQ();
													break;
												}
												case 1047:
												{
													pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_AUTH_SHOP_ITEM_AUTH_REQ();
													break;
												}
												case 1049:
												{
													pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_INVENTORY_USE_ITEM_REQ();
													break;
												}
												default:
												{
													goto Label0;
												}
											}
											break;
										}
									}
								}
							}
							else if (ushort_2 == 914)
							{
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CS_CREATE_CLAN_CONDITION_REQ();
							}
							else if (ushort_2 == 916)
							{
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CS_CHECK_DUPLICATE_REQ();
							}
							else
							{
								if (ushort_2 != 997)
								{
									goto Label0;
								}
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CS_CLAN_LIST_FILTER_REQ();
							}
						}
						else if (ushort_2 <= 1075)
						{
							switch (ushort_2)
							{
								case 1053:
								{
									pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_AUTH_SHOP_AUTH_GIFT_REQ();
									break;
								}
								case 1054:
								case 1056:
								{
									goto Label0;
								}
								case 1055:
								{
									pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_AUTH_SHOP_DELETE_ITEM_REQ();
									break;
								}
								case 1057:
								{
									pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_AUTH_GET_POINT_CASH_REQ();
									break;
								}
								default:
								{
									if (ushort_2 == 1061)
									{
										pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_AUTH_USE_ITEM_CHECK_NICK_REQ();
										break;
									}
									else if (ushort_2 == 1075)
									{
										pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_AUTH_SHOP_EXTEND_REQ();
										break;
									}
									else
									{
										goto Label0;
									}
								}
							}
						}
						else if (ushort_2 <= 1084)
						{
							if (ushort_2 == 1076)
							{
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_SHOP_REPAIR_REQ();
							}
							else
							{
								if (ushort_2 != 1084)
								{
									goto Label0;
								}
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_REQ();
							}
						}
						else if (ushort_2 == 1087)
						{
							pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ();
						}
						else
						{
							if (ushort_2 != 1811)
							{
								goto Label0;
							}
							pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_AUTH_FRIEND_INVITED_REQ();
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
									{
										pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_AUTH_FRIEND_ACCEPT_REQ();
										break;
									}
									case 1817:
									case 1819:
									{
										goto Label0;
									}
									case 1818:
									{
										pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_AUTH_FRIEND_INSERT_REQ();
										break;
									}
									case 1820:
									{
										pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_AUTH_FRIEND_DELETE_REQ();
										break;
									}
									default:
									{
										if (ushort_2 == 1826)
										{
											pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_AUTH_SEND_WHISPER_NOPID_REQ();
											break;
										}
										else if (ushort_2 == 1828)
										{
											pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_AUTH_SEND_WHISPER_REQ();
											break;
										}
										else
										{
											goto Label0;
										}
									}
								}
							}
							else if (ushort_2 == 1921)
							{
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_MESSENGER_NOTE_SEND_REQ();
							}
							else
							{
								switch (ushort_2)
								{
									case 1926:
									{
										pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_MESSENGER_NOTE_CHECK_READED_REQ();
										break;
									}
									case 1927:
									case 1929:
									{
										goto Label0;
									}
									case 1928:
									{
										pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_MESSENGER_NOTE_DELETE_REQ();
										break;
									}
									case 1930:
									{
										pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_MESSENGER_NOTE_RECEIVE_REQ();
										break;
									}
									default:
									{
										if (ushort_2 == 2307)
										{
											pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BASE_LOGOUT_REQ();
											break;
										}
										else
										{
											goto Label0;
										}
									}
								}
							}
						}
						else if (ushort_2 <= 2322)
						{
							if (ushort_2 == 2309)
							{
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BASE_KEEP_ALIVE_REQ();
							}
							else if (ushort_2 == 2312)
							{
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BASE_GAMEGUARD_REQ();
							}
							else
							{
								if (ushort_2 != 2322)
								{
									goto Label0;
								}
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BASE_OPTION_SAVE_REQ();
							}
						}
						else if (ushort_2 <= 2350)
						{
							switch (ushort_2)
							{
								case 2326:
								{
									pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BASE_CREATE_NICK_REQ();
									break;
								}
								case 2327:
								case 2329:
								case 2331:
								case 2333:
								case 2335:
								case 2337:
								{
									goto Label0;
								}
								case 2328:
								{
									pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BASE_USER_LEAVE_REQ();
									break;
								}
								case 2330:
								{
									pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BASE_USER_ENTER_REQ();
									break;
								}
								case 2332:
								{
									pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BASE_GET_CHANNELLIST_REQ();
									break;
								}
								case 2334:
								{
									pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BASE_SELECT_CHANNEL_REQ();
									break;
								}
								case 2336:
								{
									pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BASE_ATTENDANCE_REQ();
									break;
								}
								case 2338:
								{
									pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_REQ();
									break;
								}
								default:
								{
									if (ushort_2 == 2350)
									{
										pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BASE_GET_RECORD_INFO_DB_REQ();
										break;
									}
									else
									{
										goto Label0;
									}
								}
							}
						}
						else if (ushort_2 == 2360)
						{
							pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_REQ();
						}
						else
						{
							if (ushort_2 != 2364)
							{
								goto Label0;
							}
							pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BASE_QUEST_BUY_CARD_SET_REQ();
						}
					}
					else if (ushort_2 <= 2414)
					{
						if (ushort_2 <= 2384)
						{
							if (ushort_2 == 2366)
							{
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BASE_QUEST_DELETE_CARD_SET_REQ();
							}
							else
							{
								switch (ushort_2)
								{
									case 2376:
									{
										pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BASE_USER_TITLE_CHANGE_REQ();
										break;
									}
									case 2377:
									case 2379:
									{
										goto Label0;
									}
									case 2378:
									{
										pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BASE_USER_TITLE_EQUIP_REQ();
										break;
									}
									case 2380:
									{
										pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BASE_USER_TITLE_RELEASE_REQ();
										break;
									}
									default:
									{
										if (ushort_2 == 2384)
										{
											pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BASE_CHATTING_REQ();
											break;
										}
										else
										{
											goto Label0;
										}
									}
								}
							}
						}
						else if (ushort_2 <= 2399)
						{
							if (ushort_2 != 2392)
							{
								if (ushort_2 != 2399)
								{
									goto Label0;
								}
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BASE_GAME_SERVER_STATE_REQ();
							}
						}
						else if (ushort_2 == 2401)
						{
							pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BASE_ENTER_PASS_REQ();
						}
						else
						{
							if (ushort_2 != 2414)
							{
								goto Label0;
							}
							pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BASE_DAILY_RECORD_REQ();
						}
					}
					else if (ushort_2 <= 2465)
					{
						switch (ushort_2)
						{
							case 2422:
							{
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BASE_GET_USER_DETAIL_INFO_REQ();
								break;
							}
							case 2423:
							{
								goto Label0;
							}
							case 2424:
							{
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BASE_GET_ROOM_USER_DETAIL_INFO_REQ();
								break;
							}
							case 2425:
							{
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BASE_GET_USER_BASIC_INFO_REQ();
								break;
							}
							case 2426:
							{
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_AUTH_FIND_USER_REQ();
								break;
							}
							default:
							{
								if (ushort_2 == 2447)
								{
									pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BASE_GET_USER_SUBTASK_REQ();
									break;
								}
								else if (ushort_2 == 2465)
								{
									pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BASE_URL_LIST_REQ();
									break;
								}
								else
								{
									goto Label0;
								}
							}
						}
					}
					else if (ushort_2 <= 2491)
					{
						if (ushort_2 == 2489)
						{
							pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_REQ();
						}
						else
						{
							if (ushort_2 != 2491)
							{
								goto Label0;
							}
							pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_LOBBY_NEW_MYINFO_REQ();
						}
					}
					else if (ushort_2 == 2498)
					{
						pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BASE_RANDOMBOX_LIST_REQ();
					}
					else
					{
						if (ushort_2 != 2508)
						{
							goto Label0;
						}
						pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BASE_TICKET_UPDATE_REQ();
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
									pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BASE_EVENT_PORTAL_REQ();
								}
								else
								{
									if (ushort_2 == 2518 || ushort_2 == 2519)
									{
										goto Label17;
									}
									goto Label0;
								}
							}
							else if (ushort_2 == 2561)
							{
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_LOBBY_LEAVE_REQ();
							}
							else if (ushort_2 == 2567)
							{
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_LOBBY_GET_ROOMINFOADD_REQ();
							}
							else
							{
								if (ushort_2 != 2583)
								{
									goto Label0;
								}
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_LOBBY_ENTER_REQ();
							}
						}
						else if (ushort_2 <= 3329)
						{
							if (ushort_2 == 2587)
							{
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_LOBBY_GET_ROOMLIST_REQ();
							}
							else if (ushort_2 == 3083)
							{
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_LOBBY_GET_ROOMINFOADD_REQ();
							}
							else
							{
								if (ushort_2 != 3329)
								{
									goto Label0;
								}
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_INVENTORY_ENTER_REQ();
							}
						}
						else if (ushort_2 <= 3585)
						{
							if (ushort_2 == 3331)
							{
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_INVENTORY_LEAVE_REQ();
							}
							else
							{
								if (ushort_2 != 3585)
								{
									goto Label0;
								}
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_ROOM_JOIN_REQ();
							}
						}
						else if (ushort_2 == 3592)
						{
							pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_ROOM_CREATE_REQ();
						}
						else
						{
							if (ushort_2 != 3596)
							{
								goto Label0;
							}
							pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_ROOM_GET_PLAYERINFO_REQ();
						}
					}
					else if (ushort_2 <= 3643)
					{
						if (ushort_2 <= 3619)
						{
							if (ushort_2 == 3602)
							{
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_ROOM_CHANGE_PASSWD_REQ();
							}
							else if (ushort_2 == 3604)
							{
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_ROOM_CHANGE_SLOT_REQ();
							}
							else
							{
								switch (ushort_2)
								{
									case 3609:
									{
										pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_ROOM_PERSONAL_TEAM_CHANGE_REQ();
										break;
									}
									case 3611:
									{
										pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_ROOM_REQUEST_MAIN_REQ();
										break;
									}
									case 3613:
									{
										pROTOCOLCSCLIENTENTERREQ = new Class3();
										break;
									}
									case 3615:
									{
										pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_REQ();
										break;
									}
									case 3617:
									{
										pROTOCOLCSCLIENTENTERREQ = new Class2();
										break;
									}
									case 3619:
									{
										pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_ROOM_TOTAL_TEAM_CHANGE_REQ();
										break;
									}
									default:
									{
										goto Label0;
									}
								}
							}
						}
						else if (ushort_2 <= 3635)
						{
							if (ushort_2 == 3631)
							{
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_ROOM_GET_LOBBY_USER_LIST_REQ();
							}
							else
							{
								if (ushort_2 != 3635)
								{
									goto Label0;
								}
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BASE_UNKNOWN_3635_REQ();
							}
						}
						else if (ushort_2 == 3639)
						{
							pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ();
						}
						else
						{
							if (ushort_2 != 3643)
							{
								goto Label0;
							}
							pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_GM_KICK_COMMAND_REQ();
						}
					}
					else if (ushort_2 <= 3665)
					{
						if (ushort_2 == 3647)
						{
							pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_GM_EXIT_COMMAND_REQ();
						}
						else if (ushort_2 == 3657)
						{
							pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_ROOM_LOADING_START_REQ();
						}
						else
						{
							if (ushort_2 != 3665)
							{
								goto Label0;
							}
							pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_ROOM_GET_USER_EQUIPMENT_REQ();
						}
					}
					else if (ushort_2 <= 3686)
					{
						switch (ushort_2)
						{
							case 3671:
							{
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_ROOM_INFO_ENTER_REQ();
								break;
							}
							case 3672:
							case 3674:
							case 3676:
							case 3678:
							case 3681:
							{
								goto Label0;
							}
							case 3673:
							{
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_ROOM_INFO_LEAVE_REQ();
								break;
							}
							case 3675:
							{
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_REQ();
								break;
							}
							case 3677:
							{
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_ROOM_CHANGE_COSTUME_REQ();
								break;
							}
							case 3679:
							{
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_ROOM_SELECT_SLOT_CHANGE_REQ();
								break;
							}
							case 3680:
							{
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_REQ();
								break;
							}
							case 3682:
							{
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_ROOM_SELECT_SLOT_CHANGE_REQ();
								break;
							}
							default:
							{
								if (ushort_2 == 3686)
								{
									pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_ROOM_CHANGE_OBSERVER_SLOT_REQ();
									break;
								}
								else
								{
									goto Label0;
								}
							}
						}
					}
					else if (ushort_2 == 3850)
					{
						pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_COMMUNITY_USER_REPORT_REQ();
					}
					else
					{
						if (ushort_2 != 3852)
						{
							goto Label0;
						}
						pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_COMMUNITY_USER_REPORT_CONDITION_CHECK_REQ();
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
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BATTLE_READYBATTLE_REQ();
							}
							else
							{
								switch (ushort_2)
								{
									case 5129:
									{
										pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BATTLE_PRESTARTBATTLE_REQ();
										break;
									}
									case 5130:
									case 5132:
									case 5134:
									case 5136:
									{
										goto Label0;
									}
									case 5131:
									{
										pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BATTLE_STARTBATTLE_REQ();
										break;
									}
									case 5133:
									{
										pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BATTLE_GIVEUPBATTLE_REQ();
										break;
									}
									case 5135:
									{
										pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BATTLE_DEATH_REQ();
										break;
									}
									case 5137:
									{
										pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BATTLE_RESPAWN_REQ();
										break;
									}
									default:
									{
										if (ushort_2 == 5145)
										{
											break;
										}
										goto Label0;
									}
								}
							}
						}
						else if (ushort_2 == 5146)
						{
							pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BATTLE_SENDPING_REQ();
						}
						else if (ushort_2 == 5156)
						{
							pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_REQ();
						}
						else
						{
							if (ushort_2 != 5158)
							{
								goto Label0;
							}
							pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_REQ();
						}
					}
					else if (ushort_2 <= 5172)
					{
						if (ushort_2 == 5166)
						{
							pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_REQ();
						}
						else if (ushort_2 == 5168)
						{
							pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BATTLE_TIMERSYNC_REQ();
						}
						else
						{
							if (ushort_2 != 5172)
							{
								goto Label0;
							}
							pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_REQ();
						}
					}
					else if (ushort_2 <= 5180)
					{
						if (ushort_2 == 5174)
						{
							pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BATTLE_RESPAWN_FOR_AI_REQ();
						}
						else
						{
							if (ushort_2 != 5180)
							{
								goto Label0;
							}
							pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_REQ();
						}
					}
					else if (ushort_2 == 5182)
					{
						pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_REQ();
					}
					else
					{
						if (ushort_2 != 5188)
						{
							goto Label0;
						}
						pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BATTLE_MISSION_TUTORIAL_ROUND_END_REQ();
					}
				}
				else if (ushort_2 <= 6151)
				{
					if (ushort_2 <= 5292)
					{
						if (ushort_2 == 5262)
						{
							pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_REQ();
						}
						else if (ushort_2 == 5276)
						{
							pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BATTLE_USER_SOPETYPE_REQ();
						}
						else
						{
							if (ushort_2 != 5292)
							{
								goto Label0;
							}
							pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BASE_UNKNOWN_PACKET_REQ();
						}
					}
					else if (ushort_2 <= 6145)
					{
						if (ushort_2 == 5377)
						{
							pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ();
						}
						else
						{
							if (ushort_2 != 6145)
							{
								goto Label0;
							}
							pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CHAR_CREATE_CHARA_REQ();
						}
					}
					else if (ushort_2 == 6149)
					{
						pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CHAR_CHANGE_EQUIP_REQ();
					}
					else
					{
						if (ushort_2 != 6151)
						{
							goto Label0;
						}
						pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CHAR_DELETE_CHARA_REQ();
					}
				}
				else if (ushort_2 <= 7429)
				{
					switch (ushort_2)
					{
						case 6657:
						{
							pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_GMCHAT_START_CHAT_REQ();
							break;
						}
						case 6658:
						case 6660:
						case 6662:
						case 6664:
						{
							goto Label0;
						}
						case 6659:
						{
							pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_GMCHAT_SEND_CHAT_REQ();
							break;
						}
						case 6661:
						{
							pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_GMCHAT_END_CHAT_REQ();
							break;
						}
						case 6663:
						{
							pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_GMCHAT_APPLY_PENALTY_REQ();
							break;
						}
						case 6665:
						{
							pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_GMCHAT_NOTI_USER_PENALTY_REQ();
							break;
						}
						default:
						{
							if (ushort_2 == 6965)
							{
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_CLAN_WAR_RESULT_REQ();
								break;
							}
							else if (ushort_2 == 7429)
							{
								pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_BATTLEBOX_AUTH_REQ();
								break;
							}
							else
							{
								goto Label0;
							}
						}
					}
				}
				else if (ushort_2 <= 7699)
				{
					if (ushort_2 == 7681)
					{
						pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_MATCH_SERVER_IDX_REQ();
					}
					else
					{
						if (ushort_2 != 7699)
						{
							goto Label0;
						}
						pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_MATCH_CLAN_SEASON_REQ();
					}
				}
				else if (ushort_2 == 8449)
				{
					pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_SEASON_CHALLENGE_INFO_REQ();
				}
				else
				{
					if (ushort_2 != 8453)
					{
						goto Label0;
					}
					pROTOCOLCSCLIENTENTERREQ = new PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_REQ();
				}
			Label17:
				if (pROTOCOLCSCLIENTENTERREQ != null)
				{
					using (pROTOCOLCSCLIENTENTERREQ)
					{
						if (ConfigLoader.DebugMode)
						{
							CLogger.Print(string.Format("{0}; Address: {1}; Opcode: [{2}]", pROTOCOLCSCLIENTENTERREQ.GetType().Name, this.Client.RemoteEndPoint, ushort_2), LoggerType.Debug, null);
						}
						pROTOCOLCSCLIENTENTERREQ.Makeme(this, byte_0);
						ThreadPool.QueueUserWorkItem((object object_0) => {
							try
							{
								pROTOCOLCSCLIENTENTERREQ.Run();
							}
							catch (Exception exception1)
							{
								Exception exception = exception1;
								CLogger.Print(exception.Message, LoggerType.Error, exception);
								this.Close(50, true, false);
							}
						});
						pROTOCOLCSCLIENTENTERREQ.Dispose();
					}
				}
			}
			catch (Exception exception3)
			{
				Exception exception2 = exception3;
				CLogger.Print(exception2.Message, LoggerType.Error, exception2);
			}
			return;
		Label0:
			CLogger.Print(Bitwise.ToHexData(string.Format("Opcode Not Found: [{0}] | {1}", ushort_2, string_0), byte_0), LoggerType.Opcode, null);
			goto Label17;
		}

		public void SendCompletePacket(byte[] OriginalData, string PacketName)
		{
			try
			{
				byte[] bytes = BitConverter.GetBytes((ushort)((int)OriginalData.Length + 2));
				byte[] numArray = new byte[(int)OriginalData.Length + (int)bytes.Length];
				Array.Copy(bytes, 0, numArray, 0, (int)bytes.Length);
				Array.Copy(OriginalData, 0, numArray, 2, (int)OriginalData.Length);
				byte[] numArray1 = new byte[(int)numArray.Length + 5];
				Array.Copy(numArray, 0, numArray1, 0, (int)numArray.Length);
				if (ConfigLoader.DebugMode)
				{
					ushort uInt16 = BitConverter.ToUInt16(numArray1, 2);
					Bitwise.ToByteString(numArray1);
					CLogger.Print(string.Format("{0}; Address: {1}; Opcode: [{2}]", PacketName, this.Client.RemoteEndPoint, uInt16), LoggerType.Debug, null);
				}
				Bitwise.ProcessPacket(numArray1, 2, 5, this.ushort_1);
				if (this.Client != null && numArray1.Length != 0)
				{
					this.Client.BeginSend(numArray1, 0, (int)numArray1.Length, SocketFlags.None, new AsyncCallback(this.method_2), this.Client);
				}
				numArray1 = null;
			}
			catch
			{
				this.Close(0, true, false);
			}
		}

		public void SendPacket(byte[] OriginalData, string PacketName)
		{
			try
			{
				if ((int)OriginalData.Length >= 2)
				{
					this.SendCompletePacket(OriginalData, PacketName);
				}
			}
			catch
			{
				this.Close(0, true, false);
			}
		}

		public void SendPacket(GameServerPacket Packet)
		{
			try
			{
				using (Packet)
				{
					byte[] bytes = Packet.GetBytes("GameClient.SendPacket");
					this.SendPacket(bytes, Packet.GetType().Name);
					Packet.Dispose();
				}
			}
			catch
			{
				this.Close(0, true, false);
			}
		}

		public void StartSession()
		{
			try
			{
				this.ushort_0 = this.SessionSeed;
				this.SessionShift = (this.SessionId + Bitwise.CRYPTO[0]) % 7 + 1;
				ThreadPool.QueueUserWorkItem((object object_0) => this.method_1());
				ThreadPool.QueueUserWorkItem((object object_0) => this.method_3());
				ThreadPool.QueueUserWorkItem((object object_0) => this.method_0());
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				this.Close(0, true, false);
			}
		}
	}
}