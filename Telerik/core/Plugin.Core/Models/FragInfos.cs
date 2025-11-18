using Plugin.Core.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class FragInfos
	{
		public byte Flag
		{
			get;
			set;
		}

		public List<FragModel> Frags
		{
			get;
			set;
		}

		public byte KillerSlot
		{
			get;
			set;
		}

		public CharaKillType KillingType
		{
			get;
			set;
		}

		public byte KillsCount
		{
			get;
			set;
		}

		public int Score
		{
			get;
			set;
		}

		public byte Unk
		{
			get;
			set;
		}

		public int WeaponId
		{
			get;
			set;
		}

		public float X
		{
			get;
			set;
		}

		public float Y
		{
			get;
			set;
		}

		public float Z
		{
			get;
			set;
		}

		public FragInfos()
		{
			this.Frags = new List<FragModel>();
		}

		public KillingMessage GetAllKillFlags()
		{
			KillingMessage killFlag = KillingMessage.None;
			foreach (FragModel frag in this.Frags)
			{
				if (killFlag.HasFlag(frag.KillFlag))
				{
					continue;
				}
				killFlag |= frag.KillFlag;
			}
			return killFlag;
		}
	}
}