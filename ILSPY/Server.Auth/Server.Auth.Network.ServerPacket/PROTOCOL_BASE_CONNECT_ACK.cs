using System.Collections.Generic;
using Plugin.Core.Utility;

namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_CONNECT_ACK : AuthServerPacket
{
	private readonly int int_0;

	private readonly ushort ushort_0;

	private readonly List<byte[]> list_0;

	public PROTOCOL_BASE_CONNECT_ACK(AuthClient authClient_0)
	{
		int_0 = authClient_0.SessionId;
		ushort_0 = authClient_0.SessionSeed;
		list_0 = Bitwise.GenerateRSAKeyPair(int_0, SECURITY_KEY, SEED_LENGTH);
	}

	public override void Write()
	{
		WriteH(2306);
		WriteH(0);
		WriteC(11);
		WriteB(Bitwise.HexStringToByteArray("00 00 00 00 00 00 00 00 00 00 00"));
		WriteH((ushort)(list_0[0].Length + list_0[1].Length + 2));
		WriteH((ushort)list_0[0].Length);
		WriteB(list_0[0]);
		WriteB(list_0[1]);
		WriteC(3);
		WriteH(80);
		WriteH(ushort_0);
		WriteD(int_0);
	}
}
