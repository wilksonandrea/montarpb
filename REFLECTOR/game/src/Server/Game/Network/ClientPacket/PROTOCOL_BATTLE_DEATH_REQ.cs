namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Server.Game.Data.Models;
    using Server.Game.Data.Sync.Client;
    using Server.Game.Data.Utils;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_BATTLE_DEATH_REQ : GameClientPacket
    {
        private FragInfos fragInfos_0;
        private bool bool_0;

        public override void Read()
        {
            FragInfos infos1 = new FragInfos();
            infos1.KillingType = (CharaKillType) base.ReadC();
            infos1.KillsCount = base.ReadC();
            infos1.KillerSlot = base.ReadC();
            infos1.WeaponId = base.ReadD();
            infos1.X = base.ReadT();
            infos1.Y = base.ReadT();
            infos1.Z = base.ReadT();
            infos1.Flag = base.ReadC();
            infos1.Unk = base.ReadC();
            this.fragInfos_0 = infos1;
            for (int i = 0; i < this.fragInfos_0.KillsCount; i++)
            {
                FragModel model1 = new FragModel();
                model1.VictimSlot = base.ReadC();
                model1.WeaponClass = base.ReadC();
                model1.HitspotInfo = base.ReadC();
                model1.KillFlag = (KillingMessage) base.ReadH();
                model1.Unk = base.ReadC();
                model1.X = base.ReadT();
                model1.Y = base.ReadT();
                model1.Z = base.ReadT();
                model1.AssistSlot = base.ReadC();
                model1.Unks = base.ReadB(8);
                FragModel item = model1;
                this.fragInfos_0.Frags.Add(item);
                if (item.VictimSlot == this.fragInfos_0.KillerSlot)
                {
                    this.bool_0 = true;
                }
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
                    if (((room != null) && !room.RoundTime.IsTimer()) && (room.State >= RoomState.BATTLE))
                    {
                        bool flag = room.IsBotMode();
                        SlotModel slot = room.GetSlot(this.fragInfos_0.KillerSlot);
                        if ((slot != null) && (flag || ((slot.State >= SlotState.BATTLE) && (slot.Id == player.SlotId))))
                        {
                            int num;
                            RoomDeath.RegistryFragInfos(room, slot, out num, flag, this.bool_0, this.fragInfos_0);
                            if (!flag)
                            {
                                slot.Score += num;
                                AllUtils.CompleteMission(room, player, slot, this.fragInfos_0, MissionType.NA, 0);
                                this.fragInfos_0.Score = num;
                            }
                            else
                            {
                                slot.Score += (slot.KillsOnLife + room.IngameAiLevel) + num;
                                if (slot.Score > 0xffff)
                                {
                                    slot.Score = 0xffff;
                                    AllUtils.ValidateBanPlayer(player, $"AI Score Cheating! ({slot.Score})");
                                }
                                this.fragInfos_0.Score = slot.Score;
                            }
                            using (PROTOCOL_BATTLE_DEATH_ACK protocol_battle_death_ack = new PROTOCOL_BATTLE_DEATH_ACK(room, this.fragInfos_0, slot))
                            {
                                room.SendPacketToPlayers(protocol_battle_death_ack, SlotState.BATTLE, 0);
                            }
                            RoomDeath.EndBattleByDeath(room, slot, flag, this.bool_0, this.fragInfos_0);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_BATTLE_DEATH_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

