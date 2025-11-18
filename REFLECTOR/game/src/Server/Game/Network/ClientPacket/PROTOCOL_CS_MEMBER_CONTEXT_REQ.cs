namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.SQL;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_CS_MEMBER_CONTEXT_REQ : GameClientPacket
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
                    int num2;
                    int clanPlayers;
                    int clanId = (player.ClanId == 0) ? player.FindClanId : player.ClanId;
                    if (clanId == 0)
                    {
                        num2 = -1;
                        clanPlayers = 0;
                    }
                    else
                    {
                        num2 = 0;
                        clanPlayers = DaoManagerSQL.GetClanPlayers(clanId);
                    }
                    base.Client.SendPacket(new PROTOCOL_CS_MEMBER_CONTEXT_ACK(num2, clanPlayers));
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

