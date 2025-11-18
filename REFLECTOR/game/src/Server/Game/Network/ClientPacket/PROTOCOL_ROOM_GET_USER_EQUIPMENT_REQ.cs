namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_ROOM_GET_USER_EQUIPMENT_REQ : GameClientPacket
    {
        private int int_0;

        public override void Read()
        {
            this.int_0 = base.ReadC();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    SlotModel model2;
                    RoomModel room = player.Room;
                    if (((room != null) && (player.SlotId != this.int_0)) && room.GetSlot(this.int_0, out model2))
                    {
                        uint num = 0;
                        int[] numArray = new int[2];
                        PlayerEquipment equipment = model2.Equipment;
                        if (equipment == null)
                        {
                            num = 0x8000000;
                        }
                        else
                        {
                            TeamEnum enum2 = room.ValidateTeam(model2.Team, model2.CostumeTeam);
                            if (enum2 == TeamEnum.FR_TEAM)
                            {
                                numArray[0] = equipment.CharaRedId;
                            }
                            else if (enum2 == TeamEnum.CT_TEAM)
                            {
                                numArray[0] = equipment.CharaBlueId;
                            }
                            numArray[1] = equipment.AccessoryId;
                        }
                        base.Client.SendPacket(new PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK(num, equipment, numArray, (byte) model2.Id));
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_ROOM_GET_USER_EQUIPMENT_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

