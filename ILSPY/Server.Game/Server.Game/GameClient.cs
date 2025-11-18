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

namespace Server.Game;

public class GameClient : IDisposable
{
	[CompilerGenerated]
	private sealed class Class1
	{
		public GameClient gameClient_0;

		public GameClientPacket gameClientPacket_0;

		internal void method_0(object object_0)
		{
			try
			{
				gameClientPacket_0.Run();
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				gameClient_0.Close(50, DestroyConnection: true);
			}
		}
	}

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

	private readonly SafeHandle safeHandle_0 = new SafeFileHandle(IntPtr.Zero, ownsHandle: true);

	private readonly ushort[] ushort_1 = new ushort[3] { 1030, 1099, 2499 };

	public GameClient(int int_0, Socket socket_0)
	{
		ServerId = int_0;
		Client = socket_0;
	}

	public void Dispose()
	{
		try
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	protected virtual void Dispose(bool disposing)
	{
		try
		{
			if (!bool_0)
			{
				Player = null;
				if (Client != null)
				{
					Client.Dispose();
					Client = null;
				}
				PlayerId = 0L;
				if (disposing)
				{
					safeHandle_0.Dispose();
				}
				bool_0 = true;
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	public void StartSession()
	{
		try
		{
			ushort_0 = SessionSeed;
			SessionShift = (SessionId + Bitwise.CRYPTO[0]) % 7 + 1;
			ThreadPool.QueueUserWorkItem(delegate
			{
				method_1();
			});
			ThreadPool.QueueUserWorkItem(delegate
			{
				method_3();
			});
			ThreadPool.QueueUserWorkItem(delegate
			{
				method_0();
			});
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			Close(0, DestroyConnection: true);
		}
	}

	private void method_0()
	{
		Thread.Sleep(10000);
		if (Client != null && FirstPacketId == 0)
		{
			CLogger.Print("Connection destroyed due to no responses. IPAddress: " + GetIPAddress(), LoggerType.Hack);
			Close(0, DestroyConnection: true);
		}
	}

	public string GetIPAddress()
	{
		try
		{
			if (Client != null && Client.RemoteEndPoint != null)
			{
				return (Client.RemoteEndPoint as IPEndPoint).Address.ToString();
			}
			return "";
		}
		catch
		{
			return "";
		}
	}

	public IPAddress GetAddress()
	{
		try
		{
			if (Client != null && Client.RemoteEndPoint != null)
			{
				return (Client.RemoteEndPoint as IPEndPoint).Address;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	private void method_1()
	{
		SendPacket(new PROTOCOL_BASE_CONNECT_ACK(this));
	}

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
				CLogger.Print($"{PacketName}; Address: {Client.RemoteEndPoint}; Opcode: [{num}]", LoggerType.Debug);
			}
			Bitwise.ProcessPacket(array2, 2, 5, ushort_1);
			if (Client != null && array2.Length != 0)
			{
				Client.BeginSend(array2, 0, array2.Length, SocketFlags.None, method_2, Client);
			}
			array2 = null;
		}
		catch
		{
			Close(0, DestroyConnection: true);
		}
	}

	public void SendPacket(byte[] OriginalData, string PacketName)
	{
		try
		{
			if (OriginalData.Length >= 2)
			{
				SendCompletePacket(OriginalData, PacketName);
			}
		}
		catch
		{
			Close(0, DestroyConnection: true);
		}
	}

	public void SendPacket(GameServerPacket Packet)
	{
		try
		{
			using (Packet)
			{
				byte[] bytes = Packet.GetBytes("GameClient.SendPacket");
				SendPacket(bytes, Packet.GetType().Name);
				Packet.Dispose();
			}
		}
		catch
		{
			Close(0, DestroyConnection: true);
		}
	}

	private void method_2(IAsyncResult iasyncResult_0)
	{
		try
		{
			if (iasyncResult_0.AsyncState is Socket socket && socket.Connected)
			{
				socket.EndSend(iasyncResult_0);
			}
		}
		catch
		{
			Close(0, DestroyConnection: true);
		}
	}

	private void method_3()
	{
		try
		{
			StateObject stateObject = new StateObject
			{
				WorkSocket = Client,
				RemoteEP = new IPEndPoint(IPAddress.Any, 0)
			};
			Client.BeginReceive(stateObject.TcpBuffer, 0, 8912, SocketFlags.None, method_4, stateObject);
		}
		catch
		{
			Close(0, DestroyConnection: true);
		}
	}

	private void method_4(IAsyncResult iasyncResult_0)
	{
		StateObject stateObject = iasyncResult_0.AsyncState as StateObject;
		try
		{
			int num = stateObject.WorkSocket.EndReceive(iasyncResult_0);
			if (num <= 0)
			{
				return;
			}
			if (num > 8912)
			{
				CLogger.Print("Received data exceeds buffer size. IP: " + GetIPAddress(), LoggerType.Error);
				Close(0, DestroyConnection: true);
				return;
			}
			byte[] array = new byte[num];
			Array.Copy(stateObject.TcpBuffer, 0, array, 0, num);
			int num2 = BitConverter.ToUInt16(array, 0) & 0x7FFF;
			if (num2 > 0 && num2 <= num - 2)
			{
				byte[] array2 = new byte[num2];
				Array.Copy(array, 2, array2, 0, array2.Length);
				byte[] array3 = Bitwise.Decrypt(array2, SessionShift);
				ushort ushort_ = BitConverter.ToUInt16(array3, 0);
				ushort packetSeed = BitConverter.ToUInt16(array3, 2);
				method_5(ushort_);
				if (!CheckSeed(packetSeed, IsTheFirstPacket: true))
				{
					Close(0, DestroyConnection: true);
				}
				else
				{
					if (bool_1)
					{
						return;
					}
					method_7(ushort_, array3, "REQ");
					CheckOut(array, num2);
					ThreadPool.QueueUserWorkItem(delegate
					{
						try
						{
							method_3();
						}
						catch (Exception ex)
						{
							CLogger.Print("Error processing packet in thread pool for IP: " + GetIPAddress() + ": " + ex.Message, LoggerType.Error, ex);
							Close(0, DestroyConnection: true);
						}
					});
				}
			}
			else
			{
				CLogger.Print($"Invalid PacketLength. IP: {GetIPAddress()}; Length: {num2}; RawBytes: {num}", LoggerType.Hack);
				Close(0, DestroyConnection: true);
			}
		}
		catch
		{
			Close(0, DestroyConnection: true);
		}
	}

	public void CheckOut(byte[] BufferTotal, int FirstLength)
	{
		int num = BufferTotal.Length;
		try
		{
			if (num <= FirstLength + 3)
			{
				return;
			}
			byte[] array = new byte[num - FirstLength - 3];
			Array.Copy(BufferTotal, FirstLength + 3, array, 0, array.Length);
			if (array.Length == 0)
			{
				return;
			}
			int num2 = BitConverter.ToUInt16(array, 0) & 0x7FFF;
			if (num2 > 0 && num2 <= array.Length - 2)
			{
				byte[] array2 = new byte[num2];
				Array.Copy(array, 2, array2, 0, array2.Length);
				byte[] array3 = Bitwise.Decrypt(array2, SessionShift);
				ushort ushort_ = BitConverter.ToUInt16(array3, 0);
				ushort packetSeed = BitConverter.ToUInt16(array3, 2);
				if (!CheckSeed(packetSeed, IsTheFirstPacket: false))
				{
					Close(0, DestroyConnection: true);
					return;
				}
				method_7(ushort_, array3, "REQ");
				CheckOut(array, num2);
			}
			else
			{
				CLogger.Print($"Invalid PacketLength in CheckOut. IP: {GetIPAddress()}; Length: {num2}; RawBytes: {array.Length}", LoggerType.Hack);
				Close(0, DestroyConnection: true);
			}
		}
		catch
		{
			Close(0, DestroyConnection: true);
		}
	}

	public void Close(int TimeMS, bool DestroyConnection, bool Kicked = false)
	{
		if (bool_1)
		{
			return;
		}
		try
		{
			bool_1 = true;
			GameXender.Client.RemoveSession(this);
			Account player = Player;
			if (DestroyConnection)
			{
				if (PlayerId > 0L && player != null)
				{
					player.SetOnlineStatus(Online: false);
					player.Room?.RemovePlayer(player, WarnAllPlayers: false, Kicked ? 1 : 0);
					player.Match?.RemovePlayer(player);
					player.GetChannel()?.RemovePlayer(player);
					player.Status.ResetData(PlayerId);
					AllUtils.SyncPlayerToFriends(player, all: false);
					AllUtils.SyncPlayerToClanMembers(player);
					player.SimpleClear();
					player.UpdateCacheInfo();
					Player = null;
				}
				PlayerId = 0L;
				if (Client != null)
				{
					Client.Close(TimeMS);
				}
				Thread.Sleep(TimeMS);
				Dispose();
			}
			else if (player != null)
			{
				player.SimpleClear();
				player.UpdateCacheInfo();
				Player = null;
			}
			UpdateServer.RefreshSChannel(ServerId);
		}
		catch (Exception ex)
		{
			CLogger.Print("GameClient.Close: " + ex.Message, LoggerType.Error, ex);
		}
	}

	private void method_5(ushort ushort_2)
	{
		if (FirstPacketId == 0)
		{
			FirstPacketId = ushort_2;
			if (ushort_2 != 2330 && ushort_2 != 2309)
			{
				CLogger.Print($"Connection destroyed due to unknown first packet. Opcode: {ushort_2}; IPAddress: {GetIPAddress()}", LoggerType.Hack);
				Close(0, DestroyConnection: true);
			}
		}
	}

	public bool CheckSeed(ushort PacketSeed, bool IsTheFirstPacket)
	{
		if (PacketSeed == method_6())
		{
			return true;
		}
		CLogger.Print($"Connection blocked. IP: {GetIPAddress()}; Date: {DateTimeUtil.Now()}; SessionId: {SessionId}; PacketSeed: {PacketSeed} / NextSessionSeed: {ushort_0}; PrimarySeed: {SessionSeed}", LoggerType.Hack);
		if (IsTheFirstPacket)
		{
			ThreadPool.QueueUserWorkItem(delegate
			{
				method_3();
			});
		}
		return false;
	}

	private ushort method_6()
	{
		ushort_0 = (ushort)((uint)(ushort_0 * 214013 + 2531011 >> 16) & 0x7FFFu);
		return ushort_0;
	}

	private void method_7(ushort ushort_2, byte[] byte_0, string string_0)
	{
		try
		{
			GameClientPacket gameClientPacket_0 = null;
			switch (ushort_2)
			{
			case 800:
				gameClientPacket_0 = new PROTOCOL_CS_DETAIL_INFO_REQ();
				break;
			case 802:
				gameClientPacket_0 = new PROTOCOL_CS_MEMBER_CONTEXT_REQ();
				break;
			case 804:
				gameClientPacket_0 = new PROTOCOL_CS_MEMBER_LIST_REQ();
				break;
			case 806:
				gameClientPacket_0 = new PROTOCOL_CS_CREATE_CLAN_REQ();
				break;
			case 808:
				gameClientPacket_0 = new PROTOCOL_CS_CLOSE_CLAN_REQ();
				break;
			case 810:
				gameClientPacket_0 = new PROTOCOL_CS_CHECK_JOIN_AUTHORITY_ERQ();
				break;
			case 812:
				gameClientPacket_0 = new PROTOCOL_CS_JOIN_REQUEST_REQ();
				break;
			case 814:
				gameClientPacket_0 = new PROTOCOL_CS_CANCEL_REQUEST_REQ();
				break;
			case 816:
				gameClientPacket_0 = new PROTOCOL_CS_REQUEST_CONTEXT_REQ();
				break;
			case 818:
				gameClientPacket_0 = new PROTOCOL_CS_REQUEST_LIST_REQ();
				break;
			case 820:
				gameClientPacket_0 = new PROTOCOL_CS_REQUEST_INFO_REQ();
				break;
			case 822:
				gameClientPacket_0 = new PROTOCOL_CS_ACCEPT_REQUEST_REQ();
				break;
			case 771:
				gameClientPacket_0 = new PROTOCOL_CS_CLIENT_LEAVE_REQ();
				break;
			case 769:
				gameClientPacket_0 = new PROTOCOL_CS_CLIENT_ENTER_REQ();
				break;
			case 830:
				gameClientPacket_0 = new PROTOCOL_CS_DEPORTATION_REQ();
				break;
			case 828:
				gameClientPacket_0 = new PROTOCOL_CS_SECESSION_CLAN_REQ();
				break;
			case 825:
				gameClientPacket_0 = new PROTOCOL_CS_DENIAL_REQUEST_REQ();
				break;
			case 839:
				gameClientPacket_0 = new PROTOCOL_CS_COMMISSION_REGULAR_REQ();
				break;
			case 836:
				gameClientPacket_0 = new PROTOCOL_CS_COMMISSION_STAFF_REQ();
				break;
			case 833:
				gameClientPacket_0 = new PROTOCOL_CS_COMMISSION_MASTER_REQ();
				break;
			case 868:
				gameClientPacket_0 = new PROTOCOL_CS_REPLACE_MANAGEMENT_REQ();
				break;
			case 854:
				gameClientPacket_0 = new PROTOCOL_CS_CHATTING_REQ();
				break;
			case 856:
				gameClientPacket_0 = new PROTOCOL_CS_CHECK_MARK_REQ();
				break;
			case 858:
				gameClientPacket_0 = new PROTOCOL_CS_REPLACE_NOTICE_REQ();
				break;
			case 860:
				gameClientPacket_0 = new PROTOCOL_CS_REPLACE_INTRO_REQ();
				break;
			case 886:
				gameClientPacket_0 = new PROTOCOL_CS_PAGE_CHATTING_REQ();
				break;
			case 888:
				gameClientPacket_0 = new PROTOCOL_CS_INVITE_REQ();
				break;
			case 890:
				gameClientPacket_0 = new PROTOCOL_CS_INVITE_ACCEPT_REQ();
				break;
			case 892:
				gameClientPacket_0 = new PROTOCOL_CS_NOTE_REQ();
				break;
			case 877:
				gameClientPacket_0 = new PROTOCOL_CS_ROOM_INVITED_REQ();
				break;
			case 997:
				gameClientPacket_0 = new PROTOCOL_CS_CLAN_LIST_FILTER_REQ();
				break;
			case 916:
				gameClientPacket_0 = new PROTOCOL_CS_CHECK_DUPLICATE_REQ();
				break;
			case 914:
				gameClientPacket_0 = new PROTOCOL_CS_CREATE_CLAN_CONDITION_REQ();
				break;
			case 1041:
				gameClientPacket_0 = new PROTOCOL_AUTH_SHOP_GET_GIFTLIST_REQ();
				break;
			case 1043:
				gameClientPacket_0 = new PROTOCOL_AUTH_SHOP_GOODS_BUY_REQ();
				break;
			case 1045:
				gameClientPacket_0 = new PROTOCOL_AUTH_SHOP_GOODS_GIFT_REQ();
				break;
			case 1047:
				gameClientPacket_0 = new PROTOCOL_AUTH_SHOP_ITEM_AUTH_REQ();
				break;
			case 1049:
				gameClientPacket_0 = new PROTOCOL_INVENTORY_USE_ITEM_REQ();
				break;
			case 1025:
				gameClientPacket_0 = new PROTOCOL_SHOP_ENTER_REQ();
				break;
			case 1027:
				gameClientPacket_0 = new PROTOCOL_SHOP_LEAVE_REQ();
				break;
			case 1029:
				gameClientPacket_0 = new PROTOCOL_SHOP_GET_SAILLIST_REQ();
				break;
			case 999:
				gameClientPacket_0 = new PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_REQ();
				break;
			case 1075:
				gameClientPacket_0 = new PROTOCOL_AUTH_SHOP_EXTEND_REQ();
				break;
			case 1061:
				gameClientPacket_0 = new PROTOCOL_AUTH_USE_ITEM_CHECK_NICK_REQ();
				break;
			case 1053:
				gameClientPacket_0 = new PROTOCOL_AUTH_SHOP_AUTH_GIFT_REQ();
				break;
			case 1055:
				gameClientPacket_0 = new PROTOCOL_AUTH_SHOP_DELETE_ITEM_REQ();
				break;
			case 1057:
				gameClientPacket_0 = new PROTOCOL_AUTH_GET_POINT_CASH_REQ();
				break;
			case 1084:
				gameClientPacket_0 = new PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_REQ();
				break;
			case 1076:
				gameClientPacket_0 = new PROTOCOL_SHOP_REPAIR_REQ();
				break;
			case 1811:
				gameClientPacket_0 = new PROTOCOL_AUTH_FRIEND_INVITED_REQ();
				break;
			case 1087:
				gameClientPacket_0 = new PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ();
				break;
			case 1828:
				gameClientPacket_0 = new PROTOCOL_AUTH_SEND_WHISPER_REQ();
				break;
			case 1826:
				gameClientPacket_0 = new PROTOCOL_AUTH_SEND_WHISPER_NOPID_REQ();
				break;
			case 1816:
				gameClientPacket_0 = new PROTOCOL_AUTH_FRIEND_ACCEPT_REQ();
				break;
			case 1818:
				gameClientPacket_0 = new PROTOCOL_AUTH_FRIEND_INSERT_REQ();
				break;
			case 1820:
				gameClientPacket_0 = new PROTOCOL_AUTH_FRIEND_DELETE_REQ();
				break;
			case 2307:
				gameClientPacket_0 = new PROTOCOL_BASE_LOGOUT_REQ();
				break;
			case 1926:
				gameClientPacket_0 = new PROTOCOL_MESSENGER_NOTE_CHECK_READED_REQ();
				break;
			case 1928:
				gameClientPacket_0 = new PROTOCOL_MESSENGER_NOTE_DELETE_REQ();
				break;
			case 1930:
				gameClientPacket_0 = new PROTOCOL_MESSENGER_NOTE_RECEIVE_REQ();
				break;
			case 1921:
				gameClientPacket_0 = new PROTOCOL_MESSENGER_NOTE_SEND_REQ();
				break;
			case 2322:
				gameClientPacket_0 = new PROTOCOL_BASE_OPTION_SAVE_REQ();
				break;
			case 2312:
				gameClientPacket_0 = new PROTOCOL_BASE_GAMEGUARD_REQ();
				break;
			case 2309:
				gameClientPacket_0 = new PROTOCOL_BASE_KEEP_ALIVE_REQ();
				break;
			case 2350:
				gameClientPacket_0 = new PROTOCOL_BASE_GET_RECORD_INFO_DB_REQ();
				break;
			case 2326:
				gameClientPacket_0 = new PROTOCOL_BASE_CREATE_NICK_REQ();
				break;
			case 2328:
				gameClientPacket_0 = new PROTOCOL_BASE_USER_LEAVE_REQ();
				break;
			case 2330:
				gameClientPacket_0 = new PROTOCOL_BASE_USER_ENTER_REQ();
				break;
			case 2332:
				gameClientPacket_0 = new PROTOCOL_BASE_GET_CHANNELLIST_REQ();
				break;
			case 2334:
				gameClientPacket_0 = new PROTOCOL_BASE_SELECT_CHANNEL_REQ();
				break;
			case 2336:
				gameClientPacket_0 = new PROTOCOL_BASE_ATTENDANCE_REQ();
				break;
			case 2338:
				gameClientPacket_0 = new PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_REQ();
				break;
			case 2364:
				gameClientPacket_0 = new PROTOCOL_BASE_QUEST_BUY_CARD_SET_REQ();
				break;
			case 2360:
				gameClientPacket_0 = new PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_REQ();
				break;
			case 2384:
				gameClientPacket_0 = new PROTOCOL_BASE_CHATTING_REQ();
				break;
			case 2376:
				gameClientPacket_0 = new PROTOCOL_BASE_USER_TITLE_CHANGE_REQ();
				break;
			case 2378:
				gameClientPacket_0 = new PROTOCOL_BASE_USER_TITLE_EQUIP_REQ();
				break;
			case 2380:
				gameClientPacket_0 = new PROTOCOL_BASE_USER_TITLE_RELEASE_REQ();
				break;
			case 2366:
				gameClientPacket_0 = new PROTOCOL_BASE_QUEST_DELETE_CARD_SET_REQ();
				break;
			case 2399:
				gameClientPacket_0 = new PROTOCOL_BASE_GAME_SERVER_STATE_REQ();
				break;
			case 2414:
				gameClientPacket_0 = new PROTOCOL_BASE_DAILY_RECORD_REQ();
				break;
			case 2401:
				gameClientPacket_0 = new PROTOCOL_BASE_ENTER_PASS_REQ();
				break;
			case 2465:
				gameClientPacket_0 = new PROTOCOL_BASE_URL_LIST_REQ();
				break;
			case 2447:
				gameClientPacket_0 = new PROTOCOL_BASE_GET_USER_SUBTASK_REQ();
				break;
			case 2422:
				gameClientPacket_0 = new PROTOCOL_BASE_GET_USER_DETAIL_INFO_REQ();
				break;
			case 2424:
				gameClientPacket_0 = new PROTOCOL_BASE_GET_ROOM_USER_DETAIL_INFO_REQ();
				break;
			case 2425:
				gameClientPacket_0 = new PROTOCOL_BASE_GET_USER_BASIC_INFO_REQ();
				break;
			case 2426:
				gameClientPacket_0 = new PROTOCOL_AUTH_FIND_USER_REQ();
				break;
			case 2491:
				gameClientPacket_0 = new PROTOCOL_LOBBY_NEW_MYINFO_REQ();
				break;
			case 2489:
				gameClientPacket_0 = new PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_REQ();
				break;
			case 2508:
				gameClientPacket_0 = new PROTOCOL_BASE_TICKET_UPDATE_REQ();
				break;
			case 2498:
				gameClientPacket_0 = new PROTOCOL_BASE_RANDOMBOX_LIST_REQ();
				break;
			case 2510:
				gameClientPacket_0 = new PROTOCOL_BASE_EVENT_PORTAL_REQ();
				break;
			case 2583:
				gameClientPacket_0 = new PROTOCOL_LOBBY_ENTER_REQ();
				break;
			case 2567:
				gameClientPacket_0 = new PROTOCOL_LOBBY_GET_ROOMINFOADD_REQ();
				break;
			case 2561:
				gameClientPacket_0 = new PROTOCOL_LOBBY_LEAVE_REQ();
				break;
			case 3329:
				gameClientPacket_0 = new PROTOCOL_INVENTORY_ENTER_REQ();
				break;
			case 3083:
				gameClientPacket_0 = new PROTOCOL_LOBBY_GET_ROOMINFOADD_REQ();
				break;
			case 2587:
				gameClientPacket_0 = new PROTOCOL_LOBBY_GET_ROOMLIST_REQ();
				break;
			case 3585:
				gameClientPacket_0 = new PROTOCOL_ROOM_JOIN_REQ();
				break;
			case 3331:
				gameClientPacket_0 = new PROTOCOL_INVENTORY_LEAVE_REQ();
				break;
			case 3596:
				gameClientPacket_0 = new PROTOCOL_ROOM_GET_PLAYERINFO_REQ();
				break;
			case 3592:
				gameClientPacket_0 = new PROTOCOL_ROOM_CREATE_REQ();
				break;
			case 3609:
				gameClientPacket_0 = new PROTOCOL_ROOM_PERSONAL_TEAM_CHANGE_REQ();
				break;
			case 3611:
				gameClientPacket_0 = new PROTOCOL_ROOM_REQUEST_MAIN_REQ();
				break;
			case 3613:
				gameClientPacket_0 = new Class3();
				break;
			case 3615:
				gameClientPacket_0 = new PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_REQ();
				break;
			case 3617:
				gameClientPacket_0 = new Class2();
				break;
			case 3619:
				gameClientPacket_0 = new PROTOCOL_ROOM_TOTAL_TEAM_CHANGE_REQ();
				break;
			case 3604:
				gameClientPacket_0 = new PROTOCOL_ROOM_CHANGE_SLOT_REQ();
				break;
			case 3602:
				gameClientPacket_0 = new PROTOCOL_ROOM_CHANGE_PASSWD_REQ();
				break;
			case 3635:
				gameClientPacket_0 = new PROTOCOL_BASE_UNKNOWN_3635_REQ();
				break;
			case 3631:
				gameClientPacket_0 = new PROTOCOL_ROOM_GET_LOBBY_USER_LIST_REQ();
				break;
			case 3643:
				gameClientPacket_0 = new PROTOCOL_GM_KICK_COMMAND_REQ();
				break;
			case 3639:
				gameClientPacket_0 = new PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ();
				break;
			case 3665:
				gameClientPacket_0 = new PROTOCOL_ROOM_GET_USER_EQUIPMENT_REQ();
				break;
			case 3657:
				gameClientPacket_0 = new PROTOCOL_ROOM_LOADING_START_REQ();
				break;
			case 3647:
				gameClientPacket_0 = new PROTOCOL_GM_EXIT_COMMAND_REQ();
				break;
			case 3686:
				gameClientPacket_0 = new PROTOCOL_ROOM_CHANGE_OBSERVER_SLOT_REQ();
				break;
			case 3671:
				gameClientPacket_0 = new PROTOCOL_ROOM_INFO_ENTER_REQ();
				break;
			case 3673:
				gameClientPacket_0 = new PROTOCOL_ROOM_INFO_LEAVE_REQ();
				break;
			case 3675:
				gameClientPacket_0 = new PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_REQ();
				break;
			case 3677:
				gameClientPacket_0 = new PROTOCOL_ROOM_CHANGE_COSTUME_REQ();
				break;
			case 3679:
				gameClientPacket_0 = new PROTOCOL_ROOM_SELECT_SLOT_CHANGE_REQ();
				break;
			case 3680:
				gameClientPacket_0 = new PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_REQ();
				break;
			case 3682:
				gameClientPacket_0 = new PROTOCOL_ROOM_SELECT_SLOT_CHANGE_REQ();
				break;
			case 3852:
				gameClientPacket_0 = new PROTOCOL_COMMUNITY_USER_REPORT_CONDITION_CHECK_REQ();
				break;
			case 3850:
				gameClientPacket_0 = new PROTOCOL_COMMUNITY_USER_REPORT_REQ();
				break;
			case 5129:
				gameClientPacket_0 = new PROTOCOL_BATTLE_PRESTARTBATTLE_REQ();
				break;
			case 5131:
				gameClientPacket_0 = new PROTOCOL_BATTLE_STARTBATTLE_REQ();
				break;
			case 5133:
				gameClientPacket_0 = new PROTOCOL_BATTLE_GIVEUPBATTLE_REQ();
				break;
			case 5135:
				gameClientPacket_0 = new PROTOCOL_BATTLE_DEATH_REQ();
				break;
			case 5137:
				gameClientPacket_0 = new PROTOCOL_BATTLE_RESPAWN_REQ();
				break;
			case 5123:
				gameClientPacket_0 = new PROTOCOL_BATTLE_READYBATTLE_REQ();
				break;
			case 5158:
				gameClientPacket_0 = new PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_REQ();
				break;
			case 5156:
				gameClientPacket_0 = new PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_REQ();
				break;
			case 5146:
				gameClientPacket_0 = new PROTOCOL_BATTLE_SENDPING_REQ();
				break;
			case 5172:
				gameClientPacket_0 = new PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_REQ();
				break;
			case 5168:
				gameClientPacket_0 = new PROTOCOL_BATTLE_TIMERSYNC_REQ();
				break;
			case 5166:
				gameClientPacket_0 = new PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_REQ();
				break;
			case 5180:
				gameClientPacket_0 = new PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_REQ();
				break;
			case 5174:
				gameClientPacket_0 = new PROTOCOL_BATTLE_RESPAWN_FOR_AI_REQ();
				break;
			case 5188:
				gameClientPacket_0 = new PROTOCOL_BATTLE_MISSION_TUTORIAL_ROUND_END_REQ();
				break;
			case 5182:
				gameClientPacket_0 = new PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_REQ();
				break;
			case 5292:
				gameClientPacket_0 = new PROTOCOL_BASE_UNKNOWN_PACKET_REQ();
				break;
			case 5276:
				gameClientPacket_0 = new PROTOCOL_BATTLE_USER_SOPETYPE_REQ();
				break;
			case 5262:
				gameClientPacket_0 = new PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_REQ();
				break;
			case 6145:
				gameClientPacket_0 = new PROTOCOL_CHAR_CREATE_CHARA_REQ();
				break;
			case 5377:
				gameClientPacket_0 = new PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ();
				break;
			case 6151:
				gameClientPacket_0 = new PROTOCOL_CHAR_DELETE_CHARA_REQ();
				break;
			case 6149:
				gameClientPacket_0 = new PROTOCOL_CHAR_CHANGE_EQUIP_REQ();
				break;
			case 7429:
				gameClientPacket_0 = new PROTOCOL_BATTLEBOX_AUTH_REQ();
				break;
			case 6965:
				gameClientPacket_0 = new PROTOCOL_CLAN_WAR_RESULT_REQ();
				break;
			case 6657:
				gameClientPacket_0 = new PROTOCOL_GMCHAT_START_CHAT_REQ();
				break;
			case 6659:
				gameClientPacket_0 = new PROTOCOL_GMCHAT_SEND_CHAT_REQ();
				break;
			case 6661:
				gameClientPacket_0 = new PROTOCOL_GMCHAT_END_CHAT_REQ();
				break;
			case 6663:
				gameClientPacket_0 = new PROTOCOL_GMCHAT_APPLY_PENALTY_REQ();
				break;
			case 6665:
				gameClientPacket_0 = new PROTOCOL_GMCHAT_NOTI_USER_PENALTY_REQ();
				break;
			case 7699:
				gameClientPacket_0 = new PROTOCOL_MATCH_CLAN_SEASON_REQ();
				break;
			case 7681:
				gameClientPacket_0 = new PROTOCOL_MATCH_SERVER_IDX_REQ();
				break;
			default:
				CLogger.Print(Bitwise.ToHexData($"Opcode Not Found: [{ushort_2}] | {string_0}", byte_0), LoggerType.Opcode);
				break;
			case 8453:
				gameClientPacket_0 = new PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_REQ();
				break;
			case 8449:
				gameClientPacket_0 = new PROTOCOL_SEASON_CHALLENGE_INFO_REQ();
				break;
			case 2392:
			case 2518:
			case 2519:
			case 5145:
				break;
			}
			if (gameClientPacket_0 == null)
			{
				return;
			}
			using (gameClientPacket_0)
			{
				if (ConfigLoader.DebugMode)
				{
					CLogger.Print($"{gameClientPacket_0.GetType().Name}; Address: {Client.RemoteEndPoint}; Opcode: [{ushort_2}]", LoggerType.Debug);
				}
				gameClientPacket_0.Makeme(this, byte_0);
				ThreadPool.QueueUserWorkItem(delegate
				{
					try
					{
						gameClientPacket_0.Run();
					}
					catch (Exception ex2)
					{
						CLogger.Print(ex2.Message, LoggerType.Error, ex2);
						Close(50, DestroyConnection: true);
					}
				});
				gameClientPacket_0.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	[CompilerGenerated]
	private void method_8(object object_0)
	{
		method_1();
	}

	[CompilerGenerated]
	private void method_9(object object_0)
	{
		method_3();
	}

	[CompilerGenerated]
	private void method_10(object object_0)
	{
		method_0();
	}

	[CompilerGenerated]
	private void method_11(object object_0)
	{
		try
		{
			method_3();
		}
		catch (Exception ex)
		{
			CLogger.Print("Error processing packet in thread pool for IP: " + GetIPAddress() + ": " + ex.Message, LoggerType.Error, ex);
			Close(0, DestroyConnection: true);
		}
	}

	[CompilerGenerated]
	private void method_12(object object_0)
	{
		method_3();
	}
}
