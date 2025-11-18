using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using System;
using System.Collections.Generic;

namespace Server.Match.Network.Actions.Event
{
	public class CharaFireNHitData
	{
		public CharaFireNHitData()
		{
		}

		public static List<CharaFireNHitDataInfo> ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog, bool OnlyBytes = false)
		{
			List<CharaFireNHitDataInfo> charaFireNHitDataInfos = new List<CharaFireNHitDataInfo>();
			int ınt32 = C.ReadC();
			for (int i = 0; i < ınt32; i++)
			{
				CharaFireNHitDataInfo charaFireNHitDataInfo = new CharaFireNHitDataInfo()
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
				charaFireNHitDataInfos.Add(charaFireNHitDataInfo);
			}
			return charaFireNHitDataInfos;
		}

		public static void WriteInfo(SyncServerPacket S, List<CharaFireNHitDataInfo> Hits)
		{
			S.WriteC((byte)Hits.Count);
			foreach (CharaFireNHitDataInfo hit in Hits)
			{
				S.WriteD(hit.WeaponId);
				S.WriteC(hit.Accessory);
				S.WriteC(hit.Extensions);
				S.WriteD(hit.HitInfo);
				S.WriteH(hit.Unk);
				S.WriteH(hit.X);
				S.WriteH(hit.Y);
				S.WriteH(hit.Z);
			}
		}
	}
}