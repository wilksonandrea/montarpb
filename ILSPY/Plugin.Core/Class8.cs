using System;

internal class Class8
{
	public int[] int_0;

	private readonly int[] int_1;

	public Class8(Array array_0)
	{
		int_1 = new int[array_0.Rank];
		for (int i = 0; i < array_0.Rank; i++)
		{
			int_1[i] = array_0.GetLength(i) - 1;
		}
		int_0 = new int[array_0.Rank];
	}

	public bool method_0()
	{
		int num = 0;
		while (true)
		{
			if (num < int_0.Length)
			{
				if (int_0[num] < int_1[num])
				{
					break;
				}
				num++;
				continue;
			}
			return false;
		}
		int_0[num]++;
		for (int i = 0; i < num; i++)
		{
			int_0[i] = 0;
		}
		return true;
	}
}
