namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.SQL;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_CS_CLIENT_ENTER_REQ : GameClientPacket
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
                    RoomModel room = player.Room;
                    if (room != null)
                    {
                        room.ChangeSlotState(player.SlotId, SlotState.CLAN, false);
                        room.StopCountDown(player.SlotId);
                        room.UpdateSlotsInfo();
                    }
                    int requestClanId = 0;
                    ClanModel clan = ClanManager.GetClan(player.ClanId);
                    if ((player.ClanId == 0) && (player.Nickname.Length > 0))
                    {
                        requestClanId = DaoManagerSQL.GetRequestClanId(player.PlayerId);
                    }
                    base.Client.SendPacket(new PROTOCOL_CS_CLIENT_ENTER_ACK((requestClanId > 0) ? requestClanId : clan.Id, player.ClanAccess));
                    if ((clan.Id > 0) && (requestClanId == 0))
                    {
                        base.Client.SendPacket(new PROTOCOL_CS_DETAIL_INFO_ACK(0, clan));
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_CS_CLIENT_ENTER_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

