using System;
using System.Runtime.CompilerServices;

namespace Server.Auth.Network.ClientPacket
{
	public class Mission
	{
		public string Description
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public byte[] ObjectivesData
		{
			get;
			set;
		}

		public int RewardCount
		{
			get;
			set;
		}

		public int RewardId
		{
			get;
			set;
		}

		public Mission()
		{
		}
	}
}