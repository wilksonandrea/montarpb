using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200015F RID: 351
	public class PROTOCOL_BASE_USER_TITLE_EQUIP_REQ : GameClientPacket
	{
		// Token: 0x0600037D RID: 893 RVA: 0x0000510B File Offset: 0x0000330B
		public override void Read()
		{
			this.byte_0 = base.ReadC();
			this.byte_1 = base.ReadC();
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0001AF0C File Offset: 0x0001910C
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					PlayerTitles title = player.Title;
					TitleModel title2 = TitleSystemXML.GetTitle((int)this.byte_1, true);
					TitleModel titleModel;
					TitleModel titleModel2;
					TitleModel titleModel3;
					TitleSystemXML.Get3Titles(title.Equiped1, title.Equiped2, title.Equiped3, out titleModel, out titleModel2, out titleModel3, false);
					if (this.byte_0 < 3 && this.byte_1 < 45 && title != null && title2 != null && (title2.ClassId != titleModel.ClassId || this.byte_0 == 0) && (title2.ClassId != titleModel2.ClassId || this.byte_0 == 1) && (title2.ClassId != titleModel3.ClassId || this.byte_0 == 2) && title.Contains(title2.Flag) && title.Equiped1 != (int)this.byte_1 && title.Equiped2 != (int)this.byte_1)
					{
						if (title.Equiped3 != (int)this.byte_1)
						{
							if (DaoManagerSQL.UpdateEquipedPlayerTitle(title.OwnerId, (int)this.byte_0, (int)this.byte_1))
							{
								title.SetEquip((int)this.byte_0, (int)this.byte_1);
								this.Client.SendPacket(new PROTOCOL_BASE_USER_TITLE_EQUIP_ACK(0U, (int)this.byte_0, (int)this.byte_1));
								goto IL_174;
							}
							this.Client.SendPacket(new PROTOCOL_BASE_USER_TITLE_EQUIP_ACK(2147483648U, -1, -1));
							goto IL_174;
						}
					}
					this.Client.SendPacket(new PROTOCOL_BASE_USER_TITLE_EQUIP_ACK(2147483648U, -1, -1));
					IL_174:;
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BASE_USER_TITLE_EQUIP_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BASE_USER_TITLE_EQUIP_REQ()
		{
		}

		// Token: 0x04000280 RID: 640
		private byte byte_0;

		// Token: 0x04000281 RID: 641
		private byte byte_1;
	}
}
