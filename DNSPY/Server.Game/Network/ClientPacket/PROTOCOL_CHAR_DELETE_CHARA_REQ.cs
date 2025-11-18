using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200017A RID: 378
	public class PROTOCOL_CHAR_DELETE_CHARA_REQ : GameClientPacket
	{
		// Token: 0x060003D2 RID: 978 RVA: 0x0000527E File Offset: 0x0000347E
		public override void Read()
		{
			this.int_0 = (int)base.ReadC();
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0001E778 File Offset: 0x0001C978
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					CharacterModel characterSlot = player.Character.GetCharacterSlot(this.int_0);
					if (characterSlot != null)
					{
						ItemsModel item = player.Inventory.GetItem(characterSlot.Id);
						if (item != null)
						{
							int num = 0;
							foreach (CharacterModel characterModel in player.Character.Characters)
							{
								if (characterModel.Slot != characterSlot.Slot)
								{
									characterModel.Slot = num;
									DaoManagerSQL.UpdatePlayerCharacter(num, characterModel.ObjectId, player.PlayerId);
									num++;
								}
							}
							this.Client.SendPacket(new PROTOCOL_CHAR_DELETE_CHARA_ACK(0U, this.int_0, player, item));
							if (DaoManagerSQL.DeletePlayerCharacter(characterSlot.ObjectId, player.PlayerId))
							{
								player.Character.RemoveCharacter(characterSlot);
							}
							if (DaoManagerSQL.DeletePlayerInventoryItem(item.ObjectId, player.PlayerId))
							{
								player.Inventory.RemoveItem(item);
							}
						}
						else
						{
							this.Client.SendPacket(new PROTOCOL_CHAR_DELETE_CHARA_ACK(2147487911U, -1, null, null));
						}
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_CHAR_DELETE_CHARA_ACK(2147487911U, -1, null, null));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_CHAR_DELETE_CHARA_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CHAR_DELETE_CHARA_REQ()
		{
		}

		// Token: 0x040002BE RID: 702
		private int int_0;
	}
}
