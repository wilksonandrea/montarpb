// Decompiled with JetBrains decompiler
// Type: Server.Match.Network.Actions.SubHead.ObjectMove
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core.Network;

#nullable disable
namespace Server.Match.Network.Actions.SubHead;

public class ObjectMove
{
  public ObjectMove()
  {
  }

  public static byte[] GET_CODE()
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      syncServerPacket.WriteC((byte) 66);
      syncServerPacket.WriteC((byte) 0);
      syncServerPacket.WriteT(0.0f);
      syncServerPacket.WriteC((byte) 0);
      syncServerPacket.WriteH((short) 14);
      syncServerPacket.WriteD(0);
      syncServerPacket.WriteC((byte) 8);
      return syncServerPacket.ToArray();
    }
  }

  public ObjectMove()
  {
  }
}
