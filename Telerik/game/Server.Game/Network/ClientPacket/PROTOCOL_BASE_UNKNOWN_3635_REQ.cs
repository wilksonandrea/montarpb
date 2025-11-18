using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Game.Network;
using System;
using System.Reflection;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_BASE_UNKNOWN_3635_REQ : GameClientPacket
	{
		private byte byte_0;

		private byte byte_1;

		private byte byte_2;

		private string string_0;

		private short short_0;

		public PROTOCOL_BASE_UNKNOWN_3635_REQ()
		{
		}

		public override void Read()
		{
			this.byte_0 = base.ReadC();
			this.string_0 = base.ReadU(66);
			base.ReadD();
			base.ReadH();
			this.byte_1 = base.ReadC();
			base.ReadH();
			base.ReadB(16);
			base.ReadB(12);
			this.short_0 = base.ReadH();
			this.byte_2 = base.ReadC();
		}

		public override void Run()
		{
			try
			{
				CLogger.Print(string.Format("{0}; Unk1: {1}; Nickname: {2}; Unk2: {3}; Unk3: {4}; Unk4: {5}", new object[] { base.GetType().Name, this.byte_0, this.string_0, this.byte_1, this.short_0, this.byte_2 }), LoggerType.Warning, null);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat(base.GetType().Name, " Error: ", exception.Message), LoggerType.Error, null);
			}
		}
	}
}