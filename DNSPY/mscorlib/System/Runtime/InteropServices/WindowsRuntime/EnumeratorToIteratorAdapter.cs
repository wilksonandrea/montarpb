using System;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009D5 RID: 2517
	internal sealed class EnumeratorToIteratorAdapter<T> : IIterator<T>, IBindableIterator
	{
		// Token: 0x06006409 RID: 25609 RVA: 0x00154EDC File Offset: 0x001530DC
		internal EnumeratorToIteratorAdapter(IEnumerator<T> enumerator)
		{
			this.m_enumerator = enumerator;
		}

		// Token: 0x1700114D RID: 4429
		// (get) Token: 0x0600640A RID: 25610 RVA: 0x00154EF2 File Offset: 0x001530F2
		public T Current
		{
			get
			{
				if (this.m_firstItem)
				{
					this.m_firstItem = false;
					this.MoveNext();
				}
				if (!this.m_hasCurrent)
				{
					throw WindowsRuntimeMarshal.GetExceptionForHR(-2147483637, null);
				}
				return this.m_enumerator.Current;
			}
		}

		// Token: 0x1700114E RID: 4430
		// (get) Token: 0x0600640B RID: 25611 RVA: 0x00154F29 File Offset: 0x00153129
		object IBindableIterator.Current
		{
			get
			{
				return ((IIterator<T>)this).Current;
			}
		}

		// Token: 0x1700114F RID: 4431
		// (get) Token: 0x0600640C RID: 25612 RVA: 0x00154F36 File Offset: 0x00153136
		public bool HasCurrent
		{
			get
			{
				if (this.m_firstItem)
				{
					this.m_firstItem = false;
					this.MoveNext();
				}
				return this.m_hasCurrent;
			}
		}

		// Token: 0x0600640D RID: 25613 RVA: 0x00154F54 File Offset: 0x00153154
		public bool MoveNext()
		{
			try
			{
				this.m_hasCurrent = this.m_enumerator.MoveNext();
			}
			catch (InvalidOperationException ex)
			{
				throw WindowsRuntimeMarshal.GetExceptionForHR(-2147483636, ex);
			}
			return this.m_hasCurrent;
		}

		// Token: 0x0600640E RID: 25614 RVA: 0x00154F98 File Offset: 0x00153198
		public int GetMany(T[] items)
		{
			if (items == null)
			{
				return 0;
			}
			int num = 0;
			while (num < items.Length && this.HasCurrent)
			{
				items[num] = this.Current;
				this.MoveNext();
				num++;
			}
			if (typeof(T) == typeof(string))
			{
				string[] array = items as string[];
				for (int i = num; i < items.Length; i++)
				{
					array[i] = string.Empty;
				}
			}
			return num;
		}

		// Token: 0x04002CF3 RID: 11507
		private IEnumerator<T> m_enumerator;

		// Token: 0x04002CF4 RID: 11508
		private bool m_firstItem = true;

		// Token: 0x04002CF5 RID: 11509
		private bool m_hasCurrent;
	}
}
