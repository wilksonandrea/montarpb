using Plugin.Core.Utility;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_MATCH_CLAN_SEASON_ACK : GameServerPacket
{
	private readonly bool bool_0;

	public PROTOCOL_MATCH_CLAN_SEASON_ACK(bool bool_1)
	{
		bool_0 = bool_1;
	}

	public override void Write()
	{
		WriteH(7700);
		WriteH(0);
		WriteD(2);
		WriteD(bool_0 ? 1 : 0);
		WriteB(ComDiv.AddressBytes("127.0.0.1"));
		WriteB(ComDiv.AddressBytes("255.255.255.255"));
		WriteB(new byte[109]);
		WriteB(ComDiv.AddressBytes("127.0.0.1"));
		WriteB(ComDiv.AddressBytes("127.0.0.1"));
		WriteB(ComDiv.AddressBytes("255.255.255.255"));
		WriteB(new byte[103]);
	}
}
