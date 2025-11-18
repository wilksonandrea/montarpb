using System.Runtime.CompilerServices;

namespace Plugin.Core.Models;

public class SlotChange
{
	[CompilerGenerated]
	private SlotModel slotModel_0;

	[CompilerGenerated]
	private SlotModel slotModel_1;

	public SlotModel OldSlot
	{
		[CompilerGenerated]
		get
		{
			return slotModel_0;
		}
		[CompilerGenerated]
		set
		{
			slotModel_0 = value;
		}
	}

	public SlotModel NewSlot
	{
		[CompilerGenerated]
		get
		{
			return slotModel_1;
		}
		[CompilerGenerated]
		set
		{
			slotModel_1 = value;
		}
	}

	public SlotChange(SlotModel slotModel_2, SlotModel slotModel_3)
	{
		OldSlot = slotModel_2;
		NewSlot = slotModel_3;
	}
}
