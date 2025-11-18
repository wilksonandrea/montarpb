// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Utility.StateObject
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Utility;

public class StateObject
{
  public Socket WorkSocket;
  public EndPoint RemoteEP;
  public const int TcpBufferSize = 8912;
  public const int UdpBufferSize = 8096;
  public byte[] TcpBuffer;
  public byte[] UdpBuffer;

  public void Add([In] T0 obj0)
  {
    lock (((SafeList<T0>) this).object_0)
      ((SafeList<T0>) this).list_0.Add(obj0);
  }
}
