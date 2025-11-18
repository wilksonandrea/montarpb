using System;

// Token: 0x02000036 RID: 54
internal class Class8
{
	// Token: 0x060001E3 RID: 483 RVA: 0x00018B34 File Offset: 0x00016D34
	public Class8(Array array_0)
	{
		this.int_1 = new int[array_0.Rank];
		for (int i = 0; i < array_0.Rank; i++)
		{
			this.int_1[i] = array_0.GetLength(i) - 1;
		}
		this.int_0 = new int[array_0.Rank];
	}

	// Token: 0x060001E4 RID: 484 RVA: 0x00018B8C File Offset: 0x00016D8C
	public bool method_0()
	{
		for (int i = 0; i < this.int_0.Length; i++)
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

	// Token: 0x04000098 RID: 152
	public int[] int_0;

	// Token: 0x04000099 RID: 153
	private readonly int[] int_1;
}
