namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Server.Game.Data.Models;
    using Server.Game.Data.Sync.Client;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_REQ : GameClientPacket
    {
        private int int_0;
        private float float_0;
        private float float_1;
        private float float_2;
        private byte byte_0;
        private int int_1;

        public override void Read()
        {
            this.int_0 = base.ReadD();
            this.byte_0 = base.ReadC();
            this.int_1 = base.ReadD();
            this.float_0 = base.ReadT();
            this.float_1 = base.ReadT();
            this.float_2 = base.ReadT();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    RoomModel room = player.Room;
                    if (((room != null) && (!room.RoundTime.IsTimer() && (room.State == RoomState.BATTLE))) && !room.ActiveC4)
                    {
                        SlotModel slot = room.GetSlot(this.int_0);
                        if ((slot != null) && (slot.State == SlotState.BATTLE))
                        {
                            RoomBombC4.InstallBomb(room, slot, this.byte_0, (this.int_1 == 0) ? ((ushort) 0x2a) : ((ushort) 0), this.float_0, this.float_1, this.float_2);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

