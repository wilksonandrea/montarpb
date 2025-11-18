using System;
using System.Collections.Generic;
using Server.Match.Data.Models;

namespace Server.Match.Data.Managers
{
	// Token: 0x0200005F RID: 95
	public static class AssistManager
	{
		// Token: 0x060001CA RID: 458 RVA: 0x0000B66C File Offset: 0x0000986C
		public static void AddAssist(AssistServerData Assist)
		{
			List<AssistServerData> assists = AssistManager.Assists;
			lock (assists)
			{
				AssistManager.Assists.Add(Assist);
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000B6B0 File Offset: 0x000098B0
		public static bool RemoveAssist(AssistServerData Assist)
		{
			List<AssistServerData> assists = AssistManager.Assists;
			bool flag2;
			lock (assists)
			{
				flag2 = AssistManager.Assists.Remove(Assist);
			}
			return flag2;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000B6F8 File Offset: 0x000098F8
		public static AssistServerData GetAssist(int VictimId, int RoomId)
		{
			List<AssistServerData> assists = AssistManager.Assists;
			lock (assists)
			{
				foreach (AssistServerData assistServerData in AssistManager.Assists)
				{
					if (assistServerData.Victim == VictimId && assistServerData.RoomId == RoomId)
					{
						return assistServerData;
					}
				}
			}
			return null;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00002AFC File Offset: 0x00000CFC
		// Note: this type is marked as 'beforefieldinit'.
		static AssistManager()
		{
		}

		// Token: 0x04000150 RID: 336
		public static List<AssistServerData> Assists = new List<AssistServerData>();
	}
}
