using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK : GameServerPacket
	{
		private readonly Account account_0;

		private readonly ulong ulong_0;

		public PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK(Account account_1)
		{
			this.account_0 = account_1;
			if (account_1 != null)
			{
				this.ulong_0 = ComDiv.GetClanStatus(account_1.Status, account_1.IsOnline);
			}
		}

		public PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK(Account account_1, FriendState friendState_0)
		{
			this.account_0 = account_1;
			if (account_1 != null)
			{
				this.ulong_0 = (friendState_0 == FriendState.None ? ComDiv.GetClanStatus(account_1.Status, account_1.IsOnline) : ComDiv.GetClanStatus(friendState_0));
			}
		}

		public override void Write()
		{
			base.WriteH(851);
			base.WriteQ(this.account_0.PlayerId);
			base.WriteQ(this.ulong_0);
		}
	}
}