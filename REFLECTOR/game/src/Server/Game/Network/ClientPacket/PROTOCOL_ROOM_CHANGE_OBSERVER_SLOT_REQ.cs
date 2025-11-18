namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_ROOM_CHANGE_OBSERVER_SLOT_REQ : GameClientPacket
    {
        private ViewerType viewerType_0;

        public override void Read()
        {
            this.viewerType_0 = (ViewerType) base.ReadC();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    RoomModel room = player.Room;
                    if ((room != null) && player.IsGM())
                    {
                        SlotModel slot = room.GetSlot(player.SlotId);
                        if (slot != null)
                        {
                            slot.ViewType = this.viewerType_0;
                            if (slot.ViewType == ViewerType.SpecGM)
                            {
                                slot.SpecGM = true;
                            }
                            base.Client.SendPacket(new PROTOCOL_ROOM_CHANGE_OBSERVER_SLOT_ACK(slot.Id));
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(base.GetType().Name + "; " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

