using Plugin.Core.Models;

namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_URL_LIST_ACK : AuthServerPacket
{
	private readonly ServerConfig serverConfig_0;

	public PROTOCOL_BASE_URL_LIST_ACK(ServerConfig serverConfig_1)
	{
		serverConfig_0 = serverConfig_1;
	}

	public override void Write()
	{
		WriteH(2466);
		WriteH(514);
		WriteH((ushort)serverConfig_0.OfficialBanner.Length);
		WriteD(0);
		WriteC(2);
		WriteN(serverConfig_0.OfficialBanner, serverConfig_0.OfficialBanner.Length, "UTF-16LE");
		WriteQ(0L);
	}
}
