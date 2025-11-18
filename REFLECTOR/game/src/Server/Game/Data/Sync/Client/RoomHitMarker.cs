namespace Server.Game.Data.Sync.Client
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Server.Game.Data.Models;
    using Server.Game.Data.XML;
    using Server.Game.Network.ServerPacket;
    using System;

    public static class RoomHitMarker
    {
        public static void Load(SyncClientPacket C)
        {
            int id = C.ReadH();
            int num2 = C.ReadH();
            byte slotId = C.ReadC();
            byte num4 = C.ReadC();
            byte num5 = C.ReadC();
            int num6 = C.ReadD();
            if (C.ToArray().Length > 15)
            {
                CLogger.Print($"Invalid Hit (Length > 15): {C.ToArray().Length}", LoggerType.Warning, null);
            }
            ChannelModel channel = ChannelsXML.GetChannel(C.ReadH(), num2);
            if (channel != null)
            {
                RoomModel room = channel.GetRoom(id);
                if ((room != null) && (room.State == RoomState.BATTLE))
                {
                    Account playerBySlot = room.GetPlayerBySlot(slotId);
                    if (playerBySlot != null)
                    {
                        string label = "";
                        if (num4 == 10)
                        {
                            object[] argumens = new object[] { num6 };
                            label = Translation.GetLabel("LifeRestored", argumens);
                        }
                        switch (num5)
                        {
                            case 0:
                            {
                                object[] argumens = new object[] { num6 };
                                label = Translation.GetLabel("HitMarker1", argumens);
                                break;
                            }
                            case 1:
                            {
                                object[] argumens = new object[] { num6 };
                                label = Translation.GetLabel("HitMarker2", argumens);
                                break;
                            }
                            case 2:
                                label = Translation.GetLabel("HitMarker3");
                                break;

                            case 3:
                                label = Translation.GetLabel("HitMarker4");
                                break;

                            default:
                                break;
                        }
                        playerBySlot.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK(Translation.GetLabel("HitMarkerName"), playerBySlot.GetSessionId(), 0, true, label));
                    }
                }
            }
        }
    }
}

