namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Utility;
    using Server.Game.Data.Models;
    using Server.Game.Data.Utils;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_BATTLE_PRESTARTBATTLE_REQ : GameClientPacket
    {
        private StageOptions stageOptions_0;
        private MapRules mapRules_0;
        private MapIdEnum mapIdEnum_0;
        private RoomCondition roomCondition_0;

        public override void Read()
        {
            this.mapIdEnum_0 = (MapIdEnum) base.ReadC();
            this.mapRules_0 = (MapRules) base.ReadC();
            this.stageOptions_0 = (StageOptions) base.ReadC();
            this.roomCondition_0 = (RoomCondition) base.ReadC();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    RoomModel room = player.Room;
                    if (room != null)
                    {
                        if ((room.Stage != this.stageOptions_0) || ((room.RoomType != this.roomCondition_0) || ((room.MapId != this.mapIdEnum_0) || (room.Rule != this.mapRules_0))))
                        {
                            base.Client.SendPacket(new PROTOCOL_SERVER_MESSAGE_KICK_BATTLE_PLAYER_ACK((EventErrorEnum) (-2147479542)));
                            base.Client.SendPacket(new PROTOCOL_BATTLE_PRESTARTBATTLE_ACK());
                            room.ChangeSlotState(player.SlotId, SlotState.NORMAL, true);
                            AllUtils.BattleEndPlayersCount(room, room.IsBotMode());
                        }
                        else
                        {
                            SlotModel slot = room.GetSlot(player.SlotId);
                            if ((slot == null) || (!room.IsPreparing() || ((room.UdpServer == null) || (slot.State < SlotState.LOAD))))
                            {
                                base.Client.SendPacket(new PROTOCOL_BATTLE_STARTBATTLE_ACK());
                                room.ChangeSlotState(slot, SlotState.NORMAL, true);
                                AllUtils.BattleEndPlayersCount(room, room.IsBotMode());
                                slot.StopTiming();
                            }
                            else
                            {
                                Account leader = room.GetLeader();
                                if (leader == null)
                                {
                                    base.Client.SendPacket(new PROTOCOL_SERVER_MESSAGE_KICK_BATTLE_PLAYER_ACK((EventErrorEnum) (-2147479541)));
                                    base.Client.SendPacket(new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(player, 0));
                                    room.ChangeSlotState(slot, SlotState.NORMAL, true);
                                    AllUtils.BattleEndPlayersCount(room, room.IsBotMode());
                                    slot.StopTiming();
                                }
                                else if (string.IsNullOrEmpty(player.PublicIP.ToString()))
                                {
                                    base.Client.SendPacket(new PROTOCOL_SERVER_MESSAGE_KICK_BATTLE_PLAYER_ACK((EventErrorEnum) (-2147479544)));
                                    base.Client.SendPacket(new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(player, 0));
                                    room.ChangeSlotState(slot, SlotState.NORMAL, true);
                                    AllUtils.BattleEndPlayersCount(room, room.IsBotMode());
                                    slot.StopTiming();
                                }
                                else
                                {
                                    slot.PreStartDate = DateTimeUtil.Now();
                                    if (slot.Id == room.LeaderSlot)
                                    {
                                        room.State = RoomState.PRE_BATTLE;
                                        room.UpdateRoomInfo();
                                    }
                                    room.ChangeSlotState(slot, SlotState.PRESTART, true);
                                    base.Client.SendPacket(new PROTOCOL_BATTLE_PRESTARTBATTLE_ACK(player, true));
                                    if (slot.Id != room.LeaderSlot)
                                    {
                                        leader.SendPacket(new PROTOCOL_BATTLE_PRESTARTBATTLE_ACK(player, false));
                                    }
                                    room.StartCounter(1, player, slot);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_BATTLE_PRESTARTBATTLE_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

