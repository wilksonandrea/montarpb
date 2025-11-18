namespace Server.Game.Data.Models
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Utility;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Sync.Server;
    using Server.Game.Network;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class ChannelModel
    {
        public ChannelModel(int int_6)
        {
            this.ServerId = int_6;
            this.Players = new List<PlayerSession>();
            this.Rooms = new List<RoomModel>();
            this.Matches = new List<MatchModel>();
            this.DateTime_0 = DateTimeUtil.Now();
        }

        public void AddMatch(MatchModel match)
        {
            List<MatchModel> matches = this.Matches;
            lock (matches)
            {
                if (!this.Matches.Contains(match))
                {
                    this.Matches.Add(match);
                }
            }
        }

        public bool AddPlayer(PlayerSession pS)
        {
            bool flag2;
            List<PlayerSession> players = this.Players;
            lock (players)
            {
                if (this.Players.Contains(pS))
                {
                    flag2 = false;
                }
                else
                {
                    this.Players.Add(pS);
                    UpdateServer.RefreshSChannel(this.ServerId);
                    UpdateChannel.RefreshChannel(this.ServerId, this.Id, this.Players.Count);
                    flag2 = true;
                }
            }
            return flag2;
        }

        public void AddRoom(RoomModel room)
        {
            List<RoomModel> rooms = this.Rooms;
            lock (rooms)
            {
                this.Rooms.Add(room);
            }
        }

        public MatchModel GetMatch(int id)
        {
            List<MatchModel> matches = this.Matches;
            lock (matches)
            {
                using (List<MatchModel>.Enumerator enumerator = this.Matches.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        MatchModel current = enumerator.Current;
                        if (current.MatchId == id)
                        {
                            return current;
                        }
                    }
                }
                return null;
            }
        }

        public MatchModel GetMatch(int id, int clan)
        {
            List<MatchModel> matches = this.Matches;
            lock (matches)
            {
                using (List<MatchModel>.Enumerator enumerator = this.Matches.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        MatchModel current = enumerator.Current;
                        if ((current.FriendId == id) && (current.Clan.Id == clan))
                        {
                            return current;
                        }
                    }
                }
                return null;
            }
        }

        public PlayerSession GetPlayer(int session)
        {
            List<PlayerSession> players = this.Players;
            lock (players)
            {
                using (List<PlayerSession>.Enumerator enumerator = this.Players.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        PlayerSession current = enumerator.Current;
                        if (current.SessionId == session)
                        {
                            return current;
                        }
                    }
                }
                return null;
            }
        }

        public PlayerSession GetPlayer(int SessionId, out int Index)
        {
            PlayerSession session2;
            Index = -1;
            List<PlayerSession> players = this.Players;
            lock (players)
            {
                int num = 0;
                while (true)
                {
                    if (num >= this.Players.Count)
                    {
                        session2 = null;
                    }
                    else
                    {
                        PlayerSession session = this.Players[num];
                        if (session.SessionId != SessionId)
                        {
                            num++;
                            continue;
                        }
                        Index = num;
                        session2 = session;
                    }
                    break;
                }
            }
            return session2;
        }

        public RoomModel GetRoom(int id)
        {
            List<RoomModel> rooms = this.Rooms;
            lock (rooms)
            {
                using (List<RoomModel>.Enumerator enumerator = this.Rooms.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        RoomModel current = enumerator.Current;
                        if (current.RoomId == id)
                        {
                            return current;
                        }
                    }
                }
                return null;
            }
        }

        public List<Account> GetWaitPlayers()
        {
            List<Account> list = new List<Account>();
            List<PlayerSession> players = this.Players;
            lock (players)
            {
                using (List<PlayerSession>.Enumerator enumerator = this.Players.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        Account item = AccountManager.GetAccount(enumerator.Current.PlayerId, true);
                        if ((item != null) && ((item.Room == null) && !string.IsNullOrEmpty(item.Nickname)))
                        {
                            list.Add(item);
                        }
                    }
                }
            }
            return list;
        }

        public void RemoveEmptyRooms()
        {
            List<RoomModel> rooms = this.Rooms;
            lock (rooms)
            {
                if (ComDiv.GetDuration(this.DateTime_0) >= ConfigLoader.EmptyRoomRemovalInterval)
                {
                    this.DateTime_0 = DateTimeUtil.Now();
                    for (int i = 0; i < this.Rooms.Count; i++)
                    {
                        if (this.Rooms[i].GetCountPlayers() < 1)
                        {
                            this.Rooms.RemoveAt(i--);
                        }
                    }
                }
            }
        }

        public void RemoveMatch(int matchId)
        {
            List<MatchModel> matches = this.Matches;
            lock (matches)
            {
                for (int i = 0; i < this.Matches.Count; i++)
                {
                    if (matchId == this.Matches[i].MatchId)
                    {
                        this.Matches.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        public bool RemovePlayer(Account Player)
        {
            bool flag = false;
            Player.ChannelId = -1;
            Player.ServerId = -1;
            if (Player.Session != null)
            {
                List<PlayerSession> players = this.Players;
                lock (players)
                {
                    flag = this.Players.Remove(Player.Session);
                }
                UpdateChannel.RefreshChannel(this.ServerId, this.Id, this.Players.Count);
                if (flag)
                {
                    UpdateServer.RefreshSChannel(this.ServerId);
                }
            }
            return flag;
        }

        public void SendPacketToWaitPlayers(GameServerPacket Packet)
        {
            List<Account> waitPlayers = this.GetWaitPlayers();
            if (waitPlayers.Count != 0)
            {
                byte[] completeBytes = Packet.GetCompleteBytes("Channel.SendPacketToWaitPlayers");
                using (List<Account>.Enumerator enumerator = waitPlayers.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        enumerator.Current.SendCompletePacket(completeBytes, Packet.GetType().Name);
                    }
                }
            }
        }

        public int Id { get; set; }

        public ChannelType Type { get; set; }

        public int ServerId { get; set; }

        public int MaxRooms { get; set; }

        public int ExpBonus { get; set; }

        public int GoldBonus { get; set; }

        public int CashBonus { get; set; }

        public string Password { get; set; }

        public List<PlayerSession> Players { get; set; }

        public List<RoomModel> Rooms { get; set; }

        public List<MatchModel> Matches { get; set; }

        private DateTime DateTime_0 { get; set; }
    }
}

