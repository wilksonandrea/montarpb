// Decompiled with JetBrains decompiler
// Type: Server.Auth.Network.ClientPacket.PROTOCOL_BASE_GET_USER_INFO_REQ
// Assembly: Server.Auth, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8C1FF7A1-F90C-426E-867E-2F2C9B107BC5
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Auth.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;

#nullable disable
namespace Server.Auth.Network.ClientPacket;

public class PROTOCOL_BASE_GET_USER_INFO_REQ : AuthClientPacket
{
  private void method_3(MessageModel messageModel_0, SyncServerPacket syncServerPacket_0)
  {
    syncServerPacket_0.WriteC((byte) (messageModel_0.SenderName.Length + 1));
    syncServerPacket_0.WriteC(messageModel_0.Type == NoteMessageType.Insert || messageModel_0.Type == NoteMessageType.ClanAsk || messageModel_0.Type == NoteMessageType.Clan && messageModel_0.ClanNote != NoteMessageClan.None ? (byte) 0 : (byte) (messageModel_0.Text.Length + 1));
    syncServerPacket_0.WriteN(messageModel_0.SenderName, messageModel_0.SenderName.Length + 2, "UTF-16LE");
    if (messageModel_0.Type != NoteMessageType.ClanAsk && messageModel_0.Type != NoteMessageType.Clan)
      syncServerPacket_0.WriteN(messageModel_0.Text, messageModel_0.Text.Length + 2, "UTF-16LE");
    else if (messageModel_0.ClanNote >= NoteMessageClan.JoinAccept && messageModel_0.ClanNote <= NoteMessageClan.Secession)
    {
      syncServerPacket_0.WriteH((short) (messageModel_0.Text.Length + 1));
      syncServerPacket_0.WriteH((short) messageModel_0.ClanNote);
      syncServerPacket_0.WriteN(messageModel_0.Text, messageModel_0.Text.Length + 1, "UTF-16LE");
    }
    else if (messageModel_0.ClanNote == NoteMessageClan.None)
    {
      syncServerPacket_0.WriteN(messageModel_0.Text, messageModel_0.Text.Length + 2, "UTF-16LE");
    }
    else
    {
      syncServerPacket_0.WriteH((short) 3);
      syncServerPacket_0.WriteD((int) messageModel_0.ClanNote);
      if (messageModel_0.ClanNote == NoteMessageClan.Master && messageModel_0.ClanNote == NoteMessageClan.Staff && messageModel_0.ClanNote == NoteMessageClan.Regular)
        return;
      syncServerPacket_0.WriteH((short) 0);
    }
  }

  public virtual void Read()
  {
    int num = (int) this.ReadC();
  }
}
