using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Plugin.Core.XML;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_BASE_USER_TITLE_EQUIP_REQ : GameClientPacket
	{
		private byte byte_0;

		private byte byte_1;

		public PROTOCOL_BASE_USER_TITLE_EQUIP_REQ()
		{
		}

		public override void Read()
		{
			this.byte_0 = base.ReadC();
			this.byte_1 = base.ReadC();
		}

		public override void Run()
		{
			TitleModel titleModel;
			TitleModel titleModel1;
			TitleModel titleModel2;
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					PlayerTitles title = player.Title;
					TitleModel title1 = TitleSystemXML.GetTitle((int)this.byte_1, true);
					TitleSystemXML.Get3Titles(title.Equiped1, title.Equiped2, title.Equiped3, out titleModel, out titleModel1, out titleModel2, false);
					if (this.byte_0 < 3 && this.byte_1 < 45 && title != null && title1 != null && (title1.ClassId != titleModel.ClassId || this.byte_0 == 0) && (title1.ClassId != titleModel1.ClassId || this.byte_0 == 1) && (title1.ClassId != titleModel2.ClassId || this.byte_0 == 2) && title.Contains(title1.Flag) && title.Equiped1 != this.byte_1 && title.Equiped2 != this.byte_1)
					{
						if (title.Equiped3 == this.byte_1)
						{
							goto Label2;
						}
						if (!DaoManagerSQL.UpdateEquipedPlayerTitle(title.OwnerId, (int)this.byte_0, (int)this.byte_1))
						{
							this.Client.SendPacket(new PROTOCOL_BASE_USER_TITLE_EQUIP_ACK(-2147483648, -1, -1));
							goto Label0;
						}
						else
						{
							title.SetEquip((int)this.byte_0, (int)this.byte_1);
							this.Client.SendPacket(new PROTOCOL_BASE_USER_TITLE_EQUIP_ACK(0, (int)this.byte_0, (int)this.byte_1));
							goto Label0;
						}
					}
				Label2:
					this.Client.SendPacket(new PROTOCOL_BASE_USER_TITLE_EQUIP_ACK(-2147483648, -1, -1));
				Label0:
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_BASE_USER_TITLE_EQUIP_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}