using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x02000002 RID: 2
[CompilerGenerated]
internal sealed class Class0<T, U>
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000006 RID: 6 RVA: 0x0000207C File Offset: 0x0000027C
	public T item
	{
		get
		{
			return this.gparam_0;
		}
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x06000007 RID: 7 RVA: 0x00002084 File Offset: 0x00000284
	public U inx
	{
		get
		{
			return this.gparam_1;
		}
	}

	// Token: 0x06000008 RID: 8 RVA: 0x0000208C File Offset: 0x0000028C
	[DebuggerHidden]
	public Class0(T gparam_2, U gparam_3)
	{
		this.gparam_0 = gparam_2;
		this.gparam_1 = gparam_3;
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00008050 File Offset: 0x00006250
	[DebuggerHidden]
	public bool Equals(object obj)
	{
		Class0<T, U> @class = obj as Class0<T, U>;
		return this == @class || (@class != null && EqualityComparer<T>.Default.Equals(this.gparam_0, @class.gparam_0) && EqualityComparer<U>.Default.Equals(this.gparam_1, @class.gparam_1));
	}

	// Token: 0x0600000A RID: 10 RVA: 0x000020A2 File Offset: 0x000002A2
	[DebuggerHidden]
	public int GetHashCode()
	{
		return (-1959725626 + EqualityComparer<T>.Default.GetHashCode(this.gparam_0)) * -1521134295 + EqualityComparer<U>.Default.GetHashCode(this.gparam_1);
	}

	// Token: 0x0600000B RID: 11 RVA: 0x000080A0 File Offset: 0x000062A0
	[DebuggerHidden]
	public string ToString()
	{
		IFormatProvider formatProvider = null;
		string text = "{{ item = {0}, inx = {1} }}";
		object[] array = new object[2];
		int num = 0;
		T t = this.gparam_0;
		array[num] = ((t != null) ? t.ToString() : null);
		int num2 = 1;
		U u = this.gparam_1;
		array[num2] = ((u != null) ? u.ToString() : null);
		return string.Format(formatProvider, text, array);
	}

	// Token: 0x04000001 RID: 1
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly T gparam_0;

	// Token: 0x04000002 RID: 2
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly U gparam_1;
}
