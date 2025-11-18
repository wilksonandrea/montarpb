using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;

namespace Server.Match.Network.Actions.Event;

public class CharaFireNHitData
{
	public static List<CharaFireNHitDataInfo> ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog, bool OnlyBytes = false)
	{
		List<CharaFireNHitDataInfo> list = new List<CharaFireNHitDataInfo>();
		int num = C.ReadC();
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
				CLogger.Print($"PVP Slot: {Action.Slot}; Weapon Id: {charaFireNHitDataInfo.WeaponId}; X: {charaFireNHitDataInfo.X} Y: {charaFireNHitDataInfo.Y} Z: {charaFireNHitDataInfo.Z}", LoggerType.Warning);
			}
			list.Add(charaFireNHitDataInfo);
		}
		return list;
	}

	public static void WriteInfo(SyncServerPacket S, List<CharaFireNHitDataInfo> Hits)
	{
		S.WriteC((byte)Hits.Count);
		foreach (CharaFireNHitDataInfo Hit in Hits)
		{
			S.WriteD(Hit.WeaponId);
			S.WriteC(Hit.Accessory);
			S.WriteC(Hit.Extensions);
			S.WriteD(Hit.HitInfo);
			S.WriteH(Hit.Unk);
			S.WriteH(Hit.X);
			S.WriteH(Hit.Y);
			S.WriteH(Hit.Z);
		}
	}
}
