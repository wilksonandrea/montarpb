using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_CHAR_DELETE_CHARA_ACK : GameServerPacket
	{
		private readonly uint uint_0;

		private readonly int int_0;

		private readonly ItemsModel itemsModel_0;

		private readonly CharacterModel characterModel_0;

		public PROTOCOL_CHAR_DELETE_CHARA_ACK(uint uint_1, int int_1, Account account_0, ItemsModel itemsModel_1)
		{
			this.uint_0 = uint_1;
			this.int_0 = int_1;
			this.itemsModel_0 = itemsModel_1;
			if (account_0 != null && itemsModel_1 != null)
			{
				this.characterModel_0 = account_0.Character.GetCharacter(itemsModel_1.Id);
			}
		}

		public override void Write()
		{
			base.WriteH(6152);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 0)
			{
				base.WriteC((byte)this.int_0);
				base.WriteD((uint)this.itemsModel_0.ObjectId);
				base.WriteD(this.characterModel_0.Slot);
			}
		}
	}
}