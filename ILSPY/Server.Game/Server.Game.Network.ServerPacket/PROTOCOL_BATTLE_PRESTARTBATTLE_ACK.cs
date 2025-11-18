using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_PRESTARTBATTLE_ACK : GameServerPacket
{
	private readonly Account account_0;

	private readonly RoomModel roomModel_0;

	private readonly bool bool_0;

	private readonly bool bool_1;

	private readonly uint uint_0;

	private readonly uint uint_1;

	public PROTOCOL_BATTLE_PRESTARTBATTLE_ACK(Account account_1, bool bool_2)
	{
		account_0 = account_1;
		bool_1 = bool_2;
		roomModel_0 = account_1.Room;
		if (roomModel_0 != null)
		{
			bool_0 = roomModel_0.IsPreparing();
			uint_0 = roomModel_0.UniqueRoomId;
			uint_1 = roomModel_0.Seed;
		}
	}

	public PROTOCOL_BATTLE_PRESTARTBATTLE_ACK()
	{
	}

	public override void Write()
	{
		WriteH(5130);
		WriteD(bool_0 ? 1 : 0);
		if (bool_0)
		{
			WriteD(account_0.SlotId);
			WriteC((byte)((roomModel_0.RoomType == RoomCondition.Tutorial) ? UdpState.RENDEZVOUS : ConfigLoader.UdpType));
			WriteB(ComDiv.AddressBytes(ConfigLoader.HOST[1]));
			WriteB(ComDiv.AddressBytes(ConfigLoader.HOST[1]));
			WriteH((ushort)ConfigLoader.DEFAULT_PORT[2]);
			WriteD(uint_0);
			WriteD(uint_1);
			WriteB(method_0(bool_1));
		}
	}

	private byte[] method_0(bool bool_2)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		if (bool_2)
		{
			string hexString = "02 14 03 15 04 16 05 17 06 18 07 19 08 1A 09 1B0A 1C 0B 1D 0C 1E 0D 1F 0E 20 0F 21 10 22 11 0012 01 13";
			syncServerPacket.WriteB(Bitwise.HexStringToByteArray(hexString));
		}
		return syncServerPacket.ToArray();
	}
}
