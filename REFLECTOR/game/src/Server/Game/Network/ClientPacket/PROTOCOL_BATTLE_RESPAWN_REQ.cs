namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Utility;
    using Server.Game.Data.Models;
    using Server.Game.Data.Sync.Server;
    using Server.Game.Data.Utils;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_BATTLE_RESPAWN_REQ : GameClientPacket
    {
        private int[] int_0;
        private int int_1;

        public override void Read()
        {
            this.int_0 = new int[0x10];
            this.int_0[0] = base.ReadD();
            base.ReadUD();
            this.int_0[1] = base.ReadD();
            base.ReadUD();
            this.int_0[2] = base.ReadD();
            base.ReadUD();
            this.int_0[3] = base.ReadD();
            base.ReadUD();
            this.int_0[4] = base.ReadD();
            base.ReadUD();
            this.int_0[5] = base.ReadD();
            base.ReadUD();
            this.int_0[6] = base.ReadD();
            base.ReadUD();
            this.int_0[7] = base.ReadD();
            base.ReadUD();
            this.int_0[8] = base.ReadD();
            base.ReadUD();
            this.int_0[9] = base.ReadD();
            base.ReadUD();
            this.int_0[10] = base.ReadD();
            base.ReadUD();
            this.int_0[11] = base.ReadD();
            base.ReadUD();
            this.int_0[12] = base.ReadD();
            base.ReadUD();
            this.int_0[13] = base.ReadD();
            base.ReadUD();
            this.int_0[14] = base.ReadD();
            base.ReadUD();
            this.int_1 = base.ReadH();
            this.int_0[15] = base.ReadD();
            base.ReadUD();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    RoomModel room = player.Room;
                    if ((room != null) && (room.State == RoomState.BATTLE))
                    {
                        SlotModel slot = room.GetSlot(player.SlotId);
                        if ((slot != null) && (slot.State == SlotState.BATTLE))
                        {
                            if (slot.DeathState.HasFlag(DeadEnum.Dead) || slot.DeathState.HasFlag(DeadEnum.UseChat))
                            {
                                slot.DeathState = DeadEnum.Alive;
                            }
                            PlayerEquipment equip = AllUtils.ValidateRespawnEQ(slot, this.int_0);
                            if (equip != null)
                            {
                                ComDiv.CheckEquipedItems(equip, player.Inventory.Items, true);
                                AllUtils.ClassicModeCheck(room, equip);
                                slot.Equipment = equip;
                                if ((this.int_1 & 8) > 0)
                                {
                                    AllUtils.InsertItem(equip.WeaponPrimary, slot);
                                }
                                if ((this.int_1 & 4) > 0)
                                {
                                    AllUtils.InsertItem(equip.WeaponSecondary, slot);
                                }
                                if ((this.int_1 & 2) > 0)
                                {
                                    AllUtils.InsertItem(equip.WeaponMelee, slot);
                                }
                                if ((this.int_1 & 1) > 0)
                                {
                                    AllUtils.InsertItem(equip.WeaponExplosive, slot);
                                }
                                AllUtils.InsertItem(equip.WeaponSpecial, slot);
                                AllUtils.InsertItem(equip.PartHead, slot);
                                AllUtils.InsertItem(equip.PartFace, slot);
                                AllUtils.InsertItem(equip.BeretItem, slot);
                                AllUtils.InsertItem(equip.AccessoryId, slot);
                                int num = ComDiv.GetIdStatics(this.int_0[5], 1);
                                int num2 = ComDiv.GetIdStatics(this.int_0[5], 2);
                                int num3 = ComDiv.GetIdStatics(this.int_0[5], 5);
                                if (num != 6)
                                {
                                    if (num == 15)
                                    {
                                        AllUtils.InsertItem(equip.DinoItem, slot);
                                    }
                                }
                                else if ((num2 == 1) || (num3 == 0x278))
                                {
                                    AllUtils.InsertItem(equip.CharaRedId, slot);
                                }
                                else if ((num2 == 2) || (num3 == 0x298))
                                {
                                    AllUtils.InsertItem(equip.CharaBlueId, slot);
                                }
                            }
                            using (PROTOCOL_BATTLE_RESPAWN_ACK protocol_battle_respawn_ack = new PROTOCOL_BATTLE_RESPAWN_ACK(room, slot))
                            {
                                room.SendPacketToPlayers(protocol_battle_respawn_ack, SlotState.BATTLE, 0);
                            }
                            if (!slot.FirstRespawn)
                            {
                                EquipmentSync.SendUDPPlayerSync(room, slot, player.Effects, 2);
                            }
                            else
                            {
                                slot.FirstRespawn = false;
                                EquipmentSync.SendUDPPlayerSync(room, slot, player.Effects, 0);
                            }
                            if (room.WeaponsFlag != this.int_1)
                            {
                                CLogger.Print($"Player '{player.Nickname}' Weapon Flags Doesn't Match! (Room: {(int) room.WeaponsFlag}; Player: {this.int_1})", LoggerType.Warning, null);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_BATTLE_RESPAWN_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

