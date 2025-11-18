using System.Collections.Generic;
using Server.Match.Data.Models;

namespace Server.Match.Data.Managers;

public static class AssistManager
{
	public static List<AssistServerData> Assists = new List<AssistServerData>();

	public static void AddAssist(AssistServerData Assist)
	{
		lock (Assists)
		{
			Assists.Add(Assist);
		}
	}

	public static bool RemoveAssist(AssistServerData Assist)
	{
		lock (Assists)
		{
			return Assists.Remove(Assist);
		}
	}

	public static AssistServerData GetAssist(int VictimId, int RoomId)
	{
		lock (Assists)
		{
			foreach (AssistServerData assist in Assists)
			{
				if (assist.Victim == VictimId && assist.RoomId == RoomId)
				{
					return assist;
				}
			}
		}
		return null;
	}
}
