using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Data.XML;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Data.Sync.Client;

public static class RoomPing
{
	public static void Load(SyncClientPacket C)
	{
		int id = C.ReadH();
		int ıd = C.ReadH();
		short serverId = C.ReadH();
		int slotIdx = C.ReadC();
		int num = C.ReadC();
		int num2 = C.ReadUH();
		if (C.ToArray().Length > 12)
		{
			CLogger.Print($"Invalid Ping (Length > 12): {C.ToArray().Length}", LoggerType.Warning);
		}
		ChannelModel channel = ChannelsXML.GetChannel(serverId, ıd);
		if (channel == null)
		{
			return;
		}
		RoomModel room = channel.GetRoom(id);
		if (room == null || room.RoundTime.IsTimer() || room.State != RoomState.BATTLE)
		{
			return;
		}
		SlotModel slot = room.GetSlot(slotIdx);
		if (slot == null || slot.State != SlotState.BATTLE)
		{
			return;
		}
		Account playerBySlot = room.GetPlayerBySlot(slot);
		if (!room.IsBotMode() && playerBySlot != null)
		{
			slot.Latency = num2;
			slot.Ping = num;
			if (slot.Latency >= ConfigLoader.MaxLatency)
			{
				slot.FailLatencyTimes++;
			}
			else
			{
				slot.FailLatencyTimes = 0;
			}
			if (ConfigLoader.IsDebugPing && ComDiv.GetDuration(playerBySlot.LastPingDebug) >= (double)ConfigLoader.PingUpdateTimeSeconds)
			{
				playerBySlot.LastPingDebug = DateTimeUtil.Now();
				playerBySlot.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK("Server", 0, 5, bool_1: false, $"{num2}ms ({num} bar)"));
			}
			if (slot.FailLatencyTimes >= ConfigLoader.MaxRepeatLatency)
			{
				CLogger.Print($"Player: '{playerBySlot.Nickname}' (Id: {slot.PlayerId}) kicked due to high latency. ({slot.Latency}/{ConfigLoader.MaxLatency}ms)", LoggerType.Warning);
				playerBySlot.Connection.Close(500, DestroyConnection: true);
			}
			else
			{
				AllUtils.RoomPingSync(room);
			}
		}
	}
}
