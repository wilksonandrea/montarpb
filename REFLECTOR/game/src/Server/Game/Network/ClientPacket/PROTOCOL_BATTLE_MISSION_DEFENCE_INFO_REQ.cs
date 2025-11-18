namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Server.Game.Data.Models;
    using Server.Game.Data.Sync.Client;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_REQ : GameClientPacket
    {
        private ushort ushort_0;
        private ushort ushort_1;
        private List<ushort> list_0 = new List<ushort>();
        private List<ushort> list_1 = new List<ushort>();

        public override void Read()
        {
            this.ushort_0 = base.ReadUH();
            this.ushort_1 = base.ReadUH();
            for (int i = 0; i < 0x12; i++)
            {
                this.list_0.Add(base.ReadUH());
            }
            for (int j = 0; j < 0x12; j++)
            {
                this.list_1.Add(base.ReadUH());
            }
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    RoomModel room = player.Room;
                    if (((room != null) && !room.RoundTime.IsTimer()) && (room.State == RoomState.BATTLE))
                    {
                        SlotModel slot = room.GetSlot(player.SlotId);
                        if ((slot != null) && (slot.State == SlotState.BATTLE))
                        {
                            room.Bar1 = this.ushort_0;
                            room.Bar2 = this.ushort_1;
                            int index = 0;
                            while (true)
                            {
                                if (index >= 0x12)
                                {
                                    using (PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_ACK protocol_battle_mission_defence_info_ack = new PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_ACK(room))
                                    {
                                        room.SendPacketToPlayers(protocol_battle_mission_defence_info_ack, SlotState.BATTLE, 0);
                                    }
                                    if ((this.ushort_0 == 0) && (this.ushort_1 == 0))
                                    {
                                        RoomSadeSync.EndRound(room, !room.SwapRound ? TeamEnum.FR_TEAM : TeamEnum.CT_TEAM);
                                    }
                                    break;
                                }
                                SlotModel model3 = room.Slots[index];
                                if ((model3.PlayerId > 0L) && (model3.State == SlotState.BATTLE))
                                {
                                    model3.DamageBar1 = this.list_0[index];
                                    model3.DamageBar2 = this.list_1[index];
                                }
                                index++;
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

