using Plugin.Core;
using Plugin.Core.Models;

namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_NOTICE_ACK : AuthServerPacket
{
	private readonly ServerConfig serverConfig_0;

	private readonly string string_0;

	private readonly string string_1;

	public PROTOCOL_BASE_NOTICE_ACK(ServerConfig serverConfig_1)
	{
		serverConfig_0 = serverConfig_1;
		if (serverConfig_1 != null)
		{
			string_0 = Translation.GetLabel(serverConfig_1.ChannelAnnounce);
			string_1 = Translation.GetLabel(serverConfig_1.ChatAnnounce);
		}
	}

	public override void Write()
	{
		WriteH(2455);
		WriteH(0);
		WriteD(serverConfig_0.ChatAnnounceColor);
		WriteD(serverConfig_0.ChannelAnnounceColor);
		WriteH(0);
		WriteH((ushort)string_1.Length);
		WriteN(string_1, string_1.Length, "UTF-16LE");
		WriteH((ushort)string_0.Length);
		WriteN(string_0, string_0.Length, "UTF-16LE");
	}
}
