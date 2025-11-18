using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;

namespace Server.Match.Network.Actions.Event
{
	// Token: 0x0200001E RID: 30
	public class PosRotation
	{
		// Token: 0x0600006B RID: 107 RVA: 0x0000800C File Offset: 0x0000620C
		public static PosRotationInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
		{
			PosRotationInfo posRotationInfo = new PosRotationInfo
			{
				CameraX = C.ReadUH(),
				CameraY = C.ReadUH(),
				Area = C.ReadUH(),
				RotationZ = C.ReadUH(),
				RotationX = C.ReadUH(),
				RotationY = C.ReadUH()
			};
			if (GenLog)
			{
				CLogger.Print(string.Format("PVP Slot: {0}; Camera: (X: {1}, Y: {2}); Area: {3}; Rotation: (X: {4}, Y: {5}, Z: {6})", new object[] { Action.Slot, posRotationInfo.CameraX, posRotationInfo.CameraY, posRotationInfo.Area, posRotationInfo.RotationX, posRotationInfo.RotationY, posRotationInfo.RotationZ }), LoggerType.Warning, null);
			}
			return posRotationInfo;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000080E4 File Offset: 0x000062E4
		public static void WriteInfo(SyncServerPacket S, PosRotationInfo Info)
		{
			S.WriteH(Info.CameraX);
			S.WriteH(Info.CameraY);
			S.WriteH(Info.Area);
			S.WriteH(Info.RotationZ);
			S.WriteH(Info.RotationX);
			S.WriteH(Info.RotationY);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000020A2 File Offset: 0x000002A2
		public PosRotation()
		{
		}
	}
}
