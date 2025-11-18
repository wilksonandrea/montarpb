// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Commands.ValuesCommand
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Data.Commands;

public class ValuesCommand : ICommand
{
  [SpecialName]
  public string get_Description() => "Unlock all title";

  [SpecialName]
  public string get_Permission() => "hostcommand";

  [SpecialName]
  public string get_Args() => "";

  public string Execute([In] string obj0, string[] Args, Account Player)
  {
    if (Player.Title.OwnerId == 0L)
    {
      DaoManagerSQL.CreatePlayerTitlesDB(Player.PlayerId);
      Player.Title = new PlayerTitles()
      {
        OwnerId = Player.PlayerId
      };
    }
    PlayerTitles title1 = Player.Title;
    int num = 0;
    for (int TitleId = 1; TitleId <= 44; ++TitleId)
    {
      TitleModel title2 = TitleSystemXML.GetTitle(TitleId, true);
      if (title2 != null && !title1.Contains(title2.Flag))
      {
        ++num;
        title1.Add(title2.Flag);
        if (title1.Slots < title2.Slot)
          title1.Slots = title2.Slot;
      }
    }
    if (num > 0)
    {
      ComDiv.UpdateDB("player_titles", "slots", (object) title1.Slots, "owner_id", (object) Player.PlayerId);
      DaoManagerSQL.UpdatePlayerTitlesFlags(Player.PlayerId, title1.Flags);
      Player.SendPacket((GameServerPacket) new PROTOCOL_BATTLEBOX_AUTH_ACK(Player));
    }
    return "All title Successfully Opened!";
  }

  public string Command => "player";

  public string Description
  {
    [SpecialName] get => "modify value of player";
  }

  public string Permission
  {
    [SpecialName] get => "gamemastercommand";
  }

  public string Args
  {
    [SpecialName] get => "%options1% $options2% %value% %uid%";
  }
}
