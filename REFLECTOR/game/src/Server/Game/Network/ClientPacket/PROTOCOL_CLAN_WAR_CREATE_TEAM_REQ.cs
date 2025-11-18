namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_CLAN_WAR_CREATE_TEAM_REQ : GameClientPacket
    {
        private int int_0;
        private List<int> list_0 = new List<int>();

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
                    ChannelModel channel = player.GetChannel();
                    if ((channel == null) || ((channel.Type != ChannelType.Clan) || (player.Room != null)))
                    {
                        base.Client.SendPacket(new PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK(0x80000000, null));
                    }
                    else if (player.Match != null)
                    {
                        base.Client.SendPacket(new PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK(0x80001087, null));
                    }
                    else if (player.ClanId == 0)
                    {
                        base.Client.SendPacket(new PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK(0x8000105b, null));
                    }
                    else
                    {
                        int num = -1;
                        int num2 = -1;
                        List<MatchModel> matches = channel.Matches;
                        lock (matches)
                        {
                            int id = 0;
                            while (true)
                            {
                                if (id < 250)
                                {
                                    if (channel.GetMatch(id) != null)
                                    {
                                        id++;
                                        continue;
                                    }
                                    num = id;
                                }
                                for (int i = 0; i < channel.Matches.Count; i++)
                                {
                                    MatchModel model2 = channel.Matches[i];
                                    if (model2.Clan.Id == player.ClanId)
                                    {
                                        this.list_0.Add(model2.FriendId);
                                    }
                                }
                                break;
                            }
                        }
                        int item = 0;
                        while (true)
                        {
                            if (item < 0x19)
                            {
                                if (this.list_0.Contains(item))
                                {
                                    item++;
                                    continue;
                                }
                                num2 = item;
                            }
                            if (num == -1)
                            {
                                base.Client.SendPacket(new PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK(0x80001088, null));
                            }
                            else if (num2 == -1)
                            {
                                base.Client.SendPacket(new PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK(0x80001089, null));
                            }
                            else
                            {
                                MatchModel model1 = new MatchModel(ClanManager.GetClan(player.ClanId));
                                model1.MatchId = num;
                                model1.FriendId = num2;
                                model1.Training = this.int_0;
                                model1.ChannelId = player.ChannelId;
                                model1.ServerId = player.ServerId;
                                MatchModel match = model1;
                                match.AddPlayer(player);
                                channel.AddMatch(match);
                                base.Client.SendPacket(new PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK(0, match));
                                base.Client.SendPacket(new PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK(match));
                            }
                            break;
                        }
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

