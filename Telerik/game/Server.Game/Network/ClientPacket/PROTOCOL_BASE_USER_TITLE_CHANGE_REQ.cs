using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_BASE_USER_TITLE_CHANGE_REQ : GameClientPacket
	{
		private int int_0;

		private uint uint_0;

		public PROTOCOL_BASE_USER_TITLE_CHANGE_REQ()
		{
		}

		public override void Read()
		{
			this.int_0 = base.ReadC();
		}

		public override void Run()
		{
			TitleModel titleModel;
			TitleModel titleModel1;
			try
			{
				Account player = this.Client.Player;
				if (player != null && this.int_0 < 45)
				{
					if (player.Title.OwnerId == 0)
					{
						DaoManagerSQL.CreatePlayerTitlesDB(player.PlayerId);
						player.Title = new PlayerTitles()
						{
							OwnerId = player.PlayerId
						};
					}
					TitleModel title = TitleSystemXML.GetTitle(this.int_0, true);
					if (title == null)
					{
						this.uint_0 = -2147479421;
					}
					else
					{
						TitleSystemXML.Get2Titles(title.Req1, title.Req2, out titleModel, out titleModel1, false);
						if ((title.Req1 == 0 || titleModel != null) && (title.Req2 == 0 || titleModel1 != null) && player.Rank >= title.Rank && player.Ribbon >= title.Ribbon && player.Medal >= title.Medal && player.MasterMedal >= title.MasterMedal && player.Ensign >= title.Ensign && !player.Title.Contains(title.Flag) && player.Title.Contains(titleModel.Flag) && player.Title.Contains(titleModel1.Flag))
						{
							player.Ribbon -= title.Ribbon;
							player.Medal -= title.Medal;
							player.MasterMedal -= title.MasterMedal;
							player.Ensign -= title.Ensign;
							long ınt64 = player.Title.Add(title.Flag);
							DaoManagerSQL.UpdatePlayerTitlesFlags(player.PlayerId, ınt64);
							List<ItemsModel> awards = TitleAwardXML.GetAwards(this.int_0);
							if (awards.Count > 0)
							{
								this.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, player, awards));
							}
							this.Client.SendPacket(new PROTOCOL_BASE_MEDAL_GET_INFO_ACK(player));
							DaoManagerSQL.UpdatePlayerTitleRequi(player.PlayerId, player.Medal, player.Ensign, player.MasterMedal, player.Ribbon);
							if (player.Title.Slots < title.Slot)
							{
								player.Title.Slots = title.Slot;
								ComDiv.UpdateDB("player_titles", "slots", player.Title.Slots, "owner_id", player.PlayerId);
							}
						}
						else
						{
							this.uint_0 = -2147479421;
						}
					}
					this.Client.SendPacket(new PROTOCOL_BASE_USER_TITLE_CHANGE_ACK(this.uint_0, player.Title.Slots));
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_BASE_USER_TITLE_CHANGE_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}