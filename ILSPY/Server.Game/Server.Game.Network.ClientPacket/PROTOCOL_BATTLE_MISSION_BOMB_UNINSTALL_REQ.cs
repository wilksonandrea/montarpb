using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Client;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_REQ : GameClientPacket
{
	private int int_0;

	public override void Read()
	{
		int_0 = ReadD();
	}

	public override void Run()
	{
		Account player = Client.Player;
		if (player == null)
		{
			return;
		}
		RoomModel room = player.Room;
		if (room != null && !room.RoundTime.IsTimer() && room.State == RoomState.BATTLE && room.ActiveC4)
		{
			SlotModel slot = room.GetSlot(int_0);
			if (slot != null && slot.State == SlotState.BATTLE)
			{
				RoomBombC4.UninstallBomb(room, slot);
			}
		}
	}
}
