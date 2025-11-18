using System.Collections.Generic;
using Plugin.Core.Models;
using Plugin.Core.XML;

namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_MAP_MATCHINGLIST_ACK : AuthServerPacket
{
	private readonly List<MapMatch> list_0;

	private readonly int int_0;

	public PROTOCOL_BASE_MAP_MATCHINGLIST_ACK(List<MapMatch> list_1, int int_1)
	{
		list_0 = list_1;
		int_0 = int_1;
	}

	public override void Write()
	{
		WriteH(2464);
		WriteH(0);
		WriteC((byte)list_0.Count);
		foreach (MapMatch item in list_0)
		{
			WriteD(item.Mode);
			WriteC((byte)item.Id);
			WriteC((byte)SystemMapXML.GetMapRule(item.Mode).Rule);
			WriteC((byte)SystemMapXML.GetMapRule(item.Mode).StageOptions);
			WriteC((byte)SystemMapXML.GetMapRule(item.Mode).Conditions);
			WriteC((byte)item.Limit);
			WriteC((byte)item.Tag);
			WriteC((byte)((item.Tag == 2 || item.Tag == 3) ? 1u : 0u));
			WriteC(1);
		}
		WriteD((list_0.Count != 100) ? 1 : 0);
		WriteH((ushort)(int_0 - 100));
		WriteH((ushort)SystemMapXML.Matches.Count);
	}
}
