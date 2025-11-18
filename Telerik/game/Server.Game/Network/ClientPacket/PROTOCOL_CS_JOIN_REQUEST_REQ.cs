using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_CS_JOIN_REQUEST_REQ : GameClientPacket
	{
		private int int_0;

		private string string_0;

		private uint uint_0;

		public PROTOCOL_CS_JOIN_REQUEST_REQ()
		{
		}

		public override void Read()
		{
			this.int_0 = base.ReadD();
			this.string_0 = base.ReadU(base.ReadC() * 2);
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					ClanInvite clanInvite = new ClanInvite()
					{
						Id = this.int_0,
						PlayerId = this.Client.PlayerId,
						Text = this.string_0,
						InviteDate = uint.Parse(DateTimeUtil.Now("yyyyMMdd"))
					};
					if (player.ClanId > 0 || player.Nickname.Length == 0)
					{
						this.uint_0 = -2147479460;
					}
					else if (ClanManager.GetClan(this.int_0).Id == 0)
					{
						this.uint_0 = -2147483648;
					}
					else if (DaoManagerSQL.GetRequestClanInviteCount(this.int_0) >= 100)
					{
						this.uint_0 = -2147479465;
					}
					else if (!DaoManagerSQL.CreateClanInviteInDB(clanInvite))
					{
						this.uint_0 = -2147479448;
					}
					clanInvite = null;
					this.Client.SendPacket(new PROTOCOL_CS_JOIN_REQUEST_ACK(this.uint_0, this.int_0));
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_CS_JOIN_REQUEST_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}