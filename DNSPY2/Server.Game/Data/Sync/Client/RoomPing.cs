using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Data.XML;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Data.Sync.Client
{
	// Token: 0x020001FC RID: 508
	public static class RoomPing
	{
		// Token: 0x060005ED RID: 1517 RVA: 0x00030B28 File Offset: 0x0002ED28
		public static void Load(SyncClientPacket C)
		{
			int num = (int)C.ReadH();
			int num2 = (int)C.ReadH();
			int num3 = (int)C.ReadH();
			int num4 = (int)C.ReadC();
			int num5 = (int)C.ReadC();
			int num6 = (int)C.ReadUH();
			if (C.ToArray().Length > 12)
			{
				CLogger.Print(string.Format("Invalid Ping (Length > 12): {0}", C.ToArray().Length), LoggerType.Warning, null);
			}
			ChannelModel channel = ChannelsXML.GetChannel(num3, num2);
			if (channel == null)
			{
				return;
			}
			RoomModel room = channel.GetRoom(num);
			if (room != null && !room.RoundTime.IsTimer() && room.State == RoomState.BATTLE)
			{
				SlotModel slot = room.GetSlot(num4);
				if (slot != null && slot.State == SlotState.BATTLE)
				{
					Account playerBySlot = room.GetPlayerBySlot(slot);
					if (room.IsBotMode() || playerBySlot == null)
					{
						return;
					}
					slot.Latency = num6;
					slot.Ping = num5;
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
						playerBySlot.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK("Server", 0, 5, false, string.Format("{0}ms ({1} bar)", num6, num5)));
					}
					if (slot.FailLatencyTimes >= ConfigLoader.MaxRepeatLatency)
					{
						CLogger.Print(string.Format("Player: '{0}' (Id: {1}) kicked due to high latency. ({2}/{3}ms)", new object[]
						{
							playerBySlot.Nickname,
							slot.PlayerId,
							slot.Latency,
							ConfigLoader.MaxLatency
						}), LoggerType.Warning, null);
						playerBySlot.Connection.Close(500, true, false);
						return;
					}
					AllUtils.RoomPingSync(room);
				}
			}
		}
	}
}
