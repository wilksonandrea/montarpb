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

namespace Server.Match.Data.Sync;

public class MatchSync
{
	[CompilerGenerated]
	private sealed class Class0
	{
		public MatchSync matchSync_0;

		public byte[] byte_0;

		internal void method_0(object object_0)
		{
			try
			{
				matchSync_0.method_2(byte_0);
			}
			catch (Exception ex)
			{
				CLogger.Print("Error processing AuthSync packet in thread pool: " + ex.Message, LoggerType.Error, ex);
			}
		}
	}

	protected Socket ClientSocket;

	private bool bool_0;

	public MatchSync(IPEndPoint ipendPoint_0)
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
			CLogger.Print($"Match Sync Address {ıPEndPoint.Address}:{ıPEndPoint.Port}", LoggerType.Info);
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
			CLogger.Print("MatchSync socket disposed during StartReceive.", LoggerType.Warning);
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
		if (bool_0 || MatchXender.Client == null || MatchXender.Client.ServerIsClosed)
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
			CLogger.Print("MatchSync socket disposed during ReceiveCallback.", LoggerType.Warning);
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
			default:
				CLogger.Print(Bitwise.ToHexData($"Match - Opcode Not Found: [{num}]", syncClientPacket.ToArray()), LoggerType.Opcode);
				break;
			case 1:
				RespawnSync.Load(syncClientPacket);
				break;
			case 2:
				RemovePlayerSync.Load(syncClientPacket);
				break;
			case 3:
				MatchRoundSync.Load(syncClientPacket);
				break;
			}
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
			CLogger.Print($"MatchSync socket disposed during SendPacket to {Address}.", LoggerType.Warning);
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
			CLogger.Print("MatchSync socket disposed during SendCallback.", LoggerType.Warning);
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
			CLogger.Print("Error closing MatchSync: " + ex.Message, LoggerType.Error, ex);
		}
	}

	[CompilerGenerated]
	private void method_4(object object_0)
	{
		method_0();
	}
}
