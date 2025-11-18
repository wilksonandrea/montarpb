using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x02000002 RID: 2
[CompilerGenerated]
internal sealed class <>f__AnonymousType0<<message>j__TPar>
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	public <message>j__TPar message
	{
		get
		{
			return this.<message>i__Field;
		}
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
	[DebuggerHidden]
	public <>f__AnonymousType0(<message>j__TPar message)
	{
		this.<message>i__Field = message;
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002068 File Offset: 0x00000268
	[DebuggerHidden]
	public override bool Equals(object value)
	{
		var <>f__AnonymousType = value as <>f__AnonymousType0<<message>j__TPar>;
		return this == <>f__AnonymousType || (<>f__AnonymousType != null && EqualityComparer<<message>j__TPar>.Default.Equals(this.<message>i__Field, <>f__AnonymousType.<message>i__Field));
	}

	// Token: 0x06000004 RID: 4 RVA: 0x0000209D File Offset: 0x0000029D
	[DebuggerHidden]
	public override int GetHashCode()
	{
		return -1401644745 * -1521134295 + EqualityComparer<<message>j__TPar>.Default.GetHashCode(this.<message>i__Field);
	}

	// Token: 0x06000005 RID: 5 RVA: 0x000020BC File Offset: 0x000002BC
	[DebuggerHidden]
	public override string ToString()
	{
		IFormatProvider formatProvider = null;
		string text = "{{ message = {0} }}";
		object[] array = new object[1];
		int num = 0;
		<message>j__TPar <message>j__TPar = this.<message>i__Field;
		array[num] = ((<message>j__TPar != null) ? <message>j__TPar.ToString() : null);
		return string.Format(formatProvider, text, array);
	}

	// Token: 0x04000001 RID: 1
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <message>j__TPar <message>i__Field;
}
