namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Utility;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ : GameClientPacket
    {
        private int int_0;
        private List<RoomModel> list_0 = new List<RoomModel>();
        private List<QuickstartModel> list_1 = new List<QuickstartModel>();
        private QuickstartModel quickstartModel_0;

        public override void Read()
        {
            this.int_0 = base.ReadC();
            for (int i = 0; i < 3; i++)
            {
                QuickstartModel model1 = new QuickstartModel();
                model1.MapId = base.ReadC();
                model1.Rule = base.ReadC();
                model1.StageOptions = base.ReadC();
                model1.Type = base.ReadC();
                QuickstartModel item = model1;
                this.list_1.Add(item);
            }
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    ChannelModel model;
                    player.Quickstart.Quickjoins[this.int_0] = this.list_1[this.int_0];
                    string[] cOLUMNS = new string[] { $"list{this.int_0}_map_id", $"list{this.int_0}_map_rule", $"list{this.int_0}_map_stage", $"list{this.int_0}_map_type" };
                    object[] vALUES = new object[] { this.list_1[this.int_0].MapId, this.list_1[this.int_0].Rule, this.list_1[this.int_0].StageOptions, this.list_1[this.int_0].Type };
                    ComDiv.UpdateDB("player_quickstarts", "owner_id", player.PlayerId, cOLUMNS, vALUES);
                    if ((player.Nickname.Length > 0) && ((player.Room == null) && ((player.Match == null) && player.GetChannel(out model))))
                    {
                        List<RoomModel> rooms = model.Rooms;
                        lock (rooms)
                        {
                            foreach (RoomModel model2 in model.Rooms)
                            {
                                if (model2.RoomType != RoomCondition.Tutorial)
                                {
                                    this.quickstartModel_0 = this.list_1[this.int_0];
                                    if ((this.quickstartModel_0.MapId == model2.MapId) && ((this.quickstartModel_0.Rule == ((int) model2.Rule)) && ((this.quickstartModel_0.StageOptions == ((int) model2.Stage)) && ((this.quickstartModel_0.Type == model2.RoomType) && ((model2.Password.Length == 0) && ((model2.Limit == 0) && (!model2.KickedPlayersVote.Contains(player.PlayerId) || player.IsGM())))))))
                                    {
                                        foreach (SlotModel model3 in model2.Slots)
                                        {
                                            if ((model3.PlayerId == 0) && (model3.State == SlotState.EMPTY))
                                            {
                                                this.list_0.Add(model2);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    return;
                }
                if (this.list_0.Count == 0)
                {
                    base.Client.SendPacket(new PROTOCOL_LOBBY_QUICKJOIN_ROOM_ACK(0x80000000, this.list_1, null, null));
                }
                else
                {
                    RoomModel model4 = this.list_0[new Random().Next(this.list_0.Count)];
                    if ((model4 == null) || ((model4.GetLeader() == null) || (model4.AddPlayer(player) < 0)))
                    {
                        base.Client.SendPacket(new PROTOCOL_LOBBY_QUICKJOIN_ROOM_ACK(0x80000000, null, null, null));
                    }
                    else
                    {
                        player.ResetPages();
                        using (PROTOCOL_ROOM_GET_SLOTONEINFO_ACK protocol_room_get_slotoneinfo_ack = new PROTOCOL_ROOM_GET_SLOTONEINFO_ACK(player))
                        {
                            model4.SendPacketToPlayers(protocol_room_get_slotoneinfo_ack, player.PlayerId);
                        }
                        model4.UpdateSlotsInfo();
                        base.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(0, player));
                        base.Client.SendPacket(new PROTOCOL_LOBBY_QUICKJOIN_ROOM_ACK(0, this.list_1, model4, this.quickstartModel_0));
                    }
                }
                this.list_0 = null;
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

