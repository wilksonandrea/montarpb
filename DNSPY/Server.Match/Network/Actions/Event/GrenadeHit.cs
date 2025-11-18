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
	// Token: 0x0200001A RID: 26
	public class GrenadeHit
	{
		// Token: 0x0600005F RID: 95 RVA: 0x000078D4 File Offset: 0x00005AD4
		public static List<GrenadeHitInfo> ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog, bool OnlyBytes = false)
		{
			List<GrenadeHitInfo> list = new List<GrenadeHitInfo>();
			int num = (int)C.ReadC();
			for (int i = 0; i < num; i++)
			{
				GrenadeHitInfo grenadeHitInfo = new GrenadeHitInfo
				{
					WeaponId = C.ReadD(),
					Accessory = C.ReadC(),
					Extensions = C.ReadC(),
					BoomInfo = C.ReadUH(),
					ObjectId = C.ReadUH(),
					HitInfo = C.ReadUD(),
					PlayerPos = C.ReadUHV(),
					FirePos = C.ReadUHV(),
					HitPos = C.ReadUHV(),
					GrenadesCount = C.ReadUH(),
					DeathType = (CharaDeath)C.ReadC()
				};
				if (!OnlyBytes)
				{
					grenadeHitInfo.HitEnum = (HitType)AllUtils.GetHitHelmet(grenadeHitInfo.HitInfo);
					if (grenadeHitInfo.BoomInfo > 0)
					{
						grenadeHitInfo.BoomPlayers = new List<int>();
						for (int j = 0; j < 18; j++)
						{
							int num2 = 1 << j;
							if (((int)grenadeHitInfo.BoomInfo & num2) == num2)
							{
								grenadeHitInfo.BoomPlayers.Add(j);
							}
						}
					}
					grenadeHitInfo.WeaponClass = (ClassType)ComDiv.GetIdStatics(grenadeHitInfo.WeaponId, 2);
				}
				if (GenLog)
				{
					CLogger.Print(string.Format("PVP Slot: {0}; Weapon Id: {1}; Ext: {2}; Acc: {3}", new object[] { Action.Slot, grenadeHitInfo.WeaponId, grenadeHitInfo.Extensions, grenadeHitInfo.Accessory }), LoggerType.Warning, null);
					CLogger.Print(string.Format("PVP Slot: {0}; Grenade Hit: {1}; [Object Postion] X: {2}; Y: {3}; Z: {4}", new object[]
					{
						Action.Slot,
						grenadeHitInfo.HitInfo,
						grenadeHitInfo.HitPos.X,
						grenadeHitInfo.HitPos.Y,
						grenadeHitInfo.HitPos.Z
					}), LoggerType.Warning, null);
					CLogger.Print(string.Format("PVP Slot: {0}; Grenade Hit: {1}; [Player Postion] X: {2}; Y: {3}; Z: {4}", new object[]
					{
						Action.Slot,
						grenadeHitInfo.HitInfo,
						grenadeHitInfo.FirePos.X,
						grenadeHitInfo.FirePos.Y,
						grenadeHitInfo.FirePos.Z
					}), LoggerType.Warning, null);
				}
				list.Add(grenadeHitInfo);
			}
			return list;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00007B30 File Offset: 0x00005D30
		public static void WriteInfo(SyncServerPacket S, List<GrenadeHitInfo> Hits)
		{
			S.WriteC((byte)Hits.Count);
			foreach (GrenadeHitInfo grenadeHitInfo in Hits)
			{
				S.WriteD(grenadeHitInfo.WeaponId);
				S.WriteC(grenadeHitInfo.Accessory);
				S.WriteC(grenadeHitInfo.Extensions);
				S.WriteH(grenadeHitInfo.BoomInfo);
				S.WriteH(grenadeHitInfo.ObjectId);
				S.WriteD(grenadeHitInfo.HitInfo);
				S.WriteHV(grenadeHitInfo.PlayerPos);
				S.WriteHV(grenadeHitInfo.FirePos);
				S.WriteHV(grenadeHitInfo.HitPos);
				S.WriteH(grenadeHitInfo.GrenadesCount);
				S.WriteC((byte)grenadeHitInfo.DeathType);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000020A2 File Offset: 0x000002A2
		public GrenadeHit()
		{
		}
	}
}
