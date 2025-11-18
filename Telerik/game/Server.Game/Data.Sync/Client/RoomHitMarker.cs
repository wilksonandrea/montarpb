using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Data.XML;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Data.Sync.Client
{
	public static class RoomHitMarker
	{
		public static void Load(SyncClientPacket C)
		{
			int ınt32 = C.ReadH();
			int ınt321 = C.ReadH();
			short ınt16 = C.ReadH();
			byte num = C.ReadC();
			byte num1 = C.ReadC();
			byte num2 = C.ReadC();
			int ınt322 = C.ReadD();
			if ((int)C.ToArray().Length > 15)
			{
				CLogger.Print(string.Format("Invalid Hit (Length > 15): {0}", (int)C.ToArray().Length), LoggerType.Warning, null);
			}
			ChannelModel channel = ChannelsXML.GetChannel(ınt16, ınt321);
			if (channel == null)
			{
				return;
			}
			RoomModel room = channel.GetRoom(ınt32);
			if (room != null && room.State == RoomState.BATTLE)
			{
				Account playerBySlot = room.GetPlayerBySlot((int)num);
				if (playerBySlot != null)
				{
					string label = "";
					if (num1 == 10)
					{
						label = Translation.GetLabel("LifeRestored", new object[] { ınt322 });
					}
					switch (num2)
					{
						case 0:
						{
							label = Translation.GetLabel("HitMarker1", new object[] { ınt322 });
							break;
						}
						case 1:
						{
							label = Translation.GetLabel("HitMarker2", new object[] { ınt322 });
							break;
						}
						case 2:
						{
							label = Translation.GetLabel("HitMarker3");
							break;
						}
						case 3:
						{
							label = Translation.GetLabel("HitMarker4");
							break;
						}
					}
					playerBySlot.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK(Translation.GetLabel("HitMarkerName"), playerBySlot.GetSessionId(), 0, true, label));
				}
			}
		}
	}
}