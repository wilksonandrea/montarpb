using Plugin.Core.Models.Map;
using Plugin.Core.Network;
using Plugin.Core.XML;
using Server.Auth.Network;
using System;
using System.Collections.Generic;

namespace Server.Auth.Network.ServerPacket
{
	public class PROTOCOL_BASE_MAP_RULELIST_ACK : AuthServerPacket
	{
		public PROTOCOL_BASE_MAP_RULELIST_ACK()
		{
		}

		public override void Write()
		{
			base.WriteH(2462);
			base.WriteH(0);
			base.WriteH((ushort)SystemMapXML.Rules.Count);
			foreach (MapRule rule in SystemMapXML.Rules)
			{
				base.WriteD(rule.Id);
				base.WriteC(0);
				base.WriteC((byte)rule.Rule);
				base.WriteC((byte)rule.StageOptions);
				base.WriteC((byte)rule.Conditions);
				base.WriteC(0);
				base.WriteS(rule.Name, 67);
			}
		}
	}
}