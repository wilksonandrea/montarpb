using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200088D RID: 2189
	[SecurityCritical]
	[ComVisible(true)]
	[Serializable]
	public sealed class CallContext
	{
		// Token: 0x06005CB2 RID: 23730 RVA: 0x00144E0C File Offset: 0x0014300C
		private CallContext()
		{
		}

		// Token: 0x06005CB3 RID: 23731 RVA: 0x00144E14 File Offset: 0x00143014
		internal static LogicalCallContext SetLogicalCallContext(LogicalCallContext callCtx)
		{
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			LogicalCallContext logicalCallContext = mutableExecutionContext.LogicalCallContext;
			mutableExecutionContext.LogicalCallContext = callCtx;
			return logicalCallContext;
		}

		// Token: 0x06005CB4 RID: 23732 RVA: 0x00144E3C File Offset: 0x0014303C
		[SecurityCritical]
		public static void FreeNamedDataSlot(string name)
		{
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			mutableExecutionContext.LogicalCallContext.FreeNamedDataSlot(name);
			mutableExecutionContext.IllogicalCallContext.FreeNamedDataSlot(name);
		}

		// Token: 0x06005CB5 RID: 23733 RVA: 0x00144E6C File Offset: 0x0014306C
		[SecurityCritical]
		public static object LogicalGetData(string name)
		{
			return Thread.CurrentThread.GetExecutionContextReader().LogicalCallContext.GetData(name);
		}

		// Token: 0x06005CB6 RID: 23734 RVA: 0x00144E94 File Offset: 0x00143094
		private static object IllogicalGetData(string name)
		{
			return Thread.CurrentThread.GetExecutionContextReader().IllogicalCallContext.GetData(name);
		}

		// Token: 0x17000FEB RID: 4075
		// (get) Token: 0x06005CB7 RID: 23735 RVA: 0x00144EBC File Offset: 0x001430BC
		// (set) Token: 0x06005CB8 RID: 23736 RVA: 0x00144EE3 File Offset: 0x001430E3
		internal static IPrincipal Principal
		{
			[SecurityCritical]
			get
			{
				return Thread.CurrentThread.GetExecutionContextReader().LogicalCallContext.Principal;
			}
			[SecurityCritical]
			set
			{
				Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext.Principal = value;
			}
		}

		// Token: 0x17000FEC RID: 4076
		// (get) Token: 0x06005CB9 RID: 23737 RVA: 0x00144EFC File Offset: 0x001430FC
		// (set) Token: 0x06005CBA RID: 23738 RVA: 0x00144F38 File Offset: 0x00143138
		public static object HostContext
		{
			[SecurityCritical]
			get
			{
				ExecutionContext.Reader executionContextReader = Thread.CurrentThread.GetExecutionContextReader();
				object obj = executionContextReader.IllogicalCallContext.HostContext;
				if (obj == null)
				{
					obj = executionContextReader.LogicalCallContext.HostContext;
				}
				return obj;
			}
			[SecurityCritical]
			set
			{
				ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
				if (value is ILogicalThreadAffinative)
				{
					mutableExecutionContext.IllogicalCallContext.HostContext = null;
					mutableExecutionContext.LogicalCallContext.HostContext = value;
					return;
				}
				mutableExecutionContext.IllogicalCallContext.HostContext = value;
				mutableExecutionContext.LogicalCallContext.HostContext = null;
			}
		}

		// Token: 0x06005CBB RID: 23739 RVA: 0x00144F8C File Offset: 0x0014318C
		[SecurityCritical]
		public static object GetData(string name)
		{
			object obj = CallContext.LogicalGetData(name);
			if (obj == null)
			{
				return CallContext.IllogicalGetData(name);
			}
			return obj;
		}

		// Token: 0x06005CBC RID: 23740 RVA: 0x00144FAC File Offset: 0x001431AC
		[SecurityCritical]
		public static void SetData(string name, object data)
		{
			if (data is ILogicalThreadAffinative)
			{
				CallContext.LogicalSetData(name, data);
				return;
			}
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			mutableExecutionContext.LogicalCallContext.FreeNamedDataSlot(name);
			mutableExecutionContext.IllogicalCallContext.SetData(name, data);
		}

		// Token: 0x06005CBD RID: 23741 RVA: 0x00144FF0 File Offset: 0x001431F0
		[SecurityCritical]
		public static void LogicalSetData(string name, object data)
		{
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			mutableExecutionContext.IllogicalCallContext.FreeNamedDataSlot(name);
			mutableExecutionContext.LogicalCallContext.SetData(name, data);
		}

		// Token: 0x06005CBE RID: 23742 RVA: 0x00145024 File Offset: 0x00143224
		[SecurityCritical]
		public static Header[] GetHeaders()
		{
			LogicalCallContext logicalCallContext = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
			return logicalCallContext.InternalGetHeaders();
		}

		// Token: 0x06005CBF RID: 23743 RVA: 0x00145048 File Offset: 0x00143248
		[SecurityCritical]
		public static void SetHeaders(Header[] headers)
		{
			LogicalCallContext logicalCallContext = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
			logicalCallContext.InternalSetHeaders(headers);
		}
	}
}
