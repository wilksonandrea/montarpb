namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.SQL;
    using Plugin.Core.Utility;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_CS_INVITE_ACCEPT_REQ : GameClientPacket
    {
        private int int_0;
        private int int_1;

        private MessageModel method_0(ClanModel clanModel_0, string string_0, long long_0)
        {
            MessageModel model1 = new MessageModel(15.0);
            model1.SenderName = clanModel_0.Name;
            model1.SenderId = long_0;
            model1.ClanId = clanModel_0.Id;
            model1.Type = NoteMessageType.Clan;
            model1.Text = string_0;
            model1.State = NoteMessageState.Unreaded;
            model1.ClanNote = (this.int_1 == 0) ? NoteMessageClan.JoinDenial : NoteMessageClan.JoinAccept;
            MessageModel message = model1;
            return (DaoManagerSQL.CreateMessage(clanModel_0.OwnerId, message) ? message : null);
        }

        public override void Read()
        {
            this.int_0 = base.ReadD();
            this.int_1 = base.ReadC();
        }

        public override void Run()
        {
            Account player = base.Client.Player;
            if ((player != null) && (player.Nickname.Length != 0))
            {
                ClanModel clan = ClanManager.GetClan(this.int_0);
                List<Account> players = ClanManager.GetClanPlayers(this.int_0, -1L, true);
                if (clan.Id == 0)
                {
                    base.Client.SendPacket(new PROTOCOL_CS_INVITE_ACCEPT_ACK(0x8000105b));
                }
                else if (player.ClanId > 0)
                {
                    base.Client.SendPacket(new PROTOCOL_CS_INVITE_ACCEPT_ACK(0x80001058));
                }
                else if (clan.MaxPlayers <= players.Count)
                {
                    base.Client.SendPacket(new PROTOCOL_CS_INVITE_ACCEPT_ACK(0x80001056));
                }
                else if ((this.int_1 == 0) || (this.int_1 == 1))
                {
                    try
                    {
                        uint num = 0;
                        Account account = AccountManager.GetAccount(clan.OwnerId, 0x1f);
                        if (account == null)
                        {
                            num = 0x80000000;
                        }
                        else
                        {
                            if (DaoManagerSQL.GetMessagesCount(clan.OwnerId) < 100)
                            {
                                MessageModel model2 = this.method_0(clan, player.Nickname, base.Client.PlayerId);
                                if ((model2 != null) && account.IsOnline)
                                {
                                    account.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(model2), false);
                                }
                            }
                            if (this.int_1 == 1)
                            {
                                uint num2 = uint.Parse(DateTimeUtil.Now("yyyyMMdd"));
                                string[] cOLUMNS = new string[] { "clan_id", "clan_access", "clan_date" };
                                object[] vALUES = new object[] { clan.Id, 3, num2 };
                                if (!ComDiv.UpdateDB("accounts", "player_id", player.PlayerId, cOLUMNS, vALUES))
                                {
                                    num = 0x80000000;
                                }
                                else
                                {
                                    using (PROTOCOL_CS_MEMBER_INFO_INSERT_ACK protocol_cs_member_info_insert_ack = new PROTOCOL_CS_MEMBER_INFO_INSERT_ACK(player))
                                    {
                                        ClanManager.SendPacket(protocol_cs_member_info_insert_ack, players);
                                    }
                                    player.ClanId = clan.Id;
                                    player.ClanDate = num2;
                                    player.ClanAccess = 3;
                                    base.Client.SendPacket(new PROTOCOL_CS_MEMBER_INFO_ACK(players));
                                    RoomModel room = player.Room;
                                    if (room != null)
                                    {
                                        room.SendPacketToPlayers(new PROTOCOL_ROOM_GET_SLOTONEINFO_ACK(player, clan));
                                    }
                                    base.Client.SendPacket(new PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK(clan, account, players.Count + 1));
                                }
                            }
                        }
                        base.Client.SendPacket(new PROTOCOL_MESSENGER_NOTE_SEND_ACK(num));
                    }
                    catch (Exception exception)
                    {
                        CLogger.Print(exception.Message, LoggerType.Error, exception);
                    }
                }
            }
        }
    }
}

