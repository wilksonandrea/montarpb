namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Server.Game.Data.Models;
    using Server.Game.Data.Sync.Client;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_REQ : GameClientPacket
    {
        private int int_0;

        public override void Read()
        {
            this.int_0 = base.ReadD();
        }

        public override void Run()
        {
            Account player = base.Client.Player;
            if (player != null)
            {
                RoomModel room = player.Room;
                if ((room != null) && (!room.RoundTime.IsTimer() && ((room.State == RoomState.BATTLE) && room.ActiveC4)))
                {
                    SlotModel slot = room.GetSlot(this.int_0);
                    if ((slot != null) && (slot.State == SlotState.BATTLE))
                    {
                        RoomBombC4.UninstallBomb(room, slot);
                    }
                }
            }
        }
    }
}

