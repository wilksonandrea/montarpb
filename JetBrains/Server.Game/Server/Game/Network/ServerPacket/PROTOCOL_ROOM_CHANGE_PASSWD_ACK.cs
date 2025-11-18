// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_ROOM_CHANGE_PASSWD_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_CHANGE_PASSWD_ACK : GameServerPacket
{
  private readonly string string_0;

  public byte[] NoteClanData(MessageModel short_1)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      if (short_1.Type == NoteMessageType.Normal || short_1.Type == NoteMessageType.ClanInfo)
      {
        syncServerPacket.WriteC((byte) (short_1.SenderName.Length + 2));
        syncServerPacket.WriteC((byte) (short_1.Text.Length + 2));
        syncServerPacket.WriteN(short_1.SenderName, short_1.SenderName.Length + 2, "UTF-16LE");
        if (short_1.ClanNote == NoteMessageClan.None)
        {
          syncServerPacket.WriteH((short) short_1.ClanNote);
          syncServerPacket.WriteN(short_1.Text, short_1.Text.Length + 2, "UTF-16LE");
        }
      }
      if (short_1.Type == NoteMessageType.ClanAsk || short_1.Type == NoteMessageType.Clan || short_1.Type == NoteMessageType.Insert)
      {
        syncServerPacket.WriteC((byte) (short_1.SenderName.Length + 1));
        syncServerPacket.WriteC((byte) 0);
        syncServerPacket.WriteN(short_1.SenderName, short_1.SenderName.Length + 2, "UTF-16LE");
        if (short_1.ClanNote <= NoteMessageClan.Secession)
        {
          syncServerPacket.WriteH((short) (short_1.Text.Length + 1));
          syncServerPacket.WriteH((short) short_1.ClanNote);
          syncServerPacket.WriteN(short_1.Text, short_1.Text.Length + 2, "UTF-16LE");
        }
        else
        {
          syncServerPacket.WriteH(short_1.Type == NoteMessageType.Insert ? (short) 0 : (short_1.Type == NoteMessageType.ClanAsk ? (short) 1 : (short_1.Type == NoteMessageType.Normal || short_1.Type == NoteMessageType.Clan ? (short) 3 : (short) 2)));
          syncServerPacket.WriteD((int) short_1.ClanNote);
          if (short_1.ClanNote != NoteMessageClan.Master || short_1.ClanNote != NoteMessageClan.Staff || short_1.ClanNote != NoteMessageClan.Regular)
            syncServerPacket.WriteH((short) 0);
          syncServerPacket.WriteN(short_1.Text, short_1.Text.Length + 2, "UTF-16LE");
        }
      }
      return syncServerPacket.ToArray();
    }
  }

  public PROTOCOL_ROOM_CHANGE_PASSWD_ACK(uint list_1)
  {
    ((PROTOCOL_MESSENGER_NOTE_SEND_ACK) this).uint_0 = list_1;
  }
}
