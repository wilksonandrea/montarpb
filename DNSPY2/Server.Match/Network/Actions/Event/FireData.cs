using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;

namespace Server.Match.Network.Actions.Event
{
	// Token: 0x02000015 RID: 21
	public class FireData
	{
		// Token: 0x06000050 RID: 80 RVA: 0x0000738C File Offset: 0x0000558C
		public static FireDataInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
		{
			FireDataInfo fireDataInfo = new FireDataInfo
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

		// Token: 0x06000051 RID: 81 RVA: 0x0000749C File Offset: 0x0000569C
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

		// Token: 0x06000052 RID: 82 RVA: 0x000020A2 File Offset: 0x000002A2
		public FireData()
		{
		}
	}
}
