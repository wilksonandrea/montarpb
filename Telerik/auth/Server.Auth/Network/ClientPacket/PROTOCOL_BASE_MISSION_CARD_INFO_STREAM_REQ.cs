using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Auth;
using Server.Auth.Network;
using Server.Auth.Network.ServerPacket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Server.Auth.Network.ClientPacket
{
	public class PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_REQ : AuthClientPacket
	{
		private readonly static List<Mission> list_0;

		static PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_REQ()
		{
			PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_REQ.list_0 = PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_REQ.smethod_0("Data/Missions/Basic");
		}

		public PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_REQ()
		{
		}

		private byte[] method_0(Mission mission_0)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteC((byte)mission_0.Id);
				syncServerPacket.WriteC((byte)mission_0.Name.Length);
				syncServerPacket.WriteN(mission_0.Name, mission_0.Name.Length, "UTF-16LE");
				syncServerPacket.WriteC((byte)mission_0.Description.Length);
				syncServerPacket.WriteN(mission_0.Description, mission_0.Description.Length, "UTF-16LE");
				syncServerPacket.WriteB(mission_0.ObjectivesData);
				syncServerPacket.WriteD(mission_0.RewardId);
				syncServerPacket.WriteD(mission_0.RewardCount);
				syncServerPacket.WriteB(new byte[32]);
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		public override void Read()
		{
		}

		public override void Run()
		{
			try
			{
				if (this.Client.Player != null)
				{
					foreach (Mission list0 in PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_REQ.list_0)
					{
						byte[] numArray = this.method_0(list0);
						this.Client.SendPacket(new PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_ACK(numArray, (short)PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_REQ.list_0.Count, 1));
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}

		private static List<Mission> smethod_0(string string_0)
		{
			List<Mission> missions = new List<Mission>();
			try
			{
				if (Directory.Exists(string_0))
				{
					string[] files = Directory.GetFiles(string_0, "*.hex");
					for (int i = 0; i < (int)files.Length; i++)
					{
						string[] strArrays = File.ReadAllLines(files[i]);
						if ((int)strArrays.Length >= 6)
						{
							Mission mission = new Mission()
							{
								Id = int.Parse(strArrays[0].Split(new char[] { '=' }, 2)[1]),
								Name = strArrays[1].Split(new char[] { '=' }, 2)[1],
								Description = strArrays[2].Split(new char[] { '=' }, 2)[1],
								RewardId = int.Parse(strArrays[3].Split(new char[] { '=' }, 2)[1]),
								RewardCount = int.Parse(strArrays[4].Split(new char[] { '=' }, 2)[1])
							};
							Mission array = mission;
							string[] strArrays1 = string.Join(" ", strArrays.Skip<string>(5)).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
							List<byte> nums = new List<byte>();
							string[] strArrays2 = strArrays1;
							for (int j = 0; j < (int)strArrays2.Length; j++)
							{
								nums.Add(Convert.ToByte(strArrays2[j], 16));
							}
							array.ObjectivesData = nums.ToArray();
							missions.Add(array);
						}
					}
				}
				else
				{
					CLogger.Print(string.Concat("Directory not found: ", string_0), LoggerType.Warning, null);
					return missions;
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("Error loading missions: ", exception.Message), LoggerType.Error, exception);
			}
			return (
				from mission_0 in missions
				orderby mission_0.Id
				select mission_0).ToList<Mission>();
		}
	}
}