using Plugin.Core;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_CREATE_CLAN_CONDITION_ACK : GameServerPacket
{
	public override void Write()
	{
		WriteH(915);
		WriteC((byte)ConfigLoader.MinCreateRank);
		WriteD(ConfigLoader.MinCreateGold);
	}
}
