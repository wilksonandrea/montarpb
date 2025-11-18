using System.Collections.Generic;
using Plugin.Core.Network;
using Server.Match.Data.Enums;
using Server.Match.Data.Models;

namespace Server.Match.Network.Packets;

public class PROTOCOL_EVENTS_ACTION
{
	public static byte[] GET_CODE(List<ObjectHitInfo> Objs)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		foreach (ObjectHitInfo Obj in Objs)
		{
			if (Obj.Type == 1)
			{
				if (Obj.ObjSyncId == 0)
				{
					syncServerPacket.WriteH(10);
					syncServerPacket.WriteH((ushort)Obj.ObjId);
					syncServerPacket.WriteC(3);
					syncServerPacket.WriteH(6);
					syncServerPacket.WriteH((ushort)Obj.ObjLife);
					syncServerPacket.WriteC((byte)Obj.KillerSlot);
				}
				else
				{
					syncServerPacket.WriteH(13);
					syncServerPacket.WriteH((ushort)Obj.ObjId);
					syncServerPacket.WriteC(6);
					syncServerPacket.WriteH((ushort)Obj.ObjLife);
					syncServerPacket.WriteC((byte)Obj.AnimId1);
					syncServerPacket.WriteC((byte)Obj.AnimId2);
					syncServerPacket.WriteT(Obj.SpecialUse);
				}
			}
			else if (Obj.Type == 2)
			{
				ushort num = 11;
				UdpGameEvent udpGameEvent = UdpGameEvent.HpSync;
				if (Obj.ObjLife == 0)
				{
					num = (ushort)(num + 13);
					udpGameEvent |= UdpGameEvent.GetWeaponForHost;
				}
				syncServerPacket.WriteH(num);
				syncServerPacket.WriteH((ushort)Obj.ObjId);
				syncServerPacket.WriteC(0);
				syncServerPacket.WriteD((uint)udpGameEvent);
				syncServerPacket.WriteH((ushort)Obj.ObjLife);
				if (udpGameEvent.HasFlag(UdpGameEvent.GetWeaponForHost))
				{
					syncServerPacket.WriteC((byte)Obj.DeathType);
					syncServerPacket.WriteC((byte)Obj.KillerSlot);
					syncServerPacket.WriteHV(Obj.Position);
					syncServerPacket.WriteD(0);
					syncServerPacket.WriteC((byte)Obj.HitPart);
				}
			}
			else if (Obj.Type == 3)
			{
				if (Obj.ObjSyncId == 0)
				{
					syncServerPacket.WriteH(8);
					syncServerPacket.WriteH((ushort)Obj.ObjId);
					syncServerPacket.WriteC(9);
					syncServerPacket.WriteH(1);
					syncServerPacket.WriteC((Obj.ObjLife == 0) ? ((byte)1) : ((byte)0));
				}
				else
				{
					syncServerPacket.WriteH(14);
					syncServerPacket.WriteH((ushort)Obj.ObjId);
					syncServerPacket.WriteC(12);
					syncServerPacket.WriteC((byte)Obj.DestroyState);
					syncServerPacket.WriteH((ushort)Obj.ObjLife);
					syncServerPacket.WriteT(Obj.SpecialUse);
					syncServerPacket.WriteC((byte)Obj.AnimId1);
					syncServerPacket.WriteC((byte)Obj.AnimId2);
				}
			}
			else if (Obj.Type == 4)
			{
				syncServerPacket.WriteH(11);
				syncServerPacket.WriteH((ushort)Obj.ObjId);
				syncServerPacket.WriteC(8);
				syncServerPacket.WriteD(8u);
				syncServerPacket.WriteH((ushort)Obj.ObjLife);
			}
			else if (Obj.Type == 5)
			{
				syncServerPacket.WriteH(12);
				syncServerPacket.WriteH((ushort)Obj.ObjId);
				syncServerPacket.WriteC(0);
				syncServerPacket.WriteD(1073741824u);
				syncServerPacket.WriteC((byte)((uint)Obj.DeathType * 16u));
				syncServerPacket.WriteC((byte)Obj.HitPart);
				syncServerPacket.WriteC((byte)Obj.KillerSlot);
			}
			else if (Obj.Type == 6)
			{
				syncServerPacket.WriteH(15);
				syncServerPacket.WriteH((ushort)Obj.ObjId);
				syncServerPacket.WriteC(0);
				syncServerPacket.WriteD(67108864u);
				syncServerPacket.WriteD(Obj.WeaponId);
				syncServerPacket.WriteC(Obj.Accessory);
				syncServerPacket.WriteC(Obj.Extensions);
			}
		}
		return syncServerPacket.ToArray();
	}
}
