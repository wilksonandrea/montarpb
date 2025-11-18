using Server.Auth.Data.Models;
using System;
using System.Collections.Generic;

namespace Server.Auth.Data.Sync.Update
{
	public class ClanInfo
	{
		public ClanInfo()
		{
		}

		public static void AddMember(Account Player, Account Member)
		{
			lock (Player.ClanPlayers)
			{
				Player.ClanPlayers.Add(Member);
			}
		}

		public static void ClearList(Account Player)
		{
			lock (Player.ClanPlayers)
			{
				Player.ClanId = 0;
				Player.ClanAccess = 0;
				Player.ClanPlayers.Clear();
			}
		}

		public static void RemoveMember(Account Player, long PlayerId)
		{
			lock (Player.ClanPlayers)
			{
				int ınt32 = 0;
				while (ınt32 < Player.ClanPlayers.Count)
				{
					if (Player.ClanPlayers[ınt32].PlayerId != PlayerId)
					{
						ınt32++;
					}
					else
					{
						Player.ClanPlayers.RemoveAt(ınt32);
						return;
					}
				}
			}
		}
	}
}