using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class SlotChange
	{
		public SlotModel NewSlot
		{
			get;
			set;
		}

		public SlotModel OldSlot
		{
			get;
			set;
		}

		public SlotChange(SlotModel slotModel_2, SlotModel slotModel_3)
		{
			this.OldSlot = slotModel_2;
			this.NewSlot = slotModel_3;
		}
	}
}