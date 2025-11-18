using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Match.Data.Enums;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using Server.Match.Data.Utils;

namespace Server.Match.Network.Actions.Event
{
	// Token: 0x0200001B RID: 27
	public class HitData
	{
		// Token: 0x06000062 RID: 98 RVA: 0x00007C10 File Offset: 0x00005E10
		public static List<HitDataInfo> ReadInfo(ActionModel Action, SyncClientPacket C, bool genLog, bool OnlyBytes = false)
		{
			List<HitDataInfo> list = new List<HitDataInfo>();
			int num = (int)C.ReadC();
			for (int i = 0; i < num; i++)
			{
				HitDataInfo hitDataInfo = new HitDataInfo
				{
					StartBullet = C.ReadTV(),
					EndBullet = C.ReadTV(),
					BulletPos = C.ReadTV(),
					BoomInfo = C.ReadUH(),
					ObjectId = C.ReadUH(),
					HitIndex = C.ReadUD(),
					WeaponId = C.ReadD(),
					Accessory = C.ReadC(),
					Extensions = C.ReadC()
				};
				if (!OnlyBytes)
				{
					hitDataInfo.HitEnum = (HitType)AllUtils.GetHitHelmet(hitDataInfo.HitIndex);
					if (hitDataInfo.BoomInfo > 0)
					{
						hitDataInfo.BoomPlayers = new List<int>();
						for (int j = 0; j < 18; j++)
						{
							int num2 = 1 << j;
							if (((int)hitDataInfo.BoomInfo & num2) == num2)
							{
								hitDataInfo.BoomPlayers.Add(j);
							}
						}
					}
					hitDataInfo.WeaponClass = (ClassType)ComDiv.GetIdStatics(hitDataInfo.WeaponId, 2);
				}
				if (genLog)
				{
					CLogger.Print(string.Format("PVP Slot: {0}; Weapon Id: {1}; Ext: {2}; Acc: {3}", new object[] { Action.Slot, hitDataInfo.WeaponId, hitDataInfo.Extensions, hitDataInfo.Accessory }), LoggerType.Warning, null);
					CLogger.Print(string.Format("PVP Slot: {0}; Hit Data: {1} [Start]: X: {2}; Y: {3}; Z: {4}", new object[]
					{
						Action.Slot,
						hitDataInfo.HitIndex,
						hitDataInfo.StartBullet.X,
						hitDataInfo.StartBullet.Y,
						hitDataInfo.StartBullet.Z
					}), LoggerType.Warning, null);
					CLogger.Print(string.Format("PVP Slot: {0}; Hit Data: {1} [Ended]: X: {2}; Y: {3}; Z: {4}", new object[]
					{
						Action.Slot,
						hitDataInfo.HitIndex,
						hitDataInfo.EndBullet.X,
						hitDataInfo.EndBullet.Y,
						hitDataInfo.EndBullet.Z
					}), LoggerType.Warning, null);
				}
				list.Add(hitDataInfo);
			}
			return list;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00007E54 File Offset: 0x00006054
		public static void WriteInfo(SyncServerPacket S, List<HitDataInfo> Hits)
		{
			S.WriteC((byte)Hits.Count);
			foreach (HitDataInfo hitDataInfo in Hits)
			{
				S.WriteTV(hitDataInfo.StartBullet);
				S.WriteTV(hitDataInfo.EndBullet);
				S.WriteTV(hitDataInfo.BulletPos);
				S.WriteH(hitDataInfo.BoomInfo);
				S.WriteH(hitDataInfo.ObjectId);
				S.WriteD(hitDataInfo.HitIndex);
				S.WriteD(hitDataInfo.WeaponId);
				S.WriteC(hitDataInfo.Accessory);
				S.WriteC(hitDataInfo.Extensions);
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000020A2 File Offset: 0x000002A2
		public HitData()
		{
		}
	}
}
