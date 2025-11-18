using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_USER_TITLE_EQUIP_REQ : GameClientPacket
{
	private byte byte_0;

	private byte byte_1;

	public override void Read()
	{
		byte_0 = ReadC();
		byte_1 = ReadC();
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player == null)
			{
				return;
			}
			PlayerTitles title = player.Title;
			TitleModel title2 = TitleSystemXML.GetTitle(byte_1);
			TitleSystemXML.Get3Titles(title.Equiped1, title.Equiped2, title.Equiped3, out var title3, out var title4, out var title5, ReturnNull: false);
			if (byte_0 < 3 && byte_1 < 45 && title != null && title2 != null && (title2.ClassId != title3.ClassId || byte_0 == 0) && (title2.ClassId != title4.ClassId || byte_0 == 1) && (title2.ClassId != title5.ClassId || byte_0 == 2) && title.Contains(title2.Flag) && title.Equiped1 != byte_1 && title.Equiped2 != byte_1 && title.Equiped3 != byte_1)
			{
				if (DaoManagerSQL.UpdateEquipedPlayerTitle(title.OwnerId, byte_0, byte_1))
				{
					title.SetEquip(byte_0, byte_1);
					Client.SendPacket(new PROTOCOL_BASE_USER_TITLE_EQUIP_ACK(0u, byte_0, byte_1));
				}
				else
				{
					Client.SendPacket(new PROTOCOL_BASE_USER_TITLE_EQUIP_ACK(2147483648u, -1, -1));
				}
			}
			else
			{
				Client.SendPacket(new PROTOCOL_BASE_USER_TITLE_EQUIP_ACK(2147483648u, -1, -1));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_BASE_USER_TITLE_EQUIP_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
