using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Auth.Network.ServerPacket;

namespace Server.Auth.Network.ClientPacket
{
	// Token: 0x02000035 RID: 53
	public class PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_REQ : AuthClientPacket
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x00002A8B File Offset: 0x00000C8B
		static PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_REQ()
		{
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00002A1B File Offset: 0x00000C1B
		public override void Read()
		{
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00006E88 File Offset: 0x00005088
		public override void Run()
		{
			try
			{
				if (this.Client.Player != null)
				{
					foreach (Mission mission in PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_REQ.list_0)
					{
						byte[] array = this.method_0(mission);
						this.Client.SendPacket(new PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_ACK(array, (short)PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_REQ.list_0.Count, 1));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00006F28 File Offset: 0x00005128
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

		// Token: 0x060000B8 RID: 184 RVA: 0x00006FFC File Offset: 0x000051FC
		private static List<Mission> smethod_0(string string_0)
		{
			List<Mission> list = new List<Mission>();
			try
			{
				if (!Directory.Exists(string_0))
				{
					CLogger.Print("Directory not found: " + string_0, LoggerType.Warning, null);
					return list;
				}
				string[] files = Directory.GetFiles(string_0, "*.hex");
				for (int i = 0; i < files.Length; i++)
				{
					string[] array = File.ReadAllLines(files[i]);
					if (array.Length >= 6)
					{
						Mission mission = new Mission
						{
							Id = int.Parse(array[0].Split(new char[] { '=' }, 2)[1]),
							Name = array[1].Split(new char[] { '=' }, 2)[1],
							Description = array[2].Split(new char[] { '=' }, 2)[1],
							RewardId = int.Parse(array[3].Split(new char[] { '=' }, 2)[1]),
							RewardCount = int.Parse(array[4].Split(new char[] { '=' }, 2)[1])
						};
						string[] array2 = string.Join(" ", array.Skip(5)).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
						List<byte> list2 = new List<byte>();
						foreach (string text in array2)
						{
							list2.Add(Convert.ToByte(text, 16));
						}
						mission.ObjectivesData = list2.ToArray();
						list.Add(mission);
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("Error loading missions: " + ex.Message, LoggerType.Error, ex);
			}
			return list.OrderBy(new Func<Mission, int>(PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_REQ.Class2.<>9.method_0)).ToList<Mission>();
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00002A1D File Offset: 0x00000C1D
		public PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_REQ()
		{
		}

		// Token: 0x0400006E RID: 110
		private static readonly List<Mission> list_0 = PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_REQ.smethod_0("Data/Missions/Basic");

		// Token: 0x02000036 RID: 54
		[CompilerGenerated]
		[Serializable]
		private sealed class Class2
		{
			// Token: 0x060000BA RID: 186 RVA: 0x00002A9C File Offset: 0x00000C9C
			// Note: this type is marked as 'beforefieldinit'.
			static Class2()
			{
			}

			// Token: 0x060000BB RID: 187 RVA: 0x00002409 File Offset: 0x00000609
			public Class2()
			{
			}

			// Token: 0x060000BC RID: 188 RVA: 0x00002AA8 File Offset: 0x00000CA8
			internal int method_0(Mission mission_0)
			{
				return mission_0.Id;
			}

			// Token: 0x0400006F RID: 111
			public static readonly PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_REQ.Class2 <>9 = new PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_REQ.Class2();

			// Token: 0x04000070 RID: 112
			public static Func<Mission, int> <>9__5_0;
		}
	}
}
