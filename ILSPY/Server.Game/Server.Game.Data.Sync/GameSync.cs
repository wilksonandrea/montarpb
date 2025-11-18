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

namespace Server.Game.Data.Sync;

public class GameSync
{
	[CompilerGenerated]
	private sealed class Class10
	{
		public GameSync gameSync_0;

		public byte[] byte_0;

		internal void method_0(object object_0)
		{
			try
			{
				gameSync_0.method_2(byte_0);
			}
			catch (Exception ex)
			{
				CLogger.Print("Error processing AuthSync packet in thread pool: " + ex.Message, LoggerType.Error, ex);
			}
		}
	}

	protected Socket ClientSocket;

	private bool bool_0;

	public GameSync(IPEndPoint ipendPoint_0)
	{
		ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
		ClientSocket.Bind(ipendPoint_0);
		ClientSocket.IOControl(-1744830452, new byte[1] { Convert.ToByte(value: false) }, null);
	}

	public bool Start()
	{
		try
		{
			IPEndPoint ıPEndPoint = ClientSocket.LocalEndPoint as IPEndPoint;
			CLogger.Print($"Game Sync Address {ıPEndPoint.Address}:{ıPEndPoint.Port}", LoggerType.Info);
			ThreadPool.QueueUserWorkItem(delegate
			{
				method_0();
			});
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	private void method_0()
	{
		if (bool_0)
		{
			return;
		}
		try
		{
			StateObject stateObject = new StateObject
			{
				WorkSocket = ClientSocket,
				RemoteEP = new IPEndPoint(IPAddress.Any, 8000)
			};
			ClientSocket.BeginReceiveFrom(stateObject.UdpBuffer, 0, 8096, SocketFlags.None, ref stateObject.RemoteEP, method_1, stateObject);
		}
		catch (ObjectDisposedException)
		{
			CLogger.Print("GameSync socket disposed during StartReceive.", LoggerType.Warning);
			Close();
		}
		catch (Exception ex2)
		{
			CLogger.Print("Error in StartReceive: " + ex2.Message, LoggerType.Error, ex2);
			Close();
		}
	}

	private void method_1(IAsyncResult iasyncResult_0)
	{
		if (bool_0 || GameXender.Client == null || GameXender.Client.ServerIsClosed)
		{
			return;
		}
		StateObject stateObject = iasyncResult_0.AsyncState as StateObject;
		try
		{
			int num = ClientSocket.EndReceiveFrom(iasyncResult_0, ref stateObject.RemoteEP);
			if (num <= 0)
			{
				return;
			}
			byte[] byte_0 = new byte[num];
			Array.Copy(stateObject.UdpBuffer, 0, byte_0, 0, num);
			ThreadPool.QueueUserWorkItem(delegate
			{
				try
				{
					method_2(byte_0);
				}
				catch (Exception ex3)
				{
					CLogger.Print("Error processing AuthSync packet in thread pool: " + ex3.Message, LoggerType.Error, ex3);
				}
			});
		}
		catch (ObjectDisposedException)
		{
			CLogger.Print("GameSync socket disposed during ReceiveCallback.", LoggerType.Warning);
			Close();
		}
		catch (Exception ex2)
		{
			CLogger.Print("Error in ReceiveCallback: " + ex2.Message, LoggerType.Error, ex2);
		}
		finally
		{
			method_0();
		}
	}

	private void method_2(byte[] byte_0)
	{
		try
		{
			SyncClientPacket syncClientPacket = new SyncClientPacket(byte_0);
			short num = syncClientPacket.ReadH();
			switch (num)
			{
			case 7171:
				ServerMessage.Load(syncClientPacket);
				break;
			case 1:
				RoomPassPortal.Load(syncClientPacket);
				break;
			case 2:
				RoomBombC4.Load(syncClientPacket);
				break;
			case 3:
				RoomDeath.Load(syncClientPacket);
				break;
			case 4:
				RoomHitMarker.Load(syncClientPacket);
				break;
			case 5:
				RoomSadeSync.Load(syncClientPacket);
				break;
			case 6:
				RoomPing.Load(syncClientPacket);
				break;
			case 10:
				AuthLogin.Load(syncClientPacket);
				break;
			case 11:
				FriendInfo.Load(syncClientPacket);
				break;
			case 13:
				AccountInfo.Load(syncClientPacket);
				break;
			case 15:
				ServerCache.Load(syncClientPacket);
				break;
			case 16:
				ClanSync.Load(syncClientPacket);
				break;
			case 17:
				FriendSync.Load(syncClientPacket);
				break;
			case 18:
				InventorySync.Load(syncClientPacket);
				break;
			case 19:
				PlayerSync.Load(syncClientPacket);
				break;
			case 20:
				ServerWarning.LoadGMWarning(syncClientPacket);
				break;
			case 21:
				ClanServersSync.Load(syncClientPacket);
				break;
			case 22:
				ServerWarning.LoadShopRestart(syncClientPacket);
				break;
			case 23:
				ServerWarning.LoadServerUpdate(syncClientPacket);
				break;
			case 24:
				ServerWarning.LoadShutdown(syncClientPacket);
				break;
			default:
				CLogger.Print(Bitwise.ToHexData($"Game - Opcode Not Found: [{num}]", syncClientPacket.ToArray()), LoggerType.Opcode);
				break;
			case 31:
				EventInfo.LoadEventInfo(syncClientPacket);
				break;
			case 32:
				ReloadConfig.Load(syncClientPacket);
				break;
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	public SChannelModel GetServer(AccountStatus status)
	{
		return GetServer(status.ServerId);
	}

	public SChannelModel GetServer(int serverId)
	{
		if (serverId != 255 && serverId != GameXender.Client.ServerId)
		{
			return SChannelXML.GetServer(serverId);
		}
		return null;
	}

	public void SendBytes(long PlayerId, GameServerPacket Packet, int ServerId)
	{
		try
		{
			if (Packet == null)
			{
				return;
			}
			SChannelModel server = GetServer(ServerId);
			if (server == null)
			{
				return;
			}
			string name = Packet.GetType().Name;
			byte[] bytes = Packet.GetBytes("GameSync.SendBytes");
			IPEndPoint connection = SynchronizeXML.GetServer(server.Port).Connection;
			using SyncServerPacket syncServerPacket = new SyncServerPacket();
			syncServerPacket.WriteH(13);
			syncServerPacket.WriteQ(PlayerId);
			syncServerPacket.WriteC(0);
			syncServerPacket.WriteC((byte)(name.Length + 1));
			syncServerPacket.WriteS(name, name.Length + 1);
			syncServerPacket.WriteH((ushort)bytes.Length);
			syncServerPacket.WriteB(bytes);
			SendPacket(syncServerPacket.ToArray(), connection);
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	public void SendBytes(long PlayerId, string PacketName, byte[] Data, int ServerId)
	{
		try
		{
			if (Data.Length == 0)
			{
				return;
			}
			SChannelModel server = GetServer(ServerId);
			if (server == null)
			{
				return;
			}
			IPEndPoint connection = SynchronizeXML.GetServer(server.Port).Connection;
			using SyncServerPacket syncServerPacket = new SyncServerPacket();
			syncServerPacket.WriteH(13);
			syncServerPacket.WriteQ(PlayerId);
			syncServerPacket.WriteC(0);
			syncServerPacket.WriteC((byte)(PacketName.Length + 1));
			syncServerPacket.WriteS(PacketName, PacketName.Length + 1);
			syncServerPacket.WriteH((ushort)Data.Length);
			syncServerPacket.WriteB(Data);
			SendPacket(syncServerPacket.ToArray(), connection);
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	public void SendCompleteBytes(long PlayerId, string PacketName, byte[] Data, int ServerId)
	{
		try
		{
			if (Data.Length == 0)
			{
				return;
			}
			SChannelModel server = GetServer(ServerId);
			if (server == null)
			{
				return;
			}
			IPEndPoint connection = SynchronizeXML.GetServer(server.Port).Connection;
			using SyncServerPacket syncServerPacket = new SyncServerPacket();
			syncServerPacket.WriteH(13);
			syncServerPacket.WriteQ(PlayerId);
			syncServerPacket.WriteC(1);
			syncServerPacket.WriteC((byte)(PacketName.Length + 1));
			syncServerPacket.WriteS(PacketName, PacketName.Length + 1);
			syncServerPacket.WriteH((ushort)Data.Length);
			syncServerPacket.WriteB(Data);
			SendPacket(syncServerPacket.ToArray(), connection);
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	public void SendPacket(byte[] Data, IPEndPoint Address)
	{
		if (bool_0)
		{
			return;
		}
		try
		{
			ClientSocket.BeginSendTo(Data, 0, Data.Length, SocketFlags.None, Address, method_3, ClientSocket);
		}
		catch (ObjectDisposedException)
		{
			CLogger.Print($"GameSync socket disposed during SendPacket to {Address}.", LoggerType.Warning);
		}
		catch (Exception ex2)
		{
			CLogger.Print($"Error sending UDP packet to {Address}: {ex2.Message}", LoggerType.Error, ex2);
		}
	}

	private void method_3(IAsyncResult iasyncResult_0)
	{
		try
		{
			if (iasyncResult_0.AsyncState is Socket socket && socket.Connected)
			{
				socket.EndSend(iasyncResult_0);
			}
		}
		catch (ObjectDisposedException)
		{
			CLogger.Print("GameSync socket disposed during SendCallback.", LoggerType.Warning);
		}
		catch (Exception ex2)
		{
			CLogger.Print("Error in SendCallback: " + ex2.Message, LoggerType.Error, ex2);
		}
	}

	public void Close()
	{
		if (bool_0)
		{
			return;
		}
		bool_0 = true;
		try
		{
			if (ClientSocket != null)
			{
				ClientSocket.Close();
				ClientSocket.Dispose();
				ClientSocket = null;
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("Error closing GameSync: " + ex.Message, LoggerType.Error, ex);
		}
	}

	[CompilerGenerated]
	private void method_4(object object_0)
	{
		method_0();
	}
}
