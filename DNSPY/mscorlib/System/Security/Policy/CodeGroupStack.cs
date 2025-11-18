using System;
using System.Collections;

namespace System.Security.Policy
{
	// Token: 0x02000367 RID: 871
	internal sealed class CodeGroupStack
	{
		// Token: 0x06002B18 RID: 11032 RVA: 0x000A07DF File Offset: 0x0009E9DF
		internal CodeGroupStack()
		{
			this.m_array = new ArrayList();
		}

		// Token: 0x06002B19 RID: 11033 RVA: 0x000A07F2 File Offset: 0x0009E9F2
		internal void Push(CodeGroupStackFrame element)
		{
			this.m_array.Add(element);
		}

		// Token: 0x06002B1A RID: 11034 RVA: 0x000A0804 File Offset: 0x0009EA04
		internal CodeGroupStackFrame Pop()
		{
			if (this.IsEmpty())
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EmptyStack"));
			}
			int count = this.m_array.Count;
			CodeGroupStackFrame codeGroupStackFrame = (CodeGroupStackFrame)this.m_array[count - 1];
			this.m_array.RemoveAt(count - 1);
			return codeGroupStackFrame;
		}

		// Token: 0x06002B1B RID: 11035 RVA: 0x000A0858 File Offset: 0x0009EA58
		internal bool IsEmpty()
		{
			return this.m_array.Count == 0;
		}

		// Token: 0x04001190 RID: 4496
		private ArrayList m_array;
	}
}
