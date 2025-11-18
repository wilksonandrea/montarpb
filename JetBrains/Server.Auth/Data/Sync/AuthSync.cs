// Decompiled with JetBrains decompiler
// Type: Server.Auth.Data.Sync.AuthSync
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Auth.Data.Models;
using Server.Auth.Data.Sync.Server;
using Server.Auth.Data.Sync.Update;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;

#nullable disable
namespace Server.Auth.Data.Sync;

public class AuthSync
{
  protected Socket ClientSocket;
  private bool bool_0;

  public static List<ItemsModel> LimitationIndex(Account account_0, List<ItemsModel> int_0)
  {
    int num = 600 + account_0.InventoryPlus;
    if (int_0.Count > num)
    {
      int index = num / 3;
      if (int_0.Count > index)
        int_0.RemoveRange(index, int_0.Count - num);
    }
    return int_0;
  }

  private static void smethod_2(Account Player)
  {
    List<ItemsModel> items = Player.Inventory.Items;
    lock (items)
    {
      foreach (ItemsModel itemsModel in items)
      {
        if (ComDiv.GetIdStatics(itemsModel.Id, 1) == 6 && Player.Character.GetCharacter(itemsModel.Id) == null)
          AuthSync.CreateCharacter(Player, itemsModel);
      }
    }
  }

  public static void CreateCharacter(Account Player, [In] ItemsModel obj1)
  {
    CharacterModel OwnerId = new CharacterModel()
    {
      Id = obj1.Id,
      Name = obj1.Name,
      Slot = Player.Character.GenSlotId(obj1.Id),
      CreateDate = uint.Parse(DateTimeUtil.Now("yyMMddHHmm")),
      PlayTime = 0
    };
    Player.Character.AddCharacter(OwnerId);
    if (DaoManagerSQL.CreatePlayerCharacter(OwnerId, Player.PlayerId))
      return;
    CLogger.Print($"There is an error while cheating a character! (ID: {obj1.Id}", LoggerType.Warning, (Exception) null);
  }

  public static uint GetFeatures()
  {
    AccountFeatures features = AccountFeatures.ALL;
    if (!AuthXender.Client.Config.EnableClan)
      features -= AccountFeatures.CLAN_ONLY;
    if (!AuthXender.Client.Config.EnableTicket)
      features -= AccountFeatures.TICKET_ONLY;
    if (!AuthXender.Client.Config.EnableTags)
      features -= AccountFeatures.TAGS_ONLY;
    EventPlaytimeModel runningEvent = EventPlaytimeXML.GetRunningEvent();
    if (!AuthXender.Client.Config.EnablePlaytime || runningEvent == null || !runningEvent.EventIsEnabled())
      features -= AccountFeatures.PLAYTIME_ONLY;
    return (uint) features;
  }

  public static uint ValidateKey(long Player, int Items, [In] uint obj2)
  {
    return uint.Parse($"{(int) (obj2 % 999U):000}{(int) (Player % 999L):000}{Items % 999:000}");
  }

  public AuthSync(IPEndPoint Player)
  {
    this.ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    this.ClientSocket.Bind((EndPoint) Player);
    this.ClientSocket.IOControl(-1744830452 /*0x9800000C*/, new byte[1]
    {
      Convert.ToByte(false)
    }, (byte[]) null);
  }

  public bool Start()
  {
    try
    {
      IPEndPoint localEndPoint = this.ClientSocket.LocalEndPoint as IPEndPoint;
      CLogger.Print($"Auth Sync Address {localEndPoint.Address}:{localEndPoint.Port}", LoggerType.Info, (Exception) null);
      // ISSUE: reference to a compiler-generated method
      ThreadPool.QueueUserWorkItem(new WaitCallback(((ClanInfo) this).method_4));
      return true;
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  private void method_0()
  {
    if (this.bool_0)
      return;
    try
    {
      StateObject state = new StateObject()
      {
        WorkSocket = this.ClientSocket,
        RemoteEP = (EndPoint) new IPEndPoint(IPAddress.Any, 8000)
      };
      this.ClientSocket.BeginReceiveFrom(state.UdpBuffer, 0, 8096, SocketFlags.None, ref state.RemoteEP, new AsyncCallback(this.method_1), (object) state);
    }
    catch (ObjectDisposedException ex)
    {
      CLogger.Print("AuthSync socket disposed during StartReceive.", LoggerType.Warning, (Exception) null);
      ((ClanInfo) this).Close();
    }
    catch (Exception ex)
    {
      CLogger.Print("Error in StartReceive: " + ex.Message, LoggerType.Error, ex);
      ((ClanInfo) this).Close();
    }
  }

  private void method_1([In] IAsyncResult obj0)
  {
    // ISSUE: variable of a compiler-generated type
    AuthSync.Class3 class3 = (AuthSync.Class3) new ClanInfo();
    // ISSUE: reference to a compiler-generated field
    class3.authSync_0 = this;
    if (this.bool_0 || AuthXender.Client == null || AuthXender.Client.ServerIsClosed)
      return;
    StateObject asyncState = obj0.AsyncState as StateObject;
    try
    {
      int from = this.ClientSocket.EndReceiveFrom(obj0, ref asyncState.RemoteEP);
      if (from <= 0)
        return;
      // ISSUE: reference to a compiler-generated field
      class3.byte_0 = new byte[from];
      // ISSUE: reference to a compiler-generated field
      Array.Copy((Array) asyncState.UdpBuffer, 0, (Array) class3.byte_0, 0, from);
      ThreadPool.QueueUserWorkItem(new WaitCallback(((AuthLogin) class3).method_0));
    }
    catch (ObjectDisposedException ex)
    {
      CLogger.Print("AuthSync socket disposed during ReceiveCallback.", LoggerType.Warning, (Exception) null);
      ((ClanInfo) this).Close();
    }
    catch (Exception ex)
    {
      CLogger.Print("Error in ReceiveCallback: " + ex.Message, LoggerType.Error, ex);
    }
    finally
    {
      this.method_0();
    }
  }
}
