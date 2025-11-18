using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.SharpDX;
using Plugin.Core.Utility;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using System;
using System.Collections.Generic;

namespace Server.Match.Network.Actions.Event
{
	public class Suicide
	{
		public Suicide()
		{
		}

		public static List<SuicideInfo> ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog, bool OnlyBytes = false)
		{
			List<SuicideInfo> suicideInfos = new List<SuicideInfo>();
			int ınt32 = C.ReadC();
			for (int i = 0; i < ınt32; i++)
			{
				SuicideInfo suicideInfo = new SuicideInfo()
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
					CLogger.Print(string.Format("PVP Slot: {0}; Weapon Id: {1}; Ext: {2}; Acc: {3}; Suicide Hit: {4}; X: {5}; Y: {6}; Z: {7}", new object[] { Action.Slot, suicideInfo.WeaponId, suicideInfo.Extensions, suicideInfo.Accessory, suicideInfo.HitInfo, suicideInfo.PlayerPos.X, suicideInfo.PlayerPos.Y, suicideInfo.PlayerPos.Z }), LoggerType.Warning, null);
				}
				suicideInfos.Add(suicideInfo);
			}
			return suicideInfos;
		}

		public static void WriteInfo(SyncServerPacket S, List<SuicideInfo> Hits)
		{
			S.WriteC((byte)Hits.Count);
			foreach (SuicideInfo hit in Hits)
			{
				S.WriteHV(hit.PlayerPos);
				S.WriteD(hit.WeaponId);
				S.WriteC(hit.Accessory);
				S.WriteC(hit.Extensions);
				S.WriteD(hit.HitInfo);
			}
		}
	}
}