using System.Runtime.CompilerServices;
using Plugin.Core.Enums;

namespace Plugin.Core.Models;

public class CouponFlag
{
	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private CouponEffects couponEffects_0;

	public int ItemId
	{
		[CompilerGenerated]
		get
		{
			return int_0;
		}
		[CompilerGenerated]
		set
		{
			int_0 = value;
		}
	}

	public CouponEffects EffectFlag
	{
		[CompilerGenerated]
		get
		{
			return couponEffects_0;
		}
		[CompilerGenerated]
		set
		{
			couponEffects_0 = value;
		}
	}
}
