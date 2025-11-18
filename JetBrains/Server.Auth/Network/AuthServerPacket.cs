// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.AuthServerPacket
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Microsoft.Win32.SafeHandles;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Auth.Data.Sync;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Auth.Network;

public abstract class AuthServerPacket : BaseServerPacket, IDisposable
{
  [CompilerGenerated]
  [SpecialName]
  public static void set_SocketSessions(ConcurrentDictionary<int, AuthClient> Packet)
  {
    // ISSUE: reference to a compiler-generated field
    AuthXender.concurrentDictionary_0 = Packet;
  }

  [CompilerGenerated]
  [SpecialName]
  public static ConcurrentDictionary<string, int> get_SocketConnections()
  {
    // ISSUE: reference to a compiler-generated field
    return AuthXender.concurrentDictionary_1;
  }

  [CompilerGenerated]
  [SpecialName]
  public static void set_SocketConnections(ConcurrentDictionary<string, int> PlayerId)
  {
    // ISSUE: reference to a compiler-generated field
    AuthXender.concurrentDictionary_1 = PlayerId;
  }

  public static bool GetPlugin(string object_0, [In] int obj1)
  {
    try
    {
      AuthServerPacket.set_SocketSessions(new ConcurrentDictionary<int, AuthClient>());
      AuthServerPacket.set_SocketConnections(new ConcurrentDictionary<string, int>());
      AuthXender.Sync = new AuthSync(SynchronizeXML.GetServer(obj1).Connection);
      AuthXender.Client = new AuthManager(0, object_0, ConfigLoader.DEFAULT_PORT[0]);
      AuthXender.Sync.Start();
      AuthXender.Client.Start();
      return true;
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public AuthServerPacket()
  {
  }

  public AuthServerPacket()
  {
    this.MStream = new MemoryStream();
    this.BWriter = new BinaryWriter((Stream) this.MStream);
    this.Handle = (SafeHandle) new SafeFileHandle(IntPtr.Zero, true);
    this.Disposed = false;
    this.SECURITY_KEY = Bitwise.CRYPTO[0];
    this.HASH_CODE = Bitwise.CRYPTO[1];
    this.SEED_LENGTH = Bitwise.CRYPTO[2];
    this.NATIONS = ConfigLoader.National;
  }
}
