using System;
using Plugin.Core.Models;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200002B RID: 43
	public class PROTOCOL_CHAR_DELETE_CHARA_ACK : GameServerPacket
	{
		// Token: 0x06000092 RID: 146 RVA: 0x00002B45 File Offset: 0x00000D45
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

		// Token: 0x06000093 RID: 147 RVA: 0x0000A424 File Offset: 0x00008624
		public override void Write()
		{
			base.WriteH(6152);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 0U)
			{
				base.WriteC((byte)this.int_0);
				base.WriteD((uint)this.itemsModel_0.ObjectId);
				base.WriteD(this.characterModel_0.Slot);
			}
		}

		// Token: 0x04000055 RID: 85
		private readonly uint uint_0;

		// Token: 0x04000056 RID: 86
		private readonly int int_0;

		// Token: 0x04000057 RID: 87
		private readonly ItemsModel itemsModel_0;

		// Token: 0x04000058 RID: 88
		private readonly CharacterModel characterModel_0;
	}
}
