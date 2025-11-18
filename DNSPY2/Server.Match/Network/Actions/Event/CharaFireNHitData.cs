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
	// Token: 0x02000013 RID: 19
	public class CharaFireNHitData
	{
		// Token: 0x0600004A RID: 74 RVA: 0x00007004 File Offset: 0x00005204
		public static List<CharaFireNHitDataInfo> ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog, bool OnlyBytes = false)
		{
			List<CharaFireNHitDataInfo> list = new List<CharaFireNHitDataInfo>();
			int num = (int)C.ReadC();
			for (int i = 0; i < num; i++)
			{
				CharaFireNHitDataInfo charaFireNHitDataInfo = new CharaFireNHitDataInfo
				{
					WeaponId = C.ReadD(),
					Accessory = C.ReadC(),
					Extensions = C.ReadC(),
					HitInfo = C.ReadUD(),
					Unk = C.ReadH(),
					X = C.ReadUH(),
					Y = C.ReadUH(),
					Z = C.ReadUH()
				};
				if (!OnlyBytes)
				{
					charaFireNHitDataInfo.WeaponClass = (ClassType)ComDiv.GetIdStatics(charaFireNHitDataInfo.WeaponId, 2);
				}
				if (GenLog)
				{
					CLogger.Print(string.Format("PVP Slot: {0}; Weapon Id: {1}; X: {2} Y: {3} Z: {4}", new object[] { Action.Slot, charaFireNHitDataInfo.WeaponId, charaFireNHitDataInfo.X, charaFireNHitDataInfo.Y, charaFireNHitDataInfo.Z }), LoggerType.Warning, null);
				}
				list.Add(charaFireNHitDataInfo);
			}
			return list;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00007114 File Offset: 0x00005314
		public static void WriteInfo(SyncServerPacket S, List<CharaFireNHitDataInfo> Hits)
		{
			S.WriteC((byte)Hits.Count);
			foreach (CharaFireNHitDataInfo charaFireNHitDataInfo in Hits)
			{
				S.WriteD(charaFireNHitDataInfo.WeaponId);
				S.WriteC(charaFireNHitDataInfo.Accessory);
				S.WriteC(charaFireNHitDataInfo.Extensions);
				S.WriteD(charaFireNHitDataInfo.HitInfo);
				S.WriteH(charaFireNHitDataInfo.Unk);
				S.WriteH(charaFireNHitDataInfo.X);
				S.WriteH(charaFireNHitDataInfo.Y);
				S.WriteH(charaFireNHitDataInfo.Z);
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000020A2 File Offset: 0x000002A2
		public CharaFireNHitData()
		{
		}
	}
}
