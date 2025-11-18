namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_REQ : GameClientPacket
    {
        private List<MatchModel> list_0 = new List<MatchModel>();
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
                    if (player.ClanId > 0)
                    {
                        ChannelModel channel = player.GetChannel();
                        if ((channel != null) && (channel.Type == ChannelType.Clan))
                        {
                            List<MatchModel> matches = channel.Matches;
                            lock (matches)
                            {
                                foreach (MatchModel model2 in channel.Matches)
                                {
                                    if (model2.Clan.Id == player.ClanId)
                                    {
                                        this.list_0.Add(model2);
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
                base.Client.SendPacket(new PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_ACK((player.ClanId == 0) ? 0x5b : 0, this.list_0));
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

