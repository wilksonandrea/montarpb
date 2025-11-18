using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Filters;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_BASE_CREATE_NICK_REQ : GameClientPacket
	{
		private string string_0;

		public PROTOCOL_BASE_CREATE_NICK_REQ()
		{
		}

		public override void Read()
		{
			this.string_0 = base.ReadU(base.ReadC() * 2);
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					if (player.Nickname.Length != 0 || string.IsNullOrEmpty(this.string_0) || this.string_0.Length < ConfigLoader.MinNickSize || this.string_0.Length > ConfigLoader.MaxNickSize)
					{
						this.Client.SendPacket(new PROTOCOL_BASE_CREATE_NICK_ACK(-2147483373, null));
					}
					else
					{
						List<string>.Enumerator enumerator = NickFilter.Filters.GetEnumerator();
						try
						{
							while (true)
							{
								if (enumerator.MoveNext())
								{
									string current = enumerator.Current;
									if (this.string_0.Contains(current))
									{
										this.Client.SendPacket(new PROTOCOL_BASE_CREATE_NICK_ACK(-2147479533, null));
										break;
									}
								}
								else
								{
									break;
								}
							}
						}
						finally
						{
							((IDisposable)enumerator).Dispose();
						}
						if (DaoManagerSQL.IsPlayerNameExist(this.string_0))
						{
							this.Client.SendPacket(new PROTOCOL_BASE_CREATE_NICK_ACK(-2147483373, null));
						}
						else if (!AccountManager.UpdatePlayerName(this.string_0, player.PlayerId))
						{
							this.Client.SendPacket(new PROTOCOL_BASE_CREATE_NICK_ACK(-2147479533, null));
						}
						else
						{
							DaoManagerSQL.CreatePlayerNickHistory(player.PlayerId, player.Nickname, this.string_0, "First nick choosed");
							player.Nickname = this.string_0;
							List<ItemsModel> basics = TemplatePackXML.Basics;
							if (basics.Count > 0)
							{
								foreach (ItemsModel basic in basics)
								{
									if (ComDiv.GetIdStatics(basic.Id, 1) != 6 || player.Character.GetCharacter(basic.Id) != null)
									{
										continue;
									}
									AllUtils.CreateCharacter(player, basic);
								}
							}
							List<ItemsModel> awards = TemplatePackXML.Awards;
							if (awards.Count > 0)
							{
								foreach (ItemsModel award in awards)
								{
									if (ComDiv.GetIdStatics(award.Id, 1) != 6 || player.Character.GetCharacter(award.Id) != null)
									{
										this.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, player, award));
									}
									else
									{
										AllUtils.CreateCharacter(player, award);
									}
								}
							}
							this.Client.SendPacket(new PROTOCOL_BASE_CREATE_NICK_ACK(0, player));
							this.Client.SendPacket(new PROTOCOL_SHOP_PLUS_POINT_ACK(player.Gold, player.Gold, 4));
							this.Client.SendPacket(new PROTOCOL_BASE_QUEST_GET_INFO_ACK(player));
							this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_ACK(player.Friend.Friends));
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_LOBBY_CREATE_NICK_NAME_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}