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
	public class GrenadeHit
	{
		public GrenadeHit()
		{
		}

		public static List<GrenadeHitInfo> ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog, bool OnlyBytes = false)
		{
			List<GrenadeHitInfo> grenadeHitInfos = new List<GrenadeHitInfo>();
			int ınt32 = C.ReadC();
			for (int i = 0; i < ınt32; i++)
			{
				GrenadeHitInfo grenadeHitInfo = new GrenadeHitInfo()
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
							int ınt321 = 1 << (j & 31);
							if ((grenadeHitInfo.BoomInfo & ınt321) == ınt321)
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
					CLogger.Print(string.Format("PVP Slot: {0}; Grenade Hit: {1}; [Object Postion] X: {2}; Y: {3}; Z: {4}", new object[] { Action.Slot, grenadeHitInfo.HitInfo, grenadeHitInfo.HitPos.X, grenadeHitInfo.HitPos.Y, grenadeHitInfo.HitPos.Z }), LoggerType.Warning, null);
					CLogger.Print(string.Format("PVP Slot: {0}; Grenade Hit: {1}; [Player Postion] X: {2}; Y: {3}; Z: {4}", new object[] { Action.Slot, grenadeHitInfo.HitInfo, grenadeHitInfo.FirePos.X, grenadeHitInfo.FirePos.Y, grenadeHitInfo.FirePos.Z }), LoggerType.Warning, null);
				}
				grenadeHitInfos.Add(grenadeHitInfo);
			}
			return grenadeHitInfos;
		}

		public static void WriteInfo(SyncServerPacket S, List<GrenadeHitInfo> Hits)
		{
			S.WriteC((byte)Hits.Count);
			foreach (GrenadeHitInfo hit in Hits)
			{
				S.WriteD(hit.WeaponId);
				S.WriteC(hit.Accessory);
				S.WriteC(hit.Extensions);
				S.WriteH(hit.BoomInfo);
				S.WriteH(hit.ObjectId);
				S.WriteD(hit.HitInfo);
				S.WriteHV(hit.PlayerPos);
				S.WriteHV(hit.FirePos);
				S.WriteHV(hit.HitPos);
				S.WriteH(hit.GrenadesCount);
				S.WriteC((byte)hit.DeathType);
			}
		}
	}
}