// Decompiled with JetBrains decompiler
// Type: dummy_ptr.{b1cb866f-593a-4275-8e64-84a52aa2e6d8}
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace dummy_ptr;

internal abstract class \u007Bb1cb866f\u002D593a\u002D4275\u002D8e64\u002D84a52aa2e6d8\u007D
{
  [SpecialName]
  public string get_Description() => "modify value of player";

  [SpecialName]
  public string get_Permission() => "gamemastercommand";

  [SpecialName]
  public string get_Args() => "%options1% $options2% %value% %uid%";

  public string Execute([In] string obj0, string[] Args, Account Player)
  {
    string lower1 = Args[0].ToLower();
    string lower2 = Args[1].ToLower();
    int num1 = int.Parse(Args[2]);
    long result;
    bool flag = long.TryParse(Args[3], out result);
    switch (lower1)
    {
      case "gift":
        Account account1 = flag ? ClanManager.GetAccount(result, 0) : ClanManager.GetAccount(Args[3], 1, 0);
        switch (lower2)
        {
          case "gold":
            if (account1 == null)
              return $"Player with {(flag ? $"UID: {result}" : "Nickname: " + Args[3])} doesn't Exist!";
            if (!DaoManagerSQL.UpdateAccountGold(account1.PlayerId, account1.Gold + num1))
              return ComDiv.ToTitleCase(lower1) + " Command wrong or not founded!";
            account1.Gold += num1;
            account1.SendPacket((GameServerPacket) new PROTOCOL_AUTH_RECV_WHISPER_ACK(0U, account1));
            UpdateServer.LoadGoldCash(account1);
            return $"{ComDiv.ToTitleCase(lower1)} {num1} Amount Of {ComDiv.ToTitleCase(lower2)} To UID: {account1.PlayerId} ({account1.Nickname})";
          case "cash":
            if (account1 == null)
              return $"Player with {(flag ? $"UID: {result}" : "Nickname: " + Args[3])} doesn't Exist!";
            if (!DaoManagerSQL.UpdateAccountCash(account1.PlayerId, account1.Cash + num1))
              return ComDiv.ToTitleCase(lower1) + " Command wrong or not founded!";
            account1.Cash += num1;
            account1.SendPacket((GameServerPacket) new PROTOCOL_AUTH_RECV_WHISPER_ACK(0U, account1));
            UpdateServer.LoadGoldCash(account1);
            return $"{ComDiv.ToTitleCase(lower1)} {num1} Amount Of {ComDiv.ToTitleCase(lower2)} To UID: {account1.PlayerId} ({account1.Nickname})";
          case "tags":
            if (account1 == null)
              return $"Player with {(flag ? $"UID: {result}" : "Nickname: " + Args[3])} doesn't Exist!";
            if (!DaoManagerSQL.UpdateAccountTags(account1.PlayerId, account1.Tags + num1))
              return ComDiv.ToTitleCase(lower1) + " Command wrong or not founded!";
            account1.Tags += num1;
            account1.SendPacket((GameServerPacket) new PROTOCOL_AUTH_RECV_WHISPER_ACK(0U, account1));
            UpdateServer.LoadGoldCash(account1);
            return $"{ComDiv.ToTitleCase(lower1)} {num1} Amount Of {ComDiv.ToTitleCase(lower2)} To UID: {account1.PlayerId} ({account1.Nickname})";
          case "item":
            if (account1 == null)
              return $"Player with {(flag ? $"UID: {result}" : "Nickname: " + Args[3])} doesn't Exist!";
            GoodsItem good = ShopManager.GetGood(num1);
            if (good == null)
              return $"Goods Id: {num1} not founded!";
            ItemsModel itemsModel = new ItemsModel(good.Item);
            if (itemsModel == null)
              return ComDiv.ToTitleCase(lower1) + " Command wrong or not founded!";
            account1.SendPacket((GameServerPacket) new PROTOCOL_BASE_RANK_UP_ACK(Player, itemsModel));
            if (ComDiv.GetIdStatics(itemsModel.Id, 1) == 6 && Player.Character.GetCharacter(itemsModel.Id) == null)
              AllUtils.CreateCharacter(Player, itemsModel);
            else
              account1.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_CHATTING_ACK(0, account1, itemsModel));
            return $"{ComDiv.ToTitleCase(lower1)} {itemsModel.Name} To UID: {account1.PlayerId} ({account1.Nickname})";
        }
        break;
      case "kick":
        switch (lower2)
        {
          case "uid":
            Account account2 = ClanManager.GetAccount(result, 0);
            if (account2 == null)
              return $"Player with UID: {result} doesn't Exist!";
            if (account2.PlayerId == Player.PlayerId)
              return $"Player by UID: {result} failed! (Can't Kick Yourself)";
            if (account2.Access > Player.Access)
              return $"Player by UID: {result} failed! (Can't Kick Higher Access Level Than Yours)";
            account2.SendPacket((GameServerPacket) new PROTOCOL_AUTH_FIND_USER_ACK(2), false);
            account2.Close(TimeSpan.FromSeconds((double) num1).Milliseconds, true);
            return $"{ComDiv.ToTitleCase(lower1)} Player by UID: {result} Executed in {num1} Seconds!";
          case "nick":
            Account account3 = ClanManager.GetAccount(Args[3], 1, 0);
            if (account3 == null)
              return $"Player with Nickname: {Args[3]} doesn't Exist!";
            if (account3.Nickname == Player.Nickname)
              return $"Player by Nickname: {Args[3]} failed! (Can't Kick Yourself)";
            if (account3.Access > Player.Access)
              return $"Player by Nickname: {Args[3]} failed! (Can't Kick Higher Access Level Than Yours)";
            account3.SendPacket((GameServerPacket) new PROTOCOL_AUTH_FIND_USER_ACK(2), false);
            account3.Close(TimeSpan.FromSeconds((double) num1).Milliseconds, true);
            return $"{ComDiv.ToTitleCase(lower1)} Player by Nickname: {Args[3]} Executed in {num1} Seconds!";
        }
        break;
      case "ban":
        Account account4 = flag ? ClanManager.GetAccount(result, 0) : ClanManager.GetAccount(Args[3], 1, 0);
        switch (lower2)
        {
          case "normal":
            if (account4 == null)
              return $"Player with {(flag ? $"UID: {result}" : "Nickname: " + Args[3])} doesn't Exist!";
            if (account4.PlayerId == Player.PlayerId)
              return $"Player by {(flag ? $"UID: {result}" : "Nickname: " + Args[3])} failed! (Can't Kick Yourself)";
            if (account4.Access > Player.Access)
              return $"Player by {(flag ? $"UID: {result}" : "Nickname: " + Args[3])} failed! (Can't Kick Higher Access Level Than Yours)";
            double num2 = Convert.ToDouble(num1);
            BanHistory banHistory1 = DaoManagerSQL.SaveBanHistory(Player.PlayerId, "DURATION", $"{account4.PlayerId}", DateTimeUtil.Now().AddDays(num2));
            if (banHistory1 == null)
              return ComDiv.ToTitleCase(lower1) + " Command wrong or not founded!";
            using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK iasyncResult_0 = (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK) new PROTOCOL_SERVER_MESSAGE_DISCONNECTED_HACK($"Player '{account4.Nickname}' has been banned for {num2} Day(s)!"))
              GameXender.Client.SendPacketToAllClients((GameServerPacket) iasyncResult_0);
            account4.BanObjectId = banHistory1.ObjectId;
            account4.SendPacket((GameServerPacket) new PROTOCOL_AUTH_FIND_USER_ACK(2), false);
            account4.Close(1000, true);
            return $"{ComDiv.ToTitleCase(lower1)} {(flag ? (object) $"UID: {result}" : (object) ("Nickname: " + Args[3]))} Success for {num2} Day(s)";
          case "permanent":
            if (account4 == null)
              return $"Player with {(flag ? $"UID: {result}" : "Nickname: " + Args[3])} doesn't Exist!";
            if (account4.PlayerId == Player.PlayerId)
              return $"Player by {(flag ? $"UID: {result}" : "Nickname: " + Args[3])} failed! (Can't Kick Yourself)";
            if (account4.Access > Player.Access)
              return $"Player by {(flag ? $"UID: {result}" : "Nickname: " + Args[3])} failed! (Can't Kick Higher Access Level Than Yours)";
            double num3 = 999.0;
            BanHistory banHistory2 = DaoManagerSQL.SaveBanHistory(Player.PlayerId, "DURATION", $"{account4.PlayerId}", DateTimeUtil.Now().AddDays(num3));
            if (banHistory2 == null)
              return ComDiv.ToTitleCase(lower1) + " Command wrong or not founded!";
            using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK iasyncResult_0 = (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK) new PROTOCOL_SERVER_MESSAGE_DISCONNECTED_HACK($"Player '{account4.Nickname}' has been permanently Banned!"))
              GameXender.Client.SendPacketToAllClients((GameServerPacket) iasyncResult_0);
            account4.BanObjectId = banHistory2.ObjectId;
            account4.SendPacket((GameServerPacket) new PROTOCOL_AUTH_FIND_USER_ACK(2), false);
            account4.Close(1000, true);
            return $"{ComDiv.ToTitleCase(lower1)} {(flag ? (object) $"UID: {result}" : (object) ("Nickname: " + Args[3]))} Success for {num3} Day(s)";
        }
        break;
    }
    return $"Command {ComDiv.ToTitleCase(lower1)} was not founded!";
  }

  public abstract void mp000001();

  public abstract void mp000002();

  public abstract void mp000003();

  public abstract void mp000004();

  public abstract void mp000005();
}
