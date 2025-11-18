using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BASE_GET_USER_SUBTASK_ACK : GameServerPacket
	{
		private readonly Account account_0;

		private readonly int int_0;

		public PROTOCOL_BASE_GET_USER_SUBTASK_ACK(PlayerSession playerSession_0)
		{
			this.account_0 = AccountManager.GetAccount(playerSession_0.PlayerId, true);
			this.int_0 = playerSession_0.SessionId;
		}

		public override void Write()
		{
			base.WriteH(2448);
			base.WriteD(this.int_0);
			base.WriteH(0);
			base.WriteC(0);
			base.WriteD(this.int_0);
			base.WriteC(0);
		}
	}
}