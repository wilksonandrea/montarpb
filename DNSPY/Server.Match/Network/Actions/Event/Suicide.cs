using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;

namespace Server.Match.Network.Actions.Event
{
	// Token: 0x02000022 RID: 34
	public class Suicide
	{
		// Token: 0x06000078 RID: 120 RVA: 0x00008298 File Offset: 0x00006498
		public static List<SuicideInfo> ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog, bool OnlyBytes = false)
		{
			List<SuicideInfo> list = new List<SuicideInfo>();
			int num = (int)C.ReadC();
			for (int i = 0; i < num; i++)
			{
				SuicideInfo suicideInfo = new SuicideInfo
				{
					PlayerPos = C.ReadUHV(),
					WeaponId = C.ReadD(),
					Accessory = C.ReadC(),
					Extensions = C.ReadC(),
					HitInfo = C.ReadUD()
				};
				if (!OnlyBytes)
				{
					suicideInfo.WeaponClass = (ClassType)ComDiv.GetIdStatics(suicideInfo.WeaponId, 2);
				}
				if (GenLog)
				{
					CLogger.Print(string.Format("PVP Slot: {0}; Weapon Id: {1}; Ext: {2}; Acc: {3}; Suicide Hit: {4}; X: {5}; Y: {6}; Z: {7}", new object[]
					{
						Action.Slot,
						suicideInfo.WeaponId,
						suicideInfo.Extensions,
						suicideInfo.Accessory,
						suicideInfo.HitInfo,
						suicideInfo.PlayerPos.X,
						suicideInfo.PlayerPos.Y,
						suicideInfo.PlayerPos.Z
					}), LoggerType.Warning, null);
				}
				list.Add(suicideInfo);
			}
			return list;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000083C0 File Offset: 0x000065C0
		public static void WriteInfo(SyncServerPacket S, List<SuicideInfo> Hits)
		{
			S.WriteC((byte)Hits.Count);
			foreach (SuicideInfo suicideInfo in Hits)
			{
				S.WriteHV(suicideInfo.PlayerPos);
				S.WriteD(suicideInfo.WeaponId);
				S.WriteC(suicideInfo.Accessory);
				S.WriteC(suicideInfo.Extensions);
				S.WriteD(suicideInfo.HitInfo);
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000020A2 File Offset: 0x000002A2
		public Suicide()
		{
		}
	}
}
