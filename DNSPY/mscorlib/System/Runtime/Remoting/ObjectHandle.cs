using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Lifetime;
using System.Security;

namespace System.Runtime.Remoting
{
	// Token: 0x020007D1 RID: 2001
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[ComVisible(true)]
	public class ObjectHandle : MarshalByRefObject, IObjectHandle
	{
		// Token: 0x060056CC RID: 22220 RVA: 0x0013416F File Offset: 0x0013236F
		private ObjectHandle()
		{
		}

		// Token: 0x060056CD RID: 22221 RVA: 0x00134177 File Offset: 0x00132377
		public ObjectHandle(object o)
		{
			this.WrappedObject = o;
		}

		// Token: 0x060056CE RID: 22222 RVA: 0x00134186 File Offset: 0x00132386
		public object Unwrap()
		{
			return this.WrappedObject;
		}

		// Token: 0x060056CF RID: 22223 RVA: 0x00134190 File Offset: 0x00132390
		[SecurityCritical]
		public override object InitializeLifetimeService()
		{
			MarshalByRefObject marshalByRefObject = this.WrappedObject as MarshalByRefObject;
			if (marshalByRefObject != null && marshalByRefObject.InitializeLifetimeService() == null)
			{
				return null;
			}
			return (ILease)base.InitializeLifetimeService();
		}

		// Token: 0x040027BC RID: 10172
		private object WrappedObject;
	}
}
