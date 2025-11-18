using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000191 RID: 401
	public class PROTOCOL_CS_CLAN_MATCH_RESULT_CONTEXT_REQ : GameClientPacket
	{
		// Token: 0x06000419 RID: 1049 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00020168 File Offset: 0x0001E368
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
								for (int i = 0; i < channel.Matches.Count; i++)
								{
									if (channel.Matches[i].Clan.Id == player.ClanId)
									{
										this.int_0++;
									}
								}
							}
						}
					}
					this.Client.SendPacket(new PROTOCOL_CS_CLAN_MATCH_RESULT_CONTEXT_ACK(this.int_0));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CS_CLAN_MATCH_RESULT_CONTEXT_REQ()
		{
		}

		// Token: 0x040002EC RID: 748
		private int int_0;
	}
}
