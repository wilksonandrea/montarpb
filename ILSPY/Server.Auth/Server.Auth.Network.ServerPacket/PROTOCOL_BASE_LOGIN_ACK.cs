using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Auth.Data.Models;
using Server.Auth.Data.Utils;

namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_LOGIN_ACK : AuthServerPacket
{
	private readonly EventErrorEnum eventErrorEnum_0;

	private readonly Account account_0;

	private readonly uint uint_0;

	public PROTOCOL_BASE_LOGIN_ACK(EventErrorEnum eventErrorEnum_1, Account account_1, uint uint_1)
	{
		eventErrorEnum_0 = eventErrorEnum_1;
		account_0 = account_1;
		uint_0 = uint_1;
	}

	public override void Write()
	{
		WriteH(1283);
		WriteH(0);
		WriteD(uint_0);
		WriteB(new byte[12]);
		WriteD(AllUtils.GetFeatures());
		WriteH(1402);
		WriteB(new byte[16]);
		WriteB(method_0(eventErrorEnum_0, account_0));
		WriteD((uint)eventErrorEnum_0);
	}

	private byte[] method_0(EventErrorEnum eventErrorEnum_1, Account account_1)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		if (eventErrorEnum_1.Equals(EventErrorEnum.SUCCESS))
		{
			syncServerPacket.WriteC((byte)$"{account_1.PlayerId}".Length);
			syncServerPacket.WriteS($"{account_1.PlayerId}", $"{account_1.PlayerId}".Length);
			syncServerPacket.WriteC(0);
			syncServerPacket.WriteC((byte)account_1.Username.Length);
			syncServerPacket.WriteS(account_1.Username, account_1.Username.Length);
			syncServerPacket.WriteQ(account_1.PlayerId);
		}
		else
		{
			syncServerPacket.WriteB(Bitwise.HexStringToByteArray("00 00 00 00 00 00 00 00 00 00 00"));
		}
		return syncServerPacket.ToArray();
	}
}
