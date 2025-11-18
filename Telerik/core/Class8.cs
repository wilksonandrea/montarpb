using System;

internal class Class8
{
	public int[] int_0;

	private readonly int[] int_1;

	public Class8(Array array_0)
	{
		this.int_1 = new int[array_0.Rank];
		for (int i = 0; i < array_0.Rank; i++)
		{
			this.int_1[i] = array_0.GetLength(i) - 1;
		}
		this.int_0 = new int[array_0.Rank];
	}

	public bool method_0()
	{
		for (int i = 0; i < (int)this.int_0.Length; i++)
		{
			if (this.int_0[i] < this.int_1[i])
			{
				this.int_0[i]++;
				for (int j = 0; j < i; j++)
				{
					this.int_0[j] = 0;
				}
				return true;
			}
		}
		return false;
	}
}