using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Match.Data.Enums;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using Server.Match.Data.Utils;

namespace Server.Match.Network.Actions.Event;

public class HitData
{
	public static List<HitDataInfo> ReadInfo(ActionModel Action, SyncClientPacket C, bool genLog, bool OnlyBytes = false)
	{
		List<HitDataInfo> list = new List<HitDataInfo>();
		int num = C.ReadC();
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
						if ((hitDataInfo.BoomInfo & num2) == num2)
						{
							hitDataInfo.BoomPlayers.Add(j);
						}
					}
				}
				hitDataInfo.WeaponClass = (ClassType)ComDiv.GetIdStatics(hitDataInfo.WeaponId, 2);
			}
			if (genLog)
			{
				CLogger.Print($"PVP Slot: {Action.Slot}; Weapon Id: {hitDataInfo.WeaponId}; Ext: {hitDataInfo.Extensions}; Acc: {hitDataInfo.Accessory}", LoggerType.Warning);
				CLogger.Print($"PVP Slot: {Action.Slot}; Hit Data: {hitDataInfo.HitIndex} [Start]: X: {hitDataInfo.StartBullet.X}; Y: {hitDataInfo.StartBullet.Y}; Z: {hitDataInfo.StartBullet.Z}", LoggerType.Warning);
				CLogger.Print($"PVP Slot: {Action.Slot}; Hit Data: {hitDataInfo.HitIndex} [Ended]: X: {hitDataInfo.EndBullet.X}; Y: {hitDataInfo.EndBullet.Y}; Z: {hitDataInfo.EndBullet.Z}", LoggerType.Warning);
			}
			list.Add(hitDataInfo);
		}
		return list;
	}

	public static void WriteInfo(SyncServerPacket S, List<HitDataInfo> Hits)
	{
		S.WriteC((byte)Hits.Count);
		foreach (HitDataInfo Hit in Hits)
		{
			S.WriteTV(Hit.StartBullet);
			S.WriteTV(Hit.EndBullet);
			S.WriteTV(Hit.BulletPos);
			S.WriteH(Hit.BoomInfo);
			S.WriteH(Hit.ObjectId);
			S.WriteD(Hit.HitIndex);
			S.WriteD(Hit.WeaponId);
			S.WriteC(Hit.Accessory);
			S.WriteC(Hit.Extensions);
		}
	}
}
