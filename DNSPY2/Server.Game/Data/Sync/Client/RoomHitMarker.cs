using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Data.XML;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Data.Sync.Client
{
	// Token: 0x020001FA RID: 506
	public static class RoomHitMarker
	{
		// Token: 0x060005EA RID: 1514 RVA: 0x0003081C File Offset: 0x0002EA1C
		public static void Load(SyncClientPacket C)
		{
			int num = (int)C.ReadH();
			int num2 = (int)C.ReadH();
			int num3 = (int)C.ReadH();
			byte b = C.ReadC();
			byte b2 = C.ReadC();
			byte b3 = C.ReadC();
			int num4 = C.ReadD();
			if (C.ToArray().Length > 15)
			{
				CLogger.Print(string.Format("Invalid Hit (Length > 15): {0}", C.ToArray().Length), LoggerType.Warning, null);
			}
			ChannelModel channel = ChannelsXML.GetChannel(num3, num2);
			if (channel == null)
			{
				return;
			}
			RoomModel room = channel.GetRoom(num);
			if (room != null && room.State == RoomState.BATTLE)
			{
				Account playerBySlot = room.GetPlayerBySlot((int)b);
				if (playerBySlot != null)
				{
					string text = "";
					if (b2 == 10)
					{
						text = Translation.GetLabel("LifeRestored", new object[] { num4 });
					}
					switch (b3)
					{
					case 0:
						text = Translation.GetLabel("HitMarker1", new object[] { num4 });
						break;
					case 1:
						text = Translation.GetLabel("HitMarker2", new object[] { num4 });
						break;
					case 2:
						text = Translation.GetLabel("HitMarker3");
						break;
					case 3:
						text = Translation.GetLabel("HitMarker4");
						break;
					}
					playerBySlot.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK(Translation.GetLabel("HitMarkerName"), playerBySlot.GetSessionId(), 0, true, text));
				}
			}
		}
	}
}
