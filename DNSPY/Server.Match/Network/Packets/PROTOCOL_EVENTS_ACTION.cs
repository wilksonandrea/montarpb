using System;
using System.Collections.Generic;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Enums;
using Server.Match.Data.Models;

namespace Server.Match.Network.Packets
{
	// Token: 0x02000007 RID: 7
	public class PROTOCOL_EVENTS_ACTION
	{
		// Token: 0x06000027 RID: 39 RVA: 0x0000658C File Offset: 0x0000478C
		public static byte[] GET_CODE(List<ObjectHitInfo> Objs)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				foreach (ObjectHitInfo objectHitInfo in Objs)
				{
					if (objectHitInfo.Type == 1)
					{
						if (objectHitInfo.ObjSyncId == 0)
						{
							syncServerPacket.WriteH(10);
							syncServerPacket.WriteH((ushort)objectHitInfo.ObjId);
							syncServerPacket.WriteC(3);
							syncServerPacket.WriteH(6);
							syncServerPacket.WriteH((ushort)objectHitInfo.ObjLife);
							syncServerPacket.WriteC((byte)objectHitInfo.KillerSlot);
						}
						else
						{
							syncServerPacket.WriteH(13);
							syncServerPacket.WriteH((ushort)objectHitInfo.ObjId);
							syncServerPacket.WriteC(6);
							syncServerPacket.WriteH((ushort)objectHitInfo.ObjLife);
							syncServerPacket.WriteC((byte)objectHitInfo.AnimId1);
							syncServerPacket.WriteC((byte)objectHitInfo.AnimId2);
							syncServerPacket.WriteT(objectHitInfo.SpecialUse);
						}
					}
					else if (objectHitInfo.Type == 2)
					{
						ushort num = 11;
						UdpGameEvent udpGameEvent = UdpGameEvent.HpSync;
						if (objectHitInfo.ObjLife == 0)
						{
							num += 13;
							udpGameEvent |= UdpGameEvent.GetWeaponForHost;
						}
						syncServerPacket.WriteH(num);
						syncServerPacket.WriteH((ushort)objectHitInfo.ObjId);
						syncServerPacket.WriteC(0);
						syncServerPacket.WriteD((uint)udpGameEvent);
						syncServerPacket.WriteH((ushort)objectHitInfo.ObjLife);
						if (udpGameEvent.HasFlag(UdpGameEvent.GetWeaponForHost))
						{
							syncServerPacket.WriteC((byte)objectHitInfo.DeathType);
							syncServerPacket.WriteC((byte)objectHitInfo.KillerSlot);
							syncServerPacket.WriteHV(objectHitInfo.Position);
							syncServerPacket.WriteD(0);
							syncServerPacket.WriteC((byte)objectHitInfo.HitPart);
						}
					}
					else if (objectHitInfo.Type == 3)
					{
						if (objectHitInfo.ObjSyncId == 0)
						{
							syncServerPacket.WriteH(8);
							syncServerPacket.WriteH((ushort)objectHitInfo.ObjId);
							syncServerPacket.WriteC(9);
							syncServerPacket.WriteH(1);
							syncServerPacket.WriteC((objectHitInfo.ObjLife == 0) ? 1 : 0);
						}
						else
						{
							syncServerPacket.WriteH(14);
							syncServerPacket.WriteH((ushort)objectHitInfo.ObjId);
							syncServerPacket.WriteC(12);
							syncServerPacket.WriteC((byte)objectHitInfo.DestroyState);
							syncServerPacket.WriteH((ushort)objectHitInfo.ObjLife);
							syncServerPacket.WriteT(objectHitInfo.SpecialUse);
							syncServerPacket.WriteC((byte)objectHitInfo.AnimId1);
							syncServerPacket.WriteC((byte)objectHitInfo.AnimId2);
						}
					}
					else if (objectHitInfo.Type == 4)
					{
						syncServerPacket.WriteH(11);
						syncServerPacket.WriteH((ushort)objectHitInfo.ObjId);
						syncServerPacket.WriteC(8);
						syncServerPacket.WriteD(8U);
						syncServerPacket.WriteH((ushort)objectHitInfo.ObjLife);
					}
					else if (objectHitInfo.Type == 5)
					{
						syncServerPacket.WriteH(12);
						syncServerPacket.WriteH((ushort)objectHitInfo.ObjId);
						syncServerPacket.WriteC(0);
						syncServerPacket.WriteD(1073741824U);
						syncServerPacket.WriteC((byte)(objectHitInfo.DeathType * (CharaDeath)16));
						syncServerPacket.WriteC((byte)objectHitInfo.HitPart);
						syncServerPacket.WriteC((byte)objectHitInfo.KillerSlot);
					}
					else if (objectHitInfo.Type == 6)
					{
						syncServerPacket.WriteH(15);
						syncServerPacket.WriteH((ushort)objectHitInfo.ObjId);
						syncServerPacket.WriteC(0);
						syncServerPacket.WriteD(67108864U);
						syncServerPacket.WriteD(objectHitInfo.WeaponId);
						syncServerPacket.WriteC(objectHitInfo.Accessory);
						syncServerPacket.WriteC(objectHitInfo.Extensions);
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000020A2 File Offset: 0x000002A2
		public PROTOCOL_EVENTS_ACTION()
		{
		}
	}
}
