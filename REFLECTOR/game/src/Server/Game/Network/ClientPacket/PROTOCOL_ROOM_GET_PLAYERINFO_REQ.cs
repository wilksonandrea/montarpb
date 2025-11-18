namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_ROOM_GET_PLAYERINFO_REQ : GameClientPacket
    {
        private int int_0;

        public override void Read()
        {
            this.int_0 = base.ReadD();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    Account account2;
                    RoomModel room = player.Room;
                    if (((room != null) && (player.SlotId != this.int_0)) && room.GetPlayerBySlot(this.int_0, out account2))
                    {
                        uint num = 0;
                        int[] numArray = new int[2];
                        SlotModel slot = room.GetSlot(account2.SlotId);
                        if (slot == null)
                        {
                            num = 0x8000000;
                        }
                        else
                        {
                            TeamEnum enum2 = room.ValidateTeam(slot.Team, slot.CostumeTeam);
                            if (enum2 == TeamEnum.FR_TEAM)
                            {
                                numArray[0] = slot.Equipment.CharaRedId;
                            }
                            else if (enum2 == TeamEnum.CT_TEAM)
                            {
                                numArray[0] = slot.Equipment.CharaBlueId;
                            }
                            numArray[1] = slot.Equipment.AccessoryId;
                        }
                        base.Client.SendPacket(new PROTOCOL_ROOM_GET_PLAYERINFO_ACK(num, account2, numArray));
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

