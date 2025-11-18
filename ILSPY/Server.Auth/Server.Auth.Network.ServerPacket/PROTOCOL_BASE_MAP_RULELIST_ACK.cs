using Plugin.Core.Models.Map;
using Plugin.Core.XML;

namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_MAP_RULELIST_ACK : AuthServerPacket
{
	public override void Write()
	{
		WriteH(2462);
		WriteH(0);
		WriteH((ushort)SystemMapXML.Rules.Count);
		foreach (MapRule rule in SystemMapXML.Rules)
		{
			WriteD(rule.Id);
			WriteC(0);
			WriteC((byte)rule.Rule);
			WriteC((byte)rule.StageOptions);
			WriteC((byte)rule.Conditions);
			WriteC(0);
			WriteS(rule.Name, 67);
		}
	}
}
