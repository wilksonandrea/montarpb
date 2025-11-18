using System;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Proxies
{
	// Token: 0x02000805 RID: 2053
	internal class AgileAsyncWorkerItem
	{
		// Token: 0x06005871 RID: 22641 RVA: 0x00137C8F File Offset: 0x00135E8F
		[SecurityCritical]
		public AgileAsyncWorkerItem(IMethodCallMessage message, AsyncResult ar, object target)
		{
			this._message = new MethodCall(message);
			this._ar = ar;
			this._target = target;
		}

		// Token: 0x06005872 RID: 22642 RVA: 0x00137CB1 File Offset: 0x00135EB1
		[SecurityCritical]
		public static void ThreadPoolCallBack(object o)
		{
			((AgileAsyncWorkerItem)o).DoAsyncCall();
		}

		// Token: 0x06005873 RID: 22643 RVA: 0x00137CBE File Offset: 0x00135EBE
		[SecurityCritical]
		public void DoAsyncCall()
		{
			new StackBuilderSink(this._target).AsyncProcessMessage(this._message, this._ar);
		}

		// Token: 0x04002856 RID: 10326
		private IMethodCallMessage _message;

		// Token: 0x04002857 RID: 10327
		private AsyncResult _ar;

		// Token: 0x04002858 RID: 10328
		private object _target;
	}
}
