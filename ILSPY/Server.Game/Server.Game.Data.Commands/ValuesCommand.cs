using System;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Data.Commands;

public class ValuesCommand : ICommand
{
	public string Command => "player";

	public string Description => "modify value of player";

	public string Permission => "gamemastercommand";

	public string Args => "%options1% $options2% %value% %uid%";

	public string Execute(string Command, string[] Args, Account Player)
	{
		string text = Args[0].ToLower();
		string text2 = Args[1].ToLower();
		int num = int.Parse(Args[2]);
		long result;
		bool flag = long.TryParse(Args[3], out result);
		if (text.Equals("gift"))
		{
			Account account = (flag ? AccountManager.GetAccount(result, 0) : AccountManager.GetAccount(Args[3], 1, 0));
			switch (text2)
			{
			case "item":
			{
				if (account == null)
				{
					return "Player with " + (flag ? $"UID: {result}" : ("Nickname: " + Args[3])) + " doesn't Exist!";
				}
				GoodsItem good = ShopManager.GetGood(num);
				if (good == null)
				{
					return $"Goods Id: {num} not founded!";
				}
				ItemsModel ıtemsModel = new ItemsModel(good.Item);
				if (ıtemsModel != null)
				{
					account.SendPacket(new PROTOCOL_BASE_NEW_REWARD_POPUP_ACK(Player, ıtemsModel));
					if (ComDiv.GetIdStatics(ıtemsModel.Id, 1) == 6 && Player.Character.GetCharacter(ıtemsModel.Id) == null)
					{
						AllUtils.CreateCharacter(Player, ıtemsModel);
					}
					else
					{
						account.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account, ıtemsModel));
					}
					return $"{ComDiv.ToTitleCase(text)} {ıtemsModel.Name} To UID: {account.PlayerId} ({account.Nickname})";
				}
				return ComDiv.ToTitleCase(text) + " Command wrong or not founded!";
			}
			case "tags":
				if (account == null)
				{
					return "Player with " + (flag ? $"UID: {result}" : ("Nickname: " + Args[3])) + " doesn't Exist!";
				}
				if (DaoManagerSQL.UpdateAccountTags(account.PlayerId, account.Tags + num))
				{
					account.Tags += num;
					account.SendPacket(new PROTOCOL_AUTH_GET_POINT_CASH_ACK(0u, account));
					SendItemInfo.LoadGoldCash(account);
					return $"{ComDiv.ToTitleCase(text)} {num} Amount Of {ComDiv.ToTitleCase(text2)} To UID: {account.PlayerId} ({account.Nickname})";
				}
				return ComDiv.ToTitleCase(text) + " Command wrong or not founded!";
			case "cash":
				if (account == null)
				{
					return "Player with " + (flag ? $"UID: {result}" : ("Nickname: " + Args[3])) + " doesn't Exist!";
				}
				if (DaoManagerSQL.UpdateAccountCash(account.PlayerId, account.Cash + num))
				{
					account.Cash += num;
					account.SendPacket(new PROTOCOL_AUTH_GET_POINT_CASH_ACK(0u, account));
					SendItemInfo.LoadGoldCash(account);
					return $"{ComDiv.ToTitleCase(text)} {num} Amount Of {ComDiv.ToTitleCase(text2)} To UID: {account.PlayerId} ({account.Nickname})";
				}
				return ComDiv.ToTitleCase(text) + " Command wrong or not founded!";
			case "gold":
				if (account == null)
				{
					return "Player with " + (flag ? $"UID: {result}" : ("Nickname: " + Args[3])) + " doesn't Exist!";
				}
				if (DaoManagerSQL.UpdateAccountGold(account.PlayerId, account.Gold + num))
				{
					account.Gold += num;
					account.SendPacket(new PROTOCOL_AUTH_GET_POINT_CASH_ACK(0u, account));
					SendItemInfo.LoadGoldCash(account);
					return $"{ComDiv.ToTitleCase(text)} {num} Amount Of {ComDiv.ToTitleCase(text2)} To UID: {account.PlayerId} ({account.Nickname})";
				}
				return ComDiv.ToTitleCase(text) + " Command wrong or not founded!";
			}
		}
		else if (text.Equals("kick"))
		{
			if (text2 == "uid")
			{
				Account account2 = AccountManager.GetAccount(result, 0);
				if (account2 == null)
				{
					return $"Player with UID: {result} doesn't Exist!";
				}
				if (account2.PlayerId == Player.PlayerId)
				{
					return $"Player by UID: {result} failed! (Can't Kick Yourself)";
				}
				if (account2.Access > Player.Access)
				{
					return $"Player by UID: {result} failed! (Can't Kick Higher Access Level Than Yours)";
				}
				account2.SendPacket(new PROTOCOL_AUTH_ACCOUNT_KICK_ACK(2), OnlyInServer: false);
				account2.Close(TimeSpan.FromSeconds(num).Milliseconds, kicked: true);
				return $"{ComDiv.ToTitleCase(text)} Player by UID: {result} Executed in {num} Seconds!";
			}
			if (text2 == "nick")
			{
				Account account3 = AccountManager.GetAccount(Args[3], 1, 0);
				if (account3 == null)
				{
					return "Player with Nickname: " + Args[3] + " doesn't Exist!";
				}
				if (account3.Nickname == Player.Nickname)
				{
					return "Player by Nickname: " + Args[3] + " failed! (Can't Kick Yourself)";
				}
				if (account3.Access > Player.Access)
				{
					return "Player by Nickname: " + Args[3] + " failed! (Can't Kick Higher Access Level Than Yours)";
				}
				account3.SendPacket(new PROTOCOL_AUTH_ACCOUNT_KICK_ACK(2), OnlyInServer: false);
				account3.Close(TimeSpan.FromSeconds(num).Milliseconds, kicked: true);
				return $"{ComDiv.ToTitleCase(text)} Player by Nickname: {Args[3]} Executed in {num} Seconds!";
			}
		}
		else if (text.Equals("ban"))
		{
			Account account4 = (flag ? AccountManager.GetAccount(result, 0) : AccountManager.GetAccount(Args[3], 1, 0));
			if (text2 == "normal")
			{
				if (account4 == null)
				{
					return "Player with " + (flag ? $"UID: {result}" : ("Nickname: " + Args[3])) + " doesn't Exist!";
				}
				if (account4.PlayerId == Player.PlayerId)
				{
					return "Player by " + (flag ? $"UID: {result}" : ("Nickname: " + Args[3])) + " failed! (Can't Kick Yourself)";
				}
				if (account4.Access > Player.Access)
				{
					return "Player by " + (flag ? $"UID: {result}" : ("Nickname: " + Args[3])) + " failed! (Can't Kick Higher Access Level Than Yours)";
				}
				double num2 = Convert.ToDouble(num);
				BanHistory banHistory = DaoManagerSQL.SaveBanHistory(Player.PlayerId, "DURATION", $"{account4.PlayerId}", DateTimeUtil.Now().AddDays(num2));
				if (banHistory != null)
				{
					using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK packet = new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK($"Player '{account4.Nickname}' has been banned for {num2} Day(s)!"))
					{
						GameXender.Client.SendPacketToAllClients(packet);
					}
					account4.BanObjectId = banHistory.ObjectId;
					account4.SendPacket(new PROTOCOL_AUTH_ACCOUNT_KICK_ACK(2), OnlyInServer: false);
					account4.Close(1000, kicked: true);
					return string.Format("{0} {1} Success for {2} Day(s)", ComDiv.ToTitleCase(text), flag ? $"UID: {result}" : ("Nickname: " + Args[3]), num2);
				}
				return ComDiv.ToTitleCase(text) + " Command wrong or not founded!";
			}
			if (text2 == "permanent")
			{
				if (account4 == null)
				{
					return "Player with " + (flag ? $"UID: {result}" : ("Nickname: " + Args[3])) + " doesn't Exist!";
				}
				if (account4.PlayerId == Player.PlayerId)
				{
					return "Player by " + (flag ? $"UID: {result}" : ("Nickname: " + Args[3])) + " failed! (Can't Kick Yourself)";
				}
				if (account4.Access > Player.Access)
				{
					return "Player by " + (flag ? $"UID: {result}" : ("Nickname: " + Args[3])) + " failed! (Can't Kick Higher Access Level Than Yours)";
				}
				double num3 = 999.0;
				BanHistory banHistory2 = DaoManagerSQL.SaveBanHistory(Player.PlayerId, "DURATION", $"{account4.PlayerId}", DateTimeUtil.Now().AddDays(num3));
				if (banHistory2 != null)
				{
					using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK packet2 = new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK("Player '" + account4.Nickname + "' has been permanently Banned!"))
					{
						GameXender.Client.SendPacketToAllClients(packet2);
					}
					account4.BanObjectId = banHistory2.ObjectId;
					account4.SendPacket(new PROTOCOL_AUTH_ACCOUNT_KICK_ACK(2), OnlyInServer: false);
					account4.Close(1000, kicked: true);
					return string.Format("{0} {1} Success for {2} Day(s)", ComDiv.ToTitleCase(text), flag ? $"UID: {result}" : ("Nickname: " + Args[3]), num3);
				}
				return ComDiv.ToTitleCase(text) + " Command wrong or not founded!";
			}
		}
		return "Command " + ComDiv.ToTitleCase(text) + " was not founded!";
	}
}
