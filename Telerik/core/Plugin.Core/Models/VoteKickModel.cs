using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class VoteKickModel
	{
		public int Accept
		{
			get;
			set;
		}

		public int Allies
		{
			get;
			set;
		}

		public int CreatorIdx
		{
			get;
			set;
		}

		public int Denie
		{
			get;
			set;
		}

		public int Enemies
		{
			get;
			set;
		}

		public int Motive
		{
			get;
			set;
		}

		public bool[] TotalArray
		{
			get;
			set;
		}

		public int VictimIdx
		{
			get;
			set;
		}

		public List<int> Votes
		{
			get;
			set;
		}

		public VoteKickModel(int int_7, int int_8)
		{
			this.Accept = 1;
			this.Denie = 1;
			this.CreatorIdx = int_7;
			this.VictimIdx = int_8;
			this.Votes = new List<int>()
			{
				int_7,
				int_8
			};
			this.TotalArray = new bool[18];
		}

		public int GetInGamePlayers()
		{
			int ınt32 = 0;
			for (int i = 0; i < 18; i++)
			{
				if (this.TotalArray[i])
				{
					ınt32++;
				}
			}
			return ınt32;
		}
	}
}