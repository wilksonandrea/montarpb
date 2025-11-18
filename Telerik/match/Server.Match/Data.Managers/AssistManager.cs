using Server.Match.Data.Models;
using System;
using System.Collections.Generic;

namespace Server.Match.Data.Managers
{
	public static class AssistManager
	{
		public static List<AssistServerData> Assists;

		static AssistManager()
		{
			AssistManager.Assists = new List<AssistServerData>();
		}

		public static void AddAssist(AssistServerData Assist)
		{
			lock (AssistManager.Assists)
			{
				AssistManager.Assists.Add(Assist);
			}
		}

		public static AssistServerData GetAssist(int VictimId, int RoomId)
		{
			AssistServerData assistServerDatum;
			lock (AssistManager.Assists)
			{
				List<AssistServerData>.Enumerator enumerator = AssistManager.Assists.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						AssistServerData current = enumerator.Current;
						if (current.Victim != VictimId || current.RoomId != RoomId)
						{
							continue;
						}
						assistServerDatum = current;
						return assistServerDatum;
					}
					return null;
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
			}
			return assistServerDatum;
		}

		public static bool RemoveAssist(AssistServerData Assist)
		{
			bool flag;
			lock (AssistManager.Assists)
			{
				flag = AssistManager.Assists.Remove(Assist);
			}
			return flag;
		}
	}
}