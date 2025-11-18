using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.SharpDX;
using Plugin.Core.Utility;
using Server.Match.Data.Enums;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using Server.Match.Data.Utils;
using System;
using System.Collections.Generic;

namespace Server.Match.Network.Actions.Event
{
	public class HitData
	{
		public HitData()
		{
		}

		public static List<HitDataInfo> ReadInfo(ActionModel Action, SyncClientPacket C, bool genLog, bool OnlyBytes = false)
		{
			List<HitDataInfo> hitDataInfos = new List<HitDataInfo>();
			int ınt32 = C.ReadC();
			for (int i = 0; i < ınt32; i++)
			{
				HitDataInfo hitDataInfo = new HitDataInfo()
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
							int ınt321 = 1 << (j & 31);
							if ((hitDataInfo.BoomInfo & ınt321) == ınt321)
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
					CLogger.Print(string.Format("PVP Slot: {0}; Hit Data: {1} [Start]: X: {2}; Y: {3}; Z: {4}", new object[] { Action.Slot, hitDataInfo.HitIndex, hitDataInfo.StartBullet.X, hitDataInfo.StartBullet.Y, hitDataInfo.StartBullet.Z }), LoggerType.Warning, null);
					CLogger.Print(string.Format("PVP Slot: {0}; Hit Data: {1} [Ended]: X: {2}; Y: {3}; Z: {4}", new object[] { Action.Slot, hitDataInfo.HitIndex, hitDataInfo.EndBullet.X, hitDataInfo.EndBullet.Y, hitDataInfo.EndBullet.Z }), LoggerType.Warning, null);
				}
				hitDataInfos.Add(hitDataInfo);
			}
			return hitDataInfos;
		}

		public static void WriteInfo(SyncServerPacket S, List<HitDataInfo> Hits)
		{
			S.WriteC((byte)Hits.Count);
			foreach (HitDataInfo hit in Hits)
			{
				S.WriteTV(hit.StartBullet);
				S.WriteTV(hit.EndBullet);
				S.WriteTV(hit.BulletPos);
				S.WriteH(hit.BoomInfo);
				S.WriteH(hit.ObjectId);
				S.WriteD(hit.HitIndex);
				S.WriteD(hit.WeaponId);
				S.WriteC(hit.Accessory);
				S.WriteC(hit.Extensions);
			}
		}
	}
}