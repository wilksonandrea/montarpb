using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Match.Data.Models.SubHead;

namespace Server.Match.Network.Actions.SubHead
{
	// Token: 0x0200000B RID: 11
	public class GrenadeSync
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00006AC8 File Offset: 0x00004CC8
		public static GrenadeInfo ReadInfo(SyncClientPacket C, bool GenLog, bool OnlyBytes = false)
		{
			GrenadeInfo grenadeInfo = new GrenadeInfo
			{
				Unk1 = C.ReadC(),
				Unk2 = C.ReadC(),
				Unk3 = C.ReadC(),
				Unk4 = C.ReadC(),
				BoomInfo = C.ReadUH(),
				WeaponId = C.ReadD(),
				Accessory = C.ReadC(),
				Extensions = C.ReadC(),
				ObjPosX = C.ReadUH(),
				ObjPosY = C.ReadUH(),
				ObjPosZ = C.ReadUH(),
				Unk5 = C.ReadD(),
				Unk6 = C.ReadD(),
				Unk7 = C.ReadD()
			};
			if (!OnlyBytes)
			{
				grenadeInfo.WeaponClass = (ClassType)ComDiv.GetIdStatics(grenadeInfo.WeaponId, 2);
			}
			if (GenLog)
			{
				CLogger.Print(string.Format("UDP_SUB_HEAD_GRENADE; Weapon Id: {0}; Acc Flag: {1}; Ext: {2}; Boom Info: {3}; X: {4}; Y: {5}; Z: {6}", new object[] { grenadeInfo.WeaponId, grenadeInfo.Accessory, grenadeInfo.Extensions, grenadeInfo.BoomInfo, grenadeInfo.ObjPosX, grenadeInfo.ObjPosY, grenadeInfo.ObjPosZ }), LoggerType.Warning, null);
			}
			return grenadeInfo;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00006C18 File Offset: 0x00004E18
		public static void WriteInfo(SyncServerPacket S, GrenadeInfo Info)
		{
			S.WriteC(Info.Unk1);
			S.WriteC(Info.Unk2);
			S.WriteC(Info.Unk3);
			S.WriteC(Info.Unk4);
			S.WriteH(Info.BoomInfo);
			S.WriteD(Info.WeaponId);
			S.WriteC(Info.Accessory);
			S.WriteC(Info.Extensions);
			S.WriteH(Info.ObjPosX);
			S.WriteH(Info.ObjPosY);
			S.WriteH(Info.ObjPosZ);
			S.WriteD(Info.Unk5);
			S.WriteD(Info.Unk6);
			S.WriteD(Info.Unk7);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000020A2 File Offset: 0x000002A2
		public GrenadeSync()
		{
		}
	}
}
