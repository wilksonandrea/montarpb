using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Auth.Network;
using System;

namespace Server.Auth.Network.ServerPacket
{
	public class PROTOCOL_BASE_GET_OPTION_ACK : AuthServerPacket
	{
		private readonly int int_0;

		private readonly PlayerConfig playerConfig_0;

		public PROTOCOL_BASE_GET_OPTION_ACK(int int_1, PlayerConfig playerConfig_1)
		{
			this.int_0 = int_1;
			this.playerConfig_0 = playerConfig_1;
		}

		public override void Write()
		{
			base.WriteH(2321);
			base.WriteH(0);
			base.WriteD(this.int_0);
			if (this.int_0 == 0)
			{
				base.WriteH((ushort)this.playerConfig_0.Nations);
				base.WriteH((ushort)this.playerConfig_0.Macro5.Length);
				base.WriteN(this.playerConfig_0.Macro5, this.playerConfig_0.Macro5.Length, "UTF-16LE");
				base.WriteH((ushort)this.playerConfig_0.Macro4.Length);
				base.WriteN(this.playerConfig_0.Macro4, this.playerConfig_0.Macro4.Length, "UTF-16LE");
				base.WriteH((ushort)this.playerConfig_0.Macro3.Length);
				base.WriteN(this.playerConfig_0.Macro3, this.playerConfig_0.Macro3.Length, "UTF-16LE");
				base.WriteH((ushort)this.playerConfig_0.Macro2.Length);
				base.WriteN(this.playerConfig_0.Macro2, this.playerConfig_0.Macro2.Length, "UTF-16LE");
				base.WriteH((ushort)this.playerConfig_0.Macro1.Length);
				base.WriteN(this.playerConfig_0.Macro1, this.playerConfig_0.Macro1.Length, "UTF-16LE");
				base.WriteH(49);
				base.WriteB(Bitwise.HexStringToByteArray("39 F8 10 00"));
				base.WriteB(this.playerConfig_0.KeyboardKeys);
				base.WriteH((short)this.playerConfig_0.ShowBlood);
				base.WriteC((byte)this.playerConfig_0.Crosshair);
				base.WriteC((byte)this.playerConfig_0.HandPosition);
				base.WriteD(this.playerConfig_0.Config);
				base.WriteD(this.playerConfig_0.AudioEnable);
				base.WriteH(0);
				base.WriteC((byte)this.playerConfig_0.AudioSFX);
				base.WriteC((byte)this.playerConfig_0.AudioBGM);
				base.WriteC((byte)this.playerConfig_0.PointOfView);
				base.WriteC(0);
				base.WriteC((byte)this.playerConfig_0.Sensitivity);
				base.WriteC((byte)this.playerConfig_0.InvertMouse);
				base.WriteH(0);
				base.WriteC((byte)this.playerConfig_0.EnableInviteMsg);
				base.WriteC((byte)this.playerConfig_0.EnableWhisperMsg);
				base.WriteD(this.playerConfig_0.Macro);
			}
		}
	}
}