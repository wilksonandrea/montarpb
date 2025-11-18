namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Server.Game.Data.Models;
    using Server.Game.Data.Utils;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_BATTLE_STARTBATTLE_REQ : GameClientPacket
    {
        public override void Read()
        {
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    ChannelModel model2;
                    RoomModel room = player.Room;
                    if (((room != null) && (room.GetLeader() != null)) && room.GetChannel(out model2))
                    {
                        if (!room.IsPreparing())
                        {
                            base.Client.SendPacket(new PROTOCOL_SERVER_MESSAGE_KICK_BATTLE_PLAYER_ACK((EventErrorEnum) (-2147479541)));
                            base.Client.SendPacket(new PROTOCOL_BATTLE_STARTBATTLE_ACK());
                            room.ChangeSlotState(player.SlotId, SlotState.NORMAL, true);
                        }
                        else
                        {
                            bool flag = room.IsBotMode();
                            SlotModel slot = room.GetSlot(player.SlotId);
                            if ((slot == null) || (slot.State != SlotState.PRESTART))
                            {
                                base.Client.SendPacket(new PROTOCOL_SERVER_MESSAGE_KICK_BATTLE_PLAYER_ACK((EventErrorEnum) (-2147479541)));
                                base.Client.SendPacket(new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(player, 0));
                                room.ChangeSlotState(slot, SlotState.NORMAL, true);
                                AllUtils.BattleEndPlayersCount(room, flag);
                            }
                            else
                            {
                                room.ChangeSlotState(slot, SlotState.BATTLE_READY, true);
                                slot.StopTiming();
                                if (flag)
                                {
                                    base.Client.SendPacket(new PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_ACK(room));
                                }
                                base.Client.SendPacket(new PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK(room, flag));
                                int num = 0;
                                int num2 = 0;
                                int num3 = 0;
                                int num4 = 0;
                                int num5 = 0;
                                int num6 = 0;
                                SlotModel[] slots = room.Slots;
                                int index = 0;
                                while (true)
                                {
                                    if (index >= slots.Length)
                                    {
                                        bool flag2 = ((room.GetSlot(room.LeaderSlot).State >= SlotState.BATTLE_READY) & flag) && ((((room.LeaderSlot % 2) != 0) || (num3 <= (num5 / 2))) ? (((room.LeaderSlot % 2) == 1) && (num2 > (num6 / 2))) : true);
                                        bool flag3 = (room.GetSlot(room.LeaderSlot).State >= SlotState.BATTLE_READY) && ((num2 > (num6 / 2)) && (num3 > (num5 / 2)));
                                        bool flag4 = ((room.GetSlot(room.LeaderSlot).State >= SlotState.BATTLE_READY) && (room.RoomType == RoomCondition.FreeForAll)) && ((num >= 2) && (num4 >= 2));
                                        bool flag5 = (model2.Type == ChannelType.Clan) && (num == (num3 + num2));
                                        bool flag6 = room.Competitive && (num == (num3 + num2));
                                        if ((((((room.State == RoomState.BATTLE) | flag2) | flag3) | flag4) | flag5) | flag6)
                                        {
                                            if (flag5)
                                            {
                                                CLogger.Print($"Starting Clan War Match with '{num}' players. FR: {num3} CT: {num2}", LoggerType.Warning, null);
                                            }
                                            if (flag6)
                                            {
                                                CLogger.Print($"Starting Competitive Match with '{num}' players. FR: {num3} CT: {num2}", LoggerType.Warning, null);
                                            }
                                            room.SpawnReadyPlayers();
                                        }
                                        break;
                                    }
                                    SlotModel model4 = slots[index];
                                    if (model4.State >= SlotState.LOAD)
                                    {
                                        num4++;
                                        if (model4.Team == TeamEnum.FR_TEAM)
                                        {
                                            num5++;
                                        }
                                        else
                                        {
                                            num6++;
                                        }
                                        if (model4.State >= SlotState.BATTLE_READY)
                                        {
                                            num++;
                                            if (model4.Team == TeamEnum.FR_TEAM)
                                            {
                                                num3++;
                                            }
                                            else
                                            {
                                                num2++;
                                            }
                                        }
                                    }
                                    index++;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_BATTLE_STARTBATTLE_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

