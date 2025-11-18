// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK : GameServerPacket
{
  private readonly uint uint_0;
  private readonly PlayerEquipment playerEquipment_0;
  private readonly int[] int_0;
  private readonly byte byte_0;

  private byte[] method_0([In] RoomModel obj0)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      foreach (SlotModel slot in obj0.Slots)
      {
        syncServerPacket.WriteH((short) 35);
        syncServerPacket.WriteC((byte) slot.State);
        Account playerBySlot = obj0.GetPlayerBySlot(slot);
        if (playerBySlot != null)
        {
          ClanModel clan = ClanManager.GetClan(playerBySlot.ClanId);
          syncServerPacket.WriteC((byte) playerBySlot.GetRank());
          syncServerPacket.WriteD(clan.Id);
          syncServerPacket.WriteD(playerBySlot.ClanAccess);
          syncServerPacket.WriteC((byte) clan.Rank);
          syncServerPacket.WriteD(clan.Logo);
          syncServerPacket.WriteC((byte) playerBySlot.CafePC);
          syncServerPacket.WriteC((byte) playerBySlot.Country);
          syncServerPacket.WriteQ((long) playerBySlot.Effects);
          syncServerPacket.WriteC((byte) clan.Effect);
          syncServerPacket.WriteC((byte) slot.ViewType);
          syncServerPacket.WriteC((byte) this.NATIONS);
          syncServerPacket.WriteC((byte) 0);
          syncServerPacket.WriteD(playerBySlot.Equipment.NameCardId);
          syncServerPacket.WriteC((byte) playerBySlot.Bonus.NickBorderColor);
          syncServerPacket.WriteC((byte) playerBySlot.AuthLevel());
          syncServerPacket.WriteC((byte) (clan.Name.Length * 2));
          syncServerPacket.WriteU(clan.Name, clan.Name.Length * 2);
        }
        else
        {
          syncServerPacket.WriteB(new byte[10]);
          syncServerPacket.WriteD(uint.MaxValue);
          syncServerPacket.WriteB(new byte[21]);
        }
      }
      return syncServerPacket.ToArray();
    }
  }

  private byte[] method_1([In] RoomModel obj0)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      foreach (SlotModel slot in obj0.Slots)
        syncServerPacket.WriteC((byte) obj0.ValidateTeam(slot.Team, slot.CostumeTeam));
      return syncServerPacket.ToArray();
    }
  }
}
