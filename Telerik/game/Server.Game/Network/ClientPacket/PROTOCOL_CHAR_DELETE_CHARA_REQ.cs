using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_CHAR_DELETE_CHARA_REQ : GameClientPacket
	{
		private int int_0;

		public PROTOCOL_CHAR_DELETE_CHARA_REQ()
		{
		}

		public override void Read()
		{
			this.int_0 = base.ReadC();
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					CharacterModel characterSlot = player.Character.GetCharacterSlot(this.int_0);
					if (characterSlot == null)
					{
						this.Client.SendPacket(new PROTOCOL_CHAR_DELETE_CHARA_ACK(-2147479385, -1, null, null));
					}
					else
					{
						ItemsModel ıtem = player.Inventory.GetItem(characterSlot.Id);
						if (ıtem == null)
						{
							this.Client.SendPacket(new PROTOCOL_CHAR_DELETE_CHARA_ACK(-2147479385, -1, null, null));
						}
						else
						{
							int ınt32 = 0;
							foreach (CharacterModel character in player.Character.Characters)
							{
								if (character.Slot == characterSlot.Slot)
								{
									continue;
								}
								character.Slot = ınt32;
								DaoManagerSQL.UpdatePlayerCharacter(ınt32, character.ObjectId, player.PlayerId);
								ınt32++;
							}
							this.Client.SendPacket(new PROTOCOL_CHAR_DELETE_CHARA_ACK(0, this.int_0, player, ıtem));
							if (DaoManagerSQL.DeletePlayerCharacter(characterSlot.ObjectId, player.PlayerId))
							{
								player.Character.RemoveCharacter(characterSlot);
							}
							if (DaoManagerSQL.DeletePlayerInventoryItem(ıtem.ObjectId, player.PlayerId))
							{
								player.Inventory.RemoveItem(ıtem);
							}
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_CHAR_DELETE_CHARA_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}