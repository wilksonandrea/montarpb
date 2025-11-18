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

namespace Server.Game.Data.Commands
{
	// Token: 0x02000219 RID: 537
	public class ValuesCommand : ICommand
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x000063ED File Offset: 0x000045ED
		public string Command
		{
			get
			{
				return "player";
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x000063F4 File Offset: 0x000045F4
		public string Description
		{
			get
			{
				return "modify value of player";
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x000063D1 File Offset: 0x000045D1
		public string Permission
		{
			get
			{
				return "gamemastercommand";
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x000063FB File Offset: 0x000045FB
		public string Args
		{
			get
			{
				return "%options1% $options2% %value% %uid%";
			}
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0003913C File Offset: 0x0003733C
		public string Execute(string Command, string[] Args, Account Player)
		{
			string text = Args[0].ToLower();
			string text2 = Args[1].ToLower();
			int num = int.Parse(Args[2]);
			long num2;
			bool flag = long.TryParse(Args[3], out num2);
			if (text.Equals("gift"))
			{
				Account account = (flag ? AccountManager.GetAccount(num2, 0) : AccountManager.GetAccount(Args[3], 1, 0));
				if (!(text2 == "gold"))
				{
					if (!(text2 == "cash"))
					{
						if (!(text2 == "tags"))
						{
							if (text2 == "item")
							{
								if (account == null)
								{
									return "Player with " + (flag ? string.Format("UID: {0}", num2) : ("Nickname: " + Args[3])) + " doesn't Exist!";
								}
								GoodsItem good = ShopManager.GetGood(num);
								if (good == null)
								{
									return string.Format("Goods Id: {0} not founded!", num);
								}
								ItemsModel itemsModel = new ItemsModel(good.Item);
								if (itemsModel != null)
								{
									account.SendPacket(new PROTOCOL_BASE_NEW_REWARD_POPUP_ACK(Player, itemsModel));
									if (ComDiv.GetIdStatics(itemsModel.Id, 1) == 6 && Player.Character.GetCharacter(itemsModel.Id) == null)
									{
										AllUtils.CreateCharacter(Player, itemsModel);
									}
									else
									{
										account.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account, itemsModel));
									}
									return string.Format("{0} {1} To UID: {2} ({3})", new object[]
									{
										ComDiv.ToTitleCase(text),
										itemsModel.Name,
										account.PlayerId,
										account.Nickname
									});
								}
								return ComDiv.ToTitleCase(text) + " Command wrong or not founded!";
							}
						}
						else
						{
							if (account == null)
							{
								return "Player with " + (flag ? string.Format("UID: {0}", num2) : ("Nickname: " + Args[3])) + " doesn't Exist!";
							}
							if (DaoManagerSQL.UpdateAccountTags(account.PlayerId, account.Tags + num))
							{
								account.Tags += num;
								account.SendPacket(new PROTOCOL_AUTH_GET_POINT_CASH_ACK(0U, account));
								SendItemInfo.LoadGoldCash(account);
								return string.Format("{0} {1} Amount Of {2} To UID: {3} ({4})", new object[]
								{
									ComDiv.ToTitleCase(text),
									num,
									ComDiv.ToTitleCase(text2),
									account.PlayerId,
									account.Nickname
								});
							}
							return ComDiv.ToTitleCase(text) + " Command wrong or not founded!";
						}
					}
					else
					{
						if (account == null)
						{
							return "Player with " + (flag ? string.Format("UID: {0}", num2) : ("Nickname: " + Args[3])) + " doesn't Exist!";
						}
						if (DaoManagerSQL.UpdateAccountCash(account.PlayerId, account.Cash + num))
						{
							account.Cash += num;
							account.SendPacket(new PROTOCOL_AUTH_GET_POINT_CASH_ACK(0U, account));
							SendItemInfo.LoadGoldCash(account);
							return string.Format("{0} {1} Amount Of {2} To UID: {3} ({4})", new object[]
							{
								ComDiv.ToTitleCase(text),
								num,
								ComDiv.ToTitleCase(text2),
								account.PlayerId,
								account.Nickname
							});
						}
						return ComDiv.ToTitleCase(text) + " Command wrong or not founded!";
					}
				}
				else
				{
					if (account == null)
					{
						return "Player with " + (flag ? string.Format("UID: {0}", num2) : ("Nickname: " + Args[3])) + " doesn't Exist!";
					}
					if (DaoManagerSQL.UpdateAccountGold(account.PlayerId, account.Gold + num))
					{
						account.Gold += num;
						account.SendPacket(new PROTOCOL_AUTH_GET_POINT_CASH_ACK(0U, account));
						SendItemInfo.LoadGoldCash(account);
						return string.Format("{0} {1} Amount Of {2} To UID: {3} ({4})", new object[]
						{
							ComDiv.ToTitleCase(text),
							num,
							ComDiv.ToTitleCase(text2),
							account.PlayerId,
							account.Nickname
						});
					}
					return ComDiv.ToTitleCase(text) + " Command wrong or not founded!";
				}
			}
			else if (text.Equals("kick"))
			{
				if (!(text2 == "uid"))
				{
					if (text2 == "nick")
					{
						Account account2 = AccountManager.GetAccount(Args[3], 1, 0);
						if (account2 == null)
						{
							return "Player with Nickname: " + Args[3] + " doesn't Exist!";
						}
						if (account2.Nickname == Player.Nickname)
						{
							return "Player by Nickname: " + Args[3] + " failed! (Can't Kick Yourself)";
						}
						if (account2.Access > Player.Access)
						{
							return "Player by Nickname: " + Args[3] + " failed! (Can't Kick Higher Access Level Than Yours)";
						}
						account2.SendPacket(new PROTOCOL_AUTH_ACCOUNT_KICK_ACK(2), false);
						account2.Close(TimeSpan.FromSeconds((double)num).Milliseconds, true);
						return string.Format("{0} Player by Nickname: {1} Executed in {2} Seconds!", ComDiv.ToTitleCase(text), Args[3], num);
					}
				}
				else
				{
					Account account3 = AccountManager.GetAccount(num2, 0);
					if (account3 == null)
					{
						return string.Format("Player with UID: {0} doesn't Exist!", num2);
					}
					if (account3.PlayerId == Player.PlayerId)
					{
						return string.Format("Player by UID: {0} failed! (Can't Kick Yourself)", num2);
					}
					if (account3.Access > Player.Access)
					{
						return string.Format("Player by UID: {0} failed! (Can't Kick Higher Access Level Than Yours)", num2);
					}
					account3.SendPacket(new PROTOCOL_AUTH_ACCOUNT_KICK_ACK(2), false);
					account3.Close(TimeSpan.FromSeconds((double)num).Milliseconds, true);
					return string.Format("{0} Player by UID: {1} Executed in {2} Seconds!", ComDiv.ToTitleCase(text), num2, num);
				}
			}
			else if (text.Equals("ban"))
			{
				Account account4 = (flag ? AccountManager.GetAccount(num2, 0) : AccountManager.GetAccount(Args[3], 1, 0));
				if (!(text2 == "normal"))
				{
					if (text2 == "permanent")
					{
						if (account4 == null)
						{
							return "Player with " + (flag ? string.Format("UID: {0}", num2) : ("Nickname: " + Args[3])) + " doesn't Exist!";
						}
						if (account4.PlayerId == Player.PlayerId)
						{
							return "Player by " + (flag ? string.Format("UID: {0}", num2) : ("Nickname: " + Args[3])) + " failed! (Can't Kick Yourself)";
						}
						if (account4.Access > Player.Access)
						{
							return "Player by " + (flag ? string.Format("UID: {0}", num2) : ("Nickname: " + Args[3])) + " failed! (Can't Kick Higher Access Level Than Yours)";
						}
						double num3 = 999.0;
						BanHistory banHistory = DaoManagerSQL.SaveBanHistory(Player.PlayerId, "DURATION", string.Format("{0}", account4.PlayerId), DateTimeUtil.Now().AddDays(num3));
						if (banHistory != null)
						{
							using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK protocol_SERVER_MESSAGE_ANNOUNCE_ACK = new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK("Player '" + account4.Nickname + "' has been permanently Banned!"))
							{
								GameXender.Client.SendPacketToAllClients(protocol_SERVER_MESSAGE_ANNOUNCE_ACK);
							}
							account4.BanObjectId = banHistory.ObjectId;
							account4.SendPacket(new PROTOCOL_AUTH_ACCOUNT_KICK_ACK(2), false);
							account4.Close(1000, true);
							return string.Format("{0} {1} Success for {2} Day(s)", ComDiv.ToTitleCase(text), flag ? string.Format("UID: {0}", num2) : ("Nickname: " + Args[3]), num3);
						}
						return ComDiv.ToTitleCase(text) + " Command wrong or not founded!";
					}
				}
				else
				{
					if (account4 == null)
					{
						return "Player with " + (flag ? string.Format("UID: {0}", num2) : ("Nickname: " + Args[3])) + " doesn't Exist!";
					}
					if (account4.PlayerId == Player.PlayerId)
					{
						return "Player by " + (flag ? string.Format("UID: {0}", num2) : ("Nickname: " + Args[3])) + " failed! (Can't Kick Yourself)";
					}
					if (account4.Access > Player.Access)
					{
						return "Player by " + (flag ? string.Format("UID: {0}", num2) : ("Nickname: " + Args[3])) + " failed! (Can't Kick Higher Access Level Than Yours)";
					}
					double num4 = Convert.ToDouble(num);
					BanHistory banHistory2 = DaoManagerSQL.SaveBanHistory(Player.PlayerId, "DURATION", string.Format("{0}", account4.PlayerId), DateTimeUtil.Now().AddDays(num4));
					if (banHistory2 != null)
					{
						using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK protocol_SERVER_MESSAGE_ANNOUNCE_ACK2 = new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(string.Format("Player '{0}' has been banned for {1} Day(s)!", account4.Nickname, num4)))
						{
							GameXender.Client.SendPacketToAllClients(protocol_SERVER_MESSAGE_ANNOUNCE_ACK2);
						}
						account4.BanObjectId = banHistory2.ObjectId;
						account4.SendPacket(new PROTOCOL_AUTH_ACCOUNT_KICK_ACK(2), false);
						account4.Close(1000, true);
						return string.Format("{0} {1} Success for {2} Day(s)", ComDiv.ToTitleCase(text), flag ? string.Format("UID: {0}", num2) : ("Nickname: " + Args[3]), num4);
					}
					return ComDiv.ToTitleCase(text) + " Command wrong or not founded!";
				}
			}
			return "Command " + ComDiv.ToTitleCase(text) + " was not founded!";
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x000025DF File Offset: 0x000007DF
		public ValuesCommand()
		{
		}
	}
}
