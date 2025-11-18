using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_CS_CHECK_DUPLICATE_REQ : GameClientPacket
	{
		private string string_0;

		public PROTOCOL_CS_CHECK_DUPLICATE_REQ()
		{
		}

		public override void Read()
		{
			this.string_0 = base.ReadU(base.ReadC() * 2);
		}

		public override void Run()
		{
			try
			{
				this.Client.SendPacket(new PROTOCOL_CS_CHECK_DUPLICATE_ACK((uint)((!ClanManager.IsClanNameExist(this.string_0) ? 0 : -2147483648))));
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}
	}
}