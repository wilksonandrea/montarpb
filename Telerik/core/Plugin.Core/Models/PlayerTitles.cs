using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class PlayerTitles
	{
		public int Equiped1
		{
			get;
			set;
		}

		public int Equiped2
		{
			get;
			set;
		}

		public int Equiped3
		{
			get;
			set;
		}

		public long Flags
		{
			get;
			set;
		}

		public long OwnerId
		{
			get;
			set;
		}

		public int Slots
		{
			get;
			set;
		}

		public PlayerTitles()
		{
			this.Slots = 1;
		}

		public long Add(long flag)
		{
			this.Flags = this.Flags | flag;
			return this.Flags;
		}

		public bool Contains(long flag)
		{
			if ((this.Flags & flag) == flag)
			{
				return true;
			}
			return flag == 0L;
		}

		public int GetEquip(int index)
		{
			if (index == 0)
			{
				return this.Equiped1;
			}
			if (index == 1)
			{
				return this.Equiped2;
			}
			if (index != 2)
			{
				return 0;
			}
			return this.Equiped3;
		}

		public void SetEquip(int index, int value)
		{
			if (index == 0)
			{
				this.Equiped1 = value;
				return;
			}
			if (index == 1)
			{
				this.Equiped2 = value;
				return;
			}
			if (index == 2)
			{
				this.Equiped3 = value;
			}
		}
	}
}