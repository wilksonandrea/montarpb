namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Utility;
    using Plugin.Core.XML;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_ROOM_LOADING_START_REQ : GameClientPacket
    {
        private string string_0;

        public override void Read()
        {
            this.string_0 = base.ReadS(base.ReadC());
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    SlotModel model2;
                    RoomModel room = player.Room;
                    if ((room != null) && (room.IsPreparing() && (room.GetSlot(player.SlotId, out model2) && (model2.State == SlotState.LOAD))))
                    {
                        model2.PreLoadDate = DateTimeUtil.Now();
                        room.StartCounter(0, player, model2);
                        room.ChangeSlotState(model2, SlotState.RENDEZVOUS, true);
                        room.MapName = this.string_0;
                        if (model2.Id == room.LeaderSlot)
                        {
                            room.UdpServer = SynchronizeXML.GetServer(ConfigLoader.DEFAULT_PORT[2]);
                            room.State = RoomState.RENDEZVOUS;
                            room.UpdateRoomInfo();
                        }
                    }
                    base.Client.SendPacket(new PROTOCOL_ROOM_LOADING_START_ACK(0));
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_ROOM_LOADING_START_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

