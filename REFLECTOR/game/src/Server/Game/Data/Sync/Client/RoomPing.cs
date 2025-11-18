namespace Server.Game.Data.Sync.Client
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using Plugin.Core.Utility;
    using Server.Game.Data.Models;
    using Server.Game.Data.Utils;
    using Server.Game.Data.XML;
    using Server.Game.Network.ServerPacket;
    using System;

    public static class RoomPing
    {
        public static void Load(SyncClientPacket C)
        {
            int id = C.ReadH();
            int num2 = C.ReadH();
            int slotIdx = C.ReadC();
            int num4 = C.ReadC();
            int num5 = C.ReadUH();
            if (C.ToArray().Length > 12)
            {
                CLogger.Print($"Invalid Ping (Length > 12): {C.ToArray().Length}", LoggerType.Warning, null);
            }
            ChannelModel channel = ChannelsXML.GetChannel(C.ReadH(), num2);
            if (channel != null)
            {
                RoomModel room = channel.GetRoom(id);
                if ((room != null) && (!room.RoundTime.IsTimer() && (room.State == RoomState.BATTLE)))
                {
                    SlotModel slot = room.GetSlot(slotIdx);
                    if ((slot != null) && (slot.State == SlotState.BATTLE))
                    {
                        Account playerBySlot = room.GetPlayerBySlot(slot);
                        if (!room.IsBotMode() && (playerBySlot != null))
                        {
                            slot.Latency = num5;
                            slot.Ping = num4;
                            slot.FailLatencyTimes = (slot.Latency < ConfigLoader.MaxLatency) ? 0 : (slot.FailLatencyTimes + 1);
                            if (ConfigLoader.IsDebugPing && (ComDiv.GetDuration(playerBySlot.LastPingDebug) >= ConfigLoader.PingUpdateTimeSeconds))
                            {
                                playerBySlot.LastPingDebug = DateTimeUtil.Now();
                                playerBySlot.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK("Server", 0, 5, false, $"{num5}ms ({num4} bar)"));
                            }
                            if (slot.FailLatencyTimes >= ConfigLoader.MaxRepeatLatency)
                            {
                                CLogger.Print($"Player: '{playerBySlot.Nickname}' (Id: {slot.PlayerId}) kicked due to high latency. ({slot.Latency}/{ConfigLoader.MaxLatency}ms)", LoggerType.Warning, null);
                                playerBySlot.Connection.Close(500, true, false);
                            }
                            else
                            {
                                AllUtils.RoomPingSync(room);
                            }
                        }
                    }
                }
            }
        }
    }
}

