using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;

namespace Server.Match.Network.Actions.Event;

public class PosRotation
{
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
			CLogger.Print($"PVP Slot: {Action.Slot}; Camera: (X: {posRotationInfo.CameraX}, Y: {posRotationInfo.CameraY}); Area: {posRotationInfo.Area}; Rotation: (X: {posRotationInfo.RotationX}, Y: {posRotationInfo.RotationY}, Z: {posRotationInfo.RotationZ})", LoggerType.Warning);
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
