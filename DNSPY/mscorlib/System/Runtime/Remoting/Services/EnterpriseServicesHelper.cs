using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Security;

namespace System.Runtime.Remoting.Services
{
	// Token: 0x02000807 RID: 2055
	[SecurityCritical]
	[ComVisible(true)]
	public sealed class EnterpriseServicesHelper
	{
		// Token: 0x06005875 RID: 22645 RVA: 0x00137CF4 File Offset: 0x00135EF4
		[SecurityCritical]
		public static object WrapIUnknownWithComObject(IntPtr punk)
		{
			return Marshal.InternalWrapIUnknownWithComObject(punk);
		}

		// Token: 0x06005876 RID: 22646 RVA: 0x00137CFC File Offset: 0x00135EFC
		[ComVisible(true)]
		public static IConstructionReturnMessage CreateConstructionReturnMessage(IConstructionCallMessage ctorMsg, MarshalByRefObject retObj)
		{
			return new ConstructorReturnMessage(retObj, null, 0, null, ctorMsg);
		}

		// Token: 0x06005877 RID: 22647 RVA: 0x00137D18 File Offset: 0x00135F18
		[SecurityCritical]
		public static void SwitchWrappers(RealProxy oldcp, RealProxy newcp)
		{
			object transparentProxy = oldcp.GetTransparentProxy();
			object transparentProxy2 = newcp.GetTransparentProxy();
			IntPtr serverContextForProxy = RemotingServices.GetServerContextForProxy(transparentProxy);
			IntPtr serverContextForProxy2 = RemotingServices.GetServerContextForProxy(transparentProxy2);
			Marshal.InternalSwitchCCW(transparentProxy, transparentProxy2);
		}

		// Token: 0x06005878 RID: 22648 RVA: 0x00137D49 File Offset: 0x00135F49
		public EnterpriseServicesHelper()
		{
		}
	}
}
