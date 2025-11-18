using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Enums;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;

namespace Server.Match.Network.Actions.Event;

public class UseObject
{
	public static List<UseObjectInfo> ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
	{
		List<UseObjectInfo> list = new List<UseObjectInfo>();
		int num = C.ReadC();
		for (int i = 0; i < num; i++)
		{
			UseObjectInfo useObjectInfo = new UseObjectInfo
			{
				ObjectId = C.ReadUH(),
				Use = C.ReadC(),
				SpaceFlags = (CharaMoves)C.ReadC()
			};
			if (GenLog)
			{
				CLogger.Print($"PVP Slot: {Action.Slot}; Use Object: {useObjectInfo.Use}; Flag: {useObjectInfo.SpaceFlags}; ObjectId: {useObjectInfo.ObjectId}", LoggerType.Warning);
			}
			list.Add(useObjectInfo);
		}
		return list;
	}

	public static void WriteInfo(SyncServerPacket S, List<UseObjectInfo> Infos)
	{
		S.WriteC((byte)Infos.Count);
		foreach (UseObjectInfo Info in Infos)
		{
			S.WriteH(Info.ObjectId);
			S.WriteC(Info.Use);
			S.WriteC((byte)Info.SpaceFlags);
		}
	}
}
