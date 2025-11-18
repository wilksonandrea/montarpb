using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Filters;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200013A RID: 314
	public class PROTOCOL_BASE_CREATE_NICK_REQ : GameClientPacket
	{
		// Token: 0x0600030C RID: 780 RVA: 0x00004F2A File Offset: 0x0000312A
		public override void Read()
		{
			this.string_0 = base.ReadU((int)(base.ReadC() * 2));
		}

		// Token: 0x0600030D RID: 781 RVA: 0x000188EC File Offset: 0x00016AEC
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					if (player.Nickname.Length == 0 && !string.IsNullOrEmpty(this.string_0) && this.string_0.Length >= ConfigLoader.MinNickSize && this.string_0.Length <= ConfigLoader.MaxNickSize)
					{
						foreach (string text in NickFilter.Filters)
						{
							if (this.string_0.Contains(text))
							{
								this.Client.SendPacket(new PROTOCOL_BASE_CREATE_NICK_ACK(2147487763U, null));
								break;
							}
						}
						if (!DaoManagerSQL.IsPlayerNameExist(this.string_0))
						{
							if (AccountManager.UpdatePlayerName(this.string_0, player.PlayerId))
							{
								DaoManagerSQL.CreatePlayerNickHistory(player.PlayerId, player.Nickname, this.string_0, "First nick choosed");
								player.Nickname = this.string_0;
								List<ItemsModel> basics = TemplatePackXML.Basics;
								if (basics.Count > 0)
								{
									foreach (ItemsModel itemsModel in basics)
									{
										if (ComDiv.GetIdStatics(itemsModel.Id, 1) == 6 && player.Character.GetCharacter(itemsModel.Id) == null)
										{
											AllUtils.CreateCharacter(player, itemsModel);
										}
									}
								}
								List<ItemsModel> awards = TemplatePackXML.Awards;
								if (awards.Count > 0)
								{
									foreach (ItemsModel itemsModel2 in awards)
									{
										if (ComDiv.GetIdStatics(itemsModel2.Id, 1) == 6 && player.Character.GetCharacter(itemsModel2.Id) == null)
										{
											AllUtils.CreateCharacter(player, itemsModel2);
										}
										else
										{
											this.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, player, itemsModel2));
										}
									}
								}
								this.Client.SendPacket(new PROTOCOL_BASE_CREATE_NICK_ACK(0U, player));
								this.Client.SendPacket(new PROTOCOL_SHOP_PLUS_POINT_ACK(player.Gold, player.Gold, 4));
								this.Client.SendPacket(new PROTOCOL_BASE_QUEST_GET_INFO_ACK(player));
								this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_ACK(player.Friend.Friends));
							}
							else
							{
								this.Client.SendPacket(new PROTOCOL_BASE_CREATE_NICK_ACK(2147487763U, null));
							}
						}
						else
						{
							this.Client.SendPacket(new PROTOCOL_BASE_CREATE_NICK_ACK(2147483923U, null));
						}
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_BASE_CREATE_NICK_ACK(2147483923U, null));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_LOBBY_CREATE_NICK_NAME_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BASE_CREATE_NICK_REQ()
		{
		}

		// Token: 0x0400023C RID: 572
		private string string_0;
	}
}
