using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CLAN_WAR_JOIN_ROOM_REQ : GameClientPacket
{
	private int int_0;

	private int int_1;

	private TeamEnum teamEnum_0;

	public override void Read()
	{
		int_0 = ReadD();
		teamEnum_0 = (TeamEnum)ReadH();
		int_1 = ReadH();
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player == null || player.ClanId == 0 || player.Match == null)
			{
				return;
			}
			if (player != null && player.Nickname.Length > 0 && player.Room == null && player.GetChannel(out var Channel))
			{
				RoomModel room = Channel.GetRoom(int_0);
				if (room != null && room.GetLeader() != null)
				{
					if (room.Password.Length > 0 && !player.IsGM())
					{
						Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(2147487749u, null));
					}
					else if (room.Limit == 1 && room.State >= RoomState.COUNTDOWN)
					{
						Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(2147487763u, null));
					}
					else if (room.KickedPlayersVote.Contains(player.PlayerId))
					{
						Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(2147487756u, null));
					}
					else if (room.AddPlayer(player, teamEnum_0) >= 0)
					{
						using (PROTOCOL_ROOM_GET_SLOTONEINFO_ACK packet = new PROTOCOL_ROOM_GET_SLOTONEINFO_ACK(player))
						{
							room.SendPacketToPlayers(packet, player.PlayerId);
						}
						room.UpdateSlotsInfo();
						Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(0u, player));
					}
					else
					{
						Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(2147487747u, null));
					}
				}
				else
				{
					Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(2147487748u, null));
				}
			}
			else
			{
				Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(2147487748u, null));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_CLAN_WAR_JOIN_ROOM_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
