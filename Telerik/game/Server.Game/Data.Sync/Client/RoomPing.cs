using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Data.XML;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Data.Sync.Client
{
	public static class RoomPing
	{
		public static void Load(SyncClientPacket C)
		{
			int ınt32 = C.ReadH();
			int ınt321 = C.ReadH();
			short ınt16 = C.ReadH();
			int ınt322 = C.ReadC();
			int ınt323 = C.ReadC();
			int ınt324 = C.ReadUH();
			if ((int)C.ToArray().Length > 12)
			{
				CLogger.Print(string.Format("Invalid Ping (Length > 12): {0}", (int)C.ToArray().Length), LoggerType.Warning, null);
			}
			ChannelModel channel = ChannelsXML.GetChannel(ınt16, ınt321);
			if (channel == null)
			{
				return;
			}
			RoomModel room = channel.GetRoom(ınt32);
			if (room != null && !room.RoundTime.IsTimer() && room.State == RoomState.BATTLE)
			{
				SlotModel slot = room.GetSlot(ınt322);
				if (slot != null && slot.State == SlotState.BATTLE)
				{
					Account playerBySlot = room.GetPlayerBySlot(slot);
					if (room.IsBotMode() || playerBySlot == null)
					{
						return;
					}
					slot.Latency = ınt324;
					slot.Ping = ınt323;
					if (slot.Latency < ConfigLoader.MaxLatency)
					{
						slot.FailLatencyTimes = 0;
					}
					else
					{
						slot.FailLatencyTimes++;
					}
					if (ConfigLoader.IsDebugPing && ComDiv.GetDuration(playerBySlot.LastPingDebug) >= (double)ConfigLoader.PingUpdateTimeSeconds)
					{
						playerBySlot.LastPingDebug = DateTimeUtil.Now();
						playerBySlot.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK("Server", 0, 5, false, string.Format("{0}ms ({1} bar)", ınt324, ınt323)));
					}
					if (slot.FailLatencyTimes >= ConfigLoader.MaxRepeatLatency)
					{
						CLogger.Print(string.Format("Player: '{0}' (Id: {1}) kicked due to high latency. ({2}/{3}ms)", new object[] { playerBySlot.Nickname, slot.PlayerId, slot.Latency, ConfigLoader.MaxLatency }), LoggerType.Warning, null);
						playerBySlot.Connection.Close(500, true, false);
						return;
					}
					AllUtils.RoomPingSync(room);
				}
			}
		}
	}
}