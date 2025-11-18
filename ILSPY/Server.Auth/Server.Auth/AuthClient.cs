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

namespace Server.Auth;

public class AuthClient : IDisposable
{
	[CompilerGenerated]
	private sealed class Class0
	{
		public AuthClient authClient_0;

		public TimerState timerState_0;

		internal void method_0(object object_0)
		{
			if (!authClient_0.bool_1)
			{
				CLogger.Print("Connection destroyed due to no response for 20 minutes (HeartBeat). IPAddress: " + authClient_0.GetIPAddress(), LoggerType.Hack);
				authClient_0.Close(0, DestroyConnection: true);
			}
			lock (object_0)
			{
				timerState_0.StopJob();
			}
		}
	}

	[CompilerGenerated]
	private sealed class Class1
	{
		public AuthClient authClient_0;

		public AuthClientPacket authClientPacket_0;

		internal void method_0(object object_0)
		{
			try
			{
				authClientPacket_0.Run();
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				authClient_0.Close(50, DestroyConnection: true);
			}
		}
	}

	public int ServerId;

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

	public AuthClient(int int_0, Socket socket_0)
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

	public void HeartBeatCounter()
	{
		TimerState timerState_0 = new TimerState();
		timerState_0.StartTimer(TimeSpan.FromMinutes(20.0), delegate(object object_0)
		{
			if (!bool_1)
			{
				CLogger.Print("Connection destroyed due to no response for 20 minutes (HeartBeat). IPAddress: " + GetIPAddress(), LoggerType.Hack);
				Close(0, DestroyConnection: true);
			}
			lock (object_0)
			{
				timerState_0.StopJob();
			}
		});
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

	public void SendPacket(AuthServerPacket Packet)
	{
		try
		{
			using (Packet)
			{
				byte[] bytes = Packet.GetBytes("AuthClient.SendPacket");
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

	public void Close(int TimeMS, bool DestroyConnection)
	{
		if (bool_1)
		{
			return;
		}
		try
		{
			bool_1 = true;
			AuthXender.Client.RemoveSession(this);
			Account player = Player;
			if (DestroyConnection)
			{
				if (player != null)
				{
					player.SetOnlineStatus(Online: false);
					if (player.Status.ServerId == 0)
					{
						SendRefresh.RefreshAccount(player, IsConnect: false);
					}
					player.Status.ResetData(player.PlayerId);
					player.SimpleClear();
					player.UpdateCacheInfo();
					Player = null;
				}
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
			CLogger.Print("AuthClient.Close: " + ex.Message, LoggerType.Error, ex);
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

	private void method_5(ushort ushort_2)
	{
		if (FirstPacketId == 0)
		{
			FirstPacketId = ushort_2;
			if (ushort_2 != 1281 && ushort_2 != 2309)
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
			AuthClientPacket authClientPacket_0 = null;
			switch (ushort_2)
			{
			case 2307:
				authClientPacket_0 = new PROTOCOL_BASE_LOGOUT_REQ();
				break;
			case 2309:
				authClientPacket_0 = new PROTOCOL_BASE_KEEP_ALIVE_REQ();
				break;
			case 2311:
				authClientPacket_0 = new PROTOCOL_BASE_GAMEGUARD_REQ();
				break;
			case 1281:
				authClientPacket_0 = new PROTOCOL_BASE_LOGIN_REQ();
				break;
			case 1057:
				authClientPacket_0 = new PROTOCOL_AUTH_GET_POINT_CASH_REQ();
				break;
			case 2332:
				authClientPacket_0 = new PROTOCOL_BASE_GET_CHANNELLIST_REQ();
				break;
			case 2328:
				authClientPacket_0 = new PROTOCOL_BASE_USER_LEAVE_REQ();
				break;
			case 2314:
				authClientPacket_0 = new PROTOCOL_BASE_GET_SYSTEM_INFO_REQ();
				break;
			case 2316:
				authClientPacket_0 = new PROTOCOL_BASE_GET_USER_INFO_REQ();
				break;
			case 2318:
				authClientPacket_0 = new PROTOCOL_BASE_GET_INVEN_INFO_REQ();
				break;
			case 2320:
				authClientPacket_0 = new PROTOCOL_BASE_GET_OPTION_REQ();
				break;
			case 2322:
				authClientPacket_0 = new PROTOCOL_BASE_OPTION_SAVE_REQ();
				break;
			case 2459:
				authClientPacket_0 = new PROTOCOL_BASE_GET_MAP_INFO_REQ();
				break;
			case 2414:
				authClientPacket_0 = new PROTOCOL_BASE_DAILY_RECORD_REQ();
				break;
			case 2399:
				authClientPacket_0 = new PROTOCOL_BASE_GAME_SERVER_STATE_REQ();
				break;
			case 2516:
				authClientPacket_0 = new PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_REQ();
				break;
			case 2489:
				authClientPacket_0 = new PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_REQ();
				break;
			default:
				CLogger.Print(Bitwise.ToHexData($"Opcode Not Found: [{ushort_2}] | {string_0}", byte_0), LoggerType.Opcode);
				break;
			case 7699:
				authClientPacket_0 = new PROTOCOL_MATCH_CLAN_SEASON_REQ();
				break;
			case 7681:
				authClientPacket_0 = new PROTOCOL_MATCH_SERVER_IDX_REQ();
				break;
			}
			if (authClientPacket_0 == null)
			{
				return;
			}
			using (authClientPacket_0)
			{
				if (ConfigLoader.DebugMode)
				{
					CLogger.Print($"{authClientPacket_0.GetType().Name}; Address: {Client.RemoteEndPoint}; Opcode: [{ushort_2}]", LoggerType.Debug);
				}
				authClientPacket_0.Makeme(this, byte_0);
				ThreadPool.QueueUserWorkItem(delegate
				{
					try
					{
						authClientPacket_0.Run();
					}
					catch (Exception ex2)
					{
						CLogger.Print(ex2.Message, LoggerType.Error, ex2);
						Close(50, DestroyConnection: true);
					}
				});
				authClientPacket_0.Dispose();
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
