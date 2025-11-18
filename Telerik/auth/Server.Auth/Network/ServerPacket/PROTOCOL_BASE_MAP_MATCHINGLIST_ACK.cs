using Plugin.Core.Models;
using Plugin.Core.Models.Map;
using Plugin.Core.Network;
using Plugin.Core.XML;
using Server.Auth.Network;
using System;
using System.Collections.Generic;

namespace Server.Auth.Network.ServerPacket
{
	public class PROTOCOL_BASE_MAP_MATCHINGLIST_ACK : AuthServerPacket
	{
		private readonly List<MapMatch> list_0;

		private readonly int int_0;

		public PROTOCOL_BASE_MAP_MATCHINGLIST_ACK(List<MapMatch> list_1, int int_1)
		{
			this.list_0 = list_1;
			this.int_0 = int_1;
		}

		public override void Write()
		{
			object obj;
			base.WriteH(2464);
			base.WriteH(0);
			base.WriteC((byte)this.list_0.Count);
			foreach (MapMatch list0 in this.list_0)
			{
				base.WriteD(list0.Mode);
				base.WriteC((byte)list0.Id);
				base.WriteC((byte)SystemMapXML.GetMapRule(list0.Mode).Rule);
				base.WriteC((byte)SystemMapXML.GetMapRule(list0.Mode).StageOptions);
				base.WriteC((byte)SystemMapXML.GetMapRule(list0.Mode).Conditions);
				base.WriteC((byte)list0.Limit);
				base.WriteC((byte)list0.Tag);
				if (list0.Tag == 2 || list0.Tag == 3)
				{
					obj = 1;
				}
				else
				{
					obj = null;
				}
				base.WriteC((byte)obj);
				base.WriteC(1);
			}
			base.WriteD(this.list_0.Count != 100);
			base.WriteH((ushort)(this.int_0 - 100));
			base.WriteH((ushort)SystemMapXML.Matches.Count);
		}
	}
}