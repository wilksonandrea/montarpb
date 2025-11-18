using Plugin.Core.Utility;

namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_MATCH_CLAN_SEASON_ACK : AuthServerPacket
{
	private readonly int int_0;

	public PROTOCOL_MATCH_CLAN_SEASON_ACK(int int_1)
	{
		int_0 = int_1;
	}

	public override void Write()
	{
		WriteH(7700);
		WriteH(0);
		WriteD(2);
		WriteB(ComDiv.AddressBytes("127.0.0.1"));
		WriteB(ComDiv.AddressBytes("255.255.255.255"));
		WriteB(new byte[109]);
		WriteB(ComDiv.AddressBytes("127.0.0.1"));
		WriteB(ComDiv.AddressBytes("127.0.0.1"));
		WriteB(ComDiv.AddressBytes("255.255.255.255"));
		WriteB(new byte[103]);
	}
}
