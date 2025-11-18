using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009F2 RID: 2546
	internal sealed class IteratorToEnumeratorAdapter<T> : IEnumerator<T>, IDisposable, IEnumerator
	{
		// Token: 0x060064BE RID: 25790 RVA: 0x00156F88 File Offset: 0x00155188
		internal IteratorToEnumeratorAdapter(IIterator<T> iterator)
		{
			this.m_iterator = iterator;
			this.m_hadCurrent = true;
			this.m_isInitialized = false;
		}

		// Token: 0x17001157 RID: 4439
		// (get) Token: 0x060064BF RID: 25791 RVA: 0x00156FA5 File Offset: 0x001551A5
		public T Current
		{
			get
			{
				if (!this.m_isInitialized)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumNotStarted);
				}
				if (!this.m_hadCurrent)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumEnded);
				}
				return this.m_current;
			}
		}

		// Token: 0x17001158 RID: 4440
		// (get) Token: 0x060064C0 RID: 25792 RVA: 0x00156FCB File Offset: 0x001551CB
		object IEnumerator.Current
		{
			get
			{
				if (!this.m_isInitialized)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumNotStarted);
				}
				if (!this.m_hadCurrent)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumEnded);
				}
				return this.m_current;
			}
		}

		// Token: 0x060064C1 RID: 25793 RVA: 0x00156FF8 File Offset: 0x001551F8
		[SecuritySafeCritical]
		public bool MoveNext()
		{
			if (!this.m_hadCurrent)
			{
				return false;
			}
			try
			{
				if (!this.m_isInitialized)
				{
					this.m_hadCurrent = this.m_iterator.HasCurrent;
					this.m_isInitialized = true;
				}
				else
				{
					this.m_hadCurrent = this.m_iterator.MoveNext();
				}
				if (this.m_hadCurrent)
				{
					this.m_current = this.m_iterator.Current;
				}
			}
			catch (Exception ex)
			{
				if (Marshal.GetHRForException(ex) != -2147483636)
				{
					throw;
				}
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
			}
			return this.m_hadCurrent;
		}

		// Token: 0x060064C2 RID: 25794 RVA: 0x00157090 File Offset: 0x00155290
		public void Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060064C3 RID: 25795 RVA: 0x00157097 File Offset: 0x00155297
		public void Dispose()
		{
		}

		// Token: 0x04002D00 RID: 11520
		private IIterator<T> m_iterator;

		// Token: 0x04002D01 RID: 11521
		private bool m_hadCurrent;

		// Token: 0x04002D02 RID: 11522
		private T m_current;

		// Token: 0x04002D03 RID: 11523
		private bool m_isInitialized;
	}
}
