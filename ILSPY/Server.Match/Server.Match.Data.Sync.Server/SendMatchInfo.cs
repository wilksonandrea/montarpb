using System;
using System.Collections.Generic;
using System.Net;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Match.Data.Enums;
using Server.Match.Data.Models;

namespace Server.Match.Data.Sync.Server;

public class SendMatchInfo
{
	public static void SendPortalPass(RoomModel Room, PlayerModel Player, int Portal)
	{
		try
		{
			if (Room.RoomType != RoomCondition.Boss)
			{
				return;
			}
			Player.Life = Player.MaxLife;
			IPEndPoint connection = SynchronizeXML.GetServer(Room.Server.Port).Connection;
			using SyncServerPacket syncServerPacket = new SyncServerPacket();
			syncServerPacket.WriteH(1);
			syncServerPacket.WriteH((short)Room.RoomId);
			syncServerPacket.WriteH((short)Room.ChannelId);
			syncServerPacket.WriteH((short)Room.ServerId);
			syncServerPacket.WriteC((byte)Player.Slot);
			syncServerPacket.WriteC((byte)Portal);
			MatchXender.Sync.SendPacket(syncServerPacket.ToArray(), connection);
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	public static void SendBombSync(RoomModel Room, PlayerModel Player, int Type, int BombArea)
	{
		try
		{
			IPEndPoint connection = SynchronizeXML.GetServer(Room.Server.Port).Connection;
			using SyncServerPacket syncServerPacket = new SyncServerPacket();
			syncServerPacket.WriteH(2);
			syncServerPacket.WriteH((short)Room.RoomId);
			syncServerPacket.WriteH((short)Room.ChannelId);
			syncServerPacket.WriteH((short)Room.ServerId);
			syncServerPacket.WriteC((byte)Type);
			syncServerPacket.WriteC((byte)Player.Slot);
			if (Type == 0)
			{
				syncServerPacket.WriteC((byte)BombArea);
				syncServerPacket.WriteTV(Player.Position);
				syncServerPacket.WriteH(42);
				Room.BombPosition = Player.Position;
			}
			MatchXender.Sync.SendPacket(syncServerPacket.ToArray(), connection);
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	public static void SendDeathSync(RoomModel Room, PlayerModel Killer, int ObjectId, int WeaponId, List<DeathServerData> Deaths)
	{
		try
		{
			IPEndPoint connection = SynchronizeXML.GetServer(Room.Server.Port).Connection;
			using SyncServerPacket syncServerPacket = new SyncServerPacket();
			syncServerPacket.WriteH(3);
			syncServerPacket.WriteH((short)Room.RoomId);
			syncServerPacket.WriteH((short)Room.ChannelId);
			syncServerPacket.WriteH((short)Room.ServerId);
			syncServerPacket.WriteC((byte)Deaths.Count);
			syncServerPacket.WriteC((byte)Killer.Slot);
			syncServerPacket.WriteD(WeaponId);
			syncServerPacket.WriteTV(Killer.Position);
			syncServerPacket.WriteC((byte)ObjectId);
			syncServerPacket.WriteC(0);
			foreach (DeathServerData Death in Deaths)
			{
				syncServerPacket.WriteC((byte)Death.Player.Slot);
				syncServerPacket.WriteC((byte)ComDiv.GetIdStatics(WeaponId, 2));
				syncServerPacket.WriteC((byte)((int)Death.DeathType * 16 + Death.Player.Slot));
				syncServerPacket.WriteTV(Death.Player.Position);
				syncServerPacket.WriteC((byte)Death.AssistSlot);
				syncServerPacket.WriteC(0);
				syncServerPacket.WriteB(new byte[8]);
			}
			MatchXender.Sync.SendPacket(syncServerPacket.ToArray(), connection);
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	public static void SendHitMarkerSync(RoomModel Room, PlayerModel Player, CharaDeath DeathType, HitType HitEnum, int Damage)
	{
		try
		{
			IPEndPoint connection = SynchronizeXML.GetServer(Room.Server.Port).Connection;
			using SyncServerPacket syncServerPacket = new SyncServerPacket();
			syncServerPacket.WriteH(4);
			syncServerPacket.WriteH((short)Room.RoomId);
			syncServerPacket.WriteH((short)Room.ChannelId);
			syncServerPacket.WriteH((short)Room.ServerId);
			syncServerPacket.WriteC((byte)Player.Slot);
			syncServerPacket.WriteC((byte)DeathType);
			syncServerPacket.WriteC((byte)HitEnum);
			syncServerPacket.WriteD(Damage);
			MatchXender.Sync.SendPacket(syncServerPacket.ToArray(), connection);
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	public static void SendSabotageSync(RoomModel Room, PlayerModel Player, int Damage, int UltraSync)
	{
		try
		{
			IPEndPoint connection = SynchronizeXML.GetServer(Room.Server.Port).Connection;
			using SyncServerPacket syncServerPacket = new SyncServerPacket();
			syncServerPacket.WriteH(5);
			syncServerPacket.WriteH((short)Room.RoomId);
			syncServerPacket.WriteH((short)Room.ChannelId);
			syncServerPacket.WriteH((short)Room.ServerId);
			syncServerPacket.WriteC((byte)Player.Slot);
			syncServerPacket.WriteH((ushort)Room.Bar1);
			syncServerPacket.WriteH((ushort)Room.Bar2);
			syncServerPacket.WriteC((byte)UltraSync);
			syncServerPacket.WriteH((ushort)Damage);
			MatchXender.Sync.SendPacket(syncServerPacket.ToArray(), connection);
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	public static void SendPingSync(RoomModel Room, PlayerModel Player)
	{
		try
		{
			IPEndPoint connection = SynchronizeXML.GetServer(Room.Server.Port).Connection;
			using SyncServerPacket syncServerPacket = new SyncServerPacket();
			syncServerPacket.WriteH(6);
			syncServerPacket.WriteH((short)Room.RoomId);
			syncServerPacket.WriteH((short)Room.ChannelId);
			syncServerPacket.WriteH((short)Room.ServerId);
			syncServerPacket.WriteC((byte)Player.Slot);
			syncServerPacket.WriteC((byte)Player.Ping);
			syncServerPacket.WriteH((ushort)Player.Latency);
			MatchXender.Sync.SendPacket(syncServerPacket.ToArray(), connection);
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
