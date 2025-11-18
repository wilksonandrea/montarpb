namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.XML;
    using Server.Game.Data.Models;
    using Server.Game.Data.Utils;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_BATTLE_READYBATTLE_REQ : GameClientPacket
    {
        private ViewerType viewerType_0;

        public override void Read()
        {
            this.viewerType_0 = (ViewerType) base.ReadC();
            base.ReadD();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    ChannelModel model2;
                    SlotModel model3;
                    RoomModel room = player.Room;
                    if (((room != null) && ((room.GetLeader() != null) && room.GetChannel(out model2))) && room.GetSlot(player.SlotId, out model3))
                    {
                        if (model3.Equipment == null)
                        {
                            base.Client.SendPacket(new PROTOCOL_BATTLE_READYBATTLE_ACK(0x800010ab));
                        }
                        else
                        {
                            MapMatch mapLimit = SystemMapXML.GetMapLimit((int) room.MapId, (int) room.Rule);
                            if (mapLimit != null)
                            {
                                bool flag = room.IsBotMode();
                                if (model3.ViewType != this.viewerType_0)
                                {
                                    model3.ViewType = this.viewerType_0;
                                }
                                model3.SpecGM = ((model3.ViewType != ViewerType.SpecGM) || !player.IsGM()) ? ((room.RoomType == RoomCondition.Ace) && ((model3.Id < 0) || (model3.Id > 1))) : true;
                                if (flag || (!ConfigLoader.TournamentRule || !AllUtils.ClassicModeCheck(player, room)))
                                {
                                    int totalEnemys = 0;
                                    int fRPlayers = 0;
                                    int cTPlayers = 0;
                                    AllUtils.GetReadyPlayers(room, ref fRPlayers, ref cTPlayers, ref totalEnemys);
                                    if (room.LeaderSlot != player.SlotId)
                                    {
                                        if ((room.Slots[room.LeaderSlot].State < SlotState.LOAD) || !room.IsPreparing())
                                        {
                                            if (model3.State == SlotState.NORMAL)
                                            {
                                                room.ChangeSlotState(model3, SlotState.READY, true);
                                            }
                                            else if (model3.State == SlotState.READY)
                                            {
                                                room.ChangeSlotState(model3, SlotState.NORMAL, false);
                                                if ((room.State == RoomState.COUNTDOWN) && (room.GetPlayingPlayers((TeamEnum) ((room.LeaderSlot % 2) == 0), SlotState.READY, 0) == 0))
                                                {
                                                    room.ChangeSlotState(room.LeaderSlot, SlotState.NORMAL, false);
                                                    room.StopCountDown(CountDownEnum.StopByPlayer, true);
                                                }
                                                room.UpdateSlotsInfo();
                                            }
                                        }
                                        else if (model3.State == SlotState.NORMAL)
                                        {
                                            if ((mapLimit.Limit == 8) && AllUtils.Check4vs4(room, false, ref fRPlayers, ref fRPlayers, ref totalEnemys))
                                            {
                                                base.Client.SendPacket(new PROTOCOL_ROOM_UNREADY_4VS4_ACK());
                                            }
                                            else
                                            {
                                                if ((room.BalanceType != TeamBalance.None) && !flag)
                                                {
                                                    AllUtils.TryBalancePlayer(room, player, true, ref model3);
                                                }
                                                room.ChangeSlotState(model3, SlotState.LOAD, true);
                                                model3.SetMissionsClone(player.Mission);
                                                base.Client.SendPacket(new PROTOCOL_BATTLE_READYBATTLE_ACK((uint) model3.State));
                                                base.Client.SendPacket(new PROTOCOL_BATTLE_START_GAME_ACK(room));
                                                using (PROTOCOL_BATTLE_START_GAME_TRANS_ACK protocol_battle_start_game_trans_ack = new PROTOCOL_BATTLE_START_GAME_TRANS_ACK(room, model3, player.Title))
                                                {
                                                    room.SendPacketToPlayers(protocol_battle_start_game_trans_ack, SlotState.READY, 1, model3.Id);
                                                }
                                            }
                                        }
                                    }
                                    else if ((room.State == RoomState.READY) || (room.State == RoomState.COUNTDOWN))
                                    {
                                        if ((mapLimit.Limit == 8) && AllUtils.Check4vs4(room, true, ref fRPlayers, ref cTPlayers, ref totalEnemys))
                                        {
                                            base.Client.SendPacket(new PROTOCOL_ROOM_UNREADY_4VS4_ACK());
                                        }
                                        else
                                        {
                                            uint num4;
                                            if (AllUtils.ClanMatchCheck(room, model2.Type, totalEnemys, out num4))
                                            {
                                                base.Client.SendPacket(new PROTOCOL_BATTLE_READYBATTLE_ACK(num4));
                                            }
                                            else
                                            {
                                                uint num5;
                                                if (AllUtils.CompetitiveMatchCheck(player, room, out num5))
                                                {
                                                    base.Client.SendPacket(new PROTOCOL_BATTLE_READYBATTLE_ACK(num5));
                                                }
                                                else if ((totalEnemys == 0) && (flag || (room.RoomType == RoomCondition.Tutorial)))
                                                {
                                                    room.ChangeSlotState(model3, SlotState.READY, false);
                                                    room.StartBattle(false);
                                                    room.UpdateSlotsInfo();
                                                }
                                                else if (flag || (totalEnemys <= 0))
                                                {
                                                    if ((totalEnemys == 0) && !flag)
                                                    {
                                                        base.Client.SendPacket(new PROTOCOL_BATTLE_READYBATTLE_ACK(0x80001009));
                                                    }
                                                }
                                                else
                                                {
                                                    room.ChangeSlotState(model3, SlotState.READY, false);
                                                    if (room.BalanceType != TeamBalance.None)
                                                    {
                                                        AllUtils.TryBalanceTeams(room);
                                                    }
                                                    if (!room.ThisModeHaveCD())
                                                    {
                                                        room.StartBattle(false);
                                                    }
                                                    else if (room.State != RoomState.READY)
                                                    {
                                                        if (room.State == RoomState.COUNTDOWN)
                                                        {
                                                            room.ChangeSlotState(room.LeaderSlot, SlotState.NORMAL, false);
                                                            room.StopCountDown(CountDownEnum.StopByHost, true);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        SlotModel[] modelArray = new SlotModel[] { room.GetSlot(0), room.GetSlot(1) };
                                                        if ((room.RoomType == RoomCondition.Ace) && ((modelArray[0].State != SlotState.READY) || (modelArray[1].State != SlotState.READY)))
                                                        {
                                                            base.Client.SendPacket(new PROTOCOL_BATTLE_READYBATTLE_ACK(0x80001009));
                                                            room.ChangeSlotState(room.LeaderSlot, SlotState.NORMAL, false);
                                                            room.StopCountDown(CountDownEnum.StopByHost, true);
                                                        }
                                                        else
                                                        {
                                                            room.State = RoomState.COUNTDOWN;
                                                            room.UpdateRoomInfo();
                                                            room.StartCountDown();
                                                        }
                                                    }
                                                    room.UpdateSlotsInfo();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_BATTLE_READYBATTLE_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

