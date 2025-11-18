using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200019B RID: 411
	public class PROTOCOL_CS_CREATE_CLAN_REQ : GameClientPacket
	{
		// Token: 0x0600043B RID: 1083 RVA: 0x000054A9 File Offset: 0x000036A9
		public override void Read()
		{
			base.ReadD();
			this.string_0 = base.ReadU(34);
			this.string_1 = base.ReadU(510);
			base.ReadD();
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00020EC8 File Offset: 0x0001F0C8
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					ClanModel clanModel = new ClanModel
					{
						Name = this.string_0,
						Info = this.string_1,
						Logo = 0U,
						OwnerId = player.PlayerId,
						CreationDate = uint.Parse(DateTimeUtil.Now("yyyyMMdd"))
					};
					if (player.ClanId <= 0 && DaoManagerSQL.GetRequestClanId(player.PlayerId) <= 0)
					{
						if (0 <= player.Gold - ConfigLoader.MinCreateGold && ConfigLoader.MinCreateRank <= player.Rank)
						{
							if (ClanManager.IsClanNameExist(clanModel.Name))
							{
								this.uint_0 = 2147487834U;
								return;
							}
							if (ClanManager.Clans.Count > ConfigLoader.MaxActiveClans)
							{
								this.uint_0 = 2147487829U;
							}
							else if (DaoManagerSQL.CreateClan(out clanModel.Id, clanModel.Name, clanModel.OwnerId, clanModel.Info, clanModel.CreationDate) && DaoManagerSQL.UpdateAccountGold(player.PlayerId, player.Gold - ConfigLoader.MinCreateGold))
							{
								clanModel.BestPlayers.SetDefault();
								player.ClanDate = clanModel.CreationDate;
								if (ComDiv.UpdateDB("accounts", "player_id", player.PlayerId, new string[] { "clan_access", "clan_date", "clan_id" }, new object[]
								{
									1,
									(long)((ulong)clanModel.CreationDate),
									clanModel.Id
								}))
								{
									if (clanModel.Id > 0)
									{
										player.ClanId = clanModel.Id;
										player.ClanAccess = 1;
										ClanManager.AddClan(clanModel);
										SendClanInfo.Load(clanModel, 0);
										player.Gold -= ConfigLoader.MinCreateGold;
									}
									else
									{
										this.uint_0 = 2147487819U;
									}
								}
								else
								{
									this.uint_0 = 2147487816U;
								}
							}
							else
							{
								this.uint_0 = 2147487816U;
							}
						}
						else
						{
							this.uint_0 = 2147487818U;
						}
					}
					else
					{
						this.uint_0 = 2147487836U;
					}
					this.Client.SendPacket(new PROTOCOL_CS_DETAIL_INFO_ACK((int)this.uint_0, clanModel));
					this.Client.SendPacket(new PROTOCOL_CS_CREATE_CLAN_ACK(this.uint_0, clanModel, player));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_CS_CREATE_CLAN_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CS_CREATE_CLAN_REQ()
		{
		}

		// Token: 0x040002FA RID: 762
		private uint uint_0;

		// Token: 0x040002FB RID: 763
		private string string_0;

		// Token: 0x040002FC RID: 764
		private string string_1;
	}
}
