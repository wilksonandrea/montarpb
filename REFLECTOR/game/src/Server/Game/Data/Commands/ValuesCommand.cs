namespace Server.Game.Data.Commands
{
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

    public class ValuesCommand : ICommand
    {
        public string Execute(string Command, string[] Args, Account Player)
        {
            long num2;
            string text = Args[0].ToLower();
            string str2 = Args[1].ToLower();
            int goodId = int.Parse(Args[2]);
            bool flag = long.TryParse(Args[3], out num2);
            if (text.Equals("gift"))
            {
                Account account = flag ? AccountManager.GetAccount(num2, 0) : AccountManager.GetAccount(Args[3], 1, 0);
                if (str2 == "gold")
                {
                    if (account == null)
                    {
                        return ("Player with " + (flag ? $"UID: {num2}" : ("Nickname: " + Args[3])) + " doesn't Exist!");
                    }
                    if (!DaoManagerSQL.UpdateAccountGold(account.PlayerId, account.Gold + goodId))
                    {
                        return (ComDiv.ToTitleCase(text) + " Command wrong or not founded!");
                    }
                    account.Gold += goodId;
                    account.SendPacket(new PROTOCOL_AUTH_GET_POINT_CASH_ACK(0, account));
                    SendItemInfo.LoadGoldCash(account);
                    return $"{ComDiv.ToTitleCase(text)} {goodId} Amount Of {ComDiv.ToTitleCase(str2)} To UID: {account.PlayerId} ({account.Nickname})";
                }
                if (str2 == "cash")
                {
                    if (account == null)
                    {
                        return ("Player with " + (flag ? $"UID: {num2}" : ("Nickname: " + Args[3])) + " doesn't Exist!");
                    }
                    if (!DaoManagerSQL.UpdateAccountCash(account.PlayerId, account.Cash + goodId))
                    {
                        return (ComDiv.ToTitleCase(text) + " Command wrong or not founded!");
                    }
                    account.Cash += goodId;
                    account.SendPacket(new PROTOCOL_AUTH_GET_POINT_CASH_ACK(0, account));
                    SendItemInfo.LoadGoldCash(account);
                    return $"{ComDiv.ToTitleCase(text)} {goodId} Amount Of {ComDiv.ToTitleCase(str2)} To UID: {account.PlayerId} ({account.Nickname})";
                }
                if (str2 == "tags")
                {
                    if (account == null)
                    {
                        return ("Player with " + (flag ? $"UID: {num2}" : ("Nickname: " + Args[3])) + " doesn't Exist!");
                    }
                    if (!DaoManagerSQL.UpdateAccountTags(account.PlayerId, account.Tags + goodId))
                    {
                        return (ComDiv.ToTitleCase(text) + " Command wrong or not founded!");
                    }
                    account.Tags += goodId;
                    account.SendPacket(new PROTOCOL_AUTH_GET_POINT_CASH_ACK(0, account));
                    SendItemInfo.LoadGoldCash(account);
                    return $"{ComDiv.ToTitleCase(text)} {goodId} Amount Of {ComDiv.ToTitleCase(str2)} To UID: {account.PlayerId} ({account.Nickname})";
                }
                if (str2 == "item")
                {
                    if (account == null)
                    {
                        return ("Player with " + (flag ? $"UID: {num2}" : ("Nickname: " + Args[3])) + " doesn't Exist!");
                    }
                    GoodsItem good = ShopManager.GetGood(goodId);
                    if (good == null)
                    {
                        return $"Goods Id: {goodId} not founded!";
                    }
                    ItemsModel model = new ItemsModel(good.Item);
                    if (model == null)
                    {
                        return (ComDiv.ToTitleCase(text) + " Command wrong or not founded!");
                    }
                    account.SendPacket(new PROTOCOL_BASE_NEW_REWARD_POPUP_ACK(Player, model));
                    if ((ComDiv.GetIdStatics(model.Id, 1) == 6) && (Player.Character.GetCharacter(model.Id) == null))
                    {
                        AllUtils.CreateCharacter(Player, model);
                    }
                    else
                    {
                        account.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account, model));
                    }
                    return $"{ComDiv.ToTitleCase(text)} {model.Name} To UID: {account.PlayerId} ({account.Nickname})";
                }
            }
            else if (text.Equals("kick"))
            {
                if (str2 == "uid")
                {
                    Account account = AccountManager.GetAccount(num2, 0);
                    if (account == null)
                    {
                        return $"Player with UID: {num2} doesn't Exist!";
                    }
                    if (account.PlayerId == Player.PlayerId)
                    {
                        return $"Player by UID: {num2} failed! (Can't Kick Yourself)";
                    }
                    if (account.Access > Player.Access)
                    {
                        return $"Player by UID: {num2} failed! (Can't Kick Higher Access Level Than Yours)";
                    }
                    account.SendPacket(new PROTOCOL_AUTH_ACCOUNT_KICK_ACK(2), false);
                    account.Close(TimeSpan.FromSeconds((double) goodId).Milliseconds, true);
                    return $"{ComDiv.ToTitleCase(text)} Player by UID: {num2} Executed in {goodId} Seconds!";
                }
                if (str2 == "nick")
                {
                    Account account3 = AccountManager.GetAccount(Args[3], 1, 0);
                    if (account3 == null)
                    {
                        return ("Player with Nickname: " + Args[3] + " doesn't Exist!");
                    }
                    if (account3.Nickname == Player.Nickname)
                    {
                        return ("Player by Nickname: " + Args[3] + " failed! (Can't Kick Yourself)");
                    }
                    if (account3.Access > Player.Access)
                    {
                        return ("Player by Nickname: " + Args[3] + " failed! (Can't Kick Higher Access Level Than Yours)");
                    }
                    account3.SendPacket(new PROTOCOL_AUTH_ACCOUNT_KICK_ACK(2), false);
                    account3.Close(TimeSpan.FromSeconds((double) goodId).Milliseconds, true);
                    return $"{ComDiv.ToTitleCase(text)} Player by Nickname: {Args[3]} Executed in {goodId} Seconds!";
                }
            }
            else if (text.Equals("ban"))
            {
                Account account4 = flag ? AccountManager.GetAccount(num2, 0) : AccountManager.GetAccount(Args[3], 1, 0);
                if (str2 == "normal")
                {
                    if (account4 == null)
                    {
                        return ("Player with " + (flag ? $"UID: {num2}" : ("Nickname: " + Args[3])) + " doesn't Exist!");
                    }
                    if (account4.PlayerId == Player.PlayerId)
                    {
                        return ("Player by " + (flag ? $"UID: {num2}" : ("Nickname: " + Args[3])) + " failed! (Can't Kick Yourself)");
                    }
                    if (account4.Access > Player.Access)
                    {
                        return ("Player by " + (flag ? $"UID: {num2}" : ("Nickname: " + Args[3])) + " failed! (Can't Kick Higher Access Level Than Yours)");
                    }
                    double num3 = Convert.ToDouble(goodId);
                    BanHistory history = DaoManagerSQL.SaveBanHistory(Player.PlayerId, "DURATION", $"{account4.PlayerId}", DateTimeUtil.Now().AddDays(num3));
                    if (history == null)
                    {
                        return (ComDiv.ToTitleCase(text) + " Command wrong or not founded!");
                    }
                    using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK protocol_server_message_announce_ack = new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK($"Player '{account4.Nickname}' has been banned for {num3} Day(s)!"))
                    {
                        GameXender.Client.SendPacketToAllClients(protocol_server_message_announce_ack);
                    }
                    account4.BanObjectId = history.ObjectId;
                    account4.SendPacket(new PROTOCOL_AUTH_ACCOUNT_KICK_ACK(2), false);
                    account4.Close(0x3e8, true);
                    return $"{ComDiv.ToTitleCase(text)} {(flag ? $"UID: {num2}" : ("Nickname: " + Args[3]))} Success for {num3} Day(s)";
                }
                if (str2 == "permanent")
                {
                    if (account4 == null)
                    {
                        return ("Player with " + (flag ? $"UID: {num2}" : ("Nickname: " + Args[3])) + " doesn't Exist!");
                    }
                    if (account4.PlayerId == Player.PlayerId)
                    {
                        return ("Player by " + (flag ? $"UID: {num2}" : ("Nickname: " + Args[3])) + " failed! (Can't Kick Yourself)");
                    }
                    if (account4.Access > Player.Access)
                    {
                        return ("Player by " + (flag ? $"UID: {num2}" : ("Nickname: " + Args[3])) + " failed! (Can't Kick Higher Access Level Than Yours)");
                    }
                    double num4 = 999.0;
                    BanHistory history2 = DaoManagerSQL.SaveBanHistory(Player.PlayerId, "DURATION", $"{account4.PlayerId}", DateTimeUtil.Now().AddDays(num4));
                    if (history2 == null)
                    {
                        return (ComDiv.ToTitleCase(text) + " Command wrong or not founded!");
                    }
                    using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK protocol_server_message_announce_ack2 = new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK("Player '" + account4.Nickname + "' has been permanently Banned!"))
                    {
                        GameXender.Client.SendPacketToAllClients(protocol_server_message_announce_ack2);
                    }
                    account4.BanObjectId = history2.ObjectId;
                    account4.SendPacket(new PROTOCOL_AUTH_ACCOUNT_KICK_ACK(2), false);
                    account4.Close(0x3e8, true);
                    return $"{ComDiv.ToTitleCase(text)} {(flag ? $"UID: {num2}" : ("Nickname: " + Args[3]))} Success for {num4} Day(s)";
                }
            }
            return ("Command " + ComDiv.ToTitleCase(text) + " was not founded!");
        }

        public string Command =>
            "player";

        public string Description =>
            "modify value of player";

        public string Permission =>
            "gamemastercommand";

        public string Args =>
            "%options1% $options2% %value% %uid%";
    }
}

