using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using System;

namespace Server.Match.Network.Actions.Event
{
	public class FireData
	{
		public FireData()
		{
		}

		public static FireDataInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
		{
			FireDataInfo fireDataInfo = new FireDataInfo()
			{
				Effect = C.ReadC(),
				Part = C.ReadC(),
				Index = C.ReadH(),
				WeaponId = C.ReadD(),
				Accessory = C.ReadC(),
				Extensions = C.ReadC(),
				X = C.ReadUH(),
				Y = C.ReadUH(),
				Z = C.ReadUH()
			};
			if (GenLog)
			{
				CLogger.Print(string.Format("PVP Slot: {0}; Weapon Id: {1}; Extensions: {2}; Fire: {3}; Part: {4}; X: {5}; Y: {6}; Z: {7}", new object[] { Action.Slot, fireDataInfo.WeaponId, fireDataInfo.Extensions, fireDataInfo.Effect, fireDataInfo.Part, fireDataInfo.X, fireDataInfo.Y, fireDataInfo.Z }), LoggerType.Warning, null);
			}
			return fireDataInfo;
		}

		public static void WriteInfo(SyncServerPacket S, FireDataInfo Info)
		{
			S.WriteC(Info.Effect);
			S.WriteC(Info.Part);
			S.WriteH(Info.Index);
			S.WriteD(Info.WeaponId);
			S.WriteC(Info.Accessory);
			S.WriteC(Info.Extensions);
			S.WriteH(Info.X);
			S.WriteH(Info.Y);
			S.WriteH(Info.Z);
		}
	}
}