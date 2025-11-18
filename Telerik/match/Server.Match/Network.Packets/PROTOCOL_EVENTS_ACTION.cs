using Plugin.Core.Network;
using Server.Match.Data.Enums;
using Server.Match.Data.Models;
using System;
using System.Collections.Generic;

namespace Server.Match.Network.Packets
{
	public class PROTOCOL_EVENTS_ACTION
	{
		public PROTOCOL_EVENTS_ACTION()
		{
		}

		public static byte[] GET_CODE(List<ObjectHitInfo> Objs)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				foreach (ObjectHitInfo obj in Objs)
				{
					if (obj.Type == 1)
					{
						if (obj.ObjSyncId != 0)
						{
							syncServerPacket.WriteH(13);
							syncServerPacket.WriteH((ushort)obj.ObjId);
							syncServerPacket.WriteC(6);
							syncServerPacket.WriteH((ushort)obj.ObjLife);
							syncServerPacket.WriteC((byte)obj.AnimId1);
							syncServerPacket.WriteC((byte)obj.AnimId2);
							syncServerPacket.WriteT(obj.SpecialUse);
						}
						else
						{
							syncServerPacket.WriteH(10);
							syncServerPacket.WriteH((ushort)obj.ObjId);
							syncServerPacket.WriteC(3);
							syncServerPacket.WriteH(6);
							syncServerPacket.WriteH((ushort)obj.ObjLife);
							syncServerPacket.WriteC((byte)obj.KillerSlot);
						}
					}
					else if (obj.Type == 2)
					{
						ushort uInt16 = 11;
						UdpGameEvent udpGameEvent = UdpGameEvent.HpSync;
						if (obj.ObjLife == 0)
						{
							uInt16 = (ushort)(uInt16 + 13);
							udpGameEvent |= UdpGameEvent.GetWeaponForHost;
						}
						syncServerPacket.WriteH(uInt16);
						syncServerPacket.WriteH((ushort)obj.ObjId);
						syncServerPacket.WriteC(0);
						syncServerPacket.WriteD((uint)udpGameEvent);
						syncServerPacket.WriteH((ushort)obj.ObjLife);
						if (!udpGameEvent.HasFlag(UdpGameEvent.GetWeaponForHost))
						{
							continue;
						}
						syncServerPacket.WriteC((byte)obj.DeathType);
						syncServerPacket.WriteC((byte)obj.KillerSlot);
						syncServerPacket.WriteHV(obj.Position);
						syncServerPacket.WriteD(0);
						syncServerPacket.WriteC((byte)obj.HitPart);
					}
					else if (obj.Type == 3)
					{
						if (obj.ObjSyncId != 0)
						{
							syncServerPacket.WriteH(14);
							syncServerPacket.WriteH((ushort)obj.ObjId);
							syncServerPacket.WriteC(12);
							syncServerPacket.WriteC((byte)obj.DestroyState);
							syncServerPacket.WriteH((ushort)obj.ObjLife);
							syncServerPacket.WriteT(obj.SpecialUse);
							syncServerPacket.WriteC((byte)obj.AnimId1);
							syncServerPacket.WriteC((byte)obj.AnimId2);
						}
						else
						{
							syncServerPacket.WriteH(8);
							syncServerPacket.WriteH((ushort)obj.ObjId);
							syncServerPacket.WriteC(9);
							syncServerPacket.WriteH(1);
							syncServerPacket.WriteC((byte)(obj.ObjLife == 0));
						}
					}
					else if (obj.Type == 4)
					{
						syncServerPacket.WriteH(11);
						syncServerPacket.WriteH((ushort)obj.ObjId);
						syncServerPacket.WriteC(8);
						syncServerPacket.WriteD((uint)8);
						syncServerPacket.WriteH((ushort)obj.ObjLife);
					}
					else if (obj.Type != 5)
					{
						if (obj.Type != 6)
						{
							continue;
						}
						syncServerPacket.WriteH(15);
						syncServerPacket.WriteH((ushort)obj.ObjId);
						syncServerPacket.WriteC(0);
						syncServerPacket.WriteD((uint)67108864);
						syncServerPacket.WriteD(obj.WeaponId);
						syncServerPacket.WriteC(obj.Accessory);
						syncServerPacket.WriteC(obj.Extensions);
					}
					else
					{
						syncServerPacket.WriteH(12);
						syncServerPacket.WriteH((ushort)obj.ObjId);
						syncServerPacket.WriteC(0);
						syncServerPacket.WriteD((uint)1073741824);
						syncServerPacket.WriteC((byte)((int)obj.DeathType * 16));
						syncServerPacket.WriteC((byte)obj.HitPart);
						syncServerPacket.WriteC((byte)obj.KillerSlot);
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}
	}
}