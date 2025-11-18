using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CHAR_DELETE_CHARA_REQ : GameClientPacket
{
	private int int_0;

	public override void Read()
	{
		int_0 = ReadC();
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
			CharacterModel characterSlot = player.Character.GetCharacterSlot(int_0);
			if (characterSlot != null)
			{
				ItemsModel ıtem = player.Inventory.GetItem(characterSlot.Id);
				if (ıtem != null)
				{
					int num = 0;
					foreach (CharacterModel character in player.Character.Characters)
					{
						if (character.Slot != characterSlot.Slot)
						{
							character.Slot = num;
							DaoManagerSQL.UpdatePlayerCharacter(num, character.ObjectId, player.PlayerId);
							num++;
						}
					}
					Client.SendPacket(new PROTOCOL_CHAR_DELETE_CHARA_ACK(0u, int_0, player, ıtem));
					if (DaoManagerSQL.DeletePlayerCharacter(characterSlot.ObjectId, player.PlayerId))
					{
						player.Character.RemoveCharacter(characterSlot);
					}
					if (DaoManagerSQL.DeletePlayerInventoryItem(ıtem.ObjectId, player.PlayerId))
					{
						player.Inventory.RemoveItem(ıtem);
					}
				}
				else
				{
					Client.SendPacket(new PROTOCOL_CHAR_DELETE_CHARA_ACK(2147487911u, -1, null, null));
				}
			}
			else
			{
				Client.SendPacket(new PROTOCOL_CHAR_DELETE_CHARA_ACK(2147487911u, -1, null, null));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_CHAR_DELETE_CHARA_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
