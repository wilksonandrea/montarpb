using System.Collections.Generic;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.XML;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_CONNECT_ACK : GameServerPacket
{
	private readonly int int_0;

	private readonly int int_1;

	private readonly ushort ushort_0;

	private readonly List<byte[]> list_0;

	public PROTOCOL_BASE_CONNECT_ACK(GameClient gameClient_0)
	{
		int_0 = gameClient_0.ServerId;
		int_1 = gameClient_0.SessionId;
		ushort_0 = gameClient_0.SessionSeed;
		list_0 = Bitwise.GenerateRSAKeyPair(int_1, SECURITY_KEY, SEED_LENGTH);
	}

	public override void Write()
	{
		WriteH(2306);
		WriteH(0);
		WriteC((byte)ChannelsXML.GetChannels(int_0).Count);
		foreach (ChannelModel channel in ChannelsXML.GetChannels(int_0))
		{
			WriteC((byte)channel.Type);
		}
		WriteH((ushort)(list_0[0].Length + list_0[1].Length + 2));
		WriteH((ushort)list_0[0].Length);
		WriteB(list_0[0]);
		WriteB(list_0[1]);
		WriteC(3);
		WriteH(80);
		WriteH(ushort_0);
		WriteD(int_1);
	}
}
