using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Data.Commands
{
	public class ValuesCommand : ICommand
	{
		public string Args
		{
			get
			{
				return "%options1% $options2% %value% %uid%";
			}
		}

		public string Command
		{
			get
			{
				return "player";
			}
		}

		public string Description
		{
			get
			{
				return "modify value of player";
			}
		}

		public string Permission
		{
			get
			{
				return "gamemastercommand";
			}
		}

		public ValuesCommand()
		{
		}

		public string Execute(string Command, string[] Args, Account Player)
		{
			long ınt64;
			TimeSpan timeSpan;
			DateTime dateTime;
			string lower = Args[0].ToLower();
			string str = Args[1].ToLower();
			int ınt32 = int.Parse(Args[2]);
			bool flag = long.TryParse(Args[3], out ınt64);
			if (lower.Equals("gift"))
			{
				Account account = (flag ? AccountManager.GetAccount(ınt64, 0) : AccountManager.GetAccount(Args[3], 1, 0));
				if (str == "gold")
				{
					if (account == null)
					{
						return string.Concat("Player with ", (flag ? string.Format("UID: {0}", ınt64) : string.Concat("Nickname: ", Args[3])), " doesn't Exist!");
					}
					if (!DaoManagerSQL.UpdateAccountGold(account.PlayerId, account.Gold + ınt32))
					{
						return string.Concat(ComDiv.ToTitleCase(lower), " Command wrong or not founded!");
					}
					account.Gold += ınt32;
					account.SendPacket(new PROTOCOL_AUTH_GET_POINT_CASH_ACK(0, account));
					SendItemInfo.LoadGoldCash(account);
					return string.Format("{0} {1} Amount Of {2} To UID: {3} ({4})", new object[] { ComDiv.ToTitleCase(lower), ınt32, ComDiv.ToTitleCase(str), account.PlayerId, account.Nickname });
				}
				if (str == "cash")
				{
					if (account == null)
					{
						return string.Concat("Player with ", (flag ? string.Format("UID: {0}", ınt64) : string.Concat("Nickname: ", Args[3])), " doesn't Exist!");
					}
					if (!DaoManagerSQL.UpdateAccountCash(account.PlayerId, account.Cash + ınt32))
					{
						return string.Concat(ComDiv.ToTitleCase(lower), " Command wrong or not founded!");
					}
					account.Cash += ınt32;
					account.SendPacket(new PROTOCOL_AUTH_GET_POINT_CASH_ACK(0, account));
					SendItemInfo.LoadGoldCash(account);
					return string.Format("{0} {1} Amount Of {2} To UID: {3} ({4})", new object[] { ComDiv.ToTitleCase(lower), ınt32, ComDiv.ToTitleCase(str), account.PlayerId, account.Nickname });
				}
				if (str == "tags")
				{
					if (account == null)
					{
						return string.Concat("Player with ", (flag ? string.Format("UID: {0}", ınt64) : string.Concat("Nickname: ", Args[3])), " doesn't Exist!");
					}
					if (!DaoManagerSQL.UpdateAccountTags(account.PlayerId, account.Tags + ınt32))
					{
						return string.Concat(ComDiv.ToTitleCase(lower), " Command wrong or not founded!");
					}
					account.Tags += ınt32;
					account.SendPacket(new PROTOCOL_AUTH_GET_POINT_CASH_ACK(0, account));
					SendItemInfo.LoadGoldCash(account);
					return string.Format("{0} {1} Amount Of {2} To UID: {3} ({4})", new object[] { ComDiv.ToTitleCase(lower), ınt32, ComDiv.ToTitleCase(str), account.PlayerId, account.Nickname });
				}
				if (str == "item")
				{
					if (account == null)
					{
						return string.Concat("Player with ", (flag ? string.Format("UID: {0}", ınt64) : string.Concat("Nickname: ", Args[3])), " doesn't Exist!");
					}
					GoodsItem good = ShopManager.GetGood(ınt32);
					if (good == null)
					{
						return string.Format("Goods Id: {0} not founded!", ınt32);
					}
					ItemsModel ıtemsModel = new ItemsModel(good.Item);
					if (ıtemsModel == null)
					{
						return string.Concat(ComDiv.ToTitleCase(lower), " Command wrong or not founded!");
					}
					account.SendPacket(new PROTOCOL_BASE_NEW_REWARD_POPUP_ACK(Player, ıtemsModel));
					if (ComDiv.GetIdStatics(ıtemsModel.Id, 1) != 6 || Player.Character.GetCharacter(ıtemsModel.Id) != null)
					{
						account.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account, ıtemsModel));
					}
					else
					{
						AllUtils.CreateCharacter(Player, ıtemsModel);
					}
					return string.Format("{0} {1} To UID: {2} ({3})", new object[] { ComDiv.ToTitleCase(lower), ıtemsModel.Name, account.PlayerId, account.Nickname });
				}
			}
			else if (lower.Equals("kick"))
			{
				if (str == "uid")
				{
					Account account1 = AccountManager.GetAccount(ınt64, 0);
					if (account1 == null)
					{
						return string.Format("Player with UID: {0} doesn't Exist!", ınt64);
					}
					if (account1.PlayerId == Player.PlayerId)
					{
						return string.Format("Player by UID: {0} failed! (Can't Kick Yourself)", ınt64);
					}
					if (account1.Access > Player.Access)
					{
						return string.Format("Player by UID: {0} failed! (Can't Kick Higher Access Level Than Yours)", ınt64);
					}
					account1.SendPacket(new PROTOCOL_AUTH_ACCOUNT_KICK_ACK(2), false);
					timeSpan = TimeSpan.FromSeconds((double)ınt32);
					account1.Close(timeSpan.Milliseconds, true);
					return string.Format("{0} Player by UID: {1} Executed in {2} Seconds!", ComDiv.ToTitleCase(lower), ınt64, ınt32);
				}
				if (str == "nick")
				{
					Account account2 = AccountManager.GetAccount(Args[3], 1, 0);
					if (account2 == null)
					{
						return string.Concat("Player with Nickname: ", Args[3], " doesn't Exist!");
					}
					if (account2.Nickname == Player.Nickname)
					{
						return string.Concat("Player by Nickname: ", Args[3], " failed! (Can't Kick Yourself)");
					}
					if (account2.Access > Player.Access)
					{
						return string.Concat("Player by Nickname: ", Args[3], " failed! (Can't Kick Higher Access Level Than Yours)");
					}
					account2.SendPacket(new PROTOCOL_AUTH_ACCOUNT_KICK_ACK(2), false);
					timeSpan = TimeSpan.FromSeconds((double)ınt32);
					account2.Close(timeSpan.Milliseconds, true);
					return string.Format("{0} Player by Nickname: {1} Executed in {2} Seconds!", ComDiv.ToTitleCase(lower), Args[3], ınt32);
				}
			}
			else if (lower.Equals("ban"))
			{
				Account objectId = (flag ? AccountManager.GetAccount(ınt64, 0) : AccountManager.GetAccount(Args[3], 1, 0));
				if (str == "normal")
				{
					if (objectId == null)
					{
						return string.Concat("Player with ", (flag ? string.Format("UID: {0}", ınt64) : string.Concat("Nickname: ", Args[3])), " doesn't Exist!");
					}
					if (objectId.PlayerId == Player.PlayerId)
					{
						return string.Concat("Player by ", (flag ? string.Format("UID: {0}", ınt64) : string.Concat("Nickname: ", Args[3])), " failed! (Can't Kick Yourself)");
					}
					if (objectId.Access > Player.Access)
					{
						return string.Concat("Player by ", (flag ? string.Format("UID: {0}", ınt64) : string.Concat("Nickname: ", Args[3])), " failed! (Can't Kick Higher Access Level Than Yours)");
					}
					double num = Convert.ToDouble(ınt32);
					long playerId = Player.PlayerId;
					string str1 = string.Format("{0}", objectId.PlayerId);
					dateTime = DateTimeUtil.Now();
					BanHistory banHistory = DaoManagerSQL.SaveBanHistory(playerId, "DURATION", str1, dateTime.AddDays(num));
					if (banHistory == null)
					{
						return string.Concat(ComDiv.ToTitleCase(lower), " Command wrong or not founded!");
					}
					using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK pROTOCOLSERVERMESSAGEANNOUNCEACK = new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(string.Format("Player '{0}' has been banned for {1} Day(s)!", objectId.Nickname, num)))
					{
						GameXender.Client.SendPacketToAllClients(pROTOCOLSERVERMESSAGEANNOUNCEACK);
					}
					objectId.BanObjectId = banHistory.ObjectId;
					objectId.SendPacket(new PROTOCOL_AUTH_ACCOUNT_KICK_ACK(2), false);
					objectId.Close(1000, true);
					return string.Format("{0} {1} Success for {2} Day(s)", ComDiv.ToTitleCase(lower), (flag ? string.Format("UID: {0}", ınt64) : string.Concat("Nickname: ", Args[3])), num);
				}
				if (str == "permanent")
				{
					if (objectId == null)
					{
						return string.Concat("Player with ", (flag ? string.Format("UID: {0}", ınt64) : string.Concat("Nickname: ", Args[3])), " doesn't Exist!");
					}
					if (objectId.PlayerId == Player.PlayerId)
					{
						return string.Concat("Player by ", (flag ? string.Format("UID: {0}", ınt64) : string.Concat("Nickname: ", Args[3])), " failed! (Can't Kick Yourself)");
					}
					if (objectId.Access > Player.Access)
					{
						return string.Concat("Player by ", (flag ? string.Format("UID: {0}", ınt64) : string.Concat("Nickname: ", Args[3])), " failed! (Can't Kick Higher Access Level Than Yours)");
					}
					double num1 = 999;
					long playerId1 = Player.PlayerId;
					string str2 = string.Format("{0}", objectId.PlayerId);
					dateTime = DateTimeUtil.Now();
					BanHistory banHistory1 = DaoManagerSQL.SaveBanHistory(playerId1, "DURATION", str2, dateTime.AddDays(num1));
					if (banHistory1 == null)
					{
						return string.Concat(ComDiv.ToTitleCase(lower), " Command wrong or not founded!");
					}
					using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK pROTOCOLSERVERMESSAGEANNOUNCEACK1 = new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(string.Concat("Player '", objectId.Nickname, "' has been permanently Banned!")))
					{
						GameXender.Client.SendPacketToAllClients(pROTOCOLSERVERMESSAGEANNOUNCEACK1);
					}
					objectId.BanObjectId = banHistory1.ObjectId;
					objectId.SendPacket(new PROTOCOL_AUTH_ACCOUNT_KICK_ACK(2), false);
					objectId.Close(1000, true);
					return string.Format("{0} {1} Success for {2} Day(s)", ComDiv.ToTitleCase(lower), (flag ? string.Format("UID: {0}", ınt64) : string.Concat("Nickname: ", Args[3])), num1);
				}
			}
			return string.Concat("Command ", ComDiv.ToTitleCase(lower), " was not founded!");
		}
	}
}