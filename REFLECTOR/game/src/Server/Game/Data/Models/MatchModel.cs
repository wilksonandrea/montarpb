namespace Server.Game.Data.Models
{
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Utils;
    using Server.Game.Data.XML;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class MatchModel
    {
        public MatchModel(ClanModel clanModel_1)
        {
            this.Clan = clanModel_1;
            this.MatchId = -1;
            this.Slots = new SlotMatch[9];
            this.State = MatchState.Ready;
            for (int i = 0; i < 9; i++)
            {
                this.Slots[i] = new SlotMatch(i);
            }
        }

        public bool AddPlayer(Account Player)
        {
            bool flag2;
            SlotMatch[] slots = this.Slots;
            lock (slots)
            {
                int index = 0;
                while (true)
                {
                    if (index < this.Training)
                    {
                        SlotMatch match = this.Slots[index];
                        if ((match.PlayerId != 0) || (match.State != SlotMatchState.Empty))
                        {
                            index++;
                            continue;
                        }
                        match.PlayerId = Player.PlayerId;
                        match.State = SlotMatchState.Normal;
                        Player.Match = this;
                        Player.MatchSlot = index;
                        Player.Status.UpdateClanMatch((byte) this.FriendId);
                        AllUtils.SyncPlayerToClanMembers(Player);
                        flag2 = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag2;
        }

        public List<Account> GetAllPlayers()
        {
            List<Account> list = new List<Account>();
            SlotMatch[] slots = this.Slots;
            lock (slots)
            {
                for (int i = 0; i < 9; i++)
                {
                    long playerId = this.Slots[i].PlayerId;
                    if (playerId > 0L)
                    {
                        Account item = AccountManager.GetAccount(playerId, true);
                        if (item != null)
                        {
                            list.Add(item);
                        }
                    }
                }
            }
            return list;
        }

        public List<Account> GetAllPlayers(int Exception)
        {
            List<Account> list = new List<Account>();
            SlotMatch[] slots = this.Slots;
            lock (slots)
            {
                for (int i = 0; i < 9; i++)
                {
                    long playerId = this.Slots[i].PlayerId;
                    if ((playerId > 0L) && (i != Exception))
                    {
                        Account item = AccountManager.GetAccount(playerId, true);
                        if (item != null)
                        {
                            list.Add(item);
                        }
                    }
                }
            }
            return list;
        }

        public int GetCountPlayers()
        {
            int num2;
            SlotMatch[] slots = this.Slots;
            lock (slots)
            {
                int num = 0;
                SlotMatch[] matchArray2 = this.Slots;
                num2 = 0;
                while (true)
                {
                    if (num2 >= matchArray2.Length)
                    {
                        num2 = num;
                        break;
                    }
                    if (matchArray2[num2].PlayerId > 0L)
                    {
                        num++;
                    }
                    num2++;
                }
            }
            return num2;
        }

        public Account GetLeader()
        {
            try
            {
                return AccountManager.GetAccount(this.Slots[this.Leader].PlayerId, true);
            }
            catch
            {
                return null;
            }
        }

        public Account GetPlayerBySlot(SlotMatch Slot)
        {
            try
            {
                long playerId = Slot.PlayerId;
                return ((playerId > 0L) ? AccountManager.GetAccount(playerId, true) : null);
            }
            catch
            {
                return null;
            }
        }

        public Account GetPlayerBySlot(int SlotId)
        {
            try
            {
                long playerId = this.Slots[SlotId].PlayerId;
                return ((playerId > 0L) ? AccountManager.GetAccount(playerId, true) : null);
            }
            catch
            {
                return null;
            }
        }

        public int GetServerInfo() => 
            this.ChannelId + (this.ServerId * 10);

        public SlotMatch GetSlot(int SlotId)
        {
            SlotMatch[] slots = this.Slots;
            lock (slots)
            {
                return (((SlotId < 0) || (SlotId > 0x11)) ? null : this.Slots[SlotId]);
            }
        }

        public bool GetSlot(int SlotId, out SlotMatch Slot)
        {
            SlotMatch[] slots = this.Slots;
            lock (slots)
            {
                Slot = null;
                if ((SlotId >= 0) && (SlotId <= 0x11))
                {
                    Slot = this.Slots[SlotId];
                }
                return (Slot != null);
            }
        }

        private void method_0(Account account_0)
        {
            SlotMatch[] slots = this.Slots;
            lock (slots)
            {
                SlotMatch match;
                if (this.GetSlot(account_0.MatchSlot, out match) && (match.PlayerId == account_0.PlayerId))
                {
                    match.PlayerId = 0L;
                    match.State = SlotMatchState.Empty;
                }
            }
        }

        public bool RemovePlayer(Account Player)
        {
            ChannelModel channel = ChannelsXML.GetChannel(this.ServerId, this.ChannelId);
            if (channel == null)
            {
                return false;
            }
            this.method_0(Player);
            if (this.GetCountPlayers() == 0)
            {
                channel.RemoveMatch(this.MatchId);
            }
            else
            {
                if (Player.MatchSlot == this.Leader)
                {
                    this.SetNewLeader(-1, -1);
                }
                using (PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK protocol_clan_war_regist_mercenary_ack = new PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK(this))
                {
                    this.SendPacketToPlayers(protocol_clan_war_regist_mercenary_ack);
                }
            }
            Player.MatchSlot = -1;
            Player.Match = null;
            return true;
        }

        public void SendPacketToPlayers(GameServerPacket Packet)
        {
            List<Account> allPlayers = this.GetAllPlayers();
            if (allPlayers.Count != 0)
            {
                byte[] completeBytes = Packet.GetCompleteBytes("Match.SendPacketToPlayers(SendPacket)");
                using (List<Account>.Enumerator enumerator = allPlayers.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        enumerator.Current.SendCompletePacket(completeBytes, Packet.GetType().Name);
                    }
                }
            }
        }

        public void SendPacketToPlayers(GameServerPacket Packet, int Exception)
        {
            List<Account> allPlayers = this.GetAllPlayers(Exception);
            if (allPlayers.Count != 0)
            {
                byte[] completeBytes = Packet.GetCompleteBytes("Match.SendPacketToPlayers(SendPacket,int)");
                using (List<Account>.Enumerator enumerator = allPlayers.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        enumerator.Current.SendCompletePacket(completeBytes, Packet.GetType().Name);
                    }
                }
            }
        }

        public void SetNewLeader(int Leader, int OldLeader)
        {
            SlotMatch[] slots = this.Slots;
            lock (slots)
            {
                if (Leader != -1)
                {
                    this.Leader = Leader;
                }
                else
                {
                    for (int i = 0; i < this.Training; i++)
                    {
                        if ((i != OldLeader) && (this.Slots[i].PlayerId > 0L))
                        {
                            this.Leader = i;
                            break;
                        }
                    }
                }
            }
        }

        public ClanModel Clan { get; set; }

        public int Training { get; set; }

        public int ServerId { get; set; }

        public int ChannelId { get; set; }

        public int MatchId { get; set; }

        public int Leader { get; set; }

        public int FriendId { get; set; }

        public SlotMatch[] Slots { get; set; }

        public MatchState State { get; set; }
    }
}

