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

public class GrenadeHit
{
	public static List<GrenadeHitInfo> ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog, bool OnlyBytes = false)
	{
		List<GrenadeHitInfo> list = new List<GrenadeHitInfo>();
		int num = C.ReadC();
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
						if ((grenadeHitInfo.BoomInfo & num2) == num2)
						{
							grenadeHitInfo.BoomPlayers.Add(j);
						}
					}
				}
				grenadeHitInfo.WeaponClass = (ClassType)ComDiv.GetIdStatics(grenadeHitInfo.WeaponId, 2);
			}
			if (GenLog)
			{
				CLogger.Print($"PVP Slot: {Action.Slot}; Weapon Id: {grenadeHitInfo.WeaponId}; Ext: {grenadeHitInfo.Extensions}; Acc: {grenadeHitInfo.Accessory}", LoggerType.Warning);
				CLogger.Print($"PVP Slot: {Action.Slot}; Grenade Hit: {grenadeHitInfo.HitInfo}; [Object Postion] X: {grenadeHitInfo.HitPos.X}; Y: {grenadeHitInfo.HitPos.Y}; Z: {grenadeHitInfo.HitPos.Z}", LoggerType.Warning);
				CLogger.Print($"PVP Slot: {Action.Slot}; Grenade Hit: {grenadeHitInfo.HitInfo}; [Player Postion] X: {grenadeHitInfo.FirePos.X}; Y: {grenadeHitInfo.FirePos.Y}; Z: {grenadeHitInfo.FirePos.Z}", LoggerType.Warning);
			}
			list.Add(grenadeHitInfo);
		}
		return list;
	}

	public static void WriteInfo(SyncServerPacket S, List<GrenadeHitInfo> Hits)
	{
		S.WriteC((byte)Hits.Count);
		foreach (GrenadeHitInfo Hit in Hits)
		{
			S.WriteD(Hit.WeaponId);
			S.WriteC(Hit.Accessory);
			S.WriteC(Hit.Extensions);
			S.WriteH(Hit.BoomInfo);
			S.WriteH(Hit.ObjectId);
			S.WriteD(Hit.HitInfo);
			S.WriteHV(Hit.PlayerPos);
			S.WriteHV(Hit.FirePos);
			S.WriteHV(Hit.HitPos);
			S.WriteH(Hit.GrenadesCount);
			S.WriteC((byte)Hit.DeathType);
		}
	}
}
