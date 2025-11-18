namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.SQL;
    using Plugin.Core.Utility;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Data.Sync.Server;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_CS_ACCEPT_REQUEST_REQ : GameClientPacket
    {
        private List<long> list_0 = new List<long>();
        private int int_0;

        private MessageModel method_0(ClanModel clanModel_0, long long_0, long long_1)
        {
            MessageModel model1 = new MessageModel(15.0);
            model1.SenderName = clanModel_0.Name;
            model1.SenderId = long_1;
            model1.ClanId = clanModel_0.Id;
            model1.Type = NoteMessageType.Clan;
            model1.State = NoteMessageState.Unreaded;
            model1.ClanNote = NoteMessageClan.InviteAccept;
            MessageModel message = model1;
            return (DaoManagerSQL.CreateMessage(long_0, message) ? message : null);
        }

        public override void Read()
        {
            int num = base.ReadC();
            for (int i = 0; i < num; i++)
            {
                long item = base.ReadQ();
                this.list_0.Add(item);
            }
        }

        public override void Run()
        {
            try
            {
                ClanModel clan;
                List<Account> list;
                Account player = base.Client.Player;
                if (player != null)
                {
                    clan = ClanManager.GetClan(player.ClanId);
                    if ((clan.Id <= 0) || (((player.ClanAccess < 1) || (player.ClanAccess > 2)) && (player.PlayerId != clan.OwnerId)))
                    {
                        this.int_0 = -1;
                        goto TR_0003;
                    }
                }
                else
                {
                    return;
                }
                goto TR_001C;
            TR_0003:
                base.Client.SendPacket(new PROTOCOL_CS_ACCEPT_REQUEST_ACK((uint) this.int_0));
                return;
            TR_001C:
                list = ClanManager.GetClanPlayers(clan.Id, -1L, true);
                if (list.Count < clan.MaxPlayers)
                {
                    int num = 0;
                    while (true)
                    {
                        if (num >= this.list_0.Count)
                        {
                            list = null;
                            break;
                        }
                        Account account2 = AccountManager.GetAccount(this.list_0[num], 0x1f);
                        if ((account2 != null) && ((list.Count < clan.MaxPlayers) && ((account2.ClanId == 0) && (DaoManagerSQL.GetRequestClanId(account2.PlayerId) > 0))))
                        {
                            using (PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK protocol_cs_member_info_change_ack = new PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK(account2))
                            {
                                ClanManager.SendPacket(protocol_cs_member_info_change_ack, list);
                            }
                            account2.ClanId = player.ClanId;
                            account2.ClanDate = uint.Parse(DateTimeUtil.Now("yyyyMMdd"));
                            account2.ClanAccess = 3;
                            SendClanInfo.Load(account2, null, 3);
                            string[] cOLUMNS = new string[] { "clan_access", "clan_id", "clan_date" };
                            object[] vALUES = new object[] { account2.ClanAccess, account2.ClanId, account2.ClanDate };
                            ComDiv.UpdateDB("accounts", "player_id", account2.PlayerId, cOLUMNS, vALUES);
                            DaoManagerSQL.DeleteClanInviteDB(player.ClanId, account2.PlayerId);
                            if (account2.IsOnline)
                            {
                                account2.SendPacket(new PROTOCOL_CS_MEMBER_INFO_ACK(list), false);
                                RoomModel room = account2.Room;
                                if (room != null)
                                {
                                    room.SendPacketToPlayers(new PROTOCOL_ROOM_GET_SLOTONEINFO_ACK(account2, clan));
                                }
                                account2.SendPacket(new PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK(clan, list.Count + 1), false);
                            }
                            if (DaoManagerSQL.GetMessagesCount(account2.PlayerId) < 100)
                            {
                                MessageModel model3 = this.method_0(clan, account2.PlayerId, base.Client.PlayerId);
                                if ((model3 != null) && account2.IsOnline)
                                {
                                    account2.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(model3), false);
                                }
                            }
                            this.int_0++;
                            list.Add(account2);
                        }
                        num++;
                    }
                }
                else
                {
                    this.int_0 = -1;
                    return;
                }
                goto TR_0003;
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_CS_ACCEPT_REQUEST_RESULT_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

