using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Utility;

namespace Server.Match;

public class MatchManager
{
	private readonly string string_0;

	private readonly int int_0;

	public Socket MainSocket;

	public bool ServerIsClosed;

	public MatchManager(string string_1, int int_1)
	{
		string_0 = string_1;
		int_0 = int_1;
	}

	public bool Start()
	{
		try
		{
			MainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			MainSocket.IOControl(-1744830452, new byte[1] { Convert.ToByte(value: false) }, null);
			IPEndPoint ıPEndPoint = new IPEndPoint(IPAddress.Parse(string_0), int_0);
			MainSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, optionValue: true);
			MainSocket.SetIPProtectionLevel(IPProtectionLevel.EdgeRestricted);
			MainSocket.DontFragment = false;
			MainSocket.Ttl = 128;
			MainSocket.Bind(ıPEndPoint);
			CLogger.Print($"Match Serv Address {ıPEndPoint.Address}:{ıPEndPoint.Port}", LoggerType.Info);
			ThreadPool.QueueUserWorkItem(delegate
			{
				try
				{
					method_0();
				}
				catch (Exception ex2)
				{
					CLogger.Print("Error processing UDP packet from " + string_0 + ": " + ex2.Message, LoggerType.Error, ex2);
				}
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
		try
		{
			StateObject stateObject = new StateObject
			{
				WorkSocket = MainSocket,
				RemoteEP = new IPEndPoint(IPAddress.Any, 0)
			};
			MainSocket.BeginReceiveFrom(stateObject.UdpBuffer, 0, 8096, SocketFlags.None, ref stateObject.RemoteEP, method_1, stateObject);
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	private void method_1(IAsyncResult iasyncResult_0)
	{
		if (!iasyncResult_0.IsCompleted)
		{
			CLogger.Print("IAsyncResult is not completed.", LoggerType.Warning);
		}
		StateObject stateObject = iasyncResult_0.AsyncState as StateObject;
		Socket workSocket = stateObject.WorkSocket;
		IPEndPoint ıPEndPoint = stateObject.RemoteEP as IPEndPoint;
		try
		{
			EndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
			int num = workSocket.EndReceiveFrom(iasyncResult_0, ref endPoint);
			if (num > 0)
			{
				byte[] array = new byte[num];
				Buffer.BlockCopy(stateObject.UdpBuffer, 0, array, 0, num);
				if (array.Length >= 22 && array.Length <= 8096)
				{
					BeginReceive(new MatchClient(workSocket, endPoint as IPEndPoint), array);
				}
				else
				{
					CLogger.Print($"Invalid Buffer Length: {array.Length}; IP: {ıPEndPoint}", LoggerType.Hack);
				}
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("Error during EndReceiveCallback: " + ex.Message, LoggerType.Error, ex);
			method_2(ıPEndPoint);
			method_3($"{ıPEndPoint.Address}");
		}
		finally
		{
			method_0();
		}
	}

	public void BeginReceive(MatchClient Client, byte[] Buffer)
	{
		try
		{
			if (Client == null)
			{
				CLogger.Print("Destroyed after failed to add to list.", LoggerType.Warning);
			}
			else
			{
				Client.BeginReceive(Buffer, DateTimeUtil.Now());
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	private bool method_2(IPEndPoint ipendPoint_0)
	{
		try
		{
			if (ipendPoint_0 == null)
			{
				return false;
			}
			if (MatchXender.UdpClients.ContainsKey(ipendPoint_0) && MatchXender.UdpClients.TryGetValue(ipendPoint_0, out var value))
			{
				return MatchXender.UdpClients.TryRemove(ipendPoint_0, out value);
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
		return false;
	}

	private bool method_3(string string_1)
	{
		try
		{
			if (string.IsNullOrEmpty(string_1) || string_1.Equals("0.0.0.0"))
			{
				return false;
			}
			if (MatchXender.SpamConnections.ContainsKey(string_1) && MatchXender.SpamConnections.TryGetValue(string_1, out var value))
			{
				return MatchXender.SpamConnections.TryRemove(string_1, out value);
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
		return false;
	}

	public void SendPacket(byte[] Data, IPEndPoint Address)
	{
		try
		{
			MainSocket.BeginSendTo(Data, 0, Data.Length, SocketFlags.None, Address, method_4, MainSocket);
		}
		catch (Exception ex)
		{
			CLogger.Print($"Failed to send package to {Address}: {ex.Message}", LoggerType.Error, ex);
		}
	}

	private void method_4(IAsyncResult iasyncResult_0)
	{
		try
		{
			if (iasyncResult_0.AsyncState is Socket socket && socket.Connected)
			{
				socket.EndSend(iasyncResult_0);
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("Error during EndSendCallback: " + ex.Message, LoggerType.Error, ex);
		}
	}

	[CompilerGenerated]
	private void method_5(object object_0)
	{
		try
		{
			method_0();
		}
		catch (Exception ex)
		{
			CLogger.Print("Error processing UDP packet from " + string_0 + ": " + ex.Message, LoggerType.Error, ex);
		}
	}
}
