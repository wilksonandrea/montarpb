using Server.Auth.Data.Models;

namespace Server.Auth.Data.Sync.Update;

public class ClanInfo
{
	public static void AddMember(Account Player, Account Member)
	{
		lock (Player.ClanPlayers)
		{
			Player.ClanPlayers.Add(Member);
		}
	}

	public static void RemoveMember(Account Player, long PlayerId)
	{
		lock (Player.ClanPlayers)
		{
			int num = 0;
			while (true)
			{
				if (num < Player.ClanPlayers.Count)
				{
					if (Player.ClanPlayers[num].PlayerId == PlayerId)
					{
						break;
					}
					num++;
					continue;
				}
				return;
			}
			Player.ClanPlayers.RemoveAt(num);
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
}
