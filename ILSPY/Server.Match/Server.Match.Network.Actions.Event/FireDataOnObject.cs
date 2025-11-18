using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;

namespace Server.Match.Network.Actions.Event;

public class FireDataOnObject
{
	public static FireDataObjectInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
	{
		FireDataObjectInfo fireDataObjectInfo = new FireDataObjectInfo
		{
			DeathType = (CharaDeath)C.ReadC(),
			HitPart = C.ReadC(),
			Unk = C.ReadC()
		};
		if (GenLog)
		{
			CLogger.Print($"PVP Slot: {Action.Slot}; Death Type: {fireDataObjectInfo.DeathType}; Hit Part: {fireDataObjectInfo.HitPart}", LoggerType.Warning);
		}
		return fireDataObjectInfo;
	}

	public static void WriteInfo(SyncServerPacket S, FireDataObjectInfo Info)
	{
		S.WriteC((byte)Info.DeathType);
		S.WriteC(Info.HitPart);
		S.WriteC(Info.Unk);
	}
}
