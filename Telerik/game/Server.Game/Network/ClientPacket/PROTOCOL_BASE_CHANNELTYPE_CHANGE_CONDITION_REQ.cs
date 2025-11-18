using Server.Game;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_REQ : GameClientPacket
	{
		public PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_REQ()
		{
		}

		public override void Read()
		{
		}

		public override void Run()
		{
			this.Client.SendPacket(new PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_ACK());
		}
	}
}