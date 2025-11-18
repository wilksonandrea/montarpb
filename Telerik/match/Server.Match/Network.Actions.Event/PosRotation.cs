using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using System;

namespace Server.Match.Network.Actions.Event
{
	public class PosRotation
	{
		public PosRotation()
		{
		}

		public static PosRotationInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
		{
			PosRotationInfo posRotationInfo = new PosRotationInfo()
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

		public static void WriteInfo(SyncServerPacket S, PosRotationInfo Info)
		{
			S.WriteH(Info.CameraX);
			S.WriteH(Info.CameraY);
			S.WriteH(Info.Area);
			S.WriteH(Info.RotationZ);
			S.WriteH(Info.RotationX);
			S.WriteH(Info.RotationY);
		}
	}
}