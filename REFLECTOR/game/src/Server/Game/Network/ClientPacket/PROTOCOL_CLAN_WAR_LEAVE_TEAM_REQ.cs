namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Models;
    using Server.Game.Data.Utils;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_CLAN_WAR_LEAVE_TEAM_REQ : GameClientPacket
    {
        private uint uint_0;

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
                    MatchModel match = player.Match;
                    if ((match == null) || !match.RemovePlayer(player))
                    {
                        this.uint_0 = 0x80000000;
                    }
                    base.Client.SendPacket(new PROTOCOL_CLAN_WAR_LEAVE_TEAM_ACK(this.uint_0));
                    if (this.uint_0 == 0)
                    {
                        player.Status.UpdateClanMatch(0xff);
                        AllUtils.SyncPlayerToClanMembers(player);
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

