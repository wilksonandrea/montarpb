using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004E5 RID: 1253
	[__DynamicallyInvokable]
	public sealed class AsyncLocal<T> : IAsyncLocal
	{
		// Token: 0x06003B7B RID: 15227 RVA: 0x000E2203 File Offset: 0x000E0403
		[__DynamicallyInvokable]
		public AsyncLocal()
		{
		}

		// Token: 0x06003B7C RID: 15228 RVA: 0x000E220B File Offset: 0x000E040B
		[SecurityCritical]
		[__DynamicallyInvokable]
		public AsyncLocal(Action<AsyncLocalValueChangedArgs<T>> valueChangedHandler)
		{
			this.m_valueChangedHandler = valueChangedHandler;
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06003B7D RID: 15229 RVA: 0x000E221C File Offset: 0x000E041C
		// (set) Token: 0x06003B7E RID: 15230 RVA: 0x000E2243 File Offset: 0x000E0443
		[__DynamicallyInvokable]
		public T Value
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				object localValue = ExecutionContext.GetLocalValue(this);
				if (localValue != null)
				{
					return (T)((object)localValue);
				}
				return default(T);
			}
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			set
			{
				ExecutionContext.SetLocalValue(this, value, this.m_valueChangedHandler != null);
			}
		}

		// Token: 0x06003B7F RID: 15231 RVA: 0x000E225C File Offset: 0x000E045C
		[SecurityCritical]
		void IAsyncLocal.OnValueChanged(object previousValueObj, object currentValueObj, bool contextChanged)
		{
			T t = ((previousValueObj == null) ? default(T) : ((T)((object)previousValueObj)));
			T t2 = ((currentValueObj == null) ? default(T) : ((T)((object)currentValueObj)));
			this.m_valueChangedHandler(new AsyncLocalValueChangedArgs<T>(t, t2, contextChanged));
		}

		// Token: 0x04001967 RID: 6503
		[SecurityCritical]
		private readonly Action<AsyncLocalValueChangedArgs<T>> m_valueChangedHandler;
	}
}
