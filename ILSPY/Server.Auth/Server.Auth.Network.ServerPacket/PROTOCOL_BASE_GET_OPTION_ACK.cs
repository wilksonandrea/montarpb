using Plugin.Core.Models;
using Plugin.Core.Utility;

namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_GET_OPTION_ACK : AuthServerPacket
{
	private readonly int int_0;

	private readonly PlayerConfig playerConfig_0;

	public PROTOCOL_BASE_GET_OPTION_ACK(int int_1, PlayerConfig playerConfig_1)
	{
		int_0 = int_1;
		playerConfig_0 = playerConfig_1;
	}

	public override void Write()
	{
		WriteH(2321);
		WriteH(0);
		WriteD(int_0);
		if (int_0 == 0)
		{
			WriteH((ushort)playerConfig_0.Nations);
			WriteH((ushort)playerConfig_0.Macro5.Length);
			WriteN(playerConfig_0.Macro5, playerConfig_0.Macro5.Length, "UTF-16LE");
			WriteH((ushort)playerConfig_0.Macro4.Length);
			WriteN(playerConfig_0.Macro4, playerConfig_0.Macro4.Length, "UTF-16LE");
			WriteH((ushort)playerConfig_0.Macro3.Length);
			WriteN(playerConfig_0.Macro3, playerConfig_0.Macro3.Length, "UTF-16LE");
			WriteH((ushort)playerConfig_0.Macro2.Length);
			WriteN(playerConfig_0.Macro2, playerConfig_0.Macro2.Length, "UTF-16LE");
			WriteH((ushort)playerConfig_0.Macro1.Length);
			WriteN(playerConfig_0.Macro1, playerConfig_0.Macro1.Length, "UTF-16LE");
			WriteH(49);
			WriteB(Bitwise.HexStringToByteArray("39 F8 10 00"));
			WriteB(playerConfig_0.KeyboardKeys);
			WriteH((short)playerConfig_0.ShowBlood);
			WriteC((byte)playerConfig_0.Crosshair);
			WriteC((byte)playerConfig_0.HandPosition);
			WriteD(playerConfig_0.Config);
			WriteD(playerConfig_0.AudioEnable);
			WriteH(0);
			WriteC((byte)playerConfig_0.AudioSFX);
			WriteC((byte)playerConfig_0.AudioBGM);
			WriteC((byte)playerConfig_0.PointOfView);
			WriteC(0);
			WriteC((byte)playerConfig_0.Sensitivity);
			WriteC((byte)playerConfig_0.InvertMouse);
			WriteH(0);
			WriteC((byte)playerConfig_0.EnableInviteMsg);
			WriteC((byte)playerConfig_0.EnableWhisperMsg);
			WriteD(playerConfig_0.Macro);
		}
	}
}
