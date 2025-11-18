// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_ROOM_REQUEST_MAIN_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_REQUEST_MAIN_ACK : GameServerPacket
{
  private readonly uint uint_0;

  private byte[] method_1(RoomModel playerEquipment_1)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      syncServerPacket.WriteC((byte) playerEquipment_1.Slots.Length);
      foreach (SlotModel slot in playerEquipment_1.Slots)
      {
        syncServerPacket.WriteC((byte) slot.State);
        Account playerBySlot = playerEquipment_1.GetPlayerBySlot(slot);
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
          syncServerPacket.WriteC((byte) 0);
          syncServerPacket.WriteC((byte) this.NATIONS);
          syncServerPacket.WriteC((byte) 0);
          syncServerPacket.WriteD(playerBySlot.Equipment.NameCardId);
          syncServerPacket.WriteC((byte) playerBySlot.Bonus.NickBorderColor);
          syncServerPacket.WriteC((byte) playerBySlot.AuthLevel());
          syncServerPacket.WriteU(clan.Name, 34);
          syncServerPacket.WriteC((byte) playerBySlot.SlotId);
          syncServerPacket.WriteU(playerBySlot.Nickname, 66);
          syncServerPacket.WriteC((byte) playerBySlot.NickColor);
          syncServerPacket.WriteC((byte) playerBySlot.Bonus.MuzzleColor);
          syncServerPacket.WriteC((byte) 0);
          syncServerPacket.WriteC(byte.MaxValue);
          syncServerPacket.WriteC(byte.MaxValue);
        }
        else
        {
          syncServerPacket.WriteB(new byte[10]);
          syncServerPacket.WriteD(uint.MaxValue);
          syncServerPacket.WriteB(new byte[54]);
          syncServerPacket.WriteC((byte) slot.Id);
          syncServerPacket.WriteB(new byte[68]);
          syncServerPacket.WriteC((byte) 0);
          syncServerPacket.WriteC(byte.MaxValue);
          syncServerPacket.WriteC(byte.MaxValue);
        }
      }
      return syncServerPacket.ToArray();
    }
  }

  public PROTOCOL_ROOM_REQUEST_MAIN_ACK(uint uint_1)
  {
    ((PROTOCOL_ROOM_LOADING_START_ACK) this).uint_0 = uint_1;
  }

  public virtual void Write()
  {
    this.WriteH((short) 3658);
    this.WriteD(((PROTOCOL_ROOM_LOADING_START_ACK) this).uint_0);
  }
}
