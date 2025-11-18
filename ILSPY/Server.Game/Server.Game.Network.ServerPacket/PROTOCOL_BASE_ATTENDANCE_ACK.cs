using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_ATTENDANCE_ACK : GameServerPacket
{
	private readonly EventVisitModel eventVisitModel_0;

	private readonly PlayerEvent playerEvent_0;

	private readonly EventErrorEnum eventErrorEnum_0;

	private readonly uint uint_0;

	private readonly uint uint_1;

	public PROTOCOL_BASE_ATTENDANCE_ACK(EventErrorEnum eventErrorEnum_1, EventVisitModel eventVisitModel_1, PlayerEvent playerEvent_1)
	{
		eventErrorEnum_0 = eventErrorEnum_1;
		eventVisitModel_0 = eventVisitModel_1;
		playerEvent_0 = playerEvent_1;
		if (playerEvent_1 != null)
		{
			uint_0 = uint.Parse(DateTimeUtil.Now("yyMMdd"));
			uint_1 = uint.Parse($"{DateTimeUtil.Convert($"{playerEvent_1.LastVisitDate}"):yyMMdd}");
		}
	}

	public override void Write()
	{
		WriteH(2337);
		WriteD((uint)eventErrorEnum_0);
		if (eventErrorEnum_0 == EventErrorEnum.VISIT_EVENT_SUCCESS)
		{
			WriteD(eventVisitModel_0.Id);
			WriteC((byte)playerEvent_0.LastVisitCheckDay);
			WriteC((byte)(playerEvent_0.LastVisitCheckDay - 1));
			WriteC((byte)((uint_1 < uint_0) ? 1u : 2u));
			WriteC((byte)playerEvent_0.LastVisitSeqType);
			WriteC(1);
		}
	}
}
