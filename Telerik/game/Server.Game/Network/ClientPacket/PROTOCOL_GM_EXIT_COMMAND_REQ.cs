using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_GM_EXIT_COMMAND_REQ : GameClientPacket
	{
		private byte byte_0;

		public PROTOCOL_GM_EXIT_COMMAND_REQ()
		{
		}

		public override void Read()
		{
			this.byte_0 = base.ReadC();
		}

		public override void Run()
		{
		}
	}
}