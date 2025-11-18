using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000192 RID: 402
	public class PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_REQ : GameClientPacket
	{
		// Token: 0x0600041C RID: 1052 RVA: 0x0000541A File Offset: 0x0000361A
		public override void Read()
		{
			this.int_0 = (int)base.ReadC();
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00020250 File Offset: 0x0001E450
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					if (player.ClanId > 0)
					{
						ChannelModel channel = player.GetChannel();
						if (channel != null && channel.Type == ChannelType.Clan)
						{
							List<MatchModel> matches = channel.Matches;
							lock (matches)
							{
								foreach (MatchModel matchModel in channel.Matches)
								{
									if (matchModel.Clan.Id == player.ClanId)
									{
										this.list_0.Add(matchModel);
									}
								}
							}
						}
					}
					this.Client.SendPacket(new PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_ACK((player.ClanId == 0) ? 91 : 0, this.list_0));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00005428 File Offset: 0x00003628
		public PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_REQ()
		{
		}

		// Token: 0x040002ED RID: 749
		private List<MatchModel> list_0 = new List<MatchModel>();

		// Token: 0x040002EE RID: 750
		private int int_0;
	}
}
