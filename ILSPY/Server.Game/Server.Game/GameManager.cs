using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.JSON;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Network;

namespace Server.Game;

public class GameManager
{
	private readonly string string_0;

	private readonly int int_0;

	public readonly int ServerId;

	public ServerConfig Config;

	public Socket MainSocket;

	public bool ServerIsClosed;

	public GameManager(int int_1, string string_1, int int_2)
	{
		string_0 = string_1;
		int_0 = int_2;
		ServerId = int_1;
	}

	public bool Start()
	{
		try
		{
			Config = ServerConfigJSON.GetConfig(ConfigLoader.ConfigId);
			MainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			IPEndPoint localEP = new IPEndPoint(IPAddress.Parse(string_0), int_0);
			MainSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, optionValue: true);
			MainSocket.SetIPProtectionLevel(IPProtectionLevel.EdgeRestricted);
			MainSocket.DontFragment = false;
			MainSocket.NoDelay = true;
			MainSocket.Bind(localEP);
			MainSocket.Listen(ConfigLoader.BackLog);
			CLogger.Print($"Game Serv Address {string_0}:{int_0}", LoggerType.Info);
			ThreadPool.QueueUserWorkItem(delegate
			{
				try
				{
					method_0();
				}
				catch (Exception ex2)
				{
					CLogger.Print("Error processing TCP packet from " + string_0 + ": " + ex2.Message, LoggerType.Error, ex2);
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
			MainSocket.BeginAccept(method_1, MainSocket);
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	private void method_1(IAsyncResult iasyncResult_0)
	{
		if (!ServerIsClosed)
		{
			Socket socket = iasyncResult_0.AsyncState as Socket;
			Socket socket_ = null;
			try
			{
				socket_ = socket.EndAccept(iasyncResult_0);
			}
			catch (Exception ex)
			{
				CLogger.Print($"Accept Callback Date: {DateTimeUtil.Now()}; Exception: {ex.Message}", LoggerType.Error);
			}
			method_2(socket_, socket);
		}
	}

	private void method_2(Socket socket_0, Socket socket_1)
	{
		try
		{
			Thread.Sleep(5);
			method_0();
			if (socket_0 != null)
			{
				AddSession(new GameClient(ServerId, socket_0));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	public void AddSession(GameClient Client)
	{
		try
		{
			if (Client == null)
			{
				CLogger.Print("Destroyed after failed to add to list.", LoggerType.Warning);
				return;
			}
			DateTime dateTime = DateTimeUtil.Now();
			int num = 1;
			while (true)
			{
				if (num < 100000)
				{
					if (!GameXender.SocketSessions.ContainsKey(num) && GameXender.SocketSessions.TryAdd(num, Client))
					{
						break;
					}
					num++;
					continue;
				}
				CLogger.Print($"Unable to add session list. IPAddress: {Client.GetIPAddress()}; Date: {dateTime}", LoggerType.Warning);
				Client.Close(500, DestroyConnection: true);
				return;
			}
			Client.SessionDate = dateTime;
			Client.SessionId = num;
			Client.SessionSeed = (ushort)new Random(dateTime.Millisecond).Next(num, 32767);
			Client.StartSession();
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	public bool RemoveSession(GameClient Client)
	{
		try
		{
			if (Client == null || Client.SessionId == 0)
			{
				return false;
			}
			if (GameXender.SocketSessions.ContainsKey(Client.SessionId) && GameXender.SocketSessions.TryRemove(Client.SessionId, out var _))
			{
				return true;
			}
			Client = null;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
		return false;
	}

	public int SendPacketToAllClients(GameServerPacket Packet)
	{
		int num = 0;
		if (GameXender.SocketSessions.Count == 0)
		{
			return num;
		}
		byte[] completeBytes = Packet.GetCompleteBytes("GameManager.SendPacketToAllClients");
		foreach (GameClient value in GameXender.SocketSessions.Values)
		{
			Account player = value.Player;
			if (player != null && player.IsOnline)
			{
				player.SendCompletePacket(completeBytes, Packet.GetType().Name);
				num++;
			}
		}
		return num;
	}

	public Account SearchActiveClient(long PlayerId)
	{
		if (GameXender.SocketSessions.Count == 0)
		{
			return null;
		}
		foreach (GameClient value in GameXender.SocketSessions.Values)
		{
			Account player = value.Player;
			if (player != null && player.PlayerId == PlayerId)
			{
				return player;
			}
		}
		return null;
	}

	public Account SearchActiveClient(uint SessionId)
	{
		if (GameXender.SocketSessions.Count == 0)
		{
			return null;
		}
		foreach (GameClient value in GameXender.SocketSessions.Values)
		{
			if (value.Player != null && value.SessionId == SessionId)
			{
				return value.Player;
			}
		}
		return null;
	}

	public int KickActiveClient(double Hours)
	{
		int num = 0;
		DateTime dateTime = DateTimeUtil.Now();
		foreach (GameClient value in GameXender.SocketSessions.Values)
		{
			Account player = value.Player;
			if (player != null && player.Room == null && player.ChannelId > -1 && !player.IsGM() && (dateTime - player.LastLobbyEnter).TotalHours >= Hours)
			{
				num++;
				player.Close(5000);
			}
		}
		return num;
	}

	public int KickCountActiveClient(double Hours)
	{
		int num = 0;
		DateTime dateTime = DateTimeUtil.Now();
		foreach (GameClient value in GameXender.SocketSessions.Values)
		{
			Account player = value.Player;
			if (player != null && player.Room == null && player.ChannelId > -1 && !player.IsGM() && (dateTime - player.LastLobbyEnter).TotalHours >= Hours)
			{
				num++;
			}
		}
		return num;
	}

	[CompilerGenerated]
	private void method_3(object object_0)
	{
		try
		{
			method_0();
		}
		catch (Exception ex)
		{
			CLogger.Print("Error processing TCP packet from " + string_0 + ": " + ex.Message, LoggerType.Error, ex);
		}
	}
}
