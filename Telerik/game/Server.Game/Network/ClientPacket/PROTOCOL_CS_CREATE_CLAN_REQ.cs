using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_CS_CREATE_CLAN_REQ : GameClientPacket
	{
		private uint uint_0;

		private string string_0;

		private string string_1;

		public PROTOCOL_CS_CREATE_CLAN_REQ()
		{
		}

		public override void Read()
		{
			base.ReadD();
			this.string_0 = base.ReadU(34);
			this.string_1 = base.ReadU(510);
			base.ReadD();
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					ClanModel clanModel = new ClanModel()
					{
						Name = this.string_0,
						Info = this.string_1,
						Logo = 0,
						OwnerId = player.PlayerId,
						CreationDate = uint.Parse(DateTimeUtil.Now("yyyyMMdd"))
					};
					if (player.ClanId > 0 || DaoManagerSQL.GetRequestClanId(player.PlayerId) > 0)
					{
						this.uint_0 = -2147479460;
					}
					else if (0 > player.Gold - ConfigLoader.MinCreateGold || ConfigLoader.MinCreateRank > player.Rank)
					{
						this.uint_0 = -2147479478;
					}
					else if (ClanManager.IsClanNameExist(clanModel.Name))
					{
						this.uint_0 = -2147479462;
						return;
					}
					else if (ClanManager.Clans.Count > ConfigLoader.MaxActiveClans)
					{
						this.uint_0 = -2147479467;
					}
					else if (!DaoManagerSQL.CreateClan(out clanModel.Id, clanModel.Name, clanModel.OwnerId, clanModel.Info, clanModel.CreationDate) || !DaoManagerSQL.UpdateAccountGold(player.PlayerId, player.Gold - ConfigLoader.MinCreateGold))
					{
						this.uint_0 = -2147479480;
					}
					else
					{
						clanModel.BestPlayers.SetDefault();
						player.ClanDate = clanModel.CreationDate;
						if (!ComDiv.UpdateDB("accounts", "player_id", player.PlayerId, new string[] { "clan_access", "clan_date", "clan_id" }, new object[] { 1, (long)((ulong)clanModel.CreationDate), clanModel.Id }))
						{
							this.uint_0 = -2147479480;
						}
						else if (clanModel.Id <= 0)
						{
							this.uint_0 = -2147479477;
						}
						else
						{
							player.ClanId = clanModel.Id;
							player.ClanAccess = 1;
							ClanManager.AddClan(clanModel);
							SendClanInfo.Load(clanModel, 0);
							player.Gold -= ConfigLoader.MinCreateGold;
						}
					}
					this.Client.SendPacket(new PROTOCOL_CS_DETAIL_INFO_ACK((int)this.uint_0, clanModel));
					this.Client.SendPacket(new PROTOCOL_CS_CREATE_CLAN_ACK(this.uint_0, clanModel, player));
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_CS_CREATE_CLAN_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}