// Decompiled with JetBrains decompiler
// Type: Server.Game.Data.Commands.TeamCashCommand
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
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

public class TeamCashCommand : ICommand
{
  [SpecialName]
  public string get_Description() => "change server settings";

  [SpecialName]
  public string get_Permission() => "developercommand";

  [SpecialName]
  public string get_Args() => "%options% %value%";

  public string Execute([In] string obj0, string[] Args, Account Player)
  {
    string lower1 = Args[0].ToLower();
    string lower2 = Args[1].ToLower();
    switch (lower1)
    {
      case "udp":
        int udpType = (int) ConfigLoader.UdpType;
        int num1 = int.Parse(lower2);
        if (num1.Equals(udpType))
          return $"UDP State Already Changed To: {udpType}";
        if (num1 < 1 || num1 > 4)
          return $"Cannot Change UDP State To: {num1}";
        switch (num1)
        {
          case 1:
            ConfigLoader.UdpType = (UdpState) num1;
            return $"{ComDiv.ToTitleCase(lower1)} State Changed ({num1} - {ConfigLoader.UdpType})";
          case 2:
            ConfigLoader.UdpType = (UdpState) num1;
            return $"{ComDiv.ToTitleCase(lower1)} State Changed ({num1} - {ConfigLoader.UdpType})";
          case 3:
            ConfigLoader.UdpType = (UdpState) num1;
            return $"{ComDiv.ToTitleCase(lower1)} State Changed ({num1} - {ConfigLoader.UdpType})";
          case 4:
            ConfigLoader.UdpType = (UdpState) num1;
            return $"{ComDiv.ToTitleCase(lower1)} State Changed ({num1} - {ConfigLoader.UdpType})";
          default:
            ConfigLoader.UdpType = UdpState.RELAY;
            return $"{ComDiv.ToTitleCase(lower1)} State Changed (3 - {ConfigLoader.UdpType}). Wrong Value";
        }
      case "debug":
        bool flag1 = int.Parse(lower2).Equals(1);
        if (flag1.Equals(ConfigLoader.DebugMode))
          return $"Debug Mode Already Changed To: {flag1}";
        if (flag1)
        {
          ConfigLoader.DebugMode = flag1;
          return $"{ComDiv.ToTitleCase(lower1)} Mode '{(flag1 ? "Enabled" : "Disabled")}'";
        }
        ConfigLoader.DebugMode = flag1;
        return $"{ComDiv.ToTitleCase(lower1)} Mode '{(flag1 ? "Enabled" : "Disabled")}'";
      case "test":
        bool flag2 = int.Parse(lower2).Equals(1);
        if (flag2.Equals(ConfigLoader.IsTestMode))
          return $"Test Mode Already Changed To: {flag2}";
        if (flag2)
        {
          ConfigLoader.IsTestMode = flag2;
          return $"{ComDiv.ToTitleCase(lower1)} Mode '{(flag2 ? "Enabled" : "Disabled")}'";
        }
        ConfigLoader.IsTestMode = flag2;
        return $"{ComDiv.ToTitleCase(lower1)} Mode '{(flag2 ? "Enabled" : "Disabled")}'";
      case "ping":
        bool flag3 = int.Parse(lower2).Equals(1);
        if (flag3.Equals(ConfigLoader.IsDebugPing))
          return $"Ping Mode Already Changed To: {flag3}";
        if (flag3)
        {
          ConfigLoader.IsDebugPing = flag3;
          return $"{ComDiv.ToTitleCase(lower1)} Mode '{(flag3 ? "Enabled" : "Disabled")}'";
        }
        ConfigLoader.IsDebugPing = flag3;
        return $"{ComDiv.ToTitleCase(lower1)} Mode '{(flag3 ? "Enabled" : "Disabled")}'";
      case "title":
        if (!lower2.Equals("all"))
          return ComDiv.ToTitleCase(lower1) + " Arguments was not valid!";
        if (Player.Title.OwnerId == 0L)
        {
          DaoManagerSQL.CreatePlayerTitlesDB(Player.PlayerId);
          Player.Title = new PlayerTitles()
          {
            OwnerId = Player.PlayerId
          };
        }
        PlayerTitles title1 = Player.Title;
        int num2 = 0;
        for (int TitleId = 1; TitleId <= 44; ++TitleId)
        {
          TitleModel title2 = TitleSystemXML.GetTitle(TitleId, true);
          if (title2 != null && !title1.Contains(title2.Flag))
          {
            ++num2;
            title1.Add(title2.Flag);
            if (title1.Slots < title2.Slot)
              title1.Slots = title2.Slot;
          }
        }
        if (num2 > 0)
        {
          ComDiv.UpdateDB("player_titles", "slots", (object) title1.Slots, "owner_id", (object) Player.PlayerId);
          DaoManagerSQL.UpdatePlayerTitlesFlags(Player.PlayerId, title1.Flags);
          Player.SendPacket((GameServerPacket) new PROTOCOL_BATTLEBOX_AUTH_ACK(Player));
        }
        return ComDiv.ToTitleCase(lower1) + " Successfully Opened!";
      default:
        return $"Command {ComDiv.ToTitleCase(lower1)} was not founded!";
    }
  }

  public string Command => "teamcash";

  public string Description
  {
    [SpecialName] get => "Send cash to a team";
  }

  public string Permission
  {
    [SpecialName] get => "gamemastercommand";
  }

  public string Args
  {
    [SpecialName] get => "%team% %cash% (Team = FR/CT)";
  }
}
