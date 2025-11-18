using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;

namespace Server.Match.Network.Actions.Event;

public class Suicide
{
	public static List<SuicideInfo> ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog, bool OnlyBytes = false)
	{
		List<SuicideInfo> list = new List<SuicideInfo>();
		int num = C.ReadC();
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
				CLogger.Print($"PVP Slot: {Action.Slot}; Weapon Id: {suicideInfo.WeaponId}; Ext: {suicideInfo.Extensions}; Acc: {suicideInfo.Accessory}; Suicide Hit: {suicideInfo.HitInfo}; X: {suicideInfo.PlayerPos.X}; Y: {suicideInfo.PlayerPos.Y}; Z: {suicideInfo.PlayerPos.Z}", LoggerType.Warning);
			}
			list.Add(suicideInfo);
		}
		return list;
	}

	public static void WriteInfo(SyncServerPacket S, List<SuicideInfo> Hits)
	{
		S.WriteC((byte)Hits.Count);
		foreach (SuicideInfo Hit in Hits)
		{
			S.WriteHV(Hit.PlayerPos);
			S.WriteD(Hit.WeaponId);
			S.WriteC(Hit.Accessory);
			S.WriteC(Hit.Extensions);
			S.WriteD(Hit.HitInfo);
		}
	}
}
