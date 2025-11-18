namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_BASE_GET_ROOM_USER_DETAIL_INFO_REQ : GameClientPacket
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
                    SlotModel model2;
                    RoomModel room = player.Room;
                    if ((room != null) && room.GetSlot(this.int_0, out model2))
                    {
                        Account playerBySlot = room.GetPlayerBySlot(model2);
                        if (playerBySlot != null)
                        {
                            if (player.Nickname != playerBySlot.Nickname)
                            {
                                player.FindPlayer = playerBySlot.Nickname;
                            }
                            int charaRedId = 0x7fffffff;
                            TeamEnum enum2 = room.ValidateTeam(model2.Team, model2.CostumeTeam);
                            if (enum2 == TeamEnum.FR_TEAM)
                            {
                                charaRedId = playerBySlot.Equipment.CharaRedId;
                            }
                            else if (enum2 == TeamEnum.CT_TEAM)
                            {
                                charaRedId = playerBySlot.Equipment.CharaBlueId;
                            }
                            base.Client.SendPacket(new PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK(0, playerBySlot, charaRedId));
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_BASE_GET_ROOM_USER_DETAIL_INFO_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

